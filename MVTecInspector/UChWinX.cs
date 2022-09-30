using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SJCommonLib;
using System.IO;
using HalconDotNet;

namespace CommonInspector
{
    public partial class UChWinX : UserControl
    {
        #region Declare
        private TWindow NAWindow = new TWindow();
        private TWindow ClipBoardWinInfo = null;

        public Label lbSelected;
        public GroupBox gbMeaurePos;
        public GroupBox gbMeasureLine;
        public GroupBox gbAlignSetting;
        #endregion

        #region IniDeIni

        public UChWinX()
        {
            InitializeComponent();
        }

        #endregion
#if UnderConstruction
        #region HalconControlEvent
        public HObject ho_Image;
        public HObject ho_Source;
        public int ZoomIndex = 2;
        private double[] ZoomFactor = new double[10] { 0.125, 0.25, 0.5, 1, 2, 4, 8, 16, 32, 64 };
        private bool bMouseDown = false;
        private HTuple HDownX = new HTuple();
        private HTuple HDownY = new HTuple();
        public HTuple PartRow = new HTuple();
        public HTuple PartCol = new HTuple();
        public HTuple PartWidth = new HTuple();
        public HTuple PartHeight = new HTuple();
        private double Resolution = 1;
        public bool IsShowCross = false;
        public bool IsDrawing = false;
        private HTuple ImageWidth = 0;
        private HTuple ImageHeight = 0;
        private int InspectIndex { get; set; } = 0;
        private string CurrntFileName = string.Empty;
        private string RecheckPathName = string.Empty;
        private int ReCheckFileIndex = -1;
        private string[] CheckFiles = new string[0];
        private string[] spUnderLine = new string[] { "_" };
        private bool IsValid(HObject obj)
        {
            if (obj == null) return false;
            if (!obj.IsInitialized())
            {
                return false;
            }
            return true;
        }
        public HWindow hWin
        {
            get
            {
                return hWinX.HalconWindow;
            }
        }
        public void SourceImage()
        {
            if (!IsValid(ho_Source)) return;
            if (IsValid(ho_Image)) ho_Image.Dispose();
            ho_Image = ho_Source.Clone();
        }
        public void SaveDefaultImage(HObject ho_Save, int Index, bool IsUp)
        {
            string FolderName = Path.GetFileNameWithoutExtension(VDM.Sgt.systemSD.LastRecipeName);
            string sPath = Application.StartupPath + "\\Recipe\\" + FolderName + "\\" + FolderName;
            if (!Directory.Exists(sPath))
            {
                Directory.CreateDirectory(sPath);
            }
            if (IsUp)
            {
                string FullFileName = sPath + "\\" + Index.ToString() + "U";
                HOperatorSet.WriteImage(ho_Save, "png fastest", 0, FullFileName);
            }
            else
            {
                string FullFileName = sPath + "\\" + Index.ToString() + "D";
                HOperatorSet.WriteImage(ho_Save, "png fastest", 0, FullFileName);
            }
        }
        public void DisplayWindows()
        {
            if (VDM.Sgt.rcp.Views.Count == 0) return;
            try
            {
                HOperatorSet.QueryFont(hWinX.HalconWindow, out HTuple myFont);
                HOperatorSet.SetFont(hWinX.HalconWindow, myFont[0] + "-14");
                //HOperatorSet.SetSystem("flush_graphic", "false");
                //HOperatorSet.SetSystem("flush_graphic", "true");
                //Show Measure
                if (IsValid(ho_Image))
                    hWin.DispObj(ho_Image);
                if (WindowIndex == -1)
                {
                    if (IsValid(ho_Source))
                        hWin.DispObj(ho_Source);
                }
                HOperatorSet.SetColor(hWinX.HalconWindow, "magenta");
                if (IsValid(CrossStart)) HOperatorSet.DispObj(CrossStart, hWin);
                if (IsValid(CrossEnd)) HOperatorSet.DispObj(CrossEnd, hWin);
                if (IsValid(CrossStart) && IsValid(CrossEnd))
                {
                    HOperatorSet.DispLine(hWinX.HalconWindow, MeasureStartY, MeasureStartX, MeasureEndY, MeasureEndX);
                    HOperatorSet.DistancePp(MeasureStartY, MeasureStartX, MeasureEndY, MeasureEndX, out Distance);
                    HOperatorSet.DispText(hWinX.HalconWindow, string.Format("距離 =  {0:0.000} mm", Tr.To_mm(Distance.D, 0)), "image", MeasureEndY, MeasureEndX, "black", "box", "true");
                }
                HOperatorSet.SetColor(hWinX.HalconWindow, "cyan");
                for (int i = 0; i < VDM.Sgt.rcp.Views[InspectIndex].Wins.Count; i++)
                {
                    VDM.Sgt.rcp.Views[InspectIndex].Wins[i].ClearAligned();
                    if (VDM.Sgt.rcp.Views[InspectIndex].Wins[i].IsMeasure)
                    {
                        if (IsDrawing && i == WindowIndex) continue;
                        if (!InspectMode)
                        {

                            HOperatorSet.SetColor(hWinX.HalconWindow, "yellow");
                            hWin.DispObj(VDM.Sgt.rcp.Views[InspectIndex].Wins[i].Rec2);
                            HOperatorSet.SetColor(hWinX.HalconWindow, "magenta");
                            VDM.Sgt.rcp.Views[InspectIndex].Wins[i].DisplayArrow(hWin);
                            HOperatorSet.SetFont(hWinX.HalconWindow, "Verdana-Normal-14");
                            HOperatorSet.DispText(hWinX.HalconWindow, i.ToString() + "." + VDM.Sgt.rcp.Views[InspectIndex].Wins[i].InsType.ToString() + "==>" + VDM.Sgt.rcp.Views[InspectIndex].Wins[i].AlignTo
                            , "image", VDM.Sgt.rcp.Views[InspectIndex].Wins[i].Row, VDM.Sgt.rcp.Views[InspectIndex].Wins[i].Col, "black", "box", "true");
                            HOperatorSet.SetColor(hWinX.HalconWindow, "cyan");
                            if (i == WindowIndex)
                            {
                                HOperatorSet.SetColor(hWinX.HalconWindow, "red");
                                //hWin.DispObj(VDM.Sgt.rcp.Views[InspectIndex].Wins[i].Rec);
                                hWin.DispObj(VDM.Sgt.rcp.Views[InspectIndex].Wins[i].Rec2);
                                HOperatorSet.SetColor(hWinX.HalconWindow, "cyan");
                                if (VDM.Sgt.IsDetail)
                                {
                                    if (IsValid(VDM.Sgt.rcp.Views[InspectIndex].Wins[i].AddMetrologyObjectLineMeasurePrm.LineObj))
                                        hWin.DispObj(VDM.Sgt.rcp.Views[InspectIndex].Wins[i].AddMetrologyObjectLineMeasurePrm.LineObj);
                                }
                            }
                            if (IsDrawing) continue;
                        }
                        else
                        {
                            if (Tc.IsValid(VDM.Sgt.rcp.Views[InspectIndex].Wins[i].Result))
                            {
                                HOperatorSet.SetColor(hWinX.HalconWindow, "green");
                                hWin.DispObj(VDM.Sgt.rcp.Views[InspectIndex].Wins[i].Result);
                            }
                            if (Tc.IsValid(VDM.Sgt.rcp.Views[InspectIndex].Wins[i].Result2))
                            {
                                HOperatorSet.SetColor(hWinX.HalconWindow, "red");
                                hWin.DispObj(VDM.Sgt.rcp.Views[InspectIndex].Wins[i].Result2);
                            }
                            HOperatorSet.SetColor(hWinX.HalconWindow, "cyan");
                            if (VDM.Sgt.IsDetail)
                            {
                                if (IsValid(VDM.Sgt.rcp.Views[InspectIndex].Wins[i].AddMetrologyObjectLineMeasurePrm.LineObj))
                                    hWin.DispObj(VDM.Sgt.rcp.Views[InspectIndex].Wins[i].AddMetrologyObjectLineMeasurePrm.LineObj);
                            }
                            if (Tc.IsTupleNotEmptry(VDM.Sgt.rcp.Views[InspectIndex].Wins[i].InterDistance))
                            {
                                HOperatorSet.SetFont(hWinX.HalconWindow, myFont[0] + "-10");
                                HOperatorSet.TupleLength(VDM.Sgt.rcp.Views[InspectIndex].Wins[i].InterDistance, out HTuple Length);
                                for (int j = 0; j < Length; j++)
                                {
                                    HOperatorSet.DispText(hWinX.HalconWindow, string.Format("e{0:00} = {1:0.000}  mm", j + 1, VDM.Sgt.rcp.Views[InspectIndex].Wins[i].InterDistance[j].D * VDM.Sgt.systemSD.SingleResolution), "image", PartRow + j * 12 / ZoomFactor[ZoomIndex], PartCol, "black", "box", "true");
                                }
                                HOperatorSet.DispText(hWinX.HalconWindow, string.Format("CoplanarMax = {0:0.000} mm", VDM.Sgt.rcp.Views[cbSides.SelectedIndex].CoplanarMax.D * VDM.Sgt.systemSD.SingleResolution), "image", PartRow + (Length) * 12 / ZoomFactor[ZoomIndex], PartCol, "black", "box", "true");
                                HOperatorSet.DispText(hWinX.HalconWindow, string.Format("CoplanarMin = {0:0.000}  mm", VDM.Sgt.rcp.Views[cbSides.SelectedIndex].CoplanarMin.D * VDM.Sgt.systemSD.SingleResolution), "image", PartRow + (Length + 1) * 12 / ZoomFactor[ZoomIndex], PartCol, "black", "box", "true");
                            }
                        }
                    }
                    else
                    {
                        if (!InspectMode)
                        {
                            if (VDM.Sgt.rcp.Views[InspectIndex].Wins[i].InsType == EmMeasureType.NccModel || VDM.Sgt.rcp.Views[InspectIndex].Wins[i].InsType == EmMeasureType.ShapeModel)
                            {
                                HObject Search;

                                if (IsDrawing) continue;
                                HOperatorSet.DilationRectangle1(VDM.Sgt.rcp.Views[InspectIndex].Wins[i].Rec, out Search, VDM.Sgt.rcp.Views[InspectIndex].Wins[i].SearchW, VDM.Sgt.rcp.Views[InspectIndex].Wins[i].SearchH);
                                HOperatorSet.SetColor(hWinX.HalconWindow, "blue");
                                hWin.DispObj(Search);
                                HOperatorSet.SetColor(hWinX.HalconWindow, "cyan");
                                HOperatorSet.SetFont(hWinX.HalconWindow, "Verdana-Bold-14");
                                hWin.DispObj(VDM.Sgt.rcp.Views[InspectIndex].Wins[i].Rec);
                                HOperatorSet.DispText(hWinX.HalconWindow, i.ToString() + "." + VDM.Sgt.rcp.Views[InspectIndex].Wins[i].InsType + ":" + "(Score = " + (VDM.Sgt.rcp.Views[InspectIndex].Wins[i].FindScore * 100.0).ToString("0.00") + "}"
                                , "image", VDM.Sgt.rcp.Views[InspectIndex].Wins[i].Row1, VDM.Sgt.rcp.Views[InspectIndex].Wins[i].Col1, "black", "box", "true");
                                Search.Dispose();
                                if (VDM.Sgt.rcp.Views[InspectIndex].Wins[i].InsType == EmMeasureType.ShapeModel)
                                {
                                    HOperatorSet.SetColor(hWinX.HalconWindow, "red");
                                    if (IsValid(VDM.Sgt.rcp.Views[InspectIndex].Wins[i].ModelCountour))
                                        hWin.DispObj(VDM.Sgt.rcp.Views[InspectIndex].Wins[i].ModelCountour);
                                    HOperatorSet.SetColor(hWinX.HalconWindow, "cyan");
                                }
                            }
                            else
                            {
                                if (IsDrawing) continue;
                                hWin.DispObj(VDM.Sgt.rcp.Views[InspectIndex].Wins[i].Rec);
                                HOperatorSet.SetFont(hWinX.HalconWindow, "Verdana-Normal-14");
                                HOperatorSet.DispText(hWinX.HalconWindow, i.ToString() + "." + VDM.Sgt.rcp.Views[InspectIndex].Wins[i].InsType.ToString() + "==>" + VDM.Sgt.rcp.Views[InspectIndex].Wins[i].AlignTo
                                , "image", VDM.Sgt.rcp.Views[InspectIndex].Wins[i].Row1, VDM.Sgt.rcp.Views[InspectIndex].Wins[i].Col1, "black", "box", "true");
                            }
                            if (i == WindowIndex)
                            {
                                HOperatorSet.SetColor(hWinX.HalconWindow, "red");
                                hWin.DispObj(VDM.Sgt.rcp.Views[InspectIndex].Wins[i].Rec);
                                HOperatorSet.SetColor(hWinX.HalconWindow, "cyan");
                            }

                        }
                        else
                        {

                        }
                    }
                }
            }
            catch (Exception e)
            {
                LogMgr.SendLog(e.Message, e);
            }
        }
        public void IniWinX()
        {
            if (IsValid(ho_Image)) ho_Image.Dispose();
            HOperatorSet.GenImageConst(out HObject img, "byte", 2048, 2592);
            HOperatorSet.PaintRegion(img, img, out ho_Image, 64, "fill");
            img.Dispose();
            PartWidth = hWinX.Width / ZoomFactor[ZoomIndex];
            PartHeight = hWinX.Height / ZoomFactor[ZoomIndex];
            PartRow = 0;// / 2;
            PartCol = 0;// / 2;
            HOperatorSet.SetPart(hWinX.HalconWindow, PartRow, PartCol, PartRow + PartHeight, PartCol + PartWidth);
            HOperatorSet.SetSystem("clip_region", "false");
            HOperatorSet.SetSystem("use_window_thread", "true");
            HOperatorSet.SetDraw(hWinX.HalconWindow, "margin");
            HOperatorSet.SetLineWidth(hWinX.HalconWindow, 2);
            DisplayWindows();
        }
        private void hWinX_HMouseDown(object sender, HalconDotNet.HMouseEventArgs e)
        {
            bMouseDown = true;
            HDownX = e.X;
            HDownY = e.Y;
            DistanceMoved = 0;
        }
        private double DistanceMoved = 0;
        private void hWinX_HMouseMove(object sender, HalconDotNet.HMouseEventArgs e)
        {

            try
            {
                HTuple GrayValue;
                HOperatorSet.GetGrayval(ho_Image, e.Y, e.X, out GrayValue);
                lbGray.Text = "灰階 = " + GrayValue.TupleInt().ToString();
                lbX.Text = string.Format("X = {0:0.0}", e.X);
                lbY.Text = string.Format("Y = {0:0.0}", e.Y);
                if (bMouseDown && e.Button == MouseButtons.Right)
                {
                    double dY = (e.Y - HDownY);// / ZoomFactor[ZoomIndex];
                    double dX = (e.X - HDownX);// / ZoomFactor[ZoomIndex];
                    DistanceMoved += dY;
                    DistanceMoved += dX;
                    //lbDownXY.Text = DownX.ToString() + " , " + DownY.ToString();
                    PartWidth = hWinX.Width / ZoomFactor[ZoomIndex];
                    PartHeight = hWinX.Height / ZoomFactor[ZoomIndex];
                    PartRow = PartRow - dY;
                    PartCol = PartCol - dX;
                    HOperatorSet.SetPart(hWinX.HalconWindow, PartRow, PartCol, PartRow + PartHeight, PartCol + PartWidth);
                    HOperatorSet.SetSystem("flush_graphic", "false");
                    hWinX.HalconWindow.ClearWindow();
                    HOperatorSet.SetSystem("flush_graphic", "true");
                    HOperatorSet.DispObj(ho_Image, hWinX.HalconWindow);
                }
                DisplayWindows();
                if (InspectMode)
                {
                    //OnResultEnter(e);
                }
                if (IsShowCross)
                    DispCross();
            }
            catch
            {

            }
        }
        private void hWinX_HMouseUp(object sender, HalconDotNet.HMouseEventArgs e)
        {

            MX = e.X;
            MY = e.Y;
            if (bIsDrawing) return;
            bMouseDown = false;
            bMouseDown = false;
            if (e.Button == MouseButtons.Left)
            {
                if (InspectMode) return;
                SelectWindow(e);
            }
            if (e.Button == MouseButtons.Right)
            {
                if (InspectMode)
                    EnableTsmControl(false);
                else
                    EnableTsmControl(true);
                if (DistanceMoved == 0)
                    cmsEditWondow.Show(new Point((int)HX + this.Left + tcMain.Left + tpMain.Left + hWinX.Left, (int)HY + this.Top + tcMain.Top + tpMain.Top + hWinX.Top));
            }
        }
        private void EnableTsmControl(bool OnOff)
        {
            tsmCopy.Enabled = OnOff;
            tsmPaste.Enabled = OnOff;
            tsmEdit.Enabled = OnOff;
            tsmDelete.Enabled = OnOff;
            tsmAdd.Enabled = OnOff;
        }
        private int WindowIndex = -1;
        private List<int> LastIndex = new List<int>();
        private int XIndex = 0;
        private bool IsWindowExist = false;
        private void SelectWindow(HMouseEventArgs e)
        {
            if (VDM.Sgt.rcp.Views.Count == 0) return;
            gbMeaurePos.Enabled = false;
            gbMeasureLine.Enabled = false;
            gbAlignSetting.Enabled = false;
            IsWindowExist = false;
            LastIndex.Clear();
            double X = 0;
            double Y = 0;
            lbSelected.ForeColor = Color.Yellow;
            lbSelected.Text = "ウィンドウを選択してください";
            if (e != null)
            {
                X = e.X;
                Y = e.Y;
            }
            else
            {
                X = -9999;
                Y = -9999;
            }
            for (int i = 0; i < VDM.Sgt.rcp.Views[InspectIndex].Wins.Count; i++)
            {
                if (Y < VDM.Sgt.rcp.Views[InspectIndex].Wins[i].Row2 + 1
                    && Y > VDM.Sgt.rcp.Views[InspectIndex].Wins[i].Row1 - 1
                    && X < VDM.Sgt.rcp.Views[InspectIndex].Wins[i].Col2 + 1
                    && X > VDM.Sgt.rcp.Views[InspectIndex].Wins[i].Col1 - 1)
                {
                    LastIndex.Add(i);
                    IsWindowExist = true;
                }
            }
            //cb Control===============================
            cbAlignTo.Items.Clear();
            cbAlignTo.Text = string.Empty;
            for (int i = 0; i < VDM.Sgt.rcp.Views[InspectIndex].Wins.Count; i++)
            {
                if (VDM.Sgt.rcp.Views[InspectIndex].Wins[i].InsType == EmMeasureType.NccModel || VDM.Sgt.rcp.Views[InspectIndex].Wins[i].InsType == EmMeasureType.ShapeModel)
                {
                    cbAlignTo.Items.Add(string.Format("{0:00}", i) + "." + VDM.Sgt.rcp.Views[InspectIndex].Wins[i].InsType);
                }
            }
            cbAlignTo.Items.Add(string.Format("{0:00}", -1) + "." + NA);

            //======================================
            if (LastIndex.Count == 1)
                XIndex = 0;
            if (!IsWindowExist)
            {
                IsWindowExist = false;
                WindowIndex = -1;
                bsWindowInfo.DataSource = NAWindow;
                bsWindowInfo.ResetBindings(false);
                EnableAlignControl(false);
                XIndex = 0;
                return;
            }
            else
            {
                if (XIndex < LastIndex.Count)
                {
                    WindowIndex = LastIndex[XIndex];
                    OnWindowSelected(WindowIndex);
                    //if (cbAlignTo.Items.Count == 0)
                    //    cbAlignTo.Text = "-1.無指定";
                    if (XIndex >= LastIndex.Count - 1)
                    {
                        XIndex = 0;
                    }
                    else
                    {
                        XIndex++;
                    }
                }
                else
                {
                    XIndex = 0;
                }
            }
            DisplayWindows();
        }
        private void OnWindowSelected(int Index)
        {
            bsWindowInfo.DataSource = VDM.Sgt.rcp.Views[InspectIndex].Wins[Index];
            bsWindowInfo.ResetBindings(false);
            if (VDM.Sgt.rcp.Views[InspectIndex].Wins[Index].InsType == EmMeasureType.MeasurePoint || VDM.Sgt.rcp.Views[InspectIndex].Wins[Index].InsType == EmMeasureType.MeasurePair)
            {
                gbMeaurePos.Enabled = true;
                bsMeasurePosPrm.DataSource = VDM.Sgt.rcp.Views[InspectIndex].Wins[Index].MeasurePosPrm;
                bsMeasurePosPrm.ResetBindings(false);

                cbTransition.SelectedIndex = (int)VDM.Sgt.rcp.Views[InspectIndex].Wins[Index].MeasurePosPrm.Transition;
                cbSelect.SelectedIndex = (int)VDM.Sgt.rcp.Views[InspectIndex].Wins[Index].MeasurePosPrm.Select;
            }
            if (VDM.Sgt.rcp.Views[InspectIndex].Wins[Index].InsType == EmMeasureType.MeasureLine)
            {
                gbMeasureLine.Enabled = true;
                bsLineMeasure.DataSource = VDM.Sgt.rcp.Views[InspectIndex].Wins[Index].AddMetrologyObjectLineMeasurePrm;
                bsLineMeasure.ResetBindings(false);
            }
            //HOperatorSet.DispText(hWinX.HalconWindow, WindowIndex.ToString() + "." + VDM.Sgt.rcp.AllDicerns[InspectIndex][WindowIndex].EmInsType + "已選擇", "image", PartRow, PartCol, "black", "box", "true");
            lbSelected.ForeColor = Color.Lime;
            lbSelected.Text = Index.ToString() + "." + VDM.Sgt.rcp.Views[InspectIndex].Wins[Index].InsType + "選ばれた";
            if (VDM.Sgt.rcp.Views[InspectIndex].Wins[Index].InsType == EmMeasureType.ShapeModel || VDM.Sgt.rcp.Views[InspectIndex].Wins[Index].InsType == EmMeasureType.NccModel)
            {
                //OnPreProcess(true);
                EnableAlignControl(true);
            }
            cbAlignTo.Text = VDM.Sgt.rcp.Views[InspectIndex].Wins[Index].AlignTo;
        }

