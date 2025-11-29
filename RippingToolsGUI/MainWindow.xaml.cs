using System.Windows;

namespace RippingToolsGUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Main.Content = new ModelFBXPage();
            btnTopModelFBX.IsEnabled = false;
        }

        private void btnTopModelFBX_Click(object sender, RoutedEventArgs e)
        {
            Main.Content = new ModelFBXPage();
            btnTopModelFBX.IsEnabled = false;
            btnTopDDSConv.IsEnabled = true;
            btnTopHKXConv.IsEnabled = true;
            btnTopColConv.IsEnabled = true;
            btnTopSTEXConv.IsEnabled = true;
            /// btnTopBRRES.IsEnabled = true;
        }

        private void btnTopDDSConv_Click(object sender, RoutedEventArgs e)
        {
            Main.Content = new DDSConvPage();
            btnTopModelFBX.IsEnabled = true;
            btnTopDDSConv.IsEnabled = false;
            btnTopHKXConv.IsEnabled = true;
            btnTopColConv.IsEnabled = true;
            btnTopSTEXConv.IsEnabled = true;
            /// btnTopBRRES.IsEnabled = true;
        }

        private void btnTopHKXConv_Click(object sender, RoutedEventArgs e)
        {
            Main.Content = new HKXConvPage();
            btnTopModelFBX.IsEnabled = true;
            btnTopDDSConv.IsEnabled = true;
            btnTopHKXConv.IsEnabled = false;
            btnTopColConv.IsEnabled = true;
            btnTopSTEXConv.IsEnabled = true;
            /// btnTopBRRES.IsEnabled = true;
        }

        private void btnTopColConv_Click(object sender, RoutedEventArgs e)
        {
            Main.Content = new ColConvPage();
            btnTopModelFBX.IsEnabled = true;
            btnTopDDSConv.IsEnabled = true;
            btnTopHKXConv.IsEnabled = true;
            btnTopColConv.IsEnabled = false;
            btnTopSTEXConv.IsEnabled = true;
            /// btnTopBRRES.IsEnabled = true;
        }

        private void btnTopSTEXConv_Click(object sender, RoutedEventArgs e)
        {
            Main.Content = new STEXConvPage();
            btnTopModelFBX.IsEnabled = true;
            btnTopDDSConv.IsEnabled = true;
            btnTopHKXConv.IsEnabled = true;
            btnTopColConv.IsEnabled = true;
            btnTopSTEXConv.IsEnabled = false;
            /// btnTopBRRES.IsEnabled = true;
        }

        /// private void btnTopBRRES_Click(object sender, RoutedEventArgs e)
        /// {
        ///    Main.Content = new BRRESPage();
        ///    btnTopModelFBX.IsEnabled = true;
        ///    btnTopDDSConv.IsEnabled = true;
        ///    btnTopHKXConv.IsEnabled = true;
        ///    btnTopColConv.IsEnabled = true;
        ///    btnTopSTEXConv.IsEnabled = true;
        ///    btnTopBRRES.IsEnabled = false;
        /// }
    }
}
