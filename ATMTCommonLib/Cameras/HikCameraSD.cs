using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MvCamCtrl;

namespace ATMTCommonLib
{
    class HikCameraSD
    {
    }
    namespace HikIINFO
    {
        //
        // 摘要:
        //     MyCamera
        public class MyCamera
        {
            //
            // 摘要:
            //     Unknown Device Type, Reserved
            public const int MV_UNKNOW_DEVICE = 0;
            //
            // 摘要:
            //     线程绑核失败
            public const int MV_ALG_E_BIND_CORE_FAILED = 268435500;
            //
            // 摘要:
            //     噪声特性图像格式错误
            public const int MV_ALG_E_DENOISE_NE_IMG_FORMAT = 272637953;
            //
            // 摘要:
            //     噪声特性类型错误
            public const int MV_ALG_E_DENOISE_NE_FEATURE_TYPE = 272637954;
            //
            // 摘要:
            //     噪声特性个数错误
            public const int MV_ALG_E_DENOISE_NE_PROFILE_NUM = 272637955;
            //
            // 摘要:
            //     噪声特性增益个数错误
            public const int MV_ALG_E_DENOISE_NE_GAIN_NUM = 272637956;
            //
            // 摘要:
            //     噪声曲线增益值输入错误
            public const int MV_ALG_E_DENOISE_NE_GAIN_VAL = 272637957;
            //
            // 摘要:
            //     分配内存错误
            public const int MV_ALG_E_MALLOC_MEM = 268435499;
            //
            // 摘要:
            //     噪声曲线柱数错误
            public const int MV_ALG_E_DENOISE_NE_BIN_NUM = 272637958;
            //
            // 摘要:
            //     噪声估计未初始化
            public const int MV_ALG_E_DENOISE_NE_NOT_INIT = 272637960;
            //
            // 摘要:
            //     颜色空间模式错误
            public const int MV_ALG_E_DENOISE_COLOR_MODE = 272637961;
            //
            // 摘要:
            //     图像ROI原点错误
            public const int MV_ALG_E_DENOISE_ROI_ORI_PT = 272637963;
            //
            // 摘要:
            //     图像ROI大小错误
            public const int MV_ALG_E_DENOISE_ROI_SIZE = 272637964;
            //
            // 摘要:
            //     输入的相机增益不存在(增益个数已达上限)
            public const int MV_ALG_E_DENOISE_GAIN_NOT_EXIST = 272637965;
            //
            // 摘要:
            //     输入的相机增益不在范围内
            public const int MV_ALG_E_DENOISE_GAIN_BEYOND_RANGE = 272637966;
            //
            // 摘要:
            //     噪声估计初始化增益设置错误
            public const int MV_ALG_E_DENOISE_NE_INIT_GAIN = 272637959;
            //
            // 摘要:
            //     模型类型错误
            public const int MV_ALG_E_MODEL_TYPE = 268435498;
            //
            // 摘要:
            //     文件类型错误
            public const int MV_ALG_E_FILE_TYPE = 268435497;
            //
            // 摘要:
            //     文件读取大小错误
            public const int MV_ALG_E_FILE_READ_SIZE = 268435496;
            //
            // 摘要:
            //     参数范围不正确
            public const int MV_ALG_E_BAD_ARG = 268435481;
            //
            // 摘要:
            //     数据大小不正确
            public const int MV_ALG_E_DATA_SIZE = 268435482;
            //
            // 摘要:
            //     数据step不正确
            public const int MV_ALG_E_STEP = 268435483;
            //
            // 摘要:
            //     cpu不支持优化代码中的指令集
            public const int MV_ALG_E_CPUID = 268435484;
            //
            // 摘要:
            //     警告
            public const int MV_ALG_WARNING = 268435485;
            //
            // 摘要:
            //     算法库超时
            public const int MV_ALG_E_TIME_OUT = 268435486;
            //
            // 摘要:
            //     算法版本号出错
            public const int MV_ALG_E_LIB_VERSION = 268435487;
            //
            // 摘要:
            //     模型版本号出错
            public const int MV_ALG_E_MODEL_VERSION = 268435488;
            //
            // 摘要:
            //     GPU内存分配错误
            public const int MV_ALG_E_GPU_MEM_ALLOC = 268435489;
            //
            // 摘要:
            //     文件不存在
            public const int MV_ALG_E_FILE_NON_EXIST = 268435490;
            //
            // 摘要:
            //     字符串为空
            public const int MV_ALG_E_NONE_STRING = 268435491;
            //
            // 摘要:
            //     图像解码器错误
            public const int MV_ALG_E_IMAGE_CODEC = 268435492;
            //
            // 摘要:
            //     打开文件错误
            public const int MV_ALG_E_FILE_OPEN = 268435493;
            //
            // 摘要:
            //     文件读取错误
            public const int MV_ALG_E_FILE_READ = 268435494;
            //
            // 摘要:
            //     文件写错误
            public const int MV_ALG_E_FILE_WRITE = 268435495;
            //
            // 摘要:
            //     输入的噪声特性内存大小错误
            public const int MV_ALG_E_DENOISE_NP_BUF_SIZE = 272637967;
            //
            // 摘要:
            //     ch:信息结构体的最大缓存 | en: Max buffer size of information structs
            public const int INFO_MAX_BUFFER_SIZE = 64;
            public const int MV_MAX_DEVICE_NUM = 256;
            //
            // 摘要:
            //     ch:最大Interface数量 | en:Max num of interfaces
            public const int MV_MAX_GENTL_IF_NUM = 256;
            public const int MV_MAX_XML_NODE_NUM_C = 128;
            public const int MV_MAX_XML_SYMBOLIC_NUM = 64;
            public const int MV_MAX_XML_STRVALUE_STRLEN_C = 64;
            public const int MV_MAX_XML_PARENTS_NUM = 8;
            public const int MV_MAX_XML_SYMBOLIC_STRLEN_C = 64;
            public const int MV_EXCEPTION_DEV_DISCONNECT = 32769;
            public const int MV_EXCEPTION_VERSION_CHECK = 32770;
            public const int MV_ACCESS_Exclusive = 1;
            public const int MV_ACCESS_ExclusiveWithSwitch = 2;
            public const int MV_ACCESS_Control = 3;
            public const int MV_ACCESS_ControlWithSwitch = 4;
            public const int MV_ACCESS_ControlSwitchEnable = 5;
            public const int MV_ACCESS_ControlSwitchEnableWithKey = 6;
            public const int MV_ACCESS_Monitor = 7;
            public const int MAX_EVENT_NAME_SIZE = 128;
            public const int MV_MAX_XML_NODE_STRLEN_C = 64;
            //
            // 摘要:
            //     算法库使用期限错误
            public const int MV_ALG_E_EXPIRE = 268435480;
            public const int MV_MAX_XML_DISC_STRLEN_C = 512;
            public const int MV_MATCH_TYPE_NET_DETECT = 1;
            //
            // 摘要:
            //     ch:最大GenTL设备数量 | en:Max num of GenTL devices
            public const int MV_MAX_GENTL_DEV_NUM = 256;
            public const int MV_IP_CFG_STATIC = 83886080;
            public const int MV_IP_CFG_DHCP = 100663296;
            public const int MV_IP_CFG_LLA = 67108864;
            public const int MV_NET_TRANS_DRIVER = 1;
            public const int MV_NET_TRANS_SOCKET = 2;
            public const int MV_CAML_BAUDRATE_9600 = 1;
            public const int MV_CAML_BAUDRATE_19200 = 2;
            public const int MV_CAML_BAUDRATE_38400 = 4;
            public const int MV_CAML_BAUDRATE_57600 = 8;
            public const int MV_CAML_BAUDRATE_115200 = 16;
            public const int MV_CAML_BAUDRATE_230400 = 32;
            public const int MV_CAML_BAUDRATE_460800 = 64;
            public const int MV_CAML_BAUDRATE_921600 = 128;
            public const int MV_CAML_BAUDRATE_AUTOMAX = 1073741824;
            public const int MV_MATCH_TYPE_USB_DETECT = 2;
            //
            // 摘要:
            //     加密错误
            public const int MV_ALG_E_ENCRYPT = 268435479;
            //
            // 摘要:
            //     图像ROI个数错误
            public const int MV_ALG_E_DENOISE_ROI_NUM = 272637962;
            //
            // 摘要:
            //     超过限定的最大内存
            public const int MV_ALG_E_OVER_MAX_MEM = 268435477;
            //
            // 摘要:
            //     Unknown error
            public const int MV_E_UNKNOW = -2147483393;
            //
            // 摘要:
            //     General error
            public const int MV_E_GC_GENERIC = -2147483392;
            //
            // 摘要:
            //     Illegal parameters
            public const int MV_E_GC_ARGUMENT = -2147483391;
            //
            // 摘要:
            //     The value is out of range
            public const int MV_E_GC_RANGE = -2147483390;
            //
            // 摘要:
            //     Property
            public const int MV_E_GC_PROPERTY = -2147483389;
            //
            // 摘要:
            //     Running environment error
            public const int MV_E_GC_RUNTIME = -2147483388;
            //
            // 摘要:
            //     Logical error
            public const int MV_E_GC_LOGICAL = -2147483387;
            //
            // 摘要:
            //     Node accessing condition error
            public const int MV_E_GC_ACCESS = -2147483386;
            //
            // 摘要:
            //     Timeout
            public const int MV_E_GC_TIMEOUT = -2147483385;
            //
            // 摘要:
            //     Transformation exception
            public const int MV_E_GC_DYNAMICCAST = -2147483384;
            //
            // 摘要:
            //     GenICam unknown error
            public const int MV_E_GC_UNKNOW = -2147483137;
            //
            // 摘要:
            //     The command is not supported by device
            public const int MV_E_NOT_IMPLEMENTED = -2147483136;
            //
            // 摘要:
            //     The target address being accessed does not exist
            public const int MV_E_INVALID_ADDRESS = -2147483135;
            //
            // 摘要:
            //     The target address is not writable
            public const int MV_E_WRITE_PROTECT = -2147483134;
            //
            // 摘要:
            //     No permission
            public const int MV_E_ACCESS_DENIED = -2147483133;
            //
            // 摘要:
            //     Encryption error
            public const int MV_E_ENCRYPT = -2147483634;
            //
            // 摘要:
            //     Device is busy, or network disconnected
            public const int MV_E_BUSY = -2147483132;
            //
            // 摘要:
            //     No Avaliable Buffer
            public const int MV_E_NOOUTBUF = -2147483635;
            //
            // 摘要:
            //     Abnormal image, maybe incomplete image because of lost packet
            public const int MV_E_ABNORMAL_IMAGE = -2147483637;
            //
            // 摘要:
            //     GigE Device
            public const int MV_GIGE_DEVICE = 1;
            //
            // 摘要:
            //     1394-a/b Device
            public const int MV_1394_DEVICE = 2;
            //
            // 摘要:
            //     USB3.0 Device
            public const int MV_USB_DEVICE = 4;
            //
            // 摘要:
            //     CameraLink Device
            public const int MV_CAMERALINK_DEVICE = 8;
            //
            // 摘要:
            //     Successed, no error
            public const int MV_OK = 0;
            //
            // 摘要:
            //     回调函数出错
            public const int MV_ALG_E_CALL_BACK = 268435478;
            //
            // 摘要:
            //     Not supported function
            public const int MV_E_SUPPORT = -2147483647;
            //
            // 摘要:
            //     Buffer overflow
            public const int MV_E_BUFOVER = -2147483646;
            //
            // 摘要:
            //     Function calling order error
            public const int MV_E_CALLORDER = -2147483645;
            //
            // 摘要:
            //     Incorrect parameter
            public const int MV_E_PARAMETER = -2147483644;
            //
            // 摘要:
            //     Applying resource failed
            public const int MV_E_RESOURCE = -2147483642;
            //
            // 摘要:
            //     No data
            public const int MV_E_NODATA = -2147483641;
            //
            // 摘要:
            //     Precondition error, or running environment changed
            public const int MV_E_PRECONDITION = -2147483640;
            //
            // 摘要:
            //     Version mismatches
            public const int MV_E_VERSION = -2147483639;
            //
            // 摘要:
            //     Insufficient memory
            public const int MV_E_NOENOUGH_BUF = -2147483638;
            //
            // 摘要:
            //     Load library failed
            public const int MV_E_LOAD_LIBRARY = -2147483636;
            //
            // 摘要:
            //     Network data packet error
            public const int MV_E_PACKET = -2147483131;
            //
            // 摘要:
            //     Error or invalid handle
            public const int MV_E_HANDLE = int.MinValue;
            //
            // 摘要:
            //     Device IP conflict
            public const int MV_E_IP_CONFLICT = -2147483103;
            //
            // 摘要:
            //     内存空间大小不满足对齐要求
            public const int MV_ALG_E_MEM_SIZE_ALIGN = 268435461;
            //
            // 摘要:
            //     内存地址不满足对齐要求
            public const int MV_ALG_E_MEM_ADDR_ALIGN = 268435462;
            //
            // 摘要:
            //     图像格式不正确或者不支持
            public const int MV_ALG_E_IMG_FORMAT = 268435463;
            //
            // 摘要:
            //     图像宽高与step参数不匹配
            public const int MV_ALG_E_IMG_STEP = 268435465;
            //
            // 摘要:
            //     图像数据存储地址为空
            public const int MV_ALG_E_IMG_DATA_NULL = 268435466;
            //
            // 摘要:
            //     设置或者获取参数类型不正确
            public const int MV_ALG_E_CFG_TYPE = 268435467;
            //
            // 摘要:
            //     设置或者获取参数的输入、输出结构体大小不正确
            public const int MV_ALG_E_CFG_SIZE = 268435468;
            //
            // 摘要:
            //     处理类型不正确
            public const int MV_ALG_E_PRC_TYPE = 268435469;
            //
            // 摘要:
            //     处理时输入、输出参数大小不正确
            public const int MV_ALG_E_PRC_SIZE = 268435470;
            //
            // 摘要:
            //     子处理类型不正确
            public const int MV_ALG_E_FUNC_TYPE = 268435471;
            //
            // 摘要:
            //     子处理时输入、输出参数大小不正确
            public const int MV_ALG_E_FUNC_SIZE = 268435472;
            //
            // 摘要:
            //     index参数不正确
            public const int MV_ALG_E_PARAM_INDEX = 268435473;
            //
            // 摘要:
            //     value参数不正确或者超出范围
            public const int MV_ALG_E_PARAM_VALUE = 268435474;
            //
            // 摘要:
            //     param_num参数不正确
            public const int MV_ALG_E_PARAM_NUM = 268435475;
            //
            // 摘要:
            //     函数参数指针为空
            public const int MV_ALG_E_NULL_PTR = 268435476;
            //
            // 摘要:
            //     内存空间大小不够
            public const int MV_ALG_E_MEM_LACK = 268435460;
            //
            // 摘要:
            //     内存对齐不满足要求
            public const int MV_ALG_E_MEM_ALIGN = 268435459;
            //
            // 摘要:
            //     图像宽高不正确或者超出范围
            public const int MV_ALG_E_IMG_SIZE = 268435464;
            //
            // 摘要:
            //     能力集中存在无效参数
            public const int MV_ALG_E_ABILITY_ARG = 268435457;
            //
            // 摘要:
            //     Reading USB error
            public const int MV_E_USB_READ = -2147482880;
            //
            // 摘要:
            //     Writing USB error
            public const int MV_E_USB_WRITE = -2147482879;
            //
            // 摘要:
            //     Device exception
            public const int MV_E_USB_DEVICE = -2147482878;
            //
            // 摘要:
            //     内存地址为空
            public const int MV_ALG_E_MEM_NULL = 268435458;
            //
            // 摘要:
            //     Insufficient bandwidth, this error code is newly added
            public const int MV_E_USB_BANDWIDTH = -2147482876;
            //
            // 摘要:
            //     Driver mismatch or unmounted drive
            public const int MV_E_USB_DRIVER = -2147482875;
            //
            // 摘要:
            //     USB unknown error
            public const int MV_E_USB_UNKNOW = -2147482625;
            //
            // 摘要:
            //     GenICam error
            public const int MV_E_USB_GENICAM = -2147482877;
            //
            // 摘要:
            //     Firmware language mismatches
            public const int MV_E_UPG_LANGUSGE_MISMATCH = -2147482623;
            //
            // 摘要:
            //     Firmware mismatches
            public const int MV_E_UPG_FILE_MISMATCH = -2147482624;
            //
            // 摘要:
            //     不确定类型错误
            public const int MV_ALG_ERR = 268435456;
            //
            // 摘要:
            //     处理正确
            public const int MV_ALG_OK = 0;
            //
            // 摘要:
            //     Unknown error during upgrade
            public const int MV_E_UPG_UNKNOW = -2147482369;
            //
            // 摘要:
            //     Network error
            public const int MV_E_NETER = -2147483130;
            //
            // 摘要:
            //     Camera internal error during upgrade
            public const int MV_E_UPG_INNER_ERR = -2147482621;
            //
            // 摘要:
            //     Upgrading conflicted (repeated upgrading requests during device upgrade)
            public const int MV_E_UPG_CONFLICT = -2147482622;
#if NOUSE
            //
            // 摘要:
            //     Constructor
            public MyCamera();

