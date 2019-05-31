using System;
using System.Linq;
using NicheNameSorter.Exceptions;
using NicheNameSorter.Models;
using NicheNameSorter.Validation;

namespace NicheNameSorter.Factory
{
    /// <summary>
    /// Factory that allows us to construct instances of full names
    /// </summary>
    public class FullNameFactory
    {
        /// <summary>
        /// The character to look for in order to split / identify given names from each other and last name.
        /// </summary>
        private readonly char _nameSeparator;

        /// <summary>
        /// Constructs a new Full Name Factory
        /// </summary>
        public FullNameFactory(char[] separatedBy = null)
        {
            _nameSeparator = separatedBy?.FirstOrDefault() ?? ' ';
        }

        /// <summary>
        /// Creates a new full name using the specified name separation parameters on factory instantiation.
        /// </summary>
        public virtual FullName CreateNewFullName(string nameString)
        {
            if(string.IsNullOrEmpty(nameString))
                throw new InvalidNameException("Provided name cannot be null or empty.");

            string[] names = nameString.Split(_nameSeparator, StringSplitOptions.RemoveEmptyEntries);

            // Double check the split has been successful (in-case the separator is not present within the input)
            if (string.Equals(nameString, names.FirstOrDefault()))
                throw new InvalidNameException("Invalid name string provided or invalid separator specified, unable to split name string.");

            // Sanity check to ensure we have at least one given name (and not too many) and a last name.
            Validate.NamesFromStringInput(names);

            string lastName = names.LastOrDefault();
            // Given names are all the names except for the last name.
            string[] givenNames = names.Take(names.Length - 1).ToArray();

            return new FullName(givenNames, lastName, new []{ _nameSeparator});
        }
    }
}
