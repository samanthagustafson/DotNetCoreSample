using System;
using System.Web;
using Newtonsoft.Json;
using Serilog;
using Serilog.Formatting.Json;
using Serilog.Sinks.Http.Private.Network;
using SerilogLogger = Serilog.Core.Logger;

namespace MoveToCore.Logger
{
    public class LogManager
    {
        private readonly SerilogLogger _logger;

        public static LogManager Instance;

        static LogManager()
        {
            Instance = new LogManager();
        }

        private LogManager()
        {
            _logger = new LoggerConfiguration()
                .WriteTo.Http("http://10.0.75.1:5000", httpClient: new LoggerHttpClient())
                .CreateLogger();
        }

        public void WriteException(Exception ex)
        {
            _logger.Error(HttpUtility.UrlEncode(JsonConvert.SerializeObject(new
            {
                Date = DateTime.Now,
                Type = ex.GetType().Name,
                ex.Message,
                ex.StackTrace,
                InnerException = ex.InnerException != null ? SerializeInnerException(ex.InnerException) : null
            })));
        }

        private object SerializeInnerException(Exception ex)
        {
            return new
            {
                Type = ex.GetType().Name,
                ex.Message,
                ex.StackTrace,
                InnerException = ex.InnerException != null ? SerializeInnerException(ex.InnerException) : null
            };
        }
    }
}