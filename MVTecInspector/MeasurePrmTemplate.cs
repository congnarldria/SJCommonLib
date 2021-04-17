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
    public enum EmTransition { all, positive, negative, separate }
    public enum EmSelect { all, first, last, separate }
    [Serializable]
    public class TMeasurePosPrm
    {
        public TMeasurePosPrm()
        {

        }
        public EmTransition Transition { get; set; } = EmTransition.all;
        public EmSelect Select { get; set; } = EmSelect.first;
        public string MeasureTransition { get; set; } = "all";
        public string MeasureSelect { get; set; } = "first";
        public int Threashold { get; set; } = 30;
        public int Sigma { get; set; } = 1;
        public double Amplitude { get; set; } = 30;
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
        public void CreateMetrologyModel()
        {
            if (Tc.IsTupleNotEmptry(MetrologyHandle))
            {
                HOperatorSet.ClearMetrologyModel(MetrologyHandle);
            }
            HOperatorSet.CreateMetrologyModel(out HTuple MetrologyModel);
            MetrologyHandle = MetrologyModel;
        }
        public void SetPrm()
        {
            HOperatorSet.SetMetrologyModelParam(MetrologyHandle, EmMetrologyPrm.num_measures.ToString(), NumMeasures);
            HOperatorSet.SetMetrologyModelParam(MetrologyHandle, EmMetrologyPrm.measure_transition.ToString(), MeasureTransition);
            HOperatorSet.SetMetrologyModelParam(MetrologyHandle, EmMetrologyPrm.measure_select.ToString(), MeasureSelect);
            HOperatorSet.SetMetrologyModelParam(MetrologyHandle, EmMetrologyPrm.min_score.ToString(), MinScore);
        }
        [XmlIgnore]
        public HTuple MetrologyHandle { get; set; } = null;
        public double RowBegin { get; set; } = 0;
        public double ColumnBegin { get; set; } = 0;
        public double RowEnd { get; set; } = 1;
        public double ColumnEnd { get; set; } = 1;
        public double MeasureSigma { get; set; } = 1;
        public double MeasureThreshold { get; set; } = 30;
        public int NumMeasures { get; set; } = 100;
        public double MeasureLength1 { get; set; } = 30;
        public double MeasureLength2 { get; set; } = 5;

        public string MeasureTransition { get; set; } = "positive";
        public string MeasureSelect { get; set; } = "all";
        public double MinScore { get; set; } = 0.6;
        [XmlIgnore]
        public double LineRowBegin { get; set; } = 0;
        [XmlIgnore]
        public double LineRowEnd { get; set; } = 0;
        [XmlIgnore]
        public double LineColBegin { get; set; } = 10;
        [XmlIgnore]
        public double LineColEnd { get; set; } = 10;

        public EmMetrologyPrm MetrologyPrm = EmMetrologyPrm.distance_threshold;
        public byte[] MetrologyandleByteArray { get; set; } = null;
        [XmlIgnore]
        public HObject LineObj = null;
        public void SaveMetrologyPrm()
        {
            if (MetrologyHandle != null)
            {
                HOperatorSet.SerializeMeasure(MetrologyHandle, out HTuple SeriallizeItem);
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
                    MetrologyHandle = m_Handle;
                }
            }
        }
    }
    public enum EmMetrologyPrm { distance_threshold, instances_outside_measure_regions, max_num_iterations, measure_distance, measure_interpolation, measure_select, measure_transition, min_score, num_instances, num_measures, rand_seed }

}

