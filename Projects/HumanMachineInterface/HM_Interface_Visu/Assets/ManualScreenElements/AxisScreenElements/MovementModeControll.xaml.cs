﻿using HM_Interface_Visu.Classes;
using MaterialDesignThemes.Wpf;
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
    /// Interaction logic for MovementModeControll.xaml
    /// </summary>
    public partial class MovementModeControll : UserControl
    {
        public MovementModeControll()
        {
            InitializeComponent();
        }
        private void InitLanguage()
        {
            SpeedBox.Text = LanguageHandler.GetMessageResource("ContSpeed") + "0 %";
        }
        private void InitData()
        {
            SpeedBar.Value = AdsCommunication.ReadLongReal(MainWindow.GetReferenceAdress("continousOverride", MainWindow.NotificationData));
            switch(AdsCommunication.ReadLongReal(MainWindow.GetReferenceAdress("StepSize", MainWindow.NotificationData))*10)
            {
                case 1:
                    StepSize.SelectedIndex = 0;
                    break;
                case 2:
                    StepSize.SelectedIndex = 1;
                    break;
                case 5:
                    StepSize.SelectedIndex = 2;
                    break;
                case 10:
                    StepSize.SelectedIndex = 3;
                    break;
                default:                 
                    break;
            }
        }
        public void SetStatement(bool state)
        {
            if (state)
            {
                MovementModeSelect.Content = LanguageHandler.GetMessageResource("btnContinous");
                SpeedBar.IsEnabled = true;
                StepSize.IsEnabled = false;
            }
            else
            {
                MovementModeSelect.Content = LanguageHandler.GetMessageResource("btnStep");
                SpeedBar.IsEnabled = false;
                StepSize.IsEnabled = true;
            }
            AdsCommunication.WriteAny(MainWindow.GetReferenceAdress("ManualMovementMode", MainWindow.NotificationData), state);
        }

        private void MovementModeSelect_Click(object sender, RoutedEventArgs e)
        {
            if (AdsCommunication.ReadBit(MainWindow.GetReferenceAdress("ManualMovementMode", MainWindow.NotificationData)))
            {
                SetStatement(false);
            }
            else
            {
                SetStatement(true);
            }
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {          
            SetStatement(AdsCommunication.ReadBit(MainWindow.GetReferenceAdress("ManualMovementMode", MainWindow.NotificationData)));
            InitLanguage();
            InitData();
        }
        private async void SpeedBox_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            var ParameterInput = new ParameterInputBox();
            ParameterInput.Message.Text = LanguageHandler.GetMessageResource("ContinousSpeed");
            await DialogHost.Show(ParameterInput, "RootDialog");

            if (ParameterInput.result)
            {
                if ((ParameterInput.Parameter.Text != null) && (ParameterInput.Parameter.Text != ""))
                {
                    if ((float.Parse(ParameterInput.Parameter.Text) > 0 && float.Parse(ParameterInput.Parameter.Text) <= 20))
                    {
                        SpeedBar.Value = float.Parse(ParameterInput.Parameter.Text);
                        SpeedBox.Text = LanguageHandler.GetMessageResource("ContSpeed") + float.Parse(ParameterInput.Parameter.Text).ToString() + " %";
                    }
                }
            }
        }

        private void SpeedBar_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (SpeedBox != null)
            {
                SpeedBox.Text = LanguageHandler.GetMessageResource("ContSpeed") + Math.Round((decimal)SpeedBar.Value, 0).ToString() + " %";
                AdsCommunication.WriteAny(MainWindow.GetReferenceAdress("continousOverride", MainWindow.NotificationData), (Double)Math.Round((decimal)SpeedBar.Value, 2));
            }
        }

        private void StepSize_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            double value = 0;
            switch (StepSize.SelectedIndex)
            {
                case 0:
                    value = 0.1D;
                    break;
                case 1:
                    value = 0.2D;
                    break;
                case 2:
                    value = 0.5D;
                    break;
                case 3:
                    value = 1D;
                    break;
                default:
                    value = 0D;
                    break;
            }
            AdsCommunication.WriteAny(MainWindow.GetReferenceAdress("StepSize", MainWindow.NotificationData), value);
        }
    }
}
