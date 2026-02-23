namespace Application.Common;
[Serializable]
public class ServiceError
{
    public int Code { get; }
    public string Message { get; }
    public object Errors { get; set; }

    public static ServiceError DefaultError => new("An exception occured.", 500);
    public static ServiceError ExceptionMessages => new("فشلت العملية يرجى المحاولة لاحقا ", 500);

    public ServiceError(string message, int code)
    {
        Message = message;
        Code = code;
    }
    public ServiceError(string message, object errors, int code)
    {
        Message = message;
        Code = code;
        Errors = errors;
    }

    public static ServiceError CustomMessage(string errorMessage)
    {
        return new ServiceError(errorMessage, 500);
    }

    public static ServiceError CustomMessage(string errorMessage, int code)
    {
        return new ServiceError(errorMessage, code);
    }
    public static ServiceError CustomMessage(string errorMessage, object errors)
    {
        return new ServiceError(errorMessage, errors, 500);
    }
    public static ServiceError CustomMessage(string errorMessage, object errors, int code)
    {
        return new ServiceError(errorMessage, errors, code);
    }

    #region Override Equals Operator

    public override bool Equals(object? obj)
    {
        // If parameter cannot be cast to ServiceError or is null return false.
        var error = obj as ServiceError;

        // Return true if the error codes match. False if the object we're comparing to is nul
        // or if it has a different code.
        return Code == error?.Code;
    }

    public bool Equals(ServiceError error)
    {
        // Return true if the error codes match. False if the object we're comparing to is nul
        // or if it has a different code.
        return Code == error?.Code;
    }

    public override int GetHashCode()
    {
        return Code;
    }

    public static bool operator ==(ServiceError a, ServiceError b)
    {
        // If both are null, or both are same instance, return true.
        if (ReferenceEquals(a, b)) return true;

        // If one is null, but not both, return false.
        if ((object?)a == null || (object?)b == null) return false;

        // Return true if the fields match:
        return a.Equals(b);
    }

    public static bool operator !=(ServiceError a, ServiceError b)
    {
        return !(a == b);
    }

    #endregion
}