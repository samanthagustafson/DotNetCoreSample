using System;
using System.Net;
using MongoDB.Bson;
using Serilog;
using Serilog.Sinks.Network;
using SerilogLogger = Serilog.Core.Logger;

namespace MoveToCore.Loggers
{
    public class LogstashLogger : ILogger
    {
        private readonly SerilogLogger _logger;

        public LogstashLogger()
        {
            _logger = new LoggerConfiguration()
               .WriteTo.TCPSink(IPAddress.Parse(Environment.GetEnvironmentVariable("LOGSTASH_HOST")),
                                int.Parse(Environment.GetEnvironmentVariable("LOGSTASH_PORT")))
               .CreateLogger();
        }

        public void Write(string log)
        {
            _logger.Error(log);
        }
    }
}
