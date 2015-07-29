using System;
using System.Collections;
using System.Data;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows;
using Excel = Microsoft.Office.Interop.Excel;

namespace Utilities.ExcelExport
{

	/// <summary>
	/// ExportStyle specifies Column wise,Row wise or Sheet wise
	/// </summary>
	public enum ExportStyle
	{
		RowWise = 0,
		ColumnWise ,
		SheetWise
	}

	/// <summary>
	/// Specifies the formatting details for the excel
	/// </summary>
	sealed public class ExcelStyle
	{
		public Color HeaderBackColor = Color.LightGray;
		public Color HeaderForeColor = Color.Black;
		public Color ItemForeColor = Color.Black;
		public Color ItemBackColor = Color.White;
		public Color ItemAlternateBackColor = Color.White;
		public string FontName = "Verdana";
		public bool ItemFontBold = false;
		public bool HeaderFontBold = true;
		public bool ItemItalic = false;
		public bool HeaderItalic = false;
		public ushort FontSize = 9;
		//Excel column-row settings
		public int ColumnSpace = 1;
		public int RowSpace = 1;
		public int ColumnSpaceBetweenTables = 2;
		public int RowSpaceBetweenTables = 2;
		public bool RepeatColumnHeader = true;
	}

	/// <summary>
	/// Exports Data in Dataset to Excel.
	/// </summary>
	public class Export
	{
		public delegate void ExportProgressDelegete(int iCurrentItem,int iTotalItems);
		/// <summary>
		/// Export progress indicator event.
		/// </summary>
		public event ExportProgressDelegete ExportRowProgress;
		SortedList _listrow = null;
		ExcelStyle excelStyle;

		/// <summary>
		/// Specifies the formatting style for the excel like color,font etc.
		/// </summary>
		public ExcelStyle ExcelFormattingStyle
		{
			set
			{
				excelStyle = value;
			}
			get
			{
				return excelStyle;
			}
		}
 
		public Export()
		{
			excelStyle = new ExcelStyle();
		}

		/// <summary>
		/// Exports the DataSet to the excel and opens the excel.
		/// </summary>
		/// <param name="dsData">Dataset to be exported</param>
		/// <param name="style">Style specifies Column wise,Row wise or Sheet wise</param>
		public void ExportDataToExcel(DataSet dsData,ExportStyle style)
		{
			Excel.ApplicationClass excel = null;
			Excel.Workbooks workbooks = null;
			Excel.Workbook workbook = null;
			try
			{
				_listrow = new SortedList();
				excel =  new Excel.ApplicationClass();
                excel.Visible = true;
				workbooks = excel.Workbooks;
				workbook = workbooks.Add(true);

				ExportCurrentData(excel,dsData,style);
				_listrow = null;
				Excel.Worksheet worksheet = (Excel.Worksheet)excel.ActiveSheet; 
				worksheet.Activate();

				excel.Visible = true;
				
			}
			catch(Exception ee)
			{
				throw ee;
			}
			finally
			{
				if(workbook!=null) Marshal.ReleaseComObject(workbook);
				if(workbooks!=null) Marshal.ReleaseComObject(workbooks);
				if(excel!=null)Marshal.ReleaseComObject(excel);
				workbook = null;
				workbooks = null;
				excel = null;
			}
		}

		
		private void ExportCurrentData(Excel.ApplicationClass excel,DataSet dsData,ExportStyle style)
		{
			int rowIndex = 1;
			int colIndex = 0;
			int startCol = 0;
			int i=0;
			int eventRow = 1;
			foreach(DataTable dtb in dsData.Tables)
			{
				startCol = colIndex;
				AddDataTableToExcel(excel,dtb,style,ref rowIndex,ref colIndex);
				if(ExportRowProgress!=null)
					ExportRowProgress(eventRow++,dsData.Tables.Count);
				switch(style)
				{
					case ExportStyle.RowWise:
					{
						colIndex = 0;
						rowIndex += excelStyle.RowSpaceBetweenTables + 1;
						break;
					}
					case ExportStyle.ColumnWise:
					{
						rowIndex = 1;
						colIndex += excelStyle.ColumnSpaceBetweenTables;
						break;
					}
					case ExportStyle.SheetWise:
					{
						if(i != dsData.Tables.Count-1)
						{
							excel.Worksheets.Add(
								Type.Missing, Type.Missing, Type.Missing, Type.Missing);
						}
						colIndex = 0;
						rowIndex = 1;
						break;
					}
				}
				i++;
			}//For
		}

