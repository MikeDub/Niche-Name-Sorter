using System;
using NicheNameSorter.Exceptions;

namespace NicheNameSorter.Validation
{
    /// <summary>
    /// Validation helper class
    /// </summary>
    public static class Validate
    {
        /// <summary>
        /// Does the names from the string input meet the required conditions?
        /// </summary>
        public static void NamesFromStringInput(string[] names = null)
        {
            if (names.NullOrEmpty())
                throw new ArgumentNullException(nameof(names), "Names input cannot be null.");

            // Must have at least 1 given name 
            if (names?.Length <= 1)
                throw new InvalidNameException(FormatException("At least one given name and one last name needs to be specified.", names));

            // and may have up to 3 given names (3 given, 1 last = 4 name parts).
            if (names?.Length > 4)
                throw new InvalidNameException(FormatException("Cannot have more than 3 given names specified.", names));
        }

        /// <summary>
        /// Formats a text array exception for display on the client.
        /// </summary>
        private static string FormatException(string message, string[] dataSource)
        {
            var errorMessage = $"{message}.{Environment.NewLine}Where it went wrong: {string.Join(' ', dataSource)}.";
            return errorMessage;
        }

        #region Extension Methods

        /// <summary>
        /// Static array helper method to check if the array is either null or contains no elements.
        /// </summary>
        public static bool NullOrEmpty(this string[] source)
        {
            if (source == null || source.Length == 0)
                return true;

            return false;
        }

        #endregion

    }
}
