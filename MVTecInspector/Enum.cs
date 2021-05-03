using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonInspector
{
    public enum EmInsType : int { LeftAlign = 0, RightAlign, MarkArea, DataMatrix, CellMarkArea, FirstUnit, CellArea, NA }
    public enum EmMeasureType { NccModel, ShapeModel, MeasureLine, MeasurePoint, MeasurePair, EdgeThreshold, NoAlignArea, NoInspArea }
    public enum EmAlignType { ShapeModel, NccModel }
    public enum EmWindowEditType { Rec, Rec2, Line, Polygon }
    public  enum PreType { 無, 高斯平滑, 中值, 灰階閉運算 , 灰階開運算 }
    public enum EmMetrologyPrm { distance_threshold, instances_outside_measure_regions, max_num_iterations, measure_distance, measure_interpolation, measure_select, measure_transition, min_score, num_instances, num_measures, rand_seed }

    //public enum 
}
