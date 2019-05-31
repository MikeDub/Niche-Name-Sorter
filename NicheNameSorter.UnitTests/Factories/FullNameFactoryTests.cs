using NicheNameSorter.Exceptions;
using NicheNameSorter.Factory;
using NUnit.Framework;

namespace NicheNameSorter.UnitTests.Factories
{
    [TestFixture]
    public class FullNameFactoryTests
    {
        /// <summary>
        /// Dependencies
        /// </summary>
        private readonly char[] _defaultSeparator = {' '};

        /// <summary>
        /// Factory under test
        /// </summary>
        private FullNameFactory _fullNameFactory;

        [SetUp]
        public void Setup()
        {
            _fullNameFactory = new FullNameFactory(_defaultSeparator);
        }

        [Test(Author = "Michael Whitman", Description = "Tests that a valid full name instance is created with a valid name string input.")]
        public void CreateNewFullName_WithValidNameString_CreatesNewFullNameInstance()
        {
            // Arrange
            string nameString = "James Smashing Bond";

            // Act
            var result = _fullNameFactory.CreateNewFullName(nameString);

            // Assert
            Assert.That(result.GivenNames, Is.EqualTo(new []{"James", "Smashing"}));
            Assert.That(result.LastName, Is.EqualTo("Bond"));
            Assert.That(result.SeparatedBy, Is.EqualTo(_defaultSeparator));
            Assert.That(result.ToText(), Is.EqualTo(nameString));
        }

        [Test(Author = "Michael Whitman", Description = "Tests that a valid full name instance is created with a valid name string input, but with a different separator.")]
        public void CreateNewFullName_WithValidNameAndAlternateSeparator_CreatesNewFullNameInstance()
        {
            // Test instantiation with a different separator
            // Arrange
            string nameString = "James,Smashing,Bond";
            var altSeparator = new[] {','};
            _fullNameFactory = new FullNameFactory(altSeparator);

            // Act
            var result = _fullNameFactory.CreateNewFullName(nameString);

            // Assert
            Assert.That(result.GivenNames, Is.EqualTo(new[] { "James", "Smashing" }));
            Assert.That(result.LastName, Is.EqualTo("Bond"));
            Assert.That(result.SeparatedBy, Is.EqualTo(altSeparator));
            // Test this still represents a comma separated string.
            Assert.That(result.ToText(), Is.EqualTo("James Smashing Bond"));
            Assert.That(result.ToString(), Is.EqualTo(nameString));
        }


        [Test(Author = "Michael Whitman", Description = "Tests that a input string that is null/empty or has less than two names throws an exception.")]
        public void CreateNewFullName_WithLessThanTwoNamesOrEmptyString_ThrowsInvalidNameException()
        {
            // Less that minimum requirement
            // Arrange
            string nameString = "Scrumptious";

            // Act && Assert
            Assert.Throws<InvalidNameException>(() => _fullNameFactory.CreateNewFullName(nameString));

            // Arrange
            string emptyString = "";

            // Act && Assert
            Assert.Throws<InvalidNameException>(() => _fullNameFactory.CreateNewFullName(null));
            Assert.Throws<InvalidNameException>(() => _fullNameFactory.CreateNewFullName(emptyString));

        }

        [Test(Author = "Michael Whitman", Description = "Tests that a input string with more than the allowed number of names throws an exception.")]
        public void CreateNewFullName_WithFiveOrMoreNames_ThrowsInvalidNameException()
        {
            // Arrange
            string nameString = "Scrumptious Apple Crumble Granny Smith";

            // Act && Assert
            Assert.Throws<InvalidNameException>(() => _fullNameFactory.CreateNewFullName(nameString));
        }

        [Test(Author = "Michael Whitman", Description = "Tests that an exception is thrown if the splitting of names has not been successful.")]
        public void CreateNewFullName_WithSeparatorMissingFromInput_ThrowsInvalidNameException()
        {
            // We make a wrong assumption that the string is separated by ';'
            // Arrange
            string nameString = "Holey;Dooley";

            // Act && Assert
            Assert.Throws<InvalidNameException>(() => _fullNameFactory.CreateNewFullName(nameString));

            // Or we instantiate the factory with the wrong separator -> ','
            // Arrange
            _fullNameFactory = new FullNameFactory(new [] { ','});
            nameString = "Holey Dooley";

            // Act && Assert
            Assert.Throws<InvalidNameException>(() => _fullNameFactory.CreateNewFullName(nameString));
        }

    }
}