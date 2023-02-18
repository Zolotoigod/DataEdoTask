namespace ConsoleApp
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;

    /// <summary>
    /// Read data from file
    /// </summary>
    public class FileDataReader : IDataReader
    {
        private readonly string FilePath;
        private ICollection<ImportedObject> ImportedObjects;

        public FileDataReader(string filePath)
        {
            if (filePath is null)
            { 
                throw new ArgumentNullException(nameof(filePath));
            }

            FilePath = filePath;
        }

        /// <inheritdoc/>
        public IReadOnlyCollection<ImportedObject> ImportData()
        {
            var importedLines = new List<string>();
            if (!File.Exists(FilePath))
            {
                throw new InvalidOperationException($"Path {FilePath} not found!");
            }

            using (var streamReader = new StreamReader(FilePath))
            {
                if (!streamReader.EndOfStream)
                {
                    streamReader.ReadLine(); //skip annotation
                }

                while (!streamReader.EndOfStream)
                {
                    var line = streamReader.ReadLine();
                    importedLines.Add(line);
                }
            }

            ImportedObjects = importedLines
                .Select(line => Map(line))
                .Where(model => !string.IsNullOrEmpty(model.Type))
                .Select(model => Normolize(model))
                .ToList();

            // assign number of children
            foreach (var importedObject in ImportedObjects)
            {
                foreach (var impObj in ImportedObjects)
                {
                    if (impObj.ParentType == importedObject.Type & impObj.ParentName == importedObject.Name)
                    {
                        importedObject.NumberOfChildren++;
                    }
                }
            }

            return ImportedObjects.ToArray();
        }

        private ImportedObject Map(string data)
        {
            string[] values = data.Split(';');
            ImportedObject model = new ImportedObject();

            if (values.Length > 0)
            {
                model.Type = values[0];
            }

            if (values.Length > 1)
            {
                model.Name = values[1];
            }

            if (values.Length > 2)
            {
                model.Schema = values[2];
            }

            if (values.Length > 3)
            {
                model.ParentName = values[3];
            }

            if (values.Length > 4)
            {
                model.ParentType = values[4];
            }

            if (values.Length > 5)
            {
                model.DataType = values[5];
            }

            if (values.Length > 6)
            {
                model.IsNullable = values[6];
            }

            return model;
        }

        private ImportedObject Normolize(ImportedObject model)
        {
            model.Type = model.Type.Trim().Replace(" ", "").Replace(Environment.NewLine, "").ToUpper();
            model.Name = model.Name.Trim().Replace(" ", "").Replace(Environment.NewLine, "");
            model.Schema = model.Schema.Trim().Replace(" ", "").Replace(Environment.NewLine, "");
            model.ParentName = model.ParentName.Trim().Replace(" ", "").Replace(Environment.NewLine, "");
            model.ParentType = model.ParentType.Trim().Replace(" ", "").Replace(Environment.NewLine, "").ToUpper();

            return model;
        }
    }
}
