using inapp.Enums;
using inapp.Extensions;
using NSwag.Annotations;

namespace inapp.Attribiutes
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true)]
    public class InnerCodeSwaggerResponseAttribute : SwaggerResponseAttribute
    {
        public InnerCodeSwaggerResponseAttribute(int httpStatusCode, InnerCode innerCode) : base(httpStatusCode, typeof(InnerCode))
        {
            Description = GenerateDescription(innerCode);
        }

        public InnerCodeSwaggerResponseAttribute(int httpStatusCode, InnerCode innerCode, string details) : base(httpStatusCode, typeof(InnerCode))
        {
            Description = GenerateDescription(innerCode, details);
        }

        private string GenerateDescription(InnerCode innerCode, string details = null)
        {
            var result = $"{innerCode.ToIntString()} = {innerCode}";
            return details != null ? $" {result}. Details: {details}" : result;
        }
    }
}
