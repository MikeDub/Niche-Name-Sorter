using System;

namespace NicheNameSorter.Exceptions
{
    /// <summary>
    /// A generic exception thrown when a name doesn't meet the requirements for a full name defined in our requirements.
    /// Ie. A name must have at least 1 given name and may have up to 3 given names.
    /// </summary>
    public class InvalidNameException : Exception
    {
        private const string DEFAULT_ERROR_MESSAGE = "The specified name doesn't meet the minimum requirements.";

        /// <summary>
        /// Constructs a new InvalidNameException
        /// </summary>
        public InvalidNameException(string errorMessage = null) : base(errorMessage ?? DEFAULT_ERROR_MESSAGE)
        {
            
        }
    }
}
