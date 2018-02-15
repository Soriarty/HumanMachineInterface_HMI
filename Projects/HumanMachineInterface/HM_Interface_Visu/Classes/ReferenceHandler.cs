using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwinCAT.Ads;

namespace HM_Interface_Visu.Classes
{
    public static class ReferenceHandler
    {
        public static AdsCommunication.VariableInfo[] CommunicationData;
        public static object GetReferenceObject(string RefName)
        {
            AdsCommunication.VariableInfo ResultVariableInfo = Array.Find(CommunicationData, s => s.Objects.ToString() == RefName);
            return ResultVariableInfo.Objects;
        }
        public static string GetReferenceAdress(string RefName)
        {
            string result = null;
            for (int index = 0; index <= CommunicationData.Length; index++)
            {
                if (CommunicationData[index].Objects.ToString().ToLower() == RefName.ToLower()) { result = CommunicationData[index].VarAdress; break; }
            }
            return result;
        }
        private static int GetIndexOf(string RefName)
        {
            int index = -1;
            bool isValid = false;
            for (index = 0; index <= CommunicationData.Length; index++)
            {
                if (CommunicationData[index].Objects.ToString().ToLower() == RefName.ToLower()) { isValid = true; break; }
            }
            if (isValid) return index;
            else return -1;
        }

        public static void ConnectRoutineADS(AdsNotificationExEventHandler eventHandler)
        {
            CommunicationData = ReadControlData.GetVariable();
            AdsCommunication.Connect(eventHandler);
            AdsCommunication.RegisterNotification(CommunicationData, 256);
        }
    }
}
