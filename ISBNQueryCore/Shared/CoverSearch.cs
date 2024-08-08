using ISBNQuery.Erros;
using SkiaSharp;

namespace ISBNQuery.Shared
{
    internal class CoverSearch
    {
        private static readonly string _apiRoute = "https://covers.openlibrary.org/b/isbn/";

        /// <summary>
        /// Obtém a capa de um exemplar, caso disponível
        /// </summary>
        /// <param name="size">Tamanho da imagem</param>
        /// <param name="book">Objeto <see cref="Book"/> com as informações sobre o exemplar</param>
        /// <returns>Um <see cref="SKImage"/> com a capa, se disponível</returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="BookException"></exception>

        public static async Task<SKImage> GetCompostImage(ImageSize size, Book book)
        {
            if (book == null)
                ArgumentNullException.ThrowIfNull(nameof(book));

            if (book!.HasCover)
            {
                try
                {
                    byte[] imageBytes = await GetImageBytes((string.IsNullOrWhiteSpace(book.ISBN13) ? book.ISBN10 : book.ISBN13)!, size);
                    SKImage image = ImageProcessor.GetImageFromByteArray(imageBytes);
                    return image;
                }
                catch (Exception)
                {
                    throw;
                }
            }

            throw new BookException("Cover unvaible");
        }

        private static async Task<byte[]> GetImageBytes(string key, ImageSize size)
        {
            string endPoint = string.Format("{0}{1}-{2}.jpg", _apiRoute, key, ((char)size).ToString());
            return await DataDownload.DownloadAsyncData(endPoint);
        }
    }
}
