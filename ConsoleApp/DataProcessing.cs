using System;

namespace ConsoleApp
{
    /// <summary>
    /// Main service, process data
    /// </summary>
    public static class DataProcessing
    {
        public static void ProcessData(string importPath, string exportPath)
        {
            IDataReader reader = new FileDataReader(importPath);
            var importedData = reader.ImportData();
            DataPrinter consolePrinter = new DataPrinter(Console.WriteLine);
            consolePrinter.PrintData(importedData);

            using (FileWriter writer = new FileWriter(exportPath))
            {
                DataPrinter filePrinter = new DataPrinter(writer.FileWriteLine);
                filePrinter.PrintData(importedData);
            }   
        }
    }
}
