using System.Web.Http.ExceptionHandling;
using NLog;

namespace Infrastructure.Common
{
    public class GlobalExceptionLogger : ExceptionLogger
    {
        /// <summary>
        ///     todo: exception, cant use dependency injection here
        /// </summary>
        private readonly ILogger _logger = LogManager.GetCurrentClassLogger();

        #region Overrides of ExceptionLogger

        public override void Log(ExceptionLoggerContext context)
        {
            this._logger.Error(context.Exception);
        }

        #endregion
    }
}