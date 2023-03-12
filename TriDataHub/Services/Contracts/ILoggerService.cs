namespace TriDataHub.Services.Contracts
{
    public interface ILoggerService
    {
        public Task LogWarning(string message);

        public Task LogError(string message);

        public Task LogInfo(string message);

        public Task LogDebug(string message);
    }
}
