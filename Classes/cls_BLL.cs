using System;
using System.Collections;
using System.Linq;
using System.Text;
using System.Data;
using System.Net;
using System.Collections.Generic;

namespace DataExporter.Classes
{
    class cls_BLL
    {
        string Error = string.Empty;

        public cls_BLL()
        {}

        #region MF_OSA_System_Spec

        /// <summary>
        /// 获取Spec表中的所有PN
        /// </summary>
        /// <returns></returns>
        public ArrayList GetDistinctPNInSpecTable()
        {
            DataTable dt = new cls_DAL().GetView("view_DistinctPNInSpecTable", "", ref Error);
            if (dt == null)
            {
                throw new Exception(Error);
            }
            else
            {
                ArrayList ret = new ArrayList();
                foreach (DataRow dr in dt.Rows)
                {
                    ret.Add(dr[0].ToString());
                }
                return ret;
            }
        }

        /// <summary>
        /// 获取Spec表中的所有Temperature
        /// </summary>
        /// <returns></returns>
        public ArrayList GetDisitnctTemperatureInSpecTable()
        {
            DataTable dt = new cls_DAL().GetView("view_DisitnctTemperatureInSpecTable", "", ref Error);
            if (dt == null)
            {
                throw new Exception(Error);
            }
            else
            {
                ArrayList ret = new ArrayList();
                foreach (DataRow dr in dt.Rows)
                {
                    ret.Add(dr[0].ToString());
                }
                return ret;
            }
        }

        /// <summary>
        /// 获取料号
        /// </summary>
        /// <returns></returns>
        public List<string> GetDistinctProductNoInSpecTable()
        {
            DataTable dt = new cls_DAL().GetView("view_distinctpninspectable", "", ref Error);
            if (dt == null)
            {
                throw new Exception(Error);
            }
            else
            {
                List<string> ret = new List<string>();
                ret.Add("全部");

                foreach (DataRow dr in dt.Rows)
                {
                    ret.Add(dr[0].ToString());
                }
                return ret;
            }
        }

        /// <summary>
        /// 从Spec表中删除PN
        /// </summary>
        /// <param name="PN"></param>
        /// <param name="Error"></param>
        /// <returns></returns>
        public bool DeletePNInSpecTable(string[] PN, ref string Error)
        {
            StringBuilder strPN = new StringBuilder();
            for (int i = 0; i < PN.Length; i++)
            {
                strPN.Append("'" + PN + "'");
                if (i < (PN.Length - 1))
                {
                    strPN.Append(",");
                }
            }

            return new cls_DAL().Delete_MF_OSA_System_Spec("PN in (" + strPN.ToString() + ")", ref Error);
        }
        #endregion

        #region MF_OSA_System_UserList 表相关
        /// <summary>
        /// 读取所有用户信息
        /// </summary>
        /// <returns></returns>
        public DataTable GetAllUserList()
        {
            DataTable dt = new cls_DAL().GetTable_MF_OSA_System_UserList("", ref Error);
            if (dt == null)
            {
                throw new Exception(Error);
            }
            else
            {
                return dt;
            }
        }

  

        /// <summary>
        /// 验证用户名及密码
        /// </summary>
        /// <param name="WorkID"></param>
        /// <param name="Password"></param>
        /// <param name="Error"></param>
        /// <returns></returns>
        public bool ValidatePasswordByWorkID(string WorkID, string Password, ref string Error)
        { 
            DataTable dt = new cls_DAL().GetTable_MF_OSA_System_UserList(string.Format(" WHERE `Work ID` = '{0}'", WorkID), ref Error);
            if (dt == null || dt.Rows.Count != 1)
            {
                Error = cls_PublicMethods.MessageCollectionReader("Error0027");
                return false;
            }
            else
            {
                DataRow drUserInfo = dt.Rows[0];
                if (cls_PublicMethods.MD5(Password) == drUserInfo["Password"].ToString())
                {
                    //SetUserInfoToGlobalVariables(drUserInfo);
                    return true;
                }
                else
                {
                    Error = cls_PublicMethods.MessageCollectionReader("Error0028");
                    return false;
                }
            }
        }

        /// <summary>
        /// 验证用户名及密码
        /// </summary>
        /// <param name="WorkID"></param>
        /// <param name="Password"></param>
        /// <param name="Error"></param>
        /// <returns></returns>
        public bool ValidatePasswordByUID(string UID, string Password, ref string Error)
        {
            DataTable dt = new cls_DAL().GetTable_MF_OSA_System_UserList(string.Format(" WHERE UID = '{0}'", UID), ref Error);
            if (dt == null || dt.Rows.Count != 1)
            {
                return false;
            }
            else
            {
                DataRow drUserInfo = dt.Rows[0];
                if (cls_PublicMethods.MD5(Password) == drUserInfo["Password"].ToString())
                {
                    //SetUserInfoToGlobalVariables(drUserInfo);
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        /// <summary>
        /// 按UID删除用户
        /// </summary>
        /// <param name="UID"></param>
        /// <returns></returns>
        public bool DeleteUserByUID(string UID, ref string Error)
        {
            string where = string.Format("WHERE UID = '{0}'", UID);
            return new cls_DAL().Delete_MF_OSA_System_UserList(where, ref Error);
        }

        #endregion

  
    }
}
