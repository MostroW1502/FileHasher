namespace FileHasher.Exporting
{
    public class ProgressReporter
    {
        public ProgressReporter(bool Working, string FileName, int CurrentFile, int TotalFiles)
        {
            this.Working = Working;
            this.FileName = FileName;
            this.CurrentFile = CurrentFile;
            this.TotalFiles = TotalFiles;
        }

        public bool Working { get; private set; }

        public string FileName { get; private set; }

        public int CurrentFile { get; private set; }

        public int TotalFiles { get; private set; }
    }
}