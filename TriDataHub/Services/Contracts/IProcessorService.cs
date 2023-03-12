using TriDataHub.Classes.Processor;

namespace TriDataHub.Services.Contracts
{
    public interface IProcessorService
    {
        public Task<List<Processor>> ScrapeTopoProcesors();

        public IEnumerable<Processor> GetProcessorsFromPage(string page);

        public Task<string> ScrapeJavaScriptPage(string url);
    }
}
