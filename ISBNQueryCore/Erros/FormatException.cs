namespace ISBNQuery.Erros
{
    /// <summary>
    /// Triggers whenever a generic formatting error occurs
    /// </summary>
    public class FormatException : System.FormatException
    {
        private readonly ReturnType type;

        /// <summary> 
        /// Throws a new exception 
        /// </summary> 
        /// <param name="message">Nice message</param> 
        /// <param name="innerException">Stack of previous error</param>
        /// <param name="return">Type of casting error</param>
        public FormatException(string message, Exception innerException, ReturnType @return) : base(message, innerException)
        {
            type = @return;
        }

        /// <summary>
        /// Gets the Exception cast error
        /// </summary>
        public ReturnType ReturnType => type;
    }

    /// <summary>
    /// Triggers whenever a generic formatting error occurs
    /// </summary>
    public class FormatExceptionArgument : ArgumentException
    {
        private readonly ReturnType type;

        /// <summary> 
        /// Throws a new exception 
        /// </summary> 
        /// <param name="message">Nice message</param> 
        /// <param name="innerException">Stack of previous error</param>
        /// <param name="return">Type of casting error</param>
        public FormatExceptionArgument(string message, Exception innerException, ReturnType @return) : base(message, innerException)
        {
            type = @return;
        }

        /// <summary>
        /// Gets the Exception cast error
        /// </summary>
        public ReturnType ReturnType => type;
    }
}
