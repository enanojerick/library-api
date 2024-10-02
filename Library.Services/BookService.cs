using Library.Contracts;
using Library.Data.Entities;
using Library.Data.Repository.Interfaces;
using Library.Dto;

namespace Library.Services
{
    public class BookService : IBookService
    {
        private readonly IRepository<Books> _books;

        public BookService(IRepository<Books> books)
        {
            _books = books;
        }

        #region DTO Setters
        private BookDto? SetBookDto(Books? books)
        {
            return books != null ? new BookDto()
            {
                Id = books.Id,
                Author = books.Author,
                Isbn = books.Isbn,
                PublishedDate = books.PublishedDate,
                Title = books.Title
            } : null;
        }

        private List<BookDto> SetBookDtoList(List<Books> books)
        {
            List<BookDto> bookList = new List<BookDto>();

            foreach (var item in books)
            {
                var book = SetBookDto(item);
                if (book != null)
                {
                    bookList.Add(book);
                }
            }

            return bookList;
        }
        #endregion

        #region CRUD

        public IList<BookDto> GetBooks()
        {
            return SetBookDtoList(_books.GetAll().ToList());
        }

        public BookDto? GetBookById(int Id)
        { 
           var ibook = (from b in _books.GetAll()
                        where b.Id == Id
                        select b).FirstOrDefault();

            return SetBookDto(ibook);
        }

        public BookDto? AddBook(BookDto book)
        {
            var ibook = new Books() 
            {
                Author = book.Author,
                Isbn = book.Isbn,
                PublishedDate = book.PublishedDate, 
                Title = book.Title  
            };

            var savedBook = _books.Insert(ibook);

            return SetBookDto(savedBook);
        }

        public BookDto? UpdateBook(BookDto book)
        {
            var savedBook = (from e in _books.GetAll()
                             where e.Id == book.Id
                             select e).FirstOrDefault();

            if (savedBook != null)
            {
                savedBook.Author = book.Author;
                savedBook.PublishedDate = book.PublishedDate;
                savedBook.Title = book.Title;
                savedBook.Isbn = book.Isbn;

                var updatedBook = _books.Update(savedBook, m => m.Id == savedBook.Id);

                return SetBookDto(updatedBook);
            }

            return null;
        }

        public bool DeleteBook(int Id)
        {
            var savedBook = (from e in _books.GetAll()
                             where e.Id == Id
                             select e).FirstOrDefault();


            if (savedBook != null)
            {
                return _books.Delete(m => m.Id == savedBook.Id); ;
            }

            return false;
        }
        #endregion

    }
}

