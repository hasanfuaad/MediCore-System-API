
namespace Application.Common
{
    public class SuccessResult:Results,ISuccessResult
    {
        public SuccessResult() { base.Success = true; }
        public SuccessResult(ServiceSuccess serviceSuccess)
        {
            Message = serviceSuccess.Message;
            Code = serviceSuccess.Code;
            base.Success = true;
        }
        public static new SuccessResult<T> Success<T>(ServiceSuccess success) => new(success);
        public static new SuccessResult Success(ServiceSuccess success) => new(success);
        public static new SuccessResult<T> Success<T>(T Data) => new(Data);

        public static new SuccessResult<T> Success<T>(T Data,ServiceSuccess serviceSuccess) => new(Data,serviceSuccess);
       
    }
    public class SuccessResult<T>:Results<T>,ISuccessResult
    {
        public SuccessResult():base(default) { Success = true;}
        public SuccessResult(T Data):base(Data) { Success = true;Code = 200; }
        public SuccessResult(T Data, ServiceSuccess serviceSuccess) : base(default)
        {
            Success=true;
            Code = serviceSuccess.Code;
            Message = serviceSuccess.Message;
        }
        public SuccessResult(ServiceSuccess serviceSuccess) : base(default)
        {
            Success=true;
            Code = serviceSuccess.Code;
            Message = serviceSuccess.Message;
        }
    }
}
