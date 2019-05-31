using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using NicheNameSorter.Models;

namespace NicheNameSorter.Services
{
    /// <summary>
    /// Service that handles reading and writing to files.
    /// In a larger system, this would be split out between a query and state modification services.
    /// </summary>
    public class FileInteropService
    {
        /// <summary>
        /// Reads a text file at a specified path and returns the data as string collection for each line.
        /// </summary>
        public virtual async Task<IEnumerable<string>> ReadFromTextFile(string filePath)
        {
            List<string> contents = new List<string>();

            EnsureFilePathIsValid(filePath);

            if (!File.Exists(filePath))
                throw new FileNotFoundException($"Invalid file path specified: {filePath}");

            using (var file = File.OpenText(filePath))
            {
                while (!file.EndOfStream)
                {
                    string line = await file.ReadLineAsync();
                    if (!string.IsNullOrEmpty(line))
                        contents.Add(line);
                }
            }

            return contents;
        }

        /// <summary>
        /// Writes a collection of generic text lines to a text file at a specified path.
        /// </summary>
        public virtual async Task WriteTextToFile(string filePath, IEnumerable<IDisplayText> source)
        {
            EnsureFilePathIsValid(filePath);

            var data = source.ToList(); // Will by design throw an argument null exception.
            if (data?.FirstOrDefault() == null)
                throw new ArgumentNullException(nameof(source), "Data source cannot be null or empty.");

            // This uses the classes text representation interface to transform into a text friendly string.
            var contents = data.Select(line => line.ToText()); 
            await File.WriteAllLinesAsync(filePath, contents);
        }
        
        /// <summary>
        /// Sanity Check to ensure the path is not null or empty which prevents both reading and writing.
        /// </summary>
        private void EnsureFilePathIsValid(string filePath)
        {
            if (string.IsNullOrEmpty(filePath))
                throw new ArgumentNullException(nameof(filePath), "FilePath cannot be null or empty.");
        }
    }
}
