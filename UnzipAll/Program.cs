using System;
using System.IO;
using System.IO.Compression;

namespace UnzipAll
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("What is the directory with the zip files?");
            var sourceDir = Console.ReadLine();
            Console.WriteLine("What sub directory should I place them in?");
            var targetDir = Console.ReadLine();
            Console.WriteLine("Do you want me to create a sub directory for each zip file? [Y/n]");
            var response = Console.ReadKey();
            var makeSubDirs = true;
            if (response.KeyChar == 'n' || response.KeyChar == 'N')
            {
                makeSubDirs = false;
            }

            Console.WriteLine("Should I be chatty? [Y/n]");
            response = Console.ReadKey();
            var verbose = true;
            if (response.KeyChar == 'n'|| response.KeyChar == 'N')
            {
                verbose = false;
            }
            Console.WriteLine("Here we go!");

            if (!Directory.Exists(sourceDir))
            {
                Console.WriteLine("Hey, that directory doesn't exist. Check for typos or something. Press any key to exit.");
                Console.ReadKey();
                return;
            }

            var zips = Directory.GetFiles(sourceDir, "*.zip");
            var zipCount = zips.Length + 1;
            Console.WriteLine($"Yo, we found {zipCount} files. Let's do it.");
            foreach (var zipFile in zips) 
            {
                var file = new FileInfo(zipFile);
                if (verbose)
                {
                    Console.WriteLine($"We're on file {file.Name}.");
                }

                var unzipPath = targetDir;
                if (makeSubDirs)
                {
                    var di = new DirectoryInfo(targetDir);
                    var subDi = di.CreateSubdirectory(file.Name[0..^4]);
                    unzipPath = subDi.FullName;
                }

                ZipFile.ExtractToDirectory(file.FullName, unzipPath);
            }

        }
    }
}
