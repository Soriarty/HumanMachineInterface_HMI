using System;
using HM_Interface_Visu.Assets.ManualScreenElements.AxisScreenElements;
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
using HM_Interface_Visu.Classes;
using MaterialDesignThemes.Wpf;
using System.Data;

namespace HM_Interface_Visu.Assets.ManualScreenElements.AxisScreenElements
{
    /// <summary>
    /// Interaction logic for PositionControll.xaml
    /// </summary>
    public partial class PositionControll : UserControl
    {
        private bool C_Axis_Exists = false;
        int MaxCoordinateNR = 1000;
        private AdsCommunication.Coordinate[] coordinate;
        private DataTable dataTable = new DataTable();
        private DateTime click_Started;
        System.Windows.Threading.DispatcherTimer dispatcherTimer = new System.Windows.Threading.DispatcherTimer();

        private void DisplayData(AdsCommunication.Coordinate coordinate, bool C_Axis)
        {
            if (C_Axis)
            {
                DataRow row = dataTable.NewRow();
                row["X"] = "0.00";
                row["Y"] = "0.00";
                row["Z"] = "0.00";
                row["C"] = "0.00";
                dataTable.Rows.Add(row);
                PositionGrid.ItemsSource = dataTable.DefaultView;
            }
            else
            {
                DataRow row = dataTable.NewRow();
                row["X"] = "0.00";
                row["Y"] = "0.00";
                row["Z"] = "0.00";
                dataTable.Rows.Add(row);
                PositionGrid.ItemsSource = dataTable.DefaultView;                
            }
            
        }
        private void SetGridStlye(bool C_Axis)
        {
            int count = 3;
            if (C_Axis) count++;

            for(int index = 0; index < count; index++)
            {
                PositionGrid.Columns[index].HeaderStyle = FindResource("HeaderStyleResource") as Style;
                PositionGrid.Columns[index].CanUserResize = false;
                PositionGrid.Columns[index].CanUserReorder = false;

                PositionGrid.Columns[index].CellStyle = FindResource("CellStyleResource") as Style;
            }
            PositionGrid.RowStyle = FindResource("RowStyleResource") as Style;
        }
        private void generate_columns(bool C_Axis)
        {
            dataTable.Columns.Add("X");
            dataTable.Columns.Add("Y");
            dataTable.Columns.Add("Z");

            if (C_Axis)
            {
                dataTable.Columns.Add("C");
            }

        }
        public PositionControll()
        {
            InitializeComponent();

            coordinate = new AdsCommunication.Coordinate[MaxCoordinateNR];

            dispatcherTimer.Tick += new EventHandler(dispatcherTimer_Tick);
            dispatcherTimer.Interval = new TimeSpan(0, 0, 0, 0, 100);

            generate_columns(false);
            DisplayData(coordinate[0], false);
            PositionGrid.CanUserAddRows = false;
            PositionGrid.CanUserDeleteRows = false;
        }
        private void SyncCoord()
        {

        }
        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            SavePos.Content = (10-Math.Round((DateTime.Now - click_Started).TotalSeconds, 0)).ToString();
            if (SavePos.Content as string == "0")
            {
                dispatcherTimer.Stop();
                SavePos.Content = "Save";
                SavePos.Foreground = FindResource("AccentColorBrush") as SolidColorBrush;
            }
        }
        private async void LoadPos_Click(object sender, RoutedEventArgs e)
        {
            SyncCoord();
            var ParameterInput = new ParameterInputBox();
            ParameterInput.Message.Text = LanguageHandler.GetMessageResource("ContinousSpeed");
            await DialogHost.Show(ParameterInput, "RootDialog");

            if (ParameterInput.result)
            {
                if ((ParameterInput.Parameter.Text != null) && (ParameterInput.Parameter.Text != ""))
                {
                    if ((int.Parse(ParameterInput.Parameter.Text) > 0 && int.Parse(ParameterInput.Parameter.Text) <= 1000))
                    {
                        if (C_Axis_Exists)
                        {

                        }
                        else
                        {

                        }
                    }
                }
            }
        }

        private void SavePos_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            SavePos.Foreground = (FindResource("PrimaryHueLightBrush") as SolidColorBrush);
            click_Started = DateTime.Now;
            dispatcherTimer.Start();
        }

        private void SavePos_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if ((DateTime.Now - click_Started).TotalSeconds > 10)
            {
                dispatcherTimer.Stop();
                SavePos.Content = "Save";
                SavePos.Foreground = FindResource("AccentColorBrush") as SolidColorBrush;
            }
            else
            {
                dispatcherTimer.Stop();
                SavePos.Content = "Save";
            }
        }

        private void PositionGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            PositionGrid.SelectedIndex = (-1);
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            SetGridStlye(false);
        }
    }

}
