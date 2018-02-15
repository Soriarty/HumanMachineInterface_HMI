using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media;
using MaterialDesignThemes.Wpf;
using System.IO;
using System;
using HM_Interface_Visu.Classes;
using TwinCAT.Ads;
using CameraControll;
using System.Windows.Interop;
using System.Runtime.InteropServices;
using System.Security.AccessControl;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using System.Diagnostics;
using System.Windows.Threading;

namespace HM_Interface_Visu
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>

    public partial class MainWindow
    {
        [DllImport("kernel32.dll", EntryPoint = "GlobalAddAtomA", CharSet = CharSet.Ansi)]
        static extern UInt16 GlobalAddAtom(string lpString);

        [DllImport("user32.dll", EntryPoint = "SetPropA", CharSet = CharSet.Ansi)]
        static extern UInt32 SetProp(IntPtr hWnd, UInt32 lpString, UInt32 hData);

        public static AdsCommunication.VariableInfo[] NotificationData;
        SecurityCryption securityCryption = new SecurityCryption("G1FID#iY6j48Q7D");
        private Notification notifiDisplayer;      

        public static Snackbar Snackbar;
        private static Assets.LoginDialogBox LoginDialog;
        private static Assets.MainPage mainPage;
        private static Assets.ManualPage manualPage;
        private static Assets.SettingsPage settingsPage;
        private static Assets.HistoryPage historyPage;

        private static MainWindowViewModel WindowViewModel;
        private static int UserStatus = 1;
        public MainWindow()
        {
            InitializeComponent();
            this.SourceInitialized += mainWindow_SourceInitialized;
            InitializeContent();
        }
        #region Methods
        private void CloseNotification()
        {
            this.NotifiSlot.Children.Remove(NotifiSlot.Children[0]);
        }        
        private void DisplayError(int ErrorCode)
        {
            if(ErrorCode == 8)
            {
                string message = "Aktív vészköri megszakítás!";
                notifiDisplayer.Configuration(message , (SolidColorBrush)FindResource("BaseRedBrush"), true);
                NotifiSlot.Children.Add(notifiDisplayer);
            }
            
        }
        private void InitializeContent()
        {
            EventLogger.RecordEvent(EventLogger.EventType.Info,"0x0001", "Start intializing the FORM");
            
            notifiDisplayer = new Notification();
            notifiDisplayer.btnOK.Click += new RoutedEventHandler(CloseNotification_ButtonClick);

            uEye_Handler.InitializeCamera();
            if (uEye_Handler.CameraResult.Status != "Initialized")
            {
                EventLogger.RecordEvent(EventLogger.EventType.Warning, uEye_Handler.CameraResult.ErrorCode, uEye_Handler.CameraResult.ErrorDescription);
            }
            else
            {
                EventLogger.RecordEvent(EventLogger.EventType.Info, "0x0002", "Camera status is: "+uEye_Handler.CameraResult.Status);
            }
            mainPage = new Assets.MainPage();
            manualPage = new Assets.ManualPage();
            settingsPage = new Assets.SettingsPage();
            historyPage = new Assets.HistoryPage();
            LoginDialog = new Assets.LoginDialogBox();

            WindowViewModel = new MainWindowViewModel(this);
            mainPage.tbDate.Text = DateTime.Now.ToShortDateString();
            mainPage.tbCurrentTime.Text = DateTime.Now.ToShortTimeString();

            ReferenceHandler.ConnectRoutineADS(TwinCat3Client_AdsNotificationEx);

            WindowViewModel.DisplayPage(mainPage);
            WindowViewModel.InitialLanguage();
        }
        #endregion

        #region Events
        private void mainWindow_SourceInitialized(object sender, EventArgs e)
        {
            IntPtr handle = (new WindowInteropHelper(this)).Handle;
            HwndSource.FromHwnd(handle).AddHook(NativeMethods.MaximizedSizeFixWindowProc);

            UInt16 atom = GlobalAddAtom("MicrosoftTabletPenServiceProperty");
            if (atom != 0) SetProp(handle, (UInt32)atom, 1);
        }
        public void TwinCat3Client_AdsNotificationEx(object sender, AdsNotificationExEventArgs e)
        {
            Application.Current.Dispatcher.Invoke
            (new Action(() =>
            {
                if (e.UserData == ReferenceHandler.GetReferenceObject("controll"))
                {
                    if ((bool)e.Value) { btnControl.Background = (SolidColorBrush)FindResource("BaseGreenBrush"); }
                    else { btnControl.Background = (SolidColorBrush)FindResource("BaseRedBrush"); }
                }
                if (e.UserData == ReferenceHandler.GetReferenceObject("mode"))
                {
                    if ((bool)e.Value)
                    {
                        btnMode.Background = (SolidColorBrush)FindResource("BaseGreenBrush");
                        btnModeContent.Text = "A";
                    }
                    else
                    {
                        btnMode.Background = (SolidColorBrush)FindResource("BaseYellowBrush");
                        btnModeContent.Text = "M";
                    }
                }
                if (e.UserData == ReferenceHandler.GetReferenceObject("deprag_status")) { mainPage.tbStatus.Text = e.Value.ToString(); }
                if (e.UserData == ReferenceHandler.GetReferenceObject("deprag_step")) { mainPage.tbStepNr.Text = e.Value.ToString(); }
                if (e.UserData == ReferenceHandler.GetReferenceObject("result_torque")) { mainPage.tbTorque.Text = Math.Round((Single)e.Value, 3).ToString(); }
                if (e.UserData == ReferenceHandler.GetReferenceObject("result_angle")) { mainPage.tbAngle.Text = e.Value.ToString(); }
                if (e.UserData == ReferenceHandler.GetReferenceObject("deprag_program")) { mainPage.tbProgramNumber.Text = e.Value.ToString(); }
                if (e.UserData == ReferenceHandler.GetReferenceObject("deprag_CycleTime")) { mainPage.tbProcessTime.Text = Math.Round((Single)e.Value, 3).ToString(); }
                if (e.UserData == ReferenceHandler.GetReferenceObject("Override"))
                {
                    SpeedBar.Value = (Double)e.Value;
                    SpeedBox.Text = Math.Round((decimal)(Double)e.Value, 0).ToString() + " %";
                }
                if (e.UserData == ReferenceHandler.GetReferenceObject("message_number"))
                {
                    if(int.Parse(e.Value.ToString()) !=0)
                    {
                        AdsCommunication.WriteAny(ReferenceHandler.GetReferenceAdress("confirm"), false);
                        AdsCommunication.WriteAny(ReferenceHandler.GetReferenceAdress("ClearNotifi"), false);
                        DisplayError(int.Parse(e.Value.ToString()));

                    }
                    
                }

                if (e.UserData == ReferenceHandler.GetReferenceObject("ClearNotifi") && AdsCommunication.ReadInt(ReferenceHandler.GetReferenceAdress("message_number"))==0 && NotifiSlot.Children.Count !=0)
                {
                    CloseNotification();
                    AdsCommunication.WriteAny(ReferenceHandler.GetReferenceAdress("confirm"), false);
                }
            }));
        }
        private async void MenuPopupButton_OnClick(object sender, RoutedEventArgs e)
        {            
            if ((((ButtonBase)sender) == btnLogin2) && (UserStatus ==0))
            {
                LoginDialog.Message.Text = LanguageHandler.GetMessageResource("loginmessage");

                await DialogHost.Show(LoginDialog, "RootDialog");
                UserStatus = UserHandler.SelectUserLevel(LoginDialog.User.Text, LoginDialog.Password.Password);
                WindowViewModel.SetUserAcces(UserStatus);
                LoginDialog.User.Text = "";
                LoginDialog.Password.Password = "";

                if (NotifiSlot.Children.Count == 0 && UserStatus == 0)
                {
                    notifiDisplayer.Configuration(LanguageHandler.GetMessageResource("wrongpass"), (SolidColorBrush)FindResource("BaseYellowBrush"), false);
                    NotifiSlot.Children.Add(notifiDisplayer);
                }
                await Task.Factory.StartNew(() => { Thread.Sleep(5000); }).ContinueWith(t =>
                {
                    if (NotifiSlot.Children.Count != 0)
                    {
                        this.NotifiSlot.Children.Remove(NotifiSlot.Children[0]);
                    }

                }, TaskScheduler.FromCurrentSynchronizationContext());
         
            }
            else if ((((ButtonBase)sender) == btnLogin2) && (UserStatus > 0))
            {
                LoginDialog.User.Text = "";
                LoginDialog.Password.Password = "";
                UserStatus = UserHandler.SelectUserLevel(null , null);
                WindowViewModel.SetUserAcces(UserStatus);
            }
            this.Focus();
        }
        private void MetroWindow_Loaded(object sender, RoutedEventArgs e)
        {
            WindowViewModel.SetUserAcces(UserStatus);
            PositionSyncronizer.StartSyncing(100);
        }
        private void CloseNotification_ButtonClick(object sender, RoutedEventArgs e)
        {

            if (notifiDisplayer.btnOK.Opacity == 100 )
            {
                AdsCommunication.WriteAny(ReferenceHandler.GetReferenceAdress("confirm"), true);
                //if(AdsCommunication.ReadInt(GetReferenceAdress("message_number", NotificationData)) == 0)
                //{
                //    CloseNotification();
                //}               
            }
        }
        private void btnControl_Click(object sender, RoutedEventArgs e)
        {           
            if (!AdsCommunication.ReadBit(ReferenceHandler.GetReferenceAdress("controll"))) { AdsCommunication.WriteAny(ReferenceHandler.GetReferenceAdress("controll"), true); }
            else { AdsCommunication.WriteAny(ReferenceHandler.GetReferenceAdress("controll"), false); }
        }

        private void btnMainScreen_Click(object sender, RoutedEventArgs e)
        {
            if(uEye_Handler.CameraResult.Status == "Live")
            {            
                uEye_Handler.StopLiveView();
            }
            WindowViewModel.DisplayPage(mainPage);
        }

        private void btnManualScreen_Click(object sender, RoutedEventArgs e)
        {
            WindowViewModel.DisplayPage(manualPage);
        }

        private void btnSettingsScreen_Click(object sender, RoutedEventArgs e)
        {
            if (uEye_Handler.CameraResult.Status == "Live")
            {
                uEye_Handler.StopLiveView();
            }
            WindowViewModel.DisplayPage(settingsPage);
        }

        private void btnHistoryScreen_Click(object sender, RoutedEventArgs e)
        {
            if (uEye_Handler.CameraResult.Status == "Live")
            {
                uEye_Handler.StopLiveView();
            }
            WindowViewModel.DisplayPage(historyPage);
        }
        private void CloseCommandHandler(object sender, ExecutedRoutedEventArgs e)
        {
            uEye_Handler.StopLiveView();
            PositionSyncronizer.StopSyncing();
            this.Close();
        }
        private void btnMode_Click(object sender, RoutedEventArgs e)
        {
            if (!AdsCommunication.ReadBit(ReferenceHandler.GetReferenceAdress("mode"))) { AdsCommunication.WriteAny(ReferenceHandler.GetReferenceAdress("mode"), true); }
            else { AdsCommunication.WriteAny(ReferenceHandler.GetReferenceAdress("mode"), false); }
        }
        private void SpeedBar_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (SpeedBox != null)
            {
                SpeedBox.Text = Math.Round((decimal)SpeedBar.Value, 0).ToString() + " %";
                AdsCommunication.WriteAny(ReferenceHandler.GetReferenceAdress("Override"), (Double)Math.Round((decimal)SpeedBar.Value, 2));
            }
            
        }

        private async void SpeedBox_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (SpeedBar.IsEnabled)
            {
                var ParameterInput = new Assets.ParameterInputBox();
                ParameterInput.Message.Text = LanguageHandler.GetMessageResource("speedmsg");
                await DialogHost.Show(ParameterInput, "RootDialog");

                if (ParameterInput.result)
                {
                    if ((ParameterInput.Parameter.Text != null) && (ParameterInput.Parameter.Text != ""))
                    {
                        if ((float.Parse(ParameterInput.Parameter.Text) > 0 && float.Parse(ParameterInput.Parameter.Text) <= 100))
                        {
                            SpeedBar.Value = float.Parse(ParameterInput.Parameter.Text);
                            SpeedBox.Text = float.Parse(ParameterInput.Parameter.Text).ToString() + " %";
                        }
                    }
                }
            }

        }
        private void MinimizeButton_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }
        #endregion

        #region Class
        internal static class NativeMethods
        {
            #region Constants
            // The WM_GETMINMAXINFO message is sent to a window when the size or
            // position of the window is about to change.
            // An application can use this message to override the window's
            // default maximized size and position, or its default minimum or
            // maximum tracking size.
            private const int WM_GETMINMAXINFO = 0x0024;

            // Constants used with MonitorFromWindow()
            // Returns NULL.
            private const int MONITOR_DEFAULTTONULL = 0;

            // Returns a handle to the primary display monitor.
            private const int MONITOR_DEFAULTTOPRIMARY = 1;

            // Returns a handle to the display monitor that is nearest to the window.
            private const int MONITOR_DEFAULTTONEAREST = 2;
            #endregion

            #region Structs
            /// <summary>
            /// Native Windows API-compatible POINT struct
            /// </summary>
            [Serializable, StructLayout(LayoutKind.Sequential)]
            private struct POINT
            {
                public int X;
                public int Y;
            }
            /// <summary>
            /// The RECT structure defines the coordinates of the upper-left
            /// and lower-right corners of a rectangle.
            /// </summary>
            /// <see cref="http://msdn.microsoft.com/en-us/library/dd162897%28VS.85%29.aspx"/>
            /// <remarks>
            /// By convention, the right and bottom edges of the rectangle
            /// are normally considered exclusive.
            /// In other words, the pixel whose coordinates are ( right, bottom )
            /// lies immediately outside of the the rectangle.
            /// For example, when RECT is passed to the FillRect function, the rectangle
            /// is filled up to, but not including,
            /// the right column and bottom row of pixels. This structure is identical
            /// to the RECTL structure.
            /// </remarks>
            [StructLayout(LayoutKind.Sequential)]
            private struct RECT
            {
                /// <summary>
                /// The x-coordinate of the upper-left corner of the rectangle.
                /// </summary>
                public int Left;

                /// <summary>
                /// The y-coordinate of the upper-left corner of the rectangle.
                /// </summary>
                public int Top;

                /// <summary>
                /// The x-coordinate of the lower-right corner of the rectangle.
                /// </summary>
                public int Right;

                /// <summary>
                /// The y-coordinate of the lower-right corner of the rectangle.
                /// </summary>
                public int Bottom;
            }

            /// <summary>
            /// The MINMAXINFO structure contains information about a window's
            /// maximized size and position and its minimum and maximum tracking size.
            /// <seealso cref="http://msdn.microsoft.com/en-us/library/ms632605%28VS.85%29.aspx"/>
            /// </summary>
            [StructLayout(LayoutKind.Sequential)]
            private struct MINMAXINFO
            {
                /// <summary>
                /// Reserved; do not use.
                /// </summary>
                public POINT Reserved;

                /// <summary>
                /// Specifies the maximized width (POINT.x)
                /// and the maximized height (POINT.y) of the window.
                /// For top-level windows, this value
                /// is based on the width of the primary monitor.
                /// </summary>
                public POINT MaxSize;

                /// <summary>
                /// Specifies the position of the left side
                /// of the maximized window (POINT.x)
                /// and the position of the top
                /// of the maximized window (POINT.y).
                /// For top-level windows, this value is based
                /// on the position of the primary monitor.
                /// </summary>
                public POINT MaxPosition;

                /// <summary>
                /// Specifies the minimum tracking width (POINT.x)
                /// and the minimum tracking height (POINT.y) of the window.
                /// This value can be obtained programmatically
                /// from the system metrics SM_CXMINTRACK and SM_CYMINTRACK.
                /// </summary>
                public POINT MinTrackSize;

                /// <summary>
                /// Specifies the maximum tracking width (POINT.x)
                /// and the maximum tracking height (POINT.y) of the window.
                /// This value is based on the size of the virtual screen
                /// and can be obtained programmatically
                /// from the system metrics SM_CXMAXTRACK and SM_CYMAXTRACK.
                /// </summary>
                public POINT MaxTrackSize;
            }

            /// <summary>
            /// The WINDOWINFO structure contains window information.
            /// <seealso cref="http://msdn.microsoft.com/en-us/library/ms632610%28VS.85%29.aspx"/>
            /// </summary>
            [StructLayout(LayoutKind.Sequential)]
            private struct WINDOWINFO
            {
                /// <summary>
                /// The size of the structure, in bytes.
                /// The caller must set this to sizeof(WINDOWINFO).
                /// </summary>
                public uint Size;

                /// <summary>
                /// Pointer to a RECT structure
                /// that specifies the coordinates of the window.
                /// </summary>
                public RECT Window;

                /// <summary>
                /// Pointer to a RECT structure
                /// that specifies the coordinates of the client area.
                /// </summary>
                public RECT Client;

                /// <summary>
                /// The window styles. For a table of window styles,
                /// <see cref="http://msdn.microsoft.com/en-us/library/ms632680%28VS.85%29.aspx">
                /// CreateWindowEx
                /// </see>.
                /// </summary>
                public uint Style;

                /// <summary>
                /// The extended window styles. For a table of extended window styles,
                /// see CreateWindowEx.
                /// </summary>
                public uint ExStyle;

                /// <summary>
                /// The window status. If this member is WS_ACTIVECAPTION,
                /// the window is active. Otherwise, this member is zero.
                /// </summary>
                public uint WindowStatus;

                /// <summary>
                /// The width of the window border, in pixels.
                /// </summary>
                public uint WindowBordersWidth;

                /// <summary>
                /// The height of the window border, in pixels.
                /// </summary>
                public uint WindowBordersHeight;

                /// <summary>
                /// The window class atom (see
                /// <see cref="http://msdn.microsoft.com/en-us/library/ms633586%28VS.85%29.aspx">
                /// RegisterClass
                /// </see>).
                /// </summary>
                public ushort WindowType;

                /// <summary>
                /// The Windows version of the application that created the window.
                /// </summary>
                public ushort CreatorVersion;
            }

            /// <summary>
            /// The MONITORINFO structure contains information about a display monitor.
            /// The GetMonitorInfo function stores information in a MONITORINFO structure.
            /// <seealso cref="http://msdn.microsoft.com/en-us/library/dd145065%28VS.85%29.aspx"/>
            /// </summary>
            [StructLayout(LayoutKind.Sequential)]
            private struct MONITORINFO
            {
                /// <summary>
                /// The size, in bytes, of the structure. Set this member
                /// to sizeof(MONITORINFO) (40) before calling the GetMonitorInfo function.
                /// Doing so lets the function determine
                /// the type of structure you are passing to it.
                /// </summary>
                public int Size;

                /// <summary>
                /// A RECT structure that specifies the display monitor rectangle,
                /// expressed in virtual-screen coordinates.
                /// Note that if the monitor is not the primary display monitor,
                /// some of the rectangle's coordinates may be negative values.
                /// </summary>
                public RECT Monitor;

                /// <summary>
                /// A RECT structure that specifies the work area rectangle
                /// of the display monitor that can be used by applications,
                /// expressed in virtual-screen coordinates.
                /// Windows uses this rectangle to maximize an application on the monitor.
                /// The rest of the area in rcMonitor contains system windows
                /// such as the task bar and side bars.
                /// Note that if the monitor is not the primary display monitor,
                /// some of the rectangle's coordinates may be negative values.
                /// </summary>
                public RECT WorkArea;

                /// <summary>
                /// The attributes of the display monitor.
                ///
                /// This member can be the following value:
                /// 1 : MONITORINFOF_PRIMARY
                /// </summary>
                public uint Flags;
            }
            #endregion

            #region Imported methods
            /// <summary>
            /// The GetWindowInfo function retrieves information about the specified window.
            /// <seealso cref="http://msdn.microsoft.com/en-us/library/ms633516%28VS.85%29.aspx"/>
            /// </summary>
            /// <param name="hwnd">The window handle.</param>
            /// <param name="pwi">The reference to WINDOWINFO structure.</param>
            /// <returns>true on success</returns>
            [return: MarshalAs(UnmanagedType.Bool)]
            [DllImport("user32.dll", SetLastError = true)]
            private static extern bool GetWindowInfo(IntPtr hwnd, ref WINDOWINFO pwi);

            /// <summary>
            /// The MonitorFromWindow function retrieves a handle to the display monitor
            /// that has the largest area of intersection with the bounding rectangle
            /// of a specified window.
            /// <seealso cref="http://msdn.microsoft.com/en-us/library/dd145064%28VS.85%29.aspx"/>
            /// </summary>
            /// <param name="hwnd">The window handle.</param>
            /// <param name="dwFlags">Determines the function's return value
            /// if the window does not intersect any display monitor.</param>
            /// <returns>
            /// Monitor HMONITOR handle on success or based on dwFlags for failure
            /// </returns>
            [DllImport("user32.dll")]
            private static extern IntPtr MonitorFromWindow(IntPtr hwnd, uint dwFlags);

            /// <summary>
            /// The GetMonitorInfo function retrieves information about a display monitor
            /// <seealso cref="http://msdn.microsoft.com/en-us/library/dd144901%28VS.85%29.aspx"/>
            /// </summary>
            /// <param name="hMonitor">A handle to the display monitor of interest.</param>
            /// <param name="lpmi">
            /// A pointer to a MONITORINFO structure that receives information
            /// about the specified display monitor.
            /// </param>
            /// <returns></returns>
            [DllImport("user32.dll")]
            [return: MarshalAs(UnmanagedType.Bool)]
            private static extern bool GetMonitorInfo(IntPtr hMonitor, ref MONITORINFO lpmi);
            #endregion

            /// <summary>
            /// Window procedure callback.
            /// Hooked to a WPF maximized window works around a WPF bug:
            /// https://connect.microsoft.com/VisualStudio/feedback/details/363288/maximised-wpf-window-not-covering-full-screen?wa=wsignin1.0#tabs
            /// possibly also:
            /// https://connect.microsoft.com/VisualStudio/feedback/details/540394/maximized-window-does-not-cover-working-area-after-screen-setup-change?wa=wsignin1.0
            /// </summary>
            /// <param name="hwnd">The window handle.</param>
            /// <param name="msg">The window message.</param>
            /// <param name="wParam">The wParam (word parameter).</param>
            /// <param name="lParam">The lParam (long parameter).</param>
            /// <param name="handled">
            /// if set to <c>true</c> - the message is handled
            /// and should not be processed by other callbacks.
            /// </param>
            /// <returns></returns>
            internal static IntPtr MaximizedSizeFixWindowProc(
                IntPtr hwnd,
                int msg,
                IntPtr wParam,
                IntPtr lParam,
                ref bool handled)
            {
                switch (msg)
                {
                    case WM_GETMINMAXINFO:
                        // Handle the message and mark it as handled,
                        // so other callbacks do not touch it
                        WmGetMinMaxInfo(hwnd, lParam);
                        handled = true;
                        break;
                }
                return (IntPtr)0;
            }

            /// <summary>
            /// Creates and populates the MINMAXINFO structure for a maximized window.
            /// Puts the structure into memory address given by lParam.
            /// Only used to process a WM_GETMINMAXINFO message.
            /// </summary>
            /// <param name="hwnd">The window handle.</param>
            /// <param name="lParam">The lParam.</param>
            private static void WmGetMinMaxInfo(IntPtr hwnd, IntPtr lParam)
            {
                // Get the MINMAXINFO structure from memory location given by lParam
                MINMAXINFO mmi =
                    (MINMAXINFO)Marshal.PtrToStructure(lParam, typeof(MINMAXINFO));

                // Get the monitor that overlaps the window or the nearest
                IntPtr monitor = MonitorFromWindow(hwnd, MONITOR_DEFAULTTONEAREST);
                if (monitor != IntPtr.Zero)
                {
                    // Get monitor information
                    MONITORINFO monitorInfo = new MONITORINFO();
                    monitorInfo.Size = Marshal.SizeOf(typeof(MONITORINFO));
                    GetMonitorInfo(monitor, ref monitorInfo);

                    // The display monitor rectangle.
                    // If the monitor is not the primary display monitor,
                    // some of the rectangle's coordinates may be negative values
                    RECT rcMonitorArea = monitorInfo.Monitor;

                    // Get window information
                    WINDOWINFO windowInfo = new WINDOWINFO();
                    windowInfo.Size = (UInt32)(Marshal.SizeOf(typeof(WINDOWINFO)));
                    GetWindowInfo(hwnd, ref windowInfo);
                    int borderWidth = (int)windowInfo.WindowBordersWidth;
                    int borderHeight = (int)windowInfo.WindowBordersHeight;

                    // Set the dimensions of the window in maximized state
                    mmi.MaxPosition.X = -borderWidth;
                    mmi.MaxPosition.Y = -borderHeight;
                    mmi.MaxSize.X =
                        rcMonitorArea.Right - rcMonitorArea.Left + 2 * borderWidth;
                    mmi.MaxSize.Y =
                        rcMonitorArea.Bottom - rcMonitorArea.Top + 2 * borderHeight;

                    // Set minimum and maximum size
                    // to the size of the window in maximized state
                    mmi.MinTrackSize.X = mmi.MaxSize.X;
                    mmi.MinTrackSize.Y = mmi.MaxSize.Y;
                    mmi.MaxTrackSize.X = mmi.MaxSize.X;
                    mmi.MaxTrackSize.Y = mmi.MaxSize.Y;
                }

                // Copy the structure to memory location specified by lParam.
                // This concludes processing of WM_GETMINMAXINFO.
                Marshal.StructureToPtr(mmi, lParam, true);
            }
        }
        #endregion

    }
}

