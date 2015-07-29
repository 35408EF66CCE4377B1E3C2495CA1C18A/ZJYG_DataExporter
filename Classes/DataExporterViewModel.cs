
using System;
using System.Data;
using System.Diagnostics;
using System.Text;
using System.Threading;
using System.Windows;
using System.Windows.Input;
using Utilities.ExcelExport;

namespace DataExporter.Classes
{
    public class DataExporterViewModel : CommonBase
    {
        #region Variables
        private RelayCommand search_command;
        private RelayCommand export_command;
        #endregion

        /// <summary>
        /// 主界面的ViewModel
        /// </summary>
        public DataExporterViewModel()
        {
                Initialize();
        }

        /// <summary>
        /// 初始化实例
        /// </summary>
        void Initialize()
        {
            this.PNList = new cls_BLL().GetDistinctProductNoInSpecTable().ToArray();
            this.StartDate = DateTime.Now.AddMonths(-1);
            this.EndDate = DateTime.Now;
            this.PN = "全部";
            this.SearchElapsed = 0;
            this.IsBusy = false;
            this.ReturnedRecordCount = 0;

            search_command = new RelayCommand(SearchExecute);
            export_command = new RelayCommand(ExportExecute);
        }

        #region Properties
        public string[] PNList
        {
            set;
            get;
        }

        public string PN
        {
            set;
            get;
        }

        public DateTime StartDate
        {
            set;
            get;
        }

        public DateTime EndDate
        {
            set;
            get;
        }

        public string Barcode
        {
            set;
            get;

        }

        public string WorkOrder
        {
            set;
            get;
        }

        public DataTable TestData
        {
            private set;
            get;
        }

        /// <summary>
        /// 返回查询耗时（秒）
        /// </summary>
        public double SearchElapsed
        {
            private set;
            get;
        }

        /// <summary>
        /// 返回查询的记录数
        /// </summary>
        public int ReturnedRecordCount
        {
            private set;
            get;
        }

        /// <summary>
        /// 是否正在查询
        /// </summary>
        public bool IsBusy
        {
            private set;
            get;
        }

        public ICommand Search
        {
            get
            {
                return search_command;
            }
        }

        public ICommand Export
        {
            get
            {
                return export_command;
            }
        }
        #endregion

        #region Commands Excute

        #region Search Excute
        private void SearchExecute(object param)
        {
            string err = string.Empty;
            
            this.IsBusy = true;
            RaisePropertyChangedEvent("IsBusy");
            
            /* 清空上次查询记录 */
            this.TestData = null;
            RaisePropertyChangedEvent("TestData");

            this.ReturnedRecordCount = 0;
            RaisePropertyChangedEvent("ReturnedRecordCount");

            /* 查询数据 */
            StringBuilder query_condition = new StringBuilder();
            query_condition.Append(
                @"SELECT 
                    `a`.`PN`          	    AS `PN`,
                    `t`.`SN`          	    AS `SN`,
                    DATE_FORMAT(`t`.`LIV_WhenTested`, '%Y/%m/%d %H:%i:%s') AS `LIV TestDate`,
                    `t`.`LIV_ErrorCode`     AS `ErrorCode`,
                    `t`.`TestTemp`    	    AS `Temperature`,
                    `t`.`LIV_Para_01`       AS `Ith`,
                    `t`.`LIV_Para_14`       AS `Vf@Io`,
                    `t`.`LIV_Para_12`       AS `Po@Io`,
                    `t`.`LIV_Para_13`       AS `Im@Io`,  
                    `t`.`LIV_Para_05`       AS `Po@Ith+20mA`,
                    `t`.`LIV_Para_06`       AS `Im@Ith+20mA`,
                    `t`.`LIV_Para_17`       AS `Vf@Power`,
                    `t`.`LIV_Para_15`       AS `Iop@Power`,
                    `t`.`LIV_Para_16`       AS `Im@Power`,
                    `t`.`LIV_Para_02`       AS `SE_Po`,
                    `t`.`LIV_Para_10`       AS `TrackingError`,
                    DATE_FORMAT(`t`.`Sens_WhenTested`, '%Y/%m/%d %H:%i:%s') AS `SEN TestDate`,
                    `t`.`Sens_Para_01`   AS `VBR`
                FROM `mf_osa_result_integration` AS `t`
                LEFT JOIN `mf_osa_productbase` AS `a` ON `t`.`SN` = `a`.`SN`
                WHERE ");
            if (this.Barcode != null && this.Barcode != "")
            {
                query_condition.Append(string.Format("`t`.`SN` = '{0}' AND ", this.Barcode));
            }

            if(this.PN != null && (this.PN != "" && this.PN != "全部"))
            {
                query_condition.Append(string.Format("`a`.`PN` = '{0}' AND ", this.PN));
            }

            if(this.WorkOrder != null && this.WorkOrder != "")
            {
                query_condition.Append(string.Format("`a`.`WorkOrder` = '{0}' AND ", this.WorkOrder));
            }

            query_condition.Append(string.Format("(`t`.`LIV_WhenTested` BETWEEN '{0}' AND '{1}' OR `t`.`Sens_WhenTested` BETWEEN '{0}' AND '{1}')", 
                this.StartDate.ToShortDateString() + " 00:00:00", this.EndDate.ToShortDateString() + " 23:59:59"));

            Thread t = new Thread(()=>
                {
                    // The stopwatch is used to analyize the elapse time of the searching
                    Stopwatch sw = new Stopwatch();
                    sw.Start();
                    
                    // Search the data from the database
                    this.TestData = new cls_DAL().ReturnCommonQuery(query_condition.ToString(), ref err);
                    RaisePropertyChangedEvent("TestData");

                    sw.Stop();
                    this.SearchElapsed = Math.Round(sw.Elapsed.TotalSeconds, 3);
                    RaisePropertyChangedEvent("SearchElapsed");

                    this.IsBusy = false;
                    RaisePropertyChangedEvent("IsBusy");

                    this.ReturnedRecordCount = this.TestData.Rows.Count;
                    RaisePropertyChangedEvent("ReturnedRecordCount");

                });
            t.Start();
            
        }

        private bool CanExecuteSearchCommand()
        {
            return true;
        }

        #endregion


        #region Export Excute

        public void ExportExecute(object param)
        {
            string para = param.ToString();

            switch(para)
            {
                case "Excel":
                    try
                    {
                        Export excel = new Utilities.ExcelExport.Export();
                        DataSet ds = this.TestData.DataSet;
                        excel.ExcelFormattingStyle = new ExcelStyle();
                        excel.ExportDataToExcel(ds, ExportStyle.SheetWise);
                    }
                    catch(Exception ex)
                    {
                        MessageBox.Show(ex.Message, "错误", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                    break;
            }
        }

        private bool CanExecuteExportCommand()
        {
            if (this.TestData == null || this.TestData.Rows.Count == 0)
                return false;
            else
                return true;
        }

        #endregion

        #endregion
    }
}
