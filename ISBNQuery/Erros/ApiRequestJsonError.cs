using System;

namespace ISBNQuery.Erros
{
    /// <summary>
    /// Fired when an api query returns no json value to create Book objects
    /// </summary>

    public class ApiRequestJsonError : Exception
    {
        /// <summary> 
        /// Throws a new exception 
        /// </summary> 
        /// <param name="message">Nice message</param> 
        /// <param name="innerException">Stack of previous error</param>
        public ApiRequestJsonError(string message, Exception innerException) : base(message, innerException)
        {

        }
    }
}
