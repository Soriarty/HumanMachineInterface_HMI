﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Runtime.InteropServices;
using TwinCAT.Ads;
using System.Collections;
using System.Windows;
using System.IO;
using System.ComponentModel;

namespace HM_Interface_Visu.Classes
{
    public static class AdsCommunication
    {
        private static TcAdsClient TwinCat3Client;
        private static ArrayList NotificationHandles;
        private static int hVar;
        public static event PropertyChangedEventHandler PropertyChanged;
        public static TcAdsClient TwinCAT_Client
        {
            get
            {
                return TwinCat3Client;
            }
            set
            {
                TwinCat3Client = value;
            }
        }
        public struct Coordinate
        {
            public double X;
            public double Y;
            public double Z;
            public double C;
        }
        public struct VariableInfo
        {
            public string VarAdress;
            public Type Types;
            public object Objects;
        }
        #region Connect - Disconnect
        public static void Connect(AdsNotificationExEventHandler TwinCat3Client_AdsNotificationEx)
        {
            TwinCat3Client = new TcAdsClient();
            NotificationHandles = new ArrayList();

            try
            {
                TwinCat3Client.Connect(851);
                TwinCat3Client.AdsNotificationEx += new AdsNotificationExEventHandler(TwinCat3Client_AdsNotificationEx);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        public static void Disconnect()
        {
            try
            {
                foreach (int handle in NotificationHandles)
                    TwinCat3Client.DeleteDeviceNotification(handle);
                TwinCat3Client.Dispose();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            NotificationHandles.Clear();
            TwinCat3Client.Dispose();
        }
        #endregion
        #region Notification
        public static void RegisterNotification(VariableInfo[] NotificationData, int maxStringLenght)
        {
            try
            {
                if (NotificationData.Count() != 0)
                {
                    for (int index = 0; index <= (NotificationData.Count() - 1); index++)
                    {
                        if (NotificationData[index].Types == typeof(Int16)) AddDIntNotification(NotificationData[index].VarAdress, NotificationData[index].Objects, TwinCat3Client, NotificationHandles);
                        if (NotificationData[index].Types == typeof(Byte)) AddUIntNotification(NotificationData[index].VarAdress, NotificationData[index].Objects, TwinCat3Client, NotificationHandles);
                        if (NotificationData[index].Types == typeof(Boolean)) AddBoolNotification(NotificationData[index].VarAdress, NotificationData[index].Objects, TwinCat3Client, NotificationHandles);
                        if (NotificationData[index].Types == typeof(Single)) AddRealNotification(NotificationData[index].VarAdress, NotificationData[index].Objects, TwinCat3Client, NotificationHandles);
                        if (NotificationData[index].Types == typeof(Double)) AddLongRealNotification(NotificationData[index].VarAdress, NotificationData[index].Objects, TwinCat3Client, NotificationHandles);
                        if (NotificationData[index].Types == typeof(String)) AddStringNotification(NotificationData[index].VarAdress, NotificationData[index].Objects, TwinCat3Client, NotificationHandles, maxStringLenght);
                    }
                }
                else { throw new ArgumentException("Error in list count", "List"); }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private static void AddDIntNotification(string VariableAdress, object i_Controller, TcAdsClient adsClient, ArrayList NotifiCollection)
        {
            NotifiCollection.Add(adsClient.AddDeviceNotificationEx(VariableAdress, AdsTransMode.OnChange, 100, 0, i_Controller, typeof(Int16)));
        }
        private static void AddUIntNotification(string VariableAdress, object i_Controller, TcAdsClient adsClient, ArrayList NotifiCollection)
        {
            NotifiCollection.Add(adsClient.AddDeviceNotificationEx(VariableAdress, AdsTransMode.OnChange, 100, 0, i_Controller, typeof(Byte)));
        }
        private static void AddBoolNotification(string VariableAdress, object i_Controller, TcAdsClient adsClient, ArrayList NotifiCollection)
        {
            NotifiCollection.Add(adsClient.AddDeviceNotificationEx(VariableAdress, AdsTransMode.OnChange, 100, 0, i_Controller, typeof(Boolean)));
        }
        private static void AddRealNotification(string VariableAdress, object i_Controller, TcAdsClient adsClient, ArrayList NotifiCollection)
        {
             NotifiCollection.Add(adsClient.AddDeviceNotificationEx(VariableAdress, AdsTransMode.OnChange, 100, 0, i_Controller, typeof(Single)));
        }
        private static void AddLongRealNotification(string VariableAdress, object i_Controller, TcAdsClient adsClient, ArrayList NotifiCollection)
        {
            NotifiCollection.Add(adsClient.AddDeviceNotificationEx(VariableAdress, AdsTransMode.OnChange, 100, 0, i_Controller, typeof(Double)));
        }
        private static void AddStringNotification(string VariableAdress, object i_Controller, TcAdsClient adsClient, ArrayList NotifiCollection, int MaxChar)
        {
            NotifiCollection.Add(adsClient.AddDeviceNotificationEx(VariableAdress, AdsTransMode.OnChange, 100, 0, i_Controller, typeof(String), new int[] { MaxChar }));
        }
        #endregion
        #region Single instance Read
        public static int ReadInt(string VarAdress)
        {
            int result = 0;
            try
            {
                hVar = TwinCat3Client.CreateVariableHandle(VarAdress);
                AdsStream ADSdataStream = new AdsStream(4);
                BinaryReader binRead = new BinaryReader(ADSdataStream);
                TwinCat3Client.Read(hVar, ADSdataStream);
                ADSdataStream.Position = 0;
                result = binRead.ReadInt32();
                TwinCat3Client.DeleteVariableHandle(hVar);
                hVar = 0;
                ADSdataStream.Dispose();
                binRead.Dispose();
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);
            }
            return result;
        }
        public static byte ReadByte(string VarAdress)
        {
            byte result = 0;
            try
            {
                hVar = TwinCat3Client.CreateVariableHandle(VarAdress);
                AdsStream ADSdataStream = new AdsStream(1);
                BinaryReader binRead = new BinaryReader(ADSdataStream);
                TwinCat3Client.Read(hVar, ADSdataStream);
                ADSdataStream.Position = 0;
                result = binRead.ReadByte();
                TwinCat3Client.DeleteVariableHandle(hVar);
                hVar = 0;
                ADSdataStream.Dispose();
                binRead.Dispose();
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);
            }
            return result;
        }
        public static bool ReadBit(string VarAdress)
        {
            bool result = false;
            try
            {
                hVar = TwinCat3Client.CreateVariableHandle(VarAdress);
                AdsStream ADSdataStream = new AdsStream(1);
                BinaryReader binRead = new BinaryReader(ADSdataStream);
                TwinCat3Client.Read(hVar, ADSdataStream);
                ADSdataStream.Position = 0;
                result = binRead.ReadBoolean();
                TwinCat3Client.DeleteVariableHandle(hVar);
                hVar = 0;
                ADSdataStream.Dispose();
                binRead.Dispose();
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);
            }
            return result;
        }
        public static float ReadReal(string VarAdress)
        {
            float result = 0;
            try
            {
                hVar = TwinCat3Client.CreateVariableHandle(VarAdress);
                AdsStream ADSdataStream = new AdsStream(4);
                BinaryReader binRead = new BinaryReader(ADSdataStream);
                TwinCat3Client.Read(hVar, ADSdataStream);
                ADSdataStream.Position = 0;
                result = binRead.ReadSingle();
                TwinCat3Client.DeleteVariableHandle(hVar);
                hVar = 0;
                ADSdataStream.Dispose();
                binRead.Dispose();
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);
            }
            return result;
        }
        public static double ReadLongReal(string VarAdress)
        {
            double result = 0;
            try
            {
                hVar = TwinCat3Client.CreateVariableHandle(VarAdress);
                AdsStream ADSdataStream = new AdsStream(8);
                BinaryReader binRead = new BinaryReader(ADSdataStream);
                TwinCat3Client.Read(hVar, ADSdataStream);
                ADSdataStream.Position = 0;
                result = binRead.ReadDouble();
                TwinCat3Client.DeleteVariableHandle(hVar);
                hVar = 0;
                ADSdataStream.Dispose();
                binRead.Dispose();
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);
            }
            return result;
        }
        public static string ReadString(string VarAdress)
        {
            string result = null;
            try
            {
                hVar = TwinCat3Client.CreateVariableHandle(VarAdress);
                AdsStream ADSdataStream = new AdsStream(200);
                BinaryReader binRead = new BinaryReader(ADSdataStream, System.Text.Encoding.ASCII);
                TwinCat3Client.Read(hVar, ADSdataStream);
                ADSdataStream.Position = 0;
                result = binRead.ReadString();
                TwinCat3Client.DeleteVariableHandle(hVar);
                hVar = 0;
                ADSdataStream.Dispose();
                binRead.Dispose();
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);
            }
            return result;
        }
        public static Coordinate CoordinateRead(string VarAdress)
        {
            Coordinate ReadedCoordinate = new Coordinate();
            try
            {
                hVar = TwinCat3Client.CreateVariableHandle(VarAdress);
                AdsStream ADSdataStream = new AdsStream(8*4);
                BinaryReader binRead = new BinaryReader(ADSdataStream);
                TwinCat3Client.Read(hVar, ADSdataStream);

                ADSdataStream.Position = 0;

                ReadedCoordinate.X = binRead.ReadDouble();
                ReadedCoordinate.Y = binRead.ReadDouble();
                ReadedCoordinate.Z = binRead.ReadDouble();
                ReadedCoordinate.C = binRead.ReadDouble();

                TwinCat3Client.DeleteVariableHandle(hVar);
                hVar = 0;
                ADSdataStream.Dispose();
                binRead.Dispose();
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);
            }
            return ReadedCoordinate;
        }
        public static Coordinate[] CoordinateArrayRead(int ArraySize, string VarAdress)
        {
            Coordinate[] ReadedCoordinate = new Coordinate[ArraySize];
            try
            {
                hVar = TwinCat3Client.CreateVariableHandle(VarAdress);
                AdsStream ADSdataStream = new AdsStream(8 * 4 * ArraySize);
                BinaryReader binRead = new BinaryReader(ADSdataStream);
                TwinCat3Client.Read(hVar, ADSdataStream);

                ADSdataStream.Position = 0;
                for (int i = 0; i < ArraySize; i++)
                {
                    ReadedCoordinate[i].X = binRead.ReadDouble();
                    ReadedCoordinate[i].Y = binRead.ReadDouble();
                    ReadedCoordinate[i].Z = binRead.ReadDouble();
                    ReadedCoordinate[i].C = binRead.ReadDouble();
                }

                TwinCat3Client.DeleteVariableHandle(hVar);
                hVar = 0;
                ADSdataStream.Dispose();
                binRead.Dispose();
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);
            }
            return ReadedCoordinate;
        }
        #endregion
        #region Single instace Write
        public static bool WriteAny(string VarAdress, object Message)
        {
            bool result = false;
            try
            {
                hVar = TwinCat3Client.CreateVariableHandle(VarAdress);
                TwinCat3Client.WriteAny(hVar, Message);
                result = true;
                TwinCat3Client.DeleteVariableHandle(hVar);
                hVar = 0;
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);
            }
            return result;
        }
        #endregion
    }
}
