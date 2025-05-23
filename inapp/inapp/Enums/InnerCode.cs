namespace inapp.Enums
{
    public enum InnerCode
    {
        None = 0,
        // 1xxx - User / Account
        UserNotFound = 1000,
        UserAlreadyExists = 1001,
        InvalidEmailFormat = 1002,
        WeakPassword = 1003,
        InvalidCredentials = 1004,
        AccountLocked = 1005,
        EmailNotConfirmed = 1006,
        BadCredentials = 1007,


        // 2xxx - Validation / Bad Request
        BadRequest = 2000,
        MissingRequiredField = 2001,
        InvalidFieldValue = 2002,
        TooManyRequests = 2003,

        // 3xxx - Auth / Security / Token
        InvalidToken = 3000,
        TokenExpired = 3001,
        Unauthorized = 3002,
        Forbidden = 3003,

        // 4xxx - Server Errors
        InternalServerError = 4000,
        ServiceUnavailable = 4001
    }
}
