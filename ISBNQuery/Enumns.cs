namespace ISBNQuery
{
    /// <summary>
    /// Tamanhos disponíveis da imagem
    /// </summary>

    [Flags]

    public enum ImageSize : int
    {
        /// <summary>
        /// Small size
        /// </summary>
        S = 83,
        /// <summary>
        /// Medium size
        /// </summary>
        M = 77,
        /// <summary>
        /// Large size
        /// </summary>
        L = 76
    }

    /// <summary>
    /// Retornos possíveis da validação
    /// </summary>

    [Flags]
    public enum ReturnType : int
    {
        /// <summary>
        /// Indica que o ISBN-13 passado é válido
        /// </summary>
        ValidISBN13 = 0x0,
        /// <summary>
        /// Indica que o ISBN-13 passado é inválido
        /// </summary>
        InvalidISBN13 = 0x1,
        /// <summary>
        /// Indica que o tamanho do fluxo de caracteres do código ISBN-13 difere de 13 dígitos
        /// </summary>
        ISBN13LenghtError = 0x2,
        /// <summary>
        /// Indica que o ISBN-10 é válido
        /// </summary>
        ValidISBN10 = 0x3,
        /// <summary>
        /// Indica que o ISBN-10 é inválido
        /// </summary>
        InvalidISBN10 = 0x4,
        /// <summary>
        /// Indica que o tamanho do fluxo de caracteres do código ISBN-10 difere de 10 dígitos
        /// </summary>
        ISBN10LenghtError = 0x5,
        /// <summary>
        /// Indica que ocorreu um erro na operação de validação
        /// </summary>
        InternalError = 0x6,
        /// <summary>
        /// Indica que a entrada do método de verificação foi <c>null</c>
        /// </summary>
        NullArgumentException = 0x7,
        /// <summary>
        /// Indica que o formato de entrada do fluxo do código ISBN era inválido
        /// </summary>
        InvalidInputFormat = 0x8
    }
}
