using inapp.Enums;

namespace inapp.Models
{
    public class CommonResult
    {
        private readonly string _errorMessage;
        private readonly InnerCode? _code;
        private readonly Exception _exception;
        private readonly bool? _default;

        public bool IsFaulted => _code.HasValue;
        public bool IsSuccess => _code.HasValue == false;
        public InnerCode InnerCode => _code ?? InnerCode.None;
        public string ErrorMessage => IsFaulted ? _errorMessage : null;
        public Exception Exception => IsFaulted ? _exception : null;
        public bool Default => _default ?? true;

        private CommonResult(InnerCode? code, string errorMessage, Exception? exception)
        {
            _errorMessage = errorMessage;
            _code = code;
            _exception = exception;
            _default = false;
        }
        public static CommonResult Success()
        {
            return new CommonResult(default, default, default);
        }

        public static CommonResult Failure(InnerCode innerCode, string errorMessage)
        {
            return new CommonResult(innerCode, errorMessage, default);
        }

        public TResult Match<TResult>(Func<TResult> successPath, Func<CommonResult, TResult> failPath) => IsFaulted ? failPath(this) : successPath();
    }
}
