namespace ISBNQuery.Erros
{
    /// <summary>
    /// Triggers whenever a generic internet error occurs
    /// </summary>
    public class InternetException : Exception
    {
        /// <summary> 
        /// Throws a new exception 
        /// </summary> 
        /// <param name="message">Nice message</param> 
        /// <param name="innerException">Stack of previous error</param>
        public InternetException(string message, Exception innerException) : base(message, innerException)
        {
            //TODO: Implement necessary fields (if necessary)
        }
    }

    /// <summary>
    /// Fires whenever the server returns code 404
    /// </summary>
    public class InternetException404 : Exception
    {
        /// <summary> 
        /// Throws a new exception 
        /// </summary> 
        /// <param name="message">Nice message</param> 
        /// <param name="innerException">Stack of previous error</param>
        public InternetException404(string message, Exception innerException) : base(message, innerException)
        {
            //TODO: Implement necessary fields (if necessary)
        }
    }
}
