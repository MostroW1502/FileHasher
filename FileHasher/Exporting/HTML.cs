using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace FileHasher.Exporting
{
    public class HTML : Export, IExport
    {
		public HTML(List<FileHasherFile> Files, Preferences p) : base(Files, p)
		{
			ExportType = ExportType.HTML;
		}

		public async void Export(IProgress<ProgressReporter> progress)
		{
			progress.Report(new(true, "", 0, files.Count));

			string htmlFile = GetEmbeddedResource("FileHasher.Resources.HTMLTemplate.html");

			string exportedFileName = $"{outputdir}\\{FileName}";
			htmlFile = htmlFile.Replace("{{ExportedFileName}}", exportedFileName);

			string columns = "<th>File</th>\r\n";
			if (p.IncludeFilePath) columns += "<th>Path</th>\r\n";
			if (p.CheckCRC32) columns += "<th>CRC32</th>\r\n";
			if (p.CheckMD5) columns += "<th>MD5</th>\r\n";
			if (p.CheckSHA1) columns += "<th>SHA1</th>\r\n";
			if (p.CheckSHA256) columns += "<th>SHA256</th>\r\n";
			if (p.CheckSHA384) columns += "<th>SHA384</th>\r\n";
			if (p.CheckSHA512) columns += "<th>SHA512</th>\r\n";

			htmlFile = htmlFile.Replace("{{ExportedColumns}}", columns);

			using StreamWriter f = new(exportedFileName, append: true);

			string rows = "";

			int i = 0;
			foreach (FileHasherFile file in files)
			{
				progress.Report(new(true, $"{file.FileName}", ++i, files.Count));

				string row = "<tr>\r\n";
				row += $"<td>{file.FileName}</td>\r\n";
				if (p.IncludeFilePath) row += $"<td>{file.FilePath}</td>\r\n";
				if (p.CheckCRC32) row += $"<td>{file.CheckSums.CRC32:X8}</td>\r\n";
				if (p.CheckMD5) row += $"<td>{BytesToString(file.CheckSums.MD5Hash)}</td>\r\n";
				if (p.CheckSHA1) row += $"<td>{BytesToString(file.CheckSums.SHA1Hash)}</td>\r\n";
				if (p.CheckSHA256) row += $"<td>{BytesToString(file.CheckSums.SHA256Hash)}</td>\r\n";
				if (p.CheckSHA384) row += $"<td>{BytesToString(file.CheckSums.SHA384Hash)}</td>\r\n";
				if (p.CheckSHA512) row += $"<td>{BytesToString(file.CheckSums.SHA512Hash)}</td>\r\n";
				row += "</tr>\r\n";

				rows += row;
			}

			htmlFile = htmlFile.Replace("{{ExportedRows}}", rows);

			await f.WriteLineAsync(htmlFile);

			progress.Report(new(false, "All Done!", i, files.Count));
        }

		private static string GetEmbeddedResource(string ResourceName)
        {
            using StreamReader sr = new(Assembly.GetExecutingAssembly().GetManifestResourceStream(ResourceName));
            return sr.ReadToEnd();
        }
    }
}