using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace HM_Interface_Visu.Classes
{
    public class MainWindowViewModel
    {
        private MainWindow parent;
        private List<Rectangle> Indicators = new List<Rectangle>();
        private List<Button> Buttons = new List<Button>();
        private string[] PageName = { "Main", "Manual", "Settings", "History" };
        public MainWindowViewModel(MainWindow _parent)
        {
            this.parent = _parent;
            ViewModelInitialize();
        }
        public void ViewModelInitialize()
        {
            Indicators.Add(parent.MainIndicator);
            Indicators.Add(parent.ManualIndicator);
            Indicators.Add(parent.SettingsIndicator);
            Indicators.Add(parent.HistoryIndicator);

            Buttons.Add(parent.btnMainScreen);
            Buttons.Add(parent.btnManualScreen);
            Buttons.Add(parent.btnSettingsScreen);
            Buttons.Add(parent.btnHistoryScreen);
        }
        public void SetUserAcces(int UserLevel)
        {
            switch (UserLevel)
            {
                case 0:
                    parent.btnManualScreen.IsEnabled = false;
                    parent.btnSettingsScreen.IsEnabled = false;
                    parent.btnHistoryScreen.IsEnabled = false;
                    parent.SpeedBar.IsEnabled = false;

                    parent.btnLogin2_Icon.Foreground = (SolidColorBrush)parent.FindResource("MaterialDesignBackground");
                    parent.btnLogin2_Text.Foreground = (SolidColorBrush)parent.FindResource("MaterialDesignBackground");
                    parent.btnLogin2_Text.Text = "Operator";
                    break;
                case 1:
                    parent.btnManualScreen.IsEnabled = true;
                    parent.btnSettingsScreen.IsEnabled = true;
                    parent.btnHistoryScreen.IsEnabled = true;
                    parent.SpeedBar.IsEnabled = true;

                    parent.btnLogin2_Icon.Foreground = (SolidColorBrush)parent.FindResource("AccentColorBrush");
                    parent.btnLogin2_Text.Foreground = (SolidColorBrush)parent.FindResource("AccentColorBrush");
                    parent.btnLogin2_Text.Text = "Maintenance";
                    break;
                case 2:
                    parent.btnManualScreen.IsEnabled = true;
                    parent.btnSettingsScreen.IsEnabled = true;
                    parent.btnHistoryScreen.IsEnabled = true;
                    parent.SpeedBar.IsEnabled = true;

                    parent.btnLogin2_Icon.Foreground = (SolidColorBrush)parent.FindResource("AccentColorBrush");
                    parent.btnLogin2_Text.Foreground = (SolidColorBrush)parent.FindResource("AccentColorBrush");
                    parent.btnLogin2_Text.Text = "Engineer";
                    break;
            }
        }
        public void DisplayPage(UserControl userControl)
        {
            for (int index = 0; index < Buttons.Count; index++)
            {
                if (userControl.Name.Contains(PageName[index]))
                {
                    Indicators[index].Visibility = System.Windows.Visibility.Visible;
                    Buttons[index].Foreground = (SolidColorBrush)parent.FindResource("AccentColorBrush");
                }
                else
                {
                    Indicators[index].Visibility = System.Windows.Visibility.Hidden;
                    Buttons[index].Foreground = (SolidColorBrush)parent.FindResource("PrimaryHueMidForegroundBrush");
                }
            }

            if (parent.PageSlot.Children.Count == 0)
            {
                parent.PageSlot.Children.Add(userControl);
            }
            else
            {
                parent.PageSlot.Children.Remove(parent.PageSlot.Children[0]);
                parent.PageSlot.Children.Add(userControl);
            }
        }

        public void InitialLanguage()
        {
            parent.btnMainScreen.Content = LanguageHandler.GetMessageResource("MainScreen");
            parent.btnManualScreen.Content = LanguageHandler.GetMessageResource("ManualScreen");
            parent.btnSettingsScreen.Content = LanguageHandler.GetMessageResource("SettingsScreen");
            parent.btnHistoryScreen.Content = LanguageHandler.GetMessageResource("HistoryScreen");
            parent.lbSpeed.Text = LanguageHandler.GetMessageResource("speed");
        }
    }
}
