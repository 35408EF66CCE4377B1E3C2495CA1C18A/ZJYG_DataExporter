using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Resources;
using System.Reflection;
using System.Security.Cryptography;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows.Forms;
using System.Configuration;



namespace DataExporter.Classes
{
    class cls_PublicMethods
    {
      

        /// <summary>
        /// 获取数据导出的路径
        /// </summary>
        /// <param name="ext"></param>
        /// <returns></returns>
        public static string GetExportPath(string DefaultFileNamePrefix, string ext)
        {
            SaveFileDialog g = new SaveFileDialog();
            g.FileName = string.Format("{0}_{1}", DefaultFileNamePrefix, DateTime.Now.ToString("yyyyMMdd-HHmmss"));
            g.Filter = string.Format("{0}|*.{0}", ext);
            if (g.ShowDialog() == DialogResult.OK)
                return g.FileName;
            else
                return "";

        }

        public static string MessageCollectionReader(string ItemName)
        {
            ResourceManager rm = new ResourceManager("Optical_Test_System.Resources.ErrorMessageCollection_zh-CN", Assembly.GetExecutingAssembly());
            return rm.GetString(ItemName);
        }

        public static string UIContentReader(string ItemName)
        {
            ResourceManager rm = new ResourceManager("Optical_Test_System.Resources.UIContent_en", Assembly.GetExecutingAssembly());
            return rm.GetString(ItemName);
        }

        public static object GetAssemblyResource(string ItemName)
        {
            ResourceManager rm = new ResourceManager("Optical Test System", Assembly.GetExecutingAssembly());
            return rm.GetString(ItemName);
        }

        /// <summary>
        /// 返回测试耗时描述的字符串
        /// </summary>
        /// <param name="Duration"></param>
        /// <returns></returns>
        public static string GetDurationString(double Duration)
        {
            return string.Format(" Duration: {0}s", Duration);
        }

        /// <summary>
        /// 验证字符串
        /// </summary>
        /// <param name="RawString"></param>
        /// <param name="Pattern"></param>
        /// <returns></returns>
        public static bool ValidateStringFormat(string RawString, string Pattern)
        {
            Regex r = new Regex(Pattern);
            return r.IsMatch(RawString);
        }

        /// <summary>
        /// 字符串进行MD5编码
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string MD5(string str)
        {
            //Declarations
            Byte[] originalBytes;
            Byte[] encodedBytes;
            MD5 md5;

            //Instantiate MD5CryptoServiceProvider, get bytes for original password and compute hash (encoded password)
            md5 = new MD5CryptoServiceProvider();
            originalBytes = ASCIIEncoding.Default.GetBytes(str);
            encodedBytes = md5.ComputeHash(originalBytes);

            //Convert encoded bytes back to a 'readable' string
            return BitConverter.ToString(encodedBytes).Replace("-", "");

        }

        /// <summary>
        /// 获取图片对象并且按比例缩放
        /// </summary>
        /// <param name="FileName"></param>
        /// <returns></returns>
        public static Image GetPictureToBitmapAndResize(string FileName, int TargetSize)
        {
            Bitmap bmp = new Bitmap(FileName);

            // 按比例缩放
            int NewWidth, NewHeigh;
            if (bmp.Width > bmp.Height)
            {
                NewWidth = TargetSize;
                NewHeigh = (TargetSize / bmp.Width) * bmp.Height;
            }
            else
            {
                NewHeigh = TargetSize;
                NewWidth = (TargetSize / bmp.Height) * bmp.Width;
            }

            Graphics g = Graphics.FromImage(bmp);
            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
            g.DrawImage(bmp, new Rectangle(0, 0, NewWidth, NewHeigh), new Rectangle(0, 0, bmp.Width, bmp.Height), GraphicsUnit.Pixel);
            g.Dispose();

            return bmp;

        }

  

        /// <summary>
        /// 图片转换为二进制
        /// </summary>
        /// <param name="image"></param>
        /// <returns></returns>
        public static byte[] ImageToByteArray(Image image)
        {
            System.IO.MemoryStream ms = new System.IO.MemoryStream();
            image.Save(ms, ImageFormat.Jpeg);
            return ms.ToArray();
        }

        /// <summary>
        /// 讲对象序列化为二进制数组
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static byte[] SerializeObjectToBinaryArray(object obj)
        {
            MemoryStream str = new MemoryStream();
            BinaryFormatter oBFormatter = new BinaryFormatter();
            oBFormatter.Serialize(str, obj);
            byte[] oSerializedObj = str.ToArray();
            return oSerializedObj;
        }

        /// <summary>
        /// 从二进制序列中恢复对象
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static object DeserializeObjectFromBinaryArray(byte[] data)
        {
            MemoryStream oStr = new MemoryStream(data);
            BinaryFormatter oBFormatter = new BinaryFormatter();
            return oBFormatter.Deserialize(oStr);
        }

        /// <summary>
        /// SN流水号格式转换为字符串
        /// </summary>
        /// <param name="Format"></param>
        /// <returns></returns>
        public static string FlowNumberFormatForSNToString(cls_PublicVariables.EnumFlowNumberFormatForSN Format)
        {
            string ret = string.Empty;
            switch(Format)
            {
                case cls_PublicVariables.EnumFlowNumberFormatForSN.Dex:
                    ret = "D";
                    break;

                case cls_PublicVariables.EnumFlowNumberFormatForSN.Hex:
                    ret = "H";
                    break;
            }
            return ret;
        }

        /// <summary>
        /// 字符转换为SN流水号格式
        /// </summary>
        /// <param name="Format"></param>
        /// <returns></returns>
        public static cls_PublicVariables.EnumFlowNumberFormatForSN StringToFlowNumberFormatForSN(string Format)
        {
            cls_PublicVariables.EnumFlowNumberFormatForSN mFlowNumberFormat = cls_PublicVariables.EnumFlowNumberFormatForSN.Dex;
            switch (Format)
            {
                case "D":
                    mFlowNumberFormat = cls_PublicVariables.EnumFlowNumberFormatForSN.Dex;
                    break;

                case "H":
                    mFlowNumberFormat = cls_PublicVariables.EnumFlowNumberFormatForSN.Hex;
                    break;
            }
            return mFlowNumberFormat;
        }

   

        public static string GetCDKEYPWD()
        {
            return "AMC_";
        }
    }
}
