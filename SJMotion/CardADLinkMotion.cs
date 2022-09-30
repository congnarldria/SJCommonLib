using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ATMTCommonLib;
using System.Threading;

namespace ATMTMotion
{
    public class TCardADLinkMotion : PCBase
    {
        public TCardADLinkMotion()
        {
            Initial();
        }
        ~TCardADLinkMotion()
        {
            Function_Result(APS168.APS_close());
        }
        public override uint GetAxisCount()
        {
            throw new NotImplementedException();
        }

        public override bool OpenBoard1203L()
        {
            return false;
        }

        public override bool OpenBoardADLinkPCI8154()
        {
            return false;
        }
        public static void Function_Result(Int32 Ret)
        {
            if (Ret != 0)
            {
                LogMgr.SendLog("Function Fail, ErrorCode  " + Ret.ToString());
            }
        }

        Int16 CardID = 0;	      //Card number for setting.
        Int32 Start_Axis_ID = 0;  //First Axis number in Motion Net bus.
        public Int32 TotalCard = 0;
        public void Initial()
        {
            Int32 DPAC_ID_Bits = 0;
            Int32 Info = 0;
            Int32 AxisNo = 0;
            Int32 i = 0;
            if (APS168.APS_initial(ref DPAC_ID_Bits, 0) == 0)
            {
                for (i = 0; i < 16; i++)
                {
                    if ((DPAC_ID_Bits & (1 << i)) == 1)

                        TotalCard = TotalCard + 1;
                }
            }
            else
            {
                LogMgr.SendLog("Initail Card Fail!");
            }
        }
    }
    public class TADLinkAxisPrm
    {
        public TADLinkAxisPrm()
        {

        }
        public TADLinkAxisPrm(double mtp)
        {
            MmToPulse = mtp;
        }
        public int Index { get; set; } = 0;
        public string AxisName { get; set; } = string.Empty;
        public double MmToPulse { get; set; } = 1000;
        public double SingleMoveSpeed { get; set; } = 10;
        public int LimitLogic { get; set; } = 0;
        public int ALMLogic { get; set; } = 0;
        /// <summary>
        /// 0: OUT/DIR
        ///1: CW/CCW
        ///2: 1x AB phase
        ///3: 2x AB phase
        ///4: 4x AB
        /// </summary>
        public int PulseInputMode { get; set; } = 3;
        /// <summary>
        /// 0: OUT/DIR (AL,H+)
        ///1: OUT/DIR(AH, H+)
        ///2: OUT/DIR(AL, L+)
        ///3: OUT/DIR(AH, L+)
        ///4: CW/CCW(AH)
        ///5: CW/CCW(AL)
        ///6: AB(Out Leading)
        ///7: AB(Out Lagging)
        /// </summary>
        public int PulseOutputMode { get; set; } = 4;
        //Set Single Move Parameter==================================================
        public int Acc { get; set; } = 1000000;
        public int Dec { get; set; } = 1000000;
        public int StartVelocity { get; set; } = 0;
        /// <summary>
        ///  0 : T
        ///  1: S
        /// </summary>
        public int Curve { get; set; } = 0;
        //===================================================================
        //Set Home Move Parameter==================================================
        /// <summary>
        /// Home Mode 0 => ORG + EZ?
        /// Home Mode 1 => MEL + EZ?
        /// Home Mode 2 => PEL + EZ?
        /// </summary>
        public int HomeMode { get; set; } = 0;
        public int HomeVelocityMax { get; set; } = 10000; //(Unit:pulse/sec)  
        public int HomeVelocityLeave { get; set; } = 152; //(Unit:pulse/sec)
        public int EZCount { get; set; } = 0;
        public int ORGOffset { get; set; } = 100;
        //Set EMG logic==========================================================
        /// <summary>
        /// Set GPIO Input
        /// </summary>
        public int EMGLogic { get; set; } = 0;
        //Set Pulser Parameter======================================================

    }
    public class TADLinkAxis
    {
        public EmMotorType MotorType = EmMotorType.Servo;
        public TADLinkAxisPrm ActivePrm = new TADLinkAxisPrm();
        //Motion IO  bit Table
        //0 ALM
        //1 PEL
        //2 MEL
        //3 ORG
        //4 EMG
        //5 EZ
        //6 INP
        //7 SVON
        //8 RDY
        public bool ALM { get; set; } = false;
        public bool PEL { get; set; } = false;
        public bool MEL { get; set; } = false;
        public bool ORG { get; set; } = false;
        public bool EMG { get; set; } = false;
        public bool EZ { get; set; } = false;
        public bool INP { get; set; } = false;
        public bool SVON { get; set; } = false;
        public bool RDY { get; set; } = false;
        private Int32 AxisNo = 0;
        private double mmToPulse = 1000;
        public TADLinkAxis(Int32 Nunber, double mmtopulse, EmMotorType mt = EmMotorType.Servo)
        {
            AxisNo = Nunber;
            mmToPulse = mmtopulse;
            MotorType = mt;
            InitialMotionStatusDictionary();
        }
        public static void Function_Result(Int32 Ret)
        {
            if (Ret != 0)
            {
                LogMgr.SendLog("Function Fail, ErrorCode  " + Ret.ToString());
                //FunctionFail = true;
            }
            else
            {
                //FunctionFail = false;
            }
        }
        private Dictionary<Int32, string> MotionStatusDic = new Dictionary<int, string>();
        private void InitialMotionStatusDictionary()
        {
            MotionStatusDic.Add(1 << 0, "Command stop signal");
            MotionStatusDic.Add(1 << 1, "At maximum velocity");
            MotionStatusDic.Add(1 << 2, "In acceleration");
            MotionStatusDic.Add(1 << 3, "In deceleration");
            MotionStatusDic.Add(1 << 4, "(Last)Moving direction");
            MotionStatusDic.Add(1 << 5, "Normal stop(Motion done)");
            MotionStatusDic.Add(1 << 6, "In home operation");
            MotionStatusDic.Add(1 << 7, " Single axis move");
            MotionStatusDic.Add(1 << 8, " Linear interpolation");
            MotionStatusDic.Add(1 << 9, "Circular interpolation");
            MotionStatusDic.Add(1 << 10, "At start velocity");
            MotionStatusDic.Add(1 << 11, "Point table move");
            MotionStatusDic.Add(1 << 12, "Point table dwell move");
            MotionStatusDic.Add(1 << 13, "Point table pause state");
            MotionStatusDic.Add(1 << 14, "Slave axis move");
            MotionStatusDic.Add(1 << 15, "Jog move");
            MotionStatusDic.Add(1 << 16, "Abnormal stop");
            MotionStatusDic.Add(1 << 17, "Servo off stopped.");
            MotionStatusDic.Add(1 << 18, "EMG / SEMG stopped");
            MotionStatusDic.Add(1 << 19, " Alarm stop");
            MotionStatusDic.Add(1 << 20, "Warning stopped");
            MotionStatusDic.Add(1 << 21, "PEL stopped");
            MotionStatusDic.Add(1 << 22, "MEL stopped");
            MotionStatusDic.Add(1 << 23, "Error counter check level reaches and stopped");
            MotionStatusDic.Add(1 << 24, "Soft PEL stopped");
            MotionStatusDic.Add(1 << 25, "Soft MEL stopped");
            MotionStatusDic.Add(1 << 26, "Stop by others axes");
            MotionStatusDic.Add(1 << 27, "Gantry deviation error level reaches and stopped");
            MotionStatusDic.Add(1 << 28, "Gantry mode turn on");
            MotionStatusDic.Add(1 << 29, "Pulsar mode turn on");
        }
        public void SetPrm(EmSpeed Speed)
        {
            //Function_Result(APS168.APS_set_servo_on(AxisNo, 1));                  //servo ON            
            Function_Result(APS168.APS_set_axis_param(AxisNo, 0, ActivePrm.LimitLogic));             //PEL/MEL input logic
            Function_Result(APS168.APS_set_axis_param(AxisNo, 4, ActivePrm.ALMLogic));             //Set PRA_ALM_LOGIC           
            Function_Result(APS168.APS_set_axis_param(AxisNo, 128, ActivePrm.PulseInputMode));           //Set PRA_PLS_IPT_MODE(The user must make change based on actual conditions)
            Function_Result(APS168.APS_set_axis_param(AxisNo, 129, ActivePrm.PulseOutputMode));           //Set PRA_PLS_OPT_MODE(The user must make change based on actual conditions)

            //Set Single Move Parameter
            Function_Result(APS168.APS_set_axis_param(AxisNo, 33, ActivePrm.Acc));      //Set Acceleration rate
            Function_Result(APS168.APS_set_axis_param(AxisNo, 34, ActivePrm.Dec));      //Set Deceleration rate
            Function_Result(APS168.APS_set_axis_param(AxisNo, 35, ActivePrm.StartVelocity));            //Set Start velocity
            Function_Result(APS168.APS_set_axis_param(AxisNo, 32, ActivePrm.Curve));            //Set S-Curve

            //Set Home Move Parameter
            Function_Result(APS168.APS_set_axis_param(AxisNo, 16, ActivePrm.HomeMode));             //Set Home mode (Home search 1st mode)
            Function_Result(APS168.APS_set_axis_param(AxisNo, 21, ActivePrm.HomeVelocityMax));         //Set Homing maximum Velocity (Unit:pulse/sec)            
            Function_Result(APS168.APS_set_axis_param(AxisNo, 25, ActivePrm.HomeVelocityLeave));           //Set Homing leave home Velocity (Unit:pulse/sec)           
            Function_Result(APS168.APS_set_axis_param(AxisNo, 24, ActivePrm.EZCount));             //Set Specify the EZ count up Value          
            Function_Result(APS168.APS_set_axis_param(AxisNo, 26, ActivePrm.ORGOffset));           //Set Homing leave home distance.Specify ORG Offset (Unit:pulse)

            //Set EMG logic
            Function_Result(APS168.APS_set_axis_param(AxisNo, 561, ActivePrm.EMGLogic));   //Select gpio input

            //Set Pulser Parameter
            Function_Result(APS168.APS_set_axis_param(AxisNo, (Int32)APS_Define.PRA_PSR_IPT_MODE, 0));       // 0 A B phase.
            Function_Result(APS168.APS_set_axis_param(AxisNo, (Int32)APS_Define.PRA_PSR_IPT_DIR, 0));        // No inverse
            Function_Result(APS168.APS_set_axis_param(AxisNo, (Int32)APS_Define.PRA_PSR_PDV, 0));            //Set PDV 0
            Function_Result(APS168.APS_set_axis_param(AxisNo, (Int32)APS_Define.PRA_PSR_PMG, 0));            //Set PMG 0
            Function_Result(APS168.APS_set_axis_param(AxisNo, (Int32)APS_Define.PRA_PSR_HOME_TYPE, 1));      //Home move type
            Function_Result(APS168.APS_set_axis_param(AxisNo, (Int32)APS_Define.PRA_PSR_HOME_SPD, 5000));      //Home move maximum velocity 
            LogMgr.SendLog("Axis " + AxisNo.ToString() + " SetPrm Success !!");
        }
        public void ServoOn()
        {
            Function_Result(APS168.APS_set_servo_on(AxisNo, 1));
        }
        public void ServoOff()
        {
            Function_Result(APS168.APS_set_servo_on(AxisNo, 0));
        }
        public void Home(EmDirection Dir)
        {
            Function_Result(APS168.APS_set_axis_param(AxisNo, (Int32)APS_Define.PRA_HOME_DIR, (int)Dir));		  //Set PRA_HOME_DIR   0:Positive 1:Negative
            Function_Result(APS168.APS_home_move(AxisNo));
        }
        public void Home(double Angle)
        {
            int Encoder = (int)(mmToPulse * Angle);
            Function_Result(APS168.APS_set_position(AxisNo, Encoder));
        }
        public bool HomeDone()
        {
            return ORG;
        }
        public void Stop()
        {
            Function_Result(APS168.APS_stop_move(AxisNo));
        }
        public void EmgStop()
        {
            Function_Result(APS168.APS_emg_stop(AxisNo));
        }
        public void AbsMove(double CmdPos, double MaxSpeed)
        {
            Function_Result((APS168.APS_absolute_move(AxisNo, (int)(CmdPos * mmToPulse), (int)(MaxSpeed * mmToPulse))));
        }
        public bool MoveDone()
        {
            return INP;
        }
        public void ResetCmd(int Cmd = 0)
        {
            Function_Result(APS168.APS_set_command(AxisNo, Cmd));
        }
        public void ResetEncoder(int Encoder = 0)
        {
            Function_Result(APS168.APS_set_position(AxisNo, Encoder));
        }
        public void AlarmReset()
        {

        }
        public void Jog(EmDirection dir, double MaxSpeed)
        {
            if (dir == EmDirection.Positive)
                Function_Result(APS168.APS_set_command(AxisNo, 10000000));
            else
                Function_Result(APS168.APS_set_command(AxisNo, -10000000));
            Function_Result(APS168.APS_velocity_move(AxisNo, (int)(MaxSpeed * mmToPulse)));
        }
        private Int32 IoSts = 0;
        public string MotionStatus = string.Empty;
        private void RefreshMotionIO()
        {
            IoSts = APS168.APS_motion_status(AxisNo);
            ALM = (IoSts & 0b_0000_0000_0000_0001) == 1;
            PEL = (IoSts & 0b_0000_0000_0000_0010) == 1;
            MEL = (IoSts & 0b_0000_0000_0000_0100) == 1;
            ORG = (IoSts & 0b_0000_0000_0000_1000) == 1;
            EMG = (IoSts & 0b_0000_0000_0001_0000) == 1;
            EZ = (IoSts & 0b_0000_0000_0010_0000) == 1;
            INP = (IoSts & 0b_0000_0000_0100_0000) == 1;
            SVON = (IoSts & 0b_0000_0000_1000_0000) == 1;
            RDY = (IoSts & 0b_0000_0001_0000_0000) == 1;
        }
        private void RefreshMotionStatus()
        {
            Int32 ret = APS168.APS_motion_status(AxisNo);
            MotionStatus = MotionStatusDic[ret];
        }
        public double NowPos = 0;
        public double CmdPos = 0;
        private void RefreshEncoder()
        {
            int EncoderPulse = 0;
            int CommnadPulse = 0;
            if (MotorType != EmMotorType.Stepping)
                Function_Result(APS168.APS_get_position(AxisNo, ref EncoderPulse));
            Function_Result(APS168.APS_get_command(AxisNo, ref CommnadPulse));
            NowPos = EncoderPulse / mmToPulse;
            CmdPos = CommnadPulse / mmToPulse;
        }
        private bool m_Polling = false;
        private Thread PollingThread;
        private AutoResetEvent PollngStopEvent = new AutoResetEvent(false);
        public void StartPolling()
        {
            if (!m_Polling)
            {
                m_Polling = true;
                PollingThread = new Thread(PollingContext);
                PollingThread.IsBackground = true;
                PollingThread.Start();
            }
            else
            {
                LogMgr.SendLog("Polling Now");
            }
        }
        private void PollingContext()
        {
            while (m_Polling)
            {
                RefreshMotionIO();
                RefreshMotionStatus();
                RefreshEncoder();
                Thread.Sleep(1);
            }
            PollngStopEvent.Set();
        }
        public void StopPolling()
        {
            if (m_Polling)
            {
                m_Polling = false;
                Thread.Sleep(1);
                PollngStopEvent.WaitOne(1000);
            }
            else
            {
                LogMgr.SendLog("Not In Polling");
            }
        }
    }
    public enum EmSpeed : int { Low = 0, Normal, High }
    public enum EmDirection : int { Positive = 0, Negative }
}
