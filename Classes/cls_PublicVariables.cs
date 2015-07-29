using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace DataExporter.Classes
{
    public class cls_PublicVariables
    {
       
        /// <summary>
        /// 设备初始化信息改变事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="Message"></param>
        public delegate void dlgTestModuleInitializationProgressChanged(object sender, string Message);

        public static string ConnString = "";

        public static string OperatorUID = "N/A";
        public static string OperatorName = "N/A";
        public static string OperatorWorkID = "N/A";
        public static string Station = "N/A";
        public static string IPAddress = "0.0.0.0";
        public static UInt64 OperatorAuthorityCode = 0;
        public static DateTime OperatorLoginTime = DateTime.MinValue;
        public static Image OperatorPhoto;
        public static string PortNameBert = "";
        public static string PortNameAttenuator = "";

        /*************************************/
        // Updated @ 2011/10/19
        // Version to 1.1.4
        // COMMENT 程序启动时保存exe文件路径，用以访问Sqlite数据库，解决程序退出时无法找到Sqlite数据库的问题。
        /*************************************/
        public static string ApplicationPath = ""; //程序exe文件所在的路径

        /// <summary>
        /// VOA序列号
        /// </summary>
        public static string VOASeriesNumber = "Unknown"; 

        /// <summary>
        /// 灵敏度测试模式-定值
        /// </summary>
        public const int SENS_TESTMODE_FIX = 0;

        /// <summary>
        /// 灵敏度测试模式-真值
        /// </summary>
        public const int SENS_TESTMODE_REAL = 1;

        #region 各个模块权限代码常量
        public const UInt64 AUTH_SPECSETTING = 0x1;
        public const UInt64 AUTH_USERSETTING = 0x2;
        public const UInt64 AUTH_DATASEARCHING = 0x4;
        public const UInt64 AUTH_SYSTEMSETTING = 0x8;
        public const UInt64 AUTH_TEST_LIV = 0x100;
        public const UInt64 AUTH_TEST_SENS = 0x200;
        public const UInt64 AUTH_TEST_VBR = 0x400;
        #endregion

        public static bool IsGuestMode = false; // 是否为来宾模式

        public static Color colorPASS = Color.LawnGreen;
        public static Color colorFAIL = Color.Red;
        public static Color colorUnavailable = SystemColors.Control;

        /// <summary>
        /// 系统支持的测试类型
        /// </summary>
        public enum EnumTestCategory
        {
            LIV, SENS, VBR
        }

        /// <summary>
        /// 测试结果类型
        /// </summary>
        public enum EnumTestResult
        {
            PASS, FAIL, NA
        }

        /// <summary>
        /// 数字源表类型
        /// </summary>
        public enum EnumSourceMeterType
        {
            DSM1000S,
            DSM1100S
        }

        /// <summary>
        /// 衰减器类型
        /// </summary>
        public enum EnumVOAType
        {
            OLA_55M,
            GM8040,
            G_VPMC,
            OPHYLINK
        }

        /// <summary>
        /// 支持Bert类型
        /// </summary>
        public enum EnumBertType
        {
            BERT8010,
            PLX0M3
        }

        /// <summary>
        /// SN中的流水号支持的格式
        /// </summary>
        public enum EnumFlowNumberFormatForSN
        {
            /// <summary>
            /// 十进制
            /// </summary>
            Dex,
            /// <summary>
            /// 十六进制
            /// </summary>
            Hex
        }

        /// <summary>
        /// LIV测试支持的设备
        /// </summary>
        public static string[] EquipmentsSupportForLIV = null;

        /// <summary>
        /// 测试结果的精度
        /// </summary>
        //public const int TESTRETPrecision = 6;  // 这个值在结果显示的文本框控件里

        /// <summary>
        /// 无效测试值
        /// </summary>
        public const double NARESULTFLAG = -999999;
    }
}
