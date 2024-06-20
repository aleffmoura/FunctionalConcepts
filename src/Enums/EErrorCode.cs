namespace FunctionalConcepts.Enums;

public enum EErrorCode
{
    Unauthorized = 401,
    Forbidden = 0403,
    NotFound = 0404,
    Conflict = 0409,
    NotAllowed = 0405,
    InvalidObject = 0422,
    Unhandled = 0500,
    ServiceUnavailable = 0503,
}
