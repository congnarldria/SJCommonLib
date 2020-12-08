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

namespace ATMTCommonLib
{
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

        #region VirtualMouse
        private const UInt32 MouseEventLeftDown = 0x0002;
        private const UInt32 MouseEventLeftUp = 0x0004;
        private const UInt32 MouseEventRightDown = 0x0008;
        private const UInt32 MouseEventRightUp = 0x0010;

        public static void SendLeftClick(UInt32 posX, UInt32 posY)
        {
            mouse_event(MouseEventLeftDown, posX, posY, 0, new System.IntPtr());
            mouse_event(MouseEventLeftUp, posX, posY, 0, new System.IntPtr());
        }
        public static void SendRightClick(UInt32 posX, UInt32 posY)
        {
            mouse_event(MouseEventRightDown, posX, posY, 0, new System.IntPtr());
            mouse_event(MouseEventRightUp, posX, posY, 0, new System.IntPtr());
        }
        #endregion

        #region KeyBoard
        public static void KeyClick(Keys key)
        { 
            //第三個參數 0 按下 2 鬆開
            keybd_event(key, 0, 0, 0);
            keybd_event(key, 0, 2, 0);
        }
        public static void KeyDown(Keys key)
        {
            //第三個參數 0 按下 2 鬆開
            keybd_event(key, 0, 0, 0);
        }
        public static void KeyUp(Keys key)
        {
            //第三個參數 0 按下 2 鬆開
            keybd_event(key, 0, 2, 0);
        }
        #endregion

        #region Proma
        public static void RunApp(string AppName)
        {
            try
            {
                Process poc = Process.Start(AppName);
                IntPtr hwnd = poc.Handle;
                SetForegroundWindow(hwnd);//設置App的焦點
            }
            catch(Exception e)
            {
                LogMgr.SendLog(e.Message, e);
            }
        }
        #endregion


        //                        KEY    CODE       KEY     CODE          KEY         CODE          KEY                        KEYCODE

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
        //                         P　　　80 　　　/ 　　111　　　　--　　 -- 　　 　Up Arrow　　　　38 
        //                          Q　　　81 　　　-- 　　--　　　 　--　　 -- 　　 Right Arrow 　　39 
        //                          R　　　82 　　　-- 　　--　　　　--　　 -- 　　   Down Arrow 　　 40 
        //                          S　　　83 　　　-- 　　--　　　　　-- 　　-- 　　 　Insert 　　　　 45 
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
