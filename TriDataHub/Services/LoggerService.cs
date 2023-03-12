using TriDataHub.Services.Contracts;

namespace TriDataHub.Services
{
    public class LoggerService : ILoggerService
    {
        private readonly string _logsDirectory = @"./Logs";

        public async Task LogWarning(string message)
        {
            await WriteLogToFile("#eb9021", message);
        }

        public async Task LogError(string message)
        {
            await WriteLogToFile("#db1818", message);
        }

        public async Task LogInfo(string message)
        {
            await WriteLogToFile("#1846db", message);
        }

        public async Task LogDebug(string message)
        {
            await WriteLogToFile("#000000", message);
        }

        private async Task WriteLogToFile(string htmlColor, string message)
        {
            CreateFolderOrDefault();

            var logStartTag = $"<p style=\"color: {htmlColor};" +
                $" margin: 1px;" +
                $" padding: 0px;" +
                $" font-family: 'Courier New', monospace;" +
                $" font-size: 15;" +
                $" font-weight: bold;\">";

            await File.AppendAllTextAsync(GetLogsPath(),
                $"{logStartTag}<span style=\" " +
                $"background-color: #cce0e0; " +
                $"border-radius: 5px; \">" +
                $"{DateTime.Now:yyyy-MM-dd HH:mm:ss}: " +
                $"</span>{message}</p>");
        }

        private string GetLogsPath() =>
            $@"{_logsDirectory}/{DateTime.Now:yyyy-MM-dd}.html";

        private void CreateFolderOrDefault() =>
                Directory.CreateDirectory(_logsDirectory);
    }
}
