using ISBNQuery.Erros;
using SkiaSharp;

namespace ISBNQuery.Shared
{
    internal class ImageProcessor
    {
        /// <summary>
        /// Converte um array de bytes na sua imagem correspondente
        /// </summary>
        /// <param name="bytes">Bytes da imagem</param>
        /// <returns>Imagem convertida</returns>

        public static SKImage GetImageFromByteArray(byte[] bytes)
        {
            if (bytes == null)
                ArgumentNullException.ThrowIfNull(nameof(bytes), null);

            try
            {
                using (MemoryStream Stream = new MemoryStream(bytes!))
                {
                    var image = SKImage.FromEncodedData(Stream);
                    return image;
                }
            }
            catch (Exception e)
            {
                throw new BookException("Generic error while getting cover", e);
            }
        }
    }
}
