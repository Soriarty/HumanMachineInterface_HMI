using CameraControll;
using HM_Interface_Visu.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace HM_Interface_Visu.Assets
{
    /// <summary>
    /// Interaction logic for ManualPage.xaml
    /// </summary>
    public partial class ManualPage : UserControl
    {
        private List<Rectangle> Indicators = new List<Rectangle>();
        private List<Button> Buttons = new List<Button>();
        private List<UserControl> Screens = new List<UserControl>();
        private string[] PageName = { "Axis", "Pneumathic", "MFU", "Other" };
        public ManualPage()
        {
            InitializeComponent();
            this.Name = "Manual";
            ViewModelInitialize();
            InitialLanguage();
            DisplayPage(Screens[0]);
        }
        public void ViewModelInitialize()
        {
            Screens.Add(new ManualScreenElements.AxisScreen());
            Screens.Add(new ManualScreenElements.PneumathicScreen());
            Screens.Add(new ManualScreenElements.MFUScreen());
            Screens.Add(new ManualScreenElements.OtherScreen());

            Indicators.Add(AxisIndicator);
            Indicators.Add(PneumathiclIndicator);
            Indicators.Add(MFUIndicator);
            Indicators.Add(OtherIndicator);

            Buttons.Add(btnAxis);
            Buttons.Add(btnPneumathic);
            Buttons.Add(btnMFU);
            Buttons.Add(btnOther);
        }
        public void InitialLanguage()
        {
            btnAxis.Content = LanguageHandler.GetMessageResource("AxisTabText");
            btnPneumathic.Content = LanguageHandler.GetMessageResource("PneuTabText");
            btnMFU.Content = LanguageHandler.GetMessageResource("MFUTabText");
            btnOther.Content = LanguageHandler.GetMessageResource("OtherTabText");
        }
        public void DisplayPage(UserControl userControl)
        {
            for (int index = 0; index < Buttons.Count; index++)
            {
                if (userControl.Name.Contains(PageName[index]))
                {
                    Indicators[index].Visibility = Visibility.Visible;
                    Buttons[index].Foreground = (SolidColorBrush)FindResource("AccentColorBrush");
                }
                else
                {
                    Indicators[index].Visibility = Visibility.Hidden;
                    Buttons[index].Foreground = (SolidColorBrush)FindResource("PrimaryHueMidForegroundBrush");
                }
            }

            if (ManualPageSlot.Children.Count == 0)
            {
                ManualPageSlot.Children.Add(userControl);
            }
            else
            {
                ManualPageSlot.Children.Remove(ManualPageSlot.Children[0]);
                ManualPageSlot.Children.Add(userControl);
            }
        }

        private void btnMenu_Click(object sender, RoutedEventArgs e)
        {
            if (((ButtonBase)sender) == btnAxis) DisplayPage(Screens[0]);
            if (((ButtonBase)sender) != btnAxis)
            {
                if (uEye_Handler.CameraResult.Status == "Live")
                {
                    uEye_Handler.StopLiveView();
                }
            }
            if (((ButtonBase)sender) == btnPneumathic) DisplayPage(Screens[1]);
            if (((ButtonBase)sender) == btnMFU) DisplayPage(Screens[2]);
            if (((ButtonBase)sender) == btnOther) DisplayPage(Screens[3]);
        }
    }

}
