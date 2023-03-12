using TriDataHub.Classes.Book;

namespace TriDataHub.Models
{
    public class BookModel
    {
        public int Quantity { get; set; } = 1;
        public string Title { get; set; } = "Books";

        public string Description { get; set; } = "This is where we will show Books from API";
        public Book[] Books { get; set; }

        public BookModel(Book book)
        {
            Books = new Book[] { book };
        }

        public BookModel(Book[] books)
        {
            Books = books;
        }
    }
}