		private void AddDataTableToExcel(Excel.ApplicationClass excel,DataTable table,ExportStyle style,ref int rowIndex,ref int columnIndex)
		{
			int colstart = columnIndex;
			int colbak = colstart;
			if(style == ExportStyle.SheetWise)
			{
				Excel.Worksheet worksheet = (Excel.Worksheet)excel.ActiveSheet;
				worksheet.Name = table.TableName;
			}



            /*************** 写入Header ****************/
            columnIndex += excelStyle.ColumnSpace;
            int col_cnt = table.Columns.Count;
            StringBuilder header_string = new StringBuilder();
            foreach (DataColumn col in table.Columns)
            {
                header_string.Append(col.ColumnName);
                header_string.Append("\t");
            }
            // 数据粘贴到系统剪贴板
            Clipboard.SetText(header_string.ToString());
            // 选择当前活动的sheet
            Excel.Worksheet wsh = (Excel.Worksheet)excel.ActiveSheet;
            // 选择将要粘贴数据的左上角的Cell
            Excel.Range rng = (Excel.Range)excel.Cells[rowIndex, columnIndex];
            rng.Select();
            // 粘贴
            wsh.Paste();

            // 设置Range格式
            rng = wsh.Range[excel.Cells[rowIndex, columnIndex], excel.Cells[rowIndex, columnIndex + col_cnt - 1]];
            rng.Select();
            rng.Font.Bold = true;
            rng.Interior.Color = System.Drawing.ColorTranslator.ToOle(excelStyle.HeaderBackColor);
            rng.Font.Color = System.Drawing.ColorTranslator.ToOle(excelStyle.HeaderForeColor);
            rng.Font.Name = excelStyle.FontName;
            rng.Font.Size = excelStyle.FontSize;
            rng.Font.Italic = excelStyle.HeaderItalic;
            rng.Font.Bold = excelStyle.HeaderFontBold;


            /*************** 写入数据 ****************/
            rowIndex++;
            int row_cnt = table.Rows.Count;
            StringBuilder data_string = new StringBuilder();
            foreach (DataRow row in table.Rows)
            {
                
                foreach (DataColumn col in table.Columns)
                {
                    data_string.Append(row[col.ColumnName].ToString());
                    data_string.Append("\t"); 
                }
                data_string.Append("\n");   // 添加换行符
            }

            // 数据粘贴到系统剪贴板
            Clipboard.SetText(data_string.ToString());
            // 选择将要粘贴数据的左上角的Cell
            rng = (Excel.Range)excel.Cells[rowIndex, columnIndex];
            rng.Select();
            // 粘贴
            wsh.Paste();
            // 设置Range格式
            rng = wsh.Range[excel.Cells[rowIndex, columnIndex], excel.Cells[rowIndex + row_cnt - 1, columnIndex + col_cnt - 1]];
            rng.Select();
            rng.Font.Color = System.Drawing.ColorTranslator.ToOle(excelStyle.ItemForeColor);
            rng.Font.Name = excelStyle.FontName;
            rng.Font.Size = excelStyle.FontSize;
            rng.Font.Italic = excelStyle.ItemItalic;
            rng.Font.Bold = excelStyle.ItemFontBold;

            /**************原程序采用Cell By Cell的写方式，效率低下，我把它改成Copy字符串的形式，提高速度 ***************/
            /*
            foreach(DataColumn col in table.Columns) 
            { 
                columnIndex+= excelStyle.ColumnSpace ; 
                Excel.Range cel = (Excel.Range)excel.Cells[rowIndex,columnIndex];
                cel.Font.Bold = true;
                cel.Interior.Color = System.Drawing.ColorTranslator.ToOle(excelStyle.HeaderBackColor);
                cel.Font.Color =  System.Drawing.ColorTranslator.ToOle(excelStyle.HeaderForeColor);
                cel.Font.Name = excelStyle.FontName;
                cel.Font.Size = excelStyle.FontSize;
                cel.Font.Italic = excelStyle.HeaderItalic;
                cel.Font.Bold = excelStyle.HeaderFontBold;
                excel.Cells[rowIndex,columnIndex]=col.ColumnName; 
            } 

            foreach(DataRow row in table.Rows)
            { 
                rowIndex+= excelStyle.RowSpace; 
                foreach(DataColumn col in table.Columns) 
                { 
                    colstart += excelStyle.ColumnSpace ;

                    Excel.Range cel = (Excel.Range)excel.Cells[rowIndex,colstart];

                    if(rowIndex!= 0 && rowIndex%2 == 0)
                    {
                        if(excelStyle.ItemAlternateBackColor != Color.White)
                            cel.Interior.Color = System.Drawing.ColorTranslator.ToOle(excelStyle.ItemAlternateBackColor);
                    }
                    else
                    {
                        if(excelStyle.ItemBackColor != Color.White)
                            cel.Interior.Color = System.Drawing.ColorTranslator.ToOle(excelStyle.ItemBackColor);
                    }

                    cel.Font.Color =  System.Drawing.ColorTranslator.ToOle(excelStyle.ItemForeColor);

                    cel.Font.Name = excelStyle.FontName;
                    cel.Font.Size = excelStyle.FontSize;
                    cel.Font.Italic = excelStyle.ItemItalic;
                    cel.Font.Bold = excelStyle.ItemFontBold;
                    excel.Cells[rowIndex,colstart]=row[col.ColumnName].ToString(); 
                } 
                colstart = colbak;

            }
            */
            if (!_listrow.ContainsKey(table.Rows.Count))
			{
				_listrow.Add(table.Rows.Count,rowIndex);
			}
		}

	}
}
