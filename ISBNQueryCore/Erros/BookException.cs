namespace ISBNQuery.Erros
{
    /// <summary>
    /// Generic error when creating the Book object
    /// </summary>
    public class BookException : Exception
    {
        /// <summary>
        /// Throws a new exception 
        /// </summary>
        /// <param name="message">Nice message</param>
        public BookException(string message) : base(message)
        {
            //TODO: Implement necessary fields (if necessary)
        }

        /// <summary> 
        /// Throws a new exception 
        /// </summary> 
        /// <param name="message">Nice message</param> 
        /// <param name="innerException">Stack of previous error</param>

        public BookException(string message, Exception innerException) : base(message, innerException)
        {
            //TODO: Implement necessary fields (if necessary)
        }
    }
}
