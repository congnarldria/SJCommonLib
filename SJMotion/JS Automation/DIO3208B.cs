using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;

namespace SJMotion
{
    public class DIO3208B
    {


        //******************************************************
        public const short _CARD_ID_MAX = 15;
        public const short _NO_CARD = 0xFF;
        public const short DIO3208B_INPORT_MIN = 0;
        public const short DIO3208B_INPORT_MAX = 1;
        public const short DIO3208B_OUTPORT_MIN = 2;
        public const short DIO3208B_OUTPORT_MAX = 3;
        public const short DIO3208B_IN_POINT_MAX = 7;
        public const short DIO3208B_OUT_POINT_MAX = 7;
        public const short DIO3208B_INDEX_MIN = 0;
        public const short DIO3208B_INDEX_MAX = 7;
        public const short DIO3208B_DEBOUNCE_MODE_MAX = 3;


        public const short FAIL = 0;


        ////////////// Error Code ///////////////////////
        public const short JSDRV_NO_ERROR = 0;

        /************ Driver Error ***************/
        public const short JSDRV_READ_DATA_ERROR = 1;
        public const short JSDRV_INIT_ERROR = 2;
        public const short JSDRV_UNLOCK_ERROR = 3;	// Driver Error
        public const short JSDRV_LOCK_COUNTER_ERROR = 4;	// Driver Error
        public const short JSDRV_SET_SECURITY_ERROR = 5;	// Driver Error

        /************ Device Error ***************/
        public const short DEVICE_RW_ERROR = 100;
        public const short JSDRV_NO_CARD = 101;
        public const short JSDRV_DUPLICATE_ID = 102;


        /************ User Parameter Error ********/
        public const short JSDIO_ID_ERROR = 300;
        public const short JSDIO_PORT_ERROR = 301;
        public const short JSDIO_IN_POINT_ERROR = 302;
        public const short JSDIO_OUT_POINT_ERROR = 303;
        public const short JSDIO_VERSION_ERROR = 304;
        public const short JSDIO_SOURCE_ERROR = 305;
        public const short JSDIO_INDEX_ERROR = 406;
        public const short JSDIO_TO_MODE_ERROR = 407;
        public const short JSDIO_TI_MODE_ERROR = 408;
        //-----------------------------------------------

        public const short DEBOUNCE_MODE_ERROR = 501;



        /************ DIO ************************/
        public const short INPORT0 = 0;//inport0
        public const short OUTPORT0 = 1;//outport0


        /************ DEBOUNCE_TIME *************/
        public const short NO_DEBOUNCE_TIME = 0;	//SET DEBOUNCE_TIME
        public const short DEBOUNCE_TIME_100HZ = 1;
        public const short DEBOUNCE_TIME_200HZ = 2;
        public const short DEBOUNCE_TIME_1KHZ = 3;
        public const short TC_DEBOUNCE_0 = 0;
        public const short TC_DEBOUNCE_1 = 1;
        /************ TIMER/COUNTER **********************/
        public const short TC_CONTROL = 0;	//TC_function
        public const short TC_MODE = 1;
        public const short TI_GATE_MODE = 2;
        public const short TO_MODE = 3;
        public const short RETRIGGER_MODE = 4;
        public const short PRELOAD = 5;
        public const short COUNTER = 6;
        public const short OUT_WIDTH = 7;
        public const short QUADURATE = 8;

        public const short STOP = 0;//tc_control
        public const short RUN = 1;

        public const short IO = 0;	//Interrupt source
        public const short TC = 1;
        public const short PCI_ENABLE = 2;
        //******************ERROR CODE OVERFLOW CODE*******************/
        public const int ushortBIT_MAX = 65535;
        public const int ushortBIT_MIN = -65535;
        public const short CNT_BUFFER = 1023;
        public const short ONE = 1;
        public const short TWO = 2;
        public const short THREE = 3;
        public const short FOUR = 4;
        public const short SEVEN = 7;
        public const long TIMER_MAX = 0xFFFFFFFF;
        public const short POLARITY_MAX = 0xFF;
        public const short VALUE_MAX = 0xF;
        public const short MASK_MAX = 0xFF;
        //-------------------------------------------------------------

