using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Advantech.Motion;
using SJCommonLib;
using System.Threading;
using System.Runtime.InteropServices;

namespace SJMotion
{

    #region CommonDefine
    /// <summary>
    /// 不解釋
    /// </summary>
    public class TAxisInfo
    {
        public double PulseToMm = 0.01;
        public string State { get; set; }
        public int IntState { get; set; } = 1;
        public Int32 EncoderPos { get; set; }
        public Int32 CommandPos { get; set; }

        public double EncoderPosMm
        {
            get
            {
                return EncoderPos * PulseToMm;
            }
        }
        public double CommandPosMm
        {
            get
            {
                return CommandPos * PulseToMm;
            }
            set
            {
                CommandPos = (int)(CommandPosMm / PulseToMm);
            }
        }
        public UInt16 NowTorque { get; set; }
        /// <summary>
        /// 力回饋%
        /// </summary>
        public UInt16 TorqueTh { get; set; }
        /// <summary>
        /// 回饋極限
        /// </summary>
        public UInt16 TorqueLimit { get; set; }
        public bool EZ { get; set; }
        public bool ORG { get; set; }
        public bool PEL { get; set; }
        public bool MEL { get; set; }
        public bool SVON { get; set; }
        public bool INP { get; set; }
        public bool RDY { get; set; }
        public bool EMG { get; set; }
        public bool ALM { get; set; }
        public bool ALARM { get; set; }

        public bool BUSY { get; set; }
        /* for smc 
           This terminal is ON during the movement of the actuator
           Caution : During the pushing operation without movement (no
            movement but the actuator generating the pushing force),
        “BUSY” is OFF.*/
        public bool SETON { get; set; }
        /* for smc 
              When the actuator is in the SETON status (the position
              information is established), this signal turns ON.
              When the position status is not established, this signal is OFF.*/
        public bool WAREA { get; set; }
        public bool AREA { get; set; }
        public bool SVRE { get; set; }
        public bool OUT0 { get; set; }
        public bool OUT1 { get; set; }
        public bool OUT2 { get; set; }
        public bool OUT3 { get; set; }
        public bool OUT4 { get; set; }
        public bool OUT5 { get; set; }
        //輸出
        public bool FLGTH { get; set; }
        public bool JOGP { get; set; }
        public bool JOGN { get; set; }
        public bool SETUP { get; set; }
        public bool RESET { get; set; }
        public bool DRIVE { get; set; }
        public bool HOLD { get; set; }
        public bool IN0 { get; set; }
        public bool IN1 { get; set; }
        public bool IN2 { get; set; }
        public bool IN3 { get; set; }
        public bool IN4 { get; set; }
        public bool IN5 { get; set; }
        public byte SMCALMMsg1 { get; set; }
        public byte SMCALMMsg2 { get; set; }
        public byte SMCALMMsg3 { get; set; }
        public byte SMCALMMsg4 { get; set; }
    }
    public class TAdvanteckDi : DI
    {
        private byte Input;
        private IntPtr DeviceHandle;
        public UInt16 SlaveIP = 0;
        public UInt16 Channel = 0;
        public TAdvanteckDi(IntPtr ptr, UInt16 IP, UInt16 Chl)
        {
            DeviceHandle = ptr;
            SlaveIP = IP;
            Channel = Chl;
        }
        public override bool Get()
        {
            return State;
        }
        private bool _State = false;
        public bool State
        {
            get
            {
                Motion.mAcm_DaqDiGetBitEx(DeviceHandle, IORing, SlaveIP, Channel, ref Input);
                return _State = Input == 1;
            }
        }
    }
    /// <summary>
    /// 不解釋
    /// </summary>
    public class TAdvanteckDo : DO
    {
        private byte Output;
        private IntPtr DeviceHandle;
        public UInt16 SlaveIP = 0;
        public UInt16 Channel = 0;
        public TAdvanteckDo(IntPtr ptr, UInt16 IP, UInt16 Chl)
        {
            DeviceHandle = ptr;
            SlaveIP = IP;
            Channel = Chl;
        }
        /// <summary>
        /// Get Do
        /// </summary>
        /// <returns></returns>
        public override bool Get()
        {
            return State;
        }
        private bool _State;
        public bool State
        {
            get
            {
                Motion.mAcm_DaqDoGetBitEx(DeviceHandle, IORing, SlaveIP, Channel, ref Output);
                return _State = Output == 1;
            }
            set
            {
                if (value)
                    Motion.mAcm_DaqDoSetBitEx(DeviceHandle, IORing, SlaveIP, Channel, 1);
                else
                    Motion.mAcm_DaqDoSetBitEx(DeviceHandle, IORing, SlaveIP, Channel, 0);
                _State = value;
            }
        }
        public override void On()
        {
            State = true;
        }
        public override void Off()
        {
            State = false;
        }
    }
    public enum EmAdvantekCommonModuleHomeMode : uint
    {
        /// <summary>
        /// 
        /// </summary>
        MODE1_Abs = 0,
        /// <summary>
        /// 
        /// </summary>
        MODE2_Lmt = 1,
        /// <summary>
        /// 
        /// </summary>
        MODE3_Ref = 2,
        /// <summary>
        /// 
        /// </summary>
        MODE4_Abs_Ref = 3,
        /// <summary>
        /// 
        /// </summary>
        MODE5_Abs_NegRef = 4,
        /// <summary>
        /// 
        /// </summary>
        MODE6_Lmt_Ref = 5,
        /// <summary>
        /// 
        /// </summary>
        MODE7_AbsSearch = 6,
        /// <summary>
        /// 
        /// </summary>
        MODE8_LmtSearch = 7,
        /// <summary>
        /// 
        /// </summary>
        MODE9_AbsSearch_Ref = 8,
        /// <summary>
        /// 
        /// </summary>
        MODE10_AbsSearch_NegRef = 9,
        /// <summary>
        /// 
        /// </summary>
        MODE11_LmtSearch_Ref = 10,
        /// <summary>
        /// 
        /// </summary>
        MODE12_AbsSearchReFind = 11,
        /// <summary>
        /// 
        /// </summary>
        MODE13_LmtSearchReFind = 12,
        /// <summary>
        /// 
        /// </summary>
        MODE14_AbsSearchReFind_Ref = 13,
        /// <summary>
        /// 
        /// </summary>
        MODE15_AbsSearchReFind_NegRef = 14,
        /// <summary>
        /// 
        /// </summary>
        MODE16_LmtSearchReFind_Ref = 15
    }
    /// <summary>
    /// Cia 402 Home Mode
    /// </summary>
    public enum EmCia402HomeMode : uint
    {
        /// <summary>
        /// Method 1: Home on Negative Limit Switch and Index Pulse
        ///Initial movement is negative if the negative limit switch is inactive. The home position is the first
        ///index pulse in the positive direction from the position where the negative limit switch becomes
        ///inactive.
        /// </summary>
        Cia402HomeMode1 = 101,
        /// <summary>
        /// Method 2: Home on Positive Limit Switch and Index Pulse
        ///Initial movement is positive if the positive limit switch is inactive. The home position is the first
        ///index pulse in the negative direction from the position where the positive limit switch becomes
        ///inactive.
        /// </summary>
        Cia402HomeMode2 = 102,
        Cia402HomeMode3 = 103,
        Cia402HomeMode4 = 104,
        Cia402HomeMode5 = 105,
        Cia402HomeMode6 = 106,
        Cia402HomeMode7 = 107,
        Cia402HomeMode8 = 108,
        Cia402HomeMode9 = 109,
        Cia402HomeMode10 = 110,
        Cia402HomeMode11 = 111,
        Cia402HomeMode12 = 112,
        Cia402HomeMode13 = 113,
        Cia402HomeMode14 = 114,
        Cia402HomeMode15 = 115,
        Cia402HomeMode16 = 116,
        Cia402HomeMode17 = 117,
        Cia402HomeMode18 = 118,
        Cia402HomeMode19 = 119,
        Cia402HomeMode20 = 120,
        Cia402HomeMode22 = 122,
        Cia402HomeMode23 = 123,
        Cia402HomeMode24 = 124,
        Cia402HomeMode25 = 125,
        Cia402HomeMode26 = 126,
        Cia402HomeMode27 = 127,
        Cia402HomeMode28 = 128,
        Cia402HomeMode29 = 129,
        Cia402HomeMode30 = 130,
        Cia402HomeMode31 = 131,
        Cia402HomeMode32 = 132,
        Cia402HomeMode33 = 133,
        Cia402HomeMode34 = 134,
        Cia402HomeMode35 = 135,
        Cia402HomeMode36 = 136,
        Cia402HomeMode37 = 137
    }

    #endregion

