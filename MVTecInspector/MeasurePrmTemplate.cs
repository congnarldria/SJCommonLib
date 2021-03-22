using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using HalconDotNet;

namespace CommonInspector
{
    public class MeasurePrmTemplate
    {

    }
    public class Tc
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
        public static bool IsTupleNotEmptry(HTuple val)
        {
            HOperatorSet.TupleLength(val, out HTuple Count);
            if (Count > 0)
                return true;
            else
                return false;
        }
        public static void SafeDispose(HObject obj)
        {
            if (IsValid(obj))
                obj.Dispose();
        }
    }
    public enum EmTransition { all, positive, negative }
    public enum EmSelect { all, firse, last }
    [Serializable]
    public class TMeasurePosPrm
    {
        public TMeasurePosPrm()
        {

        }
        public EmTransition Transition { get; set; } = EmTransition.all;
        public EmSelect Select { get; set; } = EmSelect.firse;
        public int Threashold { get; set; } = 30;
        public int Sigma { get; set; } = 1;
        public byte[] MeasureHandleByteArray { get; set; } = null;
        [XmlIgnore]
        public HTuple MeasureHandle { get; set; } = null;
        public void SaveMeasurePos()
        {
            if (MeasureHandle != null)
            {
                HOperatorSet.SerializeMeasure(MeasureHandle, out HTuple SeriallizeItem);
                HSerializedItem item = new HSerializedItem(SeriallizeItem.H);
                MeasureHandleByteArray = item;
                item.Dispose();
            }
        }
        public void LoadMeasurePos()
        {
            if (MeasureHandleByteArray != null)
            {
                byte[] bb = MeasureHandleByteArray;
                HSerializedItem item = new HSerializedItem();
                unsafe
                {
                    fixed (byte* p = &bb[0])
                    {
                        item.CreateSerializedItemPtr((IntPtr)p, bb.Length, "true");
                    }
                    HOperatorSet.DeserializeMeasure(item, out HTuple m_Handle);
                    MeasureHandle = m_Handle;
                }
            }
        }
    }
    [Serializable]
    public class TAddMetrologyObjectLineMeasurePrm
    {
        public TAddMetrologyObjectLineMeasurePrm()
        {

        }
        [XmlIgnore]
        public HTuple MetrologyHandle { get; set; } = null;
        public double RowBegin { get; set; } = 0;
        public double ColumnBegin { get; set; } = 0;
        public double RowEnd { get; set; } = 1;
        public double ColumnEnd { get; set; } = 1;
        public double MeasureLength1 { get; set; } = 20;
        public double MeasureLength2 { get; set; } = 5;
        public double MeasureSigma { get; set; } = 1;
        public double MeasureThreshold { get; set; } = 30;
        public EmMetrologyPrm MetrologyPrm = EmMetrologyPrm.distance_threshold;
        public byte[] MetrologyandleByteArray { get; set; } = null;
        [XmlIgnore]
        public HTuple MeasureHandle { get; set; } = null;
        public void SaveMetrologyPrm()
        {
            if (MeasureHandle != null)
            {
                HOperatorSet.SerializeMeasure(MeasureHandle, out HTuple SeriallizeItem);
                HSerializedItem item = new HSerializedItem(SeriallizeItem.H);
                MetrologyandleByteArray = item;
                item.Dispose();
            }
        }
        public void LoadMetrologyPrm()
        {
            if (MetrologyandleByteArray != null)
            {
                byte[] bb = MetrologyandleByteArray;
                HSerializedItem item = new HSerializedItem();
                unsafe
                {
                    fixed (byte* p = &bb[0])
                    {
                        item.CreateSerializedItemPtr((IntPtr)p, bb.Length, "true");
                    }
                    HOperatorSet.DeserializeMeasure(item, out HTuple m_Handle);
                    MeasureHandle = m_Handle;
                }
            }
        }
    }
    public enum EmMetrologyPrm { distance_threshold, instances_outside_measure_regions, max_num_iterations, measure_distance, measure_interpolation, measure_select, measure_transition, min_score, num_instances, num_measures, rand_seed }

}

