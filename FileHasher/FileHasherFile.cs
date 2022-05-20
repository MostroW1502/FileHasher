using System.IO;

namespace FileHasher
{
    public class FileHasherFile
    {
        private readonly FileInfo fi;
        private readonly CheckSums cs;

        public FileHasherFile(string FileName)
        {
            fi = new FileInfo(FileName);
            cs = new CheckSums(fi);
        }

        public string FileName
        {
            get
            {
                return fi.Name;
            }
        }

        public string FilePath
        {
            get
            {
                return fi.DirectoryName;
            }
        }

        public long FileSize
        {
            get
            {
                return fi.Exists ? fi.Length : 0;
            }
        }

        public CheckSums CheckSums
        {
            get
            {
                return cs;
            }
        }
    }
}