using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using CameraControll;
using System.Windows.Forms;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Forms.Integration;
using System.ComponentModel;

namespace HM_Interface_Visu.Assets.ManualScreenElements
{
    /// <summary>
    /// Interaction logic for AxisScreen.xaml
    /// </summary>
    public partial class AxisScreen : System.Windows.Controls.UserControl
    {        
        public AxisScreen()
        {
            if (System.ComponentModel.DesignerProperties.GetIsInDesignMode(this))
            {
                return;
            }
            InitializeComponent();
            Name = "Axis";
            CameraControll.CameraOn.Click += new RoutedEventHandler(CameraCotnrollCameraOn_Click);
        }
        public void CameraCotnrollCameraOn_Click(object sender, RoutedEventArgs e)
        {
            if (uEye_Handler.CameraResult.Status == "Initialized" || uEye_Handler.CameraResult.Status == "Sleep")
            {
                CameraControll.CameraOn.Foreground = (FindResource("PrimaryHueLightBrush") as SolidColorBrush);
                uEye_Handler.StartLiveView(LiveImage);
            }
            else if (uEye_Handler.CameraResult.Status == "Live")
            {
                CameraControll.CameraOn.Foreground = (FindResource("AccentColorBrush") as SolidColorBrush);
                uEye_Handler.StopLiveView();
                LiveImage.Source = null;
            }
        }
        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {

        }
        private void UserControl_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {

        }
    }
}
