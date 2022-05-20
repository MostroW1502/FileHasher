using System;
using System.Collections.Generic;
using System.IO;

namespace FileHasher.Exporting
{
    public class CSV : Export, IExport
    {
        public CSV(List<FileHasherFile> Files, Preferences p) : base(Files, p)
        {
            ExportType = ExportType.CSV;
        }

        public async void Export(IProgress<ProgressReporter> progress)
        {
            progress.Report(new(true, "", 0, files.Count));

            using StreamWriter f = new($"{outputdir}\\{FileName}", append: true);

            string headers = "File, ";
            if (p.CheckCRC32) { headers += "CRC32, "; }
            if (p.CheckMD5) { headers += "MD5, "; }
            if (p.CheckSHA1) { headers += "SHA1, "; }
            if (p.CheckSHA256) { headers += "SHA256, "; }
            if (p.CheckSHA384) { headers += "SHA384, "; }
            if (p.CheckSHA512) { headers += "SHA512 "; }
            await f.WriteLineAsync(headers);

            int i = 0;
            foreach (FileHasherFile file in files)
            {
                progress.Report(new(true, $"{file.FileName}", ++i, files.Count));

                string line = p.IncludeFilePath ? $"\"{file.FilePath}\\{file.FileName}\", " : $"\"{file.FileName}\", ";
                if (p.CheckCRC32) { line += $"{file.CheckSums.CRC32:X8}, "; }
                if (p.CheckMD5) { line += $"{BytesToString(file.CheckSums.MD5Hash)}, "; }
                if (p.CheckSHA1) { line += $"{BytesToString(file.CheckSums.SHA1Hash)}, "; }
                if (p.CheckSHA256) { line += $"{BytesToString(file.CheckSums.SHA256Hash)}, "; }
                if (p.CheckSHA384) { line += $"{BytesToString(file.CheckSums.SHA384Hash)}, "; }
                if (p.CheckSHA512) { line += $"{BytesToString(file.CheckSums.SHA512Hash)} "; }

                await f.WriteLineAsync(line);
            }

            progress.Report(new(false, "All Done!", i, files.Count));
        }
    }
}