            //
            // 摘要:
            //     Destructor
            ~MyCamera();

            //
            // 摘要:
            //     Byte array to struct
            //
            // 參數:
            //   bytes:
            //     Byte array
            //
            //   type:
            //     Struct type
            //
            // 傳回:
            //     Struct object
            public static object ByteToStruct(byte[] bytes, Type type);
            //
            // 摘要:
            //     Enumerate Device Based On GenTL
            //
            // 參數:
            //   stIFInfo:
            //     Interface information
            //
            //   stDevList:
            //     Device List
            //
            // 傳回:
            //     Success, return MV_OK. Failure, return error code
            public static int MV_CC_EnumDevicesByGenTL_NET(ref MV_GENTL_IF_INFO stIFInfo, ref MV_GENTL_DEV_INFO_LIST stDevList);
            //
            // 摘要:
            //     Enumerate device according to manufacture name
            //
            // 參數:
            //   nTLayerType:
            //     Enumerate TLs
            //
            //   stDevList:
            //     Device List
            //
            //   pManufacturerName:
            //     Manufacture Name
            //
            // 傳回:
            //     Success, return MV_OK. Failure, return error code
            public static int MV_CC_EnumDevicesEx_NET(uint nTLayerType, ref MV_CC_DEVICE_INFO_LIST stDevList, string pManufacturerName);
            //
            // 摘要:
            //     Enumerate Device
            //
            // 參數:
            //   nTLayerType:
            //     Enumerate TLs
            //
            //   stDevList:
            //     Device List
            //
            // 傳回:
            //     Success, return MV_OK. Failure, return error code
            public static int MV_CC_EnumDevices_NET(uint nTLayerType, ref MV_CC_DEVICE_INFO_LIST stDevList);
            //
            // 摘要:
            //     Get supported Transport Layer
            //
            // 傳回:
            //     Supported Transport Layer number
            public static int MV_CC_EnumerateTls_NET();
            //
            // 摘要:
            //     Enumerate interfaces by GenTL
            //
            // 參數:
            //   stIFInfoList:
            //     Interface information list
            //
            //   pGenTLPath:
            //     Path of GenTL's cti file
            public static int MV_CC_EnumInterfacesByGenTL_NET(ref MV_GENTL_IF_INFO_LIST stIFInfoList, string pGenTLPath);
            //
            // 摘要:
            //     Get SDK Version
            //
            // 傳回:
            //     Always return 4 Bytes of version number |Main |Sub |Rev |Test| 8bits 8bits 8bits
            //     8bits
            public static uint MV_CC_GetSDKVersion_NET();
            //
            // 摘要:
            //     Is the device accessible
            //
            // 參數:
            //   stDevInfo:
            //     Device Information
            //
            //   nAccessMode:
            //     Access Right
            //
            // 傳回:
            //     Access, return true. Not access, return false
            public static bool MV_CC_IsDeviceAccessible_NET(ref MV_CC_DEVICE_INFO stDevInfo, uint nAccessMode);
            //
            // 摘要:
            //     Set SDK log path (Interfaces not recommended) If the logging service MvLogServer
            //     is enabled, the interface is invalid and The logging service is enabled by default
            //
            // 參數:
            //   pSDKLogPath:
            public static int MV_CC_SetSDKLogPath_NET(string pSDKLogPath);
            //
            // 摘要:
            //     Get Multicast Status
            //
            // 參數:
            //   pstDevInfo:
            //     Device Information
            //
            //   pStatus:
            //     Status of Multicast
            //
            // 傳回:
            //     Success, return MV_OK. Failure, return error code
            public static int MV_GIGE_GetMulticastStatus_NET(ref MV_CC_DEVICE_INFO pstDevInfo, ref bool pStatus);
            //
            // 摘要:
            //     Get Camera Handle
            public IntPtr GetCameraHandle();
            public int MV_CAML_GetDeviceBauderate_NET(ref uint pnCurrentBaudrate);
            //
            // 摘要:
            //     Get device baudrate, using one of the CL_BAUDRATE_XXXX value
            //
            // 參數:
            //   pnCurrentBaudrate:
            //     Return pointer of baud rate to user. Refer to the 'CameraParams.h' for parameter
            //     definitions, for example, #define MV_CAML_BAUDRATE_9600 0x00000001
            //
            // 傳回:
            //     Success, return MV_OK. Failure, return error code
            public int MV_CAML_GetDeviceBaudrate_NET(ref uint pnCurrentBaudrate);
            public int MV_CAML_GetSupportBauderates_NET(ref uint pnBaudrateAblity);
            //
            // 摘要:
            //     Get supported baudrates of the combined device and host interface
            //
            // 參數:
            //   pnBaudrateAblity:
            //     Return pointer of the supported baudrates to user. 'OR' operation results of
            //     the supported baudrates. Refer to the 'CameraParams.h' for single value definitions,
            //     for example, #define MV_CAML_BAUDRATE_9600 0x00000001
            //
            // 傳回:
            //     Success, return MV_OK. Failure, return error code
            public int MV_CAML_GetSupportBaudrates_NET(ref uint pnBaudrateAblity);
            public int MV_CAML_SetDeviceBauderate_NET(uint nBaudrate);
            //
            // 摘要:
            //     Set device baudrate using one of the CL_BAUDRATE_XXXX value
            //
            // 參數:
            //   nBaudrate:
            //     Baudrate to set. Refer to the 'CameraParams.h' for parameter definitions, for
            //     example, #define MV_CAML_BAUDRATE_9600 0x00000001
            //
            // 傳回:
            //     Success, return MV_OK. Failure, return error code
            public int MV_CAML_SetDeviceBaudrate_NET(uint nBaudrate);
            //
            // 摘要:
            //     Sets the timeout for operations on the serial port
            //
            // 參數:
            //   nMillisec:
            //     Timeout in [ms] for operations on the serial port.
            //
            // 傳回:
            //     Success, return MV_OK. Failure, return error code
            public int MV_CAML_SetGenCPTimeOut_NET(uint nMillisec);
            //
            // 摘要:
            //     Noise estimate of Bayer format
            //
            // 參數:
            //   pstNoiseEstimateParam:
            //     Noise estimate parameter structure
            //
            // 傳回:
            //     Success, return MV_OK. Failure, return error code
            public int MV_CC_BayerNoiseEstimate_NET(ref MV_CC_BAYER_NOISE_ESTIMATE_PARAM pstNoiseEstimateParam);
            //
            // 摘要:
            //     Spatial Denoise of Bayer format
            //
            // 參數:
            //   pstSpatialDenoiseParam:
            //     Spatial Denoise parameter structure
            //
            // 傳回:
            //     Success, return MV_OK. Failure, return error code
            public int MV_CC_BayerSpatialDenoise_NET(ref MV_CC_BAYER_SPATIAL_DENOISE_PARAM pstSpatialDenoiseParam);
            //
            // 摘要:
            //     Clear image Buffers to clear old data
            //
            // 傳回:
            //     Success, return MV_OK. Failure, return error code
            public int MV_CC_ClearImageBuffer_NET();
            //
            // 摘要:
            //     Close Device
            //
            // 傳回:
            //     Success, return MV_OK. Failure, return error code
            public int MV_CC_CloseDevice_NET();
            //
            // 摘要:
            //     Color Correct(include CCM and CLUT)
            //
            // 參數:
            //   pstColorCorrectParam:
            //     Color Correct parameter structure
            //
            // 傳回:
            //     Success, return MV_OK. Failure, return error code
            public int MV_CC_ColorCorrect_NET(ref MV_CC_COLOR_CORRECT_PARAM pstColorCorrectParam);
            //
            // 摘要:
            //     Pixel format conversion
            //
            // 參數:
            //   pstCvtParam:
            //     Convert Pixel Type parameter structure
            //
            // 傳回:
            //     Success, return MV_OK. Failure, return error code
            public int MV_CC_ConvertPixelType_NET(ref MV_PIXEL_CONVERT_PARAM pstCvtParam);
            //
            // 摘要:
            //     Create Device Handle Based On GenTL Device Info
            //
            // 參數:
            //   stDevInfo:
            //     Device Information Structure
            //
            // 傳回:
            //     Success, return MV_OK. Failure, return error code
            public int MV_CC_CreateDeviceByGenTL_NET(ref MV_GENTL_DEV_INFO stDevInfo);
            //
            // 摘要:
            //     Create Device without log
            //
            // 參數:
            //   stDevInfo:
            //     Device Information
            //
            // 傳回:
            //     Success, return MV_OK. Failure, return error code
            public int MV_CC_CreateDeviceWithoutLog_NET(ref MV_CC_DEVICE_INFO stDevInfo);
            //
            // 摘要:
            //     Create Device
            //
            // 參數:
            //   stDevInfo:
            //     Device Information
            //
            // 傳回:
            //     Success, return MV_OK. Failure, return error code
            public int MV_CC_CreateDevice_NET(ref MV_CC_DEVICE_INFO stDevInfo);
            //
            // 摘要:
            //     Destroy Device
            //
            // 傳回:
            //     Success, return MV_OK. Failure, return error code
            public int MV_CC_DestroyDevice_NET();
            //
            // 摘要:
            //     Display one frame image
            //
            // 參數:
            //   pDisplayInfo:
            //     Image information
            //
            // 傳回:
            //     Success, return MV_OK. Failure, return error code
            public int MV_CC_DisplayOneFrame_NET(ref MV_DISPLAY_FRAME_INFO pDisplayInfo);
            //
            // 摘要:
            //     This interface is abandoned, it is recommended to use the MV_CC_DisplayOneFrame
            //
            // 參數:
            //   hWnd:
            public int MV_CC_Display_NET(IntPtr hWnd);
            //
            // 摘要:
            //     Load camera feature
            //
            // 參數:
            //   pFileName:
            //     File name
            //
            // 傳回:
            //     Success, return MV_OK. Failure, return error code
            public int MV_CC_FeatureLoad_NET(string pFileName);
            //
            // 摘要:
            //     Save camera feature
            //
            // 參數:
            //   pFileName:
            //     File name
            //
            // 傳回:
            //     Success, return MV_OK. Failure, return error code
            public int MV_CC_FeatureSave_NET(string pFileName);
            //
            // 摘要:
            //     Read the file from the camera
            //
            // 參數:
            //   pstFileAccess:
            //     File access structure
            //
            // 傳回:
            //     Success, return MV_OK. Failure, return error code
            public int MV_CC_FileAccessRead_NET(ref MV_CC_FILE_ACCESS pstFileAccess);
            //
            // 摘要:
            //     Write the file to camera
            //
            // 參數:
            //   pstFileAccess:
            //     File access structure
            //
            // 傳回:
            //     Success, return MV_OK. Failure, return error code
            public int MV_CC_FileAccessWrite_NET(ref MV_CC_FILE_ACCESS pstFileAccess);
            //
            // 摘要:
            //     Flip Image
            //
            // 參數:
            //   pstFlipParam:
            //     Flip image parameter structure
            //
            // 傳回:
            //     Success, return MV_OK. Failure, return error code
            public int MV_CC_FlipImage_NET(ref MV_CC_FLIP_IMAGE_PARAM pstFlipParam);
            //
            // 摘要:
            //     Free image buffer（used with MV_CC_GetImageBuffer）
            //
            // 參數:
            //   pFrame:
            //     Image data and image information
            //
            // 傳回:
            //     Success, return MV_OK. Failure, return error code
            public int MV_CC_FreeImageBuffer_NET(ref MV_FRAME_OUT pFrame);
            //
            // 摘要:
            //     This interface is replaced by general interface
            //
            // 參數:
            //   pstValue:
            public int MV_CC_GetAcquisitionLineRate_NET(ref MVCC_INTVALUE pstValue);
            //
            // 摘要:
            //     This interface is replaced by general interface
            //
            // 參數:
            //   pstValue:
            public int MV_CC_GetAcquisitionMode_NET(ref MVCC_ENUMVALUE pstValue);
            //
            // 摘要:
            //     Get various type of information
            //
            // 參數:
            //   pstInfo:
            //     Various type of information
            //
            // 傳回:
            //     Success, return MV_OK. Failure, return error code
            public int MV_CC_GetAllMatchInfo_NET(ref MV_ALL_MATCH_INFO pstInfo);
            //
            // 摘要:
            //     This interface is replaced by general interface
            //
            // 參數:
            //   pstValue:
            public int MV_CC_GetAOIoffsetX_NET(ref MVCC_INTVALUE pstValue);
            //
            // 摘要:
            //     This interface is replaced by general interface
            //
            // 參數:
            //   pstValue:
            public int MV_CC_GetAOIoffsetY_NET(ref MVCC_INTVALUE pstValue);
            //
            // 摘要:
            //     This interface is replaced by general interface
            //
            // 參數:
            //   pstValue:
            public int MV_CC_GetAutoExposureTimeLower_NET(ref MVCC_INTVALUE pstValue);
            //
            // 摘要:
            //     This interface is replaced by general interface
            //
            // 參數:
            //   pstValue:
            public int MV_CC_GetAutoExposureTimeUpper_NET(ref MVCC_INTVALUE pstValue);
            //
            // 摘要:
            //     This interface is replaced by general interface
            //
            // 參數:
            //   pstValue:
            public int MV_CC_GetBalanceRatioBlue_NET(ref MVCC_INTVALUE pstValue);
            //
            // 摘要:
            //     This interface is replaced by general interface
            //
            // 參數:
            //   pstValue:
            public int MV_CC_GetBalanceRatioGreen_NET(ref MVCC_INTVALUE pstValue);
            //
            // 摘要:
            //     This interface is replaced by general interface
            //
            // 參數:
            //   pstValue:
            public int MV_CC_GetBalanceRatioRed_NET(ref MVCC_INTVALUE pstValue);
            //
            // 摘要:
            //     This interface is replaced by general interface
            //
            // 參數:
            //   pstValue:
            public int MV_CC_GetBalanceWhiteAuto_NET(ref MVCC_ENUMVALUE pstValue);
            //
            // 摘要:
            //     Get Boolean value
            //
            // 參數:
            //   strKey:
            //     Key value
            //
            //   pbValue:
            //     Value of device features
            //
            // 傳回:
            //     Success, return MV_OK. Failure, return error code
            public int MV_CC_GetBoolValue_NET(string strKey, ref bool pbValue);
            //
            // 摘要:
            //     This interface is replaced by general interface
            //
            // 參數:
            //   pstValue:
            public int MV_CC_GetBrightness_NET(ref MVCC_INTVALUE pstValue);
            //
            // 摘要:
            //     This interface is replaced by general interface
            //
            // 參數:
            //   pstValue:
            public int MV_CC_GetBurstFrameCount_NET(ref MVCC_INTVALUE pstValue);
            //
            // 摘要:
            //     Get device information(Called before start grabbing)
            //
            // 參數:
            //   pstDevInfo:
            //     device information
            //
            // 傳回:
            //     Success, return MV_OK. Failure, return error code
            public int MV_CC_GetDeviceInfo_NET(ref MV_CC_DEVICE_INFO pstDevInfo);
            //
            // 摘要:
            //     This interface is replaced by general interface
            //
            // 參數:
            //   pstValue:
            public int MV_CC_GetDeviceUserID_NET(ref MVCC_STRINGVALUE pstValue);
            //
            // 摘要:
            //     Get Enum value
            //
            // 參數:
            //   strKey:
            //     Key value, for example, using "PixelFormat" to get pixel format
            //
            //   pstValue:
            //     Value of device features
            //
            // 傳回:
            //     Success, return MV_OK. Failure, return error code
            public int MV_CC_GetEnumValue_NET(string strKey, ref MVCC_ENUMVALUE pstValue);
            //
            // 摘要:
            //     This interface is replaced by general interface
            //
            // 參數:
            //   pstValue:
            public int MV_CC_GetExposureAutoMode_NET(ref MVCC_ENUMVALUE pstValue);
            //
            // 摘要:
            //     This interface is replaced by general interface
            //
            // 參數:
            //   pstValue:
            public int MV_CC_GetExposureTime_NET(ref MVCC_FLOATVALUE pstValue);
            //
            // 摘要:
            //     Get File Access Progress
            //
            // 參數:
            //   pstFileAccessProgress:
            //     File access Progress
            //
            // 傳回:
            //     Success, return MV_OK. Failure, return error code
            public int MV_CC_GetFileAccessProgress_NET(ref MV_CC_FILE_ACCESS_PROGRESS pstFileAccessProgress);
            //
            // 摘要:
            //     Get Float value
            //
            // 參數:
            //   strKey:
            //     Key value
            //
            //   pstValue:
            //     Value of device features
            //
            // 傳回:
            //     Success, return MV_OK. Failure, return error code
            public int MV_CC_GetFloatValue_NET(string strKey, ref MVCC_FLOATVALUE pstValue);
            //
            // 摘要:
            //     This interface is replaced by general interface
            //
            // 參數:
            //   pstValue:
            public int MV_CC_GetFrameRate_NET(ref MVCC_FLOATVALUE pstValue);
            //
            // 摘要:
            //     This interface is replaced by general interface
            //
            // 參數:
            //   pstValue:
            public int MV_CC_GetGainMode_NET(ref MVCC_ENUMVALUE pstValue);
            //
            // 摘要:
            //     This interface is replaced by general interface
            //
            // 參數:
            //   pstValue:
            public int MV_CC_GetGain_NET(ref MVCC_FLOATVALUE pstValue);
            //
            // 摘要:
            //     This interface is replaced by general interface
            //
            // 參數:
            //   pstValue:
            public int MV_CC_GetGammaSelector_NET(ref MVCC_ENUMVALUE pstValue);
            //
            // 摘要:
            //     This interface is replaced by general interface
            //
            // 參數:
            //   pstValue:
            public int MV_CC_GetGamma_NET(ref MVCC_FLOATVALUE pstValue);
            //
            // 摘要:
            //     This interface is replaced by general interface
            //
            // 參數:
            //   pstValue:
            public int MV_CC_GetHeartBeatTimeout_NET(ref MVCC_INTVALUE pstValue);
            //
            // 摘要:
            //     This interface is replaced by general interface
            //
            // 參數:
            //   pstValue:
            public int MV_CC_GetHeight_NET(ref MVCC_INTVALUE pstValue);
            //
            // 摘要:
            //     This interface is replaced by general interface
            //
            // 參數:
            //   pstValue:
            public int MV_CC_GetHue_NET(ref MVCC_INTVALUE pstValue);
            //
            // 摘要:
            //     Get a frame of an image using an internal cache
            //
            // 參數:
            //   pFrame:
            //     Image data and image information
            //
            //   nMsec:
            //     Waiting timeout
            //
            // 傳回:
            //     Success, return MV_OK. Failure, return error code
            public int MV_CC_GetImageBuffer_NET(ref MV_FRAME_OUT pFrame, int nMsec);
            //
            // 摘要:
            //     Get one frame of BGR image, this function is using query to get data query whether
            //     the internal cache has data, get data if there has, return error code if no data
            //
            // 參數:
            //   pData:
            //     Image data receiving buffer
            //
            //   nDataSize:
            //     Buffer size
            //
            //   pFrameInfo:
            //     Image information
            //
            //   nMsec:
            //     Waiting timeout
            //
            // 傳回:
            //     Success, return MV_OK. Failure, return error cod
            public int MV_CC_GetImageForBGR_NET(IntPtr pData, uint nDataSize, ref MV_FRAME_OUT_INFO_EX pFrameInfo, int nMsec);
            //
            // 摘要:
            //     Get one frame of RGB image, this function is using query to get data query whether
            //     the internal cache has data, get data if there has, return error code if no data
            //
            // 參數:
            //   pData:
            //     Image data receiving buffer
            //
            //   nDataSize:
            //     Buffer size
            //
            //   pFrameInfo:
            //     Image information
            //
            //   nMsec:
            //     Waiting timeout
            //
            // 傳回:
            //     Success, return MV_OK. Failure, return error code
            public int MV_CC_GetImageForRGB_NET(IntPtr pData, uint nDataSize, ref MV_FRAME_OUT_INFO_EX pFrameInfo, int nMsec);
            //
            // 摘要:
            //     Get basic information of image (Interfaces not recommended)
            //
            // 參數:
            //   pstInfo:
            public int MV_CC_GetImageInfo_NET(ref MV_IMAGE_BASIC_INFO pstInfo);
            //
            // 摘要:
            //     Get Integer value
            //
            // 參數:
            //   strKey:
            //     Key value, for example, using "Width" to get width
            //
            //   pstValue:
            //     Value of device features
            //
            // 傳回:
            //     Success, return MV_OK. Failure, return error code
            public int MV_CC_GetIntValueEx_NET(string strKey, ref MVCC_INTVALUE_EX pstValue);
            //
            // 摘要:
            //     This interface is abandoned, it is recommended to use the MV_CC_GetIntValueEx
            //
            // 參數:
            //   strKey:
            //
            //   pstValue:
            public int MV_CC_GetIntValue_NET(string strKey, ref MVCC_INTVALUE pstValue);
            //
            // 摘要:
            //     This interface is abandoned, it is recommended to use the MV_CC_GetOneFrameTimeOut
            //
            // 參數:
            //   pData:
            //
            //   nDataSize:
            //
            //   pFrameInfo:
            public int MV_CC_GetOneFrameEx_NET(IntPtr pData, uint nDataSize, ref MV_FRAME_OUT_INFO_EX pFrameInfo);
            //
            // 摘要:
            //     Get a frame of an image
            //
            // 參數:
            //   pData:
            //     Image data receiving buffer
            //
            //   nDataSize:
            //     Buffer size
            //
            //   pFrameInfo:
            //     Image information
            //
            //   nMsec:
            //     Waiting timeout
            //
            // 傳回:
            //     Success, return MV_OK. Failure, return error code
            public int MV_CC_GetOneFrameTimeout_NET(IntPtr pData, uint nDataSize, ref MV_FRAME_OUT_INFO_EX pFrameInfo, int nMsec);
            //
            // 摘要:
            //     This interface is abandoned, it is recommended to use the MV_CC_GetOneFrameTimeOut
            //
            // 參數:
            //   pData:
            //
            //   nDataSize:
            //
            //   pFrameInfo:
            public int MV_CC_GetOneFrame_NET(IntPtr pData, uint nDataSize, ref MV_FRAME_OUT_INFO pFrameInfo);
            //
            // 摘要:
            //     Get the optimal Packet Size, Only support GigE Camera
            //
            // 傳回:
            //     Optimal packet size
            public int MV_CC_GetOptimalPacketSize_NET();
            //
            // 摘要:
            //     This interface is replaced by general interface
            //
            // 參數:
            //   pstValue:
            public int MV_CC_GetPixelFormat_NET(ref MVCC_ENUMVALUE pstValue);
            //
            // 摘要:
            //     This interface is replaced by general interface
            //
            // 參數:
            //   pstValue:
            public int MV_CC_GetSaturation_NET(ref MVCC_INTVALUE pstValue);
            //
            // 摘要:
            //     This interface is replaced by general interface
            //
            // 參數:
            //   pstValue:
            public int MV_CC_GetSharpness_NET(ref MVCC_INTVALUE pstValue);
            //
            // 摘要:
            //     Get String value
            //
            // 參數:
            //   strKey:
            //     Key value
            //
            //   pstValue:
            //     Value of device features
            //
            // 傳回:
            //     Success, return MV_OK. Failure, return error code
            public int MV_CC_GetStringValue_NET(string strKey, ref MVCC_STRINGVALUE pstValue);
            //
            // 摘要:
            //     Get GenICam proxy (Interfaces not recommended)
            public IntPtr MV_CC_GetTlProxy_NET();
            //
            // 摘要:
            //     This interface is replaced by general interface
            //
            // 參數:
            //   pstValue:
            public int MV_CC_GetTriggerDelay_NET(ref MVCC_FLOATVALUE pstValue);
            //
            // 摘要:
            //     This interface is replaced by general interface
            //
            // 參數:
            //   pstValue:
            public int MV_CC_GetTriggerMode_NET(ref MVCC_ENUMVALUE pstValue);
            public int MV_CC_GetTriggerSource_NET(ref MVCC_ENUMVALUE pstValue);
            //
            // 摘要:
            //     Get Upgrade Progress
            //
            // 參數:
            //   pnProcess:
            //     Value of Progress
            //
            // 傳回:
            //     Success, return MV_OK. Failure, return error code
            public int MV_CC_GetUpgradeProcess_NET(ref uint pnProcess);
            //
            // 摘要:
            //     This interface is replaced by general interface
            //
            // 參數:
            //   pstValue:
            public int MV_CC_GetWidth_NET(ref MVCC_INTVALUE pstValue);
            //
            // 摘要:
            //     High Bandwidth Decode
            //
            // 參數:
            //   pstDecodeParam:
            //     High Bandwidth Decode parameter structure
            //
            // 傳回:
            //     Success, return MV_OK. Failure, return error code
            public int MV_CC_HB_Decode_NET(ref MV_CC_HB_DECODE_PARAM pstDecodeParam);
            //
            // 摘要:
            //     Adjust image contrast
            //
            // 參數:
            //   pstContrastParam:
            //     Contrast parameter structure
            //
            // 傳回:
            //     Success, return MV_OK. Failure, return error code
            public int MV_CC_ImageContrast_NET(ref MV_CC_CONTRAST_PARAM pstContrastParam);
            //
            // 摘要:
            //     Image sharpen
            //
            // 參數:
            //   pstSharpenParam:
            //     Sharpen parameter structure
            //
            // 傳回:
            //     Success, return MV_OK. Failure, return error code
            public int MV_CC_ImageSharpen_NET(ref MV_CC_SHARPEN_PARAM pstSharpenParam);
            //
            // 摘要:
            //     Input RAW data to Record
            //
            // 參數:
            //   pstInputFrameInfo:
            //     Record data structure
            //
            // 傳回:
            //     Success, return MV_OK. Failure, return error code
            public int MV_CC_InputOneFrame_NET(ref MV_CC_INPUT_FRAME_INFO pstInputFrameInfo);
            //
            // 摘要:
            //     Invalidate GenICam Nodes
            //
            // 傳回:
            //     Success, return MV_OK. Failure, return error code
            public int MV_CC_InvalidateNodes_NET();
            //
            // 摘要:
            //     Is the device connected
            //
            // 傳回:
            //     Connected, return true. Not Connected or DIsconnected, return false
            public bool MV_CC_IsDeviceConnected_NET();
            //
            // 摘要:
            //     Device Local Upgrade
            //
            // 參數:
            //   pFilePathName:
            //     File path and name
            //
            // 傳回:
            //     Success, return MV_OK. Failure, return error code
            public int MV_CC_LocalUpgrade_NET(string pFilePathName);
            //
            // 摘要:
            //     LSC Calib
            //
            // 參數:
            //   pstLSCCalibParam:
            //     LSC Calib parameter structure
            //
            // 傳回:
            //     Success, return MV_OK. Failure, return error code
            public int MV_CC_LSCCalib_NET(ref MV_CC_LSC_CALIB_PARAM pstLSCCalibParam);
            //
            // 摘要:
            //     LSC Correct
            //
            // 參數:
            //   pstLSCCorrectParam:
            //     LSC Correct parameter structure
            //
            // 傳回:
            //     Success, return MV_OK. Failure, return error code
            public int MV_CC_LSCCorrect_NET(ref MV_CC_LSC_CORRECT_PARAM pstLSCCorrectParam);
            //
            // 摘要:
            //     Noise Estimate
            //
            // 參數:
            //   pstNoiseEstimateParam:
            //     Noise Estimate parameter structure
            //
            // 傳回:
            //     Success, return MV_OK. Failure, return error code
            public int MV_CC_NoiseEstimate_NET(ref MV_CC_NOISE_ESTIMATE_PARAM pstNoiseEstimateParam);
            //
            // 摘要:
            //     Open Device
            //
            // 傳回:
            //     Success, return MV_OK. Failure, return error code
            public int MV_CC_OpenDevice_NET();
            //
            // 摘要:
            //     Open Device
            //
            // 參數:
            //   nAccessMode:
            //     Access Right
            //
            //   nSwitchoverKey:
            //     Switch key of access right
            //
            // 傳回:
            //     Success, return MV_OK. Failure, return error code
            public int MV_CC_OpenDevice_NET(uint nAccessMode, ushort nSwitchoverKey);
            //
            // 摘要:
            //     Read Memory
            //
            // 參數:
            //   pBuffer:
            //     Used as a return value, save the read-in memory value(Memory value is stored
            //     in accordance with the big end model)
            //
            //   nAddress:
            //     Memory address to be read, which can be obtained from the Camera.xml file of
            //     the device, the form xml node value of xxx_RegAddr
            //
            //   nLength:
            //     Length of the memory to be read
            //
            // 傳回:
            //     Success, return MV_OK. Failure, return error code
            public int MV_CC_ReadMemory_NET(IntPtr pBuffer, long nAddress, long nLength);
            //
            // 摘要:
            //     Register event callback, which is called after the device is opened
            //
            // 參數:
            //   cbEvent:
            //     Event CallBack Function
            //
            //   pUser:
            //     User defined variable
            //
            // 傳回:
            //     Success, return MV_OK. Failure, return error code
            public int MV_CC_RegisterAllEventCallBack_NET(cbEventdelegateEx cbEvent, IntPtr pUser);
            //
            // 摘要:
            //     Register single event callback, which is called after the device is opened
            //
            // 參數:
            //   pEventName:
            //     Event name
            //
            //   cbEvent:
            //     Event CallBack Function
            //
            //   pUser:
            //     User defined variable
            //
            // 傳回:
            //     Success, return MV_OK. Failure, return error code
            public int MV_CC_RegisterEventCallBackEx_NET(string pEventName, cbEventdelegateEx cbEvent, IntPtr pUser);
            //
            // 摘要:
            //     This interface is abandoned, it is recommended to use the MV_CC_RegisterEventCallBackEx
            //
            // 參數:
            //   cbEvent:
            //
            //   pUser:
            public int MV_CC_RegisterEventCallBack_NET(cbEventdelegate cbEvent, IntPtr pUser);
            //
            // 摘要:
            //     Register Exception Message CallBack, call after open device
            //
            // 參數:
            //   cbException:
            //     Exception Message CallBack Function
            //
            //   pUser:
            //     User defined variable
            //
            // 傳回:
            //     Success, return MV_OK. Failure, return error code
            public int MV_CC_RegisterExceptionCallBack_NET(cbExceptiondelegate cbException, IntPtr pUser);
            //
            // 摘要:
            //     Register the image callback function
            //
            // 參數:
            //   cbOutput:
            //     Callback function pointer
            //
            //   pUser:
            //     User defined variable
            //
            // 傳回:
            //     Success, return MV_OK. Failure, return error code
            public int MV_CC_RegisterImageCallBackEx_NET(cbOutputExdelegate cbOutput, IntPtr pUser);
            //
            // 摘要:
            //     Register the BGR image callback function
            //
            // 參數:
            //   cbOutput:
            //     Callback function pointer
            //
            //   pUser:
            //     User defined variable
            //
            // 傳回:
            //     Success, return MV_OK. Failure, return error code
            public int MV_CC_RegisterImageCallBackForBGR_NET(cbOutputExdelegate cbOutput, IntPtr pUser);
            //
            // 摘要:
            //     Register the RGB image callback function
            //
            // 參數:
            //   cbOutput:
            //     Callback function pointer
            //
            //   pUser:
            //     User defined variable
            //
            // 傳回:
            //     Success, return MV_OK. Failure, return error code
            public int MV_CC_RegisterImageCallBackForRGB_NET(cbOutputExdelegate cbOutput, IntPtr pUser);
            //
            // 摘要:
            //     This interface is abandoned, it is recommended to use the MV_CC_RegisterImageCallBackEx
            //
            // 參數:
            //   cbOutput:
            //
            //   pUser:
            public int MV_CC_RegisterImageCallBack_NET(cbOutputdelegate cbOutput, IntPtr pUser);
            //
            // 摘要:
            //     Rotate Image
            //
            // 參數:
            //   pstRotateParam:
            //     Rotate image parameter structure
            //
            // 傳回:
            //     Success, return MV_OK. Failure, return error code
            public int MV_CC_RotateImage_NET(ref MV_CC_ROTATE_IMAGE_PARAM pstRotateParam);
            //
            // 摘要:
            //     Save image, support Bmp and Jpeg. Encoding quality(50-99]
            //
            // 參數:
            //   stSaveParam:
            //     Save image parameters structure
            //
            // 傳回:
            //     Success, return MV_OK. Failure, return error code
            public int MV_CC_SaveImageEx_NET(ref MV_SAVE_IMAGE_PARAM_EX stSaveParam);
            //
            // 摘要:
            //     Save the image file, support Bmp、 Jpeg、Png and Tiff. Encoding quality(50-99]
            //
            // 參數:
            //   pstSaveFileParam:
            //     Save the image file parameter structure
            //
            // 傳回:
            //     Success, return MV_OK. Failure, return error code
            public int MV_CC_SaveImageToFile_NET(ref MV_SAVE_IMG_TO_FILE_PARAM pstSaveFileParam);
            //
            // 摘要:
            //     This interface is abandoned, it is recommended to use the MV_CC_SaveImageEx
            //
            // 參數:
            //   stSaveParam:
            public int MV_CC_SaveImage_NET(ref MV_SAVE_IMAGE_PARAM stSaveParam);
            //
            // 摘要:
            //     Save 3D point data, support PLY、CSV and OBJ
            //
            // 參數:
            //   pstPointDataParam:
            //     Save 3D point data parameters structure
            //
            // 傳回:
            //     Success, return MV_OK. Failure, return error code
            public int MV_CC_SavePointCloudData_NET(ref MV_SAVE_POINT_CLOUD_PARAM pstPointDataParam);
            //
            // 摘要:
            //     This interface is replaced by general interface
            //
            // 參數:
            //   nValue:
            public int MV_CC_SetAcquisitionLineRate_NET(uint nValue);
            //
            // 摘要:
            //     This interface is replaced by general interface
            //
            // 參數:
            //   nValue:
            public int MV_CC_SetAcquisitionMode_NET(uint nValue);
            //
            // 摘要:
            //     This interface is replaced by general interface
            //
            // 參數:
            //   nValue:
            public int MV_CC_SetAOIoffsetX_NET(uint nValue);
            //
            // 摘要:
            //     This interface is replaced by general interface
            //
            // 參數:
            //   nValue:
            public int MV_CC_SetAOIoffsetY_NET(uint nValue);
            //
            // 摘要:
            //     This interface is replaced by general interface
            //
            // 參數:
            //   nValue:
            public int MV_CC_SetAutoExposureTimeLower_NET(uint nValue);
            //
            // 摘要:
            //     This interface is replaced by general interface
            //
            // 參數:
            //   nValue:
            public int MV_CC_SetAutoExposureTimeUpper_NET(uint nValue);
            //
            // 摘要:
            //     This interface is replaced by general interface
            //
            // 參數:
            //   nValue:
            public int MV_CC_SetBalanceRatioBlue_NET(uint nValue);
            //
            // 摘要:
            //     This interface is replaced by general interface
            //
            // 參數:
            //   nValue:
            public int MV_CC_SetBalanceRatioGreen_NET(uint nValue);
            //
            // 摘要:
            //     This interface is replaced by general interface
            //
            // 參數:
            //   nValue:
            public int MV_CC_SetBalanceRatioRed_NET(uint nValue);
            //
            // 摘要:
            //     This interface is replaced by general interface
            //
            // 參數:
            //   nValue:
            public int MV_CC_SetBalanceWhiteAuto_NET(uint nValue);
            //
            // 摘要:
            //     Set CCM param
            //
            // 參數:
            //   pstCCMParam:
            //     CCM parameter structure
            //
            // 傳回:
            //     Success, return MV_OK. Failure, return error code
            public int MV_CC_SetBayerCCMParamEx_NET(ref MV_CC_CCM_PARAM_EX pstCCMParam);
            //
            // 摘要:
            //     Set CCM param
            //
            // 參數:
            //   pstCCMParam:
            //     CCM parameter structure
            //
            // 傳回:
            //     Success, return MV_OK. Failure, return error code
            public int MV_CC_SetBayerCCMParam_NET(ref MV_CC_CCM_PARAM pstCCMParam);
            //
            // 摘要:
            //     Set CLUT param
            //
            // 參數:
            //   pstCLUTParam:
            //     CLUT parameter structure
            //
            // 傳回:
            //     Success, return MV_OK. Failure, return error code
            public int MV_CC_SetBayerCLUTParam_NET(ref MV_CC_CLUT_PARAM pstCLUTParam);
            //
            // 摘要:
            //     Interpolation algorithm type setting
            //
            // 參數:
            //   BayerCvtQuality:
            //     Bayer interpolation method 0-Fast 1-Equilibrium 2-Optimal
            //
            // 傳回:
            //     Success, return MV_OK. Failure, return error code
            public int MV_CC_SetBayerCvtQuality_NET(uint BayerCvtQuality);
            //
            // 摘要:
            //     Set Gamma param
            //
            // 參數:
            //   pstGammaParam:
            //     Gamma parameter structure
            //
            // 傳回:
            //     Success, return MV_OK. Failure, return error code
            public int MV_CC_SetBayerGammaParam_NET(ref MV_CC_GAMMA_PARAM pstGammaParam);
            //
            // 摘要:
            //     Set Gamma value
            //
            // 參數:
            //   fBayerGammaValue:
            //     Gamma value[0.1,4.0]
            //
            // 傳回:
            //     Success, return MV_OK. Failure, return error code
            public int MV_CC_SetBayerGammaValue_NET(float fBayerGammaValue);
            //
            // 摘要:
            //     Set Boolean value
            //
            // 參數:
            //   strKey:
            //     Key value
            //
            //   bValue:
            //     Feature value to set
            //
            // 傳回:
            //     Success, return MV_OK. Failure, return error code
            public int MV_CC_SetBoolValue_NET(string strKey, bool bValue);
            //
            // 摘要:
            //     This interface is replaced by general interface
            //
            // 參數:
            //   nValue:
            public int MV_CC_SetBrightness_NET(uint nValue);
            //
            // 摘要:
            //     This interface is replaced by general interface
            //
            // 參數:
            //   nValue:
            public int MV_CC_SetBurstFrameCount_NET(uint nValue);
            //
            // 摘要:
            //     Send Command
            //
            // 參數:
            //   strKey:
            //     Key value
            //
            // 傳回:
            //     Success, return MV_OK. Failure, return error code
            public int MV_CC_SetCommandValue_NET(string strKey);
            //
            // 摘要:
            //     This interface is replaced by general interface
            //
            // 參數:
            //   chValue:
            public int MV_CC_SetDeviceUserID_NET(string chValue);
            //
            // 摘要:
            //     Set Enum value
            //
            // 參數:
            //   strKey:
            //     Key value, for example, using "PixelFormat" to set pixel format
            //
            //   sValue:
            //     Feature String to set
            //
            // 傳回:
            //     Success, return MV_OK. Failure, return error code
            public int MV_CC_SetEnumValueByString_NET(string strKey, string sValue);
            //
            // 摘要:
            //     Set Enum value
            //
            // 參數:
            //   strKey:
            //     Key value, for example, using "PixelFormat" to set pixel format
            //
            //   nValue:
            //     Feature value to set
            //
            // 傳回:
            //     Success, return MV_OK. Failure, return error code
            public int MV_CC_SetEnumValue_NET(string strKey, uint nValue);
            //
            // 摘要:
            //     This interface is replaced by general interface
            //
            // 參數:
            //   nValue:
            public int MV_CC_SetExposureAutoMode_NET(uint nValue);
            //
            // 摘要:
            //     This interface is replaced by general interface
            //
            // 參數:
            //   fValue:
            public int MV_CC_SetExposureTime_NET(float fValue);
            //
            // 摘要:
            //     Set float value
            //
            // 參數:
            //   strKey:
            //     Key value
            //
            //   fValue:
            //     Feature value to set
            //
            // 傳回:
            //     Success, return MV_OK. Failure, return error code
            public int MV_CC_SetFloatValue_NET(string strKey, float fValue);
            //
            // 摘要:
            //     This interface is replaced by general interface
            //
            // 參數:
            //   fValue:
            public int MV_CC_SetFrameRate_NET(float fValue);
            //
            // 摘要:
            //     This interface is replaced by general interface
            //
            // 參數:
            //   nValue:
            public int MV_CC_SetGainMode_NET(uint nValue);
            //
            // 摘要:
            //     This interface is replaced by general interface
            //
            // 參數:
            //   fValue:
            public int MV_CC_SetGain_NET(float fValue);
            //
            // 摘要:
            //     This interface is replaced by general interface
            //
            // 參數:
            //   nValue:
            public int MV_CC_SetGammaSelector_NET(uint nValue);
            //
            // 摘要:
            //     This interface is replaced by general interface
            //
            // 參數:
            //   fValue:
            public int MV_CC_SetGamma_NET(float fValue);
            //
            // 摘要:
            //     Set Grab Strategy
            //
            // 參數:
            //   enGrabStrategy:
            //     The value of grab strategy
            //
            // 傳回:
            //     Success, return MV_OK. Failure, return error code
            public int MV_CC_SetGrabStrategy_NET(MV_GRAB_STRATEGY enGrabStrategy);
            //
            // 摘要:
            //     This interface is replaced by general interface
            //
            // 參數:
            //   nValue:
            public int MV_CC_SetHeartBeatTimeout_NET(uint nValue);
            //
            // 摘要:
            //     This interface is replaced by general interface
            //
            // 參數:
            //   nValue:
            public int MV_CC_SetHeight_NET(uint nValue);
            //
            // 摘要:
            //     This interface is replaced by general interface
            //
            // 參數:
            //   nValue:
            public int MV_CC_SetHue_NET(uint nValue);
            //
            // 摘要:
            //     Set the number of the internal image cache nodes in SDK(Greater than or equal
            //     to 1, to be called before the capture)
            //
            // 參數:
            //   nNum:
            //     Number of cache nodes
            //
            // 傳回:
            //     Success, return MV_OK. Failure, return error code
            public int MV_CC_SetImageNodeNum_NET(uint nNum);
            //
            // 摘要:
            //     Set Integer value
            //
            // 參數:
            //   strKey:
            //     Key value, for example, using "Width" to set width
            //
            //   nValue:
            //     Feature value to set
            //
            // 傳回:
            //     Success, return MV_OK. Failure, return error code
            public int MV_CC_SetIntValueEx_NET(string strKey, long nValue);
            //
            // 摘要:
            //     This interface is abandoned, it is recommended to use the MV_CC_SetIntValueEx
            //
            // 參數:
            //   strKey:
            //
            //   nValue:
            public int MV_CC_SetIntValue_NET(string strKey, uint nValue);
            //
            // 摘要:
            //     Set The Size of Output Queue(Only work under the strategy of MV_GrabStrategy_LatestImages，rang：1-ImageNodeNum)
            //
            // 參數:
            //   nOutputQueueSize:
            //     The Size of Output Queue
            //
            // 傳回:
            //     Success, return MV_OK. Failure, return error code
            public int MV_CC_SetOutputQueueSize_NET(uint nOutputQueueSize);
            public int MV_CC_SetPixelFormat_NET(uint nValue);
            //
            // 摘要:
            //     This interface is replaced by general interface
            //
            // 參數:
            //   nValue:
            public int MV_CC_SetSaturation_NET(uint nValue);
            //
            // 摘要:
            //     This interface is replaced by general interface
            //
            // 參數:
            //   nValue:
            public int MV_CC_SetSharpness_NET(uint nValue);
            //
            // 摘要:
            //     Set String value
            //
            // 參數:
            //   strKey:
            //     Key value
            //
            //   strValue:
            //     Feature value to set
            //
            // 傳回:
            //     Success, return MV_OK. Failure, return error code
            public int MV_CC_SetStringValue_NET(string strKey, string strValue);
            //
            // 摘要:
            //     This interface is replaced by general interface
            //
            // 參數:
            //   fValue:
            public int MV_CC_SetTriggerDelay_NET(float fValue);
            //
            // 摘要:
            //     This interface is replaced by general interface
            //
            // 參數:
            //   nValue:
            public int MV_CC_SetTriggerMode_NET(uint nValue);
            //
            // 摘要:
            //     This interface is replaced by general interface
            //
            // 參數:
            //   nValue:
            public int MV_CC_SetTriggerSource_NET(uint nValue);
            //
            // 摘要:
            //     This interface is replaced by general interface
            //
            // 參數:
            //   nValue:
            public int MV_CC_SetWidth_NET(uint nValue);
            //
            // 摘要:
            //     Spatial Denoise
            //
            // 參數:
            //   pstSpatialDenoiseParam:
            //     Spatial Denoise parameter structure
            //
            // 傳回:
            //     Success, return MV_OK. Failure, return error code
            public int MV_CC_SpatialDenoise_NET(ref MV_CC_SPATIAL_DENOISE_PARAM pstSpatialDenoiseParam);
            //
            // 摘要:
            //     Start Grabbing
            //
            // 傳回:
            //     Success, return MV_OK. Failure, return error code
            public int MV_CC_StartGrabbing_NET();
            //
            // 摘要:
            //     Start Record
            //
            // 參數:
            //   pstRecordParam:
            //     Record param structure
            //
            // 傳回:
            //     Success, return MV_OK. Failure, return error code
            public int MV_CC_StartRecord_NET(ref MV_CC_RECORD_PARAM pstRecordParam);
            //
            // 摘要:
            //     Stop Grabbing
            //
            // 傳回:
            //     Success, return MV_OK. Failure, return error code
            public int MV_CC_StopGrabbing_NET();
            //
            // 摘要:
            //     Stop Record
            //
            // 傳回:
            //     Success, return MV_OK. Failure, return error code
            public int MV_CC_StopRecord_NET();
            //
            // 摘要:
            //     This interface is replaced by general interface
            public int MV_CC_TriggerSoftwareExecute_NET();
            //
            // 摘要:
            //     Write Memory
            //
            // 參數:
            //   pBuffer:
            //     Memory value to be written ( Note the memory value to be stored in accordance
            //     with the big end model)
            //
            //   nAddress:
            //     Memory address to be written, which can be obtained from the Camera.xml file
            //     of the device, the form xml node value of xxx_RegAddr
            //
            //   nLength:
            //     Length of the memory to be written
            //
            // 傳回:
            //     Success, return MV_OK. Failure, return error code
            public int MV_CC_WriteMemory_NET(IntPtr pBuffer, long nAddress, long nLength);
            //
            // 摘要:
            //     Force IP
            //
            // 參數:
            //   nIP:
            //     IP to set
            //
            //   nSubNetMask:
            //     Subnet mask
            //
            //   nDefaultGateWay:
            //     Default gateway
            //
            // 傳回:
            //     Success, return MV_OK. Failure, return error code
            public int MV_GIGE_ForceIpEx_NET(uint nIP, uint nSubNetMask, uint nDefaultGateWay);
            //
            // 摘要:
            //     This interface is abandoned, it is recommended to use the MV_GIGE_ForceIpEx
            //
            // 參數:
            //   nIP:
            public int MV_GIGE_ForceIp_NET(uint nIP);
            //
            // 摘要:
            //     This interface is replaced by general interface
            //
            // 參數:
            //   pnIP:
            public int MV_GIGE_GetGevSCDA_NET(ref uint pnIP);
            //
            // 摘要:
            //     This interface is replaced by general interface
            //
            // 參數:
            //   pstValue:
            public int MV_GIGE_GetGevSCPD_NET(ref MVCC_INTVALUE pstValue);
            //
            // 摘要:
            //     This interface is replaced by general interface
            //
            // 參數:
            //   pstValue:
            public int MV_GIGE_GetGevSCPSPacketSize_NET(ref MVCC_INTVALUE pstValue);
            //
            // 摘要:
            //     This interface is replaced by general interface
            //
            // 參數:
            //   pnPort:
            public int MV_GIGE_GetGevSCSP_NET(ref uint pnPort);
            //
            // 摘要:
            //     Get GVCP cammand timeout
            //
            // 參數:
            //   pMillisec:
            //     Timeout, ms as unit
            //
            // 傳回:
            //     Success, return MV_OK. Failure, return error code
            public int MV_GIGE_GetGvcpTimeout_NET(ref uint pMillisec);
            //
            // 摘要:
            //     Get GVSP streaming timeout
            //
            // 參數:
            //   pMillisec:
            //     Timeout, ms as unit
            //
            // 傳回:
            //     Success, return MV_OK. Failure, return error code
            public int MV_GIGE_GetGvspTimeout_NET(ref uint pMillisec);
            //
            // 摘要:
            //     Get net transmission information
            //
            // 參數:
            //   pstInfo:
            //     Transmission information
            //
            // 傳回:
            //     Success, return MV_OK. Failure, return error code
            public int MV_GIGE_GetNetTransInfo_NET(ref MV_NETTRANS_INFO pstInfo);
            //
            // 摘要:
            //     Get the max resend retry times
            //
            // 參數:
            //   pnRetryTimes:
            //     the max times to retry resending lost packets
            //
            // 傳回:
            //     Success, return MV_OK. Failure, return error code
            public int MV_GIGE_GetResendMaxRetryTimes_NET(ref uint pnRetryTimes);
            //
            // 摘要:
            //     Get time interval between same resend requests
            //
            // 參數:
            //   pnMillisec:
            //     The time interval between same resend requests
            //
            // 傳回:
            //     Success, return MV_OK. Failure, return error code
            public int MV_GIGE_GetResendTimeInterval_NET(ref uint pnMillisec);
            //
            // 摘要:
            //     Get the number of retry GVCP cammand
            //
            // 參數:
            //   pRetryGvcpTimes:
            //     The number of retries
            //
            // 傳回:
            //     Success, return MV_OK. Failure, return error code
            public int MV_GIGE_GetRetryGvcpTimes_NET(ref uint pRetryGvcpTimes);
            //
            // 摘要:
            //     Issue Action Command
            //
            // 參數:
            //   pstActionCmdInfo:
            //     Action Command info
            //
            //   pstActionCmdResults:
            //     Action Command Result List
            //
            // 傳回:
            //     Success, return MV_OK. Failure, return error code
            public int MV_GIGE_IssueActionCommand_NET(ref MV_ACTION_CMD_INFO pstActionCmdInfo, ref MV_ACTION_CMD_RESULT_LIST pstActionCmdResults);
            //
            // 摘要:
            //     Setting the ACK mode of devices Discovery
            //
            // 參數:
            //   nMode:
            //     ACK mode（Default-Broadcast）,0-Unicast,1-Broadcast
            //
            // 傳回:
            //     Success, return MV_OK. Failure, return error code
            public int MV_GIGE_SetDiscoveryMode_NET(uint nMode);
            //
            // 摘要:
            //     This interface is replaced by general interface
            //
            // 參數:
            //   nIP:
            public int MV_GIGE_SetGevSCDA_NET(uint nIP);
            //
            // 摘要:
            //     This interface is replaced by general interface
            //
            // 參數:
            //   nValue:
            public int MV_GIGE_SetGevSCPD_NET(uint nValue);
            //
            // 摘要:
            //     This interface is replaced by general interface
            //
            // 參數:
            //   nValue:
            public int MV_GIGE_SetGevSCPSPacketSize_NET(uint nValue);
            //
            // 摘要:
            //     This interface is replaced by general interface
            //
            // 參數:
            //   nPort:
            public int MV_GIGE_SetGevSCSP_NET(uint nPort);
            //
            // 摘要:
            //     Set GVCP cammand timeout
            //
            // 參數:
            //   nMillisec:
            //     Timeout, ms as unit, range: 0-10000
            //
            // 傳回:
            //     Success, return MV_OK. Failure, return error code
            public int MV_GIGE_SetGvcpTimeout_NET(uint nMillisec);
            //
            // 摘要:
            //     Set GVSP streaming timeout
            //
            // 參數:
            //   nMillisec:
            //     Timeout, default 300ms, range: >10ms
            //
            // 傳回:
            //     Success, return MV_OK. Failure, return error code
            public int MV_GIGE_SetGvspTimeout_NET(uint nMillisec);
            //
            // 摘要:
            //     IP configuration method
            //
            // 參數:
            //   nType:
            //     IP type, refer to MV_IP_CFG_x
            //
            // 傳回:
            //     Success, return MV_OK. Failure, return error code
            public int MV_GIGE_SetIpConfig_NET(uint nType);
            //
            // 摘要:
            //     Set to use only one mode,type: MV_NET_TRANS_x. When do not set, priority is to
            //     use driver by default
            //
            // 參數:
            //   nType:
            //     Net transmission mode, refer to MV_NET_TRANS_x
            //
            // 傳回:
            //     Success, return MV_OK. Failure, return error code
            public int MV_GIGE_SetNetTransMode_NET(uint nType);
            //
            // 摘要:
            //     Set the max resend retry times
            //
            // 參數:
            //   nRetryTimes:
            //     The max times to retry resending lost packets，default 20
            //
            // 傳回:
            //     Success, return MV_OK. Failure, return error code
            public int MV_GIGE_SetResendMaxRetryTimes_NET(uint nRetryTimes);
            //
            // 摘要:
            //     Set time interval between same resend requests
            //
            // 參數:
            //   nMillisec:
            //     The time interval between same resend requests,default 10ms
            //
            // 傳回:
            //     Success, return MV_OK. Failure, return error code
            public int MV_GIGE_SetResendTimeInterval_NET(uint nMillisec);
            //
            // 摘要:
            //     Set whethe to enable resend, and set resend
            //
            // 參數:
            //   bEnable:
            //     Enable resend
            //
            //   nMaxResendPercent:
            //     Max resend persent
            //
            //   nResendTimeout:
            //     Resend timeout
            //
            // 傳回:
            //     Success, return MV_OK. Failure, return error code
            public int MV_GIGE_SetResend_NET(uint bEnable, uint nMaxResendPercent, uint nResendTimeout);
            //
            // 摘要:
            //     Set the number of retry GVCP cammand
            //
            // 參數:
            //   nRetryGvcpTimes:
            //     The number of retries，rang：0-100
            //
            // 傳回:
            //     Success, return MV_OK. Failure, return error code
            public int MV_GIGE_SetRetryGvcpTimes_NET(uint nRetryGvcpTimes);
            //
            // 摘要:
            //     Set transmission type,Unicast or Multicast
            //
            // 參數:
            //   pstTransmissionType:
            //     Struct of transmission type
            //
            // 傳回:
            //     Success, return MV_OK. Failure, return error code
            public int MV_GIGE_SetTransmissionType_NET(ref MV_CC_TRANSMISSION_TYPE pstTransmissionType);
            //
            // 摘要:
            //     Get transfer size of U3V device
            //
            // 參數:
            //   pTransferSize:
            //     Transfer size，Byte
            //
            // 傳回:
            //     Success, return MV_OK. Failure, return error code
            public int MV_USB_GetTransferSize_NET(ref uint pTransferSize);
            //
            // 摘要:
            //     Get transfer ways of U3V device
            //
            // 參數:
            //   pTransferWays:
            //     Transfer ways
            //
            // 傳回:
            //     Success, return MV_OK. Failure, return error code
            public int MV_USB_GetTransferWays_NET(ref uint pTransferWays);
            //
            // 摘要:
            //     Set transfer size of U3V device
            //
            // 參數:
            //   nTransferSize:
            //     Transfer size，Byte，default：1M，rang：>=0x10000
            //
            // 傳回:
            //     Success, return MV_OK. Failure, return error code
            public int MV_USB_SetTransferSize_NET(uint nTransferSize);
            //
            // 摘要:
            //     Set transfer ways of U3V device
            //
            // 參數:
            //   nTransferWays:
            //     Transfer ways，rang：1-10
            //
            // 傳回:
            //     Success, return MV_OK. Failure, return error code
            public int MV_USB_SetTransferWays_NET(uint nTransferWays);
            //
            // 摘要:
            //     Get all children node of specific node from xml, root node is Root (Interfaces
            //     not recommended)
            //
            // 參數:
            //   pstNode:
            //
            //   pstNodesList:
            public int MV_XML_GetChildren_NET(ref MV_XML_NODE_FEATURE pstNode, ref MV_XML_NODES_LIST pstNodesList);
            //
            // 摘要:
            //     Get all children node of specific node from xml, root node is Root (Interfaces
            //     not recommended)
            //
            // 參數:
            //   pstNode:
            //
            //   pstNodesList:
            public int MV_XML_GetChildren_NET(ref MV_XML_NODE_FEATURE pstNode, IntPtr pstNodesList);
            //
            // 摘要:
            //     Get camera feature tree XML
            //
            // 參數:
            //   pData:
            //     XML data receiving buffer
            //
            //   nDataSize:
            //     Buffer size
            //
            //   pnDataLen:
            //     Actual data length
            //
            // 傳回:
            //     Success, return MV_OK. Failure, return error code
            public int MV_XML_GetGenICamXML_NET(IntPtr pData, uint nDataSize, ref uint pnDataLen);
            //
            // 摘要:
            //     Get Access mode of cur node
            //
            // 參數:
            //   pstrName:
            //     Name of node
            //
            //   pAccessMode:
            //     Access mode of the node
            //
            // 傳回:
            //     Success, return MV_OK. Failure, return error code
            public int MV_XML_GetNodeAccessMode_NET(string pstrName, ref MV_XML_AccessMode pAccessMode);
            //
            // 摘要:
            //     Get current node feature (Interfaces not recommended)
            //
            // 參數:
            //   pstNode:
            //
            //   pstFeature:
            public int MV_XML_GetNodeFeature_NET(ref MV_XML_NODE_FEATURE pstNode, IntPtr pstFeature);
            //
            // 摘要:
            //     Get Interface Type of cur node
            //
            // 參數:
            //   pstrName:
            //     Name of node
            //
            //   pInterfaceType:
            //     Interface Type of the node
            //
            // 傳回:
            //     Success, return MV_OK. Failure, return error code
            public int MV_XML_GetNodeInterfaceType_NET(string pstrName, ref MV_XML_InterfaceType pInterfaceType);
            //
            // 摘要:
            //     Get root node (Interfaces not recommended)
            //
            // 參數:
            //   pstNode:
            public int MV_XML_GetRootNode_NET(ref MV_XML_NODE_FEATURE pstNode);
            //
            // 摘要:
            //     Register update callback (Interfaces not recommended)
            //
            // 參數:
            //   cbXmlUpdate:
            //
            //   pUser:
            public int MV_XML_RegisterUpdateCallBack_NET(cbXmlUpdatedelegate cbXmlUpdate, IntPtr pUser);
            //
            // 摘要:
            //     Update node (Interfaces not recommended)
            //
            // 參數:
            //   enType:
            //
            //   pstFeature:
            public int MV_XML_UpdateNodeFeature_NET(MV_XML_InterfaceType enType, IntPtr pstFeature);
#endif
            public enum MV_IMG_FLIP_TYPE
            {
                MV_FLIP_VERTICAL = 1,
                MV_FLIP_HORIZONTAL = 2
            }
            public enum MV_CC_GAMMA_TYPE
            {
                MV_CC_GAMMA_TYPE_NONE = 0,
                MV_CC_GAMMA_TYPE_VALUE = 1,
                MV_CC_GAMMA_TYPE_USER_CURVE = 2,
                MV_CC_GAMMA_TYPE_LRGB2SRGB = 3,
                MV_CC_GAMMA_TYPE_SRGB2LRGB = 4
            }
            public enum MV_CAM_ACQUISITION_MODE
            {
                MV_ACQ_MODE_SINGLE = 0,
                MV_ACQ_MODE_MUTLI = 1,
                MV_ACQ_MODE_CONTINUOUS = 2
            }
            public enum MV_CC_BAYER_NOISE_FEATURE_TYPE
            {
                MV_CC_BAYER_NOISE_FEATURE_TYPE_INVALID = 0,
                MV_CC_BAYER_NOISE_FEATURE_TYPE_PROFILE = 1,
                MV_CC_BAYER_NOISE_FEATURE_TYPE_LEVEL = 2,
                MV_CC_BAYER_NOISE_FEATURE_TYPE_DEFAULT = 2
            }
            public enum MV_RECORD_FORMAT_TYPE
            {
                MV_FormatType_Undefined = 0,
                MV_FormatType_AVI = 1
            }
            public enum MV_SAVE_POINT_CLOUD_FILE_TYPE
            {
                MV_PointCloudFile_Undefined = 0,
                MV_PointCloudFile_PLY = 1,
                MV_PointCloudFile_CSV = 2,
                MV_PointCloudFile_OBJ = 3
            }
            public enum MV_IMG_ROTATION_ANGLE
            {
                MV_IMAGE_ROTATE_90 = 1,
                MV_IMAGE_ROTATE_180 = 2,
                MV_IMAGE_ROTATE_270 = 3
            }
            public enum MV_CAM_GAIN_MODE
            {
                MV_GAIN_MODE_OFF = 0,
                MV_GAIN_MODE_ONCE = 1,
                MV_GAIN_MODE_CONTINUOUS = 2
            }
            public enum MV_CAM_BALANCEWHITE_AUTO
            {
                MV_BALANCEWHITE_AUTO_OFF = 0,
                MV_BALANCEWHITE_AUTO_CONTINUOUS = 1,
                MV_BALANCEWHITE_AUTO_ONCE = 2
            }
            public enum MV_CAM_EXPOSURE_AUTO_MODE
            {
                MV_EXPOSURE_AUTO_MODE_OFF = 0,
                MV_EXPOSURE_AUTO_MODE_ONCE = 1,
                MV_EXPOSURE_AUTO_MODE_CONTINUOUS = 2
            }
            public enum MV_CAM_TRIGGER_MODE
            {
                MV_TRIGGER_MODE_OFF = 0,
                MV_TRIGGER_MODE_ON = 1
            }
            public enum MV_CAM_GAMMA_SELECTOR
            {
                MV_GAMMA_SELECTOR_USER = 1,
                MV_GAMMA_SELECTOR_SRGB = 2
            }
            public enum MV_SAVE_IAMGE_TYPE
            {
                MV_Image_Undefined = 0,
                MV_Image_Bmp = 1,
                MV_Image_Jpeg = 2,
                MV_Image_Png = 3,
                MV_Image_Tif = 4
            }
            public enum MV_CAM_TRIGGER_SOURCE
            {
                MV_TRIGGER_SOURCE_LINE0 = 0,
                MV_TRIGGER_SOURCE_LINE1 = 1,
                MV_TRIGGER_SOURCE_LINE2 = 2,
                MV_TRIGGER_SOURCE_LINE3 = 3,
                MV_TRIGGER_SOURCE_COUNTER0 = 4,
                MV_TRIGGER_SOURCE_SOFTWARE = 7,
                MV_TRIGGER_SOURCE_FrequencyConverter = 8
            }
            public enum MV_GIGE_TRANSMISSION_TYPE
            {
                MV_GIGE_TRANSTYPE_UNICAST = 0,
                MV_GIGE_TRANSTYPE_MULTICAST = 1,
                MV_GIGE_TRANSTYPE_LIMITEDBROADCAST = 2,
                MV_GIGE_TRANSTYPE_SUBNETBROADCAST = 3,
                MV_GIGE_TRANSTYPE_CAMERADEFINED = 4,
                MV_GIGE_TRANSTYPE_UNICAST_DEFINED_PORT = 5,
                MV_GIGE_TRANSTYPE_UNICAST_WITHOUT_RECV = 65536,
                MV_GIGE_TRANSTYPE_MULTICAST_WITHOUT_RECV = 65537
            }
            public enum MV_XML_InterfaceType
            {
                IFT_IValue = 0,
                IFT_IBase = 1,
                IFT_IInteger = 2,
                IFT_IBoolean = 3,
                IFT_ICommand = 4,
                IFT_IFloat = 5,
                IFT_IString = 6,
                IFT_IRegister = 7,
                IFT_ICategory = 8,
                IFT_IEnumeration = 9,
                IFT_IEnumEntry = 10,
                IFT_IPort = 11
            }
            public enum MV_XML_AccessMode
            {
                AM_NI = 0,
                AM_NA = 1,
                AM_WO = 2,
                AM_RO = 3,
                AM_RW = 4,
                AM_Undefined = 5,
                AM_CycleDetect = 6
            }
            public enum MV_XML_Visibility
            {
                V_Beginner = 0,
                V_Expert = 1,
                V_Guru = 2,
                V_Invisible = 3,
                V_Undefined = 99
            }
            public enum MV_CAM_EXPOSURE_MODE
            {
                MV_EXPOSURE_MODE_TIMED = 0,
                MV_EXPOSURE_MODE_TRIGGER_WIDTH = 1
            }
            public enum MV_GRAB_STRATEGY
            {
                MV_GrabStrategy_OneByOne = 0,
                MV_GrabStrategy_LatestImagesOnly = 1,
                MV_GrabStrategy_LatestImages = 2,
                MV_GrabStrategy_UpcomingImage = 3
            }
            public enum MvGvspPixelType
            {
                PixelType_Gvsp_Jpeg = -2145910783,
                PixelType_Gvsp_HB_Mono8 = -2130182143,
                PixelType_Gvsp_HB_BayerGR8 = -2130182136,
                PixelType_Gvsp_HB_BayerRG8 = -2130182135,
                PixelType_Gvsp_HB_BayerGB8 = -2130182134,
                PixelType_Gvsp_HB_BayerBG8 = -2130182133,
                PixelType_Gvsp_HB_Mono10_Packed = -2129919996,
                PixelType_Gvsp_HB_Mono12_Packed = -2129919994,
                PixelType_Gvsp_HB_BayerGR10_Packed = -2129919962,
                PixelType_Gvsp_HB_BayerRG10_Packed = -2129919961,
                PixelType_Gvsp_HB_BayerGB10_Packed = -2129919960,
                PixelType_Gvsp_HB_BayerBG10_Packed = -2129919959,
                PixelType_Gvsp_HB_BayerGR12_Packed = -2129919958,
                PixelType_Gvsp_HB_BayerRG12_Packed = -2129919957,
                PixelType_Gvsp_HB_BayerGB12_Packed = -2129919956,
                PixelType_Gvsp_HB_BayerBG12_Packed = -2129919955,
                PixelType_Gvsp_HB_Mono10 = -2129657853,
                PixelType_Gvsp_HB_Mono12 = -2129657851,
                PixelType_Gvsp_HB_Mono16 = -2129657849,
                PixelType_Gvsp_HB_BayerGR10 = -2129657844,
                PixelType_Gvsp_HB_BayerRG10 = -2129657843,
                PixelType_Gvsp_HB_BayerGB10 = -2129657842,
                PixelType_Gvsp_HB_BayerBG10 = -2129657841,
                PixelType_Gvsp_HB_BayerGR12 = -2129657840,
                PixelType_Gvsp_HB_BayerRG12 = -2129657839,
                PixelType_Gvsp_HB_BayerGB12 = -2129657838,
                PixelType_Gvsp_HB_BayerBG12 = -2129657837,
                PixelType_Gvsp_Coord3D_A32 = -2128596987,
                PixelType_Gvsp_Coord3D_C32 = -2128596986,
                PixelType_Gvsp_HB_YUV422_Packed = -2112880609,
                PixelType_Gvsp_HB_YUV422_YUYV_Packed = -2112880590,
                PixelType_Gvsp_HB_RGB8_Packed = -2112356332,
                PixelType_Gvsp_HB_BGR8_Packed = -2112356331,
                PixelType_Gvsp_COORD3D_DEPTH_PLUS_MASK = -2112094207,
                PixelType_Gvsp_HB_RGBA8_Packed = -2111832042,
                PixelType_Gvsp_HB_BGRA8_Packed = -2111832041,
                PixelType_Gvsp_Coord3D_AB32f = -2109722622,
                PixelType_Gvsp_Coord3D_AB32 = -2109722621,
                PixelType_Gvsp_Coord3D_AC32 = -2109722620,
                PixelType_Gvsp_Coord3D_ABC32 = -2107625471,
                PixelType_Gvsp_Undefined = -1,
                PixelType_Gvsp_Mono1p = 16842807,
                PixelType_Gvsp_Mono2p = 16908344,
                PixelType_Gvsp_Mono4p = 17039417,
                PixelType_Gvsp_Mono8 = 17301505,
                PixelType_Gvsp_Mono8_Signed = 17301506,
                PixelType_Gvsp_BayerGR8 = 17301512,
                PixelType_Gvsp_BayerRG8 = 17301513,
                PixelType_Gvsp_BayerGB8 = 17301514,
                PixelType_Gvsp_BayerBG8 = 17301515,
                PixelType_Gvsp_Mono10_Packed = 17563652,
                PixelType_Gvsp_Mono12_Packed = 17563654,
                PixelType_Gvsp_BayerGR10_Packed = 17563686,
                PixelType_Gvsp_BayerRG10_Packed = 17563687,
                PixelType_Gvsp_BayerGB10_Packed = 17563688,
                PixelType_Gvsp_BayerBG10_Packed = 17563689,
                PixelType_Gvsp_BayerGR12_Packed = 17563690,
                PixelType_Gvsp_BayerRG12_Packed = 17563691,
                PixelType_Gvsp_BayerGB12_Packed = 17563692,
                PixelType_Gvsp_BayerBG12_Packed = 17563693,
                PixelType_Gvsp_Mono10 = 17825795,
                PixelType_Gvsp_Mono12 = 17825797,
                PixelType_Gvsp_Mono16 = 17825799,
                PixelType_Gvsp_BayerGR10 = 17825804,
                PixelType_Gvsp_BayerRG10 = 17825805,
                PixelType_Gvsp_BayerGB10 = 17825806,
                PixelType_Gvsp_BayerBG10 = 17825807,
                PixelType_Gvsp_BayerGR12 = 17825808,
                PixelType_Gvsp_BayerRG12 = 17825809,
                PixelType_Gvsp_BayerGB12 = 17825810,
                PixelType_Gvsp_BayerBG12 = 17825811,
                PixelType_Gvsp_Mono14 = 17825829,
                PixelType_Gvsp_BayerGR16 = 17825838,
                PixelType_Gvsp_BayerRG16 = 17825839,
                PixelType_Gvsp_BayerGB16 = 17825840,
                PixelType_Gvsp_BayerBG16 = 17825841,
                PixelType_Gvsp_Coord3D_C16 = 17825976,
                PixelType_Gvsp_Coord3D_A32f = 18874557,
                PixelType_Gvsp_Coord3D_C32f = 18874559,
                PixelType_Gvsp_YUV411_Packed = 34340894,
                PixelType_Gvsp_YCBCR411_8_CBYYCRYY = 34340924,
                PixelType_Gvsp_YCBCR601_411_8_CBYYCRYY = 34340927,
                PixelType_Gvsp_YCBCR709_411_8_CBYYCRYY = 34340930,
                PixelType_Gvsp_YUV422_Packed = 34603039,
                PixelType_Gvsp_YUV422_YUYV_Packed = 34603058,
                PixelType_Gvsp_RGB565_Packed = 34603061,
                PixelType_Gvsp_BGR565_Packed = 34603062,
                PixelType_Gvsp_YCBCR422_8 = 34603067,
                PixelType_Gvsp_YCBCR601_422_8 = 34603070,
                PixelType_Gvsp_YCBCR709_422_8 = 34603073,
                PixelType_Gvsp_YCBCR422_8_CBYCRY = 34603075,
                PixelType_Gvsp_YCBCR601_422_8_CBYCRY = 34603076,
                PixelType_Gvsp_YCBCR709_422_8_CBYCRY = 34603077,
                PixelType_Gvsp_RGB8_Packed = 35127316,
                PixelType_Gvsp_BGR8_Packed = 35127317,
                PixelType_Gvsp_YUV444_Packed = 35127328,
                PixelType_Gvsp_RGB8_Planar = 35127329,
                PixelType_Gvsp_YCBCR8_CBYCR = 35127354,
                PixelType_Gvsp_YCBCR601_8_CBYCR = 35127357,
                PixelType_Gvsp_YCBCR709_8_CBYCR = 35127360,
                PixelType_Gvsp_RGBA8_Packed = 35651606,
                PixelType_Gvsp_BGRA8_Packed = 35651607,
                PixelType_Gvsp_RGB10V1_Packed = 35651612,
                PixelType_Gvsp_RGB10V2_Packed = 35651613,
                PixelType_Gvsp_RGB12V1_Packed = 35913780,
                PixelType_Gvsp_RGB10_Packed = 36700184,
                PixelType_Gvsp_BGR10_Packed = 36700185,
                PixelType_Gvsp_RGB12_Packed = 36700186,
                PixelType_Gvsp_BGR12_Packed = 36700187,
                PixelType_Gvsp_RGB10_Planar = 36700194,
                PixelType_Gvsp_RGB12_Planar = 36700195,
                PixelType_Gvsp_RGB16_Planar = 36700196,
                PixelType_Gvsp_RGB16_Packed = 36700211,
                PixelType_Gvsp_Coord3D_ABC16 = 36700345,
                PixelType_Gvsp_Coord3D_AC32f = 37748930,
                PixelType_Gvsp_Coord3D_AC32f_Planar = 37748931,
                PixelType_Gvsp_Coord3D_ABC32f = 39846080,
                PixelType_Gvsp_Coord3D_ABC32f_Planar = 39846081
            }

