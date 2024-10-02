using Library.Contracts;
using Library.Data.Entities;
using Library.Dto;
using Library.Dto.Views;
using Microsoft.AspNetCore.Mvc;

namespace Library.WebApi.Controllers
{
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly IBookService _bookService;

        public BooksController(IBookService bookService)
        {
            _bookService = bookService;
        }

        /// <summary>
        /// Get All books
        /// </summary>
        /// <remarks>Retrieve a list of all books in the library.</remarks>
        [HttpGet]
        [Route("books")]
        public IActionResult Get()
        {
            var books = _bookService.GetBooks();
            return books.Count() > 0 ? Ok(books) : BadRequest("List of books not found");
        }

        /// <summary>
        /// Get a specific book
        /// </summary>
        /// <remarks>Retrieve a specific book by its ID.</remarks>
        [HttpGet()]
        [Route("book/{id}")]
        public IActionResult Get(int id)
        {
            var book = _bookService.GetBookById(id);
            return book != null ? Ok(book) : BadRequest("No book was found");
        }

        /// <summary>
        /// Add a new book
        /// </summary>
        /// <remarks>Add a new book to the library.</remarks>
        [HttpPost]
        [Route("book")]
        public IActionResult Post([FromBody] Book book)
        {
            var bookDto = new BookDto()
            { 
                Author = book.Author,
                Title = book.Title,
                Isbn = book.Isbn,
                PublishedDate = book.PublishedDate
            };

            var savedBook = _bookService.AddBook(bookDto);

            return savedBook != null ? Ok(savedBook) : BadRequest("Book was not saved");
        }

        /// <summary>
        /// Update a book
        /// </summary>
        /// <remarks>Update an existing book by its ID.</remarks>
        [HttpPut()]
        [Route("book/{id}")]
        public IActionResult Put(int id, [FromBody] Book book)
        {
            var savedBook = _bookService.GetBookById(id);

            if (savedBook != null)
            {
                savedBook.Author = book.Author;
                savedBook.Title = book.Title;
                savedBook.PublishedDate = book.PublishedDate;
                savedBook.Isbn = book.Isbn;

                var updatedBook = _bookService.UpdateBook(savedBook);

                return Ok(updatedBook);
            }

            return BadRequest("Book was not updated");
           
        }
    }
}
