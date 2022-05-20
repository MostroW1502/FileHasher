using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace FileHasher
{
    public class FileHasherData : INotifyPropertyChanged
    {
        private bool working;

        public FileHasherData()
        {
            Files = new ObservableCollection<FileHasherFile>();
        }

        public string PlaceHolder
        {
            get
            {
                return "Enter Hash here.";
            }
        }
        public string Search { get; set; }

        public ObservableCollection<FileHasherFile> Files { get; private set; }

        public bool Working
        {
            get
            {
                return working;
            }
            set
            {
                working = value;
                UpdateUI();
            }
        }

        public bool EnableMenu
        {
            get
            {
                return !Working;
            }
        }

        public bool ButtonGoEnabled
        {
            get
            {
                return FilesTotal > 0;
            }
        }

        public bool ButtonEnabled
        {
            get
            {
                return !Working && FilesTotal > 0;
            }
        }

        public int FilesTotal
        {
            get
            {
                return Files.Count;
            }
        }

        public void UpdateUI()
        {
            OnPropertyChanged(nameof(EnableMenu));
            OnPropertyChanged(nameof(ButtonGoEnabled));
            OnPropertyChanged(nameof(ButtonEnabled));
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string Name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(Name));
        }
    }
}