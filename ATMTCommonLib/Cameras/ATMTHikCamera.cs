using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MvCamCtrl.NET;
using System.Runtime.InteropServices;

namespace ATMTCommonLib
{
    /// <summary>
    /// CallBack Args
    /// </summary>
    public class ATMTImageGrabbedEventArgs : EventArgs
    {
        public int CameraIndex;
        public IntPtr ImagePtr;
        public uint ImageWidth;
        public uint ImageHeight;
        public string PixelFormat;
        public bool IsColor;
    }
    public class HikImage
    {
        public HikImage(IntPtr p, bool c, uint w, uint h)
        {
            ptr = p;
            IsColor = c;
            W = w;
            H = h;
        }
        public IntPtr ptr;
        public bool IsColor;
        public uint W;
        public uint H;
    }
    /// <summary>
    /// 相機基底抽象類別
    /// </summary>
    public abstract class CameraBase
    {
        /// <summary>
        /// 列舉相機序號
        /// </summary>
        /// <returns></returns>
        public abstract List<string> EnumerateCamera();
        /// <summary>
        ///  開啟相機 by SerailNumber or IP
        /// </summary>
        /// <param name="SerailNumber"></param>
        /// <returns>對應的Index</returns>
        public abstract int OpenCamera(string SerailNumber);
        /// <summary>
        /// 開啟相機 by Index
        /// </summary>
        /// <param name="Index"></param>
        /// <returns></returns>
        public abstract bool OpenCamera(int Index);
        public abstract void CloseCamera(int Index);
        /// <summary>
        /// 外部觸發設定
        /// </summary>
        /// <param name="Index"></param>
        public abstract void SetTriggerMode(int Index, bool OnOff);
        /// <summary>
        /// 單張取像
        /// </summary>
        /// <param name="Index"></param>
        public abstract HikImage OneShot(int Index);
        /// <summary>
        /// 連續取像開始取像
        /// </summary>
        /// <param name="Index"></param>
        public abstract void GrabStart(int Index);
        /// <summary>
        /// 連續取像停止
        /// </summary>
        /// <param name="Index"></param>
        public abstract void GrabStop(int Index);
        /// <summary>
        /// 單張取像 Live中
        /// </summary>
        /// <param name="Index"></param>
        /// <returns></returns>
        public abstract HikImage GetLastOne(int Index);
    }

