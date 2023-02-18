namespace ConsoleApp
{
    using System;

    internal class Program
    {
        static void Main(string[] args)
        {
            // cant handle the command line arguments
            string importPath = "data.csv";
            string exportPath = "convertedData.txt";
            if (args.Length > 0)
            {
                importPath = args[0];
            }

            if (args.Length > 1)
            {
                exportPath = args[1];
            }

            DataProcessing.ProcessData(importPath, exportPath);

            Console.ReadLine();
        }
    }
}