            public struct MV_FRAME_OUT
            {
                public IntPtr pBufAddr;
                public MV_FRAME_OUT_INFO_EX stFrameInfo;
                public uint[] nReserved;
            }
            public struct MV_ACTION_CMD_RESULT
            {
                public string strDeviceAddress;
                public int nStatus;
                public uint[] nReserved;
            }
            public struct MV_ACTION_CMD_INFO
            {
                public uint nDeviceKey;
                public uint nGroupKey;
                public uint nGroupMask;
                public uint bActionTimeEnable;
                public long nActionTime;
                public string pBroadcastAddress;
                public uint nTimeOut;
                public uint[] nReserved;
            }
            public struct MV_CC_TRANSMISSION_TYPE
            {
                public MV_GIGE_TRANSMISSION_TYPE enTransmissionType;
                public uint nDestIp;
                public ushort nDestPort;
                public uint[] nReserved;
            }
            public struct MV_CC_FILE_ACCESS_PROGRESS
            {
                public long nCompleted;
                public long nTotal;
                public uint[] nReserved;
            }
            public struct MV_CC_FILE_ACCESS
            {
                public string pUserFileName;
                public string pDevFileName;
                public uint[] nReserved;
            }
            public struct MV_EVENT_OUT_INFO
            {
                public string EventName;
                public ushort nEventID;
                public ushort nStreamChannel;
                public uint nBlockIdHigh;
                public uint nBlockIdLow;
                public uint nTimestampHigh;
                public uint nTimestampLow;
                public IntPtr pEventData;
                public uint nEventDataSize;
                public uint[] nReserved;
            }
            public struct MV_ACTION_CMD_RESULT_LIST
            {
                public uint nNumResults;
                public IntPtr pResults;
            }
            public struct MV_XML_FEATURE_Port
            {
                public MV_XML_Visibility enVisivility;
                public string strDescription;
                public string strDisplayName;
                public string strName;
                public string strToolTip;
                public MV_XML_AccessMode enAccessMode;
                public int bIsLocked;
                public uint[] nReserved;
            }
            public struct MV_MATCH_INFO_USB_DETECT
            {
                public long nReviceDataSize;
                public uint nRevicedFrameCount;
                public uint nErrorFrameCount;
                public uint[] nReserved;
            }
            public struct MV_MATCH_INFO_NET_DETECT
            {
                public long nReviceDataSize;
                public long nLostPacketCount;
                public uint nLostFrameCount;
                public uint nNetRecvFrameCount;
                public long nRequestResendPacketCount;
                public long nResendPacketCount;
            }
            public struct MV_ALL_MATCH_INFO
            {
                public uint nType;
                public IntPtr pInfo;
                public uint nInfoSize;
            }
            //
            // 摘要:
            //     ch: GigE设备信息 | en: GigE device information
            public struct MV_GIGE_DEVICE_INFO_EX
            {
                public uint nIpCfgOption;
                public uint nIpCfgCurrent;
                public uint nCurrentIp;
                public uint nCurrentSubNetMask;
                public uint nDefultGateWay;
                public string chManufacturerName;
                public string chModelName;
                public string chDeviceVersion;
                public string chManufacturerSpecificInfo;
                public string chSerialNumber;
                public byte[] chUserDefinedName;
                public uint nNetExport;
                public uint[] nReserved;
            }
            public struct MV_GIGE_DEVICE_INFO
            {
                public uint nIpCfgOption;
                public uint nIpCfgCurrent;
                public uint nCurrentIp;
                public uint nCurrentSubNetMask;
                public uint nDefultGateWay;
                public string chManufacturerName;
                public string chModelName;
                public string chDeviceVersion;
                public string chManufacturerSpecificInfo;
                public string chSerialNumber;
                public string chUserDefinedName;
                public uint nNetExport;
                public uint[] nReserved;
            }
            public struct MV_FRAME_OUT_INFO_EX
            {
                public ushort nWidth;
                public UNPARSED_CHUNK_LIST UnparsedChunkList;
                public uint nUnparsedChunkNum;
                public uint nLostPacket;
                public ushort nChunkHeight;
                public ushort nChunkWidth;
                public ushort nOffsetY;
                public ushort nOffsetX;
                public uint nOutput;
                public uint nInput;
                public uint nTriggerIndex;
                public uint nFrameCounter;
                public uint nBlue;
                public uint nGreen;
                public uint[] nReserved;
                public uint nRed;
                public float fExposureTime;
                public float fGain;
                public uint nCycleOffset;
                public uint nCycleCount;
                public uint nSecondCount;
                public uint nFrameLen;
                public long nHostTimeStamp;
                public uint nReserved0;
                public uint nDevTimeStampLow;
                public uint nDevTimeStampHigh;
                public uint nFrameNum;
                public MvGvspPixelType enPixelType;
                public ushort nHeight;
                public uint nAverageBrightness;

