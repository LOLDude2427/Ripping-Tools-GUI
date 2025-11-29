using Microsoft.Win32;
using System;
using System.Diagnostics;
using System.IO;
using System.Windows;
using System.Windows.Controls;

namespace TestWFP
{
    /// <summary>
    /// Interaction logic for ColConvPage.xaml
    /// </summary>
    public partial class ColConvPage : Page
    {
        public ColConvPage()
        {
            InitializeComponent();
            btnConvCol.IsEnabled = false;
        }

        string ColDir = "None";
        private void btnSelectCol_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = ("Collision file|*.phy.hkx");

            if (ofd.ShowDialog() == true)
            {
                btnConvCol.IsEnabled = true;

                string fullPath = ofd.FileName;
                ColDir = fullPath.Substring(0, fullPath.LastIndexOf('\\'));

                foreach (String item in Directory.EnumerateFiles(ColDir, "*.phy.hkx"))
                {
                    listBoxColView.Items.Add(item);
                }
            }
        }

        private void btnConvCol_Click(object sender, RoutedEventArgs e)
        {

            if(ColVersion == "None")
            {
                MessageBox.Show("No version selected.", "Not all parameters filled.");
            }

            else if(ColVersion == "ColGenerations")
            {
                var sourceDirPath = System.IO.Path.Combine(Environment.CurrentDirectory, "Resources/ColConv/Generations");
                var sourceDirInfo = new DirectoryInfo(sourceDirPath);

                var targetDirPath = ColDir;
                var targetDirInfo = new DirectoryInfo(targetDirPath);

                CopyFiles(sourceDirInfo, targetDirInfo);

                string batLoc = string.Format($@"{ColDir}");   //this is where the .bat is 
                Process proc = new Process();
                proc.StartInfo.WorkingDirectory = batLoc;
                proc.StartInfo.FileName = "BatchConvert.bat";
                proc.StartInfo.CreateNoWindow = false;
                proc.Start();

                proc.WaitForExit();

                listBoxColView.Items.Clear();
                System.IO.File.Delete($@"{ColDir}/BatchConvert.bat");
                System.IO.File.Delete($@"{ColDir}/collision2fbx.exe");

                MessageBox.Show("Converted *.phy.hkx to *.fbx.");
            }
            else if(ColVersion == "ColLostWorld")
            {
                var sourceDirPath = System.IO.Path.Combine(Environment.CurrentDirectory, "Resources/ColConv/LostWorld");
                var sourceDirInfo = new DirectoryInfo(sourceDirPath);

                var targetDirPath = ColDir;
                var targetDirInfo = new DirectoryInfo(targetDirPath);

                CopyFiles(sourceDirInfo, targetDirInfo);

                string batLoc = string.Format($@"{ColDir}");   //this is where the .bat is 
                Process proc = new Process();
                proc.StartInfo.WorkingDirectory = batLoc;
                proc.StartInfo.FileName = "BatchConvert.bat";
                proc.StartInfo.CreateNoWindow = false;
                proc.Start();

                proc.WaitForExit();

                listBoxColView.Items.Clear();
                System.IO.File.Delete($@"{ColDir}/BatchConvert.bat");
                System.IO.File.Delete($@"{ColDir}/TagTools.exe");
                System.IO.File.Delete($@"{ColDir}/TagTools.VIR");
                System.IO.File.Delete($@"{ColDir}/TypeDatabase.xml");

                MessageBox.Show("Converted *.phy.hkx to *.fbx.");
            }
        }

        private void CopyFiles(DirectoryInfo source, DirectoryInfo target)
        {
            Directory.CreateDirectory(target.FullName);

            foreach (var file in source.GetFiles())
            {
                file.CopyTo(System.IO.Path.Combine(target.FullName, file.Name), true);
            }
        }

        string ColVersion = "None";
        private void comboBoxColVersion_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (comboBoxColVersion == null) return;
            var combo = (ComboBox)sender;
            var item = (ComboBoxItem)combo.SelectedItem;
            ColVersion = item.Content.ToString();

            if (ColVersion == "System.Windows.Controls.Label: Generations")
            {
                ColVersion = "ColGenerations";
            }
            else if (ColVersion == "System.Windows.Controls.Label: Lost World")
            {
                ColVersion = "ColLostWorld";
            }
        }
    }
}
