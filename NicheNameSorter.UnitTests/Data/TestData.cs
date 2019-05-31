using System.Collections.Generic;
using NicheNameSorter.Models;

namespace NicheNameSorter.UnitTests.Data
{
    public static class TestData
    {
        #region Good / Affirmation Test Data

        public static FullName ValidFullNameWithThreeGivenNames
            => new FullName(new[] {"Bob", "Larry", "Good"}, "Vibes", new[] {' '});

        public static FullName ValidFullNameWithOneGivenName
            => new FullName(new[] {"Good"}, "Vibes", new[] {' '});


        public static List<string> ValidUnsortedListOfNamesAsStrings
            => new List<string>()
            {
                "Janet Parsons",
                "Vaughn Lewis",
                "Adonis Julius Archer",
                "Shelby Nathan Yoder",
                "Marin Alvarez",
                "London Lindsey",
                "Beau Tristan Bentley"
            };

        public static List<string> ValidSortedListOfNamesAsStrings
            => new List<string>()
            {
                "Marin Alvarez",
                "Adonis Julius Archer",
                "Beau Tristan Bentley",
                "Vaughn Lewis",
                "London Lindsey",
                "Janet Parsons",
                "Shelby Nathan Yoder"
            };

        #endregion

        #region Erraneous / Invalid Test Data



        public static List<string> InvalidListOfNamesAsStrings
            => new List<string>()
            {
                "Parsons",
                "Vaughn Lewis Adonis Julius Archer",
                " ",
            };


        #endregion
    }
}

