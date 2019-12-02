using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

using Microsoft.Extensions.Logging;
using Microsoft.ApplicationInsights;
using Microsoft.ApplicationInsights.DataContracts;
using Starter.Framework.Entities;

namespace Starter.Logger.ApplicationInsights
{
    public class ApplicationInsightsLogger : ILogger, IDisposable
    {
        private readonly TelemetryClient _telemetryClient;

        public ApplicationInsightsLogger(ISettings settings)
        {
            if (settings.A == null)
            {
                return;
            }

            //var configuration = TelemetryConfiguration.CreateDefault();
            //configuration.InstrumentationKey = key;

            _telemetryClient = new TelemetryClient() { InstrumentationKey = key };
        }

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
            Log(logLevel, state.ToString(), exception);
        }

        public void Log(LogLevel logLevel = LogLevel.Trace,
                string message = "",
                Exception exception = null,
                [CallerMemberName] string memberName = "",
                [CallerFilePath] string sourceFilePath = "",
                [CallerLineNumber] int sourceLineNumber = 0)
        {
            var fullMessage = $"{message}";

            switch (logLevel)
            {
                case LogLevel.Debug:
                    Debug(fullMessage, exception);

                    break;
                case LogLevel.Information:
                    Info(fullMessage, exception);

                    break;
                case LogLevel.Warning:
                    Warning(fullMessage, exception);

                    break;
                case LogLevel.Error:
                    Error(fullMessage, exception);

                    break;
                case LogLevel.Critical:
                    Critical(fullMessage, exception);

                    break;
                case LogLevel.None:
                    break;
            }
        }

        private void Debug(string message = "", Exception exception = null)
        {
            _telemetryClient.TrackTrace($"{message} {Environment.NewLine}{exception?.ToString()}",
                SeverityLevel.Verbose);
        }

        private void Error(string message = "", Exception exception = null)
        {
            _telemetryClient.TrackException(exception,
                new Dictionary<string, string>()
                    {{"message", message}, {"SeverityLevel", SeverityLevel.Error.ToString()}});
        }

        private void Critical(string message = "", Exception exception = null)
        {
            _telemetryClient.TrackException(exception,
                new Dictionary<string, string>()
                    {{"message", message}, {"SeverityLevel", SeverityLevel.Error.ToString()}});
        }

        private void Info(string message = "", Exception exception = null)
        {
            _telemetryClient.TrackTrace($"{message} {Environment.NewLine}{exception}", SeverityLevel.Information);
        }

        private void Warning(string message = "", Exception exception = null)
        {
            _telemetryClient.TrackTrace($"{message} {Environment.NewLine}{exception}", SeverityLevel.Warning);
        }

        public void Dispose()
        {
            if (_telemetryClient == null)
            {
                return;
            }

            _telemetryClient.Flush();

            System.Threading.Tasks.Task.Delay(5000).Wait();
        }

        public bool IsEnabled(LogLevel logLevel)
        {
            throw new NotImplementedException();
        }

        public IDisposable BeginScope<TState>(TState state)
        {
            throw new NotImplementedException();
        }
    }
}