                public struct UNPARSED_CHUNK_LIST
                {
                    public IntPtr pUnparsedChunkContent;
                    public long nAligning;
                }
            }
            public struct MV_IMAGE_BASIC_INFO
            {
                public ushort nWidthValue;
                public ushort nWidthMin;
                public uint nWidthMax;
                public uint nWidthInc;
                public uint nHeightValue;
                public uint nHeightMin;
                public uint nHeightMax;
                public uint nHeightInc;
                public float fFrameRateValue;
                public float fFrameRateMin;
                public float fFrameRateMax;
                public uint enPixelType;
                public uint nSupportedPixelFmtNum;
                public uint[] enPixelList;
                public uint[] nReserved;
            }
            public struct MV_USB3_DEVICE_INFO
            {
                public byte CrtlInEndPoint;
                public string chUserDefinedName;
                public string chSerialNumber;
                public string chManufacturerName;
                public string chDeviceVersion;
                public string chFamilyName;
                public string chModelName;
                public uint nbcdUSB;
                public string chVendorName;
                public uint nDeviceNumber;
                public ushort idProduct;
                public ushort idVendor;
                public byte EventEndPoint;
                public byte StreamEndPoint;
                public byte CrtlOutEndPoint;
                public string chDeviceGUID;
                public uint[] nReserved;
            }
            public struct MV_XML_NODE_FEATURE
            {
                public MV_XML_InterfaceType enType;
                public MV_XML_Visibility enVisivility;
                public string strDescription;
                public string strDisplayName;
                public string strName;
                public string strToolTip;
                public uint[] nReserved;
            }
            public struct MVCC_INTVALUE
            {
                public uint nCurValue;
                public uint nMax;
                public uint nMin;
                public uint nInc;
                public uint[] nReserved;
            }
            public struct MV_XML_FEATURE_Enumeration
            {
                public MV_XML_Visibility enVisivility;
                public string strDescription;
                public string strDisplayName;
                public string strName;
                public string strToolTip;
                public int nSymbolicNum;
                public string strCurrentSymbolic;
                public StrSymbolic[] strSymbolic;
                public MV_XML_AccessMode enAccessMode;
                public int bIsLocked;
                public long nValue;
                public uint[] nReserved;
            }
            public struct StrSymbolic
            {
                public string str;
            }
            public struct MV_XML_FEATURE_EnumEntry
            {
                public string strName;
                public string strDisplayName;
                public string strDescription;
                public string strToolTip;
                public int bIsImplemented;
                public int nParentsNum;
                public MV_XML_NODE_FEATURE[] stParentsList;
                public MV_XML_Visibility enVisivility;
                public long nValue;
                public MV_XML_AccessMode enAccessMode;
                public int bIsLocked;
                public uint[] nReserved;
            }
            public struct MV_XML_FEATURE_Category
            {
                public string strDescription;
                public string strDisplayName;
                public string strName;
                public string strToolTip;
                public MV_XML_Visibility enVisivility;
                public uint[] nReserved;
            }
            public struct MV_XML_FEATURE_Register
            {
                public string strName;
                public string strDisplayName;
                public string strDescription;
                public string strToolTip;
                public MV_XML_Visibility enVisivility;
                public MV_XML_AccessMode enAccessMode;
                public int bIsLocked;
                public long nAddrValue;
                public uint[] nReserved;
            }
            public struct MV_XML_FEATURE_String
            {
                public string strName;
                public string strDisplayName;
                public string strDescription;
                public string strToolTip;
                public MV_XML_Visibility enVisivility;
                public MV_XML_AccessMode enAccessMode;
                public int bIsLocked;
                public string strValue;
                public uint[] nReserved;
            }
            public struct MV_XML_NODES_LIST
            {
                public uint nNodeNum;
                public MV_XML_NODE_FEATURE[] stNodes;
            }
            public struct MV_XML_FEATURE_Float
            {
                public string strName;
                public string strDisplayName;
                public string strDescription;
                public string strToolTip;
                public MV_XML_Visibility enVisivility;
                public MV_XML_AccessMode enAccessMode;
                public int bIsLocked;
                public double dfValue;
                public double dfMinValue;
                public double dfMaxValue;
                public double dfIncrement;
                public uint[] nReserved;
            }
            public struct MV_XML_FEATURE_Boolean
            {
                public string strName;
                public string strDisplayName;
                public string strDescription;
                public string strToolTip;
                public MV_XML_Visibility enVisivility;
                public MV_XML_AccessMode enAccessMode;
                public int bIsLocked;
                public bool bValue;
                public uint[] nReserved;
            }
            public struct MV_XML_FEATURE_Integer
            {
                public string strName;
                public string strDisplayName;
                public string strDescription;
                public string strToolTip;
                public MV_XML_Visibility enVisivility;
                public MV_XML_AccessMode enAccessMode;
                public int bIsLocked;
                public long nValue;
                public long nMinValue;
                public long nMaxValue;
                public long nIncrement;
                public uint[] nReserved;
            }
            public struct MVCC_STRINGVALUE
            {
                public string chCurValue;
                public long nMaxLength;
                public uint[] nReserved;
            }
            public struct MVCC_ENUMVALUE
            {
                public uint nCurValue;
                public uint nSupportedNum;
                public uint[] nSupportValue;
                public uint[] nReserved;
            }
            public struct MVCC_FLOATVALUE
            {
                public float fCurValue;
                public float fMax;
                public float fMin;
                public uint[] nReserved;
            }
            public struct MVCC_INTVALUE_EX
            {
                public long nCurValue;
                public long nMax;
                public long nMin;
                public long nInc;
                public uint[] nReserved;
            }
            public struct MV_XML_FEATURE_Command
            {
                public string strName;
                public string strDisplayName;
                public string strDescription;
                public string strToolTip;
                public MV_XML_Visibility enVisivility;
                public MV_XML_AccessMode enAccessMode;
                public int bIsLocked;
                public uint[] nReserved;
            }
            //
            // 摘要:
            //     ch:CamLink设备信息 | en:CamLink device information
            public struct MV_CamL_DEV_INFO
            {
                public string chPortID;
                public string chModelName;
                public string chFamilyName;
                public string chDeviceVersion;
                public string chManufacturerName;
                public string chSerialNumber;
                public uint[] nReserved;
            }
            //
            // 摘要:
            //     ch:USB3 设备信息 | en:USB3 device information
            public struct MV_USB3_DEVICE_INFO_EX
            {
                public byte CrtlInEndPoint;
                public byte[] chUserDefinedName;
                public string chSerialNumber;
                public string chManufacturerName;
                public string chDeviceVersion;
                public string chFamilyName;
                public string chModelName;
                public uint nbcdUSB;
                public string chVendorName;
                public uint nDeviceNumber;
                public ushort idProduct;
                public ushort idVendor;
                public byte EventEndPoint;
                public byte StreamEndPoint;
                public byte CrtlOutEndPoint;
                public string chDeviceGUID;
                public uint[] nReserved;
            }
            public struct MV_CC_RECORD_PARAM
            {
                public MvGvspPixelType enPixelType;
                public ushort nWidth;
                public ushort nHeight;
                public float fFrameRate;
                public uint nBitRate;
                public MV_RECORD_FORMAT_TYPE enRecordFmtType;
                public string strFilePath;
                public uint[] nRes;
            }
            public struct MV_CC_CLUT_PARAM
            {
                public bool bCLUTEnable;
                public uint nCLUTScale;
                public uint nCLUTSize;
                public IntPtr pCLUTBuf;
                public uint nCLUTBufLen;
                public uint[] nRes;
            }
            public struct MV_CC_CCM_PARAM_EX
            {
                public bool bCCMEnable;
                public int[] nCCMat;
                public uint nCCMScale;
                public uint[] nRes;
            }
            public struct MV_CC_CCM_PARAM
            {
                public bool bCCMEnable;
                public int[] nCCMat;
                public uint[] nRes;
            }
            public struct MV_CC_GAMMA_PARAM
            {
                public MV_CC_GAMMA_TYPE enGammaType;
                public float fGammaValue;
                public IntPtr pGammaCurveBuf;
                public uint nGammaCurveBufLen;
                public uint[] nRes;
            }
            //
            // 摘要:
            //     ch:通过GenTL枚举到的设备信息 | en:Device Information discovered by with GenTL
            public struct MV_GENTL_DEV_INFO
            {
                public string chInterfaceID;
                public string chDeviceID;
                public string chVendorName;
                public string chModelName;
                public string chTLType;
                public string chDisplayName;
                public string chUserDefinedName;
                public string chSerialNumber;
                public string chDeviceVersion;
                public uint nCtiIndex;
                public uint[] nReserved;
            }
            public struct MV_PIXEL_CONVERT_PARAM
            {
                public ushort nWidth;
                public ushort nHeight;
                public MvGvspPixelType enSrcPixelType;
                public IntPtr pSrcData;
                public uint nSrcDataLen;
                public MvGvspPixelType enDstPixelType;
                public IntPtr pDstBuffer;
                public uint nDstLen;
                public uint nDstBufferSize;
                public uint[] nRes;
            }
            public struct MV_CC_FLIP_IMAGE_PARAM
            {
                public MvGvspPixelType enPixelType;
                public uint nWidth;
                public uint nHeight;
                public IntPtr pSrcData;
                public uint nSrcDataLen;
                public IntPtr pDstBuf;
                public uint nDstBufLen;
                public uint nDstBufSize;
                public MV_IMG_FLIP_TYPE enFlipType;
                public uint[] nRes;
            }
            //
            // 摘要:
            //     ch:GenTL设备列表 | en:GenTL devices list
            public struct MV_GENTL_DEV_INFO_LIST
            {
                public uint nDeviceNum;
                public IntPtr[] pDeviceInfo;
            }
            public struct MV_CC_ROTATE_IMAGE_PARAM
            {
                public MvGvspPixelType enPixelType;
                public uint nWidth;
                public uint nHeight;
                public IntPtr pSrcData;
                public uint nSrcDataLen;
                public IntPtr pDstBuf;
                public uint nDstBufLen;
                public uint nDstBufSize;
                public MV_IMG_ROTATION_ANGLE enRotationAngle;
                public uint[] nRes;
            }
            public struct MV_NETTRANS_INFO
            {
                public long nReviceDataSize;
                public int nThrowFrameCount;
                public uint nNetRecvFrameCount;
                public long nRequestResendPacketCount;
                public long nResendPacketCount;
            }
            public struct MV_SAVE_IMG_TO_FILE_PARAM
            {
                public MvGvspPixelType enPixelType;
                public IntPtr pData;
                public uint nDataLen;
                public ushort nWidth;
                public ushort nHeight;
                public MV_SAVE_IAMGE_TYPE enImageType;
                public uint nQuality;
                public string pImagePath;
                public uint iMethodValue;
                public uint[] nRes;
            }
            public struct MV_SAVE_IMAGE_PARAM_EX
            {
                public IntPtr pData;
                public uint nDataLen;
                public MvGvspPixelType enPixelType;
                public ushort nWidth;
                public ushort nHeight;
                public IntPtr pImageBuffer;
                public uint nImageLen;
                public uint nBufferSize;
                public MV_SAVE_IAMGE_TYPE enImageType;
                public uint nJpgQuality;
                public uint iMethodValue;
                public uint[] nReserved;
            }
            public struct MV_SAVE_IMAGE_PARAM
            {
                public IntPtr pData;
                public uint nDataLen;
                public MvGvspPixelType enPixelType;
                public ushort nWidth;
                public ushort nHeight;
                public IntPtr pImageBuffer;
                public uint nImageLen;
                public uint nBufferSize;
                public MV_SAVE_IAMGE_TYPE enImageType;
            }
            public struct MV_SAVE_POINT_CLOUD_PARAM
            {
                public uint nLinePntNum;
                public uint nLineNum;
                public MvGvspPixelType enSrcPixelType;
                public IntPtr pSrcData;
                public uint nSrcDataLen;
                public IntPtr pDstBuf;
                public uint nDstBufSize;
                public uint nDstBufLen;
                public MV_SAVE_POINT_CLOUD_FILE_TYPE enPointCloudFileType;
                public uint[] nRes;
            }
            public struct MV_FRAME_OUT_INFO
            {
                public ushort nWidth;
                public ushort nHeight;
                public MvGvspPixelType enPixelType;
                public uint nFrameNum;
                public uint nDevTimeStampHigh;
                public uint nDevTimeStampLow;
                public uint nReserved0;
                public long nHostTimeStamp;
                public uint nFrameLen;
                public uint nLostPacket;
                public uint[] nReserved;
            }
            public struct MV_DISPLAY_FRAME_INFO
            {
                public IntPtr hWnd;
                public IntPtr pData;
                public uint nDataLen;
                public ushort nWidth;
                public ushort nHeight;
                public MvGvspPixelType enPixelType;
                public uint[] nReserved;
            }
            public struct MV_CHUNK_DATA_CONTENT
            {
                public IntPtr pChunkData;
                public uint nChunkID;
                public uint nChunkLen;
                public uint[] nReserved;
            }
            public struct MV_CC_CONTRAST_PARAM
            {
                public uint nWidth;
                public uint nHeight;
                public IntPtr pSrcBuf;
                public uint nSrcBufLen;
                public MvGvspPixelType enPixelType;
                public IntPtr pDstBuf;
                public uint nDstBufSize;
                public uint nDstBufLen;
                public uint nContrastFactor;
                public uint[] nRes;
            }
            public struct MV_CC_SHARPEN_PARAM
            {
                public uint nWidth;
                public uint nHeight;
                public IntPtr pSrcBuf;
                public uint nSrcBufLen;
                public MvGvspPixelType enPixelType;
                public IntPtr pDstBuf;
                public uint nDstBufSize;
                public uint nDstBufLen;
                public uint nSharpenAmount;
                public uint nSharpenRadius;
                public uint nSharpenThreshold;
                public uint[] nRes;
            }
            public struct MV_CC_COLOR_CORRECT_PARAM
            {
                public uint nWidth;
                public uint nHeight;
                public IntPtr pSrcBuf;
                public uint nSrcBufLen;
                public MvGvspPixelType enPixelType;
                public IntPtr pDstBuf;
                public uint nDstBufSize;
                public uint nDstBufLen;
                public uint nImageBit;
                public MV_CC_GAMMA_PARAM stGammaParam;
                public MV_CC_CCM_PARAM_EX stCCMParam;
                public MV_CC_CLUT_PARAM stCLUTParam;
                public uint[] nRes;
            }
            public struct MV_CC_RECT_I
            {
                public uint nX;
                public uint nY;
                public uint nWidth;
                public uint nHeight;
            }
            //
            // 摘要:
            //     ch:设备信息 | en:Device information
            public struct MV_CC_DEVICE_INFO
            {
                public ushort nMajorVer;
                public ushort nMinorVer;
                public uint nMacAddrHigh;
                public uint nMacAddrLow;
                public uint nTLayerType;
                public uint[] nReserved;
                public SPECIAL_INFO SpecialInfo;

