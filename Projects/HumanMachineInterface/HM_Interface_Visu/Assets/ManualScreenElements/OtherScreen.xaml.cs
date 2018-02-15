using HM_Interface_Visu.Assets.ManualScreenElements.OtherScreenElements;
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

namespace HM_Interface_Visu.Assets.ManualScreenElements
{
    /// <summary>
    /// Interaction logic for OtherScreen.xaml
    /// </summary>
    public partial class OtherScreen : UserControl
    {
        private Y_Page y_Page = new Y_Page();
        private X_Page x_Page = new X_Page();
        private Z_Page z_Page = new Z_Page();

        private int pageNr = 1;
        public OtherScreen()
        {
            InitializeComponent();
            this.Name = "Other";
        }
        private void DisplayPage(UserControl userControl)
        {
            if (AxisSlot.Children.Count != 0)
            {
                AxisSlot.Children.Remove(AxisSlot.Children[0]);
                AxisSlot.Children.Add(userControl);
            }
            else
            {
                AxisSlot.Children.Add(userControl);
            }
        }
        private void ChangePage(int _pageNr)
        {
            switch (_pageNr)
            {
                case 1:
                    DisplayPage(x_Page);
                    Prevoius.IsEnabled = false;
                    Next.IsEnabled = true;
                    PageName.Text = "X Axis";
                    break;
                case 2:
                    DisplayPage(y_Page);
                    Prevoius.IsEnabled = true;
                    Next.IsEnabled = true;
                    PageName.Text = "Y Axis";
                    break;
                case 3:
                    DisplayPage(z_Page);
                    Prevoius.IsEnabled = true;
                    Next.IsEnabled = false;
                    PageName.Text = "Z Axis";
                    break;
            }
        }

        private void Prevoius_Click(object sender, RoutedEventArgs e)
        {
            pageNr--;
            ChangePage(pageNr);
        }

        private void Next_Click(object sender, RoutedEventArgs e)
        {
            pageNr++;
            ChangePage(pageNr);
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            ChangePage(pageNr);
        }
    }
}
