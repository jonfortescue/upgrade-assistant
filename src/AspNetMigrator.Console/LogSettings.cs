﻿using System;
using Microsoft.Extensions.Logging;
using Serilog.Core;
using Serilog.Events;

namespace AspNetMigrator
{
    public class LogSettings
    {
        public LogSettings(bool verbose)
        {
            SelectedTarget = LogTarget.Both;
            LoggingLevelSwitch = new LoggingLevelSwitch();
            SetLogLevel(verbose ? LogLevel.Trace : LogLevel.Information);
        }

        public LogTarget SelectedTarget { get; set; }

        public LoggingLevelSwitch LoggingLevelSwitch { get; private set; }

        public bool IsFileEnabled
        {
            get
            {
                return SelectedTarget == LogTarget.Both || SelectedTarget == LogTarget.File;
            }
        }

        public bool IsConsoleEnabled
        {
            get
            {
                return SelectedTarget == LogTarget.Both || SelectedTarget == LogTarget.Console;
            }
        }

        public void SetLogLevel(LogLevel newLogLevel)
        {
            LoggingLevelSwitch.MinimumLevel = newLogLevel switch
            {
                LogLevel.Trace => LogEventLevel.Verbose,
                LogLevel.Debug => LogEventLevel.Debug,
                LogLevel.Information => LogEventLevel.Information,
                LogLevel.Warning => LogEventLevel.Warning,
                LogLevel.Error => LogEventLevel.Error,
                LogLevel.Critical => LogEventLevel.Fatal,
                LogLevel.None => LogEventLevel.Fatal,
                _ => throw new NotImplementedException()
            };

            if (newLogLevel == LogLevel.None)
            {
                SelectedTarget = LogTarget.None;
            }
        }
    }

    public enum LogTarget
    {
        Console = 0,
        File = 1,
        Both = 2,
        None = 3
    }
}