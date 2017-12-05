using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using MaterialDesignThemes.Wpf;
using System.IO;


namespace HM_Interface_Visu
{
    /// <summary>
    /// Interaction logic for Notification.xaml
    /// </summary>
    public partial class Notification : UserControl
    {

        public Notification()
        {
            InitializeComponent();
        }
        public void Configuration(string strMessage, Color ErrorColor)
        {
            this.Message.Text = strMessage;
            this.ErrorColor.Fill = new SolidColorBrush(ErrorColor);
        }
        public void OkButton_Click(object sender, RoutedEventArgs e)
        {
        }
    }
}
