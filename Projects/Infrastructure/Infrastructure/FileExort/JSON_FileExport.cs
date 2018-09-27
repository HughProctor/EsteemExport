using Newtonsoft.Json;
using System;
using System.IO;

namespace Infrastructure.FileExport
{
    public static class JSON_FileExport
    {
        public const string _fileLocation = @"C:\_EsteemBAMService";
        //public const string _fileLocation = @"..\..\..\..\..\__DataExtracts";
        public const string _filePrefix = "ExtractData_";

        public static void WriteFile(string fileExt, object data, int count)
        {
            WriteFile(fileExt, data, count, null);
        }

        public static void WriteFile(string fileExt, object data, int count, string subFolder = null)
        {
            if (string.IsNullOrEmpty(fileExt) || data == null)
                throw new Exception("JSON_FileExport FileExt and Data object cannot be null or empty");

            var path = Path.Combine(_fileLocation, string.IsNullOrEmpty(subFolder) ? "" : subFolder);
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);
            path = path.EndsWith("\\") ? path : path + "\\";

            using (StreamWriter file = File.CreateText(string.Concat(path, _filePrefix, fileExt, ".json")))
            {
                JsonSerializer serializer = new JsonSerializer();
                serializer.Formatting = Formatting.Indented;
                //serialize object directly into file stream

                var jsonExport = new JsonExport()
                {
                    RecordCount = count,
                    Data = data
                };
                serializer.Serialize(file, jsonExport);
            }
        }

        public static void WriteFile(string fileExt, object data, string subFolder = null)
        {
            if (string.IsNullOrEmpty(fileExt) || data == null)
                throw new Exception("JSON_FileExport FileExt and Data object cannot be null or empty");

            var path = Path.Combine(_fileLocation, string.IsNullOrEmpty(subFolder) ? "" : subFolder);
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);
            path = path.EndsWith("\\") ? path : path + "\\";

            using (StreamWriter file = File.CreateText(string.Concat(path, _filePrefix, fileExt, ".json")))
            {
                JsonSerializer serializer = new JsonSerializer();
                serializer.Formatting = Formatting.Indented;
                //serialize object directly into file stream

                var jsonExport = new JsonExport_StartEndDate()
                {
                    Data = data
                };
                serializer.Serialize(file, jsonExport);
            }
        }

        public static string ReadFile(string fileName, string filePath = null)
        {
            if (string.IsNullOrEmpty(fileName) || filePath == null)
                throw new Exception("JSON_FileExport FileExt and Folder cannot be null");

            var returnValue = "";
            filePath = Path.Combine(_fileLocation, "Service", filePath);
            var path = Path.Combine(filePath, fileName);

            if (!Directory.Exists(filePath) && !File.Exists(path))
            {
                //string applicationDirectory = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase);
                //path = Path.Combine(applicationDirectory, path);
                if (!File.Exists(path))
                    throw new Exception("JSON_FileExport FileExt and Folder cannot be null " + path);
            }

            using (StreamReader r = new StreamReader(path))
            {
                returnValue = r.ReadToEnd();
            }
            return returnValue;
        }

        public static void WriteFile(string fileExt, object data, int count, string subFolder = null, DateTime? startDate = null, DateTime? endDate = null)
        {
            if (string.IsNullOrEmpty(fileExt) || data == null)
                throw new Exception("JSON_FileExport FileExt and Data object cannot be null or empty");

            var path = Path.Combine(_fileLocation, string.IsNullOrEmpty(subFolder) ? "" : subFolder);
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);
            path = path.EndsWith("\\") ? path : path + "\\";

            // Skip Creating a file if no records
            if (count <= 0) return;

            using (StreamWriter file = File.CreateText(string.Concat(path, _filePrefix, fileExt, ".json")))
            {
                JsonSerializer serializer = new JsonSerializer();
                serializer.Formatting = Formatting.Indented;
                //serialize object directly into file stream

                var jsonExport = new JsonExport_StartEndDate()
                {
                    RecordCount = count,
                    Data = data,
                    StartDate = startDate,
                    EndDate = endDate
                };
                serializer.Serialize(file, jsonExport);
            }
        }
    }

    public class JsonExport
    {
        public int RecordCount { get; set; }
        public object Data { get; set; }
    }

    public class JsonExport_StartEndDate : JsonExport
    {
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
    }

}
