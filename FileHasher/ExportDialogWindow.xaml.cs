using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace FileHasher
{
    /// <summary>
    /// Interaction logic for ExportDialogWindow.xaml
    /// </summary>
    public partial class ExportDialogWindow : Window
    {
        public ExportDialogWindow()
        {
            InitializeComponent();
        }

        public Preferences Preferences { get; set; }

        public List<FileHasherFile> Files { get; set; }

        private void Radio_Checked(object sender, RoutedEventArgs e)
        {
            ExportType = ((RadioButton)sender).Name;
        }

        private void ButtonExport_Click(object sender, RoutedEventArgs e)
        {
            _ = new ExportProgressDialog(Files, Preferences, ExportType)
            {
                Owner = this
            }.ShowDialog();
            Close();
        }

        private string ExportType { get; set; }
    }
}