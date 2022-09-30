using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SJCommonLib;
using System.Threading;

namespace SJMotion
{
    public class TCardADLinkMotion : PCBase
    {
        public TCardADLinkMotion()
        {
            Initial();
        }
        ~TCardADLinkMotion()
        {
            Function_Result(APS168.APS_close(), -999);
        }
        public override uint GetAxisCount()
        {
            throw new NotImplementedException();
        }

        public override bool OpenMotionBoard()
        {
            return false;
        }
        public static void Function_Result(Int32 Ret, int AxisNo)
        {
            if (Ret != 0)
            {
                LogMgr.SendLog("AxisNo" + AxisNo.ToString(), " ErrorCode  =" + ((EmErrorCode)Ret).ToString());
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
            //    if (APS168._8154_initial(ref DPAC_ID_Bits, 0))
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
    [Serializable]
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
        public int INPLogic { get; set; } = 0;
        public int EZLogic { get; set; } = 0;
        public int ServoLogic { get; set; } = 0;
        public int EZA { get; set; } = 0;
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
        public int HomeMoveSearchTarget { get; set; } = 1;
        public int HomeDir0Positive { get; set; } = 0;
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
        public EmMoveType MoveType = EmMoveType.Line;
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
        public TADLinkAxis(Int32 Nunber, double mmtopulse, EmMotorType mt = EmMotorType.Servo, EmMoveType MvT = EmMoveType.Line)
        {
            AxisNo = Nunber;
            mmToPulse = mmtopulse;
            MotorType = mt;
            MoveType = MvT;
            InitialMotionStatusDictionary();
        }
        public void Function_Result(Int32 Ret)
        {
            if (Ret != 0)
            {
                LogMgr.SendLog("AxisNo" + AxisNo.ToString(), " ErrorCode  =" + ((EmErrorCode)Ret).ToString());
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
            Function_Result(APS168.APS_set_axis_param(AxisNo, (int)APS_Define.PRA_INP_LOGIC, ActivePrm.INPLogic)); //
            Function_Result(APS168.APS_set_axis_param(AxisNo, (int)APS_Define.PRA_SERVO_LOGIC, ActivePrm.ServoLogic));
            Function_Result(APS168.APS_set_axis_param(AxisNo, (int)APS_Define.PRA_EZ_LOGIC, ActivePrm.EZLogic));
            Function_Result(APS168.APS_set_axis_param(AxisNo, 128, ActivePrm.PulseInputMode));           //Set PRA_PLS_IPT_MODE(The user must make change based on actual conditions)
            Function_Result(APS168.APS_set_axis_param(AxisNo, 129, ActivePrm.PulseOutputMode));           //Set PRA_PLS_OPT_MODE(The user must make change based on actual conditions)

            //Set Single Move Parameter
            Function_Result(APS168.APS_set_axis_param(AxisNo, (int)APS_Define.PRA_HOME_EZA, ActivePrm.EZA));            //Set EZA   
            Function_Result(APS168.APS_set_axis_param(AxisNo, (int)APS_Define.PRA_ERC_LOGIC, 0));
            //SetLegacyHomeConfig((short)AxisNo, (short)ActivePrm.HomeMode);
            Function_Result(APS168.APS_set_axis_param(AxisNo, 33, ActivePrm.Acc));      //Set Acceleration rate
            Function_Result(APS168.APS_set_axis_param(AxisNo, 34, ActivePrm.Dec));      //Set Deceleration rate
            Function_Result(APS168.APS_set_axis_param(AxisNo, 35, ActivePrm.StartVelocity));            //Set Start velocity
            Function_Result(APS168.APS_set_axis_param(AxisNo, 32, ActivePrm.Curve));            //Set S-Curve

            //Set Home Move Parameter
            //Function_Result(APS168.APS_set_axis_param(AxisNo, (int)APS_Define.PRA_HOME_SEARCH_TARGET, 1)); //0:ORG 1:EL
            Function_Result(APS168.APS_set_axis_param(AxisNo, (int)APS_Define.PRA_HOME_MODE, ActivePrm.HomeMode));             //Set Home mode (Home search 1st mode)
            Function_Result(APS168.APS_set_axis_param(AxisNo, 21, ActivePrm.HomeVelocityMax));         //Set Homing maximum Velocity (Unit:pulse/sec)            
            Function_Result(APS168.APS_set_axis_param(AxisNo, 25, ActivePrm.HomeVelocityLeave));           //Set Homing leave home Velocity (Unit:pulse/sec)
            Function_Result(APS168.APS_set_axis_param(AxisNo, 24, ActivePrm.EZCount));             //Set Specify the EZ count up Value
            Function_Result(APS168.APS_set_axis_param(AxisNo, 26, ActivePrm.ORGOffset));           //Set Homing leave home distance.Specify ORG Offset (Unit:pulse)

            //Set EMG logic
            Function_Result(APS168.APS_set_axis_param(AxisNo, 561, ActivePrm.EMGLogic));   //Select gpio input

            //Set Pulser Parameter
            //Function_Result(APS168.APS_set_axis_param(AxisNo, (Int32)APS_Define.PRA_PSR_IPT_MODE, 0));       // 0 A B phase.
            //Function_Result(APS168.APS_set_axis_param(AxisNo, (Int32)APS_Define.PRA_PSR_IPT_DIR, 0));        // No inverse
            //Function_Result(APS168.APS_set_axis_param(AxisNo, (Int32)APS_Define.PRA_PSR_PDV, 0));            //Set PDV 0
            //Function_Result(APS168.APS_set_axis_param(AxisNo, (Int32)APS_Define.PRA_PSR_PMG, 0));            //Set PMG 0
            //Function_Result(APS168.APS_set_axis_param(AxisNo, (Int32)APS_Define.PRA_PSR_HOME_TYPE, 1));      //Home move type
            //Function_Result(APS168.APS_set_axis_param(AxisNo, (Int32)APS_Define.PRA_PSR_HOME_SPD, 5000));      //Home move maximum velocity 
            LogMgr.SendLog("Axis " + AxisNo.ToString() + " SetPrm Success !!");
        }
        public void SetLegacyHomeConfig(short axisno, short homemode)
        {
            short tret = APS168._8154_set_home_config(1, homemode, (short)ActivePrm.ServoLogic, (short)ActivePrm.EZLogic, (short)ActivePrm.EZCount, (short)0);
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
            Function_Result(APS168.APS_set_axis_param(AxisNo, (Int32)APS_Define.PRA_HOME_DIR, ActivePrm.HomeDir0Positive));         //Set PRA_HOME_DIR   0:Positive 1:Negative
            Function_Result(APS168.APS_home_move(AxisNo));
            //Function_Result(APS168._8154_home_search((short)AxisNo, (double)ActivePrm.StartVelocity, (double)ActivePrm.HomeVelocityMax, (double)ActivePrm.Acc, (double)ActivePrm.ORGOffset));
            //Function_Result(APS168._8154_home_move((short)AxisNo, (double)ActivePrm.StartVelocity, (double)ActivePrm.HomeVelocityMax, (double)ActivePrm.Acc));
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
        public double PreciousCmdPos { get; set; } = 0;
        public void AbsMove(double CommandPos, double MaxSpeed)
        {
            if (MotorType != EmMotorType.Stepping)
            {
                Function_Result((APS168.APS_absolute_move(AxisNo, (int)(CommandPos * ActivePrm.MmToPulse), (int)(MaxSpeed * ActivePrm.MmToPulse))));
            }
            else
            {
                double RelDistance = CommandPos - PreciousCmdPos;
                CmdPos += RelDistance;
                Function_Result((APS168.APS_relative_move(AxisNo, (int)(RelDistance * ActivePrm.MmToPulse), (int)(MaxSpeed * ActivePrm.MmToPulse))));
                PreciousCmdPos = CmdPos;
            }
        }
        public bool MoveDone()
        {
            return INP && (MotionStatusValue & 1<<5) == 1;
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
            int Signed = 1;
            if (dir == EmDirection.Positive)
                Signed = 1;
            else
                Signed = -1;
            Function_Result(APS168.APS_velocity_move(AxisNo, (int)(Signed * MaxSpeed * mmToPulse)));
        }
        private int IoSts = 0;
        public string MotionStatus = string.Empty;
        public Int32 MotionStatusValue = 0;
        private void RefreshMotionIO()
        {
            IoSts = APS168.APS_motion_io_status(AxisNo);
            ALM = (IoSts & 0b_0000_0000_0000_0001) == 1 << 0;
            PEL = (IoSts & 0b_0000_0000_0000_0010) == 1 << 1;
            MEL = (IoSts & 0b_0000_0000_0000_0100) == 1 << 2;
            ORG = (IoSts & 0b_0000_0000_0000_1000) == 1 << 3;
            EMG = (IoSts & 0b_0000_0000_0001_0000) == 1 << 4;
            EZ = (IoSts & 0b_0000_0000_0010_0000) == 1 << 5;
            INP = (IoSts & 0b_0000_0000_0100_0000) == 1 << 6;
            SVON = (IoSts & 0b_0000_0000_1000_0000) == 1 << 7;
            RDY = (IoSts & 0b_0000_0001_0000_0000) == 1 << 8;
        }
        private void RefreshMotionStatus()
        {
            MotionStatusValue = APS168.APS_motion_status(AxisNo);
            for (int i = 0; i < MotionStatusDic.Count; i++)
            {
                if (i == 0) continue;
                if ((MotionStatusValue & 1 << i) == 1<<i)
                {
                    MotionStatus = MotionStatusDic[1 << i];
                }
            }
            //MotionStatus = MotionStatusDic[ret];
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
            NowPos = EncoderPulse / ActivePrm.MmToPulse;
            CmdPos = CommnadPulse / ActivePrm.MmToPulse;
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
                try
                {
                    RefreshMotionIO();
                    RefreshMotionStatus();
                    RefreshEncoder();
                    Thread.Sleep(1);
                }
                catch (Exception e)
                {
                    LogMgr.SendLog("Axis" + AxisNo.ToString(), e.Message, e);
                }
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
    public enum EmErrorCode : int
    {
        ERR_NoError = (0),      //No Error
        ERR_OSVersion = (-1),   // Operation System type mismatched
        ERR_OpenDriverFailed = (-2),    // Open device driver failed - Create driver interface failed
        ERR_InsufficientMemory = (-3),  // System memory insufficiently
        ERR_DeviceNotInitial = (-4),    // Cards not be initialized
        ERR_NoDeviceFound = (-5),   // Cards not found(No card in your system)
        ERR_CardIdDuplicate = (-6), // Cards' ID is duplicated. 
        ERR_DeviceAlreadyInitialed = (-7),  // Cards have been initialed
        ERR_InterruptNotEnable = (-8),  // Cards' interrupt events not enable or not be initialized
        ERR_TimeOut = (-9), // Function time out
        ERR_ParametersInvalid = (-10),  // Function input parameters are invalid
        ERR_SetEEPROM = (-11),  // Set data to EEPROM (or nonvolatile memory) failed
        ERR_GetEEPROM = (-12),  // Get data from EEPROM (or nonvolatile memory) failed
        ERR_FunctionNotAvailable = (-13),   // Function is not available in this step, The device is not support this function or Internal process failed
        ERR_FirmwareError = (-14),   // Firmware error, please reboot the system
        ERR_CommandInProcess = (-15),   // Previous command is in process
        ERR_AxisIdDuplicate = (-16),    // Axes' ID is duplicated.
        ERR_ModuleNotFound = (-17),   // Slave module not found.
        ERR_InsufficientModuleNo = (-18),   // System ModuleNo insufficiently
        ERR_HandShakeFailed = (-19),   // HandSake with the DSP out of time.
        ERR_FILE_FORMAT = (-20),    // Config file format error.(cannot be parsed)
        ERR_ParametersReadOnly = (-21), // Function parameters read only.
        ERR_DistantNotEnough = (-22),   // Distant is not enough for motion.
        ERR_FunctionNotEnable = (-23),  // Function is not enabled.
        ERR_ServerAlreadyClose = (-24), // Server already closed.
        ERR_DllNotFound = (-25),    // Related dll is not found, not in correct path.
        ERR_TrimDAC_Channel = (-26),
        ERR_Satellite_Type = (-27),
        ERR_Over_Voltage_Spec = (-28),
        ERR_Over_Current_Spec = (-29),
        ERR_SlaveIsNotAI = (-30),
        ERR_Over_AO_Channel_Scope = (-31),
        ERR_DllFuncFailed = (-32),  // Failed to invoke dll function. Extension Dll version is wrong.
        ERR_FeederAbnormalStop = (-33), //Feeder abnormal stop, External stop or feeding stop
        ERR_AreadyClose = (-34),
        ERR_NullObject = (-35), // Null object is detected
        ERR_PreMoveErr = (-36), // last move is on error stop
        ERR_PreMoveNotDone = (-37), // last move not be done
        ERR_MismatchState = (-38),  // there is a mismatch state
        ERR_Read_ModuleType_Dismatch = (-39),
        ERR_DoubleOverflow = (-40), // Double format parameter is overflow
        ERR_SlaveNumberErr = (-41),
        ERR_SlaveStatusErr = (-42),
        ERR_MapPDOOffset_TimeOut = (-43),
        ERR_Fifo_Access_Fail = (-44),
        ERR_KernelVerifyError = (-45),
        ERR_LatchFlowErr = (-46),
        ERR_NoSystemAuthority = (-47),
        ERR_KernelUpdateError = (-50), //For Kernel update
        ERR_KernelGeneralFunc = (-51),  //for general functions
        ERR_Win32Error = (-1000), // No such INT number, or WIN32_API error, contact with ADLINK's FAE staff.
        ERR_DspStart = (-2000), // The base for DSP error
    }

}
