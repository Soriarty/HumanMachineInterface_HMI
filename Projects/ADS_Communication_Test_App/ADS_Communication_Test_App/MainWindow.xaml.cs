using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
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
using System.Windows.Threading;
using ADS_Communication_Test_App.adsService;
using TwinCAT.Ads;

namespace ADS_Communication_Test_App
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        AdsCommunication AdsCommunication = new AdsCommunication();

        Label t_Label = new Label();
        TextBox t_TextBox = new TextBox();
        Rectangle t_Rectangle = new Rectangle();
        AdsCommunication.VariableInfo[] NotificationData;
        public MainWindow()
        {
            InitializeComponent();
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            NotificationData = new AdsCommunication.VariableInfo[2];

            FillStruct();

            AdsCommunication.Connect(TwinCat3Client_AdsNotificationEx);
            AdsCommunication.RegisterNotification(NotificationData, 100);

            btnReadParameter.IsEnabled = true;
            btnSetParameter.IsEnabled = true;
            btnConnect.IsEnabled = false;
            btnDisconnect.IsEnabled = true;
        }       
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            AdsCommunication.Disconnect();

        }
        private void TwinCat3Client_AdsNotificationEx(object sender, AdsNotificationExEventArgs e)
        {
            Application.Current.Dispatcher.Invoke
            (new Action(() => 
            {
                Type ObjectType = e.UserData.GetType();
                

                this.lblist.Items.Add(e.Value.ToString());
                if (this.lblist.Items.Count == 20) { this.lblist.Items.Remove(this.lblist.Items[0]); }

                if (ObjectType == typeof(Rectangle))
                {
                    t_Rectangle = (Rectangle)e.UserData;
                    if ((bool)e.Value)
                    {            
                        
                        t_Rectangle.Fill = new SolidColorBrush(Colors.Green);

                    }
                    else
                    {
                        t_Rectangle.Fill = new SolidColorBrush(Colors.Red);
                    }                    
                }

                if (ObjectType == typeof(Label))
                {
                    t_Label = (Label)e.UserData;
                    t_Label.Content = e.Value.ToString();
                    
                }

                if (ObjectType == typeof(TextBox))
                {
                    t_TextBox = (TextBox)e.UserData;
                    t_TextBox.Text = e.Value.ToString();
                }

            }
            ));
        }
        private void btnConnect_Click(object sender, RoutedEventArgs e)
        {
            AdsCommunication.Connect(TwinCat3Client_AdsNotificationEx);
            AdsCommunication.RegisterNotification(NotificationData, 100);
            btnReadParameter.IsEnabled = true;
            btnSetParameter.IsEnabled = true;
            btnDisconnect.IsEnabled = true;
            btnConnect.IsEnabled = false;
        }
        private void btnDisconnect_Click(object sender, RoutedEventArgs e)
        {
            AdsCommunication.Disconnect();
            btnConnect.IsEnabled = true;
            btnReadParameter.IsEnabled = false;
            btnSetParameter.IsEnabled = false;
            btnDisconnect.IsEnabled = false;
        }
        private void btnReadParameter_Click(object sender, RoutedEventArgs e)
        {
            tbIncrement.Text = AdsCommunication.ReadInt("MAIN.in_increment").ToString();
            tbMaxCount.Text = AdsCommunication.ReadInt("MAIN.in_maxcount").ToString();
            tbSpeed.Text = AdsCommunication.ReadInt("MAIN.in_speed").ToString();
            tbSpeedStep.Text = AdsCommunication.ReadInt("MAIN.in_speedstep").ToString();             
        }
        private void btnSetParameter_Click(object sender, RoutedEventArgs e)
        {
            AdsCommunication.WriteAny("MAIN.in_increment", short.Parse(tbIncrement.Text));
            AdsCommunication.WriteAny("MAIN.in_maxcount", short.Parse(tbMaxCount.Text));
            AdsCommunication.WriteAny("MAIN.in_speed", short.Parse(tbSpeed.Text));
            AdsCommunication.WriteAny("MAIN.in_speedstep", short.Parse(tbSpeedStep.Text));
        }
        private void colorBit_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (AdsCommunication.ReadBit("MAIN.b_IncrementNow"))
            {
                AdsCommunication.WriteAny("MAIN.b_IncrementNow", false);
            }
            else
            {
                AdsCommunication.WriteAny("MAIN.b_IncrementNow", true);
            }
        }
        private void FillStruct()
        {
            NotificationData[0].VarAdress = "GlobalVar.G_Cycle";
            NotificationData[1].VarAdress = "MAIN.b_IncrementNow";
           
            NotificationData[0].Types = typeof(int);
            NotificationData[1].Types = typeof(Boolean);
           
            NotificationData[0].Objects = lbStatus;
            NotificationData[1].Objects = colorBit;
        }
    }
}
