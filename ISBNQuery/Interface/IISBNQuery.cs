namespace ISBNQuery.Interface
{
    internal interface IISBNQuery
    {
        bool IsValid(string isbn);
        Book SearchBook(string isbn);
        ReturnType ExpectedSuccessCode();
        ReturnType ValidateISBN(string isbn);
    }
}