    #region AdvanteckStandardAxis
    /// <summary>
    /// 
    /// </summary>
    public class TAdvantekBoard : PCBase
    {
        /// <summary>
        /// Ecat 板卡共用
        /// </summary>
        /// <returns></returns>
        public override bool OpenMotionBoard()
        {
            uint DeviceNum = 0;
            uint deviceCount = 0;

            DEV_LIST[] CurAvailableDevs = new DEV_LIST[Motion.MAX_DEVICES];
            int ret = -1;
#if !Virtual
            ret = Motion.mAcm_GetAvailableDevs(CurAvailableDevs, Motion.MAX_DEVICES, ref deviceCount);
#else
            ret = (int)ErrorCode.SUCCESS;
            deviceCount = 1;
#endif
            if (ret == (int)ErrorCode.SUCCESS && deviceCount > 0)
            {
                DeviceNum = CurAvailableDevs[0].DeviceNum;
                uint Result = Motion.mAcm_DevOpen(DeviceNum, ref DeviceHandle);
                return true;
            }
            else
            {
                LogMgr.SendLog("Master 不存在 = " + ret.ToString());
                return false;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override uint GetAxisCount()
        {
            uint AxesPerDev = 0;
            uint Result = Motion.mAcm_GetU32Property(DeviceHandle, (uint)PropertyID.FT_DevAxesCount, ref AxesPerDev);
            if (Result == (int)ErrorCode.SUCCESS && AxesPerDev > 0)
            {
                return AxesPerDev;
            }
            else
            {
                LogMgr.SendLog("Master 不存在 = " + Result.ToString());
                return 0;
            }
        }
    }
    [Serializable]
    /// <summary>
    /// 軸設定
    /// </summary>
    public class TAxisSettings
    {
        public TAxisSettings()
        {

        }
        public TAxisSettings(int Index, string Name)
        {
            ID = Index;
            AxisName = Name;
        }
        //==================================================
        public int ID { get; set; }
        public string AxisName { get; set; } = string.Empty;
        /// <summary>
        /// 1 OUT/DIR
        ///2 OUT/DIR，OUT 负逻辑
        ///4 OUT/DIR，DIR 负逻辑
        ///8 OUT/DIR，OUT&DIR 负逻辑
        ///16 CW/CCW(默认值)
        ///32 CW/CCW，CW&CCW 负逻辑
        ///256 CW/CCW，OUT 负逻辑
        ///512 CW/CCW，DIR 负逻辑
        /// </summary>
        public uint PulseOutMode { get; set; } = 1;
        public uint EnablePulseReverse { get; set; } = 0;
        /// <summary>
        /// 0 1XAB
        ///1 2XAB
        ///2 4XAB(默认值 )
        ///3 CCW/CW
        /// </summary>
        public uint PulseInMode { get; set; } = 2;
        /// <summary>
        /// 0 不倒转方向 ( 默认值 )
        ///1 倒转方向
        /// </summary>
        public uint PulseInLogic { get; set; }
        public uint LimitLogic { get; set; } = 1;
        /// <summary>
        /// Cia402HomeMode1 負極限原點 = Cia402HomeMode1 = 101
        /// </summary>
        public uint HomeExMode { get; set; } = (uint)EmCia402HomeMode.Cia402HomeMode1;
        /// <summary>
        /// Card
        /// </summary>
        public uint HomeMode { get; set; } = (uint)EmAdvantekCommonModuleHomeMode.MODE2_Lmt;
        public uint HomeDir { get; set; } = 0;
        public decimal HomeVelLow { get; set; } = 2000;
        public decimal HomeVelHigh { get; set; } = 8000;
        public decimal HomeAcc { get; set; } = 10000;
        public decimal HomeDec { get; set; } = 10000;
        /// <summary>
        /// 0 : TCurve  1: SCurve
        /// </summary>
        public uint HomeJerk { get; set; } = 0;
        public uint MoveJerk { get; set; } = 0;
        public uint AxHomeCrossDistance { get; set; } = 100;
        public uint AxHomeExSwitchMode { get; set; } = 0;
        //===============P2P==================================
        public uint VelLow { get; set; } = 2000;
        public uint VelHigh { get; set; } = 8000;
        public uint Acc { get; set; } = 10000;
        public uint Dec { get; set; } = 10000;
        //public uint MaxVel { get; set; } = 2000000;
        //public uint MaxDec { get; set; } = 2000000;
        //public uint MaxAcc { get; set; } = 20000000;
        ///// <summary>
        ///// 軸的最大加加速度
        ///// </summary>
        //public uint MaxJerk { get; set; } = 200000000;
        /// <summary>
        /// PulsePerUnit
        /// </summary>
        public uint PPU { get; set; } = 1000;
        /// <summary>
        /// PPUDenominator
        /// </summary>
        public uint PPUDenominator { get; set; } = 1;
        //public bool SwMelEnable { get; set; } = true;
        //public bool SwPelEnable { get; set; } = true;
        //public uint SwMelReact { get; set; } = 0;
        //public uint SwPelReact { get; set; } = 0;
        //public int SwMelValue { get; set; } = -10000000;
        //public uint SwPelValue { get; set; } = 10000000;
        //public bool MelToleranceEnable { get; set; } = false;
        //public uint MelToleranceValue { get; set; } = 5000;
        //public bool PelToleranceEnable { get; set; } = false;
        //public uint PelToleranceValue { get; set; } = 5000;
        //public bool SwMelToleranceEnable { get; set; } = false;
        //public uint SwMelToleranceValue { get; set; } = 5000;
        //public bool SwPelToleranceEnable { get; set; } = false;
        //public uint SwPelToleranceValue { get; set; } = 5000;
        //public bool BacklashEnable { get; set; } = false;
        /// <summary>
        /// 背隙補償脈衝數
        /// </summary>
        //public uint BacklashPulses { get; set; } = 10;
        ///// <summary>
        ///// 背隙補償速度
        ///// </summary>
        //public uint BacklashVel { get; set; } = 3000;
        /// <summary>
        /// 軸旋轉360度脈衝數
        /// </summary>
        public uint ModuleRange { get; set; } = 3600;
        ///// <summary>
        ///// DI(外部訊號) Stop 的減速度
        ///// </summary>
        //public uint KillDec { get; set; } = 100000;
        /// <summary>
        /// Encoder 和 Commnad Pos的差值數 ， 超過 Alarm
        /// </summary>
        //public uint MaxErrorCnt { get; set; } = 10000;
        //SMC
        /// <summary>
        /// SMC ACC
        /// </summary>
        public uint SMCAcc { get; set; } = 10000;
        /// <summary>
        /// SMCDec
        /// </summary>
        public uint SMCDec { get; set; } = 10000;
        /// <summary>
        /// 推力移動扭力
        /// </summary>
        public ushort PushingForce { get; set; } = 70;
        /// <summary>
        /// 回饋推力門檻
        /// </summary>
        public ushort TorqueTriggerLV { get; set; } = 60;
        public ushort MovingForce { get; set; } = 100;
        /// <summary>
        /// 推力移動速度
        /// </summary>
        public ushort PushingSpeed { get; set; } = 1000;
        /// <summary>
        /// 位置移動速度
        /// </summary>
        public ushort MoveSpeed { get; set; } = 1000;
    }
    #region SMCSDODefine
    /// <summary>
    /// SDO Input Address
    /// </summary>
    public class SDOIn
    {
        /// <summary>
        /// U16
        /// </summary>
        public const ushort adiSMCINPUT = 0x6010;
        /// <summary>
        /// U16
        /// </summary>
        public const ushort adiCONTROLLERSTATE = 0x6011;
        /// <summary>
        /// U32
        /// </summary>
        public const ushort adiNowPos = 0x6020;
        /// <summary>
        /// U16
        /// </summary>
        public const ushort adiNowVelocity = 0x6021;
        /// <summary>
        /// U16
        /// </summary>
        public const ushort adiNowTorque = 0x6022;
        /// <summary>
        /// U32
        /// </summary>
        public const ushort adiTargetPos = 0x6022;
        /// <summary>
        /// U8 by 4 SubIndex 1,2,3,4
        /// </summary>
        public const ushort adiALMMessage = 0x6030;

        //輸入信號顯示
        public static ushort bCTLREADY = 0b_0000_0000_0001_0000;
        /// <summary>
        /// 數據組別_2進制輸出
        /// </summary>
        public const UInt16 bOUT0 = 0b_0000_0000_0000_0001;
        public const UInt16 bOUT1 = 0b_0000_0000_0000_0010;
        public const UInt16 bOUT2 = 0b_0000_0000_0000_0100;
        public const UInt16 bOUT3 = 0b_0000_0000_0000_1000;
        public const UInt16 bOUT4 = 0b_0000_0000_0001_0000;
        public const UInt16 bOUT5 = 0b_0000_0000_0010_0000;
        public const UInt16 bX = 0b_0000_0000_0100_0000;
        public const UInt16 bXX = 0b_0000_0000_1000_0000;
        /// <summary>
        /// 運轉中 IN
        /// </summary>
        public const UInt16 bBUSY = 0b_0000_0001_0000_0000;
        /// <summary>
        /// SV_Ready IN
        /// </summary>
        public const UInt16 bSVRE = 0b_0000_0010_0000_0000;
        /// <summary>
        /// 原點復歸完成 IN 
        /// </summary>
        public const UInt16 bSETON = 0b_0000_0100_0000_0000;
        /// <summary>
        /// 位置到達 IN
        /// </summary>
        public const UInt16 bINP = 0b_0000_1000_0000_0000;
        /// <summary>
        /// 區域到達 IN
        /// </summary>
        public const UInt16 bAREA = 0b_0001_0000_0000_0000;
        /// <summary>
        /// W區域到達
        /// </summary>
        public const UInt16 bWAREA = 0b_0010_0000_0000_0000;
        public const UInt16 bESTOP = 0b_0100_0000_0000_0000;
        public const UInt16 bALARM = 0b_1000_0000_0000_0000;
    }
    /// <summary>
    /// 輸出信號
    /// </summary>
    public class SDOOut
    {
        /// <summary>
        /// U16
        /// </summary>
        public const ushort adoSMCOUTPUT = 0;
        /// <summary>
        /// U16
        /// </summary>
        public const ushort adoControlData = 2;
        /// <summary>
        /// U8
        /// </summary>
        /// 
        public const ushort adoStartSignal = 4;
        public const byte bSTART = 0b_0000_0001;
        /// <summary>
        /// U8
        /// </summary>
        public const ushort adoActionMethod = 5;
        public const byte bABS = 0b_0000_0001;
        public const byte bREL = 0b_0000_0010;
        /// <summary>
        /// U16
        /// </summary>
        public const ushort adoSpeed = 6;
        /// <summary>
        /// U32
        /// </summary>
        public const ushort adoPosition = 8;
        /// <summary>
        /// U16
        /// </summary>
        public const ushort adoAcc = 12;
        /// <summary>
        /// U16
        /// </summary>
        public const ushort adoDec = 14;
        /// <summary>
        /// U16
        /// </summary>
        public const ushort adoFinalTorque = 16;
        /// <summary>
        /// U16
        /// </summary>
        public const ushort adoTorqueTh = 18;
        /// <summary>
        /// U16
        /// </summary>
        public const ushort adoPushSpeed = 20;
        /// <summary>
        /// U16
        /// </summary>
        public const ushort adoPosTorque = 22;
        /// <summary>
        /// U32
        /// </summary>
        public const ushort adoArea = 24;
        /// <summary>
        /// U32
        /// </summary>
        public const ushort adoWArea = 28;
        /// <summary>
        /// U32
        /// </summary>
        public const ushort adoINPWidth = 32;
        /// <summary>
        /// 數據組別_2進制輸入  OUT
        /// </summary>
        public const UInt16 bIN0 = 0;
        public const UInt16 bIN1 = 1;
        public const UInt16 bIN2 = 2;
        public const UInt16 bIN3 = 3;
        public const UInt16 bIN4 = 4;
        public const UInt16 bIN5 = 5;
        public const UInt16 bx = 6;
        public const UInt16 bxx = 7;
        /// <summary>
        /// 暫停運轉 OUT
        /// “RESET” is a signal to reset the alarm and the operation.
        ///    After “RESET”, the speed decreases at maximum
        ///    deceleration of the basic parameter until the actuator stops.
        ///“INP” and “OUT0” to “OUT5” will be turned OFF (however, if the
        ///    actuator is stopped within the in-position range, “INP” will turn ON).
        /// </summary>
        public const UInt16 bHOLD = 8;
        /// <summary>
        /// When “SVON” is OFF, turn OFF “DRIVE” and “SETUP”.
        /// </summary>
        public const UInt16 bSVON = 9;
        /// <summary>
        /// 組別啟動  OUT
        /// </summary>
        public const UInt16 bDRIVE = 10;
        /// <summary>
        /// 異常復歸  OUT
        /// </summary>
        public const UInt16 bRESET = 11;
        /// <summary>
        /// 原點復歸  OUT
        /// </summary>
        public const UInt16 bSETUP = 12;
        public const UInt16 bJOGN = 13;
        public const UInt16 bJOGP = 14;
        public const UInt16 bFLGTH = 15;
    }
    #endregion

    public sealed class TVituralAxis : TAdvanteckAxis
    {
        public override bool AbsMoveMm(double pos)
        {
            return true;
        }
    }

    /// <summary>
    /// 相容研華cia 402 標準軸
    /// </summary>
    public class TAdvanteckAxis : EcatAxis
    {
        /// <summary>
        /// Servo Brake
        /// </summary>
        public event Action WhenServoOnNotify = null;
        public event Action WhenServoOffNotify = null;
        /// <summary>
        /// 板卡Handle
        /// </summary>
        protected IntPtr m_DeviceHandle;
        /// <summary>
        /// 軸Handle
        /// </summary>
        protected IntPtr m_AxisHandle;
        /// <summary>
        /// 軸ID
        /// </summary>
        protected ushort ID = 0;
        protected uint ret;
        protected uint ret0;
        protected bool m_IsPolling = false;
        protected Thread PoollingThread = null;
        protected Thread MoveThread = null;
        protected AutoResetEvent PollingStopEvent = new AutoResetEvent(false);
        public bool IsOpen = false;
        public double mmToPulse { get; set; } = 1000;
        public TAxisInfo AxisInfo = new TAxisInfo();
        public TAxisSettings AS;
        /// <summary>
        /// 不可使用 ， 必須使用另一個
        /// </summary>

        public TAdvanteckAxis()
        {

        }
        public TAdvanteckAxis(IntPtr ptr, int Index, double PPU = 1000, double PPUDenominator = 1)
        {
            try
            {
                //yaskawa turn 1 circle, 10mm 
                m_DeviceHandle = ptr;
                ID = (ushort)Index;
                InitalAxis(PPU, PPUDenominator);
            }
            catch (Exception e)
            {
                LogMgr.SendLog(e.Message, e);
            }
        }
        /// <summary>
        /// 取得軸數
        /// </summary>
        /// <returns></returns>
        public IntPtr GetAxisHandle()
        {
            return m_AxisHandle;
        }
        private void Polling()
        {
            while (m_IsPolling)
            {
                Thread.Sleep(10);
                RefreshAxisCommandPos();
                RefreshAxisEncoderPos();
                RefreshAxisIO();
                RefreshAxisState();
            }
            PollingStopEvent.Set();
        }
        /// <summary>
        /// 不解釋
        /// </summary>
        /// <param name="ptr"></param>
        /// <param name="Index"></param>
        /// <param name="PPU"></param>
        /// <param name="PPUDenominator"></param>

        /// <summary>
        /// 不解釋
        /// </summary>
        /// <param name="PPU"></param>
        /// <param name="PPUDenominator"></param>
        public void InitalAxis(double PPU = 1000, double PPUDenominator = 1)
        {
            ret = Motion.mAcm_SetU32Property(m_AxisHandle, (UInt32)PropertyID.CFG_AxPPU, (uint)PPU);
            ret = Motion.mAcm_SetU32Property(m_AxisHandle, (UInt32)PropertyID.CFG_AxPPUDenominator, (uint)PPUDenominator);
            if (PPUDenominator != 0)
                mmToPulse = PPU / PPUDenominator;
        }
        /// <summary>
        /// 不解釋
        /// </summary>
        public override void StartPolling()
        {
            if (PoollingThread == null && !m_IsPolling)
            {
                m_IsPolling = true;
                PoollingThread = new Thread(Polling);
                PoollingThread.IsBackground = true;
                PoollingThread.Start();
            }
        }
        /// <summary>
        /// 不解釋
        /// </summary>
        public override void StopPolling()
        {
            if (m_IsPolling)
            {
                m_IsPolling = false;
                bool ret = PollingStopEvent.WaitOne(1000);
                PoollingThread = null;
                if (!ret)
                {
                    LogMgr.SendLog(ID.ToString() + "軸迴圈結束失敗");
                }
            }
        }
        /// <summary>
        /// 不解釋
        /// </summary>
        /// <returns></returns>
        public override bool Open()
        {
            try
            {
                ret = Motion.mAcm_AxOpen(m_DeviceHandle, ID, ref m_AxisHandle);
                if (ret != (uint)ErrorCode.SUCCESS)
                {
                    LogMgr.SendLog(ret.ToString());
                    return false;
                }
                IsOpen = true;
                return true;
            }
            catch (Exception e)
            {
                LogMgr.SendLog(e.Message, e);
            }
            return false;
        }
        public void SetAllPrm()
        {
            SetHomePrm();
            SetPosMovePrm();
            InitalAxis(AS.PPU, AS.PPUDenominator);
        }
        /// <summary>
        /// 寫入原點軸參數
        /// </summary>
        public override void SetHomePrm()
        {
            Motion.mAcm_SetF64Property(m_AxisHandle, (uint)PropertyID.PAR_AxVelLow, (uint)AS.HomeVelLow);
            Motion.mAcm_SetF64Property(m_AxisHandle, (uint)PropertyID.PAR_AxVelHigh, (uint)AS.HomeVelHigh);
            Motion.mAcm_SetF64Property(m_AxisHandle, (uint)PropertyID.PAR_AxAcc, (uint)AS.HomeAcc);
            Motion.mAcm_SetF64Property(m_AxisHandle, (uint)PropertyID.PAR_AxDec, (uint)AS.HomeDec);
            Motion.mAcm_SetF64Property(m_AxisHandle, (uint)PropertyID.PAR_AxHomeCrossDistance, AS.AxHomeCrossDistance); //T Curve
            Motion.mAcm_SetF64Property(m_AxisHandle, (uint)PropertyID.PAR_AxJerk, AS.HomeJerk); //T Curve
#if !ECAT
            Motion.mAcm_SetU32Property(m_AxisHandle, (uint)PropertyID.PAR_AxHomeMode, AS.HomeMode);
#else
            Motion.mAcm_SetU32Property(m_AxisHandle, (uint)PropertyID.PAR_AxHomeExMode, AS.HomeExMode);
#endif
        }
        /// <summary>
        /// 寫入移動參數
        /// </summary>
        public override void SetPosMovePrm()
        {
            Motion.mAcm_SetF64Property(m_AxisHandle, (uint)PropertyID.PAR_AxVelLow, AS.VelLow);
            Motion.mAcm_SetF64Property(m_AxisHandle, (uint)PropertyID.PAR_AxVelHigh, AS.VelHigh);
            Motion.mAcm_SetF64Property(m_AxisHandle, (uint)PropertyID.PAR_AxAcc, AS.Acc);
            Motion.mAcm_SetF64Property(m_AxisHandle, (uint)PropertyID.PAR_AxDec, AS.Dec);
            Motion.mAcm_SetF64Property(m_AxisHandle, (uint)PropertyID.PAR_AxJerk, AS.MoveJerk); //T Curve
            Motion.mAcm_SetU32Property(m_AxisHandle, (uint)PropertyID.CFG_AxPulseOutMode, AS.PulseOutMode);
            Motion.mAcm_SetU32Property(m_AxisHandle, (uint)PropertyID.CFG_AxPulseOutReverse, AS.EnablePulseReverse);
            Motion.mAcm_SetU32Property(m_AxisHandle, (uint)PropertyID.CFG_AxElLogic, AS.LimitLogic);
            Motion.mAcm_SetU32Property(m_AxisHandle, (uint)PropertyID.CFG_AxPulseInMode, AS.PulseInMode);
            Motion.mAcm_SetU32Property(m_AxisHandle, (uint)PropertyID.CFG_AxPulseInLogic, AS.PulseInLogic);
        }
        /// <summary>
        /// 不解釋
        /// </summary>
        /// <returns></returns>
        public override bool ServoOn()
        {
            try
            {
                ret = Motion.mAcm_AxSetSvOn(m_AxisHandle, 1);
                if (ret != (uint)ErrorCode.SUCCESS)
                {
                    LogMgr.SendLog(ret.ToString());
                    return false;
                }
                return true;
            }
            catch (Exception e)
            {
                LogMgr.SendLog(e.Message, e);
            }
            return false;
        }
        /// <summary>
        /// 不解釋
        /// </summary>
        /// <returns></returns>
        public override bool ServoOff()
        {
            try
            {
                ret = Motion.mAcm_AxSetSvOn(m_AxisHandle, 0);
                if (ret != (uint)ErrorCode.SUCCESS)
                {
                    LogMgr.SendLog(ret.ToString());
                    return false;
                }
                return true;
            }
            catch (Exception e)
            {
                LogMgr.SendLog(e.Message, e);
            }
            return false;
        }
        /// <summary>
        /// 不解釋
        /// </summary>
        /// <returns></returns>
        public override bool Home()
        {
            try
            {
                if (AxisInfo.IntState == 3)
                    ResetError();
                Thread.Sleep(100);
                ret = Motion.mAcm_AxHome(m_AxisHandle, AS.HomeMode, AS.HomeDir);
                if (ret != (uint)ErrorCode.SUCCESS)
                {
                    LogMgr.SendLog(ret.ToString());
                    return false;
                }
                return true;
            }
            catch (Exception e)
            {
                LogMgr.SendLog(e.Message, e);
            }
            return false;
        }
        /// <summary>
        /// 不解釋
        /// </summary>
        /// <returns></returns>
        public override bool HomeDone()
        {
            try
            {
                return AxisInfo.ORG;
            }
            catch (Exception e)
            {
                LogMgr.SendLog(e.Message, e);
            }
            return false;
        }
        /// <summary>
        /// 不解釋
        /// </summary>
        /// <returns></returns>
        public override bool ResetError()
        {
            try
            {
                ret = Motion.mAcm_AxResetError(m_AxisHandle);
                if (ret != (uint)ErrorCode.SUCCESS)
                {
                    LogMgr.SendLog(ret.ToString());
                    return false;
                }
                return true;
            }
            catch (Exception e)
            {
                LogMgr.SendLog(e.Message, e);
            }
            return false;
        }
        /// <summary>
        /// 不解釋
        /// </summary>
        /// <returns></returns>
        public override bool DecStop()
        {
            try
            {
                ret = Motion.mAcm_AxStopDec(m_AxisHandle);
                if (ret != (uint)ErrorCode.SUCCESS)
                {
                    LogMgr.SendLog(ret.ToString());
                    return false;
                }
                return true;
            }
            catch (Exception e)
            {
                LogMgr.SendLog(e.Message, e);
            }
            return false;
        }
        private UInt32 IOStatus = new UInt32();
        private  bool isServoOn = false;
        /// <summary>
        /// 不解釋
        /// </summary>
        public override void RefreshAxisIO()
        {
            try
            {
                
                ret = Motion.mAcm_AxGetMotionIO(m_AxisHandle, ref IOStatus);
                AxisInfo.EZ = (IOStatus & (uint)Ax_Motion_IO.AX_MOTION_IO_EZ) > 0;
                AxisInfo.ORG = (IOStatus & (uint)Ax_Motion_IO.AX_MOTION_IO_ORG) > 0;
                AxisInfo.PEL = (IOStatus & (uint)Ax_Motion_IO.AX_MOTION_IO_LMTP) > 0;
                AxisInfo.MEL = (IOStatus & (uint)Ax_Motion_IO.AX_MOTION_IO_LMTN) > 0;
                AxisInfo.SVON = (IOStatus & (uint)Ax_Motion_IO.AX_MOTION_IO_SVON) > 0;
                AxisInfo.INP = (IOStatus & (uint)Ax_Motion_IO.AX_MOTION_IO_INP) > 0;
                AxisInfo.RDY = (IOStatus & (uint)Ax_Motion_IO.AX_MOTION_IO_RDY) > 0;
                AxisInfo.ALM = (IOStatus & (uint)Ax_Motion_IO.AX_MOTION_IO_ALM) > 0;
                AxisInfo.ALARM = (IOStatus & (uint)Ax_Motion_IO.AX_MOTION_IO_ALRM) > 0;
                AxisInfo.EMG = (IOStatus & (uint)Ax_Motion_IO.AX_MOTION_IO_EMG) > 0;
                if (isServoOn != AxisInfo.SVON)
                {
                    if (AxisInfo.SVON)
                        WhenServoOnNotify?.Invoke();
                    else
                        WhenServoOffNotify?.Invoke();
                }
                isServoOn = AxisInfo.SVON;
            }
            catch (Exception e)
            {
                LogMgr.SendLog(e.Message, e);
            }
        }
        /// <summary>
        /// 不解釋
        /// </summary>
        public override void RefreshAxisEncoderPos()
        {
            try
            {
                double pos = 0;
                ret = Motion.mAcm_AxGetActualPosition(m_AxisHandle, ref pos);
                AxisInfo.EncoderPos = (Int32)pos;
            }
            catch (Exception e)
            {
                LogMgr.SendLog(e.Message, e);
            }
        }
        /// <summary>
        /// 不解釋
        /// </summary>
        public override void RefreshAxisCommandPos()
        {
            try
            {
                double pos = 0;
                ret = Motion.mAcm_AxGetCmdPosition(m_AxisHandle, ref pos);
                AxisInfo.CommandPos = (Int32)pos;
            }
            catch (Exception e)
            {
                LogMgr.SendLog(e.Message, e);
            }
        }
        /// <summary>
        /// 不解釋
        /// </summary>
        public override void RefreshAxisState()
        {
            try
            {
                ushort state = 0;
                ret = Motion.mAcm_AxGetState(m_AxisHandle, ref state);
                AxisInfo.State = string.Empty;
                AxisInfo.IntState = state;
                if (state == 0)
                    AxisInfo.State = "Disable";
                if (state == 1)
                    AxisInfo.State = "Ready";
                if (state == 2)
                    AxisInfo.State = "停止";
                if (state == 3)
                    AxisInfo.State = "錯誤停止";
                if (state == 4)
                    AxisInfo.State = "歸原點中...";
                if (state == 5)
                    AxisInfo.State = "定位移動中...";
                if (state == 6)
                    AxisInfo.State = "連續移動中";
                if (state == 7)
                    AxisInfo.State = "補間中...";
                if (state == 8)
                    AxisInfo.State = "外部訊號控制中...";
                if (state == 9)
                    AxisInfo.State = "外部訊號控制中...";
            }
            catch (Exception e)
            {
                LogMgr.SendLog(e.Message, e);
            }
        }
        /// <summary>
        /// 不解釋
        /// </summary>
        /// <param name="pos"></param>
        /// <returns></returns>
        public override bool AbsMoveMm(double pos)
        {
            if (AxisInfo.IntState == 3)
                ResetError();
            Thread.Sleep(100);
            ret = Motion.mAcm_AxMoveAbs(m_AxisHandle, pos);
            if (ret != (uint)ErrorCode.SUCCESS)
            {
                LogMgr.SendLog(ret.ToString());
                return false;
            }
            return true;
        }
        /// <summary>
        /// 不解釋
        /// </summary>
        /// <param name="pos"></param>
        /// <returns></returns>
        public override bool MoveDone()
        {
            return AxisInfo.INP;
        }
        /// <summary>
        /// 不解釋
        /// </summary>
        /// <returns></returns>
        public override bool JogPositive()
        {
            try
            {
                ret = Motion.mAcm_AxMoveVel(m_AxisHandle, 0);
                if (ret != (uint)ErrorCode.SUCCESS)
                {
                    LogMgr.SendLog(ret.ToString());
                    return false;
                }
                return true;
            }
            catch (Exception e)
            {
                LogMgr.SendLog(e.Message, e);
            }
            return false;
        }
        /// <summary>
        /// 不解釋
        /// </summary>
        /// <returns></returns>
        public override bool JogNegative()
        {
            try
            {
                ret = Motion.mAcm_AxMoveVel(m_AxisHandle, 1);
                if (ret != (uint)ErrorCode.SUCCESS)
                {
                    LogMgr.SendLog(ret.ToString());
                    return false;
                }
                return true;
            }
            catch (Exception e)
            {
                LogMgr.SendLog(e.Message, e);
            }
            return false;
        }
        /// <summary>
        /// 取得現在位置
        /// </summary>
        /// <returns></returns>
        public override double NowPos()
        {
            return AxisInfo.EncoderPos;
        }
    }
    #endregion

    #region SMC
    /// <summary>
    /// SMC電動缸
    /// </summary>
    public sealed class TSMCAxis : TAdvanteckAxis, ITorqueFucntion, IVelocityFucntion
    {
        private Dictionary<int, ECATType> TypeOfIndex = new Dictionary<int, ECATType>();
        /// <summary>
        /// 
        /// </summary>
        public TSMCAxis()
        {
            InitailDictionary();
        }
        /// <summary>
        ///   IO控制電動缸
        /// </summary>
        /// <param name="ptr"></param>
        /// <param name="Index"></param>
        public TSMCAxis(IntPtr ptr, int Index)
        {
            InitailDictionary();
            m_DeviceHandle = ptr;
            ID = (ushort)Index;
        }
        public override double NowPos()
        {
            return (double)AxisInfo.EncoderPos / 100.0f;
        }
        /// <summary>
        /// 
        /// </summary>
        public void Polling()
        {
            while (m_IsPolling)
            {
                Thread.Sleep(1);
                RefreshAxisCommandPos();
                RefreshAxisEncoderPos();
                RefreshAxisIO();
                RefreshAxisState();
            }
            PollingStopEvent.Set();
        }
        /// <summary>
        /// 
        /// </summary>
        public override void StartPolling()
        {
            if (PoollingThread == null)
            {
                PoollingThread = new Thread(Polling);
                PoollingThread.IsBackground = true;
                PoollingThread.Start();
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public override void StopPolling()
        {
            m_IsPolling = false;
            bool ret = PollingStopEvent.WaitOne(1000);
            PoollingThread = null;
            if (!ret)
            {
                LogMgr.SendLog(ID.ToString() + "軸迴圈結束失敗");
            }
        }
        private void InitailDictionary()
        {
            //===============================INPUT====================================
            TypeOfIndex.Add(0x6010, ECATType.TypeU16); //輸入信號顯示
            TypeOfIndex.Add(0x6011, ECATType.TypeU16); //控制器訊息
            TypeOfIndex.Add(0x6020, ECATType.TypeU32); //現在位置[0.01mm]
            TypeOfIndex.Add(0x6021, ECATType.TypeU16);//現在速度[mm/s]
            TypeOfIndex.Add(0x6022, ECATType.TypeU16);//現在推力[%]
            TypeOfIndex.Add(0x6023, ECATType.TypeU32);//目標位置[0.01mm]
            TypeOfIndex.Add(0x6030, ECATType.TypeU8);//警報訊息1 SubIndex 1,2,3,4
            //===============================OUTPUT====================================
            TypeOfIndex.Add(0x7010, ECATType.TypeU16);//輸出信號
            TypeOfIndex.Add(0x7011, ECATType.TypeU16);//控制器數據
            TypeOfIndex.Add(0x7012, ECATType.TypeU8);//啟動信號 bit 0
            TypeOfIndex.Add(0x7020, ECATType.TypeU8);//動作方法
            TypeOfIndex.Add(0x7021, ECATType.TypeU16);//速度[mm/s]
            TypeOfIndex.Add(0x7022, ECATType.TypeU32);//位置[0.01mm]
            TypeOfIndex.Add(0x7023, ECATType.TypeU16);//加速度[mm/s^2]
            TypeOfIndex.Add(0x7024, ECATType.TypeU16);//減速度[mm/s^2
            TypeOfIndex.Add(0x7025, ECATType.TypeU16);//最終推力值[%]
            TypeOfIndex.Add(0x7026, ECATType.TypeU16);//推力門檻値[%]
            TypeOfIndex.Add(0x7027, ECATType.TypeU16);//推押速度[mm/s]
            TypeOfIndex.Add(0x7028, ECATType.TypeU16);//定位推力[%]
            TypeOfIndex.Add(0x7029, ECATType.TypeU32);//區域端點1[0.01mm]
            TypeOfIndex.Add(0x702A, ECATType.TypeU32);//區域端點2[0.01mm]
            TypeOfIndex.Add(0x702B, ECATType.TypeU32);//定位寬度[0.01mm]
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override bool Open()
        {
            return true;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override bool ServoOn()
        {
            try
            {
                Setbit(SDOOut.adoSMCOUTPUT, SDOOut.bSVON);
            }
            catch (Exception e)
            {
                LogMgr.SendLog(e.Message, e);
            }
            return false;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override bool ServoOff()
        {
            try
            {
                Resetbit(SDOOut.adoSMCOUTPUT, SDOOut.bSVON);
            }
            catch (Exception e)
            {
                LogMgr.SendLog(e.Message, e);
            }
            return false;
        }
        public void SetZeroActive()
        {
            ushort EasySet = 0b0000_0000_0000_0000;
            WriteU16(SDOOut.adoControlData, EasySet);
        }
        public void SetEasyActive()
        {
            ushort EasySet = 0b0000_0000_0110_0000;
            WriteU16(SDOOut.adoControlData, EasySet);
        }
        public void SetTorqueActive()
        {
            ushort TorqueSet = 0b0001_1110_0110_0000;
            WriteU16(SDOOut.adoControlData, TorqueSet);
        }
        /// <summary>
        /// SetHomePrm
        /// </summary>
        public override void SetHomePrm()
        {
            try
            {
                ushort EasySet = 0b0000_0000_0110_0000;
                WriteU16(SDOOut.adoControlData, EasySet);
                WriteU8(SDOOut.adoActionMethod, SDOOut.bABS);
                WriteU16(SDOOut.adoAcc, (ushort)AS.SMCAcc);
                WriteU16(SDOOut.adoDec, (ushort)AS.SMCDec);
                WriteU16(SDOOut.adoPosTorque, AS.MovingForce);
                WriteU16(SDOOut.adoTorqueTh, AS.TorqueTriggerLV);
            }
            catch (Exception e)
            {
                LogMgr.SendLog(e.Message, e);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public override void SetPosMovePrm()
        {
            try
            {
                ushort EasySet = 0b0000_0000_0110_0000;
                WriteU16(SDOOut.adoControlData, EasySet);
                WriteU8(SDOOut.adoActionMethod, SDOOut.bABS);
                WriteU16(SDOOut.adoSpeed, (ushort)AS.MoveSpeed);
                WriteU32(SDOOut.adoPosition, (uint)(AxisInfo.CommandPos));
                WriteU16(SDOOut.adoAcc, (ushort)AS.SMCAcc);
                WriteU16(SDOOut.adoDec, (ushort)AS.SMCDec);
                WriteU16(SDOOut.adoPosTorque, AS.MovingForce);
                WriteU16(SDOOut.adoPushSpeed, 0);
                WriteU16(SDOOut.adoTorqueTh, 0);
                WriteU16(SDOOut.adoFinalTorque, 0);
            }
            catch (Exception e)
            {
                LogMgr.SendLog(e.Message, e);
            }
        }
        /// <summary>
        /// Torque Contorl
        /// </summary>
        public void SetPushMovePrm()
        {
            try
            {
                ushort PushSet = 0b0001_1110_0110_0000;
                WriteU16(SDOOut.adoControlData, PushSet);
                WriteU8(SDOOut.adoActionMethod, SDOOut.bABS);
                WriteU16(SDOOut.adoSpeed, (ushort)AS.MoveSpeed);
                WriteU16(SDOOut.adoPushSpeed, AS.PushingSpeed);
                WriteU32(SDOOut.adoPosition, (uint)(AxisInfo.CommandPos));
                WriteU16(SDOOut.adoAcc, (ushort)AS.Acc);
                WriteU16(SDOOut.adoDec, (ushort)AS.Dec);
                WriteU16(SDOOut.adoPosTorque, AS.MovingForce);
                WriteU16(SDOOut.adoTorqueTh, AS.TorqueTriggerLV);
                WriteU16(SDOOut.adoFinalTorque, AS.PushingForce);
            }
            catch (Exception e)
            {
                LogMgr.SendLog(e.Message, e);
            }
        }
        public void StopPushMoveMove()
        {
            STOP();
        }
        /// <summary>
        /// Return to Origin position command → Travels in the set Origin position direction →
        ///  Stops traveling → Reverse travel → Sets the Origin position
        /// </summary>
        /// <returns></returns>
        public override bool Home()
        {
            try
            {
                /*Home Flow by Setp No.
                * SVON_____________▇▇▇▇▇▇▇▇▇▇▇▇▇▇▇▇▇▇▇▇
                * SETUP__________________▇▇▇▇▇________________
                * BUSY _____________________▇▇▇▇▇▇▇▇▇__________
                * SVRE__________________▇▇▇▇▇▇▇▇▇▇▇▇▇▇▇▇▇
                * SETON________________________________▇▇▇▇▇
                * INP   __________________________________▇▇▇▇▇
                * 
                */
                unsafe
                {
                    //if (!ReadU8bit(SDOOut.bSTART, SDOOut.a    doControlData))
                    //{
                    SetZeroActive();
                    if (ReadU16bit(SDOIn.adiSMCINPUT, SDOIn.bSVRE))
                    {
                        //Resetbit(SDOOut.adoSMCOUTPUT, SDOOut.bDRIVE);
                        Setbit(SDOOut.adoSMCOUTPUT, SDOOut.bSETUP);
                        Resetbit(SDOOut.adoSMCOUTPUT, SDOOut.bSETUP);
                    }
                    //}
                }
            }
            catch (Exception e)
            {
                LogMgr.SendLog(e.Message, e);
            }
            return false;
        }
        public override bool HomeDone()
        {
            return AxisInfo.SETON;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override bool ResetError()
        {
            try
            {
                Setbit(SDOOut.adoSMCOUTPUT, SDOOut.bRESET);
                Thread.Sleep(1);
                Resetbit(SDOOut.adoSMCOUTPUT, SDOOut.bRESET);
            }
            catch (Exception e)
            {
                LogMgr.SendLog(e.Message, e);
            }
            return true;
        }
        /// <summary>
        ///  絕對移動 pos
        /// </summary>
        /// <param name="pos">絕對位置</param>
        /// <returns></returns>
        public override bool AbsMoveMm(double pos)
        {
            try
            {
                unsafe
                {
                    SetEasyActive();
                    pos = pos * 100.0f;
                    //if (!ReadU8bit(SDOOut.bSTART, SDOOut.adoControlData))
                    //{
                    if (!ReadU16bit(SDOIn.adiSMCINPUT, SDOIn.bBUSY))
                    {
                        //WriteU8(SDOOut.adoActionMethod, SDOOut.bABS);
                        WriteU32(SDOOut.adoPosition, (UInt32)pos);
                        //WriteU16(SDOOut.adoFinalTorque, 0);
                        //WriteU16(SDOOut.adoPosTorque, 0);
                        //WriteU16(SDOOut.adoTorqueTh, 0);
                        WriteU16(SDOOut.adoSpeed, 1000);
                        //Resetbit(SDOOut.adoSMCOUTPUT, SDOOut.bDRIVE);
                        SetStartbit(SDOOut.adoStartSignal, 0);
                        ResetStartbit(SDOOut.adoStartSignal, 0);
                        //Task.Run(() => TurnOffDrive());
                    }
                    //SetU8bit(SDOOut.adoControlData, SDOOut.bSTART);

                    //}
                }
            }
            catch (Exception e)
            {
                LogMgr.SendLog(e.Message, e);
            }
            return true;
        }
        public override bool MoveDone()
        {
            return AxisInfo.INP;
        }
        /// <summary>
        /// 
        /// </summary>
        private void TurnOffDrive()
        {
            long CNT = 0;
            while (ReadU16bit(SDOIn.adiSMCINPUT, SDOIn.bBUSY))
            {
                try
                {
                    Thread.Sleep(1);
                    CNT++;
                    Resetbit(SDOOut.adoSMCOUTPUT, SDOOut.bDRIVE);
                    if (CNT > 5000) break;
                }
                catch (Exception e)
                {
                    LogMgr.SendLog(e.Message, e);
                }
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="pos"></param>
        /// <param name="MoveTorque"></param>
        /// <param name="TorqeTriggerLvelPercent"></param>
        public void AbsPushMoveMm(uint pos, ushort MoveTorque, ushort TorqeTriggerLvelPercent)
        {
            try
            {
                unsafe
                {
                    SetTorqueActive();
                    if (ReadU16bit(SDOIn.adiSMCINPUT, SDOIn.bSVRE))
                    {
                        WriteU16(SDOOut.adoFinalTorque, 100);
                        WriteU16(SDOOut.adoPosTorque, 100);
                        WriteU16(SDOOut.adoTorqueTh, TorqeTriggerLvelPercent);
                        WriteU16(SDOOut.adoSpeed, 1000);
                        WriteU16(SDOOut.adoPushSpeed, 1000);
                        //WriteU8(SDOOut.adoActionMethod, SDOOut.bABS);
                        WriteU32(SDOOut.adoPosition, pos);
                        //Setbit(SDOOut.adoSMCOUTPUT, SDOOut.bDRIVE);
                        SetStartbit(SDOOut.adoStartSignal, 0);
                        ResetStartbit(SDOOut.adoStartSignal, 0);
                        //Task.Run(() => TurnOffDrive());
                    }
                }
            }
            catch (Exception e)
            {
                LogMgr.SendLog(e.Message, e);
            }
        }
        /// <summary>
        ///  UnHOLD
        /// </summary>
        public void Continue()
        {
            try
            {
                unsafe
                {
                    Resetbit(SDOOut.adoSMCOUTPUT, SDOOut.bHOLD);
                }
            }
            catch (Exception e)
            {
                LogMgr.SendLog(e.Message, e);
            }
        }
        /// <summary>
        ///  HOLD
        /// </summary>
        public void STOP()
        {
            try
            {
                unsafe
                {
                    Setbit(SDOOut.adoSMCOUTPUT, SDOOut.bHOLD);
                }
            }
            catch (Exception e)
            {
                LogMgr.SendLog(e.Message, e);
            }
        }
        /// <summary>
        /// Velocity Control
        /// </summary>
        public void SetVelocityMovePrm()
        {

        }
        /// <summary>
        /// Jog 移動
        /// </summary>
        /// <param name="Velocity"></param>
        /// <param name="Dir"></param>
        public void VelocityMove(uint Velocity, bool Dir)
        {
            if (Dir)
            {
                Setbit(SDOOut.adoSMCOUTPUT, SDOOut.bJOGP);
            }
            else
            {
                Setbit(SDOOut.adoSMCOUTPUT, SDOOut.bJOGN);
            }
        }
        /// <summary>
        /// 停止Jog
        /// </summary>
        public void StopVelocityMove()
        {
            Resetbit(SDOOut.adoSMCOUTPUT, SDOOut.bJOGP);
            Resetbit(SDOOut.adoSMCOUTPUT, SDOOut.bJOGN);
        }
        /// <summary>
        /// 
        /// </summary>
        public override void RefreshAxisCommandPos()
        {
            AxisInfo.CommandPos = (Int32)ReadU32(SDOOut.adoPosition);
        }
        /// <summary>
        /// 
        /// </summary>
        public override void RefreshAxisEncoderPos()
        {
            AxisInfo.EncoderPos = (Int32)ReadU32(SDOIn.adiNowPos);
        }
        /// <summary>
        /// 
        /// </summary>
        public override void RefreshAxisIO()
        {
            AxisInfo.ALARM = ReadU16bit(SDOIn.adiSMCINPUT, SDOIn.bALARM);
            AxisInfo.EMG = ReadU16bit(SDOIn.adiSMCINPUT, SDOIn.bESTOP);
            AxisInfo.WAREA = ReadU16bit(SDOIn.adiSMCINPUT, SDOIn.bWAREA);
            AxisInfo.AREA = ReadU16bit(SDOIn.adiSMCINPUT, SDOIn.bAREA);
            AxisInfo.INP = ReadU16bit(SDOIn.adiSMCINPUT, SDOIn.bINP);
            AxisInfo.SETON = ReadU16bit(SDOIn.adiSMCINPUT, SDOIn.bSETON);
            AxisInfo.SVRE = ReadU16bit(SDOIn.adiSMCINPUT, SDOIn.bSVRE);
            AxisInfo.BUSY = ReadU16bit(SDOIn.adiSMCINPUT, SDOIn.bBUSY);


            //AxisInfo.FLGTH = ReadU16bit(SDOOut.adoSMCOUTPUT, SDOOut.bFLGTH);
            //AxisInfo.JOGP = ReadU16bit(SDOOut.adoSMCOUTPUT, SDOOut.bJOGP);
            //AxisInfo.JOGN = ReadU16bit(SDOOut.adoSMCOUTPUT, SDOOut.bJOGN);
            //AxisInfo.SETUP = ReadU16bit(SDOOut.adoSMCOUTPUT, SDOOut.bSETUP);
            //AxisInfo.RESET = ReadU16bit(SDOOut.adoSMCOUTPUT, SDOOut.bRESET);
            //AxisInfo.DRIVE = ReadU16bit(SDOOut.adoSMCOUTPUT, SDOOut.bDRIVE);
            //AxisInfo.SVON = ReadU16bit(SDOOut.adoSMCOUTPUT, SDOOut.bSVON);
            //AxisInfo.HOLD = ReadU16bit(SDOOut.adoSMCOUTPUT, SDOOut.bHOLD);
        }
        /// <summary>
        /// 
        /// </summary>
        public override void RefreshAxisState()
        {
            byte[] AM1 = new byte[1];
            byte[] AM2 = new byte[1];
            byte[] AM3 = new byte[1];
            byte[] AM4 = new byte[1];
            IntPtr UnManagePtr1 = Marshal.AllocHGlobal(1);
            IntPtr UnManagePtr2 = Marshal.AllocHGlobal(1);
            IntPtr UnManagePtr3 = Marshal.AllocHGlobal(1);
            IntPtr UnManagePtr4 = Marshal.AllocHGlobal(1);
            ret = Motion.mAcm_DevReadSDOData(m_DeviceHandle, IORing, ID, SDOIn.adiALMMessage, 1, (ushort)TypeOfIndex[SDOIn.adiALMMessage], 1, UnManagePtr1);
            ret = Motion.mAcm_DevReadSDOData(m_DeviceHandle, IORing, ID, SDOIn.adiALMMessage, 2, (ushort)TypeOfIndex[SDOIn.adiALMMessage], 1, UnManagePtr2);
            ret = Motion.mAcm_DevReadSDOData(m_DeviceHandle, IORing, ID, SDOIn.adiALMMessage, 3, (ushort)TypeOfIndex[SDOIn.adiALMMessage], 1, UnManagePtr3);
            ret = Motion.mAcm_DevReadSDOData(m_DeviceHandle, IORing, ID, SDOIn.adiALMMessage, 4, (ushort)TypeOfIndex[SDOIn.adiALMMessage], 1, UnManagePtr4);
            Marshal.Copy(UnManagePtr1, AM1, 0, 1);
            Marshal.Copy(UnManagePtr2, AM2, 0, 1);
            Marshal.Copy(UnManagePtr3, AM3, 0, 1);
            Marshal.Copy(UnManagePtr4, AM4, 0, 1);
            AxisInfo.SMCALMMsg1 = AM1[0];
            AxisInfo.SMCALMMsg2 = AM2[0];
            AxisInfo.SMCALMMsg3 = AM3[0];
            AxisInfo.SMCALMMsg4 = AM4[0];
            Marshal.FreeHGlobal(UnManagePtr1);
            Marshal.FreeHGlobal(UnManagePtr2);
            Marshal.FreeHGlobal(UnManagePtr3);
            Marshal.FreeHGlobal(UnManagePtr4);
        }
        #region READWRITE
        private void SetU8bit(ushort IOAdress, byte Target)
        {
            try
            {
                unsafe
                {
                    IntPtr UnManagePtr = Marshal.AllocHGlobal(2);
                    byte[] DATA = new byte[1];

                    ret = Motion.mAcm_DevReadSDOData(m_DeviceHandle, IORing, ID, IOAdress, 0, (ushort)TypeOfIndex[IOAdress], 1, UnManagePtr);
                    Marshal.Copy(UnManagePtr, DATA, 0, 2);
                    DATA[0] = (byte)(DATA[0] | (byte)((Target >> 8)));
                    Marshal.Copy(DATA, 0, UnManagePtr, DATA.Length);
                    ret = Motion.mAcm_DevWriteSDOData(m_DeviceHandle, IORing, ID, IOAdress, 0, (ushort)TypeOfIndex[IOAdress], 1, UnManagePtr);
                    Marshal.FreeHGlobal(UnManagePtr);
                }
            }
            catch (Exception e)
            {
                LogMgr.SendLog(e.Message, e);
            }
        }
        private void iSetU8bit(ushort IOAdress, byte Target)
        {
            byte data = 0;
            ret = Motion.mAcm_DevReadSDOData(m_DeviceHandle, IORing, ID, IOAdress, 0, ref data);
            ret = Motion.mAcm_DevWriteSDOData(m_DeviceHandle, IORing, ID, IOAdress, 0, data | Target);
        }
        private void ResetU8bit(ushort IOAdress, byte Target)
        {
            try
            {
                unsafe
                {
                    IntPtr UnManagePtr = Marshal.AllocHGlobal(2);
                    byte[] DATA = new byte[1];

                    ret = Motion.mAcm_DevReadSDOData(m_DeviceHandle, IORing, ID, IOAdress, 0, (ushort)TypeOfIndex[IOAdress], 2, UnManagePtr);
                    Marshal.Copy(UnManagePtr, DATA, 0, 1);
                    DATA[0] = (byte)(DATA[1] & (byte)(~(Target >> 8)));
                    Marshal.Copy(DATA, 0, UnManagePtr, DATA.Length);
                    ret = Motion.mAcm_DevWriteSDOData(m_DeviceHandle, IORing, ID, IOAdress, 0, (ushort)TypeOfIndex[IOAdress], 2, UnManagePtr);
                    Marshal.FreeHGlobal(UnManagePtr);
                }
            }
            catch (Exception e)
            {
                LogMgr.SendLog(e.Message, e);
            }
        }
        private void iReSetU8bit(byte IOAdress, ushort Target)
        {
            byte data = 0;
            ret = Motion.mAcm_DevReadSDOData(m_DeviceHandle, IORing, ID, IOAdress, 0, ref data);
            ret = Motion.mAcm_DevWriteSDOData(m_DeviceHandle, IORing, ID, IOAdress, 0, data & ~Target);
        }
        public void SetStartbit(ushort IOAdress, ushort Target)
        {
            ushort data = 0;
            ret = Motion.mAcm_DevReadSDOData(m_DeviceHandle, IORing, ID, 0x7012, 0, ref data);
            ret = Motion.mAcm_DaqDoSetByteEx(m_DeviceHandle, IORing, ID, (ushort)(IOAdress), ((byte)(data | 1 << Target)));
        }
        public void ResetStartbit(ushort IOAdress, ushort Target)
        {
            ushort data = 0;
            ret = Motion.mAcm_DevReadSDOData(m_DeviceHandle, IORing, ID, 0x7012, 0, ref data);
            ret = Motion.mAcm_DaqDoSetByteEx(m_DeviceHandle, IORing, ID, (ushort)(IOAdress), ((byte)(data & ~(1 << Target))));
        }
        private void Setbit(ushort IOAdress, ushort Target)
        {
            try
            {
                bool IsUpper = false;
                ushort Offset = 0;
                if (Target > 7)
                {
                    Target = (byte)(Target % 8);
                    Offset = 1;
                    IsUpper = true;
                }
                ushort data = 0;
                if (IsUpper)
                {
                    ret = Motion.mAcm_DevReadSDOData(m_DeviceHandle, IORing, ID, 0x7010, 0, ref data);
                    byte cc = (byte)((byte)(data >> 8));
                    ret = Motion.mAcm_DaqDoSetByteEx(m_DeviceHandle, IORing, ID, (ushort)(IOAdress + Offset), (byte)((byte)(data >> 8) | 1 << Target));
                }
                else
                {
                    ret = Motion.mAcm_DevReadSDOData(m_DeviceHandle, IORing, ID, 0x7010, 0, ref data);
                    ret = Motion.mAcm_DaqDoSetByteEx(m_DeviceHandle, IORing, ID, (ushort)(IOAdress), ((byte)(data | 1 << Target)));
                }
                //ret = Motion.mAcm_DaqDoSetBitEx(m_DeviceHandle, IORing, ID, (ushort)(IOAdress + Offset), 1);
            }
            catch (Exception e)
            {
                LogMgr.SendLog(e.Message, e);
            }
        }
        private void Resetbit(ushort IOAdress, ushort Target)
        {
            try
            {
                bool IsUpper = false;
                ushort Offset = 0;
                if (Target > 7)
                {
                    Target = (byte)(Target % 8);
                    Offset = 1;
                    IsUpper = true;
                }
                ushort data = 0;
                if (IsUpper)
                {
                    ret = Motion.mAcm_DevReadSDOData(m_DeviceHandle, IORing, ID, 0x7010, 0, ref data);
                    byte cc = (byte)(data >> 8);
                    ret = Motion.mAcm_DaqDoSetByteEx(m_DeviceHandle, IORing, ID, (ushort)(IOAdress + Offset), (byte)((byte)(data >> 8) & ~(1 << Target)));
                }
                else
                {
                    ret = Motion.mAcm_DevReadSDOData(m_DeviceHandle, IORing, ID, 0x7010, 0, ref data);
                    ret = Motion.mAcm_DaqDoSetByteEx(m_DeviceHandle, IORing, ID, (ushort)(IOAdress), ((byte)(data & ~(1 << Target))));
                }
            }
            catch (Exception e)
            {
                LogMgr.SendLog(e.Message, e);
            }
        }
        private bool iReadU8bit(ushort IOAdress, byte Target)
        {
            byte data = 0;
            ret = Motion.mAcm_DevReadSDOData(m_DeviceHandle, IORing, ID, IOAdress, 0, ref data);
            return (data & Target) > 0;
        }
        private bool iReadU16bit(ushort IOAdress, byte Target)
        {
            ushort data = 0;
            ret = Motion.mAcm_DevReadSDOData(m_DeviceHandle, IORing, ID, IOAdress, 0, ref data);
            return (data & Target) > 0;
        }
        private bool ReadU16bit(ushort IOAdress, ushort Target)
        {
            try
            {
                unsafe
                {
                    IntPtr UnManagePtr = Marshal.AllocHGlobal(2);
                    byte[] DATA = new byte[2];

                    ret = Motion.mAcm_DevReadSDOData(m_DeviceHandle, IORing, ID, IOAdress, 0, (ushort)TypeOfIndex[IOAdress], 2, UnManagePtr);
                    Marshal.Copy(UnManagePtr, DATA, 0, 2);
                    ushort data = (ushort)(DATA[0] + (ushort)(DATA[1] << 8));
                    Marshal.FreeHGlobal(UnManagePtr);

                    return (data & Target) > 0;
                }
            }
            catch (Exception e)
            {
                LogMgr.SendLog(e.Message, e);
            }
            return false;
        }
        private bool ReadU8bit(ushort IOAdress, ushort Target)
        {
            try
            {
                unsafe
                {
                    IntPtr UnManagePtr = Marshal.AllocHGlobal(2);
                    byte[] DATA = new byte[1];

                    ret = Motion.mAcm_DevReadSDOData(m_DeviceHandle, IORing, ID, IOAdress, 0, (ushort)TypeOfIndex[IOAdress], 1, UnManagePtr);
                    Marshal.Copy(UnManagePtr, DATA, 0, 1);
                    ushort data = DATA[0];
                    Marshal.FreeHGlobal(UnManagePtr);
                    return (data & Target) > 0;
                }
            }
            catch (Exception e)
            {
                LogMgr.SendLog(e.Message, e);
            }
            return false;
        }
        private UInt16 ReadU16(ushort IOAdress)
        {
            try
            {
                unsafe
                {
                    IntPtr UnManagePtr = Marshal.AllocHGlobal(4);
                    byte[] DATA = new byte[4];

                    ret = Motion.mAcm_DevReadSDOData(m_DeviceHandle, IORing, ID, IOAdress, 0, (ushort)TypeOfIndex[IOAdress], 4, UnManagePtr);
                    Marshal.Copy(UnManagePtr, DATA, 0, 4);
                    UInt16 data = (ushort)((UInt16)DATA[0] + (UInt16)(DATA[1] << 8));
                    Marshal.FreeHGlobal(UnManagePtr);
                    return data;
                }
            }
            catch (Exception e)
            {
                LogMgr.SendLog(e.Message, e);
            }
            return 0;
        }
        private UInt32 ReadU32(ushort IOAdress)
        {
            IntPtr UnManagePtr = Marshal.AllocHGlobal(4);
            byte[] DATA = new byte[4];

            ret = Motion.mAcm_DevReadSDOData(m_DeviceHandle, IORing, ID, IOAdress, 0, (ushort)TypeOfIndex[IOAdress], 4, UnManagePtr);
            Marshal.Copy(UnManagePtr, DATA, 0, 4);
            UInt32 data = DATA[0] + (UInt32)(DATA[1] << 8) + (UInt32)(DATA[2] << 16) + (UInt32)(DATA[3] << 24);
            Marshal.FreeHGlobal(UnManagePtr);
            return data;
        }
        private void WriteU8(ushort IOAdress, byte data)
        {
            try
            {
                unsafe
                {
                    Motion.mAcm_DaqDoSetByteEx(m_DeviceHandle, IORing, ID, IOAdress, data);
                }
            }
            catch (Exception e)
            {
                LogMgr.SendLog(e.Message, e);
            }
        }
        private void WriteU16(ushort IOAdress, UInt16 data)
        {
            byte[] DATA = new byte[2];
            DATA[0] = (byte)data;
            DATA[1] = (byte)(data >> 8);
            Motion.mAcm_DaqDoSetByteEx(m_DeviceHandle, IORing, ID, IOAdress, DATA[0]);
            Motion.mAcm_DaqDoSetByteEx(m_DeviceHandle, IORing, ID, (ushort)(IOAdress + 1), DATA[1]);
        }
        private void WriteU32(ushort IOAdress, UInt32 data)
        {
            byte[] DATA = new byte[4];
            DATA[0] = (byte)data;
            DATA[1] = (byte)(data >> 8);
            DATA[2] = (byte)(data >> 16);
            DATA[3] = (byte)(data >> 24);
            Motion.mAcm_DaqDoSetByteEx(m_DeviceHandle, IORing, ID, IOAdress, DATA[0]);
            Motion.mAcm_DaqDoSetByteEx(m_DeviceHandle, IORing, ID, (ushort)(IOAdress + 1), DATA[1]);
            Motion.mAcm_DaqDoSetByteEx(m_DeviceHandle, IORing, ID, (ushort)(IOAdress + 2), DATA[2]);
            Motion.mAcm_DaqDoSetByteEx(m_DeviceHandle, IORing, ID, (ushort)(IOAdress + 3), DATA[3]);
        }
        #endregion
    }
    #endregion

    #region DIMA
    /// <summary>
    /// 帝馬步進控制
    /// </summary>
    public sealed class TDIMAAxis : EcatAxis, IVelocityFucntion
    {
        private ushort ID;
        public TAxisInfo AxisInfo = new TAxisInfo();
        public TDIMAAxis()
        {

        }
        /// <summary>
        ///   IO控制電動缸
        /// </summary>
        /// <param name="ptr"></param>
        /// <param name="Index"></param>
        public TDIMAAxis(int Index)
        {
            ID = (ushort)Index;
        }

        public override bool AbsMoveMm(double pos)
        {
            return true;
        }
        public override bool MoveDone()
        {
            return true;
        }
        public override double NowPos()
        {
            return 0;
        }

        public override bool DecStop()
        {
            return true;
        }

        public override bool Home()
        {
            return true;
        }
        public override bool HomeDone()
        {
            return true;
        }

        public override bool JogNegative()
        {
            return true;
        }

        public override bool JogPositive()
        {
            return true;
        }

        public override bool Open()
        {
            return true;
        }

        public override void RefreshAxisCommandPos()
        {

        }

        public override void RefreshAxisEncoderPos()
        {

        }

        public override void RefreshAxisIO()
        {

        }

        public override void RefreshAxisState()
        {

        }

        public override bool ResetError()
        {
            return true;
        }

        public override bool ServoOff()
        {
            return true;
        }

        public override bool ServoOn()
        {
            return true;
        }

        public override void SetHomePrm()
        {
            throw new NotImplementedException();
        }

        public override void SetPosMovePrm()
        {
            throw new NotImplementedException();
        }

        public override void StartPolling()
        {

        }

        public override void StopPolling()
        {

        }

        public void SetVelocityMovePrm()
        {

        }

        public void VelocityMove(uint Velocity, bool Dir)
        {

        }

        public void StopVelocityMove()
        {

        }
    }
    #endregion
}
