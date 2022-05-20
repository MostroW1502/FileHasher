using System;

namespace FileHasher.Exporting
{
    public interface IExport
    {
        public void Export(IProgress<ProgressReporter> progress);
    }
}