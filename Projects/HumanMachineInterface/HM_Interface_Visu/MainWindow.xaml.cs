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

        public MainWindow()
        {
            InitializeComponent();
            WindowState = WindowState.Maximized;
            notifiDisplayer = new Notification();
            notifiDisplayer.btnOK.Click += new RoutedEventHandler(CloseNotification_ButtonClick);
            notifiDisplayer.BaseCard.GotTouchCapture += new EventHandler<TouchEventArgs>(CloseNotification_CardTouch);
            notifiDisplayer.BaseCard.MouseLeftButtonUp += new MouseButtonEventHandler(CloseNotification_CardClick);
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
            var LoginDialog = new LoginDialogBox();
            if (((ButtonBase)sender).Content.ToString() == "Login")
            {
                LoginDialog.Message.Text = "Jelentkezz be a karbantartási funkciók eléréséhez!";

                await DialogHost.Show(LoginDialog, "RootDialog");

                if (LoginDialog.User.Text == "admin" && LoginDialog.Password.Password == "100")
                {
                    notifiDisplayer.Configuration("Sikeres bejelentkezés", Colors.Yellow, false);
                    if (NotifiSlot.Children.Count == 0)
                    {
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
                    notifiDisplayer.Configuration("Sikertelen bejelentkezés, helytelen felhasználónév vagy jelszó!", Colors.Red, true);
                    if (NotifiSlot.Children.Count == 0)
                    {
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
        #endregion
    }
}

