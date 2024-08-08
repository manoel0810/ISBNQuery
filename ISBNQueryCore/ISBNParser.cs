using ISBNQuery.Erros;
using ISBNQuery.Shared;

namespace ISBNQuery
{
    /// <summary>
    /// Fornece métodos para conversão e operações com códigos ISBN
    /// </summary>

    public class ISBNParser
    {
        /// <summary>
        /// Converte um ISBN-10 em ISBN-13 e vice versa
        /// </summary>
        /// <param name="isbn">Código para conversão</param>
        /// <returns>Um código ISBN10 ou ISBN13</returns>
        /// <exception cref="FormatExceptionArgument"></exception>
        /// <exception cref="ArgumentNullException"></exception>

        public static string ParseISBN(string isbn)
        {
            if (string.IsNullOrWhiteSpace(isbn))
                throw new ArgumentNullException(nameof(isbn));

            string temp = StringValidate.RemoveUnwantedCases(isbn);
            if (temp.Length == 10)
                return Parse10(isbn);
            else if (temp.Length == 13)
                return Parse13(isbn);


            throw new FormatExceptionArgument("ISBN code wrong", new Exception(), ReturnType.InvalidInputFormat);
        }

        private static string Parse10(string isbn)
        {
            int Soma = 0, fator = 1, digito = 0;
            int[] Valores = new int[12];

            Valores[0] = 9;
            Valores[1] = 7;
            Valores[2] = 8;

            for (int i = 3; i < 12; i++)
                Valores[i] = int.Parse(Convert.ToString(isbn[i - 3]));

            for (int i = 0; i < 12; i++)
            {
                Soma += Valores[i] * fator;
                fator = fator == 1 ? 3 : 1;
            }

            int mod = Soma % 10;
            if (mod != 0) { digito = 10 - mod; }

            return String.Format("978{0}{1}", isbn.Substring(0, 9), digito);
        }

        private static string Parse13(string isbn)
        {
            int[] Valores = new int[9];
            for (int i = 3; i < 12; i++)
                Valores[i - 3] = int.Parse(isbn[i].ToString());

            int Soma = 0, fator = 10;
            for (int i = 0; i < 9; i++)
                Soma += Valores[i] * (fator - i);

            int mod = Soma % 11;
            int digito = 0;
            if (mod != 0) { digito = 11 - mod; }

            string part = "";
            foreach (int i in Valores)
                part += i.ToString();

            return String.Format("{0}{1}", part, digito != 10 ? digito.ToString() : "X");
        }
    }
}
