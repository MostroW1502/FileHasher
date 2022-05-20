using System.Collections.Generic;

namespace FileHasher.XMLExport
{
    public class FileHasherXMLExport
    {
        public FileHasherXMLExport()
        {
            Files = new();
        }

        public List<FileHasherExportedFile> Files { get; set; }
    }
}