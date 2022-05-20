using System.ComponentModel;
using System.Configuration;
using System.Runtime.CompilerServices;

namespace FileHasher
{
    public class Preferences : ApplicationSettingsBase, INotifyPropertyChanged
    {
        [UserScopedSetting(), DefaultSettingValue("true")]
        public bool CheckCRC32
        {
            get
            {
                return (bool)this[nameof(CheckCRC32)];
            }
            set
            {
                this[nameof(CheckCRC32)] = value;
                OnPropertyChanged(nameof(CheckCRC32));
            }
        }

        [UserScopedSetting(), DefaultSettingValue("true")]
        public bool CheckMD5
        {
            get
            {
                return (bool)this[nameof(CheckMD5)];
            }
            set
            {
                this[nameof(CheckMD5)] = value;
                OnPropertyChanged(nameof(CheckMD5));
            }
        }

        [UserScopedSetting(), DefaultSettingValue("true")]
        public bool CheckSHA1
        {
            get
            {
                return (bool)this[nameof(CheckSHA1)];
            }
            set
            {
                this[nameof(CheckSHA1)] = value;
                OnPropertyChanged(nameof(CheckSHA1));
            }
        }

        [UserScopedSetting(), DefaultSettingValue("true")]
        public bool CheckSHA256
        {
            get
            {
                return (bool)this[nameof(CheckSHA256)];
            }
            set
            {
                this[nameof(CheckSHA256)] = value;
                OnPropertyChanged(nameof(CheckSHA256));
            }
        }

        [UserScopedSetting(), DefaultSettingValue("true")]
        public bool CheckSHA384
        {
            get
            {
                return (bool)this[nameof(CheckSHA384)];
            }
            set
            {
                this[nameof(CheckSHA384)] = value;
                OnPropertyChanged(nameof(CheckSHA384));
            }
        }

        [UserScopedSetting(), DefaultSettingValue("true")]
        public bool CheckSHA512
        {
            get
            {
                return (bool)this[nameof(CheckSHA512)];
            }
            set
            {
                this[nameof(CheckSHA512)] = value;
                OnPropertyChanged(nameof(CheckSHA512));
            }
        }

        [UserScopedSetting(), DefaultSettingValue("true")]
        public bool IncludeFilePath
        {
            get
            {
                return (bool)this[nameof(IncludeFilePath)];
            }
            set
            {
                this[nameof(IncludeFilePath)] = value;
                OnPropertyChanged(nameof(IncludeFilePath));
            }
        }

        [UserScopedSetting(), DefaultSettingValue("true")]
        public bool IncludeSubFolders
        {
            get
            {
                return (bool)this[nameof(IncludeSubFolders)];
            }
            set
            {
                this[nameof(IncludeSubFolders)] = value;
                OnPropertyChanged(nameof(IncludeSubFolders));
            }
        }

        public bool AllEnabled
        {
            get
            {
                return CheckCRC32 && CheckMD5 && CheckSHA1 && CheckSHA256 && CheckSHA384 && CheckSHA512;
            }
            set
            {
                CheckCRC32 = CheckMD5 = CheckSHA1 = CheckSHA256 = CheckSHA384 = CheckSHA512 = value;
                OnPropertyChanged(nameof(AllEnabled));
            }
        }

        public new event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string Name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(Name));
        }
    }
}