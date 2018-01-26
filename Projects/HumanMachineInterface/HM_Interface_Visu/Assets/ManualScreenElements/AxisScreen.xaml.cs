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
        private PictureBox CameraLiveImage = null;
        private BackgroundWorker DisplayWorker = new BackgroundWorker();
        private System.Windows.Forms.ProgressBar progressBar = new System.Windows.Forms.ProgressBar();
        private WindowsFormsHost host = null;
        public AxisScreen()
        {
            InitializeComponent();
            Name = "Axis";
            HostImage();
        }
        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            
        }
        private void UserControl_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {

        }
        private void CamerImage_MouseClick(object sender, EventArgs e)
        {
            if (CameraControll.CameraControll.Status == "sleeped") CameraControll.CameraControll.Sleep(false);            
            else if (CameraControll.CameraControll.Status == "started") CameraControll.CameraControll.Sleep(true);
        }

        private void HostImage()
        {
            DisplayWorker.DoWork += DisplayWorker_DoWork;
            DisplayWorker.ProgressChanged += DisplayWorker_ProgressChanged;
            DisplayWorker.RunWorkerCompleted += DisplayWorker_RunWorkerCompleted;
            DisplayWorker.WorkerReportsProgress = true;
            DisplayWorker.WorkerSupportsCancellation = true;
            DisplayWorker.RunWorkerAsync();
        }
        private  void DisplayWorker_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            System.Windows.Application.Current.Dispatcher.Invoke
                (new Action(() =>
                {
                    if (host == null && CameraLiveImage == null)
                    {
                        CameraLiveImage = CameraControll.CameraControll.CameraImage;
                        this.host = new WindowsFormsHost();
                        this.host.Child = CameraLiveImage;
                        LiveView.Children.Add(host);
                        CameraControll.CameraControll.CameraImage.Click += new EventHandler(CamerImage_MouseClick);
                    }                                     
                }));
        }
        public  void DisplayWorker_ProgressChanged(object sender, System.ComponentModel.ProgressChangedEventArgs e)
        {

        }
        private  void DisplayWorker_RunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
        {

        }
    }
}
