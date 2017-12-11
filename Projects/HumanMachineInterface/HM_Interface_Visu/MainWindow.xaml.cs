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

namespace HM_Interface_Visu
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        AdsCommunication AdsCommunication = new AdsCommunication();
        AdsCommunication.VariableInfo[] NotificationData;

        public static Snackbar Snackbar;
        private Notification notifiDisplayer;
        private static Assets.LoginDialogBox LoginDialog;
        private static Assets.MainPage mainPage;
        private static Assets.ManualPage manualPage;
        private static Assets.SettingsPage settingsPage;
        private static Assets.HistoryPage historyPage;
        private static int UserStatus = 0;
        public MainWindow()
        {
            NotificationData = new AdsCommunication.VariableInfo[2];

            InitializeComponent();
            InitializeContent();
            WindowState = WindowState.Maximized;

            //AdsCommunication.Connect(TwinCat3Client_AdsNotificationEx);
            //AdsCommunication.RegisterNotification(NotificationData, 100);

        }
        #region Methods

        private void CloseNotification()
        {
            this.NotifiSlot.Children.Remove(NotifiSlot.Children[0]);
        }
        //private void FillStruct()
        //{
        //    NotificationData[0].VarAdress = "GlobalVar.G_Cycle";
        //    NotificationData[1].VarAdress = "MAIN.b_IncrementNow";

        //    NotificationData[0].Types = typeof(int);
        //    NotificationData[1].Types = typeof(Boolean);

        //    NotificationData[0].Objects = lbStatus;
        //    NotificationData[1].Objects = colorBit;
        //}

        private int SelectUserLevel(string userdata)
        {
            if (userdata == "admin100")
            {
                return 1;
            }
            else if (userdata == "engineer100engineer100")
            {
                return 2;
            }
            else
            {
                return 0;
            }
        }
        private void SetUserAcces(int userLevel)
        {
            switch(userLevel)
            {
                case 0:
                    btnManualScreen.IsEnabled = false;
                    btnSettingsScreen.IsEnabled = false;
                    btnHistoryScreen.IsEnabled = false;
                    SpeedBar.IsEnabled = false;
                    btnLogout.IsEnabled = false;
                    btnLogin.IsEnabled = true;
                    btnLogin2_Icon.Foreground = (SolidColorBrush)FindResource("MaterialDesignBackground");
                    btnLogin2_Text.Foreground = (SolidColorBrush)FindResource("MaterialDesignBackground");
                    btnLogin2_Text.Text = "Operator";
                    UserStatus = userLevel;
                    break;
                case 1:
                    btnManualScreen.IsEnabled = true;
                    btnSettingsScreen.IsEnabled = true;
                    btnHistoryScreen.IsEnabled = true;
                    SpeedBar.IsEnabled = true;
                    btnLogout.IsEnabled = true;
                    btnLogin.IsEnabled = false;
                    btnLogin2_Icon.Foreground = (SolidColorBrush)FindResource("AccentColorBrush");
                    btnLogin2_Text.Foreground = (SolidColorBrush)FindResource("AccentColorBrush");
                    btnLogin2_Text.Text = "Maintenance";
                    UserStatus = userLevel;
                    break;
                case 2:
                    btnManualScreen.IsEnabled = true;
                    btnSettingsScreen.IsEnabled = true;
                    btnHistoryScreen.IsEnabled = true;
                    SpeedBar.IsEnabled = true;
                    btnLogout.IsEnabled = true;
                    btnLogin.IsEnabled = false;
                    btnLogin2_Icon.Foreground = (SolidColorBrush)FindResource("AccentColorBrush");
                    btnLogin2_Text.Foreground = (SolidColorBrush)FindResource("AccentColorBrush");
                    btnLogin2_Text.Text = "Engineer";
                    UserStatus = userLevel;
                    break;
            }
                

        }

        private void InitializeContent()
        {
            notifiDisplayer = new Notification();
            notifiDisplayer.btnOK.Click += new RoutedEventHandler(CloseNotification_ButtonClick);
            notifiDisplayer.BaseCard.GotTouchCapture += new EventHandler<TouchEventArgs>(CloseNotification_CardTouch);
            notifiDisplayer.BaseCard.MouseLeftButtonUp += new MouseButtonEventHandler(CloseNotification_CardClick);

            mainPage = new Assets.MainPage();
            manualPage = new Assets.ManualPage();
            settingsPage = new Assets.SettingsPage();
            historyPage = new Assets.HistoryPage();
            LoginDialog = new Assets.LoginDialogBox();
        }
        #endregion

        #region Events
        private void TwinCat3Client_AdsNotificationEx(object sender, AdsNotificationExEventArgs e)
        {

        }
            private async void MenuPopupButton_OnClick(object sender, RoutedEventArgs e)
        {            
            if ((((ButtonBase)sender).Name.ToString() == "btnLogin2") && (UserStatus ==0))
            {
                LoginDialog.Message.Text = "Add meg a felhasználónevet és jelszót a karbantartási funkciók eléréséhez!";

                await DialogHost.Show(LoginDialog, "RootDialog");
                SetUserAcces(SelectUserLevel(LoginDialog.User.Text + LoginDialog.Password.Password));
                LoginDialog.User.Text = "";
                LoginDialog.Password.Password = "";

                if (NotifiSlot.Children.Count == 0 && UserStatus == 0)
                {
                    notifiDisplayer.Configuration("Sikertelen bejelentkezés, hibás jelszó vagy felhasználónév", (SolidColorBrush)FindResource("BaseYellowBrush"), false);
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
            else if ((((ButtonBase)sender).Name.ToString() == "btnLogin2") && (UserStatus > 0))
            {
                LoginDialog.User.Text = "";
                LoginDialog.Password.Password = "";
                SetUserAcces(0);
            }
            this.Focus();
        }

        private void SnackBarButton_Click(object sender, RoutedEventArgs e)
        {

        }
        private void MetroWindow_Loaded(object sender, RoutedEventArgs e)
        {
            btnMode.Background = (SolidColorBrush)FindResource("BaseYellowBrush");
            MainIndicator.Visibility = Visibility.Visible;
            ManualIndicator.Visibility = Visibility.Hidden;
            SettingsIndicator.Visibility = Visibility.Hidden;
            HistoryIndicator.Visibility = Visibility.Hidden;

            btnMainScreen.Foreground = (SolidColorBrush)FindResource("AccentColorBrush");
            btnManualScreen.Foreground = (SolidColorBrush)FindResource("PrimaryHueMidForegroundBrush");
            btnSettingsScreen.Foreground = (SolidColorBrush)FindResource("PrimaryHueMidForegroundBrush");
            btnHistoryScreen.Foreground = (SolidColorBrush)FindResource("PrimaryHueMidForegroundBrush");

            mainPage.tbProgramNumber.Text = "P28";
            mainPage.tbDate.Text = "08.12.2017";
            mainPage.tbCurrentTime.Text = "13:18";
            mainPage.tbProcessTime.Text = "1.8" + "s";
            mainPage.tbStatus.Text = "In Progress";
            mainPage.tbStepNr.Text = "2";
            mainPage.tbTorque.Text = "1.85";
            mainPage.tbAngle.Text = "54";

            if (PageSlot.Children.Count == 0)
            {
                PageSlot.Children.Add(mainPage);
            }
            else
            {
                PageSlot.Children.Remove(PageSlot.Children[0]);
                PageSlot.Children.Add(mainPage);
            }
        }
        private void MenuToggleButton_Checked(object sender, RoutedEventArgs e)
        {

        }
        private void CloseNotification_ButtonClick(object sender, RoutedEventArgs e)
        {
            if (notifiDisplayer.btnOK.Opacity == 100) { CloseNotification(); }

        }
        private void CloseNotification_CardTouch(object sender, TouchEventArgs e)
        {
            if (notifiDisplayer.btnOK.Opacity == 0) { CloseNotification(); }
        }
        private void CloseNotification_CardClick(object sender, MouseButtonEventArgs e)
        {
            if (notifiDisplayer.btnOK.Opacity == 0) { CloseNotification(); }
        }
        private void btnControl_Click(object sender, RoutedEventArgs e)
        {
            if (btnControl.Background == (SolidColorBrush)FindResource("BaseRedBrush")) { btnControl.Background = (SolidColorBrush)FindResource("BaseGreenBrush"); }
            else { btnControl.Background = (SolidColorBrush)FindResource("BaseRedBrush"); }
        }

        private void btnMainScreen_Click(object sender, RoutedEventArgs e)
        {
            MainIndicator.Visibility = Visibility.Visible;
            ManualIndicator.Visibility = Visibility.Hidden;
            SettingsIndicator.Visibility = Visibility.Hidden;
            HistoryIndicator.Visibility = Visibility.Hidden;

            btnMainScreen.Foreground = (SolidColorBrush)FindResource("AccentColorBrush");
            btnManualScreen.Foreground = (SolidColorBrush)FindResource("PrimaryHueMidForegroundBrush");
            btnSettingsScreen.Foreground = (SolidColorBrush)FindResource("PrimaryHueMidForegroundBrush");
            btnHistoryScreen.Foreground = (SolidColorBrush)FindResource("PrimaryHueMidForegroundBrush");

            mainPage.tbProgramNumber.Text = "P28";
            mainPage.tbDate.Text = "08.12.2017";
            mainPage.tbCurrentTime.Text = "13:18";
            mainPage.tbProcessTime.Text = "1.8" + "s";
            mainPage.tbStatus.Text = "In Progress";
            mainPage.tbStepNr.Text = "2";
            mainPage.tbTorque.Text = "1.85";
            mainPage.tbAngle.Text = "54";

            if (PageSlot.Children.Count == 0)
            {
                PageSlot.Children.Add(mainPage);
            }
            else
            {
                PageSlot.Children.Remove(PageSlot.Children[0]);
                PageSlot.Children.Add(mainPage);
            }
        }

        private void btnManualScreen_Click(object sender, RoutedEventArgs e)
        {
            MainIndicator.Visibility = Visibility.Hidden;
            ManualIndicator.Visibility = Visibility.Visible;
            SettingsIndicator.Visibility = Visibility.Hidden;
            HistoryIndicator.Visibility = Visibility.Hidden;

            btnMainScreen.Foreground = (SolidColorBrush)FindResource("PrimaryHueMidForegroundBrush");
            btnManualScreen.Foreground = (SolidColorBrush)FindResource("AccentColorBrush");
            btnSettingsScreen.Foreground = (SolidColorBrush)FindResource("PrimaryHueMidForegroundBrush");
            btnHistoryScreen.Foreground = (SolidColorBrush)FindResource("PrimaryHueMidForegroundBrush");

            if (PageSlot.Children.Count == 0)
            {
                PageSlot.Children.Add(manualPage);
            }
            else
            {
                PageSlot.Children.Remove(PageSlot.Children[0]);
                PageSlot.Children.Add(manualPage);
            }
        }

        private void btnSettingsScreen_Click(object sender, RoutedEventArgs e)
        {
            MainIndicator.Visibility = Visibility.Hidden;
            ManualIndicator.Visibility = Visibility.Hidden;
            SettingsIndicator.Visibility = Visibility.Visible;
            HistoryIndicator.Visibility = Visibility.Hidden;

            btnMainScreen.Foreground = (SolidColorBrush)FindResource("PrimaryHueMidForegroundBrush");
            btnManualScreen.Foreground = (SolidColorBrush)FindResource("PrimaryHueMidForegroundBrush");
            btnSettingsScreen.Foreground = (SolidColorBrush)FindResource("AccentColorBrush");
            btnHistoryScreen.Foreground = (SolidColorBrush)FindResource("PrimaryHueMidForegroundBrush");

            if (PageSlot.Children.Count == 0)
            {
                PageSlot.Children.Add(settingsPage);
            }
            else
            {
                PageSlot.Children.Remove(PageSlot.Children[0]);
                PageSlot.Children.Add(settingsPage);
            }
        }

        private void btnHistoryScreen_Click(object sender, RoutedEventArgs e)
        {
            MainIndicator.Visibility = Visibility.Hidden;
            ManualIndicator.Visibility = Visibility.Hidden;
            SettingsIndicator.Visibility = Visibility.Hidden;
            HistoryIndicator.Visibility = Visibility.Visible;

            btnMainScreen.Foreground = (SolidColorBrush)FindResource("PrimaryHueMidForegroundBrush");
            btnManualScreen.Foreground = (SolidColorBrush)FindResource("PrimaryHueMidForegroundBrush");
            btnSettingsScreen.Foreground = (SolidColorBrush)FindResource("PrimaryHueMidForegroundBrush");
            btnHistoryScreen.Foreground = (SolidColorBrush)FindResource("AccentColorBrush");

            if (PageSlot.Children.Count == 0)
            {
                PageSlot.Children.Add(historyPage);
            }
            else
            {
                PageSlot.Children.Remove(PageSlot.Children[0]);
                PageSlot.Children.Add(historyPage);
            }
        }
        

        private void btnMode_Click(object sender, RoutedEventArgs e)
        {
            if (btnMode.Background == (SolidColorBrush)FindResource("BaseYellowBrush"))
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

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
                           
        }

        private void SpeedBar_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (SpeedBox != null)
            {
                SpeedBox.Text = Math.Round((decimal)SpeedBar.Value, 0).ToString() + " %";
            }
            
        }

        private async void SpeedBox_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (SpeedBar.IsEnabled)
            {
                var ParameterInput = new Assets.ParameterInputBox();
                ParameterInput.Message.Text = "Add meg a sebességet %-ban (1 és 100 között!)";
                await DialogHost.Show(ParameterInput, "RootDialog");

                if (ParameterInput.result)
                {
                    if ((ParameterInput.Parameter.Text != null) && (ParameterInput.Parameter.Text != ""))
                    {
                        if ((int.Parse(ParameterInput.Parameter.Text) > 0 && int.Parse(ParameterInput.Parameter.Text) <= 100))
                        {
                            SpeedBar.Value = float.Parse(ParameterInput.Parameter.Text);
                            SpeedBox.Text = float.Parse(ParameterInput.Parameter.Text).ToString() + " %";
                        }
                    }
                }
            }

        }
        #endregion
    }
}