        private void hWinX_HMouseWheel(object sender, HalconDotNet.HMouseEventArgs e)
        {
            HTuple XRatio, YRatio;
            try
            {
                XRatio = (e.X - PartCol) / PartWidth;
                YRatio = (e.Y - PartRow) / PartHeight;
                if (e.Delta > 0)
                {
                    ZoomIndex = ZoomIndex + 1;
                    if (ZoomIndex > 9)
                    {
                        ZoomIndex = 9;
                    }
                    PartWidth = hWinX.Width / ZoomFactor[ZoomIndex];
                    PartHeight = hWinX.Height / ZoomFactor[ZoomIndex];
                    PartRow = e.Y - YRatio * PartHeight;
                    PartCol = e.X - XRatio * PartWidth;
                    HOperatorSet.SetPart(hWinX.HalconWindow, PartRow, PartCol, PartRow + PartHeight, PartCol + PartWidth);
                    hWinX.HalconWindow.ClearWindow();
                    if (IsValid(ho_Image))
                        HOperatorSet.DispObj(ho_Image, hWinX.HalconWindow);
                    DisplayWindows();
                }
                else
                {
                    ZoomIndex = ZoomIndex - 1;
                    if (ZoomIndex < 0)
                    {
                        ZoomIndex = 0;
                        return;
                    }
                    PartWidth = hWinX.Width / ZoomFactor[ZoomIndex];
                    PartHeight = hWinX.Height / ZoomFactor[ZoomIndex];
                    PartRow = e.Y - YRatio * PartHeight;// / 2;
                    PartCol = e.X - XRatio * PartWidth;// / 2;
                    HOperatorSet.SetPart(hWinX.HalconWindow, PartRow, PartCol, PartRow + PartHeight, PartCol + PartWidth);
                    hWinX.HalconWindow.ClearWindow();
                    if (IsValid(ho_Image))
                        HOperatorSet.DispObj(ho_Image, hWinX.HalconWindow);
                    DisplayWindows();
                }
                if (IsShowCross)
                    DispCross();
            }
            catch
            {

            }
        }
        private void DispImage(HObject img)
        {
            HOperatorSet.DispObj(ho_Image, hWinX.HalconWindow);
        }
        private void EnableAlignControl(bool OnOff)
        {
            gbAlignSetting.Enabled = OnOff;
        }

