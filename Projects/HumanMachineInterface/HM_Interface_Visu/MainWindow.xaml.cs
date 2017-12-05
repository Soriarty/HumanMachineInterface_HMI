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
        private Notification notifiDisplayer = new Notification();
        public MainWindow()
        {
            InitializeComponent();
            notifiDisplayer.btnOK.Click += new RoutedEventHandler(clearNotification_event);
        }
        private void UIElement_OnPreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            //until we had a StaysOpen glag to Drawer, this will help with scroll bars
            var dependencyObject = Mouse.Captured as DependencyObject;
            while (dependencyObject != null)
            {
                if (dependencyObject is ScrollBar) return;
                dependencyObject = VisualTreeHelper.GetParent(dependencyObject);
            }

            MenuToggleButton.IsChecked = false;
        }

        private async void MenuPopupButton_OnClick(object sender, RoutedEventArgs e)
        {
            var LoginDialog = new LoginDialogBox();
            if (((ButtonBase)sender).Content.ToString() == "Login")
            {
                LoginDialog.Message.Text = "Jelentkezz be a karbantartási funkciók eléréséhez!";

                await DialogHost.Show(LoginDialog, "RootDialog");

                if (LoginDialog.User.Text == "admin" && LoginDialog.Password.Password == "100")
                {
                    await Task.Factory.StartNew(() => { Thread.Sleep(500); }).ContinueWith(t =>
                    {
                        notifiDisplayer.Configuration("Ez egy figyelmeztető jelzés", Colors.Yellow);

                        if (NotifiSlot.Children.Count == 0)
                        {
                            NotifiSlot.Children.Add(notifiDisplayer);
                        }
                    }, TaskScheduler.FromCurrentSynchronizationContext());
                }
                else
                {
                    await Task.Factory.StartNew(() => { Thread.Sleep(500); }).ContinueWith(t =>
                     {
                         notifiDisplayer.Configuration("Ez egy hiba jelzés", Colors.Red);
                         if (NotifiSlot.Children.Count == 0)
                         {
                             NotifiSlot.Children.Add(notifiDisplayer);
                         }
                     }, TaskScheduler.FromCurrentSynchronizationContext());
                }
            }

        }
        private void SnackBarButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void MetroWindow_Loaded(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Maximized;
        }

        private void MenuToggleButton_Checked(object sender, RoutedEventArgs e)
        {

        }
        public void clearNotification_event(object sender, RoutedEventArgs e)
        {
            this.NotifiSlot.Children.Remove(NotifiSlot.Children[0]);
        }
    }
}

