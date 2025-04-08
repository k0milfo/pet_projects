namespace Pingo.Identity.Service;

public enum LoginErrorType
{
    EmailNotFound,
    InvalidPassword,
    InvalidInputData,
    EmailAlreadyExists,
    InvalidRefreshToken,
}