        private HObject Cross;
        private void DispCross()
        {
            if (IsValid(Cross)) Cross.Dispose();
            hWinX.HalconWindow.SetColor("red");
            HOperatorSet.GetImageSize(ho_Image, out ImageWidth, out ImageHeight);
            HOperatorSet.GenCrossContourXld(out Cross, ImageHeight / 2, ImageWidth / 2, 9999, 0);
            hWinX.HalconWindow.DispObj(Cross);
        }
        #endregion

        #region CmsEditWondow
         private void InitalCsm()
        {
            tsmAdd.DropDownItems.Clear();
            tsmAdd.DropDownItems.Add(EmMeasureType.NccModel.ToString());
            tsmAdd.DropDownItems.Add(EmMeasureType.ShapeModel.ToString());
            tsmAdd.DropDownItems.Add(EmMeasureType.MeasureLine.ToString());
            tsmAdd.DropDownItems.Add(EmMeasureType.MeasurePoint.ToString());
            tsmAdd.DropDownItems.Add(EmMeasureType.MeasurePair.ToString());
            tsmAdd.DropDownItems.Add(EmMeasureType.NoAlignArea.ToString());
            tsmAdd.DropDownItems.Add(EmMeasureType.NoInspArea.ToString());
            for (int i = 0; i < tsmAdd.DropDownItems.Count; i++)
            {
                tsmAdd.DropDownItems[i].Click += tsmAddAny_Click;
            }
        }
        private void tsmSave_Click(object sender, EventArgs e)
        {
            saveFileDialog1.Filter = "BMP File (*.bmp) |*.bmp |Fastest png (*.png)|*.png";
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                string cc = Path.GetExtension(saveFileDialog1.FileName);
                if (Path.GetExtension(saveFileDialog1.FileName) == ".bmp")
                    HOperatorSet.WriteImage(ho_Image, "bmp", 0, saveFileDialog1.FileName);
                else
                    HOperatorSet.WriteImage(ho_Image, "png fastest", 0, saveFileDialog1.FileName);
            }
        }
        private void tsmPaste_Click(object sender, EventArgs e)
        {
            if (ClipBoardWinInfo != null)
            {
                double Dx = HDownX - ClipBoardWinInfo.Col1;
                double Dy = HDownY - ClipBoardWinInfo.Row1;
                ClipBoardWinInfo.Row1 += Dy;
                ClipBoardWinInfo.Row2 += Dy;
                ClipBoardWinInfo.Col1 += Dx;
                ClipBoardWinInfo.Col2 += Dx;
                ClipBoardWinInfo.AlignInfo.Row += Dy;
                ClipBoardWinInfo.AlignInfo.Col += Dx;
                if (IsValid(ClipBoardWinInfo.GolenObj))
                {
                    HOperatorSet.MoveRegion(ClipBoardWinInfo.GolenObj, out HObject Moved, Dy, Dx);
                    ClipBoardWinInfo.GolenObj.Dispose();
                    ClipBoardWinInfo.GolenObj = Moved.Clone();
                    Moved.Dispose();

                }
                //VDM.Sgt.rcp.Views[InspectIndex].Add(ClipBoardWinInfo.Clone() as TWindowInfo);
                //VDM.Sgt.rcp.Views[InspectIndex][VDM.Sgt.rcp.Views[InspectIndex].Count - 1].SaveHalconGolenObject(VDM.Sgt.rcp.Views[InspectIndex][VDM.Sgt.rcp.Views[InspectIndex].Count - 1].GolenObj);
                //ClipBoardWinInfo.DeepClone = false;
                //for (int i = 0; i < VDM.Sgt.rcp.Views[InspectIndex].Count; i++)
                //{
                //    VDM.Sgt.rcp.Views[InspectIndex][i].DeepClone = false;
                //}
                DisplayWindows();
            }
        }
        private void tsmDelete_Click(object sender, EventArgs e)
        {
            btnDelete_Click(sender, e);
        }
        private void btnEdit_Click(object sender, EventArgs e)
        {
            hWinX.Focus();
            if (WindowIndex > -1)
            {
                HTuple Row1, Row2, Col1, Col2;
                bIsDrawing = true;
                DisplayWindows();

                if (VDM.Sgt.rcp.Views[InspectIndex].Wins[WindowIndex].WindowEditType == EmWindowEditType.Rec)
                {
                    HOperatorSet.SetColor(hWinX.HalconWindow, "red");
                    HOperatorSet.SetFont(hWinX.HalconWindow, "Verdana-Normal-14");
                    HOperatorSet.DispText(hWinX.HalconWindow, "滑鼠左鍵編輯，按右鍵結束編輯", "image", PartRow, PartCol, "black", "box", "true");
                    hWinX.Focus();
                    bool ret = SJVirtualMouse.LockCursor(hWinX);
                    HOperatorSet.DrawRectangle1Mod(hWinX.HalconWindow, VDM.Sgt.rcp.Views[InspectIndex].Wins[WindowIndex].Row1
                        , VDM.Sgt.rcp.Views[InspectIndex].Wins[WindowIndex].Col1
                        , VDM.Sgt.rcp.Views[InspectIndex].Wins[WindowIndex].Row2
                        , VDM.Sgt.rcp.Views[InspectIndex].Wins[WindowIndex].Col2, out Row1, out Col1, out Row2, out Col2);
                    SJVirtualMouse.UnLockCursor();
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
                    bool ret = SJVirtualMouse.LockCursor(hWinX);
                    HOperatorSet.DrawRectangle2Mod(hWinX.HalconWindow, VDM.Sgt.rcp.Views[InspectIndex].Wins[WindowIndex].Row
                        , VDM.Sgt.rcp.Views[InspectIndex].Wins[WindowIndex].Col
                        , VDM.Sgt.rcp.Views[InspectIndex].Wins[WindowIndex].Phi
                        , VDM.Sgt.rcp.Views[InspectIndex].Wins[WindowIndex].L1
                         , VDM.Sgt.rcp.Views[InspectIndex].Wins[WindowIndex].L2
                        , out HTuple Row, out HTuple Col, out HTuple Phi, out HTuple L1, out HTuple L2);
                    SJVirtualMouse.UnLockCursor();
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
                bIsDrawing = false;
                DisplayWindows();
            }
        }

        private void chkCross_OnChange(object sender, EventArgs e)
        {
            if (chkCross.Checked)
            {
                IsShowCross = true;
                DispCross();
            }
            else
            {
                IsShowCross = false;
                hWinX.ClearWindow();
                DisplayWindows();
            }

        }

        private void tsmAddAny_Click(object sender, EventArgs e)
        {
            EmMeasureType ins = EmMeasureType.NoInspArea;
            switch (((ToolStripItem)sender).Text)
            {
                // , , , , 
                case "NccModel":
                    ins = EmMeasureType.NccModel;
                    break;
                case "ShapeModel":
                    ins = EmMeasureType.ShapeModel;
                    break;
                case "MeasureLine":
                    ins = EmMeasureType.MeasureLine;
                    break;
                case "MeasurePoint":
                    ins = EmMeasureType.MeasurePoint;
                    break;
                case "MeasurePair":
                    ins = EmMeasureType.MeasurePair;
                    break;
                case "NoAlignArea":
                    ins = EmMeasureType.NoAlignArea;
                    break;
                case "NoInspArea":
                    ins = EmMeasureType.NoInspArea;
                    break;

            }
            hWinX.Focus();
            HTuple Row1, Row2, Col1, Col2;
            bIsDrawing = true;
            HOperatorSet.SetColor(hWinX.HalconWindow, "red");
            HOperatorSet.SetFont(hWinX.HalconWindow, "Verdana-Normal-14");
            HOperatorSet.DispText(hWinX.HalconWindow, "滑鼠左鍵編輯，按右鍵結束編輯", "image", PartRow, PartCol, "black", "box", "true");
            PartWidth = hWinX.Width / ZoomFactor[ZoomIndex];
            PartHeight = hWinX.Height / ZoomFactor[ZoomIndex];
            hWinX.Focus();
            if (ins == EmMeasureType.MeasureLine | ins == EmMeasureType.MeasurePoint | ins == EmMeasureType.MeasurePair)
            {
                bool ret = SJVirtualMouse.LockCursor(hWinX);
                HOperatorSet.DrawLine(hWinX, out Row1, out Col1, out Row2, out Col2);
                SJVirtualMouse.UnLockCursor();
                TWindowInfo NewWindow = new TWindowInfo(ins, Row1, Col1, Row2, Col2, true);
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
                NewWindow.InsType = ins;
                VDM.Sgt.rcp.Views[InspectIndex].Wins.Add(NewWindow);
                Rect.Dispose();
            }
            else
            {
                bool ret = SJVirtualMouse.LockCursor(hWinX);
                HOperatorSet.DrawRectangle1Mod(hWinX.HalconWindow, MY, MX, MY + 100 / ZoomFactor[ZoomIndex], MX + 100 / ZoomFactor[ZoomIndex], out Row1, out Col1, out Row2, out Col2);
                SJVirtualMouse.UnLockCursor();
                HOperatorSet.GenRectangle1(out HObject Rec, Row1, Col1, Row2, Col2);
                HOperatorSet.SmallestRectangle1(Rec, out Row1, out Col1, out Row2, out Col2);
                TWindowInfo w = new TWindowInfo(ins, Row1, Col1, Row2, Col2);
                if (ins == EmMeasureType.NccModel)
                    w.AlignType = EmAlignType.NccModel;
                else
                    w.AlignType = EmAlignType.ShapeModel;
                w.InsType = ins;
                VDM.Sgt.rcp.Views[InspectIndex].Wins.Add(w);
                Rec.Dispose();
            }
            bIsDrawing = false;
            DisplayWindows();
        }
        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (WindowIndex > -1)
                {
                    if (DialogResult.Yes == MessageBox.Show("確定刪除?", "問題", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
                    {
                        if (VDM.Sgt.rcp.Views[InspectIndex].Wins[WindowIndex].InsType == EmMeasureType.NccModel || VDM.Sgt.rcp.Views[InspectIndex].Wins[WindowIndex].InsType == EmMeasureType.ShapeModel)
                        {
                            MessageBox.Show("刪除Alignment框需要重設其他種類框的對應Alignment框");
                            for (int i = 0; i < VDM.Sgt.rcp.Views[InspectIndex].Wins.Count; i++)
                            {
                                try
                                {
                                    if (VDM.Sgt.rcp.Views[InspectIndex].Wins[i].InsType != EmMeasureType.NccModel && VDM.Sgt.rcp.Views[InspectIndex].Wins[i].InsType != EmMeasureType.ShapeModel)
                                    {
                                        if (int.Parse(VDM.Sgt.rcp.Views[InspectIndex].Wins[i].AlignTo.Substring(0, 2)) == WindowIndex)
                                        {
                                            VDM.Sgt.rcp.Views[InspectIndex].Wins[i].AlignTo = "";
                                            VDM.Sgt.rcp.Views[InspectIndex].Wins[i].GoldenAlignTo = "";
                                        }
                                    }
                                }
                                catch (Exception ex)
                                {
                                    LogMgr.SendLog(ex.Message, ex);
                                }
                            }
                            VDM.Sgt.rcp.Views[InspectIndex].Wins[WindowIndex].Dispose();
                        }
                        else
                        {
                            VDM.Sgt.rcp.Views[InspectIndex].Wins[WindowIndex].Dispose();
                        }
                        VDM.Sgt.rcp.Views[InspectIndex].Wins.RemoveAt(WindowIndex);
                        WindowIndex = -1;
                        bsWindowInfo.DataSource = NAWindow;
                        bsWindowInfo.ResetBindings(false);
                    }
                }
                DisplayWindows();
            }
            catch (Exception ex)
            {
                LogMgr.SendLog(ex.Message, ex);
            }
        }
        private double MeasureStartX = 0, MeasureStartY = 0, MeasureEndX = 0, MeasureEndY = 0;

        private void btnSaveRecipe_Click(object sender, EventArgs e)
        {
            VDM.Sgt.rcp.Save();
            VDM.Sgt.systemSD.Save();
        }
        private HObject CrossStart, CrossEnd;
        private void tsmMeasureStart_Click(object sender, EventArgs e)
        {
            MeasureStartX = MX;
            MeasureStartY = MY;
            if (IsValid(CrossStart)) CrossStart.Dispose();
            HOperatorSet.GenCrossContourXld(out CrossStart, MY, MX, 20 / ZoomFactor[ZoomIndex], 3.1415926 / 4);
            DisplayWindows();
        }
        private HTuple Distance = 0;
        private void tsmMeasureEnd_Click(object sender, EventArgs e)
        {
            MeasureEndX = MX;
            MeasureEndY = MY;
            if (IsValid(CrossEnd)) CrossEnd.Dispose();
            HOperatorSet.GenCrossContourXld(out CrossEnd, MY, MX, 20 / ZoomFactor[ZoomIndex], 3.1415926 / 4);
        }
        private void tsmStopMeasure_Click(object sender, EventArgs e)
        {
            if (IsValid(CrossStart)) CrossStart.Dispose();
            if (IsValid(CrossEnd)) CrossEnd.Dispose();
            DisplayWindows();
        }
        #endregion
#endif
    }
}
