using System.Web.Http.Filters;
using Newtonsoft.Json;
using NLog;

namespace BalticAmadeus.BuildServer.Infrastructure
{
	public class BuildWatchExceptionFilterAttribute : ExceptionFilterAttribute
	{
		private static readonly ILogger Logger = LogManager.GetLogger("fileLogger");

		public override void OnException(HttpActionExecutedContext context)
		{
			var serializer = new JsonSerializer();

			Logger.Error(context.Exception, "An exception occurred during {0} execution. Request is {1}", context.ActionContext.ActionDescriptor.ActionName, serializer.Serialize(context.ActionContext.Request.Content));
		}
	}
}