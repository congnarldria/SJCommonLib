using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HalconDotNet;
using System.Collections.Concurrent;
using System.Threading;
using System.Xml.Serialization;
using System.IO;
using ATMTCommonLib;

namespace CommonInspector
{
    public class TMVTecInspector
    {

    }
    [Serializable]
    public class TAlignInfoBase : ICloneable
    {
        public TAlignInfoBase()
        {

        }
        public double Row { get; set; }
        public double Col { get; set; }
        public double Angle { get; set; }
        public bool IsLearn { get; set; } = false;
        [XmlIgnore]
        public double RRow;
        [XmlIgnore]
        public double RCol;
        [XmlIgnore]
        public double RAngle;
        [XmlIgnore]
        public double RScore;

        public object Clone()
        {
            return MemberwiseClone();
        }
    }
    [Serializable]
    public class TWindowInfoBase : ICloneable, IDisposable
    {
        public TWindowInfoBase()
        {

        }
        public TWindowInfoBase(InsType _type, double r1, double c1, double r2, double c2, bool IsUpSide)
        {
            insType = _type;
            Row1 = r1;
            Col1 = c1;
            Row2 = r2;
            Col2 = c2;
            UpSide = IsUpSide;
        }
        public TWindowInfoBase(EmMeasureType _type, double r1, double c1, double r2, double c2, bool IsUpSide)
        {
            MeasureType = _type;
            Row1 = r1;
            Col1 = c1;
            Row2 = r2;
            Col2 = c2;
            UpSide = IsUpSide;
        }
        [XmlIgnore]
        public TWindowInfoBase SubWin { get; set; } = null;
        public double DX { get; set; } = 0;
        public double DY { get; set; } = 0;
        [XmlIgnore]
        public double W
        {
            get
            {
                return Col2 - Col1 + 1;
            }
        }
        [XmlIgnore]
        public double H
        {
            get
            {
                return Row2 - Row1 + 1;
            }
        }
        [XmlIgnore]
        public bool IsXXMark = false;
        public InsType insType { get; set; }
        public EmMeasureType MeasureType { get; set; }
        public EmAlignType AlignType { get; set; }
        [XmlIgnore]
        public EmWindowEditType WindowEditType
        {
            get
            {
                if (MeasureType == EmMeasureType.MeasurePair || MeasureType == EmMeasureType.MeasurePoint)
                {
                    return EmWindowEditType.Rec2;
                }
                else if (MeasureType == EmMeasureType.MeasureLine)
                {
                    return EmWindowEditType.Line;
                }
                else
                {
                    return EmWindowEditType.Rec;
                }
            }
        }
        public bool UpSide { get; set; } = true;
        private TAlignInfoBase _AlignInfo = new TAlignInfoBase();
        public TAlignInfoBase AlignInfo
        {
            get
            {
                if (DeepClone)
                {
                    return _AlignInfo.Clone() as TAlignInfoBase;
                }
                else
                    return _AlignInfo;
            }
            set
            {
                if (DeepClone)
                {
                    _AlignInfo = value.Clone() as TAlignInfoBase;
                }
                else
                {
                    _AlignInfo = value;
                }
            }
        }
        public TMeasurePosPrm MeasurePosPrm { get; set; } = new TMeasurePosPrm();
        public TAddMetrologyObjectLineMeasurePrm AddMetrologyObjectLineMeasurePrm { get; set; } = new TAddMetrologyObjectLineMeasurePrm();
        public bool DeepClone { get; set; } = false;
        public string AlignTo { get; set; } = string.Empty;
        public string GoldenAlignTo { get; set; } = string.Empty;
        public double Row1 { get; set; } = 0;
        public double Row2 { get; set; } = 0;
        public double Col1 { get; set; } = 10;
        public double Col2 { get; set; } = 10;
        public void ClearAligned()
        {
            dr = 0;
            dc = 0;
            dphi = 0;
        }
        public double dr = 0;
        public double dc = 0;
        public double dphi = 0;
        private double _Row = 1;
        public double Row
        {
            get
            {
                return _Row + dr;
            }
            set
            {
                _Row = value;
            }
        }
        private double _Col = 1;
        public double Col
        {
            get
            {
                return _Col + dc;
            }
            set
            {
                _Col = value;
            }
        }
        private double _Phi = 0;
        public double Phi
        {
            get
            {
                return _Phi + dphi;
            }
            set
            {
                _Phi = value;
            }
        }
        public double L1 { get; set; } = 100;
        public double L2 { get; set; } = 5;
        public int UnitX { get; set; } = 1;
        public int UnitY { get; set; } = 1;
        public int RealX { get; set; } = 1;
        public int RealY { get; set; } = 1;
        public int BlockX { get; set; } = 1;
        public int BlockY { get; set; } = 1;
        public int RealBlockX { get; set; } = 1;
        public int RealBlockY { get; set; } = 1;
        public int SearchW { get; set; } = 300;
        public int SearchH { get; set; } = 100;
        public bool UseTh { get; set; } = false;
        public bool UseMTh { get; set; } = false;
        public bool UseMorth { get; set; } = false;
        public int Range { get; set; } = 3;
        public bool MorphDark { get; set; } = false;
        public int DynTh { get; set; } = 30;
        public double MinArea { get; set; } = 0;
        public double Diameter { get; set; } = 0;
        public double Ratio { get; set; } = 1;
        public bool UseSingle { get; set; } = false;
        public bool PrintThUse { get; set; } = false;
        public int PrintTh { get; set; } = 0;
        public int Circulity { get; set; } = 80;
        public double Rectangularity { get; set; } = 5;
        [XmlIgnore]
        public double MeasureRectangularity { get; set; } = 0;
        public int Erosion { get; set; } = 0;
        public int DefectGray { get; set; } = 0;
        public bool IsGraySmaller { get; set; } = false;
        public string PreImage { get; set; } = PreType.無.ToString();
        public int PreMask { get; set; } = 3;
        public bool PinDirRight { get; set; } = true;
        public int Score { get; set; } = 60;
        public bool IsAlignSuccess = false;
        public bool IsGoldenAlignSuccess = false;
        [XmlIgnore]
        public int FindScore { get; set; } = 0;
        public int ThL { get; set; } = 0;
        public int ThH { get; set; } = 10;
        public int VarTh { get; set; } = 15;
        public int VarArea { get; set; } = 25;
        public bool Darker { get; set; } = true;
        public int Dilate { get; set; } = 0;
        //GlueOverFlow
        public double GlueSpurMin { get; set; } = 0;
        //PinDip
        public double GlueWMax { get; set; } = 0;
        public double GlueWMin { get; set; } = 0;
        public double GlueHMin { get; set; } = 0;
        [XmlIgnore]
        public bool IsMirrDisplay = false;
        [XmlIgnore]
        private HObject _Result = new HObject();
        [XmlIgnore]
        public HObject Result
        {
            get
            {
                if (DeepClone)
                {
                    if (IsValid(_Result))
                        return _Result.Clone();
                    else
                        return new HObject();
                }
                else
                    return _Result;
            }
            set
            {
                _Result = value;
            }
        }
        public byte[] AlignByteArray { get; set; }

