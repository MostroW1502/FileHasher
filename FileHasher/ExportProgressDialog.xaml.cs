using FileHasher.Exporting;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;

namespace FileHasher
{
    /// <summary>
    /// Interaction logic for ExportProgressDialog.xaml
    /// </summary>
    public partial class ExportProgressDialog : Window
    {
        private readonly Progress<ProgressReporter> reporter;
        private readonly ReporterContext rc;

        public ExportProgressDialog(List<FileHasherFile> files, Preferences p, string ExportType)
        {
            InitializeComponent();
            reporter = new();
            reporter.ProgressChanged += reporter_ProgressChanged;
            rc = new();
            DataContext = rc;
            DoExport(files, p, ExportType);
        }

        private void reporter_ProgressChanged(object sender, ProgressReporter e)
        {
            rc.Working = !e.Working;
            rc.CurrentFile = e.CurrentFile;
            rc.FileTotal = e.TotalFiles;
            rc.CurrentFileName = e.FileName;
        }

        private async void DoExport(List<FileHasherFile> files, Preferences p, string ExportType)
        {
            await Task.Run(() =>
            {
                switch (ExportType)
                {
                    default:
                    case "ExportCSV":
                        new CSV(files, p).Export(reporter);
                        break;
                    case "ExportXML":
                        new XML(files, p).Export(reporter);
                        break;
                    case "ExportHTML":
                        new HTML(files, p).Export(reporter);
                        break;
                }
            });
        }

        private void ButtonDone_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}