    #region Hik
    public delegate void OnImageGrab(Object sender, ATMTImageGrabbedEventArgs e);
    [StructLayout(LayoutKind.Sequential)]
    public struct CamUserData
    {
        /// <summary>
        /// 不可能超過255隻
        /// </summary>
        public byte Index;
    }
    /// <summary>
    /// 海康 EnumerateCamea()  ==> Get HikCameras.deviceList.Count > 0 ==> Open(Index) => ImageGrabbedNotify += UserImageGrab
    /// </summary>
    public class HikCameras : CameraBase
    {
        /// <summary>
        /// CalllBack += OnImageGrab(Object sender, ATMTImageGrabbedEventArgs e);
        /// </summary>
        public event OnImageGrab ImageGrabbedNotify;
        private List<IntPtr> Ptrs = new List<IntPtr>();
        private List<MyCamera.MV_CC_DEVICE_INFO> deviceInfoList = new List<MyCamera.MV_CC_DEVICE_INFO>();
        private MyCamera.MV_CC_DEVICE_INFO_LIST stDevList = new MyCamera.MV_CC_DEVICE_INFO_LIST();
        public List<MyCamera> deviceList { get; set; } = new List<MyCamera>();
        public List<MyCamera> TempdeviceList { get; set; } = new List<MyCamera>();
        public MyCamera.cbOutputExdelegate[] ImageCallback = new MyCamera.cbOutputExdelegate[4];
        public List<string> SerialNumbers = new List<string>();
        public List<bool> IsGrabStart = new List<bool>();
        public HikCameras()
        {

        }
        public void SortCameraIndice(List<int> Indices)
        {
            MyCamera.MV_CC_DEVICE_INFO_LIST TempDevList = new MyCamera.MV_CC_DEVICE_INFO_LIST();
            TempDevList.nDeviceNum = stDevList.nDeviceNum;
            TempDevList.pDeviceInfo = new IntPtr[stDevList.nDeviceNum];
            for (int i = 0; i < Indices.Count; i++)
            {
                if (i < stDevList.nDeviceNum)
                {
                    TempDevList.pDeviceInfo[i] = stDevList.pDeviceInfo[Indices[i]];
                    TempdeviceList[i] = deviceList[Indices[i]];
                }
            }
            for (int i = 0; i < stDevList.nDeviceNum; i++)
            {
                stDevList.pDeviceInfo[i] = TempDevList.pDeviceInfo[i];
                deviceList[i] = TempdeviceList[i];
            }
        }
        public override List<string> EnumerateCamera()
        {
            int nRet = MyCamera.MV_OK;
            nRet = MyCamera.MV_CC_EnumDevices_NET(MyCamera.MV_GIGE_DEVICE | MyCamera.MV_USB_DEVICE, ref stDevList);
            if (MyCamera.MV_OK != nRet)
            {
                return new List<string>();

            }
            LogMgr.SendLog("Enum Hik device count : " + Convert.ToString(stDevList.nDeviceNum));
            if (0 == stDevList.nDeviceNum)
            {
                return new List<string>();
            }
            else
            {
                uint num = stDevList.nDeviceNum;
                Ptrs.Clear();
                deviceInfoList.Clear();
                MyCamera.MV_CC_DEVICE_INFO stDevInfo;
                for (int i = 0; i < stDevList.nDeviceNum; i++)
                {
                    Ptrs.Add(stDevList.pDeviceInfo[i]);
                    stDevInfo = (MyCamera.MV_CC_DEVICE_INFO)Marshal.PtrToStructure(stDevList.pDeviceInfo[i], typeof(MyCamera.MV_CC_DEVICE_INFO));
                    deviceInfoList.Add(stDevInfo);
                    if (MyCamera.MV_GIGE_DEVICE == stDevInfo.nTLayerType)
                    {
                        MyCamera.MV_GIGE_DEVICE_INFO stGigEDeviceInfo = (MyCamera.MV_GIGE_DEVICE_INFO)MyCamera.ByteToStruct(stDevInfo.SpecialInfo.stGigEInfo, typeof(MyCamera.MV_GIGE_DEVICE_INFO));
                        uint nIp1 = ((stGigEDeviceInfo.nCurrentIp & 0xff000000) >> 24);
                        uint nIp2 = ((stGigEDeviceInfo.nCurrentIp & 0x00ff0000) >> 16);
                        uint nIp3 = ((stGigEDeviceInfo.nCurrentIp & 0x0000ff00) >> 8);
                        uint nIp4 = (stGigEDeviceInfo.nCurrentIp & 0x000000ff);
                        LogMgr.SendLog("[device " + i.ToString() + "]:");
                        LogMgr.SendLog("DevIP:" + nIp1 + "." + nIp2 + "." + nIp3 + "." + nIp4);
                        LogMgr.SendLog("UserDefineName:" + stGigEDeviceInfo.chUserDefinedName + "\n");
                    }
                    else if (MyCamera.MV_USB_DEVICE == stDevInfo.nTLayerType)
                    {
                        MyCamera.MV_USB3_DEVICE_INFO stUsb3DeviceInfo = (MyCamera.MV_USB3_DEVICE_INFO)MyCamera.ByteToStruct(stDevInfo.SpecialInfo.stUsb3VInfo, typeof(MyCamera.MV_USB3_DEVICE_INFO));
                        LogMgr.SendLog("[device " + i.ToString() + "]:");
                        SerialNumbers.Add(stUsb3DeviceInfo.chSerialNumber);
                        deviceList.Add(new MyCamera());
                        TempdeviceList.Add(null);
                        IsGrabStart.Add(false);
                        LogMgr.SendLog("UserDefineName:" + stUsb3DeviceInfo.chUserDefinedName + "\n");
                    }
                }
                return SerialNumbers;
            }
        }
        public override bool OpenCamera(int Index)
        {
            MyCamera.MV_CC_DEVICE_INFO Info = deviceInfoList[Index];
            int nRet = deviceList[Index].MV_CC_CreateDevice_NET(ref Info);
            if (MyCamera.MV_OK != nRet)
            {
                LogMgr.SendLog("Create device failed:" + nRet.ToString());
                return false;
            }
            //Ini(Index);
            return true;
        }
        public override int OpenCamera(string SerialNumber)
        {
            bool IsSerialNumberExist = false;
            int Index = 0;
            for (int i = 0; i < deviceInfoList.Count; i++)
            {
                if (SerialNumber == SerialNumbers[i])
                {
                    Index = i;
                    IsSerialNumberExist = true;
                }
            }
            if (!IsSerialNumberExist)
            {
                LogMgr.SendLog("SerailNumber Not Found.");
                return -1;
            }
            MyCamera.MV_CC_DEVICE_INFO Info = deviceInfoList[Index];
            int nRet = deviceList[Index].MV_CC_CreateDevice_NET(ref Info);
            if (MyCamera.MV_OK != nRet)
            {
                LogMgr.SendLog("Create device failed:" + nRet.ToString());
                return -1;
            }
            nRet = deviceList[Index].MV_CC_OpenDevice_NET();
            if (MyCamera.MV_OK != nRet)
            {
                LogMgr.SendLog("Open device failed:" + nRet.ToString());
                return -1;
            }
            return Index;
        }
        public void SetIsColor(int Index, bool IsColor)
        {
            if (IsColor)
            {

                int nRet = deviceList[Index].MV_CC_SetPixelFormat_NET((uint)MyCamera.MvGvspPixelType.PixelType_Gvsp_BGR8_Packed);
                if (MyCamera.MV_OK != nRet)
                {
                    LogMgr.SendLog("Set Color failed:" + nRet.ToString());
                }
            }
            else
            {
                int nRet = deviceList[Index].MV_CC_SetPixelFormat_NET((uint)MyCamera.MvGvspPixelType.PixelType_Gvsp_Mono8);
                if (MyCamera.MV_OK != nRet)
                {
                    LogMgr.SendLog("Set Mono8 failed:" + nRet.ToString());
                }
            }
        }
        private void Ini(int Index)
        {
            int nRet = deviceList[Index].MV_CC_SetEnumValue_NET("ExposureAuto", 0);
            nRet = deviceList[Index].MV_CC_SetEnumValue_NET("GainAuto", 0);
            if (MyCamera.MV_OK != nRet)
            {
                LogMgr.SendLog("Set number of image node fail:", nRet.ToString());
            }
            SetTriggerMode(Index, false);
        }
        public override void CloseCamera(int Index)
        {
            int nRet = deviceList[Index].MV_CC_CloseDevice_NET();
            if (MyCamera.MV_OK != nRet)
            {
                LogMgr.SendLog("Create device failed:" + nRet.ToString());
            }
        }
        private const int Off = 0;
        private const int On = 1;
        public void SetStrategy(int Index)
        {
            int nRet = deviceList[Index].MV_CC_SetGrabStrategy_NET(MyCamera.MV_GRAB_STRATEGY.MV_GrabStrategy_LatestImagesOnly);
            if(nRet != 0)
            {
                LogMgr.SendLog("SetStrategy Fail = " + nRet,ToString());
            }
        }
        public override void SetTriggerMode(int Index, bool OnOff)
        {
            int nRet;
            if (OnOff)
                nRet = deviceList[Index].MV_CC_SetEnumValue_NET("TriggerMode", On);
            else
                nRet = deviceList[Index].MV_CC_SetEnumValue_NET("TriggerMode", Off);
            if (MyCamera.MV_OK != nRet)
            {
                LogMgr.SendLog("Create device failed:" + nRet.ToString());
            }
        }
        public void SetReverseX(int Index, bool IsReverse)
        {
            bool bValue = IsReverse;
            int nRet = deviceList[Index].MV_CC_SetBoolValue_NET("ReverseX", bValue);
            if (MyCamera.MV_OK != nRet)
            {
                Console.WriteLine("Set ReverseX Value failed:" + nRet.ToString());
                return;
            }
        }
        public void SetReverseY(int Index, bool IsReverse)
        {
            bool bValue = IsReverse;
            int nRet = deviceList[Index].MV_CC_SetBoolValue_NET("ReverseY", bValue);
            if (MyCamera.MV_OK != nRet)
            {
                LogMgr.SendLog("Set ReverseY Value failed: ErrorCode = " + nRet.ToString());
                return;
            }
        }
        public void SetExposureTime(int Index, float Value)
        {
            int nRet = deviceList[Index].MV_CC_SetFloatValue_NET("ExposureTime", Value);
            if (MyCamera.MV_OK != nRet)
            {
                LogMgr.SendLog("Set Exposure Time Fail! ErrorCode = " + nRet.ToString());
                return;
            }
        }
        public void SetGain(int Index, float Value)
        {
            int nRet = deviceList[Index].MV_CC_SetFloatValue_NET("Gain", Value);
            if (MyCamera.MV_OK != nRet)
            {
                Console.WriteLine("Set Gain Value failed:" + nRet.ToString());
                return;
            }
        }
        public void SetGamma(int Index, float Value)
        {
            int nRet = deviceList[Index].MV_CC_SetFloatValue_NET("Gamma", Value);
            if (MyCamera.MV_OK != nRet)
            {
                Console.WriteLine("Set Gamma Value failed:" + nRet.ToString());
                return;
            }
        }
        public override void GrabStart(int Index)
        {
            try
            {
                int nRet = 0;
                if (!IsGrabStart[Index])
                {
                    IsGrabStart[Index] = true;
                    nRet = deviceList[Index].MV_CC_StartGrabbing_NET();
                }
                else
                {
                    LogMgr.SendLog("Camera", "Already Start" + Index.ToString());
                }
                IsGrabStart[Index] = true;
                if (MyCamera.MV_OK != nRet)
                {
                    LogMgr.SendLog("Camera", "GrabStart Fail" + nRet.ToString());
                }
            }
            catch (Exception e)
            {
                LogMgr.SendLog("Camera", e.Message + "Cam" + Index.ToString(), e);
            }
        }

