using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using CameraControll;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CamerTesterClient
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            if (uEye_Handler.InitializeCamera().Status != "Initialized")
            {
                MessageBox.Show(uEye_Handler.CameraResult.ErrorDescription + "\n\n ErrorCode: " + uEye_Handler.CameraResult.ErrorCode, "Camera Connection Error!");
            }
            if (uEye_Handler.CameraResult.Status == "Initialized")
            {
                uEye_Handler.StartLiveView(this.LiveImage);
            }
        }

        private void LiveView_Loaded(object sender, RoutedEventArgs e)
        {
        }
    }
}
