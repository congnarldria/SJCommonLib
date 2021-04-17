using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonInspector
{
    public enum InsType : int { LeftAlign = 0, RightAlign, MarkArea, DataMatrix, CellMarkArea, FirstUnit, CellArea, NA }
    public enum EmMeasureType { NccModel, ShapeModel, MeasureLine, MeasurePoint, MeasurePair, EdgeThreshold, NoAlignArea, NoInspArea }
    public enum EmAlignType { ShapeModel, NccModel }
    public enum EmWindowEditType { Rec, Rec2, Line, Polygon }
    public  enum PreType { 無, 高斯平滑, 中值, 灰階閉運算 }
}
