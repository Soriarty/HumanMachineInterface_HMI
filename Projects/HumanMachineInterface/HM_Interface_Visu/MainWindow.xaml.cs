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

namespace HM_Interface_Visu
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {

        public static Snackbar Snackbar;
        private Notification notifiDisplayer;
        Assets.LoginDialogBox LoginDialog;
        public MainWindow()
        {
            InitializeComponent();
            WindowState = WindowState.Maximized;
            notifiDisplayer = new Notification();
            notifiDisplayer.btnOK.Click += new RoutedEventHandler(CloseNotification_ButtonClick);
            notifiDisplayer.BaseCard.GotTouchCapture += new EventHandler<TouchEventArgs>(CloseNotification_CardTouch);
            notifiDisplayer.BaseCard.MouseLeftButtonUp += new MouseButtonEventHandler(CloseNotification_CardClick);
            LoginDialog = new Assets.LoginDialogBox();
        }
        #region Methods
        private void CloseNotification()
        {
            this.NotifiSlot.Children.Remove(NotifiSlot.Children[0]);
        }
        #endregion

        #region Events
        private async void MenuPopupButton_OnClick(object sender, RoutedEventArgs e)
        {            
            if (((ButtonBase)sender).Content.ToString() == "Login")
            {
                LoginDialog.Message.Text = "Add meg a felhasználónevet és jelszót a karbantartási funkciók eléréséhez!";

                await DialogHost.Show(LoginDialog, "RootDialog");

                if (LoginDialog.User.Text == "admin" && LoginDialog.Password.Password == "100")
                {                    
                    if (NotifiSlot.Children.Count == 0)
                    {
                        notifiDisplayer.Configuration("Sikeres bejelentkezés, mostmár elérhetővé váltak a speciális funkciók", (SolidColorBrush)FindResource("BaseYellowBrush"), false);
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
                else
                {                  
                    
                    if (NotifiSlot.Children.Count == 0)
                    {
                        notifiDisplayer.Configuration("Sikertelen bejelentkezés, helytelen felhasználónév vagy jelszó!", (SolidColorBrush)FindResource("BaseRedBrush"), true);
                        NotifiSlot.Children.Add(notifiDisplayer);
                    }

                    //await Task.Factory.StartNew(() => { Thread.Sleep(5000); }).ContinueWith(t =>
                    // {
                    //     if (NotifiSlot.Children.Count != 0)
                    //     {
                    //         this.NotifiSlot.Children.Remove(NotifiSlot.Children[0]);
                    //     }
                    // }, TaskScheduler.FromCurrentSynchronizationContext());
                }
            }           

        }

        private void SnackBarButton_Click(object sender, RoutedEventArgs e)
        {

        }
        private void MetroWindow_Loaded(object sender, RoutedEventArgs e)
        {
            btnMode.Background = (SolidColorBrush)FindResource("BaseYellowBrush");
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
        }
        #endregion

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
            var ParameterInput = new Assets.ParameterInputBox();           
            ParameterInput.Message.Text = "Add meg a sebességet %-ban (1 és 100 között!)";
            await DialogHost.Show(ParameterInput, "RootDialog");
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

