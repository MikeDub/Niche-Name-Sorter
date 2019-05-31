using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using NicheNameSorter.Exceptions;
using NicheNameSorter.Factory;
using NicheNameSorter.Services;

namespace NicheNameSorter
{
    class Program
    {
        private static string _newline = Environment.NewLine;

        /// <summary>
        /// Starts the application
        /// </summary>
        /// <param name="args">The argument (source file name) to be passed in.</param>
        static async Task Main(string[] args)
        {
            var fileIoService = new FileInteropService();
            FullNameFactory nameFactory = new FullNameFactory();
            var service = new NameSortingService(fileIoService, nameFactory);

            try
            {
                // Get the file path to read from
                var filePath = args.FirstOrDefault();

                if (Debugger.IsAttached) // Override file path when executing in VS
                    filePath = "./unsorted-names-list.txt";

                if (filePath == null)
                    throw new ArgumentNullException(nameof(filePath), "Must specify the source file path.");

                Console.WriteLine($"Beginning name sorting, retrieving names from: {filePath}.{_newline}");

                // Infer destination file name as per spec, this allows for change of directory, 
                // -- However can be brittle for long file paths (ie. directory that contains unsorted).
                var destinationPath = filePath.Replace("unsorted", "sorted");
                var start = DateTimeOffset.Now;

                var sortedNames = await service.SortNames(filePath, destinationPath);
                // Display the result.
                sortedNames.ForEach(
                    name =>
                    {
                        var index = sortedNames.IndexOf(name);
                        Console.WriteLine($"{index}. {name.ToText()}");
                    });

                var elapsedTime = DateTimeOffset.Now.Subtract(start);
                Console.WriteLine($"{_newline}Sorting complete, results available at {destinationPath}. Took {elapsedTime.TotalSeconds} seconds.");
            }
            catch (ArgumentNullException argNullEx)
            {
                Console.WriteLine("Please make sure you've supplied all the arguments and try again.");
                Console.WriteLine(argNullEx);
            }
            catch (InvalidNameException badNameEx)
            {
                Console.WriteLine("It appears you've provided an invalid name. Lets try that again with proper input shall we?");
                Console.WriteLine($"Detail: {badNameEx.Message}");
            }
            catch (Exception e)
            {
                Console.WriteLine("An unexpected error occurred, check below for details.");
                Console.WriteLine(e);
            }
        }
    }
}
