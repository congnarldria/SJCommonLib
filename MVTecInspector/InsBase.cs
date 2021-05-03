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
        public TAlignInfoBase() { }
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
    public partial class TWindowEditor
    {
        private double HX = 0, HY = 0;
        private double MX = 0, MY = 0;
        HTuple DrawID = 0;
        public bool bIsDrawing = false;
        public int ZoomIndex = 2;
        private double[] ZoomFactor = new double[10] { 0.125, 0.25, 0.5, 1, 2, 4, 8, 16, 32, 64 };
        public TWindowEditor() { }
        private HWindow hWin
        {
            get
            {
                return hWinX.HalconWindow;
            }
        }
        private static HWindowControl hWinX;
        private void AddAnyMeasure<T>(EmMeasureType _type, int InspectIndex, HTuple PartRow, HTuple PartCol, HTuple PartWidth, HTuple PartHeight)
        {
            EmMeasureType mes = _type;
            hWinX.Focus();
            HTuple Row1, Row2, Col1, Col2;
            HOperatorSet.SetColor(hWinX.HalconWindow, "red");
            HOperatorSet.SetFont(hWinX.HalconWindow, "Verdana-Normal-14");
            HOperatorSet.DispText(hWinX.HalconWindow, "滑鼠左鍵編輯，按右鍵結束編輯", "image", PartRow, PartCol, "black", "box", "true");
            hWinX.Focus();
            if (mes == EmMeasureType.MeasureLine | mes == EmMeasureType.MeasurePoint | mes == EmMeasureType.MeasurePair)
            {
                bool ret = ATMTVirtualMouse.LockCursor(hWinX);
                HOperatorSet.DrawLine(hWin, out Row1, out Col1, out Row2, out Col2);
                ATMTVirtualMouse.UnLockCursor();
                TWindow NewWindow = new TWindow(mes, Row1, Col1, Row2, Col2);
                NewWindow.AddMetrologyObjectLineMeasurePrm.RowBegin = Row1;
                NewWindow.AddMetrologyObjectLineMeasurePrm.ColumnBegin = Col1;
                NewWindow.AddMetrologyObjectLineMeasurePrm.RowEnd = Row2;
                NewWindow.AddMetrologyObjectLineMeasurePrm.ColumnEnd = Col2;
                NewWindow.Row = (Row1 + Row2) / 2.0f;
                NewWindow.Col = (Col1 + Col2) / 2.0f;
                HOperatorSet.AngleLx(Row1, Col1, Row2, Col2, out HTuple Phi);
                NewWindow.Phi = Phi;
                HOperatorSet.DistancePp(Row1, Col1, Row2, Col2, out HTuple Distance);
                NewWindow.L1 = Distance / 2;
                NewWindow.L2 = Distance / 10 + 1.5;
                HOperatorSet.GenRectangle2(out HObject Rect, NewWindow.Row, NewWindow.Col, NewWindow.Phi, NewWindow.L1, NewWindow.L2);
                HOperatorSet.SmallestRectangle1(Rect, out Row1, out Col1, out Row2, out Col2);
                NewWindow.Row1 = Row1;
                NewWindow.Row2 = Row2;
                NewWindow.Col1 = Col1;
                NewWindow.Col2 = Col2;
                NewWindow.MeasureType = mes;
                VDM.Sgt.rcp.Views[InspectIndex].Wins.Add(NewWindow);
                Rect.Dispose();
            }
            else
            {
                bool ret = ATMTVirtualMouse.LockCursor(hWinX);
                HOperatorSet.DrawRectangle1Mod(hWinX.HalconWindow, MY, MX, MY + 100 / ZoomFactor[ZoomIndex], MX + 100 / ZoomFactor[ZoomIndex], out Row1, out Col1, out Row2, out Col2);
                ATMTVirtualMouse.UnLockCursor();
                HOperatorSet.GenRectangle1(out HObject Rec, Row1, Col1, Row2, Col2);
                HOperatorSet.SmallestRectangle1(Rec, out Row1, out Col1, out Row2, out Col2);
                TWindow w = new TWindow(mes, Row1, Col1, Row2, Col2);
                if (mes == EmMeasureType.NccModel)
                    w.AlignType = EmAlignType.NccModel;
                else
                    w.AlignType = EmAlignType.ShapeModel;
                w.MeasureType = mes;
                VDM.Sgt.rcp.Views[InspectIndex].Wins.Add(w);
                Rec.Dispose();
            }
        }
        private void EditWin(int InspectIndex, int WindowIndex, HTuple PartRow, HTuple PartCol)
        {
            hWinX.Focus();
            if (WindowIndex > -1)
            {
                HTuple Row1, Row2, Col1, Col2;
                if (VDM.Sgt.rcp.Views[InspectIndex].Wins[WindowIndex].WindowEditType == EmWindowEditType.Rec)
                {
                    HOperatorSet.SetColor(hWinX.HalconWindow, "red");
                    HOperatorSet.SetFont(hWinX.HalconWindow, "Verdana-Normal-14");
                    HOperatorSet.DispText(hWinX.HalconWindow, "滑鼠左鍵編輯，按右鍵結束編輯", "image", PartRow, PartCol, "black", "box", "true");
                    hWinX.Focus();
                    bool ret = ATMTVirtualMouse.LockCursor(hWinX);
                    HOperatorSet.DrawRectangle1Mod(hWinX.HalconWindow, VDM.Sgt.rcp.Views[InspectIndex].Wins[WindowIndex].Row1
                        , VDM.Sgt.rcp.Views[InspectIndex].Wins[WindowIndex].Col1
                        , VDM.Sgt.rcp.Views[InspectIndex].Wins[WindowIndex].Row2
                        , VDM.Sgt.rcp.Views[InspectIndex].Wins[WindowIndex].Col2, out Row1, out Col1, out Row2, out Col2);
                    ATMTVirtualMouse.UnLockCursor();
                    HOperatorSet.GenRectangle1(out HObject Rec, Row1, Col1.D, Row2, Col2);
                    HOperatorSet.SmallestRectangle1(Rec, out Row1, out Col1, out Row2, out Col2);
                    Rec.Dispose();
                    VDM.Sgt.rcp.Views[InspectIndex].Wins[WindowIndex].Row1 = Row1;
                    VDM.Sgt.rcp.Views[InspectIndex].Wins[WindowIndex].Col1 = Col1;
                    VDM.Sgt.rcp.Views[InspectIndex].Wins[WindowIndex].Row2 = Row2;
                    VDM.Sgt.rcp.Views[InspectIndex].Wins[WindowIndex].Col2 = Col2;
                }
                else if (VDM.Sgt.rcp.Views[InspectIndex].Wins[WindowIndex].WindowEditType == EmWindowEditType.Rec2)
                {
                    HOperatorSet.SetColor(hWinX.HalconWindow, "red");
                    HOperatorSet.SetFont(hWinX.HalconWindow, "Verdana-Normal-14");
                    HOperatorSet.DispText(hWinX.HalconWindow, "滑鼠左鍵編輯，按右鍵結束編輯", "image", PartRow, PartCol, "black", "box", "true");
                    hWinX.Focus();
                    bool ret = ATMTVirtualMouse.LockCursor(hWinX);
                    HOperatorSet.DrawRectangle2Mod(hWinX.HalconWindow, VDM.Sgt.rcp.Views[InspectIndex].Wins[WindowIndex].Row
                        , VDM.Sgt.rcp.Views[InspectIndex].Wins[WindowIndex].Col
                        , VDM.Sgt.rcp.Views[InspectIndex].Wins[WindowIndex].Phi
                        , VDM.Sgt.rcp.Views[InspectIndex].Wins[WindowIndex].L1
                         , VDM.Sgt.rcp.Views[InspectIndex].Wins[WindowIndex].L2
                        , out HTuple Row, out HTuple Col, out HTuple Phi, out HTuple L1, out HTuple L2);
                    ATMTVirtualMouse.UnLockCursor();
                    HOperatorSet.GenRectangle2(out HObject Rec2, Row, Col, Phi, L1, L2);
                    HOperatorSet.SmallestRectangle1(Rec2, out Row1, out Col1, out Row2, out Col2);
                    Rec2.Dispose();
                    VDM.Sgt.rcp.Views[InspectIndex].Wins[WindowIndex].Row1 = Row1;
                    VDM.Sgt.rcp.Views[InspectIndex].Wins[WindowIndex].Col1 = Col1;
                    VDM.Sgt.rcp.Views[InspectIndex].Wins[WindowIndex].Row2 = Row2;
                    VDM.Sgt.rcp.Views[InspectIndex].Wins[WindowIndex].Col2 = Col2;
                    VDM.Sgt.rcp.Views[InspectIndex].Wins[WindowIndex].Row = Row;
                    VDM.Sgt.rcp.Views[InspectIndex].Wins[WindowIndex].Col = Col;
                    VDM.Sgt.rcp.Views[InspectIndex].Wins[WindowIndex].Phi = Phi;
                    VDM.Sgt.rcp.Views[InspectIndex].Wins[WindowIndex].L1 = L1;
                    VDM.Sgt.rcp.Views[InspectIndex].Wins[WindowIndex].L2 = L2;
                }
                else
                {
                    HOperatorSet.DrawLineMod(hWinX.HalconWindow
                        , VDM.Sgt.rcp.Views[InspectIndex].Wins[WindowIndex].AddMetrologyObjectLineMeasurePrm.RowBegin
                        , VDM.Sgt.rcp.Views[InspectIndex].Wins[WindowIndex].AddMetrologyObjectLineMeasurePrm.ColumnBegin
                        , VDM.Sgt.rcp.Views[InspectIndex].Wins[WindowIndex].AddMetrologyObjectLineMeasurePrm.RowEnd
                        , VDM.Sgt.rcp.Views[InspectIndex].Wins[WindowIndex].AddMetrologyObjectLineMeasurePrm.ColumnEnd
                        , out HTuple RowBegin, out HTuple ColBegin, out HTuple RowEnd, out HTuple ColEnd);
                    HOperatorSet.GenRegionLine(out HObject Line, RowBegin, ColBegin, RowEnd, ColEnd);
                    HOperatorSet.SmallestRectangle1(Line, out Row1, out Col1, out Row2, out Col2);
                    VDM.Sgt.rcp.Views[InspectIndex].Wins[WindowIndex].AddMetrologyObjectLineMeasurePrm.RowBegin = RowBegin;
                    VDM.Sgt.rcp.Views[InspectIndex].Wins[WindowIndex].AddMetrologyObjectLineMeasurePrm.ColumnBegin = ColBegin;
                    VDM.Sgt.rcp.Views[InspectIndex].Wins[WindowIndex].AddMetrologyObjectLineMeasurePrm.RowEnd = RowEnd;
                    VDM.Sgt.rcp.Views[InspectIndex].Wins[WindowIndex].AddMetrologyObjectLineMeasurePrm.ColumnEnd = ColEnd;
                    VDM.Sgt.rcp.Views[InspectIndex].Wins[WindowIndex].Row1 = Row1;
                    VDM.Sgt.rcp.Views[InspectIndex].Wins[WindowIndex].Col1 = Col1;
                    VDM.Sgt.rcp.Views[InspectIndex].Wins[WindowIndex].Row2 = Row2;
                    VDM.Sgt.rcp.Views[InspectIndex].Wins[WindowIndex].Col2 = Col2;
                }
            }
        }

    }
    public interface IHWindow
    {
        void IHWindow(HWindowControl wc , double r1, double c1, double r2, double c2);
    }
    [Serializable]
    public class TWindow : ICloneable, IDisposable
    {
        public TWindow() { }
        public TWindow(EmInsType _type, double r1, double c1, double r2, double c2)
        {
            InsType = _type;
            Row1 = r1;
            Col1 = c1;
            Row2 = r2;
            Col2 = c2;
        }
        public TWindow(EmMeasureType _type, double r1, double c1, double r2, double c2)
        {
            MeasureType = _type;
            Row1 = r1;
            Col1 = c1;
            Row2 = r2;
            Col2 = c2;
            IsMeasure = true;
        }
        private HWindow hWin
        {
            get
            {
                return hWinX.HalconWindow;
            }
        }
        private HWindowControl hWinX { get; set; }
        public bool IsMeasure { get; set; } = false;
        [XmlIgnore]
        public TWindow SubWin { get; set; } = null;
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
        public EmInsType InsType { get; set; } = EmInsType.NA;
        public EmMeasureType MeasureType { get; set; } = EmMeasureType.MeasurePoint;
        public EmAlignType AlignType { get; set; } = EmAlignType.ShapeModel;
        public EmWindowEditType WindowEditType { get; set; } = EmWindowEditType.Rec;
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
        public bool DeepClone { get; set; } = false;
        public string AlignTo { get; set; } = string.Empty;
        public string GoldenAlignTo { get; set; } = string.Empty;
        public double Row1 { get; set; } = 0;
        public double Row2 { get; set; } = 0;
        public double Col1 { get; set; } = 10;
        public double Col2 { get; set; } = 10;
        /// <summary>
        /// Clear non Rectangle Aligned
        /// </summary>
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
        public int SearchW { get; set; } = 300;
        public int SearchH { get; set; } = 300;
        public string PreImage { get; set; } = PreType.無.ToString();
        public int PreMask { get; set; } = 3;
        public bool IsAlignSuccess = false;
        public bool IsGoldenAlignSuccess = false;
        public int Score { get; set; } = 60;
        //Additional Declare
        public TMeasurePosPrm MeasurePosPrm { get; set; } = new TMeasurePosPrm();
        public TAddMetrologyObjectLineMeasurePrm AddMetrologyObjectLineMeasurePrm { get; set; } = new TAddMetrologyObjectLineMeasurePrm();
        [XmlIgnore]
        public int FindScore { get; set; } = 0;
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
        public void SeriallizeHalconAlign(HTuple AlignHandle)
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
        public void DeSeriallizeHalconAlign()
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
        public void SeriallizeHalconGolenObject(HObject obj)
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
        public void DeSeriallizeHalconObject()
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
        public virtual object Clone()
        {
            DeepClone = true;
            TWindow win = this.MemberwiseClone() as TWindow;
            win.NccAlignModelID = new HTuple();
            win.ShapeAlignModelID = new HTuple();
            return win;
        }
        [XmlIgnore]
        private bool _disposed = false;
        ~TWindow() => Dispose(false);
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
            if (IsValid(_Rec)) _Rec.Dispose();
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
        public virtual List<TWindow> Wins { get; set; } = new List<TWindow>();
    }
    [Serializable]
    public class TRecipeBase
    {
        public TRecipeBase()
        {

        }
        public List<TViewBase> Views = new List<TViewBase>();
        public static TRecipeBase Load(string FileName)
        {
            TRecipeBase rcp = null;
            XmlSerializer mySerializer = new XmlSerializer(typeof(TRecipeBase));
            try
            {
                if (File.Exists(FileName))
                {
                    using (FileStream myFileStream = new FileStream(FileName, FileMode.Open))
                    {
                        rcp = (TRecipeBase)mySerializer.Deserialize(myFileStream);
                    }
                }
                else
                {
                    if (File.Exists(Environment.CurrentDirectory + "\\Recipe\\Default.rcp"))
                    {
                        rcp = Load(Environment.CurrentDirectory + "\\Recipe\\Default.rcp");
                    }
                }
            }
            catch (Exception e)
            {
                LogMgr.SendLog("Recipe", e.Message, e);
                return new TRecipeBase();
            }
            return rcp;
        }
        public virtual void Save()
        {

        }
    }
    public class TSystemBase
    {
        public TSystemBase()
        {

        }
    }
    public class TSystemSDBase
    {
        public TCameraSDBase cameraSD { get; set; } = new TCameraSDBase();
        public string LastRecipeName { get; set; } = string.Empty;
        public string SaveImagePath { get; set; } = Environment.CurrentDirectory + "\\ImageLog";
        public string ReportPath { get; set; } = Environment.CurrentDirectory + "\\Report";
        public static TSystemSDBase Load()
        {
            TSystemSDBase sys;
            XmlSerializer mySerializer = new XmlSerializer(typeof(TSystemSDBase));
            try
            {
                using (FileStream myFileStream = new FileStream(Environment.CurrentDirectory + "\\System.xml", FileMode.Open))
                {
                    sys = (TSystemSDBase)mySerializer.Deserialize(myFileStream);
                }
            }
            catch
            {
                sys = new TSystemSDBase();
            }
            return sys;
        }
        public void Save()
        {
            XmlSerializer ser = new XmlSerializer(typeof(TSystemSDBase));
            using (TextWriter writer = new StreamWriter(Environment.CurrentDirectory + "\\System.xml"))
            {
                ser.Serialize(writer, this);
            }
        }
    }
    /// <summary>
    /// Common Use Halcon Static Function
    /// </summary>
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
            try
            {
                if (IsValid(obj))
                    obj.Dispose();
            }
            catch(Exception e)
            {
                LogMgr.SendLog("Dispose Error! " + e.Message , e);
            }
        }
    }
    public class VDM
    {
        private static VDM _VDM = new VDM();
        public static VDM Sgt
        {
            get
            {
                return _VDM;
            }
        }
        public TRecipeBase rcp = new TRecipeBase();
        public TSystemSDBase systemSD = new TSystemSDBase();
    }
    #region  AllClass
    [Serializable]
    public class TCamInfoBase
    {
        public int Index { get; set; }
        public int CamRealIndex { get; set; }
        public string Description { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public string SerialNumber { get; set; }
        public double ExposureTime { get; set; } = 10000;
        public double Resolution { get; set; } = 1;
        public double Gain { get; set; } = 0;
        public double Gamma { get; set; } = 1;
        public int LineDebounceTime { get; set; } = 0;
        [XmlIgnore]
        public bool IsGrabbing { get; set; } = false;
        [XmlIgnore]
        public bool IsAvailable { get; set; } = false;
        [XmlIgnore]
        public bool IsOpen { get; set; } = false;
    }
    [Serializable]
    public class TCameraSDBase
    {
        public List<TCamInfoBase> CamInfo = new List<TCamInfoBase>();
    }
    #endregion
}
