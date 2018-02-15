using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;

namespace HM_Interface_Visu.Classes
{
    public class EventLogger
    {
        public enum EventType
        {
            Error,
            Warning,
            Exception,
            Info
        }
        public static void StartLogger()
        {

        }
        [MethodImpl(MethodImplOptions.Synchronized)]
        public static void RecordEvent(EventType type, string EventID, string EventData, [Optional, DefaultParameterValue(null)] Exception exception)
        {
            string SourceClass = new StackTrace().GetFrame(2).GetMethod().ReflectedType.FullName;
            string SourceMethod = new StackTrace().GetFrame(2).GetMethod().ToString();
        }
    }
}
