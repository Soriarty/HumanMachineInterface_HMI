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

namespace HM_Interface_Visu.Assets.ManualScreenElements.AxisScreenElements
{
    /// <summary>
    /// Interaction logic for X_Axis_Panel.xaml
    /// </summary>
    public partial class X_Axis_Panel : UserControl
    {
        public X_Axis_Panel()
        {
            InitializeComponent();
        }
        private void ExecuteButtonCommand(string VarName, bool status)
        {
            AdsCommunication.WriteAny(ReferenceHandler.GetReferenceAdress(VarName), status);
        }
        private void X_Positive_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            ExecuteButtonCommand("X_Axis_Positive", true);
        }

        private void X_Positive_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            ExecuteButtonCommand("X_Axis_Positive", false);
        }

        private void X_Negative_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            ExecuteButtonCommand("X_Axis_Negative", true);
        }

        private void X_Negative_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            ExecuteButtonCommand("X_Axis_Negative", false);
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            PositionSyncronizer.PropertyChanged += new PropertyChangedEventHandler(PosSyncEvent);
        }
        private void PosSyncEvent(object sender, PropertyChangedEventArgs e)
        {
            CurrentPos.Text = Math.Round(PositionSyncronizer.CurrentPos.X, 2).ToString();
        }
    }
}