        public const short cDeviceAddress = 6;	//EPROM ADDRESS
        public const short SUCCESS = 0;
        public const short STATUS_SUCCESS = 1;
        //--------------------------------------------------------------


        //********* T/C****************	20080922
        public struct Timer_struct
        {
            ushort Ti_Gate_MODE;
            //  0: NO_GATE //Always count without gate function,
            //IN00 is digital input.
            //  1:GATED_HIGH 
            //IN00 is gate input, after command start_TC,
            //if internal logic active high timer will start 
            //counting and logic low will halt counting. 
            //  2: GATED_LOW//IN00 is gate input, after command start_TC,
            //if internal logic active low will start timer
            //counting and logic high will halt counting.
            uint time_const;
            // Timer constant based on 1us clock
            ushort Tout_mode;
            //  0: NO_TOUT ,
            //OUT00 use as general digital output
            //  1: OUT_PULSE 
            //OUT00:timer cross zero output pulse. 
            //(out_width effective)
            //  2: OUT_LEVEL 
            //OUT00: timer cross zero output will make.
            //  4:OUT_TOGGLE 
            //OUT00: timer cross zero toggles output
            ushort Tout_width;
            // Output pulse width based on 1us clock, only
            //valid in Tout_mode is OUT_PULSE
            ushort cont_single;
            //  0: SINGLE_CYCLE
            //single cycle mode,timer will stop operation
            //when time constant count down to zero.
            //  1: ALWAYS_RUN
            //continuous operation mode, timer will reload 
            //time constant and continue operation when 
            //time constant count down to zero.
        }
        public struct Counter_struct
        {
            ushort Ti_Gate_MODE;
            //  0: invalid
            //  1:Ti_HIGH
            //IN00 is counter pulse input, after command
            //start_TC,counter will count on internal logic
            //active high transition 
            //  2:Ti_LOW 
            //IN00 is counter pulse input, after command 
            //start_TC, counter will count on internal logic 
            //active low transition 
            uint counter_const;
            // Counter constant
            ushort Tout_mode;
            //  0: NO_TOUT 
            // OUT00 use as general digital output
            //  1: OUT_PULSE //OUT00: timer cross zero output pulse.
            //(out_width effective)
            //  2: OUT_LEVEL 
            //OUT00: timer cross zero output will make.
            //  4:OUT_TOGGLE //OUT00: timer cross zero toggles output

            ushort Tout_width;
            // Output pulse width based on 1us clock, only
            //valid in Tout_mode is OUT_PULSE
            ushort cont_single;
            //  0: SINGLE_CYCLE
            //single cycle mode, counter will stop operation 
            //when time constant count down to zero.
            //  1: ALWAYS_RUN
            // continuous operation mode, counter will reload  
            //time constant and continue operation when time  
            //constant count down to zero.
        }
        public struct PWM_struct
        {
            ushort Ti_Gate_MODE;
            //  0: NO_GATE
            //Always count without gate function,
            //IN00 is digital input.
            //  1:GATED_HIGH 
            //IN00 is gate input, after command start_TC,
            //if internal logic active high PWM will start
            //counting and logic low will freeze PWM.
            //  2: GATED_LOW//IN00 is gate input, after command start_TC,
            //if internal logic active low will start PWM 
            //counting and logic high will freeze PWM.
            ushort Tout_mode;
            //  0: NO_TOUT
            // OUT00 use as general digital output
            //  4:OUT_TOGGLE 
            //OUT00: timer cross zero toggles output
            ushort PWM_freq;
            ushort PWM_duty;
        }

        //-----------------------------------------------------------------

