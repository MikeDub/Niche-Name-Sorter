using System;
using System.Linq;

namespace NicheNameSorter.Models
{
    /// <summary>
    /// Represents someones Full name
    /// </summary>
    public class FullName : IDisplayText, IComparable<FullName>
    {
        /// <summary>
        /// Constructs a new Immutable Full Name instance
        /// </summary>
        public FullName(string[] givenNames, string lastName, char[] separatedBy = null)
        {
            GivenNames = givenNames;
            LastName = lastName;
            SeparatedBy = separatedBy ?? new [] {' '};
        }

        /// <summary>
        /// Represents 1 or more given names within the Full Name
        /// </summary>
        public string[] GivenNames { get; private set; }

        /// <summary>
        /// Represents a persons last name
        /// </summary>
        public string LastName { get; private set; }

        /// <summary>
        /// What characters separate a person's names'. Ie. ' ' for a space.
        /// To funkify things a little, you can separate by more then a single character if someone wants to be fussy :) 
        /// </summary>
        public char[] SeparatedBy { get; private set; }

        /// <summary>
        /// Compare one FullName to another
        /// </summary>
        public int CompareTo(FullName other)
        {
            // Compare the last name first
            var lastNameComparison = String.Compare(this.LastName, other.LastName, StringComparison.Ordinal);
            if (lastNameComparison != 0)
                return lastNameComparison;

            // If the given names are equal, then return that this is equal.
            if (this.GivenNames.SequenceEqual(other.GivenNames))
                return 0;

            // Otherwise return the difference of the given names
            foreach (string givenName in this.GivenNames)
            {
                // Iterate through each given name and return a difference if there is one.
                foreach (string otherGivenName in other.GivenNames)
                {
                    var result = String.Compare(givenName, otherGivenName, StringComparison.Ordinal);
                    if (result != 0)
                        return result;
                }
            }

            // Otherwise return that they are the same, this is just a fall-through in theory should not get to this point.
            return 0;
        }

        /// <summary>
        /// Full Name as a String with the indicated separator, only supports first separator.
        /// </summary>
        public override string ToString()
        {
            return $"{string.Join(SeparatedBy.FirstOrDefault(), GivenNames)}{SeparatedBy.FirstOrDefault()}{LastName}";
        }

        /// <summary>
        /// Displays a person's FullName, with each name part separated by spaces.
        /// </summary>
        public string ToText()
        {
            return $"{string.Join(" ", GivenNames)} {LastName}";
        }
    }
}
