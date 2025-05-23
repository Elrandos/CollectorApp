using inapp.Enums;

namespace inapp.Models
{
    public class CommonResult<T>
    {
        private readonly CommonResult _result;
        public readonly T Data;
        private readonly bool? _default;

        public bool IsFaulted => _result.IsFaulted;
        public bool IsSuccess => _result.IsSuccess;
        public CommonResult Bottom => _result;
        public InnerCode InnerCode => _result.InnerCode;
        public Exception Exception => _result.Exception;
        public string ErrorMessage => _result.ErrorMessage;
        public bool Default => _default ?? true;

        private CommonResult(CommonResult result, T data)
        {
            _result = result;
            Data = data;
            _default = false;
        }

        public static CommonResult<T> Success(T data = default)
        {
            return new CommonResult<T>(CommonResult.Success(), data);
        }

        public static CommonResult<T> Failure(InnerCode innerCode, string errorMessage, T data = default)
        {
            return new CommonResult<T>(CommonResult.Failure(innerCode, errorMessage), data);
        }
        public TResult Match<TResult>(Func<T, TResult> successPath, Func<CommonResult, T, TResult> failPath) => IsFaulted ? failPath(Bottom, Data) : successPath(Data);
    }
}
