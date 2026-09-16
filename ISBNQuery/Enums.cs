namespace ISBNQuery
{
    /// <summary>
    /// Tamanhos disponíveis da imagem de capa ou foto de autor
    /// </summary>
    public enum ImageSize : int
    {
        /// <summary>
        /// Tamanho pequeno (Small - 'S')
        /// </summary>
        S = 83,

        /// <summary>
        /// Tamanho médio (Medium - 'M')
        /// </summary>
        M = 77,

        /// <summary>
        /// Tamanho grande (Large - 'L')
        /// </summary>
        L = 76
    }

    /// <summary>
    /// Tipos de retorno e resultados da validação de códigos ISBN
    /// </summary>
    public enum ReturnType : int
    {
        /// <summary>
        /// Indica que o código ISBN-13 é válido
        /// </summary>
        ValidISBN13 = 0,

        /// <summary>
        /// Indica que o código ISBN-13 é inválido
        /// </summary>
        InvalidISBN13 = 1,

        /// <summary>
        /// Indica que o tamanho do código ISBN-13 difere de 13 dígitos
        /// </summary>
        ISBN13LengthError = 2,

        /// <summary>
        /// Indica que o código ISBN-10 é válido
        /// </summary>
        ValidISBN10 = 3,

        /// <summary>
        /// Indica que o código ISBN-10 é inválido
        /// </summary>
        InvalidISBN10 = 4,

        /// <summary>
        /// Indica que o tamanho do código ISBN-10 difere de 10 dígitos
        /// </summary>
        ISBN10LengthError = 5,

        /// <summary>
        /// Indica que ocorreu um erro interno na validação
        /// </summary>
        InternalError = 6,

        /// <summary>
        /// Indica que a entrada fornecida era nula ou vazia
        /// </summary>
        NullArgument = 7,

        /// <summary>
        /// Indica que o formato de entrada é inválido
        /// </summary>
        InvalidFormat = 8,

        #region Obsolete Aliases for Backward Compatibility
        /// <summary>
        /// Alias legados para compatibilidade (obsoleto).
        /// </summary>
        [Obsolete("Utilize ISBN13LengthError")]
        ISBN13LenghtError = ISBN13LengthError,

        /// <summary>
        /// Alias legados para compatibilidade (obsoleto).
        /// </summary>
        [Obsolete("Utilize ISBN10LengthError")]
        ISBN10LenghtError = ISBN10LengthError,

        /// <summary>
        /// Alias legados para compatibilidade (obsoleto).
        /// </summary>
        [Obsolete("Utilize NullArgument")]
        NullArgumentException = NullArgument,

        /// <summary>
        /// Alias legados para compatibilidade (obsoleto).
        /// </summary>
        [Obsolete("Utilize InvalidFormat")]
        InvalidInputFormat = InvalidFormat
        #endregion
    }

    /// <summary>
    /// Métodos de extensão para o enum <see cref="ReturnType"/>
    /// </summary>
    public static class ReturnTypeExtensions
    {
        /// <summary>
        /// Retorna true se o resultado da validação indicar um ISBN válido (ISBN-10 ou ISBN-13)
        /// </summary>
        public static bool IsSuccess(this ReturnType result)
        {
            return result == ReturnType.ValidISBN10 || result == ReturnType.ValidISBN13;
        }
    }
}
