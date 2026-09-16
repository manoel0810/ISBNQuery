using ISBNQuery.Erros;
using ISBNQuery.Interface;
using ISBNQuery.ISBNSearch;
using ISBNQuery.Models;
using ISBNQuery.Shared;
using SkiaSharp;

namespace ISBNQuery
{
    /// <summary>
    /// Classe principal com métodos para consulta de Livros, Autores e Capas/Fotos na Open Library API.
    /// </summary>
    public class Query
    {
        /// <summary>
        /// Efetua uma consulta na API e retorna um objeto <see cref="Book"/> com as informações do exemplar.
        /// </summary>
        /// <param name="isbn">Código ISBN-10 ou ISBN-13 para consulta</param>
        /// <param name="cancellationToken">Token de cancelamento</param>
        public static async Task<Book> SearchBook(string isbn, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrWhiteSpace(isbn))
                throw new ArgumentNullException(nameof(isbn));

            string temp = StringValidate.RemoveUnwantedCases(isbn);
            if (IsWrongLength(temp) || !StringValidate.IsNumeric(temp, true))
                throw new ArgumentException("isbn code wrong", nameof(isbn));

            IISBNQuery query = QueriableObject(temp);
            if (query.IsValid(temp) && query.ValidateISBN(temp) == query.ExpectedSuccessCode())
                return await query.SearchBook(temp, cancellationToken);

            throw new BookException("error while trying to obtain and/or create book object");
        }

        /// <summary>
        /// Alias assíncrono para <see cref="SearchBook"/>.
        /// </summary>
        public static Task<Book> SearchBookAsync(string isbn, CancellationToken cancellationToken = default)
        {
            return SearchBook(isbn, cancellationToken);
        }

        /// <summary>
        /// Consulta informações detalhadas sobre um autor na Open Library API.
        /// </summary>
        /// <param name="authorKey">Chave da Open Library do autor (ex: OL26320A ou /authors/OL26320A)</param>
        /// <param name="cancellationToken">Token de cancelamento</param>
        public static async Task<AuthorInfo> SearchAuthorAsync(string authorKey, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrWhiteSpace(authorKey))
                throw new ArgumentNullException(nameof(authorKey));

            return await DataDownload.DownloadAuthorDataAsync(authorKey, cancellationToken);
        }

        /// <summary>
        /// Realiza pesquisa de autores por nome na Open Library API.
        /// </summary>
        /// <param name="authorNameQuery">Termo ou nome do autor para busca</param>
        /// <param name="cancellationToken">Token de cancelamento</param>
        public static async Task<IReadOnlyList<AuthorInfo>> SearchAuthorsByNameAsync(string authorNameQuery, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrWhiteSpace(authorNameQuery))
                throw new ArgumentNullException(nameof(authorNameQuery));

            return await DataDownload.SearchAuthorsAsync(authorNameQuery, cancellationToken);
        }

        /// <summary>
        /// Obtém a capa do exemplar como <see cref="SKImage"/>.
        /// </summary>
        /// <param name="book">Objeto <see cref="Book"/> com dados do livro</param>
        /// <param name="size">Tamanho da imagem (S, M, L)</param>
        /// <param name="cancellationToken">Token de cancelamento</param>
        public static async Task<SKImage> SearchCover(Book book, ImageSize size, CancellationToken cancellationToken = default)
        {
            if (book == null)
                throw new ArgumentNullException(nameof(book));

            if (!book.HasCover)
                throw new BookException("no cover available");

            try
            {
                return await CoverSearch.GetCompostImage(size, book, cancellationToken);
            }
            catch (Exception e)
            {
                throw new Exception("image cover get error", e);
            }
        }

        /// <summary>
        /// Obtém a capa de um livro utilizando o modelo <see cref="CoverInfo"/>.
        /// </summary>
        public static async Task<SKImage> SearchCoverAsync(CoverInfo coverInfo, CancellationToken cancellationToken = default)
        {
            if (coverInfo == null)
                throw new ArgumentNullException(nameof(coverInfo));

            return await CoverSearch.GetImageAsync(coverInfo, cancellationToken);
        }

        /// <summary>
        /// Obtém a capa de um livro especificando tipo de chave, valor e tamanho.
        /// </summary>
        public static async Task<SKImage> SearchCoverAsync(string key, CoverKeyType keyType, ImageSize size, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrWhiteSpace(key))
                throw new ArgumentNullException(nameof(key));

            var coverInfo = new CoverInfo(keyType, key, size, isAuthorPhoto: false);
            return await CoverSearch.GetImageAsync(coverInfo, cancellationToken);
        }

        /// <summary>
        /// Obtém a foto de um autor especificando a chave/ID do autor e tamanho.
        /// </summary>
        public static async Task<SKImage> SearchAuthorPhotoAsync(string authorKeyOrPhotoId, ImageSize size, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrWhiteSpace(authorKeyOrPhotoId))
                throw new ArgumentNullException(nameof(authorKeyOrPhotoId));

            string cleanKey = authorKeyOrPhotoId.Replace("/authors/", "").Trim();
            CoverKeyType keyType = long.TryParse(cleanKey, out _) ? CoverKeyType.Id : CoverKeyType.Olid;
            var coverInfo = new CoverInfo(keyType, cleanKey, size, isAuthorPhoto: true);

            return await CoverSearch.GetImageAsync(coverInfo, cancellationToken);
        }

        private static bool IsWrongLength(string code)
        {
            return (code.Length > 13 || code.Length < 10);
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
