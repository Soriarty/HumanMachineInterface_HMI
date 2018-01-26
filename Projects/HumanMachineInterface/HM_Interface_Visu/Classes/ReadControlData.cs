using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HM_Interface_Visu.Classes;
using System.Data;
using System.Xml;
using System.IO;
using System.Reflection;

namespace HM_Interface_Visu.Classes
{
    public static class ReadControlData
    {
        static string path = AppDomain.CurrentDomain.BaseDirectory + "Data\\ControlData.xml";
        static DataSet loadedData = new DataSet();
        static public AdsCommunication.VariableInfo[] NotificationData;
        static int rowCount;
        private static DataSet ReadData(string filePath)
        {
            DataSet varData = new DataSet();
            if (File.Exists(path))
            {
                string XmlDoc = File.ReadAllText(path);
                StringReader theReader = new StringReader(XmlDoc);
                varData.ReadXml(theReader);               
            }
            rowCount = varData.Tables[0].Rows.Count;
            return varData;
        }
        private static void ConvertDataSetToVariableInfo()
        {
            NotificationData = new AdsCommunication.VariableInfo[rowCount];
            for (int index= 0 ; index < rowCount; index++ )
            {
                NotificationData[index].VarAdress = "HMI_Variable." + loadedData.Tables[0].Rows[index][0].ToString();
                NotificationData[index].Types = Type.GetType(loadedData.Tables[0].Rows[index][1].ToString(), true, true);
                NotificationData[index].Objects = loadedData.Tables[0].Rows[index][2];
            }
        }
        public static AdsCommunication.VariableInfo[] GetVariable()
        {
            loadedData = ReadData(path);
            ConvertDataSetToVariableInfo();
            return NotificationData;
        }
    }
}
