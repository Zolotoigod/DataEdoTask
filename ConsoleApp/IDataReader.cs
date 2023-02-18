using System.Collections.Generic;

namespace ConsoleApp
{
    public interface IDataReader
    {
        /// <summary>
        /// Read data from source
        /// </summary>
        /// <exception cref="InvalidOperationException">Throw if source does not exist</exception>
        IReadOnlyCollection<ImportedObject> ImportData();
    }
}
