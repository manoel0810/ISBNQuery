using ISBNQuery.Erros;
using ISBNQuery.Interface;
using ISBNQuery.Shared;

namespace ISBNQuery.ISBNSearch
{
    /// <summary>
    /// Representa a estrutura para validação e consulta do código ISBN-13
    /// </summary>
    public class ISBN13 : IISBNQuery
    {
        /// <summary>
        /// Retorna o valor de sucesso da validação do ISBN-13
        /// </summary>
        public ReturnType ExpectedSuccessCode() => ReturnType.ValidISBN13;

        /// <summary>
        /// Verifica se o código contém apenas caracteres numéricos
        /// </summary>
        public bool IsValid(string value)
        {
            return StringValidate.IsNumeric(value, true);
        }

        /// <summary>
        /// Obtém os dados associados a um ISBN-13 e retorna um objeto <see cref="Book"/>
        /// </summary>
        public async Task<Book> SearchBook(string isbn, CancellationToken cancellationToken)
        {
            try
            {
                return await BookSearch.Search(this, isbn, cancellationToken);
            }
            catch (Exception ex)
            {
                throw new BookException("error while book search", ex);
            }
        }

        /// <summary>
        /// Verifica se o código ISBN-13 é válido segundo o algoritmo do dígito verificador
        /// </summary>
        public ReturnType ValidateISBN(string isbn)
        {
            if (string.IsNullOrEmpty(isbn))
                return ReturnType.NullArgument;

            if (!StringValidate.IsNumeric(isbn))
                return ReturnType.InvalidFormat;

            if (isbn.Length != 13)
                return ReturnType.ISBN13LengthError;

            int[] produtos = new int[12];
            int[] isbnDigits = new int[13];

            for (int position = 0; position < isbnDigits.Length; position++)
            {
                isbnDigits[position] = int.Parse(isbn.Substring(position, 1));
            }

            for (int i = 0; i < 12; i++)
            {
                produtos[i] = isbnDigits[i] * (i % 2 == 0 ? 1 : 3);
            }

            MathHelp.Sum(produtos, out int resultado);
            int test = (resultado + isbnDigits[12]) % 10;

            if (test == 0)
                return ReturnType.ValidISBN13;
            else
                return ReturnType.InvalidISBN13;
        }
    }
}