        //***********************************************************************************8888
        [DllImport("DIO3208B_64.dll", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
        public static extern int dio3208B_initial();
        [DllImport("DIO3208B_64.dll", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
        public static extern int dio3208B_close();
        [DllImport("DIO3208B_64.dll", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
        public static extern int dio3208B_info(byte CardID, ref ushort address, ref ushort TC_address);
        [DllImport("DIO3208B_64.dll", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
        public static extern int dio3208B_read_port(byte CardID, byte port, ref byte data);
        [DllImport("DIO3208B_64.dll", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
        public static extern int dio3208B_read_in_point(byte CardID, byte point, ref byte state);
        [DllImport("DIO3208B_64.dll", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
        public static extern int dio3208B_read_out_point(byte CardID, byte point, ref byte state);
        [DllImport("DIO3208B_64.dll", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
        public static extern int dio3208B_set_port(byte CardID, byte data);
        [DllImport("DIO3208B_64.dll", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
        public static extern int dio3208B_set_out_point(byte CardID, byte point, byte state);
        [DllImport("DIO3208B_64.dll", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
        public static extern int dio3208B_set_debounce_time(byte CardID, byte data);			//2008.06.23
        [DllImport("DIO3208B_64.dll", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
        public static extern int dio3208B_read_debounce_time(byte CardID, ref byte data);			//2008.06.23
        
        //*******  Interrupt Section (support for dio3208BA version only) ***********
        [DllImport("DIO3208B_64.dll", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
        public static extern int dio3208B_enable_IRQ(byte CardID, ref int phEvent);
        [DllImport("DIO3208B_64.dll", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
        public static extern int dio3208B_disable_IRQ(byte CardID);
        [DllImport("DIO3208B_64.dll", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
        public static extern int dio3208B_set_IRQ_mask(byte CardID, byte source, byte mask); //0526
        [DllImport("DIO3208B_64.dll", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
        public static extern int dio3208B_read_IRQ_mask(byte CardID, byte source, byte mask);//0526
        [DllImport("DIO3208B_64.dll", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
        public static extern int dio3208B_read_IRQ_status(byte CardID, byte source, ref byte Event_Status);		//new resource 0521
        [DllImport("DIO3208B_64.dll", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
        public static extern int dio3208B_link_IRQ_process(byte CardID, IRQ_Process lpIRQ_Process);
        [DllImport("DIO3208B_64.dll", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
        //*******  Security Section (support for dio3208BA version only) ************
        public static extern int dio3208B_read_security_status(byte CardID, ref byte lock_status, ref byte seurity_enable);
        [DllImport("DIO3208B_64.dll", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
        public static extern int dio3208B_unlock_security(byte CardID, ushort[] password);
        [DllImport("DIO3208B_64.dll", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
        public static extern int dio3208B_set_password(byte CardID, ushort[] password);
        [DllImport("DIO3208B_64.dll", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
        public static extern int dio3208B_change_password(byte CardID, ref ushort[] Oldpassword, ref ushort[] Password);
        [DllImport("DIO3208B_64.dll", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
        public static extern int dio3208B_clear_password(byte CardID, ref ushort[] Password);

        //-------------------------------------------------------------------------------------
        //------------------------------------------------------------------
        [DllImport("DIO3208B_64.dll", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
        public static extern int dio3208B_set_timer(byte CardID, ref Timer_struct Timer_struct);
        [DllImport("DIO3208B_64.dll", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
        public static extern int dio3208B_set_counter(byte CardID, ref Counter_struct Counter_struct);
        [DllImport("DIO3208B_64.dll", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
        public static extern int dio3208B_set_PWM(byte CardID, ref PWM_struct PWM_struct);
        [DllImport("DIO3208B_64.dll", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
        public static extern int dio3208B_start_TC(byte CardID);
        [DllImport("DIO3208B_64.dll", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
        public static extern int dio3208B_stop_TC(byte CardID);
        [DllImport("DIO3208B_64.dll", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
        public static extern int dio3208B_set_TC(byte CardID, byte index, uint data);
        [DllImport("DIO3208B_64.dll", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
        public static extern int dio3208B_read_TC(byte CardID, byte index, ref uint data);

        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        public delegate void IRQ_Process(byte CardID);

    }
}
