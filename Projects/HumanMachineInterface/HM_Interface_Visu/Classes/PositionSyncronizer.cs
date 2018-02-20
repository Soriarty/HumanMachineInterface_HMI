using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HM_Interface_Visu.Classes
{
    public static class PositionSyncronizer
    {
        private static System.Windows.Threading.DispatcherTimer dispatcherTimer = new System.Windows.Threading.DispatcherTimer();

        private static AdsCommunication.Coordinate CurrentCoordinate;
        private static AdsCommunication.Coordinate TargetCoordinate; 
        public static event PropertyChangedEventHandler PropertyChanged;
        public static AdsCommunication.Coordinate TargetPosition
        {
            get
            {
                return TargetCoordinate;
            }
            set
            {
                TargetCoordinate = value;
                OnPropertyChanged("TargetPosition");
            }
        }
        public static AdsCommunication.Coordinate CurrentPosition
        {
            get
            {
                return CurrentCoordinate;
            }
            set
            {
                CurrentCoordinate = value;
                OnPropertyChanged("CurrentPosition");
            }
        }

        public static void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(null, new PropertyChangedEventArgs(name));
            }
        }
        public static void StartSyncing(int mSecound)
        {
            dispatcherTimer.Tick += new EventHandler(dispatcherTimer_Tick);
            dispatcherTimer.Interval = new TimeSpan(0, 0, 0, 0, 100);
            dispatcherTimer.Start();
        }

        public static void StopSyncing()
        {
            dispatcherTimer.Stop();
        }

        private static void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            CurrentPosition = AdsCommunication.CoordinateRead(ReferenceHandler.GetReferenceAdress("CurrentPos"));
        }
    }
}
