using Microsoft.AspNetCore.Mvc.Filters;

namespace WebAPI_MAM.Filters
{
    public class FilterAction : IActionFilter
    {
        private readonly ILogger<FilterAction> log;

        public FilterAction(ILogger<FilterAction> log)
        {
            this.log = log;
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            log.LogInformation("MODIFICAT");
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            log.LogInformation("MODIFICAR");
        }


    }
}
