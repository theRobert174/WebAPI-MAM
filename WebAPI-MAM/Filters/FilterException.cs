using Microsoft.AspNetCore.Mvc.Filters;

namespace WebAPI_MAM.Filters
{
    public class FilterException : ExceptionFilterAttribute
    {
        private readonly ILogger<FilterException> log;

        public FilterException(ILogger<FilterException> log)
        {
            this.log = log;
        }

        public override void OnException(ExceptionContext context)
        {
            log.LogError(context.Exception, context.Exception.Message);

            base.OnException(context);
        }
    }
}
