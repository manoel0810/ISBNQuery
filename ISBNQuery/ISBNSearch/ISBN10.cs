using ISBNQuery.Erros;
using ISBNQuery.Interface;
using ISBNQuery.Shared;

namespace ISBNQuery.ISBNSearch
{
    /// <summary>
    /// Representa a estrutura para validação e consulta do código ISBN-10
    /// </summary>
    public class ISBN10 : IISBNQuery
    {
        /// <summary>
        /// Retorna o valor de sucesso da validação do ISBN-10
        /// </summary>
        public ReturnType ExpectedSuccessCode() => ReturnType.ValidISBN10;

        /// <summary>
        /// Verifica se o código contém apenas caracteres numéricos ou 'X' no final
        /// </summary>
        public bool IsValid(string value)
        {
            return StringValidate.IsNumeric(value, true);
        }

        /// <summary>
        /// Obtém os dados associados a um ISBN-10 e retorna um objeto <see cref="Book"/>
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
        /// Verifica se o código ISBN-10 é válido segundo o algoritmo do dígito verificador
        /// </summary>
        public ReturnType ValidateISBN(string isbn)
        {
            if (string.IsNullOrEmpty(isbn))
                return ReturnType.NullArgument;

            if (!StringValidate.IsNumeric(isbn, true))
                return ReturnType.InvalidFormat;

            if (isbn.Length != 10)
                return ReturnType.ISBN10LengthError;

            int[] produtos = new int[9];
            int[] isbnDigits = new int[10];

            for (int position = 0; position < isbnDigits.Length; position++)
            {
                string charAt = isbn.Substring(position, 1);
                isbnDigits[position] = charAt.Equals("X", StringComparison.OrdinalIgnoreCase) ? 10 : int.Parse(charAt);
            }

            for (int i = 0; i < 9; i++)
            {
                produtos[i] = isbnDigits[i] * (i + 1);
            }

            MathHelp.Sum(produtos, out int resultado);
            int test = resultado % 11;

            if (test == isbnDigits[9])
                return ReturnType.ValidISBN10;
            else
                return ReturnType.InvalidISBN10;
        }
    }
}
