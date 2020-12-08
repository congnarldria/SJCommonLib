using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Runtime.InteropServices;
using System.Windows.Input;
using System.Windows.Forms;

namespace ATMTCommonLib
{
    public static class ATMTVirtualMouse
    {
        [DllImport("user32.dll")]
        private static extern void mouse_event(UInt32 dwFlags, UInt32 dx, UInt32 dy, UInt32 dwData, IntPtr dwExtraInfo);
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
    }
}
