using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HM_Interface_Visu.Classes
{
    public static class UserHandler
    {
        #region Private field
        private static string path = AppDomain.CurrentDomain.BaseDirectory + "Data\\AccesData.xml";
        #endregion

        #region Private methods
        private static DataSet ReadData(string filePath)
        {
            DataSet varData = new DataSet();
            if (File.Exists(path))
            {
                string XmlDoc = File.ReadAllText(path);
                StringReader theReader = new StringReader(XmlDoc);
                varData.ReadXml(theReader);
            }            
            return varData;
        }
        private static string[] GetReferenceData(string RefName, DataSet source)
        {
            string[] resultData = new string[3];
            DataRow UserData = null;
            for (int i = 0; i < source.Tables[0].Rows.Count; i++)
            {
                if (source.Tables[0].Rows[i][0].ToString() == RefName)
                {
                    UserData = source.Tables[0].Rows[i];
                    break;
                }
            }
            if (UserData != null)
            {
                for (int i = 0; i < 3; i++)
                {
                    resultData[i] = UserData[i].ToString();
                }
            }
            else
            {
                for (int i = 0; i < 3; i++)
                {
                    resultData[i] = "0";
                }
            }
            return resultData;
        }
        public static int SelectUserLevel(string userName, string password)
        {
            string[] resultUserData = GetReferenceData(userName, ReadData(path));
            if (userName == resultUserData[0] && password == resultUserData[1])
            {
                return int.Parse(resultUserData[2]);
            }
            else
            {
                return 0;
            }
        }
        #endregion
    }
}
