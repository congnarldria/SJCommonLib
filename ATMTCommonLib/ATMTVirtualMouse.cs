using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Runtime.InteropServices;
using System.Windows.Input;
using System.Windows.Forms;
using System.Diagnostics;
using System.Drawing;

namespace ATMTCommonLib
{
    /// <summary>
    /// 虛擬鍵盤滑鼠移動按下
    /// </summary>
    public static class ATMTVirtualMouse
    {
        [DllImport("user32.dll")]
        private static extern void mouse_event(UInt32 dwFlags, UInt32 dx, UInt32 dy, UInt32 dwData, IntPtr dwExtraInfo);
        [DllImport("user32.DLL", EntryPoint = "keybd_event")]
        private static extern void keybd_event(byte bVk, byte bScan, int dwFlags, int dwExtraInfo);

        [DllImport("user32.DLL", EntryPoint = "keybd_event")]
        private static extern void keybd_event(Keys bVk, byte bScan, int dwFlags, int dwExtraInfo);

        [DllImport("user32.DLL", EntryPoint = "GetForegroundWindow")]
        private static extern IntPtr GetForegroundWindow();

        [DllImport("user32.DLL", EntryPoint = "SetForegroundWindow")]
        private static extern bool SetForegroundWindow(IntPtr hWnd);

        [DllImport("user32.dll", EntryPoint = "FindWindow")]
        private extern static IntPtr FindWindow(string lpClassName, string lpWindowName);

        [DllImport("user32.dll", EntryPoint = "FindWindowEx")]
        private static extern IntPtr FindWindowEx(IntPtr hwndParent, IntPtr hwndChildAfter, string lpszClass, string lpszWindow);

        [DllImport("user32.dll", EntryPoint = "FindWindowEx")]
        private static extern IntPtr FindWindowEx(IntPtr hwndParent, int hwndChildAfter, string lpszClass, string lpszWindow);

        [DllImport("user32.dll", EntryPoint = "SendMessage")]
        private static extern int SendMessage(IntPtr hwnd, int wMsg, IntPtr wParam, System.Text.StringBuilder lParam);

        [DllImport("user32.dll", EntryPoint = "SendMessage")]
        private static extern int SendMessage(IntPtr hwnd, int wMsg, IntPtr wParam, string lParam);

        [DllImport("user32.dll", EntryPoint = "SendMessage")]
        private static extern int SendMessage(IntPtr hwnd, int wMsg, int wParam, int lParam);
        [DllImport("user32.dll", EntryPoint = "ClipCursor")]
        private static extern long ClipCursor(RECT rec);
        [DllImport("user32.dll", EntryPoint = "ClipCursor")]
        private static extern long ClipCursor(Rectangle rec);
        [DllImport("user32.dll", EntryPoint = "ShowCursor")]
        private static extern long ShowCursor(long show);
        /// <summary>
        /// in screen coordinates
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        [DllImport("user32.dll", EntryPoint = "SetCursorPos")]
        public static extern long SetCursorPos(long x, long y);
        [DllImport("user32.dll", EntryPoint = "GetWindowRect")]
        private static extern long GetWindowRect(IntPtr hdl, out RECT rec);
        /// <summary>
        /// RECT
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        public struct RECT
        {
            /// <summary>
            /// Left
            /// </summary>
            public int Left;
            /// <summary>
            /// Top
            /// </summary>
            public int Top;
            /// <summary>
            /// Right
            /// </summary>
            public int Right;
            /// <summary>
            /// Bottom
            /// </summary>
            public int Bottom;

        }
        #region VirtualMouse
        private const UInt32 MouseEventLeftDown = 0x0002;
        private const UInt32 MouseEventLeftUp = 0x0004;
        private const UInt32 MouseEventRightDown = 0x0008;
        private const UInt32 MouseEventRightUp = 0x0010;
        /// <summary>
        /// LeftClick
        /// </summary>
        /// <param name="posX"></param>
        /// <param name="posY"></param>
        public static void LeftClick(UInt32 posX, UInt32 posY)
        {
            mouse_event(MouseEventLeftDown, posX, posY, 0, new System.IntPtr());
            mouse_event(MouseEventLeftUp, posX, posY, 0, new System.IntPtr());
        }
        /// <summary>
        /// RightClick
        /// </summary>
        /// <param name="posX"></param>
        /// <param name="posY"></param>
        public static void RightClick(UInt32 posX, UInt32 posY)
        {
            mouse_event(MouseEventRightDown, posX, posY, 0, new System.IntPtr());
            mouse_event(MouseEventRightUp, posX, posY, 0, new System.IntPtr());
        }
        /// <summary>
        /// 將滑鼠移動範圍鎖定在Control內
        /// </summary>
        /// <param name="ctl">要鎖定的控制項</param>
        /// <returns>是否鎖定成功</returns>
        public static bool LockCursor(Control ctl)
        {
            RECT rec = new RECT();
            long res;
            GetWindowRect(ctl.Handle, out rec);
            res = ClipCursor(rec);
            if (res == 1) return true;
            else
                return false;
        }
        /// <summary>
        /// 解鎖滑鼠移動範圍
        /// </summary>
        public static void UnLockCursor()
        {
            ClipCursor(Screen.PrimaryScreen.Bounds);
        }
        #endregion
        private const int KEYEVENTF_KEYUP = 0x0002;
        private const int KEYEVENTF_KEYDOWN = 0x0000;
        #region KeyBoard
        public static void KeyClick(Control ctl, Keys key)
        {
            ctl.Focus();
            keybd_event((byte)key, 0x45, KEYEVENTF_KEYDOWN, 0);
            keybd_event((byte)key, 0x45, KEYEVENTF_KEYUP, 0);
        }
        /// <summary>
        /// KeyDown
        /// </summary>
        /// <param name="ctl"></param>
        /// <param name="key"></param>
        public static void KeyDown(Control ctl, Keys key)
        {
            ctl.Focus();
            keybd_event((byte)key, 0x45, KEYEVENTF_KEYDOWN, 0);
        }
        /// <summary>
        /// KeyUp
        /// </summary>
        /// <param name="ctl"></param>
        /// <param name="key"></param>
        public static void KeyUp(Control ctl, Keys key)
        {
            ctl.Focus();
            keybd_event((byte)key, 0x45, KEYEVENTF_KEYUP, 0);
        }
        #endregion

