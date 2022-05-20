using FileHasher.Exporting;
using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace FileHasher
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        class ContextData
        {
            public ContextData()
            {
                Preferences = new();
                FileHasherData = new();
            }

            public Preferences Preferences { get; private set; }
            public FileHasherData FileHasherData { get; private set; }
        }

        private readonly ContextData cd;
        private CancellationTokenSource cts;

        public MainWindow()
        {
            InitializeComponent();
            cd = new();
            PreferenceMenu.DataContext = cd;
            DataContext = cd;
            FileGrid.ItemsSource = cd.FileHasherData.Files;
            CheckColumns();
        }

        private void ColPrefClicked(object sender, RoutedEventArgs e)
        {
            CheckColumns();
            SaveConfiguration();
        }

        private void CheckColumns()
        {
            int index = 0;
            foreach (DataGridColumn dgc in FileGrid.Columns)
            {
                switch (dgc.Header)
                {
                    case "FilePath":
                        FileGrid.Columns[index].Visibility = cd.Preferences.IncludeFilePath ? Visibility.Visible : Visibility.Collapsed;
                        break;
                    case "CRC32":
                        FileGrid.Columns[index].Visibility = cd.Preferences.CheckCRC32 ? Visibility.Visible : Visibility.Collapsed;
                        break;
                    case "MD5":
                        FileGrid.Columns[index].Visibility = cd.Preferences.CheckMD5 ? Visibility.Visible : Visibility.Collapsed;
                        break;
                    case "SHA1":
                        FileGrid.Columns[index].Visibility = cd.Preferences.CheckSHA1 ? Visibility.Visible : Visibility.Collapsed;
                        break;
                    case "SHA256":
                        FileGrid.Columns[index].Visibility = cd.Preferences.CheckSHA256 ? Visibility.Visible : Visibility.Collapsed;
                        break;
                    case "SHA384":
                        FileGrid.Columns[index].Visibility = cd.Preferences.CheckSHA384 ? Visibility.Visible : Visibility.Collapsed;
                        break;
                    case "SHA512":
                        FileGrid.Columns[index].Visibility = cd.Preferences.CheckSHA512 ? Visibility.Visible : Visibility.Collapsed;
                        break;
                }
                index++;
            }
            mnuEnableAll.IsEnabled = !cd.Preferences.AllEnabled;
            mnuDisableAll.IsEnabled = cd.Preferences.AllEnabled;
        }

        private void SaveConfiguration()
        {
            cd.Preferences.Save();
        }

        private void EnableAllColumns(object sender, RoutedEventArgs e)
        {
            cd.Preferences.AllEnabled = true;
            CheckColumns();
        }

        private void DisableAllColumns(object sender, RoutedEventArgs e)
        {
            cd.Preferences.AllEnabled = false;
            CheckColumns();
        }

        private void FileGrid_Drop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                AddFilesToGrid((string[])e.Data.GetData(DataFormats.FileDrop));
                cd.FileHasherData.UpdateUI();
            }
        }

        private void AddFilesToGrid(string[] Files)
        {
            foreach (string s in Files)
            {
                FileInfo fi = new(s);
                if (fi.Attributes.HasFlag(FileAttributes.Directory)) { AddFilesToGrid(EnumerateDirectory(s)); }
                else { cd.FileHasherData.Files.Add(new FileHasherFile(s)); }
            }
        }

        private static string[] EnumerateDirectory(string Path)
        {
            return (from file in new DirectoryInfo(Path).EnumerateFiles("*", SearchOption.AllDirectories)
                    where file.Exists
                    select file.FullName).ToArray();
        }

        private async void ButtonGo_Click(object sender, RoutedEventArgs e)
        {
            if (!cd.FileHasherData.Working)
            {
                cd.FileHasherData.Working = true;
                ButtonGo.Content = "_Cancel";

                cts = new CancellationTokenSource();

                try
                {
                    foreach (FileHasherFile fhl in cd.FileHasherData.Files)
                    {
                        await fhl.CheckSums.CalculateCheckSums(cd.Preferences, cts.Token);
                    }
                }
                catch(Exception ex)
                {
                    if (ex is TaskCanceledException or OperationCanceledException)
                    {
                        if (MessageBox.Show(this, "Operation has been canceled!", "Canceled", MessageBoxButton.OK) == MessageBoxResult.OK)
                        {
                            foreach (FileHasherFile fhl in cd.FileHasherData.Files)
                            {
                                fhl.CheckSums.Clear();
                            }
                        }
                    }
                }
                finally
                {
                    cts.Dispose();
                }

                ButtonGo.Content = "_Go";
                cd.FileHasherData.Working = false;
            }
            else
            {
                cts.Cancel();
                ButtonGo.Content = "_Go";
                cd.FileHasherData.Working = false;
            }
        }

        private void ButtonClear_Click(object sender, RoutedEventArgs e)
        {
            cd.FileHasherData.Files.Clear();
            cd.FileHasherData.UpdateUI();
        }

        private void ButtonExport_Click(object sender, RoutedEventArgs e)
        {
            _ = new ExportDialogWindow()
            {
                Owner = this,
                Preferences = cd.Preferences,
                Files = cd.FileHasherData.Files.ToList()
            }.ShowDialog();
        }

        private void ButtonExportFolder_Click(object sender, RoutedEventArgs e)
        {
            _ = Process.Start("explorer.exe", Export.OutputFolder);
        }

        private void AppExit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}