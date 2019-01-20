using System;
using MongoDB.Bson;

namespace MoveToCore.Loggers
{
    public class LogManager
    {
        private readonly ILogger _logger;

        public static LogManager Instance;

        static LogManager()
        {
            Instance = new LogManager();
        }

        private LogManager()
        {
            var isDirectLogstashLogging = Environment.GetEnvironmentVariable("IS_DIRECT_LOGSTASH_LOGGING");

            _logger = string.IsNullOrEmpty(isDirectLogstashLogging) || !bool.Parse(isDirectLogstashLogging) ?
                   (ILogger)new KafkaLogger() : new LogstashLogger();
        }

        public void WriteException(Exception ex)
        {
            var log = new
            {
                Date = DateTime.Now.ToLongDateString(),
                ExceptionType = ex.GetType().Name,
                ex.Message,
                ex.StackTrace,
                InnerException = ex.InnerException != null ? SerializeInnerException(ex.InnerException) : null
            }.ToJson();

            _logger.Write(log);
        }

        private object SerializeInnerException(Exception ex)
        {
            return new
            {
                Type = ex.GetType().Name,
                ExceptionType = ex.GetType().Name,
                ex.Message,
                ex.StackTrace,
                InnerException = ex.InnerException != null ? SerializeInnerException(ex.InnerException) : null
            };
        }
    }
}