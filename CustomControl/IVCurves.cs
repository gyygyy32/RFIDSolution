using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace CustomControl
{
    public partial class IVCurves : Control
    {
        private static int CurvesCurrent = 50;
        private static int CurvesVoltage = 10;
        private static int _CurvesPaneWidth = 600;
        private static int _CurvesPaneHeight = 300;
        private static int WidthPer5A = 120;
        private static int HeightPerV = 30;
        private Rectangle _CvsRect;
        private readonly string _FontName = "Tahoma";

        private delegate void DelegateDynamicShow(Graphics gragh);

        private DataTable _info = new DataTable();

        private int MaxPmax = int.MinValue;
        private int MinPmax = int.MaxValue;
        private int ShowPoints = 50;
        
        private List<Point> _OriPointList = null;
        private List<Point> _CurvasPointList = null;
        // add by genhong.hu On 2016-06-02 增加功率曲线
        private List<Point> _PowerPointList = null;

        /// <summary>
        /// 用于判断区分HR的标签
        /// </summary>
        private I_V_Point[] OldoriPointArray;

        public IVCurves()
        {
            InitializeComponent();

            _CvsRect = new Rectangle(40, 40, _CurvesPaneWidth, _CurvesPaneHeight);
        }

        public IVCurves(int w, int h)
        {

            _CvsRect = new Rectangle(40, 40, w, h);
            HeightPerV = h / 10;
            WidthPer5A = w / 5;
        }

        protected override void OnPaint(PaintEventArgs pe)
        {
            base.OnPaint(pe);


            _CvsRect = new Rectangle(40, 40, this.Width - 100, this.Height - 100);
            HeightPerV = (this.Height - 100) / 10;
            WidthPer5A = (this.Width - 100) / 5;




            Bitmap canvas = new Bitmap(this.Width, this.Height);
            Graphics gragh = Graphics.FromImage((System.Drawing.Image)canvas);
            gragh.FillRectangle(new SolidBrush(Color.White), this.ClientRectangle);

            //画坐标、背景            
            DrawGrid(gragh);


            //画曲线
            if (_PowerPointList != null)
            {
                // DrawCurvas(gragh);
                InvokeDynamicShow(gragh);
            }
            else
            {

            }
            //画LotID
            if (!string.IsNullOrEmpty(strPWord))
            {

                InvokeWriteWordShow(gragh);
            }
            pe.Graphics.DrawImage((System.Drawing.Image)canvas, 0, 0);
            canvas.Dispose();
            gragh.Dispose();
        }

        public void SetPoints(DataTable dt)
        {
            _info = dt;
            MaxPmax = int.MinValue;
            MinPmax = int.MaxValue;
            for (int i = 0; i < _info.Rows.Count; i++)
            {
                MaxPmax = Math.Max(MaxPmax, (int)Math.Ceiling((double.Parse(_info.Rows[i]["Pmax"].ToString()) / 5)) * 5 + 3);
                MinPmax = Math.Min(MinPmax, (int)Math.Floor((double.Parse(_info.Rows[i]["Pmax"].ToString()) / 5)) * 5 - 3);
            }
            _PowerPointList = new List<Point>();
            for (int i = 0; i < _info.Rows.Count; i++)
            {
                _PowerPointList.Add(GetPowerPointByOriginal(i));
            }
            this.Invalidate();
        }


        
        /// <summary>
        /// 设置真实的数据点，并根据需要计算插值
        /// </summary>
        /// <param name="oriPointArray">真实的数据点</param>
        /// <param name="evalAll">是否需要计算所有数据点</param>
        public void SetOriginalPoints(I_V_Point[] oriPointArray, bool evalAll,string LotID="")
        {
            strPWord = LotID;
            OldoriPointArray = oriPointArray;

            //#region 2013-06-03 曲线异常Bug修改,如果倒数第二个点靠近最后一个点，则移除
            I_V_Point[] pointArray = null;
            //if (oriPointArray != null)
            //{
            //    int oriLen = oriPointArray.Length;
            //    if (oriPointArray[oriLen - 2].Current < 0.5)
            //    {
            //        pointArray = new I_V_Point[oriLen - 1];
            //        Array.Copy(oriPointArray, pointArray, oriLen - 1);
            //        pointArray[oriLen - 2].Current = oriPointArray[oriLen - 1].Current;
            //        pointArray[oriLen - 2].Voltage = oriPointArray[oriLen - 1].Voltage;
            //    }
            //    else
            //    {
            //        pointArray = oriPointArray;
            //    }
            //}
            //#endregion

            _OriPointList = new List<Point>();

            _CurvasPointList = new List<Point>();
            _PowerPointList = new List<Point>();// add by genhong.hu On 2016-06-02 增加功率曲线
            if (oriPointArray == null)
            {
                CleanSpine();
                return;
            }
            if (oriPointArray.Length != 3)
            {
                //MessageBox.Show("RFID标签数据异常，无法显示曲线。");
                //return;
                if (oriPointArray != null)
                {
                    int oriLen = oriPointArray.Length;
                    if (oriPointArray[oriLen - 2].Current < 0.5)
                    {
                        pointArray = new I_V_Point[oriLen - 1];
                        Array.Copy(oriPointArray, pointArray, oriLen - 1);
                        pointArray[oriLen - 2].Current = oriPointArray[oriLen - 1].Current;
                        pointArray[oriLen - 2].Voltage = oriPointArray[oriLen - 1].Voltage;
                    }
                    else
                    {
                        pointArray = oriPointArray;
                    }
                }


            }
            else
            {

                #region ======================================================
                int PointCount = 100;
                double Voc = oriPointArray[2].Voltage;
                double Isc = oriPointArray[0].Current;
                double Vpm = oriPointArray[1].Voltage;
                double Ipm = oriPointArray[1].Current;
                pointArray = new I_V_Point[PointCount + 3];
                I_V_Point s1 = new I_V_Point();
                s1.Voltage = 0;
                s1.Current = Isc;
                pointArray[0] = s1;


                for (int i = 1; i < PointCount + 1; i++)
                {
                    I_V_Point s2 = new I_V_Point();
                    s2.Voltage = 0 + (Vpm - 0) / (PointCount + 1) * i;
                    s2.Current = Isc - (Isc - Ipm) / (PointCount + 1) * i;
                    pointArray[i] = s2;
                }



                I_V_Point s4 = new I_V_Point();
                s4.Voltage = Vpm;
                s4.Current = Ipm;
                I_V_Point s5 = new I_V_Point();
                s5.Voltage = Voc;
                s5.Current = 0;

                pointArray[PointCount + 1] = s4;
                pointArray[PointCount + 2] = s5;
                #endregion
            }
            foreach (I_V_Point oriPoint in pointArray)
            {
                _OriPointList.Add(GetPointByOriginal(oriPoint));
            }
            if (evalAll)
            {
                I_V_Point[] points = MySpline.Spline(pointArray);
                if (points == null)
                {
                    MessageBox.Show("RFID标签数据异常，无法显示曲线。");
                    return;
                }
                foreach (I_V_Point oriPoint in points)
                {
                    _CurvasPointList.Add(GetPointByOriginal(oriPoint));

                    // add by genhong.hu On 2016-06-02 增加功率曲线
                    P_V_Point pmaxpoint = new P_V_Point();
                    pmaxpoint.Pmax = oriPoint.Current * oriPoint.Voltage;
                    pmaxpoint.Voltage = oriPoint.Voltage;
                    _PowerPointList.Add(GetPowerPointByOriginal(pmaxpoint));

                }
                #region  不计算
                //  _CurvasPointList = _OriPointList;
                #endregion

                //2014-03-30 曲线画到电流为0
                Point lastPointToZero = new Point();
                lastPointToZero.X = _CurvasPointList[_CurvasPointList.Count - 1].X;
                lastPointToZero.Y = _CvsRect.Top + _CvsRect.Height;
                _CurvasPointList.Add(lastPointToZero);

                _PowerPointList.Add(lastPointToZero);// add by genhong.hu On 2016-06-02 增加功率曲线
            }
            this.Invalidate();
        }
        /// <summary>
        /// 清空曲线
        /// </summary>
        private void CleanSpine()
        {
            _OriPointList = null;
            _CurvasPointList = null;
            _PowerPointList = null;

            _CvsRect = new Rectangle(40, 40, this.Width - 100, this.Height - 100);
            HeightPerV = (this.Height - 100) / 10;
            WidthPer5A = (this.Width - 100) / 5;

            Bitmap canvas = new Bitmap(this.Width, this.Height);
            Graphics gragh = Graphics.FromImage((System.Drawing.Image)canvas);
            gragh.FillRectangle(new SolidBrush(Color.White), this.ClientRectangle);

            //画坐标、背景            
            DrawGrid(gragh);
            this.Invalidate();
        }
        private void OnCurvasPanePaint(object sender, PaintEventArgs e)
        {


            _CvsRect = new Rectangle(40, 40, this.Width - 100, this.Height - 100);
            HeightPerV = (this.Height - 100) / 10;
            WidthPer5A = (this.Width - 100) / 5;




            Bitmap canvas = new Bitmap(this.Width, this.Height);
            Graphics gragh = Graphics.FromImage((System.Drawing.Image)canvas);
            gragh.FillRectangle(new SolidBrush(Color.White), this.ClientRectangle);

            //画坐标、背景            
            DrawGrid(gragh);


            //画曲线
            if (_PowerPointList != null)
            {
                // DrawCurvas(gragh);
                InvokeDynamicShow(gragh);
            }
            //画LotID
            if (!string.IsNullOrEmpty (strPWord))
            {
                 
                InvokeWriteWordShow(gragh);
            }
            e.Graphics.DrawImage((System.Drawing.Image)canvas, 0, 0);
            canvas.Dispose();
            gragh.Dispose();
        }

        //获取真实坐标对应的画布坐标
        private Point GetPointByOriginal(I_V_Point oriPoint)
        {
            double X = _CvsRect.X + oriPoint.Voltage * _CvsRect.Width / CurvesCurrent;
            double Y = _CvsRect.Y + (CurvesVoltage - oriPoint.Current) * _CvsRect.Height / CurvesVoltage;
            Point point = new Point();
            point.X = (int)X;
            point.Y = (int)Y;
            return point;
        }
        private Point GetPowerPointByOriginal(P_V_Point oriPoint)
        {
            double X = _CvsRect.X + oriPoint.Voltage * (double)_CvsRect.Width / CurvesCurrent;
            double Y = _CvsRect.Y + (375 - oriPoint.Pmax) * (double)_CvsRect.Height / 375;
            Point point = new Point();
            point.X = (int)X;
            point.Y = (int)Y;
            return point;
        }

        /// <summary>
        /// 画坐标背景
        /// </summary>
        /// <param name="graph"></param>
        private void DrawGrid(Graphics graph)
        {
            Pen gridPen = null;
            Font ticFont = null;
            Font horizFont = null;
            Font vertFont = null;
            SolidBrush brush = null;


            try
            {
                gridPen = new Pen(Color.Gray, 1);
                ticFont = new Font(_FontName, 7, FontStyle.Regular);
                horizFont = new Font(@"Tahoma", 10, FontStyle.Regular, GraphicsUnit.Point, (byte)(134)); ;// graphicsText.DrawString(_text, _font, _brush, new PointF(200, 80), format, -45f);// CreateRotatedFont(graph, 0);
                vertFont = new Font(@"Tahoma", 10, FontStyle.Regular, GraphicsUnit.Point, (byte)(134));
                brush = new SolidBrush(Color.Black);

                GraphicsText graphicsText = new GraphicsText();
                graphicsText.Graphics = graph;
                // 绘制围绕点旋转的文本  
                StringFormat format = new StringFormat();
                format.Alignment = StringAlignment.Center;
                format.LineAlignment = StringAlignment.Center;

                #region

                //画水平线及坐标数值
                for (int i = 0; i < 11; i++)
                {
                    int curY = _CvsRect.Y + i * HeightPerV;
                    string ticString = (10 - i).ToString("0.0");
                    graph.DrawLine(gridPen, _CvsRect.X, curY, _CvsRect.X + _CvsRect.Width, curY);
                    graph.DrawString(ticString, ticFont, brush, _CvsRect.X - 16, curY - 5);
                }
                //画垂直线及坐标数值
                for (int j = 0; j < 6; j++)
                {
                    int curX = _CvsRect.X + j * WidthPer5A;
                    string ticString = (j * 10).ToString("0.0");
                    graph.DrawLine(gridPen, curX, _CvsRect.Y, curX, _CvsRect.Y + _CvsRect.Height);
                    if (j > 0) graph.DrawString(ticString, ticFont, brush, curX - 10, _CvsRect.Y + _CvsRect.Height + 2);
                }
                StringFormat strFmrt = new StringFormat(StringFormatFlags.NoWrap | StringFormatFlags.NoClip);
                graph.DrawString("Voltage(V)", horizFont, brush, _CvsRect.X + _CvsRect.Width - 30, _CvsRect.Y + _CvsRect.Height + 18, strFmrt);
                

                //graph.RotateTransform(90);
                //graph.DrawString("Current(A)", vertFont, brush, 20, _CvsRect.Y - 32, strFmrt);
                graphicsText.DrawString("Current(A)", vertFont, brush, new PointF(15, _CvsRect.Y + _CvsRect.Height / 2), format, -90f);


                //graph.RotateTransform(0);

                //graph.DrawString("Current(A)", vertFont, brush, 35, _CvsRect.Y - 32, strFmrt);


                //画功率坐标数值

                ticFont = new Font(_FontName, 7, FontStyle.Regular);
                brush = new SolidBrush(Color.Red);
                graph.DrawString("Pmax(W)", vertFont, brush, _CvsRect.X + _CvsRect.Width - 35, _CvsRect.Y - 32, strFmrt);
                for (int m = 0; m < 11; m++)  // 400*10=400= _CvsRect.Height 
                {
                    int curY = _CvsRect.Y - 5 + m * HeightPerV;
                    string ticString = (375 - (float)m * 37.5).ToString("0.0");
                    //graph.DrawString(ticString, ticFont, brush, _CvsRect.X + 1, curY);
                    graph.DrawString(ticString, ticFont, brush, _CvsRect.X + _CvsRect.Width - 35, curY);
                }


                #endregion

                ////画功率水平线及坐标数值，200——300
                //for (int i = 0; i < ((MaxPmax - MinPmax) + 1); i++)  // 52*10=520= _CvsRect.Height 
                //{
                //    int curY = _CvsRect.Y + i * (int)(_CvsRect.Height / ((MaxPmax - MinPmax)));
                //    string ticString = (MaxPmax - (float)i).ToString();
                //    if (ticString == MinPmax.ToString())
                //    {
                //        gridPen = new Pen(Color.Black, 2);
                //        gridPen.DashStyle = System.Drawing.Drawing2D.DashStyle.Solid;
                //        gridPen.EndCap = LineCap.ArrowAnchor;//定义线尾的样式为箭头
                //        graph.DrawLine(gridPen, _CvsRect.X, curY, _CvsRect.X + _CvsRect.Width + 30, curY);
                //    }
                //    else
                //    {
                //        gridPen = new Pen(Color.Gray, 1);
                //        gridPen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dot;//.Custom;
                //        // gridPen.DashPattern = new float[] { 1, 1 };
                //        graph.DrawLine(gridPen, _CvsRect.X, curY, _CvsRect.X + _CvsRect.Width, curY);
                //    }


                //    graph.DrawString(ticString, ticFont, brush, _CvsRect.X - 16, curY - 5);
                //}


                //GraphicsText graphicsText = new GraphicsText();
                //graphicsText.Graphics = graph;
                //// 绘制围绕点旋转的文本  
                //StringFormat format = new StringFormat();
                //format.Alignment = StringAlignment.Center;
                //format.LineAlignment = StringAlignment.Center;


                ////画电压垂直线及坐标数值，只显示 20个点
                //for (int j = 0; j < ShowPoints + 1; j++)// 50*20=1000=  _CvsRect.Width 
                //{
                //    int curX = _CvsRect.X + j * (int)(_CvsRect.Width / ShowPoints);
                //    string ticString = (j < _info.Rows.Count ? _info.Rows[j]["SN"].ToString() : "");// (j).ToString("0.0");
                //    if (j == 0)
                //    {
                //        gridPen = new Pen(Color.Black, 2);
                //        gridPen.DashStyle = System.Drawing.Drawing2D.DashStyle.Solid;
                //        gridPen.EndCap = LineCap.ArrowAnchor;//定义线尾的样式为箭头
                //        graph.DrawLine(gridPen, curX, _CvsRect.Y + _CvsRect.Height, curX, _CvsRect.Y - 30);

                //    }
                //    else
                //    {
                //        gridPen = new Pen(Color.Gray, 1);
                //        gridPen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dot;//.Custom;
                //        //  gridPen.DashPattern = new float[] { 1, 1 };
                //        graph.DrawLine(gridPen, curX, _CvsRect.Y, curX, _CvsRect.Y + _CvsRect.Height);
                //    }

                //    // if (j > 0)
                //    graphicsText.DrawString(ticString, ticFont, brush, new PointF(curX - 8, _CvsRect.Y + _CvsRect.Height + ticFont.ToString().Length / 3), format, -55f); //graph.DrawString(ticString, ticFont, brush, curX - 12, _CvsRect.Y + _CvsRect.Height + 1);
                //}

                //StringFormat strFmrt = new StringFormat(StringFormatFlags.NoWrap | StringFormatFlags.NoClip);
                //brush = new SolidBrush(Color.Red);

                //graph.DrawString("SN", vertFont, brush, _CvsRect.X + _CvsRect.Width / 2, _CvsRect.Y + _CvsRect.Height + 15, strFmrt);
                ////  graph.DrawString("Pmax(w)", vertFont, brush, 5, 22, strFmrt);
                //graphicsText.DrawString("Pmax(w)", vertFont, brush, new PointF(15, _CvsRect.Y + _CvsRect.Height / 2), format, -90f);




            }
            finally
            {
                if (gridPen != null) gridPen.Dispose();
                if (ticFont != null) ticFont.Dispose();
                if (horizFont != null) horizFont.Dispose();
                if (vertFont != null) vertFont.Dispose();
                if (brush != null) brush.Dispose();
            }
        }
        /// <summary>
        /// 根据坐标点画曲线
        /// </summary>
        /// <param name="graph"></param>
        private void DrawCurvas(Graphics graph)
        {
            //Pen pointPen = null;
            Pen curvasPen = null;
            // if (_OriPointList == null) return;
            try
            {

                if (OldoriPointArray.Length == 3)
                {
                    //pointPen = curvasPen = new Pen(Color.Red, 1);
                    //画所有数据点
                    curvasPen = new Pen(Color.Blue, 1);
                    if (_CurvasPointList != null)
                    {
                        graph.DrawLines(curvasPen, _CurvasPointList.ToArray());
                    }

                    // 画功率曲线  Add  by genhong.hu On 2016-06-02
                    curvasPen = new Pen(Color.Red, 1);
                    if (_PowerPointList != null)
                    {
                        graph.DrawLines(curvasPen, _PowerPointList.ToArray());
                    }

                }
                else // HR原厂商写的  标签
                {
                    _PowerPointList = new List<Point>();
                    _CurvasPointList = new List<Point>();
                    foreach (I_V_Point oriPoint in OldoriPointArray)
                    {
                        _CurvasPointList.Add(GetPointByOriginal(oriPoint));

                        // add by genhong.hu On 2016-06-02 增加功率曲线
                        P_V_Point pmaxpoint = new P_V_Point();
                        pmaxpoint.Pmax = oriPoint.Current * oriPoint.Voltage;
                        pmaxpoint.Voltage = oriPoint.Voltage;
                        _PowerPointList.Add(GetPowerPointByOriginal(pmaxpoint));

                    }


                    curvasPen = new Pen(Color.Blue, 1);
                    if (_CurvasPointList != null)
                    {
                        graph.DrawLines(curvasPen, _CurvasPointList.ToArray());
                    }

                    // 画功率曲线  Add  by genhong.hu On 2016-06-02
                    curvasPen = new Pen(Color.Red, 1);
                    if (_PowerPointList != null)
                    {
                        graph.DrawLines(curvasPen, _PowerPointList.ToArray());
                    }
                }

            }
            catch (Exception)
            {

            }
            finally
            {
                //if (pointPen != null) pointPen.Dispose();
                if (curvasPen != null) curvasPen.Dispose();
            }
        }
        /// <summary>
        /// 画曲线
        /// </summary>
        /// <param name="gragh"></param>
        private void InvokeDynamicShow(Graphics gragh)
        {
            object[] args = new object[1];
            args[0] = gragh;
            this.Invoke(new DelegateDynamicShow(DrawCurvas), args);
        }

        /// <summary>
        /// 获取功率曲线真实坐标对应的画布坐标
        /// </summary>
        /// <param name="oriPoint"></param>
        /// <returns></returns>
        private Point GetPowerPointByOriginal(int i)
        {
            double X = (double)_CvsRect.X + i * (double)_CvsRect.Width / ShowPoints;
            double Y = (double)_CvsRect.Y + (MaxPmax - double.Parse(_info.Rows[i]["Pmax"].ToString())) * (double)_CvsRect.Height / (MaxPmax - MinPmax);
            // double Y = _CvsRect.Y + (9 - oriPoint.Current) * _CvsRect.Height / 9;
            Point point = new Point();
            point.X = (int)Math.Floor(X);
            point.Y = (int)Math.Floor(Y);
            return point;
        }
        ToolTip toolTip1 = new ToolTip();

        private void UserControl1_MouseClick(object sender, MouseEventArgs e)
        {
            //Point screenPoint = Control.MousePosition;//鼠标相对于屏幕左上角的坐标

            //Point formPoint = this.PointToClient(Control.MousePosition);//鼠标相对于窗体左上角的坐标

            //Point contextMenuPoint = TextBox1.PointToClient(Control.MousePosition); //鼠标相对于TextBox1左上角的坐标
            Point controlPoint = this.PointToClient(Control.MousePosition);
            toolTip1.Active = false;
            foreach (Point point in _PowerPointList)
            {
                if (Math.Abs(point.X - controlPoint.X) < 10 &&
                    Math.Abs(point.Y - controlPoint.Y) < 10)
                {
                    toolTip1.Active = true;

                    int a = (int)Math.Ceiling((double)(point.X - _CvsRect.X) * ShowPoints / _CvsRect.Width);
                    string Msg = string.Empty;
                    Msg += "SN:" + _info.Rows[a]["SN"].ToString() + "\r";
                    Msg += "Pmax:" + _info.Rows[a]["Pmax"].ToString() + "\r";
                    Msg += "Isc:" + _info.Rows[a]["Isc"].ToString() + "\r";
                    Msg += "Voc:" + _info.Rows[a]["Voc"].ToString() + "\r";
                    Msg += "Ipm:" + _info.Rows[a]["Ipm"].ToString() + "\r";
                    Msg += "Vpm:" + _info.Rows[a]["Vpm"].ToString() + "\r";
                    Msg += "Eqp:" + _info.Rows[a]["Eqp"].ToString() + "\r";
                    Msg += "TestTime:" + _info.Rows[a]["TestTime"].ToString() + "\r";
                    toolTip1.Show(Msg, this, new Point(controlPoint.X + 15, controlPoint.Y + 15), 10000);

                    break;
                }

            }
        }


        private string strPWord = string.Empty;
        public void WriteWordOnPic(string strWord)
        {
            strPWord=strWord;
            Bitmap canvas = new Bitmap(this.Width, this.Height);
            Graphics gragh = Graphics.FromImage((System.Drawing.Image)canvas);
            InvokeWriteWordShow(gragh);
        }
        private void InvokeWriteWordShow(Graphics gragh)
        {
            object[] args = new object[1];
            args[0] = gragh;
            this.Invoke(new DelegateDynamicShow(WirteWord), args);
        }
        
        private void WirteWord(Graphics gragh)
        {
             Pen gridPen = null;
            Font ticFont = null;
            Font horizFont = null;
            Font vertFont = null;
            SolidBrush brush = null;
            gridPen = new Pen(Color.Gray, 1);
                ticFont = new Font(_FontName, 17, FontStyle.Regular);
                horizFont = new Font(@"Tahoma", 10, FontStyle.Regular, GraphicsUnit.Point, (byte)(134)); ;// graphicsText.DrawString(_text, _font, _brush, new PointF(200, 80), format, -45f);// CreateRotatedFont(graph, 0);
                vertFont = new Font(@"Tahoma", 10, FontStyle.Regular, GraphicsUnit.Point, (byte)(134));
                brush = new SolidBrush(Color.Blue);

             StringFormat strFmrt = new StringFormat(StringFormatFlags.NoWrap | StringFormatFlags.NoClip);
              gragh.DrawString(strPWord, horizFont, brush, _CvsRect.X+_CvsRect.Width /2 , _CvsRect.Y + _CvsRect.Height/2, strFmrt);
             
        }
          

    }

    public class I_V_Point
    {
        public double Current { get; set; }
        public double Voltage { get; set; }
        public I_V_Point()
        {
        }

        public I_V_Point(double dCurrent, double dVoltage)
        {
            Current = dCurrent; Voltage = dVoltage;
        }
    }

    public class P_V_Point
    {
        public double Pmax { get; set; }
        public double Voltage { get; set; }
    }
}
