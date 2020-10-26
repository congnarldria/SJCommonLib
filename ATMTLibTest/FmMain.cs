using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ATMTCommonLib;
using System.Diagnostics;

namespace ATMTLibTest
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
            int Level = fmLogIn.atmtLogIn1.GetAuthorityLevel();
        }
        enum logs {App }
        private static StackTrace st = new StackTrace(new StackFrame(true));
        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                throw new Exception("xxxxx");
                LogMgr.SendLog(logs.App, textBox1.Text);
            }
            catch(Exception ex)
            {
                LogMgr.SendLog(logs.App, textBox1.Text , ex);
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
