using TriDataHub.Classes.Book;

namespace TriDataHub.Services.Contracts
{
    public interface IBookService
    {
        public Task<Book> GetRandomJoke();

        public Task<Book> GetJokeById(string jokeId = "");
    }
}
