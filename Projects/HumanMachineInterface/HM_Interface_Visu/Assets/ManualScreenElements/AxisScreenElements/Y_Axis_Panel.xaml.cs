﻿using HM_Interface_Visu.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

namespace HM_Interface_Visu.Assets.ManualScreenElements.AxisScreenElements
{
    /// <summary>
    /// Interaction logic for Y_Axis_Panel.xaml
    /// </summary>
    public partial class Y_Axis_Panel : UserControl
    {
        AdsCommunication.Coordinate coordinateCurrent = new AdsCommunication.Coordinate();
        public Y_Axis_Panel()
        {
            InitializeComponent();
            
        }
        private void ExecuteButtonCommand(string VarName, bool status)
        {
            AdsCommunication.WriteAny(MainWindow.GetReferenceAdress(VarName, MainWindow.NotificationData), status);
        }
        private void GetCurrentPos()
        {
            coordinateCurrent = AdsCommunication.CoordinateRead(MainWindow.GetReferenceAdress("CurrentPos", MainWindow.NotificationData));
            CurrentPos.Text = Math.Round(coordinateCurrent.Y, 2).ToString();
        }

        private void Y_Positive_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            ExecuteButtonCommand("Y_Axis_Positive", true);
            GetCurrentPos();
        }

        private void Y_Positive_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            ExecuteButtonCommand("Y_Axis_Positive", false);
            GetCurrentPos();
        }

        private void Y_Negative_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            ExecuteButtonCommand("Y_Axis_Negative", true);
            GetCurrentPos();
        }

        private void Y_Negative_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            ExecuteButtonCommand("Y_Axis_Negative", false);
            GetCurrentPos();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            GetCurrentPos();
        }
    }
}