                //
                // 摘要:
                //     ch:特定类型的设备信息 | en:Special devcie information
                public struct SPECIAL_INFO
                {
                    public byte[] stGigEInfo;
                    public byte[] stCamLInfo;
                    public byte[] stUsb3VInfo;
                }
            }
            public struct MV_CC_DEVICE_INFO_LIST
            {
                public uint nDeviceNum;
                public IntPtr[] pDeviceInfo;
            }
            public struct MV_CC_HB_DECODE_PARAM
            {
                public IntPtr pSrcBuf;
                public uint nSrcLen;
                public uint nWidth;
                public uint nHeight;
                public IntPtr pDstBuf;
                public uint nDstBufSize;
                public uint nDstBufLen;
                public MvGvspPixelType enDstPixelType;
                public MV_CC_FRAME_SPEC_INFO stFrameSpecInfo;
                public uint[] nRes;
            }
            public struct MV_CC_FRAME_SPEC_INFO
            {
                public uint nSecondCount;
                public ushort nFrameWidth;
                public ushort nOffsetY;
                public ushort nOffsetX;
                public uint nOutput;
                public uint nInput;
                public uint nTriggerIndex;
                public uint nFrameCounter;
                public uint nBlue;
                public uint nGreen;
                public uint nRed;
                public uint nAverageBrightness;
                public float fExposureTime;
                public float fGain;
                public uint nCycleOffset;
                public uint nCycleCount;
                public ushort nFrameHeight;
                public uint[] nRes;
            }
            public struct MV_CC_INPUT_FRAME_INFO
            {
                public IntPtr pData;
                public uint nDataLen;
                public uint[] nRes;
            }
            public struct MV_CC_BAYER_NOISE_ESTIMATE_PARAM
            {
                public uint nWidth;
                public uint nHeight;
                public MvGvspPixelType enPixelType;
                public IntPtr pSrcData;
                public uint nSrcDataLen;
                public uint nNoiseThreshold;
                public IntPtr pCurveBuf;
                public MV_CC_BAYER_NOISE_PROFILE_INFO stNoiseProfile;
                public uint nThreadNum;
                public uint[] nRes;
            }
            public struct MV_CC_BAYER_NOISE_PROFILE_INFO
            {
                public uint nVersion;
                public MV_CC_BAYER_NOISE_FEATURE_TYPE enNoiseFeatureType;
                public MvGvspPixelType enPixelType;
                public int nNoiseLevel;
                public uint nCurvePointNum;
                public IntPtr nNoiseCurve;
                public IntPtr nLumCurve;
                public uint[] nRes;
            }
            public struct MV_CC_BAYER_SPATIAL_DENOISE_PARAM
            {
                public uint nWidth;
                public uint nHeight;
                public MvGvspPixelType enPixelType;
                public IntPtr pSrcData;
                public uint nSrcDataLen;
                public IntPtr pDstBuf;
                public uint nDstBufSize;
                public uint nDstBufLen;
                public MV_CC_BAYER_NOISE_PROFILE_INFO stNoiseProfile;
                public uint nDenoiseStrength;
                public uint nSharpenStrength;
                public uint nNoiseCorrect;
                public uint nThreadNum;
                public uint[] nRes;
            }
            public struct MV_CC_LSC_CORRECT_PARAM
            {
                public uint nWidth;
                public uint nHeight;
                public MvGvspPixelType enPixelType;
                public IntPtr pSrcBuf;
                public uint nSrcBufLen;
                public IntPtr pDstBuf;
                public uint nDstBufSize;
                public uint nDstBufLen;
                public IntPtr pCalibBuf;
                public uint nCalibBufLen;
                public uint[] nRes;
            }
            public struct MV_CC_LSC_CALIB_PARAM
            {
                public uint nWidth;
                public uint nHeight;
                public MvGvspPixelType enPixelType;
                public IntPtr pSrcBuf;
                public uint nSrcBufLen;
                public IntPtr pCalibBuf;
                public uint nCalibBufSize;
                public uint nCalibBufLen;
                public uint nSecNumW;
                public uint nSecNumH;
                public uint nPadCoef;
                public uint nCalibMethod;
                public uint nTargetGray;
                public uint[] nRes;
            }
            //
            // 摘要:
            //     ch:通过GenTL枚举到的设备信息列表 | en:Interface Information List with GenTL
            public struct MV_GENTL_IF_INFO_LIST
            {
                public uint nInterfaceNum;
                public IntPtr[] pIFInfo;
            }
            public struct MV_CC_NOISE_ESTIMATE_PARAM
            {
                public uint nWidth;
                public uint nHeight;
                public MvGvspPixelType enPixelType;
                public IntPtr pSrcBuf;
                public uint nSrcBufLen;
                public IntPtr pstROIRect;
                public uint nROINum;
                public uint nNoiseThreshold;
                public IntPtr pNoiseProfile;
                public uint nNoiseProfileSize;
                public uint nNoiseProfileLen;
                public uint[] nRes;
            }
            //
            // 摘要:
            //     ch:通过GenTL枚举到的Interface信息 | en:Interface Information with GenTL
            public struct MV_GENTL_IF_INFO
            {
                public string chInterfaceID;
                public string chTLType;
                public string chDisplayName;
                public uint nCtiIndex;
                public uint[] nReserved;
            }
            public struct MV_CC_SPATIAL_DENOISE_PARAM
            {
                public uint nWidth;
                public uint nStrengthChrom;
                public uint nStrengthLum;
                public uint nNoiseCorrectChrom;
                public uint nNoiseCorrectLum;
                public uint nBayerNoiseCorrect;
                public uint nBayerSharpenStrength;
                public uint nBayerDenoiseStrength;
                public uint nStrengthSharpen;
                public uint nNoiseProfileLen;
                public uint nDstBufLen;
                public uint nDstBufSize;
                public IntPtr pDstBuf;
                public uint nSrcBufLen;
                public IntPtr pSrcBuf;
                public MvGvspPixelType enPixelType;
                public uint nHeight;
                public IntPtr pNoiseProfile;
                public uint[] nRes;
            }

