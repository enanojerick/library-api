using Library.Dto;

namespace Library.Contracts
{
    public interface IBookService
    {
        IList<BookDto> GetBooks();
        BookDto? GetBookById(int Id);
        BookDto? AddBook(BookDto book);
        BookDto? UpdateBook(BookDto book);
        bool DeleteBook(int Id);
    }
}
