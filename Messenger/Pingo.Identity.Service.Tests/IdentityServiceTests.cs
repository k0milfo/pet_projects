using System.Security.Claims;
using MassTransit;
using Moq;
using Pingo.Identity.Service.Entity.Models;
using Pingo.Identity.Service.Entity.Requests;
using Pingo.Identity.Service.Implementations;
using Pingo.Identity.Service.Interface;
using Shared;

namespace Pingo.Identity.Service.Tests;

public sealed class IdentityServiceTests
{
    private readonly IdentityService _service;
    private readonly TimeProvider _timeProvider = TimeProvider.System;
    private readonly Mock<IIdentityRepository> _userRepoMock;
    private readonly Mock<IPasswordHasher> _passwordHasherMock;
    private readonly Mock<IPublishEndpoint> _publishEndpointMock;
    private readonly Mock<ITokenService> _tokenMock;

    public IdentityServiceTests()
    {
        _userRepoMock = new Mock<IIdentityRepository>();
        _passwordHasherMock = new Mock<IPasswordHasher>();
        _publishEndpointMock = new Mock<IPublishEndpoint>();
        _tokenMock = new Mock<ITokenService>();

        _service = new IdentityService(
            _userRepoMock.Object, _passwordHasherMock.Object, _tokenMock.Object, TimeProvider.System,
            _publishEndpointMock.Object);
    }

    [Fact]
    public async Task LoginAsync_ValidCredentials_ReturnsTokensAndPublishesEvent()
    {
        // Arrange
        var email = "test@test.com";
        var password = "password";
        var passwordHash = "$2a$11$owr5QCuMrUSnDBUJkUlD7OVMt.0v.n/CHJnZ6gjxeSjtqT5mFDQ0u";
        var id = Guid.NewGuid();
        var time = _timeProvider.GetUtcNow();
        var salt = "$2a$11$owr5QCuMrUSnDBUJkUlD7O";
        var accessToken = "access_token";

        _userRepoMock.Setup(r => r.GetUserAsync(email))
            .ReturnsAsync(new User(
                Id: id,
                RegisteredAt: time,
                Email: email,
                PasswordHash: passwordHash,
                Salt: salt));

        _passwordHasherMock.Setup(r => r.VerifyPassword(password, passwordHash))
            .Returns(value: true);

        _tokenMock.Setup(r => r.GenerateAccessToken(It.IsAny<List<Claim>>()))
            .Returns(accessToken);

        // Act
        var result = await _service.LoginAsync(new AuthRequest(email, password));

        // Assert
        Assert.True(result.IsSuccess);
        Assert.False(string.IsNullOrEmpty(result.Value.AccessToken));
        Assert.False(string.IsNullOrEmpty(result.Value.RefreshToken.ToString()));
        Assert.NotEqual(Guid.Empty, result.Value.RefreshToken);

        _publishEndpointMock.Verify(p => p.Publish(
            It.Is<UserLoggedIn>(e => e.Email == email),
            It.IsAny<CancellationToken>()),
            Times.Once);
    }

    [Fact]
    public async Task LoginAsync_NonexistentUser_ReturnsError()
    {
        // Arrange
        var email = "test@test.com";
        var password = "password";

        _userRepoMock.Setup(r => r.GetUserAsync(email))
            .ReturnsAsync((User)null!);

        var result = await _service.LoginAsync(new AuthRequest(email, password));

        Assert.False(result.IsSuccess);
        Assert.Equal(LoginErrorType.EmailNotFound, result.Error);
    }

    [Fact]
    public async Task LoginAsync_InvalidPassword_ReturnsError()
    {
        // Arrange
        var email = "test@test.com";
        var password = "password";
        var passwordHash = "password123";

        _userRepoMock.Setup(r => r.GetUserAsync(email)).ReturnsAsync(
            new User(
                Id: Guid.NewGuid(),
                RegisteredAt: _timeProvider.GetUtcNow(),
                Email: email,
                PasswordHash: passwordHash,
                Salt: "asdfsdf"));

        _passwordHasherMock.Setup(r => r.VerifyPassword(password, passwordHash))
            .Returns(value: false);

        // Act
        var result = await _service.LoginAsync(new AuthRequest(email, password));

        // Assert
        Assert.False(result.IsSuccess);
        Assert.Equal(LoginErrorType.InvalidPassword, result.Error);
    }
}