            //
            // 摘要:
            //     Grab callback
            //
            // 參數:
            //   pData:
            //     Image data
            //
            //   pFrameInfo:
            //     Frame info
            //
            //   pUser:
            //     User defined variable
            public delegate void cbOutputExdelegate(IntPtr pData, ref MV_FRAME_OUT_INFO_EX pFrameInfo, IntPtr pUser);
            //
            // 摘要:
            //     Grab callback
            //
            // 參數:
            //   pData:
            //     Image data
            //
            //   pFrameInfo:
            //     Frame info
            //
            //   pUser:
            //     User defined variable
            public delegate void cbOutputdelegate(IntPtr pData, ref MV_FRAME_OUT_INFO pFrameInfo, IntPtr pUser);
            //
            // 摘要:
            //     Xml Update callback(Interfaces not recommended)
            //
            // 參數:
            //   enType:
            //     Node type
            //
            //   pstFeature:
            //     Current node feature structure
            //
            //   pstNodesList:
            //     Nodes list
            //
            //   pUser:
            //     User defined variable
            public delegate void cbXmlUpdatedelegate(MV_XML_InterfaceType enType, IntPtr pstFeature, ref MV_XML_NODES_LIST pstNodesList, IntPtr pUser);
            //
            // 摘要:
            //     Exception callback
            //
            // 參數:
            //   nMsgType:
            //     Msg type
            //
            //   pUser:
            //     User defined variable
            public delegate void cbExceptiondelegate(uint nMsgType, IntPtr pUser);
            //
            // 摘要:
            //     Event callback (Interfaces not recommended)
            //
            // 參數:
            //   nUserDefinedId:
            //     User defined ID
            //
            //   pUser:
            //     User defined variable
            public delegate void cbEventdelegate(uint nUserDefinedId, IntPtr pUser);
            //
            // 摘要:
            //     Event callback
            //
            // 參數:
            //   pEventInfo:
            //     Event Info
            //
            //   pUser:
            //     User defined variable
            public delegate void cbEventdelegateEx(ref MV_EVENT_OUT_INFO pEventInfo, IntPtr pUser);
        }
    }
}
