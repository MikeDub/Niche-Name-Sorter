using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Moq;
using NicheNameSorter.Factory;
using NicheNameSorter.Models;
using NicheNameSorter.Services;
using NicheNameSorter.UnitTests.Data;
using NUnit.Framework;

namespace NicheNameSorter.UnitTests.Services
{
    /// <summary>
    /// Tests the unit level functionality of the name sorting service.
    /// </summary>
    public class NameSortingServiceTests
    {
        /// <summary>
        /// Dependencies
        /// </summary>
        private Mock<FileInteropService> _fileServiceMock;
        private Mock<FullNameFactory> _factoryMock;

        /// <summary>
        /// Local output variables
        /// </summary>
        private List<string> _fileOutput;

        /// <summary>
        /// Service under test
        /// </summary>
        private NameSortingService _nameSortingService;

        [SetUp]
        public void Setup()
        {
            _factoryMock = new Mock<FullNameFactory>(new [] {' '});

            _factoryMock.Setup(x => x.CreateNewFullName(It.IsAny<string>()))
                .Returns<string>((x) => (GenerateFullName(x)));
                
            // In order to properly isolate these tests, we need to mock the reading / writing to text files.
            _fileServiceMock = new Mock<FileInteropService>();
        }

        #region Valid Scenarios

        [Test(Author = "Michael Whitman", Description = "Tests that a valid list of names read into the service gets sorted and written back out in the new correct")]
        public void SortName_WithValidListOfNames_SortsNamesInOrder()
        {
            // Arrange
            SetupValidFileMocks();
            _nameSortingService = new NameSortingService(_fileServiceMock.Object, _factoryMock.Object);

            string sourcePath = "./unsorted-names-list.txt";
            string destinationPath = "./sorted-names-list.txt";

            // Act
            Task.FromResult(_nameSortingService.SortNames(sourcePath, destinationPath));

            // Assert
            Assert.That(_fileOutput, Is.Not.Null.Or.Empty);
            Assert.That(_fileOutput, Is.EqualTo(TestData.ValidSortedListOfNamesAsStrings));
        }

        private void SetupValidFileMocks()
        {
            _fileServiceMock.Setup(x => x.ReadFromTextFile(It.IsAny<string>()))
                .ReturnsAsync(TestData.ValidUnsortedListOfNamesAsStrings);
            _fileServiceMock.Setup(x => x.WriteTextToFile(It.IsAny<string>(), It.IsAny<IEnumerable<IDisplayText>>()))
                .Callback<string, IEnumerable<IDisplayText>>(WriteOutToList);
        }

        #endregion

        
        #region Invalid Scenarios


        [Test(Author = "Michael Whitman", Description = "Tests that a invalid list of names read into the service throws an exception before sorting the names.")]
        public void SortName_WithInvalidFileContents_ThrowsException()
        {
            // Arrange
            SetupInvalidInputFileMocks();
            _nameSortingService = new NameSortingService(_fileServiceMock.Object, _factoryMock.Object);

            string sourcePath = "./unsorted-names-list.txt";
            string destinationPath = "./sorted-names-list.txt";

            // Act
            // Assert
            Assert.ThrowsAsync<Exception>(async() => await _nameSortingService.SortNames(sourcePath, destinationPath));
        }

        private void SetupInvalidInputFileMocks()
        {
            _fileServiceMock.Setup(x => x.ReadFromTextFile(It.IsAny<string>()))
                .ReturnsAsync(new List<string>());
            _fileServiceMock.Setup(x => x.WriteTextToFile(It.IsAny<string>(), It.IsAny<IEnumerable<IDisplayText>>()))
                .Callback<string, IEnumerable<IDisplayText>>(WriteOutToList);
        }

        #endregion


        #region Helpers

        /// <summary>
        /// Generates a full name for testing this service by abstracting out the factory functionality.
        /// </summary>
        private FullName GenerateFullName(string line)
        {
            var names = line.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            var givenNames = names.Take(names.Length - 1);
            return new FullName(givenNames.ToArray(), names.LastOrDefault());
        }

        /// <summary>
        /// Overrides the WriteTextToFile functionality and writes the text output to a list instead.
        /// This is so we can isolate this test away from file io and test the sorted results.
        /// </summary>
        private void WriteOutToList(string destPath, IEnumerable<IDisplayText> source)
        {
            _fileOutput = new List<string>();
            foreach (IDisplayText line in source)
            {
                var text = line.ToText();
                if (!string.IsNullOrEmpty(text))
                    _fileOutput.Add(text);
            }
        }

        #endregion
    }
}