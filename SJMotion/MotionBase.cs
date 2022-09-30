using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Ports;

namespace MUTECHMotion
{
    #region Parent
    public enum EmMotorType { Servo, Stepping, EncoderStepping, Linear, DC, AC, Torqe, DirectDrive, Coil, Piezo }
    public enum EmMoveType { Line , Turn}
    /// <summary>
    ///  Board 基底抽象類別
    /// </summary>
    public abstract class PCBase
    {
        /// <summary>
        /// 板卡Handle
        /// </summary>
        public IntPtr DeviceHandle;
        /// <summary>
        /// 取得 PCIe-1203L板卡Handle
        /// </summary>
        /// <returns></returns>
        public abstract bool OpenMotionBoard();
        /// <summary>
        /// 由板卡 Axis Ring 取得軸數
        /// </summary>
        /// <returns></returns>
        public abstract uint GetAxisCount();
    }
    /// <summary>
    ///  扭力控制介面
    /// </summary>
    public interface ITorqueFucntion
    {
        /// <summary>
        /// 推力移動
        /// </summary>
        /// <param name="pos">目標位置</param>
        /// <param name="TorqueMove">移動扭力</param>
        /// <param name="TorqeTriggerLvelPercent">目標回饋扭力 要小於 移動扭力</param>
        void AbsPushMoveMm(uint pos, ushort TorqueMove, ushort TorqeTriggerLvelPercent);
        /// <summary>
        /// 設定推力移動參數
        /// </summary>
        void SetPushMovePrm();
        void StopPushMoveMove();
    }
    /// <summary>
    ///  速度控制介面
    /// </summary>
    public interface IVelocityFucntion
    {
        /// <summary>
        /// 速度移動
        /// </summary>
        void SetVelocityMovePrm();
        /// <summary>
        /// Jog 移動
        /// </summary>
        /// <param name="Velocity"></param>
        /// <param name="Dir"></param>
        void VelocityMove(uint Velocity, bool Dir);
        /// <summary>
        /// 停止Jog
        /// </summary>
        void StopVelocityMove();

    }
    /// <summary>
    /// 數位輸入基底類別
    /// </summary>
    public abstract class DI
    {
        /// <summary>
        /// AxisRing
        /// </summary>
        protected const int AxisRing = 0;
        /// <summary>
        /// IORing
        /// </summary>
        protected const int IORing = 1;
        /// <summary>
        /// 取得IN狀態
        /// </summary>
        /// <returns></returns>
        public abstract bool Get();
    }
    /// <summary>
    ///  DO Base Class
    /// </summary>
    public abstract class DO
    {
        /// <summary>
        /// AxisRing
        /// </summary>
        protected const int AxisRing = 0;
        /// <summary>
        /// IORing
        /// </summary>
        protected const int IORing = 1;
        /// <summary>
        /// Get State
        /// </summary>
        /// <returns></returns>
        public abstract bool Get();
        /// <summary>
        /// On
        /// </summary>
        public abstract void On();
        /// <summary>
        /// Off
        /// </summary>
        public abstract void Off();
    }
    /// <summary>
    /// Ethercat 卡 專用基底抽象類別
    /// </summary>
    public abstract class EcatAxis
    {
        /// <summary>
        /// 軸通道
        /// </summary>
        protected const int AxisRing = 0;
        /// <summary>
        /// IO通道
        /// </summary>
        protected const int IORing = 1;
        /// <summary>
        /// 是否已歸原點
        /// </summary>
        protected bool IsHome = false;
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
        /// abstract JogNegative
        /// </summary>
        /// <returns></returns>
        public abstract bool JogNegative();
        /// <summary>
        /// abstract JogPositive
        /// </summary>
        /// <returns></returns>
        public abstract bool JogPositive();
        /// <summary>
        ///abstract StartPolling
        /// </summary>
        public abstract void StartPolling();
        /// <summary>
        /// abstract StopPolling
        /// </summary>
        public abstract void StopPolling();
    }
    /// <summary>
    /// Plc  通信軸專用基底抽象類別
    /// </summary>
    public abstract class PLCAxis
    {
        /// <summary>
        /// IP
        /// </summary>
        protected string IPAdress { get; set; } = string.Empty;
        /// <summary>
        /// Port
        /// </summary>
        protected int Port { get; set; } = 0;
        public uint Pos { get; set; }
        public ushort Speed { get; set; }
        protected int AxisID { get; set; } = 0;
        protected int AxisName { get; set; } = 0;
        /// <summary>
        /// 是否已歸原點
        /// </summary>
        protected bool IsHome = false;
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
