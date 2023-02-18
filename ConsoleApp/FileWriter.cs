using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp
{
    /// <summary>
    /// Write data to file.
    /// </summary>
    public class FileWriter : IDisposable
    {
        private readonly FileStream Stream;
        private readonly StreamWriter Writer;

        public FileWriter(string filePath)
        {
            Stream = new FileStream(filePath, FileMode.OpenOrCreate, FileAccess.Write);
            Writer = new StreamWriter(Stream, Encoding.UTF8);
        }

        public void Dispose()
        {
            Writer.Dispose();
            Stream.Dispose();
        }

        public void FileWriteLine(string stringToWrite)
        {
            Writer.WriteLine(stringToWrite);
        }
    }
}
