using System;
using System.Windows;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.ComponentModel;
using System.Threading;

namespace CameraControll
{
    public static class CameraControll
    {
        private static uEye.Defines.Status statusRet = 0;
        private static uEye.Camera Camera;
        private static PictureBox Displayer = new PictureBox();
        private static string CameraStatus = "stoped";
        private static IntPtr displayHandle;
        private static BackgroundWorker CamWorker = new BackgroundWorker();
        private static ProgressBar ProgressStatus;
        public static void StartLiveView(int Width, int Height, ProgressBar progressBar)
        {
            if(progressBar!=null) ProgressStatus = progressBar;
            displayHandle = Displayer.Handle;
            Displayer.Size = new System.Drawing.Size(800, 600);
            InitCamera();
        }
        public static void StopLiveView()
        {
            Camera.EventFrame -= onFrameEvent;
            Camera.Exit();
            CameraStatus = "stoped";
        }
        public static void Sleep(bool status)
        {
            if (status)
            {
                CameraStatus = "sleeped";
                Camera.EventFrame -= onFrameEvent;
                Camera.Acquisition.Stop();
            }
            else
            {
                CameraStatus = "started";
                Camera.Acquisition.Capture();
                Camera.EventFrame += onFrameEvent;                
            }
        }
        public static string Status
        {
            get
            {
                return CameraStatus;
            }
        }
        public static PictureBox CameraImage
        {
            get
            {
                return Displayer;
            }
            set
            {
                if (value != null)
                {
                    Displayer = value;
                }
            }
        }
        public static void InitCamera()
        {            
            CamWorker.DoWork += CamWorker_DoWork;
            CamWorker.ProgressChanged += CamWorker_ProgressChanged;
            CamWorker.RunWorkerCompleted += CamWorker_RunWorkerCompleted;
            CamWorker.WorkerReportsProgress = true;
            CamWorker.WorkerSupportsCancellation = true;
            CamWorker.RunWorkerAsync();
        }
        private static void onFrameEvent(object sender, EventArgs e)
        {
            uEye.Camera CameraMaster = sender as uEye.Camera;
            uEye.Types.ImageInfo ImageInfoMaster;

            Int32 s32MemIDMaster;
            CameraMaster.Memory.GetActive(out s32MemIDMaster);
            CameraMaster.Information.GetImageInfo(s32MemIDMaster, out ImageInfoMaster);
            CameraMaster.Display.Render(s32MemIDMaster, displayHandle, uEye.Defines.DisplayRenderMode.FitToWindow);
        }

        private static void CamWorker_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            Camera = new uEye.Camera();
            CamWorker.ReportProgress(10);
            statusRet = Camera.Init(1, displayHandle);
            CamWorker.ReportProgress(20);
            if (statusRet != uEye.Defines.Status.Success)
            {
                CamWorker.CancelAsync();
                MessageBox.Show("Initialization of camera (ID = 1) failed");
            }
            CamWorker.ReportProgress(30);
            statusRet = Camera.IO.Pwm.SetMode(0);
            CamWorker.ReportProgress(40);
            statusRet = Camera.Memory.Allocate();
            CamWorker.ReportProgress(50);
            if (statusRet != uEye.Defines.Status.Success)
            {
                CamWorker.CancelAsync();
                MessageBox.Show("Allocate Memory failed");
            }
            CamWorker.ReportProgress(60);
            statusRet = Camera.Acquisition.Capture();
            CamWorker.ReportProgress(80);
            if (statusRet != uEye.Defines.Status.Success)
            {
                CamWorker.CancelAsync();
                MessageBox.Show("Start Live Video failed");
            }
            CamWorker.ReportProgress(90);
            Camera.EventFrame += onFrameEvent;
            CamWorker.ReportProgress(100);
        }
        public static void CamWorker_ProgressChanged(object sender, System.ComponentModel.ProgressChangedEventArgs e)
        {
            if(ProgressStatus != null) ProgressStatus.Value = e.ProgressPercentage;
        }

        private static void CamWorker_RunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
        {
            CameraStatus = "started";
        }
    }
}
