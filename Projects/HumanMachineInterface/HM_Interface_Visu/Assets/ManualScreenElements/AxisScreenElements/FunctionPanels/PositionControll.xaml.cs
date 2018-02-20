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
        private int CurrentlySelected = -1;
        int MaxCoordinateNR = 1000;
        private AdsCommunication.Coordinate[] coordinate;
        private DataTable dataTable = new DataTable();
        private DateTime click_Started;
        System.Windows.Threading.DispatcherTimer dispatcherTimer = new System.Windows.Threading.DispatcherTimer();

        private void DisplayData(AdsCommunication.Coordinate Single_coordinate, bool C_Axis)
        {
            if (dataTable.Rows.Count != 0)
            {
                dataTable.Clear();
                PositionGrid.ItemsSource = null;
            }
            if (C_Axis)
            {
                DataRow row = dataTable.NewRow();
                row["X"] = Math.Round(Single_coordinate.X, 2).ToString();
                row["Y"] = Math.Round(Single_coordinate.Y, 2).ToString();
                row["Z"] = Math.Round(Single_coordinate.Z, 2).ToString();
                row["C"] = Math.Round(Single_coordinate.C, 2).ToString();
                dataTable.Rows.Add(row);
                PositionGrid.ItemsSource = dataTable.DefaultView;
            }
            else
            {
                DataRow row = dataTable.NewRow();
                row["X"] = Math.Round(Single_coordinate.X, 2).ToString();
                row["Y"] = Math.Round(Single_coordinate.Y, 2).ToString();
                row["Z"] = Math.Round(Single_coordinate.Z, 2).ToString();
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
            coordinate = AdsCommunication.CoordinateArrayRead(MaxCoordinateNR, ReferenceHandler.GetReferenceAdress("PositionArray"));
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
            ParameterInput.Message.Text = LanguageHandler.GetMessageResource("LoadPositionMSG");
            await DialogHost.Show(ParameterInput, "RootDialog");

            if (ParameterInput.result)
            {
                if ((ParameterInput.Parameter.Text != null) && (ParameterInput.Parameter.Text != ""))
                {
                    if ((int.Parse(ParameterInput.Parameter.Text) >= 0 && int.Parse(ParameterInput.Parameter.Text) <= MaxCoordinateNR))
                    {
                        CurrentlySelected = int.Parse(ParameterInput.Parameter.Text);
                        SelectedPosition.Text = LanguageHandler.GetMessageResource("selectedPosition") + ParameterInput.Parameter.Text;
                        SavePos.IsEnabled = true;
                        PositionSyncronizer.TargetPosition = coordinate[int.Parse(ParameterInput.Parameter.Text)];
                        DisplayData(coordinate[int.Parse(ParameterInput.Parameter.Text)], C_Axis_Exists);
                        SetGridStlye(C_Axis_Exists);
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
                if (CurrentlySelected != -1)
                {
                    coordinate[CurrentlySelected] = PositionSyncronizer.CurrentPosition;
                    PositionSyncronizer.TargetPosition = coordinate[CurrentlySelected];
                    AdsCommunication.WriteAny(ReferenceHandler.GetReferenceAdress("PositionArray"), coordinate);
                }

                DisplayData(coordinate[CurrentlySelected], C_Axis_Exists);
                SetGridStlye(C_Axis_Exists);

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
            SelectedPosition.Text = LanguageHandler.GetMessageResource("plsSelectPos");
            SetGridStlye(C_Axis_Exists);
        }
    }

}
