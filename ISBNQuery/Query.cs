using ISBNQuery.Erros;
using ISBNQuery.Interface;
using ISBNQuery.ISBNSearch;
using ISBNQuery.Shared;
using System;
using System.Drawing;
using System.Threading;
using System.Threading.Tasks;

namespace ISBNQuery
{
    /// <summary>
    ///     <br>pt-br: Esta classe possue os métodos necessários para se obter as informações dos ISBN-10 e ISBN-13</br>
    ///     <br>en-us: This class has the methods necessary to obtain information from ISBN-10 and ISBN-13</br>
    /// </summary>

    public class Query
    {
        /// <summary>
        /// Efetua uma consulta na API e retorna um objeto <see cref="Book"/> com as informações do exemplar, caso exista
        /// </summary>
        /// <param name="isbn">Código ISBN 10/13 para consulta</param>
        /// <param name="cancellationToken">Token de cancelamento</param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        /// <exception cref="BookException"></exception>
        /// <exception cref="ArgumentNullException"></exception>

        public static async Task<Book> SearchBook(string isbn, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrWhiteSpace(isbn))
                throw new ArgumentNullException(nameof(isbn));

            string temp = StringValidate.RemoveUnwantedCases(isbn);
            if (IsWrongLenght(temp) || !StringValidate.IsNumeric(temp, true))
                throw new Exception("isbn code wrong");

            IISBNQuery query = QueriableObject(temp);
            if (query.IsValid(temp) && query.ValidateISBN(temp) == query.ExpectedSuccessCode())
                return await query.SearchBook(temp, cancellationToken);

            throw new BookException("error while trying to obtain and/or create book object");
        }

        private static bool IsWrongLenght(string code)
        {
            return (code.Length > 13 || code.Length < 10);
        }

        /// <summary>
        /// Obtém a capa do exemplar, caso disponível
        /// </summary>
        /// <param name="book">Objeto <see cref="Book"/> com os dados do exemplar</param>
        /// <param name="size">Define o tamanho da imagem a ser baixada</param>
        /// <param name="cancellationToken">Token de cancelamento</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="BookException"></exception>
        /// <exception cref="Exception"></exception>

        public static async Task<Image> SearchCover(Book book, ImageSize size, CancellationToken cancellationToken = default)
        {
            if (book == null)
                throw new ArgumentNullException(nameof(book));

            if (!book.HasCover)
                throw new BookException("no cover avaible");

            try
            {
                Image cover = await CoverSearch.GetCompostImage(size, book, cancellationToken);
                return cover;
            }
            catch (Exception e)
            {
                throw new Exception("image cover get error", e);
            }

        }

        private static IISBNQuery QueriableObject(string isbn)
        {
            if (isbn.Length == 10)
                return new ISBN10();
            else if (isbn.Length == 13)
                return new ISBN13();

            throw new Exception("ISBN query error while select interface to work");
        }
    }
}
