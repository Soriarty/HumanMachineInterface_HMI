﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
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
        #region Public field
        public static int CurrentUserAccesVar;
        public static int CurrentUserAcces
        {
            get
            {
                return CurrentUserAccesVar;
            }
            set
            {
                CurrentUserAccesVar = value;
                OnPropertyChanged("CurrentPosition");
            }
        }
        public static event PropertyChangedEventHandler PropertyChanged;
        #endregion
        public static void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(null, new PropertyChangedEventArgs(name));
            }
        }
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
                CurrentUserAccesVar = int.Parse(resultUserData[2]);
                return int.Parse(resultUserData[2]);
            }
            else
            {
                CurrentUserAccesVar = 0;
                return 0;
            }
        }
        #endregion
    }
}
