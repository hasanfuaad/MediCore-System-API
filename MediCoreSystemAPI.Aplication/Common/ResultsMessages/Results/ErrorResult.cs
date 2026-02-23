

using Application.Common;

namespace Application.Common
{
    public class ErrorResult : Results, IErrorResult
    {
        public IReadOnlyCollection<ServiceError> Errors {get; set;}
        public ErrorResult(string error):this(error, Array.Empty<ServiceError>()) { }
        public ErrorResult(ServiceError error)
        {
            Message = error.Message;
            Code = error.Code;
        }
        public ErrorResult(ErrorResult errorResult) 
        {
            Message=errorResult.Message;
            Code=errorResult.Code;
            base.Success = false;
        }
        public ErrorResult(IReadOnlyCollection<ServiceError> errors) { Errors = errors; }
        public ErrorResult(string message, IReadOnlyCollection<ServiceError> errors)
        {
            Message = message;
            Success=false; 
            Errors = errors??Array.Empty<ServiceError>();
        }
        public static ErrorResult<T> Failed<T>(T Data ,List<ServiceError> Errors)=>new(Data, Errors);
        public static ErrorResult<T> Failed<T>(T Data ,ServiceError Errors)=>new(Data, Errors);
        public static ErrorResult<T> Failed<T>(T Data )=>new(Data);
        public static ErrorResult<T> Failed<T>(List<ServiceError> Errors) => new(Errors);
        public static ErrorResult<T> Failed<T>(ServiceError Errors) => new(Errors);
        public static ErrorResult Failed(ServiceError Errors) => new(Errors);
        public static ErrorResult<T> Failed<T>(string Errors) => new(Errors);
        public static ErrorResult Failed<T>(IReadOnlyCollection<ServiceError> Errors) => new(Errors);

    }
    public class ErrorResult<T>:Results<T>,IErrorResult
    {
        public IReadOnlyCollection<ServiceError> Errors { get;}
        public ErrorResult(string message) : this(message, Array.Empty<ServiceError>()) { }
        public ErrorResult(IReadOnlyCollection<ServiceError> errors):this(null,errors) { }
        public ErrorResult(ServiceError errors) : this(errors.Message,errors) { }
        public ErrorResult(string message, IReadOnlyCollection<ServiceError> errors):base(default)
        {
            Message = message;
            Success = false;
        }
        public ErrorResult(T Data, IReadOnlyCollection<ServiceError> errors):base(Data)
        {
            Success = false;
            Errors = errors ?? Array.Empty<ServiceError>();
        }
        public ErrorResult(T Data):base(Data)
        {
            Success = false;
        }
        public ErrorResult(T Data, ServiceError errors) : base(Data)
        {
            Message=errors.Message;
            Code = errors.Code;
        }
        public ErrorResult(string message, ServiceError errors) : this(message)
        {
            Message=errors.Message;
            Code = errors.Code;
        }
        public static ErrorResult<T> Failed(IReadOnlyCollection<ServiceError> error)
        {
            return new ErrorResult<T>(error);
        }
        public static ErrorResult<T> Failed(ServiceError error)
        {
            return new ErrorResult<T>(error);
        }
    }
}
