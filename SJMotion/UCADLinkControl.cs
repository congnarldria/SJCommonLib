using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SJMotion
{
    public partial class UCADLinkControl : UserControl
    {
        private bool _Active = false;
        public bool Active
        {
            get
            {
                return _Active;
            }
            set
            {
                _Active = value;
            }
        }
        public TADLinkAxis Axis = null;
        public UCADLinkControl()
        {
            InitializeComponent();
        }
        public void UIRefresh()
        {
            if (Axis.MotorType != EmMotorType.Stepping)
                nudNowPosRead.Value = (decimal)Axis.NowPos;
            nudComPosRead.Value = (decimal)Axis.CmdPos;
            textBoxCurState.Text = Axis.MotionStatus;
            if (Axis.ALM)
                lbALARM.BackColor = Color.Red;
            else
                lbALARM.BackColor = Color.LightGray;
            if (Axis.EMG)
                lbEMG.BackColor = Color.Red;
            else
                lbEMG.BackColor = Color.LightGray;
            if (Axis.INP)
                lbINP.BackColor = Color.Lime;
            else
                lbINP.BackColor = Color.LightGray;
            if (Axis.ORG)
                lbORG.BackColor = Color.Lime;
            else
                lbORG.BackColor = Color.LightGray;
            if (Axis.SVON)
                lbSVON.BackColor = Color.Lime;
            else
                lbSVON.BackColor = Color.LightGray;
            if (Axis.RDY)
                lbReady.BackColor = Color.Lime;
            else
                lbReady.BackColor = Color.LightGray;
            if (Axis.PEL)
                lbPEL.BackColor = Color.Red;
            else
                lbPEL.BackColor = Color.LightGray;
            if (Axis.MEL)
                lbNEL.BackColor = Color.Red;
            else
                lbNEL.BackColor = Color.LightGray;
        }
        public void MorotorIsRotate()
        {
            lbAcc.Text = "ACC(Deg/ s ^ 2)";
            lbVel.Text = "Velocity (Deg/s)";
            lbDec.Text = "Dec(Deg/ s ^ 2)";
            lbJogSpeed.Text = "Job Speed (Degs)";
            rb01.Text = "0.1 Deg";
            rb05.Text = "0.5 Deg";
            rb10.Text = "1 Deg";
            rb50.Text = "5 Deg";
        }
        public void MorotorIsSteppingNoEncoder()
        {
            JogCCW.Enabled = false;
            JogCW.Enabled = false;
            nudJogSpeed.Enabled = false;
        }
        private void btnServoOn_Click(object sender, EventArgs e)
        {
            //if (!Axis.SVON)
            Axis.ServoOn();
        }

        private void btnServoOff_Click(object sender, EventArgs e)
        {
            //if (Axis.SVON)
            Axis.ServoOff();
        }
        private void btnHome_Click(object sender, EventArgs e)
        {
            Axis.Home(EmDirection.Positive);
        }
        private void btnMove_Click(object sender, EventArgs e)
        {
            Axis.AbsMove((double)nudComPos.Value, (double)nudVelocityHigh.Value);
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
                Axis.Stop();
        }

        private void btnALMRST_Click(object sender, EventArgs e)
        {
            Axis.ResetCmd();
            Axis.ResetEncoder();
            Axis.NowPos = 0;
            Axis.CmdPos = 0;
            Axis.PreciousCmdPos = 0;
            UIRefresh();
        }

        private void btnCW_Click(object sender, EventArgs e)
        {
            if (Axis.INP)
                Axis.AbsMove((double)nudNowPosRead.Value + Step, (double)nudVelocityHigh.Value);
        }

        private void btnCCW_Click(object sender, EventArgs e)
        {
            if (Axis.INP)
                Axis.AbsMove((double)nudNowPosRead.Value - Step, (double)nudVelocityHigh.Value);
        }
        private double Step = 0.1;
        private void rb_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton rb = sender as RadioButton;
            switch (rb.Name)
            {
                case "rb01":
                    Step = 0.1;
                    break;
                case "rb05":
                    Step = 0.5;
                    break;
                case "rb10":
                    Step = 0.1;
                    break;
                case "rb50":
                    Step = 0.1;
                    break;
                default:
                    Step = 0.1;
                    break;
            }
        }
        private void btnJog_MouseDown(object sender, MouseEventArgs e)
        {
            Button btn = sender as Button;
            if (btn.Name == "JogCW")
            {
                Axis.Jog(EmDirection.Positive, (double)nudJogSpeed.Value);
            }
            else
            {
                Axis.Jog(EmDirection.Negative, (double)nudJogSpeed.Value);
            }
        }
        private void btnJog_MouseUp(object sender, MouseEventArgs e)
        {
            Axis.Stop();
        }
    }
}
