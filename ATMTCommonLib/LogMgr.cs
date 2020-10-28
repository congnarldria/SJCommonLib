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

        }
        private const int WM_COPYDATA = 0x4A;
        [DllImport("USER32.DLL", EntryPoint = "SendMessageW", SetLastError = true, CharSet = CharSet.Unicode, ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        private static extern long SendMessage(IntPtr hWnd, int Msg, IntPtr wParam, IntPtr lParam);
        //异步消息发送API
        [DllImport("user32.dll", EntryPoint = "PostMessage")]
        public static extern int PostMessage(IntPtr hWnd, int Msg, IntPtr wParam, IntPtr lParam);
        [DllImport("user32.dll", EntryPoint = "FindWindow", CharSet = CharSet.Auto)]
        private static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

        private static StackTrace st = new StackTrace(true);
        private static Exception ex = null;
        private static void PostLogMethod(string log, Exception e = null)
        {
            StackFrame[] sfs = st.GetFrames();

            int Line = 0;
            string IError = string.Empty;
            log = log.Replace(",", "+");
            log = log.Replace(@"/r/n", "||");
            string Function = string.Empty;
            if (e != null)
            {
                StackFrame[] efs = new StackTrace(e, true).GetFrames();
                Line = new StackTrace(e, true).GetFrame(efs.Length - 1).GetFileLineNumber();
                Function = new StackTrace(e, true).GetFrame(efs.Length - 1).GetMethod().Name;
                IError = "Err:";
            }
            else
            {
                Line = sfs[2].GetFileLineNumber();
                IError = "No:";
                Function = sfs[2].GetMethod().Name;
            }

            IntPtr maindHwnd = FindWindow(null, "ATMTLog");
            string strSend = "NoUse" + "," + log + "," + Function + "," + IError + Line + "NoUse" + "," + log + "," + Function + "," + IError + Line;
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
            long result = PostMessage(maindHwnd, WM_COPYDATA, IntPtr.Zero , structurePtr);
            Marshal.FreeHGlobal(ptr);
            Marshal.FreeHGlobal(structurePtr);
            ex = null;
        }
        private static void SendLogMethod(string log, Exception e = null)
        {
            StackFrame[] sfs = st.GetFrames();

            int Line = 0;
            string IError = string.Empty;
            log = log.Replace(",", "+");
            log = log.Replace(@"/r/n", "||");
            string Function = string.Empty;
            if (e != null)
            {
                StackFrame[] efs = new StackTrace(e, true).GetFrames();
                Line = new StackTrace(e, true).GetFrame(efs.Length - 1).GetFileLineNumber();
                Function = new StackTrace(e, true).GetFrame(efs.Length - 1).GetMethod().Name;
                IError = "Err:";
            }
            else
            {
                Line = sfs[2].GetFileLineNumber();
                IError = "No:";
                Function = sfs[2].GetMethod().Name;
            }

            IntPtr maindHwnd = FindWindow(null, "ATMTLog");
            string strSend = "NoUse" + "," + log + "," + Function + "," + IError + Line + "NoUse" + "," + log + "," + Function + "," + IError + Line;
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
        public static void SendLog(string log, Exception e = null)
        {
            SendLogMethod(log, e);
        }
        public static void PostLog(string log, Exception e = null)
        {
            PostLogMethod(log, e);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T">泛型(建議列舉)</typeparam>
        /// <param name="Page">使用列舉項目</param>
        /// <param name="log">要傳遞的字串</param>
        /// <param name="e">Exception</param>
        public static void SendLog<T>(T Page, string log, Exception e = null)
        {
            StackFrame[] sfs = st.GetFrames();

            int Line = 0;
            string IError = string.Empty;
            log = log.Replace(",", "+");
            log = log.Replace(@"/r/n", "||");
            string Function = string.Empty;
            if (e != null)
            {
                StackFrame[] efs = new StackTrace(e, true).GetFrames();
                Line = new StackTrace(e, true).GetFrame(efs.Length - 1).GetFileLineNumber();
                Function = new StackTrace(e, true).GetFrame(efs.Length - 1).GetMethod().Name;
                IError = "Err:";
            }
            else
            {
                Line = sfs[2].GetFileLineNumber();
                IError = "No:";
                Function = sfs[2].GetMethod().Name;
            }

            IntPtr maindHwnd = FindWindow(null, "ATMTLog");
            string strSend = Page.ToString() + "," + log + "," + Function + "," + IError + Line + Page.ToString() + "," + log + "," + Function + "," + IError + Line;
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
    }
}