        [XmlIgnore]
        private HObject _AffineRegion = new HObject();
        [XmlIgnore]
        public HObject AffineRegion
        {
            get
            {
                if (DeepClone)
                {
                    if (IsValid(_AffineRegion))
                        return _AffineRegion.Clone();
                    else
                        return new HObject();
                }
                else
                    return _AffineRegion;
            }
            set
            {
                if (IsValid(_AffineRegion)) _AffineRegion.Dispose();
                _AffineRegion = value;
            }
        }
        [XmlIgnore]
        public HObject Edges = new HObject();
        [XmlIgnore]
        private HObject _AffineCountour = new HObject();
        [XmlIgnore]
        public HObject AffineCountour
        {
            get
            {
                if (DeepClone)
                {
                    if (IsValid(_AffineCountour))
                        return _AffineCountour.Clone();
                    else
                        return new HObject();
                }
                else
                    return _AffineCountour;
            }
            set
            {
                _AffineCountour = null;
                _AffineCountour = value;
            }
        }
        [XmlIgnore]
        public HTuple ShapeAlignModelID { get; set; } = null;
        [XmlIgnore]
        public HTuple NccAlignModelID { get; set; } = null;
        [XmlIgnore]
        private byte[] _ObjByteArray = null;
        public byte[] ObjByteArray
        {
            get
            {
                if (DeepClone)
                {
                    if (_ObjByteArray == null)
                        return null;
                    else
                        return _ObjByteArray.Clone() as byte[];
                }
                else
                    return _ObjByteArray;
            }
            set
            {
                if (DeepClone)
                {
                    if (_ObjByteArray != null)
                        _ObjByteArray = value.Clone() as byte[];
                }
                else
                {
                    _ObjByteArray = value;
                }
            }
        }
        [XmlIgnore]
        private HObject _GoldenObj = new HObject();
        [XmlIgnore]
        public HObject GolenObj
        {
            get
            {
                if (DeepClone)
                {
                    if (IsValid(_GoldenObj))
                        return _GoldenObj.Clone();
                    else
                        return new HObject();
                }
                else
                    return _GoldenObj;
            }
            set
            {
                _GoldenObj = value;
            }
        }
        [XmlIgnore]
        private HObject _AffineGolenObj = new HObject();
        [XmlIgnore]
        public HObject AffineGolenObj
        {
            get
            {
                if (DeepClone)
                {
                    if (IsValid(_AffineGolenObj))
                        return _AffineGolenObj.Clone();
                    else
                        return new HObject();
                }
                else
                    return _AffineGolenObj;
            }
            set
            {
                _AffineGolenObj = value;
            }
        }
        public void SaveHalconAlign(HTuple AlignHandle)
        {
            HTuple hhandle = null;
            try
            {
                if (AlignType == EmAlignType.ShapeModel)
                {
                    HOperatorSet.SerializeShapeModel(AlignHandle, out hhandle);
                }
                else
                {
                    HOperatorSet.SerializeNccModel(AlignHandle, out hhandle);
                }
                HSerializedItem item = new HSerializedItem(hhandle.H);

                AlignByteArray = item;
                item.Dispose();
            }
            catch (Exception ex)
            {
                LogMgr.SendLog(ex.ToString(), ex);
            }
        }
        public void LoadHalconAlign()
        {
            try
            {
                if (AlignByteArray != null)
                {
                    byte[] bb = AlignByteArray;
                    HSerializedItem item = new HSerializedItem();
                    unsafe
                    {
                        fixed (byte* p = &bb[0])
                        {
                            item.CreateSerializedItemPtr((IntPtr)p, bb.Length, "true");
                        }
                        if (AlignType == EmAlignType.ShapeModel)
                        {
                            HOperatorSet.DeserializeShapeModel(item, out HTuple ShapeModelID);
                            ShapeAlignModelID = ShapeModelID.Clone();
                            ShapeModelID.Dispose();
                        }
                        if (AlignType == EmAlignType.NccModel)
                        {
                            HOperatorSet.DeserializeNccModel(item, out HTuple NCCModel);
                            NccAlignModelID = NCCModel.Clone();
                            NCCModel.Dispose();
                        }
                    }
                    item.Dispose();
                }

            }
            catch (Exception ex)
            {
                LogMgr.SendLog(ex.ToString());
            }
        }
        public void SaveHalconGolenObject(HObject obj)
        {
            try
            {
                HOperatorSet.AreaCenter(obj, out HTuple a, out HTuple r, out HTuple c);
                MemoryStream ms = new MemoryStream();
                obj.Serialize(ms);
                if (ObjByteArray == null) ObjByteArray = new byte[0];
                ObjByteArray = ms.ToArray();
                ms.Dispose();
            }
            catch (Exception e)
            {
                LogMgr.SendLog(e.ToString(), e);
            }
        }
        public void LoadHalconObject()
        {
            try
            {
                if (ObjByteArray != null)
                {
                    byte[] bb = ObjByteArray;
                    HSerializedItem item = new HSerializedItem();
                    unsafe
                    {
                        fixed (byte* p = &bb[0])
                        {
                            item.CreateSerializedItemPtr((IntPtr)p, bb.Length, "true");
                        }
                        GolenObj.DeserializeObject(item);
                    }
                    item.Dispose();
                }
            }
            catch (Exception ex)
            {
                LogMgr.SendLog(ex.ToString(), ex);
            }
        }
        [XmlIgnore]
        private HObject _Rec = null;
        [XmlIgnore]
        public HObject Rec
        {
            get
            {
                if (IsValid(_Rec)) _Rec.Dispose();
                HOperatorSet.GenRectangle1(out _Rec, Row1, Col1, Row2, Col2);
                return _Rec;
            }
        }
        private bool IsValid(HObject obj)
        {
            if (obj == null) return false;
            if (!obj.IsInitialized()) return false;
            return true;
        }
        public object Clone()
        {
            TWindowInfoBase win = this.MemberwiseClone() as TWindowInfoBase;
            win.NccAlignModelID = new HTuple();
            win.ShapeAlignModelID = new HTuple();
            return win;
        }
        [XmlIgnore]
        private bool _disposed = false;
        ~TWindowInfoBase() => Dispose(false);
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        protected virtual void Dispose(bool disposing)
        {
            if (_disposed)
            {
                return;
            }

            if (disposing)
            {
                // TODO: dispose managed state (managed objects).
                AlignByteArray = null;
                ObjByteArray = null;
                GC.Collect();
            }

            // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
            // TODO: set large fields to null.
            if (IsValid(Result)) Result.Dispose();
            if (IsValid(AffineRegion)) AffineRegion.Dispose();
            if (IsValid(AffineCountour)) AffineCountour.Dispose();
            if (IsValid(GolenObj)) GolenObj.Dispose();
            if (IsValid(AffineGolenObj)) AffineGolenObj.Dispose();
            if (IsValid(Rec)) Rec.Dispose();
            _disposed = true;
        }
    }
    [Serializable]
    public class TViewBase
    {
        public int ID { get; set; } = 0;
        public double Pos { get; set; } = 0;
        public TViewBase()
        {

        }
        public TViewBase(int Index)
        {
            ID = Index;
        }
        public List<TWindowInfoBase> Wins { get; set; } = new List<TWindowInfoBase>();
    }
    [Serializable]
    public class TRecipeBase
    {
        public  virtual  T  Load<T>(string FileName)
        {
            T rcp;
            XmlSerializer mySerializer = new XmlSerializer(typeof(T));
            try
            {
                if (File.Exists(FileName))
                {
                    using (FileStream myFileStream = new FileStream(FileName, FileMode.Open))
                    {
                        rcp = (T)mySerializer.Deserialize(myFileStream);
                    }
                }
                else
                {
                    if (File.Exists(Environment.CurrentDirectory+ "\\Recipe\\Default.rcp"))
                    {
                        rcp = Load<T>(Environment.CurrentDirectory + "\\Recipe\\Default.rcp");
                    }
                }
            }
            catch (Exception e)
            {
                return default(T);
            }
            return default(T);
        }
    }
    public class Tc
    {
        public static bool IsValid(HObject obj)
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
            if (val == null) return false;
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
}
