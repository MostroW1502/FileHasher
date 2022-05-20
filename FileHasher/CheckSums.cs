using Force.Crc32;
using System.ComponentModel;
using System.IO;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Threading;
using System.Threading.Tasks;

namespace FileHasher
{
    public class CheckSums : INotifyPropertyChanged
    {
        private uint crc32;
        private byte[] md5hash;
        private byte[] sha1hash;
        private byte[] sha256hash;
        private byte[] sha384hash;
        private byte[] sha512hash;
        private readonly FileInfo fi;

        public CheckSums(FileInfo FileInfo)
        {
            fi = FileInfo;
        }

        public Task CalculateCheckSums(Preferences Preferences, CancellationToken CancellationToken)
        {
            return Task.Run(() =>
            {
                FileStream fs = File.OpenRead(fi.FullName);

                if (Preferences.CheckCRC32 && !CancellationToken.IsCancellationRequested)
                {
                    fs.Position = 0;
                    byte[] buffer = new byte[0x8000];
                    int bytesread;
                    SHA1 sha1 = SHA1.Create();
                    while ((bytesread = fs.Read(buffer, 0, buffer.Length)) > 0)
                    {
                        CRC32 = Crc32Algorithm.Append(CRC32, buffer, 0, bytesread);
                    }
                }

                if (Preferences.CheckMD5 && !CancellationToken.IsCancellationRequested)
                {
                    fs.Position = 0;
                    MD5Hash = MD5.Create().ComputeHash(fs);
                }

                if (Preferences.CheckSHA1 && !CancellationToken.IsCancellationRequested)
                {
                    fs.Position = 0;
                    SHA1Hash = SHA1.Create().ComputeHash(fs);
                }

                if (Preferences.CheckSHA256 && !CancellationToken.IsCancellationRequested)
                {
                    fs.Position = 0;
                    SHA256Hash = SHA256.Create().ComputeHash(fs);
                }

                if (Preferences.CheckSHA384 && !CancellationToken.IsCancellationRequested)
                {
                    fs.Position = 0;
                    SHA384Hash = SHA384.Create().ComputeHash(fs);
                }

                if (Preferences.CheckSHA512 && !CancellationToken.IsCancellationRequested)
                {
                    fs.Position = 0;
                    SHA512Hash = SHA512.Create().ComputeHash(fs);
                }

                fs.Close();
            }, CancellationToken);
        }

        public void Clear()
        {
            CRC32 = 0;
            MD5Hash = null;
            SHA1Hash = null;
            SHA256Hash = null;
            SHA384Hash = null;
            SHA512Hash = null;
        }

        public uint CRC32
        {
            get
            {
                return crc32;
            }
            private set
            {
                crc32 = value;
                OnPropertyChanged(nameof(CRC32));
            }
        }

        public byte[] MD5Hash
        {
            get
            {
                return md5hash;
            }
            set
            {
                md5hash = value;
                OnPropertyChanged(nameof(MD5Hash));
            }
        }

        public byte[] SHA1Hash
        {
            get
            {
                return sha1hash;
            }
            set
            {
                sha1hash = value;
                OnPropertyChanged(nameof(SHA1Hash));
            }
        }

        public byte[] SHA256Hash
        {
            get
            {
                return sha256hash;
            }
            set
            {
                sha256hash = value;
                OnPropertyChanged(nameof(SHA256Hash));
            }
        }

        public byte[] SHA384Hash
        {
            get
            {
                return sha384hash;
            }
            set
            {
                sha384hash = value;
                OnPropertyChanged(nameof(SHA384Hash));
            }
        }

        public byte[] SHA512Hash
        {
            get
            {
                return sha512hash;
            }
            set
            {
                sha512hash = value;
                OnPropertyChanged(nameof(SHA512Hash));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string Name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(Name));
        }
    }
}