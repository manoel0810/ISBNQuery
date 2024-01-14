using System.Threading.Tasks;

namespace ISBNQuery.Interface
{
    internal interface IISBNQuery
    {
        bool IsValid(string isbn);
        Task<Book> SearchBook(string isbn);
        ReturnType ExpectedSuccessCode();
        ReturnType ValidateISBN(string isbn);
    }
}
