using Microsoft.Win32;
using System;
using System.Diagnostics;
using System.IO;
using System.Windows;
using System.Windows.Controls;

namespace RippingToolsGUI
{
    /// <summary>
    /// Interaction logic for STEXConvPage.xaml
    /// </summary>
    public partial class STEXConvPage : Page
    {
        public STEXConvPage()
        {
            InitializeComponent();
            btnConvSTEX.IsEnabled = false;
        }

        string StreamingDir;
        string STEXDir;
        private void btnSelectSTEX_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = ("STEX Texture|*.stex");

            if (ofd.ShowDialog()==true)
            {
                btnConvSTEX.IsEnabled = true;

                string fullPath = ofd.FileName;
                STEXDir = fullPath.Substring(0, fullPath.LastIndexOf('\\'));

                foreach (String item in Directory.EnumerateFiles(STEXDir, "*.stex"))
                {
                    listBox.Items.Add(item);
                }
            }
        }

        bool SortSTEX = false;
        private void checkBoxSortSTEX_Click(object sender, RoutedEventArgs e)
        {

            // Defines if .stex are to be sorted by compression type

            if (checkBoxSortSTEX.IsChecked == true)
            {
                SortSTEX = true;
            }
            else
            {
                SortSTEX = false;
            }
        }

        private void btnConvSTEX_Click(object sender, RoutedEventArgs e)
        {

            var sourceDirPath = System.IO.Path.Combine(Environment.CurrentDirectory, "Resources/STEXConv");
            var sourceDirInfo = new DirectoryInfo(sourceDirPath);

            var targetDirPath = STEXDir;
            var targetDirInfo = new DirectoryInfo(targetDirPath);

            CopyFiles(sourceDirInfo, targetDirInfo);

            string batLoc = string.Format($@"{STEXDir}");   //this is where the .bat is 
            Process proc = new Process();
            proc.StartInfo.WorkingDirectory = batLoc;
            if (SortSTEX == true)
            {
                proc.StartInfo.FileName = "STEX2PNG_Sort.bat";
            }
            else
            {
                proc.StartInfo.FileName = "STEX2PNG.bat";
            }
            proc.StartInfo.CreateNoWindow = false;
            proc.Start();

            proc.WaitForExit();

            listBox.Items.Clear();
            System.IO.File.Delete($@"{STEXDir}/libgnuintl-8.dll");
            System.IO.File.Delete($@"{STEXDir}/libpng16.dll");
            System.IO.File.Delete($@"{STEXDir}/lz4.dll");
            System.IO.File.Delete($@"{STEXDir}/rom-properties.dll");
            System.IO.File.Delete($@"{STEXDir}/rpcli.exe");
            System.IO.File.Delete($@"{STEXDir}/stex_converter.exe");
            System.IO.File.Delete($@"{STEXDir}/STEX2PNG.bat");
            System.IO.File.Delete($@"{STEXDir}/STEX2PNG_Sort.bat");
            System.IO.File.Delete($@"{STEXDir}/zlib1.dll");

            btnConvSTEX.IsEnabled = false;

            MessageBox.Show("Converted all *.stex textures to *.png.");
        }

        private void CopyFiles(DirectoryInfo source, DirectoryInfo target)
        {
            Directory.CreateDirectory(target.FullName);

            foreach(var file in source.GetFiles())
            {
                file.CopyTo(System.IO.Path.Combine(target.FullName, file.Name), true);
            }
        }
    }
}
