using ISBNQuery.Erros;
using SkiaSharp;

namespace ISBNQuery.Shared
{
    internal class ImageProcessor
    {
        /// <summary>
        /// Converte um array de bytes em uma imagem <see cref="SKImage"/> correspondente
        /// </summary>
        /// <param name="bytes">Bytes da imagem</param>
        /// <returns>Objeto <see cref="SKImage"/> com a imagem carregada</returns>
        public static SKImage GetImageFromByteArray(byte[] bytes)
        {
            if (bytes == null)
                throw new ArgumentNullException(nameof(bytes));

            try
            {
                using var stream = new MemoryStream(bytes);
                var image = SKImage.FromEncodedData(stream);
                if (image == null)
                    throw new BookException("Failed to decode image from byte array");
                return image;
            }
            catch (Exception e)
            {
                throw new BookException("Generic error while getting cover", e);
            }
        }
    }
}
