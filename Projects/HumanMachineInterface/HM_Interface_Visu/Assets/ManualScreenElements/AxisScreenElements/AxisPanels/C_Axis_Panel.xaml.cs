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
    /// Interaction logic for C_Axis_Panel.xaml
    /// </summary>
    public partial class C_Axis_Panel : UserControl
    {
        public C_Axis_Panel()
        {
            InitializeComponent();
        }
        private void ExecuteButtonCommand(string VarName, bool status)
        {
            AdsCommunication.WriteAny(ReferenceHandler.GetReferenceAdress(VarName), status);
        }
        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            PositionSyncronizer.PropertyChanged += new PropertyChangedEventHandler(PosSyncEvent);
        }
        private void PosSyncEvent(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "CurrentPosition")
            {
                CurrentPos.Text = Math.Round(PositionSyncronizer.CurrentPosition.C, 2).ToString();
            }
            else if (e.PropertyName == "TargetPosition")
            {
                TargetPos.Text = Math.Round(PositionSyncronizer.TargetPosition.C, 2).ToString();
            }            
        }

        private void C_Axis_Positive_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            ExecuteButtonCommand("C_Axis_Positive", true);
        }

        private void C_Axis_Positive_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            ExecuteButtonCommand("C_Axis_Positive", false);
        }

        private void C_Axis_Negative_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            ExecuteButtonCommand("C_Axis_Negative", true);
        }

        private void C_Axis_Negative_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            ExecuteButtonCommand("C_Axis_Negative", false);
        }
    }
}
