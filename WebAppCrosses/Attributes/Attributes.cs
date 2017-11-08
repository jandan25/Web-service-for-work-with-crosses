using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using System.Web.Http.Tracing;
using WebAppCrosses.Helpers;
using System.Net;
using System.Net.Http;
using System.Threading;

namespace WebAppCrosses.Attributes
{
    public class MyBoolAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            if (value == null) return false;
            if (value.GetType() != typeof(bool)) throw new InvalidOperationException("an only be used on boolean properties.");

            return (bool)value;
        }
    }

    public class MyDateAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            // 01.10.2001 0:00:00
            DateTime dt = DateTime.ParseExact(value.ToString(), "dd.MM.yyyy H:mm:ss", null);

            if (!dt.ToString().Equals(value.ToString()))
                return false;
            return true;
        }
    }

    public class LoggingFilterAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(HttpActionContext filterContext)
        {
            GlobalConfiguration.Configuration.Services.Replace(typeof(ITraceWriter), new NLogger());
            var trace = GlobalConfiguration.Configuration.Services.GetTraceWriter();
            trace.Info(filterContext.Request, "Controller : " + filterContext.ControllerContext.ControllerDescriptor.ControllerType.FullName + Environment.NewLine + "Action : " + filterContext.ActionDescriptor.ActionName, "JSON", filterContext.ActionArguments);
        }
    }

    public class NotSupportedExceptionFilterAttribute : ExceptionFilterAttribute
    {
        public override void OnException(HttpActionExecutedContext context)
        {
            context.Response = new HttpResponseMessage(HttpStatusCode.BadRequest)
            {
                Content = new StringContent("Data you provided is not supported.")
            };
            var trace = GlobalConfiguration.Configuration.Services.GetTraceWriter();
            trace.Info(context.Request, "Exception Error", context.Exception, "Error: ");
        }
    }
}