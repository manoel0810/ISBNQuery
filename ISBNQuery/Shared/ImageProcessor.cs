using ISBNQuery.Erros;
using System;
using System.Drawing;

namespace ISBNQuery.Shared
{
    internal class ImageProcessor
    {
        /// <summary>
        /// Converte um array de bytes na sua imagem correspondente
        /// </summary>
        /// <param name="bytes">Bytes da imagem</param>
        /// <returns>Imagem convertida</returns>

        public static Image GetImageFromByteArray(byte[] bytes)
        {
            if (bytes == null)
                throw new ArgumentNullException(nameof(bytes));

            try
            {
                using (System.IO.MemoryStream Stream = new System.IO.MemoryStream(bytes))
                {
                    var image = Image.FromStream(Stream);
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
