using TriDataHub.Services.Contracts;
using TriDataHub.Classes.Joke;
using Newtonsoft.Json;

namespace TriDataHub.Services
{
    public class JokeService : IJokeService
    {
        private string _dadJokeUrl = "https://icanhazdadjoke.com/";

        private readonly ILoggerService _logger;

        public JokeService(ILoggerService logger)
        {
            _logger = logger;
        }

        public async Task<DadJoke> GetRandomJoke() => await GetDadJoke();

        public async Task<DadJoke> GetJokeById(string jokeId) => await GetDadJoke(jokeId);

        public async Task<DadJoke[]> GetNumberOfRandomJokes(int quantity)
        {
            var jokeArray = new DadJoke[quantity];

            for (int i = 0; i < quantity; i++)
            {
                jokeArray[i] = await GetRandomJoke();
            }

            return jokeArray;
        }

        private async Task<DadJoke> GetDadJoke(string id = "")
        {
            using var client = new HttpClient();
            client.DefaultRequestHeaders.Add("Accept", "application/json");

            try
            {
                var result = await client.GetAsync(string.IsNullOrEmpty(id)
                    ? _dadJokeUrl
                    : $"{_dadJokeUrl}j/{id}");

                result.EnsureSuccessStatusCode();

                return JsonConvert.DeserializeObject<DadJoke>(await result.Content.ReadAsStringAsync());
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
