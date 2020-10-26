using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using ATMTCommonLib;
using System.Reflection;
using System.Diagnostics;
using System.Threading;
using System.Runtime.ExceptionServices;
using System.Reflection;

namespace ATMTCommonLib
{


    [StructLayout(LayoutKind.Sequential)]
    public struct COPYDATASTRUCT
    {
        public IntPtr dwData;
        public int cbData;
        public IntPtr lpData;
    }

    public class LogMgr
    { 
        public LogMgr()
        {
            AppDomain.CurrentDomain.FirstChanceException += FirstChanceHandler;
        }
        private const int WM_COPYDATA = 0x4A;
        [DllImport("USER32.DLL", EntryPoint = "SendMessageW", SetLastError = true, CharSet = CharSet.Unicode, ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        public static extern long SendMessage(IntPtr hWnd, int Msg, IntPtr wParam, IntPtr lParam);
        [DllImport("user32.dll", EntryPoint = "FindWindow", CharSet = CharSet.Auto)]
        private static extern IntPtr FindWindow(string lpClassName, string lpWindowName);
        private static StackTrace st = new StackTrace(true);
        private static Exception ex = null;
        
        public static void SendLog<T>(T cc, string log)
        {
            StackFrame[] sfs = st.GetFrames();
            int Line = sfs[2].GetFileLineNumber();
            string Function = sfs[2].GetMethod().Name;
            IntPtr maindHwnd = FindWindow(null, "ATMTLog");
            string strSend = cc.ToString() + "," + log + "," + Function + "," + Line + cc.ToString() + "," + log + "," + Function + "," + Line;
            IntPtr ptr = Marshal.StringToHGlobalAnsi(strSend);
            COPYDATASTRUCT cds = new COPYDATASTRUCT();
            cds.dwData = IntPtr.Zero;
            cds.cbData = strSend.Length;
            cds.lpData = ptr;
            IntPtr structurePtr;
            unsafe
            {
                int sSize = sizeof(COPYDATASTRUCT);
                structurePtr = Marshal.AllocHGlobal(sSize);
            }
            Marshal.StructureToPtr(cds, structurePtr, true);
            long result = SendMessage(maindHwnd, WM_COPYDATA, IntPtr.Zero, structurePtr);
            Marshal.FreeHGlobal(ptr);
            Marshal.FreeHGlobal(structurePtr);
            ex = null;
        }
        private static void FirstChanceHandler(object source, FirstChanceExceptionEventArgs e)
        {
            Thread.Sleep(10);
            ex = e.Exception;
            Trace.WriteLine(ex.Message);
        }
    }
}
