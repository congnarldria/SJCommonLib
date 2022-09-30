using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SJCommonLib;
using System.Diagnostics;

namespace SJLibTest
{
    public partial class FmMain : Form
    {
        public FmMain()
        {
            InitializeComponent();
        }
        private void FmMain_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            FmLogIn fmLogIn = new FmLogIn();
            fmLogIn.ShowDialog();
            int Level = fmLogIn.SJLogIn1.GetAuthorityLevel();
        }
        enum logs {App }
        private static StackTrace st = new StackTrace(new StackFrame(true));
        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
               // LogMgr.PostLog("this is post");
                LogMgr.SendLog("this is send");
                LogMgr.SendLog(logs.App, "this is Page");
                throw new Exception("xxxxx");
            }
            catch(Exception ex)
            {
                LogMgr.SendLog("this is post", ex.ToString());
            }
        }
    }
    public class TComboLList
    {
        public List<string> DropList { get; set; } = new List<string>();
        public string DropString { get; set; } = string.Empty;
        public int Selected { get; set; } = 2;
        public int ForBinding { get; set; } = 1;
    }
}