        #region Progma
        /// <summary>
        /// RunApp
        /// </summary>
        /// <param name="AppName"></param>
        public static void RunApp(string AppName)
        {
            try
            {
                Process poc = Process.Start(AppName);
                IntPtr hwnd = poc.Handle;
                SetForegroundWindow(hwnd);//設置App的焦點
            }
            catch (Exception e)
            {
                LogMgr.SendLog(e.Message, e);
            }
        }
        #endregion

        //        SendMessage(Button.Handle, WM_LBUTTONDOWN, 0, 0); //鼠標左鍵按下 

        //        SendMessage(Button.Handle, WM_LBUTTONUP, 0, 0); //鼠標左鍵抬起

        //        SendMessage(Edit.Handle, WM_SETTEXT, 255, Integer(PChar('abc'))); //傳遞文本

        //          SendMessage(Edit.Handle, WM_Char, Wparam('Q'), 2); //傳遞字符
            
        //          SendMessage(Button.Handle, BM_SETSTYLE, BS_RADIOBUTTON, 1); //改變Button風格

        //        SendMessage(ComboBox.Handle, CB_SETDROPPEDWIDTH, 300, 0); //改變CBDownWidth

        //                        KEY    CODE       KEY     CODE          KEY         CODE          KEY                CODE

        //                        A　　　65　　   0 　　96 　　　　F1 　　112 　　Backspace 　　　8 
        //　　　　　　B　　　66　　   1　　 97 　　　　F2 　　113　　 Tab 　　　　　　9 
        //　　　　　　C　　　67 　　  2     　98 　  　  　F3 　　114　　  Clear 　　　　　12 
        //　　　　　　D　　　68　　　3　　 99 　　　　F4 　　115　　Enter 　　　　　13 
        //　　　　　　E　　　69 　　  4 　　100　　　　F5 　　116　　Shift　　　　　 16 
        //　　　　　　F　　　70 　　  5 　　101　　　　F6 　　117　　Control 　　　　17 
        //　　　　　　G　　　71 　　  6　　 102　　　　F7 　　118 　　Alt 　　　　　　18 
        //　　　　　　H　　　72 　　　7 　　103　 　　F8 　　119　　Caps Lock 　　　20 
        //                         I　　　73 　　　8 　　104　　　　F9 　　120　　Esc 　　　　　　27 
        //　　　　　　J　　　74 　　　9　　 105　　　　F10　　121　　Spacebar　　　　32 
        //　　　　　　K　　　75 　　　* 　　106　  　　F11　　122　　Page Up　　　　 33 
        //                        L　　　76 　　　+ 　　107　　  　F12　　123　　Page Down 　　　34 
        //                        M　　　77 　　　Enter 108　　　　-- 　　--　　　End 　　　　　　35 
        //　　　　　　N　　　78 　　　-　　 109　　　　-- 　　-- 　　　Home　　　　　　36 
        //　　　　　　O　　　79 　　　. 　　110　　　　--　　 -- 　　 　Left Arrow　　　37 
        //                        P　　　80 　　　/ 　　111　　　　--　　 -- 　　 　Up Arrow　　　　38 
        //                        Q　　　81 　　　-- 　　--　　　 　--　　 -- 　　 Right Arrow 　　39 
        //                        R　　　82 　　　-- 　　--　　　　--　　 -- 　　   Down Arrow 　　 40 
        //                        S　　　83 　　　-- 　　--　　　　　-- 　　-- 　　 　Insert 　　　　 45 
        //　　　　　　T　　　84 　　　-- 　　--　　　　　--　　 -- 　　 　Delete 　　　　 46 
        //　　　　　　U　　　85 　　　-- 　　--　　　 　-- 　　-- 　　 　Help 　　　　　 47 
        //　　　　　　V　　　86 　　　--　　 --　　　　-- 　　-- 　　 　Num Lock 　　　 144 
        //                       W　　　87 　　　　　　　　　
        //　　　　　　X　　　88 　　　　　
        //　　　　　　Y　　　89 　　　　　
        //　　　　　　Z　　　90 　　　　　
        //　　　　　　0　　　48 　　　　　
        //　　　　　　1　　　49 　　　　　
        //　　　　　　2　　　50 　　　　　　
        //　　　　　　3　　　51 　　　　　　
        //　　　　　　4　　　52 　　　　　　
        //　　　　　　5　　　53 　　　　　　
        //　　　　　　6　　　54 　　　　　　
        //　　　　　　7　　　55 　　　　　　
        //　　　　　　8　　　56 　　　　　　
        //　　　　　　9　　　57 　
    }
}
