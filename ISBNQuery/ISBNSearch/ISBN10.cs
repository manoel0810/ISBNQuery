using ISBNQuery.Erros;
using ISBNQuery.Interface;
using ISBNQuery.Shared;
using System;
using System.Threading.Tasks;

namespace ISBNQuery.ISBNSearch
{
    /// <summary>
    /// Representa a estrutura para o código ISBN-10
    /// </summary>

    public class ISBN10 : IISBNQuery
    {
        /// <summary>
        /// Retorna o valor padrão para sucesso da valição do ISBN
        /// </summary>
        /// <returns></returns>
        public ReturnType ExpectedSuccessCode() => ReturnType.ValidISBN10;

        /// <summary>
        /// Verifica está no formato válido
        /// </summary>
        /// <param name="value">Valor para teste</param>
        /// <returns>true, caso esteja no formato válido</returns>

        public bool IsValid(string value)
        {
            return StringValidate.IsNumeric(value, true);
        }

        /// <summary>
        /// Obtém os dados associados a um ISBN e retorna um objeto do tipo <see cref="Book"/>, caso exista
        /// </summary>
        /// <param name="isbn">Código ISBN para consulta</param>
        /// <returns>Um objeto <see cref="Book"/> com os dados disponíveis na API da Open Libary</returns>
        /// <exception cref="BookException"></exception>

        public async Task<Book> SearchBook(string isbn)
        {
            try
            {
                return await BookSearch.Search(this, isbn);
            }
            catch (Exception ex)
            {
                throw new BookException("error while book search", ex);
            }
        }
        /// <summary>
        /// Verifica se um código ISBN é válido e está com o dígito de verificação correto
        /// </summary>
        /// <param name="isbn">Código ISBN para verificação</param>
        /// <returns>Retorna uma flag do tipo <see cref="ReturnType"/> com o resultado do teste</returns>

        public ReturnType ValidateISBN(string isbn)
        {
            if (string.IsNullOrEmpty(isbn))
                return ReturnType.NullArgumentException;

            if (!StringValidate.IsNumeric(isbn, true))
                return ReturnType.InvalidInputFormat;

            if (isbn.Length != 0xa /*10DEC*/)
                return ReturnType.ISBN10LenghtError;

            int[] Produtos = new int[0x9], ISBN = new int[0xa];
            for (int Position = 0x0; Position < ISBN.Length; Position++)
                ISBN[Position] = isbn.Substring(Position, 0x1).ToUpper() == "X" ? 0xa : int.Parse(isbn.Substring(Position, 0x1));

            for (int Elemento = 0x0; Elemento < 0x9; Elemento++)
                Produtos[Elemento] = ISBN[Elemento] * (Elemento + 0x1);

            MathHelp.Sum(Produtos, out int Resultado);
            int Test = Resultado % 0xb;

            if (Test == ISBN[0x9])
                return ReturnType.ValidISBN10;
            else
                return ReturnType.InvalidISBN10;
        }
    }
}
