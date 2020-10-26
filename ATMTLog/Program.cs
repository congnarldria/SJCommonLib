using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ATMTLog
{
    static class Program
    {
        private static System.Threading.Mutex mutex;
        /// <summary>
        /// 應用程式的主要進入點。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            mutex = new System.Threading.Mutex(true, "ATMTLog");
            if (mutex.WaitOne(0, false))
                Application.Run(new FmLog());
            else
                Application.Exit();
        }
    }
}
