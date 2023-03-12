using TriDataHub.Classes.Joke;

namespace TriDataHub.Services.Contracts
{
    public interface IJokeService
    {
        public Task<DadJoke> GetRandomJoke();

        public Task<DadJoke> GetJokeById(string jokeId = "");

        public Task<DadJoke[]> GetNumberOfRandomJokes(int quantity);
    }
}
