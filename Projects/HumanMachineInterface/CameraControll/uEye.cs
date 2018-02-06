using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Controls;
using System.Windows.Threading;
using System.Drawing;
using System.Windows;
using System.Drawing.Imaging;

namespace CameraControll
{
    public static class uEye_Handler
    {
        #region Public field
        public static System.Windows.Controls.Image OutputImage = new System.Windows.Controls.Image();
        public struct CameraResults
        {
            public string ErrorCode;
            public string Status;
            public string ErrorDescription;
        }
        public static CameraResults CameraResult
        {
            get
            {
                return CameraStatus;
            }
        }
        public static System.Windows.Controls.Image LiveImage
        {
            get
            {
                return OutputImage;
            }
            set
            {
                if (value != null)
                {
                    OutputImage = value;
                }
            }
        }
        #endregion

        #region Private field
        private static uEye.Defines.Status statusRet = 0;
        private static uEye.Camera Camera;
        private static Bitmap frameCamera;
        private static CameraResults CameraStatus;
        #endregion

        #region Public functions
        public static CameraResults InitializeCamera()
        {
            Camera = new uEye.Camera();
            CameraStatus = new CameraResults();

            statusRet = Camera.Init(1);
            if (statusRet != uEye.Defines.Status.Success)
            {
                CameraStatus.ErrorCode = "0x01";
                CameraStatus.Status = "Error";
                CameraStatus.ErrorDescription="Can not be initialized uEye camera. Please check the camera connection, and try again!";
                return CameraStatus;
            }
            statusRet = Camera.IO.Pwm.SetMode(0);
            statusRet = Camera.Memory.Allocate();
            if (statusRet != uEye.Defines.Status.Success)
            {
                CameraStatus.ErrorCode = "0x02";
                CameraStatus.Status = "Error";
                CameraStatus.ErrorDescription = "Can not be allocate memory to uEye camera. Please check the camera connection, and try again!";
                return CameraStatus;
            }
            else
            {
                CameraStatus.ErrorCode = "";
                CameraStatus.Status = "Initialized";
                CameraStatus.ErrorDescription = "";
                return CameraStatus;
            }
        }
        public static CameraResults DisposeCamera()
        {
            if (CameraStatus.Status == "Live")
            {
                Camera.EventFrame -= onFrameEvent;
                Camera.Exit();
            }
            else
            {
                Application.Current.Dispatcher.Invoke(new Action(() =>
                {
                    Camera.Exit();
                }));               
            }
            CameraStatus.ErrorCode = "";
            CameraStatus.Status = "Disposed";
            CameraStatus.ErrorDescription = "";
            return CameraStatus;
        }
        public static CameraResults StartLiveView(System.Windows.Controls.Image wpfImage)
        {
            OutputImage = wpfImage;
            try
            {
                Camera.Acquisition.Capture();
                Camera.EventFrame += onFrameEvent;

                CameraStatus.ErrorCode = "";
                CameraStatus.Status = "Live";
                CameraStatus.ErrorDescription = "";
            }
            catch
            {
                CameraStatus.ErrorCode = "0x03";
                CameraStatus.Status = "Error";
                CameraStatus.ErrorDescription = "Can not be start camera capture. Please check the camera connection, and try again!";
            }
            return CameraStatus;

        }
        public static CameraResults StopLiveView()
        {
            try
            {

                Camera.Acquisition.Stop();
                Camera.EventFrame -= onFrameEvent;
                OutputImage.Source = null;
                CameraStatus.ErrorCode = "";
                CameraStatus.Status = "Sleep";
                CameraStatus.ErrorDescription = "";
            }
            catch
            {
                CameraStatus.ErrorCode = "0x04";
                CameraStatus.Status = "Error";
                CameraStatus.ErrorDescription = "Can not be sleep camera capture. Please check the camera connection, and try again!";
            }
            return CameraStatus;
        }
        #endregion

        #region Private functions
        private static BitmapSource BitmapConvert(System.Drawing.Bitmap bitmap)
        {
            var bitmapData = bitmap.LockBits(
                new System.Drawing.Rectangle(0, 0, bitmap.Width, bitmap.Height),
                System.Drawing.Imaging.ImageLockMode.ReadOnly, bitmap.PixelFormat);

            var bitmapSource = BitmapSource.Create(
                bitmapData.Width, bitmapData.Height,
                bitmap.HorizontalResolution, bitmap.VerticalResolution,
                PixelFormats.Bgr32, null,
                bitmapData.Scan0, bitmapData.Stride * bitmapData.Height, bitmapData.Stride);

            bitmap.UnlockBits(bitmapData);
            return bitmapSource;
        }
        #endregion

        #region Events
        private static void onFrameEvent(object sender, EventArgs e)
        {
            uEye.Camera Camera = sender as uEye.Camera;
            Int32 s32MemID;
            Camera.Memory.GetActive(out s32MemID);
            if (frameCamera != null)
                frameCamera.Dispose();
            frameCamera = null;
            Camera.Memory.ToBitmap(s32MemID, out frameCamera);
            try
            {
                Application.Current.Dispatcher.Invoke(new Action(() =>
                {
                    if (CameraResult.Status == "Live")
                    {
                        OutputImage.Source = BitmapConvert(frameCamera);
                    }
                    else
                    {
                        OutputImage.Source = null;
                    }
                }));
            }
            catch
            {

            }
        
        }
        #endregion
        
    }
}
