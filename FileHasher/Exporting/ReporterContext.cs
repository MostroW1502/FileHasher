using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace FileHasher.Exporting
{
    public class ReporterContext : INotifyPropertyChanged
    {
        private bool working;
        private int currentfile,
                    filetotal;
        private string currentfilename;

        public bool Working
        {
            get
            {
                return working;
            }
            set
            {
                working = value;
                OnPropertyChanged(nameof(Working));
            }
        }

        public int CurrentProgress
        {
            get
            {
                return (int)Math.Floor((double)((FileTotal / 100) * currentfile));
            }
        }

        public int CurrentFile
        {
            get
            {
                return currentfile;
            }
            set
            {
                currentfile = value;
                OnPropertyChanged(nameof(CurrentFile));
                OnPropertyChanged(nameof(CurrentProgress));
                OnPropertyChanged(nameof(CurrentFileNumber));
            }
        }

        public int FileTotal
        {
            get
            {
                return filetotal;
            }
            set
            {
                filetotal = value;
                OnPropertyChanged(nameof(FileTotal));
                OnPropertyChanged(nameof(CurrentProgress));
                OnPropertyChanged(nameof(CurrentFileNumber));
            }
        }

        public string CurrentFileNumber
        {
            get
            {
                return $"File: {CurrentFile} of {FileTotal}";
            }
        }

        public string CurrentFileName
        {
            get
            {
                return currentfilename;
            }
            set
            {
                currentfilename = value;
                OnPropertyChanged(nameof(CurrentFileName));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged([CallerMemberName] string Name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(Name));
        }
    }
}