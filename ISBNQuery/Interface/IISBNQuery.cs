using System.Drawing;

namespace ISBNQuery.Interface
{
    internal interface IISBNQuery
    {
        bool IsValid(string isbn);
        Book SearchBook(string isbn);
        ReturnType ExpectedSuccessCode();
        ReturnType ValidateISBN(string isbn);
        Image GetBookCover(Book book, ImageSize size);
    }
}
