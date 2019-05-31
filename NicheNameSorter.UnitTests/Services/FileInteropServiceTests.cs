using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using NicheNameSorter.Models;
using NicheNameSorter.Services;
using NUnit.Framework;

namespace NicheNameSorter.UnitTests.Services
{
    [TestFixture]
    public class FileInteropServiceTests
    {
        /// <summary>
        /// Dependencies
        /// </summary>
        private IEnumerable<IDisplayText> _sampleDataSource;

        /// <summary>
        /// Service under test
        /// </summary>
        private FileInteropService _fileInteropService;

        [SetUp]
        public void Setup()
        {
            _fileInteropService = new FileInteropService();
            _sampleDataSource = new List<IDisplayText> { new FullName(new[] { "Bob" }, "Sinclar", new[] { ' ' }) };
        }

        [Test(Author = "Michael Whitman", Description = "Tests that a valid file path reads and returns the data from the text file.")]
        public async Task ReadFromTextFile_WithValidDataSourceFile_OutputsFileContentsToCollection()
        {
            // Arrange
            var filePath = "./unsorted-names-list.txt";

            // Act
            var result = await _fileInteropService.ReadFromTextFile(filePath);
            var outcome = result.ToList();

            // Assert
            Assert.That(outcome, Is.Not.Null.Or.Empty);
            Assert.That(outcome.Count, Is.GreaterThan(0));
        }

        [Test(Author = "Michael Whitman", Description = "Tests that an invalid file path ie. null or empty, throws an exception.")]

        public void ReadFromTextFile_WithInvalidFilePath_ThrowsArgumentNullException()
        {
            // Arrange
            string emptyFilePath = string.Empty;

            // Act && Assert
            Assert.ThrowsAsync<ArgumentNullException>(() => _fileInteropService.ReadFromTextFile(null));
            Assert.ThrowsAsync<ArgumentNullException>(() => _fileInteropService.ReadFromTextFile(emptyFilePath));
        }

        [Test(Author = "Michael Whitman", Description = "Tests that a valid file path, but missing file, throws an exception.")]
        public void ReadFromTextFile_WithMissingFile_ThrowsFileNotFoundException()
        {
            // Arrange
            string missingFilePath = "file-that-should-not-exist.txt";

            // Act && Assert
            Assert.ThrowsAsync<FileNotFoundException>(() => _fileInteropService.ReadFromTextFile(missingFilePath));
        }

        [Test(Author = "Michael Whitman", Description = "Tests that with a valid file path and data collection writes the data to file without exceptions.")]
        public async Task WriteTextToFile_WithValidFilePathAndCollection_WritesToFileWithoutExceptions()
        {
            // Arrange
            string filePath = "./sorted-names-list.txt";

            // Act
            await _fileInteropService.WriteTextToFile(filePath, _sampleDataSource);

            // Assert
            Assert.Pass();
        }

        [Test(Author = "Michael Whitman", Description = "Tests that with an invalid file path (ie. null or empty) an exception is thrown.")]
        public void WriteTextToFile_WithInvalidFilePath_ThrowsArgumentNullException()
        {
            // Arrange
            string invalidFilePath = string.Empty;

            // Act && Assert
            Assert.ThrowsAsync<ArgumentNullException>(() => _fileInteropService.WriteTextToFile(invalidFilePath, _sampleDataSource));
        }

        [Test(Author = "Michael Whitman", Description = "Tests that with a null data/source collection an exception is thrown.")]
        public void WriteTextToFile_WithNullSourceCollection_ThrowsArgumentNullException()
        {
            // Arrange
            string filePath = "./sorted-names-list.txt";

            // Act && Assert
            Assert.ThrowsAsync<ArgumentNullException>(() => _fileInteropService.WriteTextToFile(filePath, null));
        }

        [Test(Author = "Michael Whitman", Description = "Tests that with a empty data/source collection an exception is thrown.")]
        public void WriteTextToFile_WithEmptySourceCollection_ThrowsArgumentNullException()
        {
            // Arrange
            string filePath = "./sorted-names-list.txt";
            IEnumerable<IDisplayText> emptyDataSource = new List<IDisplayText>() { };

            // Act && Assert
            Assert.ThrowsAsync<ArgumentNullException>(() => _fileInteropService.WriteTextToFile(filePath, emptyDataSource));
        }
    }
}