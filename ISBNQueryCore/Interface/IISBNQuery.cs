namespace ISBNQuery.Interface
{
    internal interface IISBNQuery
    {
        bool IsValid(string isbn);
        Task<Book> SearchBook(string isbn, CancellationToken cancellationToken);
        ReturnType ExpectedSuccessCode();
        ReturnType ValidateISBN(string isbn);
    }
}
