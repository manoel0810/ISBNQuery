namespace ISBNQuery.Shared
{
    internal class MathHelp
    {
        /// <summary>
        ///     Efetua uma soma finita de n termos do array [Nuns] e devolve sua soma em <c><b>out</b> int Resultado</c>
        /// </summary>
        /// <param name="Nuns">Recebe um array do tipo int com os números que serão somados</param>
        /// <param name="Resultado">Variável de saída por construtor</param>

        public static void Sum(int[] Nuns, out int Resultado)
        {
            int Soma = 0x0;
            foreach (int Value in Nuns)
                Soma += Value;

            Resultado = Soma;
        }
    }
}
