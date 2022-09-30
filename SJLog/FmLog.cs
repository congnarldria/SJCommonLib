using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using SJCommonLib;
using System.Threading;
using System.IO;
using System.Collections.Concurrent;
using Microsoft.VisualBasic;
using System.Diagnostics;

namespace SJLog
{
    public partial class FmLog : Form
    {
        [DllImport("user32.dll", EntryPoint = "FindWindow", CharSet = CharSet.Auto)]
        private static extern IntPtr FindWindow(string lpClassName, string lpWindowName);
        public List<TLogContent> LogList = new List<TLogContent>();
        private string LogPath = string.Empty;
        const int WM_COPYDATA = 0x4A;
        public FmLog()
        {
            InitializeComponent();
            if (!Directory.Exists(Application.StartupPath + "\\Log\\"))
                Directory.CreateDirectory(Application.StartupPath + "\\Log\\");
            LogPath = Application.StartupPath + "\\Log\\";
        }
        //[System.Security.Permissions.PermissionSet(System.Security.Permissions.SecurityAction.Demand, Name = "FullTrust")]
        protected override void WndProc(ref Message m)
        {
            if (m.Msg == WM_COPYDATA)
            {
                string strCmdLine = string.Empty;

                COPYDATASTRUCT cds = new COPYDATASTRUCT();
                cds = (COPYDATASTRUCT)Marshal.PtrToStructure(m.LParam, typeof(COPYDATASTRUCT));
                if (cds.cbData > 0)
                {
                    strCmdLine = Marshal.PtrToStringAnsi(cds.lpData).Substring(0, cds.cbData / 2);
                    Task.Run(() => Addlog(strCmdLine));
                }
            }
            base.WndProc(ref m);
        }
        private StreamWriter sw = null;
        string LastDate = string.Empty;
        private void Addlog(string log)
        {
            if (InvokeRequired)
            {
                this.Invoke((Action)(() => Addlog(log)));
            }
            else
            {
                TLogContent lc = new TLogContent();

                string[] Sp = new string[] { "," };
                string[] logs = log.Split(Sp, StringSplitOptions.None);
                lc.Category = logs[0];
                lc.Date = DateTime.Now.ToString("yyyyMMdd");
                lc.Time = DateTime.Now.ToString("HH:mm:ss.ffff");
                lc.FileName = logs[2];
                lc.Function = logs[3];
                lc.Line = logs[4];
                lc.Content = logs[1];
                if (!File.Exists(LogPath + lc.Date.Replace("/", string.Empty) + ".csv"))
                {
                    string ToTitle = ("Category" + "," + "Date" + "," + "Time" + "," + "File" + "," + " Function" + "," + "Line" + "," + "Content");
                    sw = new StreamWriter(LogPath + lc.Date.Replace("/", string.Empty) + ".csv", true, Encoding.Unicode);
                    ToTitle = ToTitle.Replace("\r\n", " ↲ ");
                    sw.WriteLine(ToTitle);
                    sw.Close();
                }
                if (LogList.Count > 6000) LogList.Clear();
                LogList.Insert(0, lc);
                string ToLine = (lc.Category + "," + lc.Date + "," + lc.Time + "," + lc.FileName + lc.Function + "," + lc.Line + "," + lc.Content);

                sw = new StreamWriter(LogPath + lc.Date.Replace("/", string.Empty) + ".csv", true, Encoding.Unicode);
                ToLine = ToLine.Replace("\r\n", " ↲ ");
                sw.WriteLine(ToLine);
                sw.Close();
                LastDate = lc.Date;
                bsLogList.ResetBindings(false);
            }
        }
        private void FmLog_Load(object sender, EventArgs e)
        {

            bsLogList.DataSource = LogList;
            notifyIconSystem.Visible = true;
            this.Hide();
            this.ShowInTaskbar = false;
        }

        private void notifyIconSystem_DoubleClick(object sender, EventArgs e)
        {
            this.TopMost = true;
            this.Show();
            this.TopMost = false;
            topMostToolStripMenuItem.Checked = false;
            this.WindowState = FormWindowState.Normal;
        }

        private void FmLog_Resize(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Minimized)
            {
                notifyIconSystem.Visible = true;
                this.Hide();
            }
        }
        private void FmLog_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            notifyIconSystem.Visible = true;
            this.Hide();
            return;
        }
        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TopMost = false;
            topMostToolStripMenuItem.Checked = false;
            topMostToolStripMenuItem.CheckState = CheckState.Unchecked;
            notifyIconSystem.Visible = false;
            string Psw = Interaction.InputBox("離開", "輸入密碼0000");
            if (Psw == "0000")
            {
                Environment.Exit(1);
            }
        }

        private void topMostToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (TopMost == false)
            {
                this.TopMost = true;
                topMostToolStripMenuItem.Checked = true;
                topMostToolStripMenuItem.CheckState = CheckState.Checked;
            }
            else
            {
                TopMost = false;
                topMostToolStripMenuItem.Checked = false;
                topMostToolStripMenuItem.CheckState = CheckState.Unchecked;
            }
        }

        private void clearToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LogList.Clear();
            bsLogList.ResetBindings(false);
        }
    }
}
