using System.Security.Claims;
using AutoFixture;
using FluentAssertions;
using MassTransit;
using Microsoft.Extensions.Time.Testing;
using Moq;
using Pingo.Identity.Events;
using Pingo.Identity.Service.Entity.Models;
using Pingo.Identity.Service.Entity.Requests;
using Pingo.Identity.Service.Implementations;
using Pingo.Identity.Service.Interface;

namespace Pingo.Identity.Service.Tests;

public sealed class IdentityServiceTests
{
    private readonly IdentityService _service;
    private readonly Fixture _fixture;
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
        _fixture = new Fixture();
        var fakeTimeProvider = new FakeTimeProvider();

        _service = new IdentityService(
            _userRepoMock.Object, _passwordHasherMock.Object, _tokenMock.Object, fakeTimeProvider,
            _publishEndpointMock.Object);
    }

    [Fact]
    public async Task LoginAsync_ValidCredentials_ReturnsTokensAndPublishesEvent()
    {
        // Arrange
        var user = _fixture.Create<User>();
        var authRequest = _fixture.Create<AuthRequest>();
        var tokenData = _fixture.Create<TokenData>();

        _userRepoMock.Setup(r => r.GetUserAsync(authRequest.Email))
            .ReturnsAsync(user);

        _passwordHasherMock.Setup(r => r.VerifyPassword(authRequest.Password, user.PasswordHash))
            .Returns(value: true);

        _tokenMock.Setup(r => r.GenerateAccessToken(It.IsAny<List<Claim>>()))
            .Returns(tokenData.Token.ToString);

        // Act
        var result = await _service.LoginAsync(authRequest);

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Value.AccessToken.Should().NotBeNullOrEmpty();
        result.Value.RefreshToken.Should().NotBe(Guid.Empty);

        _publishEndpointMock.Verify(p => p.Publish(
            It.Is<UserLoggedIn>(e => e.Email == authRequest.Email),
            It.IsAny<CancellationToken>()),
            Times.Once);
    }

    [Fact]
    public async Task LoginAsync_NonexistentUser_ReturnsError()
    {
        // Arrange
        var authRequest = _fixture.Create<AuthRequest>();

        _userRepoMock.Setup(r => r.GetUserAsync(authRequest.Email))
            .ReturnsAsync((User)null!);

        // Act
        var result = await _service.LoginAsync(authRequest);

        // Assert
        result.IsSuccess.Should().BeFalse();
        result.Error.Should().Be(LoginErrorType.EmailNotFound);

        _publishEndpointMock.VerifyNoOtherCalls();
    }

    [Fact]
    public async Task LoginAsync_InvalidPassword_ReturnsError()
    {
        // Arrange
        var authRequest = _fixture.Create<AuthRequest>();
        var user = _fixture.Create<User>();

        _userRepoMock.Setup(r => r.GetUserAsync(authRequest.Email)).ReturnsAsync(user);

        _passwordHasherMock.Setup(r => r.VerifyPassword(authRequest.Password, user.PasswordHash))
            .Returns(value: false);

        // Act
        var result = await _service.LoginAsync(authRequest);

        // Assert
        result.IsSuccess.Should().BeFalse();
        result.Error.Should().Be(LoginErrorType.InvalidPassword);

        _publishEndpointMock.VerifyNoOtherCalls();
    }
}
