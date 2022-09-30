using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Ports;

namespace SJMotion
{
    public class SerialCmd
    {
        public static string Enter = "/r/n";
        public static string Space = " ";
        public static string E = Space + Enter;
        public static string En = "En" + E;
        //etc..
    }
    public sealed class ATMTComAxis : ComPortAxis, IVelocityFucntion
    {
        public override void OnReceive(object sender, SerialDataReceivedEventArgs e)
        {
            throw new NotImplementedException();
        }
        public override bool AbsMoveMm(double pos)
        {
            throw new NotImplementedException();
        }

        public override bool DecStop()
        {
            sp.DataReceived += OnReceive;
            throw new NotImplementedException();
        }

        public override bool Home()
        {
            throw new NotImplementedException();
        }

        public override bool HomeDone()
        {
            throw new NotImplementedException();
        }

        public override bool MoveDone()
        {
            throw new NotImplementedException();
        }

        public override double NowPos()
        {
            throw new NotImplementedException();
        }

        public override bool Open()
        {
            sp = new SerialPort();
            sp.DataReceived += OnReceive;
            return true;
        }

        public override void RefreshAxisCommandPos()
        {
            throw new NotImplementedException();
        }

        public override void RefreshAxisEncoderPos()
        {
            throw new NotImplementedException();
        }

        public override void RefreshAxisIO()
        {
            throw new NotImplementedException();
        }

        public override void RefreshAxisState()
        {
            throw new NotImplementedException();
        }

        public override bool ResetError()
        {
            throw new NotImplementedException();
        }

        public override bool ServoOff()
        {
            throw new NotImplementedException();
        }

        public override bool ServoOn()
        {
            throw new NotImplementedException();
        }

        public override void SetHomePrm()
        {
            throw new NotImplementedException();
        }

        public override void SetPosMovePrm()
        {
            throw new NotImplementedException();
        }

        public void SetVelocityMovePrm()
        {
            throw new NotImplementedException();
        }

        public override void StartPolling()
        {
            throw new NotImplementedException();
        }

        public override void StopPolling()
        {
            throw new NotImplementedException();
        }

        public void StopVelocityMove()
        {
            throw new NotImplementedException();
        }

        public void VelocityMove(uint Velocity, bool Dir)
        {
            throw new NotImplementedException();
        }
    }
    #region ComBase
    /// <summary>
    /// RS232  通信軸專用基底抽象類別
    /// </summary>
    public abstract class ComPortAxis
    {
        protected SerialPort sp { get; set; }
        protected SerialDataReceivedEventHandler DataReceived;
        /// <summary>
        /// COMPORT NAME
        /// </summary>
        protected string ComPort { get; set; } = string.Empty;
        protected int AxisID { get; set; } = 0;
        protected int AxisName { get; set; } = 0;
        public uint Pos { get; set; }
        public ushort Speed { get; set; }
        /// <summary>
        /// 是否已歸原點
        /// </summary>
        protected bool IsHome = false;
        public abstract void OnReceive(object sender, SerialDataReceivedEventArgs e);
        /// <summary>
        /// abstract 開啟軸
        /// </summary>
        /// <returns></returns>
        public abstract bool Open();
        /// <summary>
        /// abstract ALM Reset
        /// </summary>
        /// <returns></returns>
        public abstract bool ResetError();
        /// <summary>
        /// abstract ServoOn
        /// </summary>
        /// <returns></returns>
        public abstract bool ServoOn();
        /// <summary>
        /// ServoOff
        /// </summary>
        /// <returns></returns>
        public abstract bool ServoOff();
        /// <summary>
        /// abstract   SetPosMovePrm
        /// </summary>
        public abstract void SetPosMovePrm();
        /// <summary>
        /// abstract AbsMoveMm
        /// </summary>
        /// <param name="pos"></param>
        /// <returns></returns>
        public abstract bool AbsMoveMm(double pos);
        /// <summary>
        /// MoveDone
        /// </summary>
        /// <returns></returns>
        public abstract bool MoveDone();
        /// <summary>
        /// abstract DecStop
        /// </summary>
        /// <returns></returns>
        public abstract bool DecStop();
        /// <summary>
        /// abstract SetHomePrm
        /// </summary>
        public abstract void SetHomePrm();
        /// <summary>
        /// Home
        /// </summary>
        /// <returns></returns>
        public abstract bool Home();
        /// <summary>
        /// HomeDone
        /// </summary>
        /// <returns></returns>
        public abstract bool HomeDone();
        /// <summary>
        /// NowPos
        /// </summary>
        /// <returns></returns>
        public abstract double NowPos();
        /// <summary>
        /// abstract RefreshAxisCommandPos
        /// </summary>
        public abstract void RefreshAxisCommandPos();
        /// <summary>
        /// RefreshAxisEncoderPos
        /// </summary>
        public abstract void RefreshAxisEncoderPos();
        /// <summary>
        /// abstract RefreshAxisIO
        /// </summary>
        public abstract void RefreshAxisIO();
        /// <summary>
        /// abstract RefreshAxisState
        /// </summary>
        public abstract void RefreshAxisState();
        /// <summary>
        ///abstract StartPolling
        /// </summary>
        public abstract void StartPolling();
        /// <summary>
        /// abstract StopPolling
        /// </summary>
        public abstract void StopPolling();
    }
    #endregion
}
