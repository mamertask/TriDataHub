using TriDataHub.Classes.Joke;

namespace TriDataHub.Models
{
    public class JokeModel
    {
        public int Quantity { get; set; } = 1;
        public string Title { get; set; } = "Jokes";
        public string Description { get; set; } = "This is where we will show a random joke from API";
        public DadJoke[] Jokes { get; set; }

        public JokeModel(DadJoke joke)
        {
            Jokes = new DadJoke[] { joke };
        }

        public JokeModel(DadJoke[] jokes)
        {
            Jokes = jokes;
        }
    }
}
