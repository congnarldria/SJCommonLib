using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HalconDotNet;

namespace CommonInspector
{
    public class TMVTecInspector
    {

    }
    public class TMeasurePrm
    {
    }
    public class TMeasureResult
    {

    }
    public class TDefectInsPrm
    {

    }
    public class TDefectInsResult
    {

    }
    public static class TCommonHalcon
    {
        private static bool IsValid(HObject obj)
        {
            if (obj == null) return false;
            if (!obj.IsInitialized())
            {
                return false;
            }
            return true;
        }
    }
}
