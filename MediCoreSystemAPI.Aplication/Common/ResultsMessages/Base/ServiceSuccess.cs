using System;

namespace Application.Common
{
    [Serializable]
    public class ServiceSuccess : IEquatable<ServiceSuccess>
    {
        public int Code { get; }
        public string Message { get; }

        public static ServiceSuccess Default => new("تمت العملية بنجاح", 200);

        public ServiceSuccess(string message, int code)
        {
            Message = message ?? throw new ArgumentNullException(nameof(message));
            Code = code;
        }

        public static ServiceSuccess WithMessage(string message)
        {
            return new ServiceSuccess(message, 200);
        }
        public static ServiceSuccess WithMessageAndCode(string message, int code)
        {
            return new ServiceSuccess(message, code);
        }

        #region Equality Members

        public override bool Equals(object? obj)
        {
            return Equals(obj as ServiceSuccess);
        }

        public bool Equals(ServiceSuccess? other)
        {
            if (other is null) return false;
            return Code == other.Code && Message == other.Message;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Code, Message);
        }

        public static bool operator ==(ServiceSuccess? left, ServiceSuccess? right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(ServiceSuccess? left, ServiceSuccess? right)
        {
            return !Equals(left, right);
        }

        #endregion

        #region Overrides for Debugging and Logging

        public override string ToString()
        {
            return $"Code: {Code}, Message: {Message}";
        }

        #endregion
    }
}
