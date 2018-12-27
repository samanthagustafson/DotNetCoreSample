/*using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using Microsoft.Extensions.Logging;

namespace move_to_core.Logger
{
    public class LogManager
    {
        private const string EVENT_SOURCE = "dchecks";
        private const string EVENT_LOG = "DCheks";

        public static void WriteException(Exception ex)
        {
            EnsureSource();

            EventLog.WriteEntry(EVENT_SOURCE, FormatException(ex), EventLogEntryType.Error);
        }

        private static void EnsureSource()
        {
            if (!EventLog.SourceExists(EVENT_SOURCE))
            {
                EventLog.CreateEventSource(EVENT_SOURCE, EVENT_LOG);
            }
        }

        private static string FormatException(Exception ex)
        {
            var baseExceptionFormating =
                $"Exception Type: {ex.GetType()}\r\nMessage: {ex.Message}\r\nThrown at: {DateTime.Now}\r\nAdditonal Data: {ex.Data}\r\nStack Trace: {ex.StackTrace}";

            if (ex.InnerException == null)
            {
                return baseExceptionFormating;
            }

            return
                $"{baseExceptionFormating}\r\n----------------------------------------------------------------\r\nInner Exception: {FormatException(ex.InnerException)}";
        }

    }
}*/