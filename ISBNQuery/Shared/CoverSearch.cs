using ISBNQuery.Erros;
using ISBNQuery.Models;
using SkiaSharp;

namespace ISBNQuery.Shared
{
    internal class CoverSearch
    {
        /// <summary>
        /// Obtém a capa de um exemplar, caso disponível
        /// </summary>
        public static async Task<SKImage> GetCompostImage(ImageSize size, Book book, CancellationToken cancellationToken)
        {
            if (book == null)
                ArgumentNullException.ThrowIfNull(book);

            if (book.HasCover)
            {
                // Se o livro possui CoverIds em numérico, pode-se tentar por ID, ou fallback por ISBN
                byte[] imageBytes;
                if (book.CoverIds != null && book.CoverIds.Count > 0)
                {
                    var coverInfo = new CoverInfo(CoverKeyType.Id, book.CoverIds[0].ToString(), size);
                    imageBytes = await GetImageBytes(coverInfo, cancellationToken);
                }
                else
                {
                    string key = (!string.IsNullOrWhiteSpace(book.ISBN13) ? book.ISBN13 : book.ISBN10)!;
                    var coverInfo = new CoverInfo(CoverKeyType.Isbn, key, size);
                    imageBytes = await GetImageBytes(coverInfo, cancellationToken);
                }

                SKImage image = ImageProcessor.GetImageFromByteArray(imageBytes);
                return image;
            }

            throw new BookException("Cover unavailable");
        }

        /// <summary>
        /// Obtém uma imagem (capa de livro ou foto de autor) com base no CoverInfo
        /// </summary>
        public static async Task<SKImage> GetImageAsync(CoverInfo coverInfo, CancellationToken cancellationToken)
        {
            if (coverInfo == null)
                ArgumentNullException.ThrowIfNull(coverInfo);

            byte[] imageBytes = await GetImageBytes(coverInfo, cancellationToken);
            return ImageProcessor.GetImageFromByteArray(imageBytes);
        }

        private static async Task<byte[]> GetImageBytes(CoverInfo coverInfo, CancellationToken cancellationToken)
        {
            return await DataDownload.DownloadAsyncData(coverInfo.Url, cancellationToken);
        }
    }
}
