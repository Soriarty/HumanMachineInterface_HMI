using CameraControll;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace HM_Interface_Visu.Assets.ManualScreenElements.AxisScreenElements
{
    /// <summary>
    /// Interaction logic for CameraControll.xaml
    /// </summary>
    public partial class CameraControll : UserControl
    {
        private bool OffsetIsOn = true;
        private DateTime click_Started;
        System.Windows.Threading.DispatcherTimer dispatcherTimer = new System.Windows.Threading.DispatcherTimer();
        System.Windows.Threading.DispatcherTimer RefreshTimer = new System.Windows.Threading.DispatcherTimer();

        public CameraControll()
        {
            InitializeComponent();  
            
            dispatcherTimer.Tick += new EventHandler(dispatcherTimer_Tick);
            dispatcherTimer.Interval = new TimeSpan(0,0,0,0, 100);

            RefreshTimer.Tick += new EventHandler(RefreshTimer_Tick);
            RefreshTimer.Interval = new TimeSpan(0, 0, 0, 0, 500);
            RefreshTimer.Start();
        }

        private void RefreshTimer_Tick(object sender, EventArgs e)
        {
            if (uEye_Handler.CameraResult.Status == "Initialized" || uEye_Handler.CameraResult.Status == "Sleep")
            {
                CameraOn.Foreground = (FindResource("PrimaryHueLightBrush") as SolidColorBrush);
            }
            else if (uEye_Handler.CameraResult.Status == "Live")
            {
                CameraOn.Foreground = (FindResource("AccentColorBrush") as SolidColorBrush);

            }
        }

        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            Save.Content = Math.Round((DateTime.Now - click_Started).TotalSeconds, 0).ToString();
            if (Save.Content as string == "10")
            {
                dispatcherTimer.Stop();
                Save.Content = "Offset Save";
                Save.Foreground = FindResource("AccentColorBrush") as SolidColorBrush;
            }
        }

        private void Save_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Save.Foreground = (FindResource("PrimaryHueLightBrush") as SolidColorBrush);
            click_Started = DateTime.Now;
            dispatcherTimer.Start();
        }

        private void Save_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if ((DateTime.Now - click_Started).TotalSeconds > 10)
            {
                dispatcherTimer.Stop();
                Save.Content = "Offset Save";
                Save.Foreground = FindResource("AccentColorBrush") as SolidColorBrush;
            }
            else
            {
                dispatcherTimer.Stop();
                Save.Content = "Offset Save";                
            }
        }

        public void CameraOn_Click(object sender, RoutedEventArgs e)
        {
        }

        private void OffSet_Click(object sender, RoutedEventArgs e)
        {
            if (OffsetIsOn)
            {
                OffSet.Foreground = FindResource("AccentColorBrush") as SolidColorBrush;
                DisplayPanel.IsEnabled = true;
                OffsetIsOn = false;
            }
            else
            {
                OffSet.Foreground = FindResource("PrimaryHueLightBrush") as SolidColorBrush;
                OffsetIsOn = true;
            }
        }
    }
}
