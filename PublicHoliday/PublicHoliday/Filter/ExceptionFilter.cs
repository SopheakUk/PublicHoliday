using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace PublicHoliday.Filter
{
    public class ExceptionFilter : ActionFilterAttribute
    {
        public override void OnActionExecuted(ActionExecutedContext context)
        {
            if (context.Exception != null)
            {
                context.Result = new ObjectResult(new
                {
                    Error = context.Exception.Message
                });
                context.ExceptionHandled = true;
            }
        }
    }
}