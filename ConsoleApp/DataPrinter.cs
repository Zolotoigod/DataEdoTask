using System;
using System.Collections.Generic;

namespace ConsoleApp
{
    /// <summary>
    /// Print data to the source according the sheme
    /// </summary>
    public class DataPrinter
    {
        private Action<string> Write;

        public DataPrinter(Action<string> write)
        {
            Write = write;
        }

        /// <summary>
        /// Print data
        /// </summary>
        /// <param name="data">Data for print</param>
        public void PrintData(IReadOnlyCollection<ImportedObject> data)
        {
            foreach (var database in data)
            {
                if (database.Type == "DATABASE")
                {
                    Write($"Database '{database.Name}' ({database.NumberOfChildren} tables)");

                    // print all database's tables
                    foreach (var table in data)
                    {
                        if (table.ParentType.ToUpper() == database.Type & table.ParentName == database.Name)
                        {
                            Write($"\tTable '{table.Schema}.{table.Name}' ({table.NumberOfChildren} columns)");

                            // print all table's columns
                            foreach (var column in data)
                            {
                                if (column.ParentType.ToUpper() == table.Type & column.ParentName == table.Name)
                                {
                                    Write($"\t\tColumn '{column.Name}' with {column.DataType} data type {(column.IsNullable.Equals("1") ? "accepts nulls" : "with no nulls")}");
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}