        public override void GrabStop(int Index)
        {
            try
            {
                int nRet = 0;
                if (IsGrabStart[Index])
                {
                    nRet = deviceList[Index].MV_CC_StopGrabbing_NET();
                    IsGrabStart[Index] = false;
                }
                else
                {
                    LogMgr.SendLog("Already Stop" + Index.ToString());
                }
                if (MyCamera.MV_OK != nRet)
                {
                    LogMgr.SendLog("GrabStop Fail" + nRet.ToString());
                }
            }
            catch (Exception e)
            {
                LogMgr.SendLog(e.Message + "Cam" + Index.ToString(), e);
            }
        }
        public bool[] IsGrabbing { get; set; } = new bool[] { false, false, false, false };
        public override HikImage OneShot(int Index)
        {
            try
            {
                bool IsColor = false;
                MyCamera.MV_FRAME_OUT stFrameInfo = new MyCamera.MV_FRAME_OUT();
                int nRet = 0;
                if (IsGrabbing[Index])
                {
                    nRet = deviceList[Index].MV_CC_StartGrabbing_NET();
                    IsGrabbing[Index] = true;
                }
                nRet = deviceList[Index].MV_CC_GetImageBuffer_NET(ref stFrameInfo, 1000);
                if (nRet == MyCamera.MV_OK)
                {
                    if (stFrameInfo.stFrameInfo.enPixelType == MyCamera.MvGvspPixelType.PixelType_Gvsp_Mono8)
                    {
                        IsColor = false;
                        nRet = deviceList[Index].MV_CC_StopGrabbing_NET();
                        if (MyCamera.MV_OK != nRet)
                        {
                            LogMgr.SendLog("Stop Grab Fail" + Index.ToString() + nRet.ToString());
                            return new HikImage(IntPtr.Zero, false, (uint)Index, (uint)Index);
                        }
                        return new HikImage(stFrameInfo.pBufAddr, IsColor, stFrameInfo.stFrameInfo.nWidth, stFrameInfo.stFrameInfo.nHeight);
                    }
                    else
                    {
                        IsColor = true;
                        nRet = deviceList[Index].MV_CC_StopGrabbing_NET();
                        if (MyCamera.MV_OK != nRet)
                        {
                            LogMgr.SendLog("Stop Grab Fail" + Index.ToString() + nRet.ToString());
                            return new HikImage(IntPtr.Zero, false, (uint)Index, (uint)Index);
                        }
                        return new HikImage(stFrameInfo.pBufAddr, IsColor, stFrameInfo.stFrameInfo.nWidth, stFrameInfo.stFrameInfo.nHeight);
                    }
                }
                else
                {
                    LogMgr.SendLog("Grab Time Out " + Index.ToString());
                    return new HikImage(IntPtr.Zero, false, (uint)Index, (uint)Index);
                }

            }
            catch (Exception e)
            {
                int nRet = deviceList[Index].MV_CC_StopGrabbing_NET();
                if (MyCamera.MV_OK != nRet)
                {
                    LogMgr.SendLog("Stop Grab Fail" + nRet.ToString());
                    return new HikImage(IntPtr.Zero, false, 0, 0);
                }
                LogMgr.SendLog(e.Message, e);
                return new HikImage(IntPtr.Zero, false, 0, 0);
            }
        }
        public override HikImage GetLastOne(int Index)
        {
            try
            {
                if (!IsGrabbing[Index])
                {
                    IsGrabbing[Index] = true;
                    GrabStart(Index);
                }
                MyCamera.MV_FRAME_OUT stFrameInfo = new MyCamera.MV_FRAME_OUT();
                int nRet = deviceList[Index].MV_CC_GetImageBuffer_NET(ref stFrameInfo, 2000);
                if (nRet == MyCamera.MV_OK)
                {
                    if (stFrameInfo.stFrameInfo.enPixelType == MyCamera.MvGvspPixelType.PixelType_Gvsp_Mono8)
                    {
                        nRet = deviceList[Index].MV_CC_FreeImageBuffer_NET(ref stFrameInfo);
                        if (MyCamera.MV_OK != nRet)
                        {
                            Console.WriteLine("Free Image Buffer fail:{0:x8}", nRet);
                        }
                        return new HikImage(stFrameInfo.pBufAddr, false, stFrameInfo.stFrameInfo.nWidth, stFrameInfo.stFrameInfo.nHeight);
                    }
                    else
                    {
                        nRet = deviceList[Index].MV_CC_FreeImageBuffer_NET(ref stFrameInfo);
                        if (MyCamera.MV_OK != nRet)
                        {
                            Console.WriteLine("Free Image Buffer fail:{0:x8}", nRet);
                        }
                        return new HikImage(stFrameInfo.pBufAddr, true, stFrameInfo.stFrameInfo.nWidth, stFrameInfo.stFrameInfo.nHeight);
                    }
                }
                else
                {
                    LogMgr.SendLog("Camera " + (Index + 1).ToString() + "GetLastOne Grab Error = " + nRet.ToString());
                    return new HikImage(IntPtr.Zero, false, (uint)Index, (uint)Index);
                }
            }
            catch (Exception e)
            {
                LogMgr.SendLog(e.Message, e);
                return new HikImage(IntPtr.Zero, false, (uint)Index, (uint)Index);
            }
        }
        /// <summary>
        /// MV_GrabStrategy_OneByOne = 0,
        /// MV_GrabStrategy_LatestImagesOnly = 1,
        /// MV_GrabStrategy_LatestImages = 2,
        /// MV_GrabStrategy_UpcomingImage = 3
        /// </summary>
        /// <param name="Index"></param>
        /// <param name="GrabStrategy"></param>
        public void SetGrabStrategy(int Index, int GrabStrategy)
        {
            if (GrabStrategy == 0)
            {
                int nRet = deviceList[Index].MV_CC_SetGrabStrategy_NET(MyCamera.MV_GRAB_STRATEGY.MV_GrabStrategy_OneByOne);
                if (MyCamera.MV_OK != nRet)
                {
                    LogMgr.SendLog("SetStrategy Error = " + nRet.ToString());
                }
            }
            if (GrabStrategy == 1)
            {
                int nRet = deviceList[Index].MV_CC_SetGrabStrategy_NET(MyCamera.MV_GRAB_STRATEGY.MV_GrabStrategy_LatestImagesOnly);
                if (MyCamera.MV_OK != nRet)
                {
                    LogMgr.SendLog("SetStrategy Error = " + nRet.ToString());
                }
            }
            if (GrabStrategy == 2)
            {
                int nRet = deviceList[Index].MV_CC_SetGrabStrategy_NET(MyCamera.MV_GRAB_STRATEGY.MV_GrabStrategy_LatestImages);
                if (MyCamera.MV_OK != nRet)
                {
                    LogMgr.SendLog("SetStrategy Error = " + nRet.ToString());
                }
            }
            if (GrabStrategy == 3)
            {
                int nRet = deviceList[Index].MV_CC_SetGrabStrategy_NET(MyCamera.MV_GRAB_STRATEGY.MV_GrabStrategy_UpcomingImage);
                if (MyCamera.MV_OK != nRet)
                {
                    LogMgr.SendLog("SetStrategy Error = " + nRet.ToString());
                }
            }
        }
        /// <summary>
        ///  Attention::  Hik 註冊 CallBack後  就只能用非同步CallBack  不能同步取像了 ， 而且無法取消註冊
        /// </summary>
        /// <param name="Index"></param>
        /// <returns></returns>
        public bool RegisterCallBack(int Index)
        {
            // ch:注册回调函数 | en:Register image callback
            try
            {
                ImageCallback[Index] = new MyCamera.cbOutputExdelegate(ImageCallbackFunc);
                //CamUserData userdata = new CamUserData();
                //IntPtr ptr = Marshal.AllocHGlobal(Marshal.SizeOf(userdata));
                //userdata.Index = (byte)Index;
                //Marshal.StructureToPtr(userdata, ptr, true);
                int nRet = deviceList[Index].MV_CC_RegisterImageCallBackEx_NET(ImageCallback[Index], IntPtr.Zero);
                if (MyCamera.MV_OK != nRet)
                {
                    LogMgr.SendLog("Create device failed:" + nRet.ToString());
                    return false;
                }
                //Ini(Index);
                return true;
            }
            catch (Exception e)
            {
                LogMgr.SendLog(e.Message, e.ToString());
                return false;
            }
        }
        //if lock needs
        //private static readonly object cblockobj = new object(); 
        private void ImageCallbackFunc(IntPtr pData, ref MyCamera.MV_FRAME_OUT_INFO_EX pFrameInfo, IntPtr pUser)
        {
            //Console.WriteLine("Get one frame: Width[" + Convert.ToString(pFrameInfo.nWidth) + "] , Height[" + Convert.ToString(pFrameInfo.nHeight)
            //                    + "] , FrameNum[" + Convert.ToString(pFrameInfo.nFrameNum) + "]");
            CamUserData data = new CamUserData();
            //unsafe
            //{
            //    lock (cblockobj)
            //    {
            //        Marshal.PtrToStructure(pUser, data);
            //    }
            //}
            bool IsImageColor = false;
            if (pFrameInfo.enPixelType == MyCamera.MvGvspPixelType.PixelType_Gvsp_Mono8)
            {
                IsImageColor = false;
            }
            else
            {
                IsImageColor = true;
            }
            ATMTImageGrabbedEventArgs arg = new ATMTImageGrabbedEventArgs()
            {
                CameraIndex = data.Index,
                ImagePtr = pData,
                ImageWidth = pFrameInfo.nWidth,
                ImageHeight = pFrameInfo.nHeight,
                PixelFormat = pFrameInfo.enPixelType.ToString(),
                IsColor = IsImageColor
            };
            ImageGrabbedNotify?.Invoke(new object(), arg);
        }
        public void CloseAndDestroy()
        {
            for (int i = 0; i < deviceList.Count; i++)
            {
                _ = deviceList[i].MV_CC_CloseDevice_NET();
            }
            for (int i = 0; i < deviceList.Count; i++)
            {
                _ = deviceList[i].MV_CC_DestroyDevice_NET();
            }
        }
    }

    #endregion
}
