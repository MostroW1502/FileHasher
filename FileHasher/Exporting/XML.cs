using FileHasher.XMLExport;
using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;

namespace FileHasher.Exporting
{
    public class XML : Export, IExport
    {
        public XML(List<FileHasherFile> Files, Preferences p) : base(Files, p)
        {
            ExportType = ExportType.XML;
        }

        public void Export(IProgress<ProgressReporter> progress)
        {
            progress.Report(new(true, "Working", 0, files.Count));

            using TextWriter tw = new StreamWriter($"{outputdir}\\{FileName}");
            XmlSerializer xs = new(typeof(FileHasherXMLExport));
            xs.Serialize(tw, Convert());
            tw.Close();

            progress.Report(new(false, "All Done!", 0, files.Count));
        }

        private FileHasherXMLExport Convert()
        {
            FileHasherXMLExport fhex = new();

            foreach (FileHasherFile f in files)
            {
                fhex.Files.Add(new()
                {
                    FileName = f.FileName,
                    Path = p.IncludeFilePath ? f.FilePath : null,
                    CRC32 = f.CheckSums.CRC32,
                    MD5 = f.CheckSums.MD5Hash,
                    SHA1 = f.CheckSums.SHA1Hash,
                    SHA256 = f.CheckSums.SHA256Hash,
                    SHA384 = f.CheckSums.SHA384Hash,
                    SHA512 = f.CheckSums.SHA512Hash
                });
            }

            return fhex;
        }
    }
}