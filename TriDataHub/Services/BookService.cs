using TriDataHub.Classes.Book;
using TriDataHub.Services.Contracts;
using Newtonsoft.Json;

namespace TriDataHub.Services
{
    public class BookService : IBookService
    {
        private string _dadJokeUrl = "https://icanhazdadjoke.com/";

        private readonly ILoggerService _logger;

        public BookService(ILoggerService logger)
        {
            _logger = logger;
        }

        public async Task<Book> GetRandomJoke() => await GetDadJoke();

        public async Task<Book> GetJokeById(string jokeId) => await GetDadJoke(jokeId);

        public async Task<Book[]> GetNumberOfRandomJokes(int quantity)
        {
            var jokeArray = new Book[quantity];

            for (int i = 0; i < quantity; i++)
            {
                jokeArray[i] = await GetRandomJoke();
            }

            return jokeArray;
        }

        private async Task<Book> GetDadJoke(string id = "")
        {
            using var client = new HttpClient();
            client.DefaultRequestHeaders.Add("Accept", "application/json");

            try
            {
                var result = await client.GetAsync(string.IsNullOrEmpty(id)
                    ? _dadJokeUrl
                    : $"{_dadJokeUrl}j/{id}");

                result.EnsureSuccessStatusCode();

                return JsonConvert.DeserializeObject<Book>(await result.Content.ReadAsStringAsync());
            }
            catch (Exception ex)
            {
                await _logger.LogWarning($"Cannot reach {_dadJokeUrl}");
                await _logger.LogWarning($"error: {ex.Message} \nTrace: {ex.StackTrace}");

                return null;
            }
        }
    }
}
