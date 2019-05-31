using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NicheNameSorter.Factory;
using NicheNameSorter.Models;

namespace NicheNameSorter.Services
{
    /// <summary>
    /// Service that takes a list of file 
    /// </summary>
    public class NameSortingService
    {
        /// <summary>
        /// Service for reading / writing to text files
        /// </summary>
        private readonly FileInteropService _fileInteropService;

        /// <summary>
        /// Factory for creating full names
        /// </summary>
        private readonly FullNameFactory _fullNameFactory;
        
        /// <summary>
        /// Constructs a new NameSortingService
        /// </summary>
        public NameSortingService(FileInteropService fileInteropService, FullNameFactory fullNameFactory)
        {
            _fileInteropService = fileInteropService;
            _fullNameFactory = fullNameFactory;
        }

        /// <summary>
        /// Sorts a bunch of given names from the supplied file and writes a sorted list of names out to the destination file.
        /// </summary>
        public async Task<List<FullName>> SortNames(string sourcePath, string destinationPath)
        {
            // Get the contents of the file
            var lines = (await _fileInteropService.ReadFromTextFile(sourcePath)).ToList();

            // Sanity check, even through the file interop service should also protect from this.
            if (lines?.Count < 1)
                throw new Exception($"File ({sourcePath}) contains no elements.");

            // Transform into the type we are working with 
            List<FullName> unsortedNames = new List<FullName>();
            foreach (string line in lines)
            {
                var fullName = _fullNameFactory.CreateNewFullName(line);
                unsortedNames.Add(fullName);
            }

            // Copy the result into a separate list for sorting and sort by the FullName sorting setup.
            List<FullName> sortedNames = unsortedNames.ToList();
            sortedNames.Sort();

            // Write the results back out to the destination file
            await _fileInteropService.WriteTextToFile(destinationPath, sortedNames);

            return sortedNames;
        }
    }
}
