using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace MoveToCore.Loggers
{
    public class LoggerExceptionFilter : ExceptionFilterAttribute
    {
        public override void OnException(ExceptionContext context)
        {
            LogManager.Instance.WriteException(context.Exception);

            context.Result = new StatusCodeResult(500);

            base.OnException(context);
        }
    }
}
