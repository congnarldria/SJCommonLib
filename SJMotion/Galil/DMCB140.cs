using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Net;
using SJCommonLib;

namespace SJMotion
{
    /// <summary>
    /// Galil 通訊非串列式 4軸Full Function控制器
    /// </summary>
    public class TDMCB140
    {
        public enum Logs { DMCB140 }
        public enum EmAxis { A, B, C, D, Count }
        private string IP { get; set; } = "127.0.0.1";
        private string Zero { get; set; } = "0";
        private string One { get; set; } = "1";
        private int Port;
        private TcpClient client { get; set; } = null;
        private Socket socket { get; set; } = null;
        private NetworkStream stream;
        /// <summary>
        /// TPA<enter> 讀取A軸編碼器回饋 
        ///0000000000 控制器回應
        ///TPAB <Enter> 讀取AB軸編碼器回饋
        ///0000000000,0000000000 控制器回應
        /// </summary>
        public string Response= string.Empty;
        public string SocketResponse = string.Empty;
        private bool CmdSucess
        {
            get
            {
                if (Response != CmdInvalid && Response != string.Empty && Response != UnKnownCmd)
                {
                    LogMgr.SendLog(Logs.DMCB140, Response);
                    return true;
                }
                else
                    return false;
            }
        }
        private const string CmdInvalid = "?";
        private const string UnKnownCmd = "1";
        private const string Comma = ",";
        private const string Space = " ";
        /// <summary>
        /// 設參數前先連結到主程式的AxisSettings , 直接參考相等
        /// </summary>
        public List<TAxisSettings> settings { get; set; }
        /// <summary>
        /// 設參數前屬性 settings先連結到主程式的AxisSettings , 直接參考相等
        /// </summary>
        /// <param name="ip">不知道的話就別再寫下去</param>
        /// <param name="port">不知道的話就別再寫下去</param>
        public TDMCB140(string ip, int port)
        {
            IP = ip;
            Port = port;
        }
        public bool Connect()
        {
            try
            {
                IPHostEntry ipHostInfo = Dns.GetHostEntry(Dns.GetHostName());
                IPAddress ipAddress = null;
                for (int i = 0; i < ipHostInfo.AddressList.Length; i++)
                {
                    if(IP == ipHostInfo.AddressList[i].ToString())
                    {
                        ipAddress = ipHostInfo.AddressList[i];
                        break;
                    }
                }
                if (ipAddress == null) return false;
                IPEndPoint localEndPoint = new IPEndPoint(ipAddress, 11000);
                socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                socket.Connect(ipAddress , 12000);
                client.Connect(ipAddress, 13000);
                SendCmd(EmDMCSystemCmd.IK.ToString() + Zero);
                return true;
            }
            catch (Exception e)
            {
                LogMgr.SendLog(Logs.DMCB140, e.Message, e);
                return false;
            }
        }
        public void SetAxisPrm()
        {
            SendCmd(EmDMCMotionCmd.AC + Space + settings[0].Acc.ToString() + Comma + settings[1].Acc.ToString() + Comma + Comma + settings[3].Acc.ToString());
            SendCmd(EmDMCMotionCmd.DC + Space + settings[0].Dec.ToString() + Comma + settings[1].Dec.ToString() + Comma + Comma + settings[3].Dec.ToString());
            SendCmd(EmDMCMotionCmd.SP + Space + settings[0].MoveSpeed.ToString() + Comma + settings[1].MoveSpeed.ToString() + Comma + Comma + settings[3].MoveSpeed.ToString());
        }
        private static readonly object SendLockObj = new object();
        public string SocketSendCmd(string CMD)
        {
            lock (SendLockObj)
            {
                try
                {
                    LogMgr.SendLog(Logs.DMCB140, "Send" + CMD);
                    Byte[] data = System.Text.Encoding.ASCII.GetBytes(CMD);
                    socket.Send(data);
                    data = new Byte[256];
                    int bytes = socket.Receive(data, SocketFlags.None);
                    SocketResponse = Encoding.ASCII.GetString(data, 0, bytes);
                    LogMgr.SendLog(Logs.DMCB140, "Receive" + SocketResponse);
                    return SocketResponse;
                }
                catch (Exception e)
                {
                    LogMgr.SendLog(Logs.DMCB140, e.Message, e);
                    return string.Empty;
                }
            }
        }
        public string SendCmd(string CMD)
        {
            lock (SendLockObj)
            {
                try
                {
                    LogMgr.SendLog(Logs.DMCB140, "Send" + CMD);
                    Byte[] data = System.Text.Encoding.ASCII.GetBytes(CMD);
                    stream = client.GetStream();
                    stream.Write(data, 0, data.Length);
                    string Response = string.Empty;
                    data = new Byte[256];
                    Int32 bytes = stream.Read(data, 0, data.Length);
                    Response = Encoding.ASCII.GetString(data, 0, bytes);
                    LogMgr.SendLog(Logs.DMCB140, "Receive" + Response);
                    stream.Close();
                    return Response;
                }
                catch (Exception e)
                {
                    LogMgr.SendLog(Logs.DMCB140, e.Message, e);
                    return string.Empty;
                }
            }
        }
        public void AllHome()
        {
            SendCmd(EmDMCMotionCmd.HM.ToString());
            SendCmd(EmDMCMotionCmd.BG.ToString());
        }
        public void Home(EmAxis Axis)
        {
            SendCmd(EmDMCMotionCmd.HM.ToString());
            SendCmd(EmDMCMotionCmd.BG.ToString() + Axis.ToString());
        }
        public void AbsMoveMm(EmAxis Axis, double pos)
        {
            string Pos = ((long)(pos * settings[(int)Axis].PPU / pos * settings[(int)Axis].PPUDenominator)).ToString();
            SendCmd(EmDMCMotionCmd.PA.ToString() + Space + Dots(Axis.ToString()) + Pos);
            SendCmd(EmDMCMotionCmd.BG.ToString() + Space + Dots(Axis.ToString()));
        }
        public bool IsAllHome()
        {
            SendCmd("AM");
            return true;
        }
        public bool IsHome(EmAxis Axis)
        {
            SendCmd("AM" + Axis.ToString());
            return true;
        }
        public void AbsMoveDegree(EmAxis Axis, decimal angle)
        {
            string Angle = ((long)(angle * settings[(int)Axis].ModuleRange)).ToString();
            SendCmd(EmDMCMotionCmd.PA.ToString() + Space + Dots(Axis.ToString()) + Angle);
            SendCmd(EmDMCMotionCmd.BG.ToString() + Space + Dots(Axis.ToString()));
        }
        public bool MoveDone(EmAxis Axis)
        {
            SendCmd(EmDMCMotionCmd.AM.ToString() + Space + Axis.ToString());
            return true;
        }
        public string Dots(string axes)
        {
            string dots = string.Empty;
            for (int i = 0; i < (int)EmAxis.Count; i++)
            {
                if (axes.IndexOf(((EmAxis)i).ToString()) != -1)
                {
                    dots += ((EmAxis)i).ToString() + Comma;
                }
                else
                {
                    dots += Comma;
                }
            }
            return dots;
        }
    }
    /// </summary>
    public enum EmDMCSystemCmd
    {
        /// <summary>
        /// ARGUMENTS: CN m,n,o,p,q 
        /// where m,n,o are integers with values 1 or -1.
        /// p is an integer, 0 or 1.
        ///m = 1 Limit switches active high 
        ///m =-1Limit switches active low
        ///n = 1 Home switch configured to drive motor in forward direction when input is high. See HM and FE commands.
        ///n = -1Home switch configured to drive motor in reverse direction when input is high. See HM and FE commands
        ///o =1 Latch input is active high
        ///o = -1 Latch input is active low
        ///p = 1 Configures inputs 5,6,7,8, as selective abort inputs for axes A, B, C, D, E respectively. Will also trigger #POSERR automatic subroutine if program is running.
        ///p = 0 Inputs 5,6,7,8, are configured as general use inputs
        ///q = 1 Abort input will not terminate program execution
        ///q= 0 Abort input will terminate program execution
        ///ex :CN 1,1 Sets limit and home switches to active high
        /// </summary>
        CN,
        /// <summary>
        /// _CN0 Contains the limit switch configuration
        /// _CN1Contains the home switch configuration
        /// _CN2 Contains the latch input configuration
        /// _CN3 Contains the state of the selective abort function (1 enabled, 0 disabled) _CN4 Contains whether the abort input will terminate the program
        /// </summary>
        _CN,
        /// <summary>
        /// Block Ethernet ports
        /// The IK command blocks the controller from receiving packets on Ethernet ports lower than 1000 except for ports 0, 23, 68, and 502.
        /// ex:IK1 Blocks undesirable port communication IK0 Allows all Ethernet ports to be used
        /// </summary>
        IK,
        /// <summary>
        /// 0,前後極限有效
        /// 1.前無效，後有效
        /// 2.跟1相反
        /// 3.極限都無效
        /// </summary>
        LD,
    }
    /// <summary>
    /// Ex: PA ,1000,,2000 第B，D軸的絕對位置
    /// WT20 等待20ms
    /// BGA A軸開始運動
    /// PR ?,?,?, 查詢
    /// PR ,? 查B軸
    /// 指令後可以空格也可以不空格
    /// 有Scrips寫法
    /// </summary>
    public enum EmDMCMotionCmd
    {
        /// <summary>
        /// FUNCTION: Home
        /// DESCRIPTION:
        ///The HM command performs a two-stage homing sequence for stepper motor operation.
        ///During first stage of the homing sequence, the motor moves at the user programmed speed until detecting a transition on the homing input for that axis. The direction for this first stage is determined by the initial state of the homing input. Once the homing input changes state, the motor decelerates to a stop. The state of the homing input can be configured using the CN command.
        ///At the second stage, the motor change directions and slowly approach the transition again. When the transition is detected, the motor is stopped instantaneously.
        /// </summary>
        HM,
        /// <summary>
        ///  原點當前狀態 
        /// </summary>
        _HM,
        /// <summary>
        /// FUNCTION: Abort DESCRIPTION:
        ///AB (Abort) stops a motion instantly without a controlled deceleration. If there is a program operating, AB also aborts the program unless a 1 argument is specified. The command, AB, will shut off the motors for any axis in which the off on error function is enabled (see command OE).
        /// AB aborts motion on all axes in motion and cannot stop individual axes.
        /// ARGUMENTS: AB n where
        ///n = 0 The controller aborts motion and program
        ///n = 1 The controller aborts motion only
        ///No argument will cause the controller to abort the motion and program
        /// </summary>
        AB,
        /// <summary>
        /// 指定相對位置
        /// </summary>
        PR,
        /// <summary>
        /// 指定絕對位置 , 
        /// </summary>
        PA,
        /// <summary>
        /// 指定運動速度
        /// </summary>
        SP,
        /// <summary>
        /// 指定加速度
        /// </summary>
        AC,
        /// <summary>
        /// 指定減速度
        /// </summary>
        DC,
        /// <summary>
        /// 開始運動
        /// </summary>
        BG,
        /// <summary>
        /// 停止運動
        /// </summary>
        ST,
        /// <summary>
        /// 增量位置
        /// </summary>
        IP,
        /// <summary>
        /// 速度平滑的時間常數
        /// </summary>
        IT,
        /// <summary>
        /// 等待運動規劃完成
        /// </summary>
        AM,
        /// <summary>
        /// 等待實際脈衝發送完成
        /// </summary>
        MC,
        /// <summary>
        /// 定義當前位置
        /// </summary>
        DP,
        /// <summary>
        /// 查詢當前位置CmdPos
        /// </summary>
        TD,
        /// <summary>
        /// 查詢當前位置(控制器規劃位置)
        /// </summary>
        RP,
        /// <summary>
        /// +n.ToString()  第n軸的當前EncoderPos
        /// </summary>
        _PR,
        /// <summary>
        /// 若第n軸不在運動中，讀取當前值，若是運動狀態，讀取之前停止的位置值
        /// </summary>
        _PA,
        /// <summary>
        /// +n.ToString()  當前第第n軸的速度設置
        /// </summary>
        _SP,
        /// <summary>
        /// +n.ToString()  當前第第n軸的加速度設置
        /// </summary>
        _AC,
        /// <summary>
        /// +n.ToString()  當前第第n軸的減速度設置
        /// </summary>
        _DC,
        /// <summary>
        ///  +n.ToString()  就是第n軸INP
        /// </summary>
        _BG,
        /// <summary>
        /// Jog JG50000 ,, -25000  A軸以50000速度CW移動 C軸以25000 CCW運動 
        /// </summary>
        JG,
        /// <summary>
        /// 開啟跟隨 PT 1 , AC 150000 , DC 150000 , SP50000 , PA 5000
        /// </summary>
        PT,
        /// <summary>
        /// 直線補間
        /// </summary>
        BGS,
        /// <summary>
        /// 
        /// </summary>
        STS,
        /// <summary>
        /// LMAB  AB補間運動
        /// </summary>
        LM,
        /// <summary>
        /// LI5000 , 0 指定補間路徑
        /// LI0 , 5000
        /// </summary>
        LI,
        /// <summary>
        /// 直線補間路徑結束
        /// </summary>
        LE,
        /// <summary>
        /// VD , VR , VV , CS , AM等等
        /// </summary>
        VS,
        CmdCount
    }
}
