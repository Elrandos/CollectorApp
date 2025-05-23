//using inapp.Enums;
//using inapp.Models;
//using Microsoft.AspNetCore.Mvc;

//namespace inapp.Controllers
//{
//    public class SimpleApiController : ControllerBase
//    {
//        protected IActionResult AccessDenied()
//        {
//            return _responseFactory.AccessDenied(innerCode: InnerCode.NoPermissions.ToInt());
//        }
//        [Obsolete("Use HandleActionResult")]
//        protected IActionResult HandleResponse(CommonResult result, object data = null)
//        {
//            if (result.IsSuccess)
//            {
//                return Success(data);
//            }

//            return result.InnerCode switch
//            {
//                InnerCode.NoPermissions => AccessDenied(),
//                InnerCode.DateRangeAccessDenied => DateRangeAccessDenied(),
//                _ => Error(result.InnerCode, data)
//            };
//        }

//        [Obsolete("Use HandleActionResult")]
//        protected IActionResult HandleResponse<T>(CommonResult<T> result)
//        {
//            return HandleResponse(result.Bottom, result.Data);
//        }
//    }
//}
