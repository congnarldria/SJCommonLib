using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ATMTCommonLib
{
    public partial class ATMTComboBox : UserControl
    {
        public enum EAlign { left , center , right}
        public ATMTComboBox()
        {
            InitializeComponent();
        }
        [Browsable(true)]
        public EAlign Align { get; set; }
        [Browsable(true)]
        public event Action ACC
        {
            add
            {
                ACC += value;
            }
            remove
            {
                ACC -= value;
            }
        }
    }
}
