using HM_Interface_Visu.Classes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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

namespace HM_Interface_Visu.Assets.ManualScreenElements.OtherScreenElements
{
    /// <summary>
    /// Interaction logic for Y_Page.xaml
    /// </summary>
    public partial class Y_Page : UserControl
    {
        double SaveTempPos;
        const string axisName = "Y_";
        public Y_Page()
        {
            InitializeComponent();
            this.Name = "Y_Page";
        }
        private void ExecuteButtonCommand(string VarName, bool status)
        {
            AdsCommunication.WriteAny(ReferenceHandler.GetReferenceAdress(VarName), status);
        }
        private void Positive_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            ExecuteButtonCommand(axisName + "Axis_Positive", true);
        }

        private void Positive_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            ExecuteButtonCommand(axisName + "Axis_Positive", false);
        }

        private void GoTo_Click(object sender, RoutedEventArgs e)
        {
            if (Selector.Text != "")
            {
                AdsCommunication.WriteAny(ReferenceHandler.GetReferenceAdress(axisName + Selector.Text), SaveTempPos);
                ComboBoxItem comboBoxItem = Selector.SelectedItem as ComboBoxItem;
                TargetPos.Text = Math.Round(AdsCommunication.ReadLongReal(ReferenceHandler.GetReferenceAdress(axisName + comboBoxItem.Content.ToString())), 2).ToString();
            }
        }

        private void Negative_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            ExecuteButtonCommand(axisName + "Axis_Negative", true);
        }

        private void Negative_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            ExecuteButtonCommand(axisName + "Axis_Negative", false);
        }
        private void PosSyncEvent(object sender, PropertyChangedEventArgs e)
        {
            CurrentPos.Text = Math.Round(PositionSyncronizer.CurrentPos.Y, 2).ToString();
            SaveTempPos = Math.Round(PositionSyncronizer.CurrentPos.Y, 2);
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            PositionSyncronizer.PropertyChanged += new PropertyChangedEventHandler(PosSyncEvent);
        }

        private void Selector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBoxItem comboBoxItem = Selector.SelectedItem as ComboBoxItem;
            TargetPos.Text = Math.Round(AdsCommunication.ReadLongReal(ReferenceHandler.GetReferenceAdress(axisName + comboBoxItem.Content.ToString())), 2).ToString();
        }
    }
}
