using HM_Interface_Visu.Classes;
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

        public void SetStatement(bool state)
        {
            if (state)
            {
                MovementModeSelect.Content = "Continous";
                ContinousSpeed.IsEnabled = true;
                StepSize.IsEnabled = false;
            }
            else
            {
                MovementModeSelect.Content = "Step";
                ContinousSpeed.IsEnabled = false;
                StepSize.IsEnabled = true;
            }
            AdsCommunication.WriteAny(MainWindow.GetReferenceAdress("ManualMovementMode", MainWindow.NotificationData), state);
        }

        private void MovementModeSelect_Click(object sender, RoutedEventArgs e)
        {
            if ((string)MovementModeSelect.Content == "Continous")
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
            StepSize.SelectedIndex = 0;
            SetStatement(AdsCommunication.ReadBit(MainWindow.GetReferenceAdress("ManualMovementMode", MainWindow.NotificationData)));
        }
    }
}
