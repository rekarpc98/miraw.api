// ---------------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE AS LONG AS SOFTWARE FUNDS ARE DONATED TO THE POOR
// ---------------------------------------------------------------

using Microsoft.Extensions.Logging;

namespace Miraw.Api.Core.Brokers.Loggings
{
    public class LoggingBroker : ILoggingBroker
    {
        readonly ILogger<LoggingBroker> _logger;

        public LoggingBroker(ILogger<LoggingBroker> logger) =>
            _logger = logger;

        public void LogInformation(string message) =>
            _logger.LogInformation(message);

        public void LogTrace(string message) =>
            _logger.LogTrace(message);

        public void LogDebug(string message) =>
            _logger.LogDebug(message);

        public void LogWarning(string message) =>
            _logger.LogWarning(message);

        public void LogError(Exception exception) =>
            _logger.LogError(exception, exception.Message);

        public void LogCritical(Exception exception) =>
            _logger.LogCritical(exception, exception.Message);
    }
}
