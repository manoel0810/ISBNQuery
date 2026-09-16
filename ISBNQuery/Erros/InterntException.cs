namespace ISBNQuery.Erros
{
    /// <summary>
    /// Disparado quando ocorre um erro genérico de rede/internet
    /// </summary>
    public class InternetException(string message, Exception? innerException = null) : Exception(message, innerException)
    {
    }

    /// <summary>
    /// Disparado quando o servidor remoto retorna HTTP 404 (Recurso não encontrado)
    /// </summary>
    public class InternetException404(string message, Exception? innerException = null) : Exception(message, innerException)
    {
    }
}
