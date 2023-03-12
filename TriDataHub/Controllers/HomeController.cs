using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using TriDataHub.Models;
using TriDataHub.Services.Contracts;

namespace TriDataHub.Controllers
{
    public class HomeController : Controller
    {
        [ApiExplorerSettings(IgnoreApi = true)]
        public class PaskaitosController : Controller
        {
            private readonly IJokeService _jokeService;
            private readonly IBookService _bookService;
            private readonly IProcessorService _processorService;

            public PaskaitosController(IJokeService jokeService, IBookService bookService, IProcessorService processorService)
            {
                _jokeService = jokeService;
                _bookService = bookService;
                _processorService = processorService;
            }

            public IActionResult Index()
            {
                return View();
            }

            [Route("/Paskaitos/Jokes/Id/{id}")]
            public async Task<IActionResult> Jokes(string id)
            {
                var model = new JokeModel(await _jokeService.GetJokeById(id));
                model.Description = $"This is where we will show jokes by id of {id} from API";

                return View(model);
            }


            [Route("/Paskaitos/Jokes/Quantity/{quantity}")]
            public async Task<IActionResult> Jokes(int quantity)
            {
                var model = new JokeModel(await _jokeService.GetNumberOfRandomJokes(quantity));
                model.Quantity = quantity;
                model.Description = $"This is where we will show {quantity} random jokes from API";

                return View(model);
            }

            public async Task<IActionResult> Jokes()
            {
                return View(new JokeModel(await _jokeService.GetRandomJoke()));
            }

            public async Task<IActionResult> Processors()
            {
                var processors = await _processorService.ScrapeTopoProcesors();
                return View(new ProcessorModel(processors));
            }

            public async Task<IActionResult> Books()

            {
                return View(new BookModel(await _bookService.GetRandomJoke()));
            }

            [Route("/Paskaitos/Books/Author/{author}")]
            public async Task<IActionResult> BooksByAuthor(string author)
            {
                var model = new BookModel(await _bookService.GetBooksByAuthor(author));
                model.Description = $"This is where we will show {author}'s books from API";

                return View(model);
            }

            [Route("/Paskaitos/Books/Isbn/{isbn}")]
            public async Task<IActionResult> BooksByIsbn(string isbn)
            {
                var model = new BookModel(await _bookService.GetBooksByIsbn(isbn));
                model.Description = $"This is where we will show book of {isbn} isbn from API";

                return View(model);
            }
        }
    }
}