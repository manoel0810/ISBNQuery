using ISBNQuery.Erros;
using ISBNQuery.Interface;
using ISBNQuery.Shared;
using System;

namespace ISBNQuery.ISBNSearch
{
    /// <summary>
    /// Representa a estrutura para o código ISBN-13
    /// </summary>

    public class ISBN13 : IISBNQuery
    {
        /// <summary>
        /// Retorna o valor padrão para sucesso da valição do ISBN
        /// </summary>
        /// <returns></returns>
        public ReturnType ExpectedSuccessCode() => ReturnType.ValidISBN13;

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

        public Book SearchBook(string isbn)
        {
            try
            {
                return BookSearch.Search(this, isbn);
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

            if (!StringValidate.IsNumeric(isbn))
                return ReturnType.InvalidInputFormat;

            if (isbn.Length != 0xd /*13DEC*/)
                return ReturnType.ISBN13LenghtError;

            int[] Produtos = new int[0xc], ISBN = new int[0xd];
            for (int Position = 0x0; Position < ISBN.Length; Position++)
                ISBN[Position] = int.Parse(isbn.Substring(Position, 0x1));

            for (int Elemento = 0x0; Elemento < 0xc; Elemento++)
                Produtos[Elemento] = ISBN[Elemento] * (Elemento % 0x2 == 0x0 ? 0x1 : 0x3);

            MathHelp.Sum(Produtos, out int Resultado);
            int Test = (Resultado + ISBN[0xc]) % 0xa;
            if (Test == 0x0)
                return ReturnType.ValidISBN13;
            else
                return ReturnType.InvalidISBN13;
        }
    }
}
