using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace FileHasher.Exporting
{
    public class Export
    {
        protected readonly List<FileHasherFile> files;
        protected readonly Preferences p;
        protected static string outputdir;

        public Export(List<FileHasherFile> Files, Preferences p)
        {
            files = Files;
            this.p = p;
            CreateOutputFolder();
        }

        public static string OutputFolder
        {
            get
            {
                CreateOutputFolder();
                return outputdir;
            }
        }

        private static void CreateOutputFolder()
        {
            outputdir = $"{Directory.GetCurrentDirectory()}\\Export";
            if (!Directory.Exists(outputdir)) { _ = Directory.CreateDirectory(outputdir); }
        }

        public string FileName
        {
            get
            {
                string extension;
                switch (ExportType)
                {
                    default:
                    case ExportType.CSV:
                        extension = "csv";
                        break;
                    case ExportType.XML:
                        extension = "xml";
                        break;
                    case ExportType.HTML:
                        extension = "html";
                        break;
                }
                return $"{Path.GetFileNameWithoutExtension(Assembly.GetEntryAssembly().Location)} {DateTime.Now.ToShortDateString().Replace('/', '-').Replace('\\', '-')}.{extension}";
            }
        }

        protected ExportType ExportType { get; set; } = ExportType.CSV;

        protected static string BytesToString(byte[] value)
        {
            string s = string.Empty;
            foreach (byte b in value)
            {
                s += $"{b:X2}";
            }
            return s;
        }
    }
}