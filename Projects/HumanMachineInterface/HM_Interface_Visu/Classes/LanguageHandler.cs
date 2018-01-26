using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Data;
using System.IO;

namespace HM_Interface_Visu.Classes
{
    public static class LanguageHandler
    {

        private static string path = AppDomain.CurrentDomain.BaseDirectory + "Config\\Language" + ConfigurationManager.AppSettings["Language"] + ".xml";
        private static DataSet ReadData(string filePath)
        {
            DataSet varData = new DataSet();
            if (File.Exists(path))
            {
                string XmlDoc = File.ReadAllText(path);
                StringReader theReader = new StringReader(XmlDoc);
                varData.ReadXml(theReader);
                theReader.Dispose();
            }
            return varData;
        }
        public static string GetMessageResource(string key)
        {
            DataSet LanguageData = ReadData(path);
            string message = null;
            for (int RowIndex = 0; RowIndex< LanguageData.Tables[0].Rows.Count; RowIndex++)
            {
                if (LanguageData.Tables[0].Rows[RowIndex][0].ToString().ToLower() == key.ToLower())
                {
                    message = LanguageData.Tables[0].Rows[RowIndex][1].ToString();
                }
            }
            return message;
        }
    }
}
