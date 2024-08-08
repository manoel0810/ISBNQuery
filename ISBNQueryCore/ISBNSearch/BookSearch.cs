using ISBNQuery.Erros;
using ISBNQuery.Interface;
using ISBNQuery.Shared;

namespace ISBNQuery.ISBNSearch
{
    internal class BookSearch
    {
        /// <summary>
        /// Busca por um exemplar com base no código fornecido
        /// </summary>
        /// <param name="query">Interface para o código</param>
        /// <param name="isbn">Código de busca</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="BookException"></exception>

        public static async Task<Book> Search(IISBNQuery query, string isbn)
        {
            if (string.IsNullOrWhiteSpace(isbn))
                throw new ArgumentNullException(nameof(isbn));

            string temp = StringValidate.RemoveUnwantedCases(isbn);
            if (query.IsValid(temp) && query.ValidateISBN(temp) == query.ExpectedSuccessCode())
            {
                try
                {
                    Book book = await DataDownload.DownloadBookDataAsync(temp);
                    return book;
                }
                catch (Exception e)
                {
                    throw new BookException("there was some error while book data download", e);
                }
            }
            else
                throw new BookException($"invalid isbn code. value is {isbn}");
        }
    }
}
