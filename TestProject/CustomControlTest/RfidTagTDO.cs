using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CustomControl;

namespace CustomControlTest
{
    public partial class RfidTagTDO
    {
        #region 私有变量
        private string _ID;
        /// <summary>
        /// 产品族
        /// </summary>
        private string _ModelNumber;
        /// <summary>
        /// 组件包装日期
        /// </summary>
        private DateTime _DateOfModulePacked;
        /// <summary>
        /// 参数
        /// </summary>
        private string _PIVF;
        /// <summary>
        /// 组件序列号
        /// </summary>
        private string _SerialNo;
        /// <summary>
        /// 托盘号
        /// </summary>
        private string _PalletNum;
        /// <summary>
        /// 电池片生产日期
        /// </summary>
        private DateTime _CellDate;
        /// <summary>
        /// 电池片厂商
        /// </summary>
        private string _CellSource;
        /// <summary>
        /// 数据点
        /// </summary>
        private I_V_Point[] _PointArray;


        private double _pmax;
        private double _voc;
        private double _isc;
        private double _vpm;
        private double _ipm;
        private double _ff;
        #endregion

        #region 公共属性
        /// <summary>
        /// 
        /// </summary>
        public string ID
        {
            get { return _ID; }
        }
        
        /// <summary>
        /// 产品族
        /// </summary>
        public string ModelNumber
        {
            get { return _ModelNumber; }
        }
        /// <summary>
        /// 组件生产日期
        /// </summary>
        public DateTime DateOfModulePacked
        {
            get { return _DateOfModulePacked; }
        }
        


        /// <summary>
        /// 参数
        /// </summary>
        public string PIVF
        {
            get { return _PIVF; }
        }
        /// <summary>
        /// 组件序列号
        /// </summary>
        public string SerialNo
        {
            get { return _SerialNo; }
            set { _SerialNo = value; }
        }
        
        /// <summary>
        /// 托盘号
        /// </summary>
        public string PalletNum
        {
            get { return _PalletNum; }
        }
        /// <summary>
        /// 数据点
        /// </summary>
        public I_V_Point[] PointArray
        {
            get { return _PointArray; }
        }
        /// <summary>
        /// 电池片生产日期
        /// </summary>
        public DateTime CellDate
        {
            get { return _CellDate; }// 增加电池片生产日期 Add by genhong.hu On2014-08-08

        }
        /// <summary>
        /// 电池片厂商
        /// </summary>
        public string CellSource
        {
            get
            {
                return _CellSource;//增加电池片厂商 Add by genhong.hu On2014-08-08
            }
        }


        /// <summary>
        /// 
        /// </summary>
        public double Pmax
        {
            get { return _pmax; }
        }
        /// <summary>
        /// 
        /// </summary>
        public double Voc
        {
            get { return _voc; }
        }
        /// <summary>
        /// 
        /// </summary>
        public double Isc
        {
            get { return _isc; }
        }
        /// <summary>
        /// 
        /// </summary>
        public double Vpm
        {
            get { return _vpm; }
        }
        /// <summary>
        /// 
        /// </summary>
        public double Ipm
        {
            get { return _ipm; }
        }

        public double FF
        {
            get { return _ff; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Profile
        {
            get
            {
                //return _ModelNumber + "|" + _DateOfModuleCell.ToString("yyyy.MM.dd") + "|"
                //    + _PIVF + "|" + _SerialNo + "|" + _PalletNum + "|" + PointArray.Length;

                return _ModelNumber + "|" + _DateOfModulePacked.ToString("yyyy.MM.dd") + "|"
                  + _PIVF + "|" + _SerialNo + "|" + _CellDate.ToString("yyyy.MM.dd") + "|" + PointArray.Length;
            }
        }

        #endregion

        public RfidTagTDO(string id)
        {
            _ID = id;
            _SerialNo = id.ToUpper().Trim(); ;
        }

        public RfidTagTDO(string modelNumber, DateTime dateOfModuleCell, string pvif, string serialNo, I_V_Point[] pointArray, DateTime celldate)
        {
            SetBasicInfo(modelNumber, dateOfModuleCell, pvif, serialNo, celldate);
            SetPointArray(pointArray);
        }

        public void SetBasicInfo(string modelNumber, DateTime dateOfModulepacked, string pivf, string serialNo, DateTime celldate)
        {
            lock (this)
            {
                _ModelNumber = modelNumber;
                _DateOfModulePacked = dateOfModulepacked;
                _PIVF = pivf;
                _SerialNo = serialNo;

                _CellDate = celldate;
                _CellSource = "RISEN ENERGY CO.,LTD";
            }
        }


        public RfidTagTDO(string modelNumber, DateTime dateOfModuleCell, double spmax, double svoc, double sisc, double svpm, double sipm, double sFF, string serialNo, I_V_Point[] pointArray, DateTime celldate)
        {
            SetBasicInfo(modelNumber, dateOfModuleCell, spmax, svoc, sisc, svpm, sipm, sFF, serialNo, celldate);
            SetPointArray(pointArray);
        }
        public void SetBasicInfo(string modelNumber, DateTime dateOfModulepacked, double spmax, double svoc, double sisc, double svpm, double sipm, double sFF, string serialNo, DateTime celldate)
        {
            lock (this)
            {
                _ModelNumber = modelNumber;
                _DateOfModulePacked = dateOfModulepacked;
                _pmax = spmax;
                _voc = svoc;
                _isc = sisc;
                _vpm = svpm;
                _ipm = sipm;

                _ff = Math.Round(Vpm * Ipm / Voc / Isc * 100, 2);

                if (_ff == 0) _ff = sFF; // add by genhong.hu On 2016-12-14

                _PIVF = spmax + "Wp," + svoc + "V," + sisc + "A," + svpm + "V," + sipm + "A," + sFF + "%";


                SerialNo = serialNo;
                _CellDate = celldate;
                _CellSource = "RISEN ENERGY CO.,LTD";
            }
        }

        public void SetPointArray(I_V_Point[] pointArray)
        {
            lock (this)
            {
                _PointArray = pointArray;
            }
        }
    }


    //public class I_V_Point
    //{
    //    public double Current { get; set; }
    //    public double Voltage { get; set; }
    //}
}
