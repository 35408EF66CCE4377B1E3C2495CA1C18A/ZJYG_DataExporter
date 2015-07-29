using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Configuration;

namespace DataExporter.Classes
{
    class cls_DAL
    {
        /// <summary>
        /// 数据库连接超时时间（秒）
        /// </summary>
        const int CONN_TIMEOUT = 10;


        MySqlConnection conn;

        public cls_DAL()
        {
            conn = new MySqlConnection(BuildTheConnectionString());
            try
            {
                conn.Open();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Get the connection string with app setting file
        /// </summary>
        /// <returns></returns>
        string BuildTheConnectionString()
        {
            // 从配置文件中获取数据库服务器地址
            string ip = ConfigurationManager.AppSettings["DBServerIP"];
            return BuildTheConnectionString(ip, "root", "0000");
        }

        /// <summary>
        /// Get the connection string with specified IP / User / Password
        /// </summary>
        /// <param name="IP"></param>
        /// <param name="User"></param>
        /// <param name="Password"></param>
        /// <returns></returns>
        string BuildTheConnectionString(string IP, string User, string Password)
        {
            MySqlConnectionStringBuilder builder = new MySqlConnectionStringBuilder();
            builder.ConnectionTimeout = CONN_TIMEOUT;
            builder.Database = "optical_test_system";
            builder.Server = IP;
            builder.UserID = User;
            builder.Password = Password;
            builder.CharacterSet = "utf8";
            return builder.ConnectionString;
        }

        #region MF_OSA_System_Setting
        public DataTable GetTable_MF_OSA_System_Setting(string Where, ref string Error)
        {
            DataTable dt_ret_ = null;
            try
            {
                //MySqlDataAdapter da = new MySqlDataAdapter(string.Format("SELECT mf_osa_system_spec.PN, mf_osa_system_spec.TestTemp, mf_osa_system_spec.SpecVersion, mf_osa_system_spec.Description, mf_osa_system_spec.WhenKeyin, mf_osa_system_spec.WhoKeyin, mf_osa_system_spec.WhereKeyin, mf_osa_system_spec.IopStart, mf_osa_system_spec.IopEnd, mf_osa_system_spec.IopStep, mf_osa_system_spec.MaxPower, mf_osa_system_spec.InitialPower, mf_osa_system_spec.FixPower, mf_osa_system_spec.FixIop, mf_osa_system_spec.IthPlusIop, mf_osa_system_spec.Lambda, mf_osa_system_spec.PoRange, mf_osa_system_spec.TECondition, mf_osa_system_spec.StopCondition, mf_osa_system_spec.SlopeCondition, mf_osa_system_spec.LIV_Spec_01, mf_osa_system_spec.LIV_Spec_02, mf_osa_system_spec.LIV_Spec_03, mf_osa_system_spec.LIV_Spec_04, mf_osa_system_spec.LIV_Spec_05, mf_osa_system_spec.LIV_Spec_06, mf_osa_system_spec.LIV_Spec_07, mf_osa_system_spec.LIV_Spec_08, mf_osa_system_spec.LIV_Spec_09, mf_osa_system_spec.LIV_Spec_10, mf_osa_system_spec.LIV_Spec_11, mf_osa_system_spec.LIV_Spec_12, mf_osa_system_spec.LIV_Spec_13, mf_osa_system_spec.LIV_Spec_14, mf_osa_system_spec.LIV_Spec_15, mf_osa_system_spec.LIV_Spec_16, mf_osa_system_spec.LIV_Spec_17, mf_osa_system_spec.LIV_Spec_18, mf_osa_system_spec.LIV_Spec_19, mf_osa_system_spec.LIV_Spec_20, mf_osa_system_spec.LIV_Spec_21, mf_osa_system_spec.LIV_Spec_22, mf_osa_system_spec.LIV_Spec_23, mf_osa_system_spec.LIV_Spec_24, mf_osa_system_spec.LIV_Spec_25, mf_osa_system_spec.LIV_Spec_26, mf_osa_system_spec.LIV_Spec_27, mf_osa_system_spec.LIV_Spec_28, mf_osa_system_spec.LIV_Spec_29, mf_osa_system_spec.LIV_Spec_30 FROM mf_osa_system_spec {0}", Where), conn);
                MySqlDataAdapter da = new MySqlDataAdapter(string.Format("SELECT * FROM mf_osa_system_Setting {0}", Where), conn);
                DataSet ds = new DataSet();
                da.Fill(ds);
                dt_ret_ = ds.Tables[0];
            }
            catch (Exception ex)
            {
                Error = ex.Message;
                dt_ret_ = null;
            }
            finally
            {
                if (conn.State == ConnectionState.Open)
                    conn.Close();
            }
            return dt_ret_;

        }

        /// <summary>
        /// 更新系统设置项目
        /// </summary>
        /// <param name="Item"></param>
        /// <param name="Value"></param>
        /// <param name="Error"></param>
        /// <returns></returns>
        public bool Updata_Setting(string Item, string Value, ref string Error)
        {
            bool ret = false;
            try
            {
                using (MySqlCommand cmd = new MySqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.Parameters.Add("Item", MySqlDbType.VarChar).Value = Item;
                    cmd.Parameters.Add("ItemValue", MySqlDbType.VarChar).Value = Value;

                    cmd.CommandText = "Update mf_osa_system_setting SET ItemValue = @ItemValue WHERE Item = @Item";
                    cmd.ExecuteNonQuery();
                }

                ret = true;
            }
            catch (Exception ex)
            {
                Error = ex.Message;
                ret = false;
            }
            finally
            {
                if (conn.State == ConnectionState.Open)
                    conn.Close();
            }
            return ret;
        }
        #endregion

        #region MF_OSA_System_Spec

        /// <summary>
        /// 查询MF_OSA_System_Spec表
        /// </summary>
        /// <param name="Where"></param>
        /// <param name="Error"></param>
        /// <returns></returns>
        public DataTable GetTable_MF_OSA_System_Spec(string Where, ref string Error)
        {
            DataTable dt_ret_ = null;
            try
            {
                //MySqlDataAdapter da = new MySqlDataAdapter(string.Format("SELECT mf_osa_system_spec.PN, mf_osa_system_spec.TestTemp, mf_osa_system_spec.SpecVersion, mf_osa_system_spec.Description, mf_osa_system_spec.WhenKeyin, mf_osa_system_spec.WhoKeyin, mf_osa_system_spec.WhereKeyin, mf_osa_system_spec.IopStart, mf_osa_system_spec.IopEnd, mf_osa_system_spec.IopStep, mf_osa_system_spec.MaxPower, mf_osa_system_spec.InitialPower, mf_osa_system_spec.FixPower, mf_osa_system_spec.FixIop, mf_osa_system_spec.IthPlusIop, mf_osa_system_spec.Lambda, mf_osa_system_spec.PoRange, mf_osa_system_spec.TECondition, mf_osa_system_spec.StopCondition, mf_osa_system_spec.SlopeCondition, mf_osa_system_spec.LIV_Spec_01, mf_osa_system_spec.LIV_Spec_02, mf_osa_system_spec.LIV_Spec_03, mf_osa_system_spec.LIV_Spec_04, mf_osa_system_spec.LIV_Spec_05, mf_osa_system_spec.LIV_Spec_06, mf_osa_system_spec.LIV_Spec_07, mf_osa_system_spec.LIV_Spec_08, mf_osa_system_spec.LIV_Spec_09, mf_osa_system_spec.LIV_Spec_10, mf_osa_system_spec.LIV_Spec_11, mf_osa_system_spec.LIV_Spec_12, mf_osa_system_spec.LIV_Spec_13, mf_osa_system_spec.LIV_Spec_14, mf_osa_system_spec.LIV_Spec_15, mf_osa_system_spec.LIV_Spec_16, mf_osa_system_spec.LIV_Spec_17, mf_osa_system_spec.LIV_Spec_18, mf_osa_system_spec.LIV_Spec_19, mf_osa_system_spec.LIV_Spec_20, mf_osa_system_spec.LIV_Spec_21, mf_osa_system_spec.LIV_Spec_22, mf_osa_system_spec.LIV_Spec_23, mf_osa_system_spec.LIV_Spec_24, mf_osa_system_spec.LIV_Spec_25, mf_osa_system_spec.LIV_Spec_26, mf_osa_system_spec.LIV_Spec_27, mf_osa_system_spec.LIV_Spec_28, mf_osa_system_spec.LIV_Spec_29, mf_osa_system_spec.LIV_Spec_30 FROM mf_osa_system_spec {0}", Where), conn);
                MySqlDataAdapter da = new MySqlDataAdapter(string.Format("SELECT * FROM view_spec {0}", Where), conn);
                DataSet ds = new DataSet();
                da.Fill(ds);
                dt_ret_ = ds.Tables[0];
            }
            catch (Exception ex)
            {
                Error = ex.Message;
                dt_ret_ = null;
            }
            finally
            {
                if (conn.State == ConnectionState.Open)
                    conn.Close();
            }
            return dt_ret_;
        }

        /// <summary>
        /// 删除规格
        /// </summary>
        /// <param name="Where"></param>
        /// <param name="Error"></param>
        /// <returns></returns>
        public bool Delete_MF_OSA_System_Spec(string Where, ref string Error)
        {
            try
            {
                using (MySqlCommand cmd = new MySqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = "DELETE FROM mf_osa_system_spec WHERE " + Where;
                    cmd.ExecuteNonQuery();
                }
                return true;
            }
            catch (Exception ex)
            {
                Error = ex.Message;
                return false;
            }
        }

        /// <summary>
        /// 插入新规格

        /// <summary>
        /// 更改PN的可用性
        /// </summary>
        /// <param name="PN"></param>
        /// <returns></returns>
        public bool ChangeEnabled_MF_OSA_System_Spec(string PN, string Temperature, ref string Error)
        {
            bool ret = false;
            try
            {
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = conn;
                cmd.Parameters.Add("PN", MySqlDbType.VarChar).Value = PN;
                cmd.Parameters.Add("TestTemp", MySqlDbType.VarChar).Value = Temperature;

                cmd.CommandText = @"DELETE FROM mf_osa_system_spec WHERE PN = @PN And TestTemp = @TestTemp";

                cmd.ExecuteNonQuery();


                ret = true;

            }
            catch (Exception ex)
            {
                Error = ex.Message;
                ret = false;
            }
            finally
            {
                if (conn.State == ConnectionState.Open)
                    conn.Close();
            }
            return ret;
        }
        #endregion

        #region MF_OSA_System_UserList

 
        
     

        /// <summary>
        /// 读取用户信息
        /// </summary>
        /// <param name="Where"></param>
        /// <param name="Error"></param>
        /// <returns></returns>
        public DataTable GetTable_MF_OSA_System_UserList(string Where, ref string Error)
        {
            DataTable dt_ret_ = null;
            try
            {
                MySqlDataAdapter da = new MySqlDataAdapter(string.Format("SELECT * FROM view_UserList {0}", Where), conn);
                DataSet ds = new DataSet();
                da.Fill(ds);
                dt_ret_ = ds.Tables[0];
            }
            catch (Exception ex)
            {
                Error = ex.Message;
                dt_ret_ = null;
            }
            finally
            {
                if (conn.State == ConnectionState.Open)
                    conn.Close();
            }
            return dt_ret_;
        }

        /// <summary>
        /// 删除记录
        /// </summary>
        /// <param name="Where"></param>
        /// <param name="Error"></param>
        /// <returns></returns>
        public bool Delete_MF_OSA_System_UserList(string Where, ref string Error)
        {
            bool ret = false;
            try
            {
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = conn;

                cmd.CommandText = string.Format("Delete from mf_osa_system_userlist {0}", Where);
                cmd.ExecuteNonQuery();

                ret = true;
            }
            catch (Exception ex)
            {
                Error = ex.Message;
                ret = false;
            }
            finally
            {
                if (conn.State == ConnectionState.Open)
                    conn.Close();
            }
            return ret;
        }

        #endregion

        #region MF_OSA_Result_LIV

        public DataTable GetTable_MF_OSA_Result_LIV(string Where, ref string Error)
        {
            DataTable dt_ret_ = null;
            try
            {
                //MySqlDataAdapter da = new MySqlDataAdapter(string.Format("SELECT mf_osa_system_spec.PN, mf_osa_system_spec.TestTemp, mf_osa_system_spec.SpecVersion, mf_osa_system_spec.Description, mf_osa_system_spec.WhenKeyin, mf_osa_system_spec.WhoKeyin, mf_osa_system_spec.WhereKeyin, mf_osa_system_spec.IopStart, mf_osa_system_spec.IopEnd, mf_osa_system_spec.IopStep, mf_osa_system_spec.MaxPower, mf_osa_system_spec.InitialPower, mf_osa_system_spec.FixPower, mf_osa_system_spec.FixIop, mf_osa_system_spec.IthPlusIop, mf_osa_system_spec.Lambda, mf_osa_system_spec.PoRange, mf_osa_system_spec.TECondition, mf_osa_system_spec.StopCondition, mf_osa_system_spec.SlopeCondition, mf_osa_system_spec.LIV_Spec_01, mf_osa_system_spec.LIV_Spec_02, mf_osa_system_spec.LIV_Spec_03, mf_osa_system_spec.LIV_Spec_04, mf_osa_system_spec.LIV_Spec_05, mf_osa_system_spec.LIV_Spec_06, mf_osa_system_spec.LIV_Spec_07, mf_osa_system_spec.LIV_Spec_08, mf_osa_system_spec.LIV_Spec_09, mf_osa_system_spec.LIV_Spec_10, mf_osa_system_spec.LIV_Spec_11, mf_osa_system_spec.LIV_Spec_12, mf_osa_system_spec.LIV_Spec_13, mf_osa_system_spec.LIV_Spec_14, mf_osa_system_spec.LIV_Spec_15, mf_osa_system_spec.LIV_Spec_16, mf_osa_system_spec.LIV_Spec_17, mf_osa_system_spec.LIV_Spec_18, mf_osa_system_spec.LIV_Spec_19, mf_osa_system_spec.LIV_Spec_20, mf_osa_system_spec.LIV_Spec_21, mf_osa_system_spec.LIV_Spec_22, mf_osa_system_spec.LIV_Spec_23, mf_osa_system_spec.LIV_Spec_24, mf_osa_system_spec.LIV_Spec_25, mf_osa_system_spec.LIV_Spec_26, mf_osa_system_spec.LIV_Spec_27, mf_osa_system_spec.LIV_Spec_28, mf_osa_system_spec.LIV_Spec_29, mf_osa_system_spec.LIV_Spec_30 FROM mf_osa_system_spec {0}", Where), conn);
                MySqlDataAdapter da = new MySqlDataAdapter(string.Format("SELECT * FROM mf_osa_result_liv_snapshot {0}", Where), conn);
                DataSet ds = new DataSet();
                da.Fill(ds);
                dt_ret_ = ds.Tables[0];
            }
            catch (Exception ex)
            {
                Error = ex.Message;
                dt_ret_ = null;
            }
            finally
            {
                if (conn.State == ConnectionState.Open)
                    conn.Close();
            }

            return dt_ret_;
        }

      #endregion

        #region MF_OSA_Result_Sens

        public bool Insert_MF_OSA_Result_Sens(string Barcode, string PN, string Workorder, string M01, string M02, string M03, string M04, string M05, string M06, string M07, string M08, string M09, string M10, string M11, string TestTemp, int SpecVersion, double ElapsedTime, double VBR, double SensAtVBR03, int ErrorCode, string Note, ref string Error)
        {
            bool ret = false;
            MySqlTransaction tran = conn.BeginTransaction();
            MySqlCommand cmd = conn.CreateCommand();
            try
            {
                cmd.Connection = conn;
                cmd.Transaction = tran;

                #region 保存Sens数据
                cmd.Parameters.Add("pSN", MySqlDbType.VarChar).Value = Barcode;
                cmd.Parameters.Add("pPN", MySqlDbType.VarChar).Value = PN;
                cmd.Parameters.Add("pTestTemp", MySqlDbType.VarChar).Value = TestTemp;
                cmd.Parameters.Add("pSpecVersion", MySqlDbType.Int32).Value = SpecVersion;
                cmd.Parameters.Add("pElapsedTime", MySqlDbType.Float).Value = ElapsedTime;
                cmd.Parameters.Add("pWhoTested", MySqlDbType.VarChar).Value = cls_PublicVariables.OperatorUID;
                cmd.Parameters.Add("pWhereTested", MySqlDbType.VarChar).Value = cls_PublicVariables.Station;
                cmd.Parameters.Add("pVBR", MySqlDbType.Float).Value = VBR;
                cmd.Parameters.Add("pSensAtVBR03", MySqlDbType.Float).Value = SensAtVBR03;
                cmd.Parameters.Add("pSensAtVBR09", MySqlDbType.Float).Value = cls_PublicVariables.NARESULTFLAG;
                cmd.Parameters.Add("pOverload", MySqlDbType.Float).Value = cls_PublicVariables.NARESULTFLAG;
                cmd.Parameters.Add("pErrorCode", MySqlDbType.UInt64).Value = ErrorCode;

                if (Note != "")
                    cmd.Parameters.Add("pNote", MySqlDbType.VarChar).Value = Note;
                else
                    cmd.Parameters.Add("pNote", MySqlDbType.VarChar).Value = "";

                cmd.CommandText = "procedure_insert_ret_Sens";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.ExecuteNonQuery();
                #endregion

                #region 保存ProductBase数据
                BuildMysqlCommandForSavingProductBase(ref cmd, Barcode, PN, Workorder, M01, M02, M03, M04, M05, M06, M07, M08, M09, M10, M11);
                #endregion

                tran.Commit();
                ret = true;

            }
            catch (Exception ex)
            {
                tran.Rollback();
                Error = ex.Message;
                ret = false;
            }
            finally
            {
                if (conn.State == ConnectionState.Open)
                    conn.Close();
            }

            return ret;

        }
        #endregion

        #region MF_OSA_ProductBase

        public void BuildMysqlCommandForSavingProductBase(ref MySqlCommand cmd, string SN, string PN, string Workorder, string M01, string M02, string M03, string M04, string M05, string M06, string M07, string M08, string M09, string M10, string M11)
        {
            cmd.Parameters.Clear();
            cmd.Parameters.Add("pSN", MySqlDbType.VarChar).Value = SN;
            cmd.Parameters.Add("pPN", MySqlDbType.VarChar).Value = PN;
            cmd.Parameters.Add("pWorkorder", MySqlDbType.VarChar).Value = Workorder;
            cmd.Parameters.Add("pM01", MySqlDbType.VarChar).Value = M01 == "" ? "N/A" : M01;
            cmd.Parameters.Add("pM02", MySqlDbType.VarChar).Value = M02 == "" ? "N/A" : M02;
            cmd.Parameters.Add("pM03", MySqlDbType.VarChar).Value = M03 == "" ? "N/A" : M03;
            cmd.Parameters.Add("pM04", MySqlDbType.VarChar).Value = M04 == "" ? "N/A" : M04;
            cmd.Parameters.Add("pM05", MySqlDbType.VarChar).Value = M05 == "" ? "N/A" : M05;
            cmd.Parameters.Add("pM06", MySqlDbType.VarChar).Value = M06 == "" ? "N/A" : M06;
            cmd.Parameters.Add("pM07", MySqlDbType.VarChar).Value = M07 == "" ? "N/A" : M07;
            cmd.Parameters.Add("pM08", MySqlDbType.VarChar).Value = M08 == "" ? "N/A" : M08;
            cmd.Parameters.Add("pM09", MySqlDbType.VarChar).Value = M09 == "" ? "N/A" : M09;
            cmd.Parameters.Add("pM10", MySqlDbType.VarChar).Value = M10 == "" ? "N/A" : M10;
            cmd.Parameters.Add("pM11", MySqlDbType.VarChar).Value = M11 == "" ? "N/A" : M11;

            cmd.CommandText = "procedure_update_productbase";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.ExecuteNonQuery();
        }

        /// <summary>
        /// 保存ProductBase信息
        /// </summary>
        /// <param name="SN"></param>
        /// <param name="PN"></param>
        /// <param name="Workorder"></param>
        /// <param name="M01"></param>
        /// <param name="M02"></param>
        /// <param name="M03"></param>
        /// <param name="M04"></param>
        /// <param name="M05"></param>
        /// <param name="M06"></param>
        /// <param name="M07"></param>
        /// <param name="M08"></param>
        /// <param name="M09"></param>
        /// <param name="M10"></param>
        /// <param name="M11"></param>
        /// <param name="Error"></param>
        /// <returns></returns>
        public bool Update_MF_OSA_ProductBase(string SN, string PN, string Workorder, string M01, string M02, string M03, string M04, string M05, string M06, string M07, string M08, string M09, string M10, string M11, ref string Error)
        {
            bool ret = false;
            try
            {
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = conn;
                cmd.Parameters.Add("pSN", MySqlDbType.VarChar).Value = SN;
                cmd.Parameters.Add("pPN", MySqlDbType.VarChar).Value = PN;
                cmd.Parameters.Add("pWorkorder", MySqlDbType.VarChar).Value = Workorder;
                cmd.Parameters.Add("pM01", MySqlDbType.VarChar).Value = M01 == "" ? "N/A" : M01;
                cmd.Parameters.Add("pM02", MySqlDbType.VarChar).Value = M02 == "" ? "N/A" : M02;
                cmd.Parameters.Add("pM03", MySqlDbType.VarChar).Value = M03 == "" ? "N/A" : M03;
                cmd.Parameters.Add("pM04", MySqlDbType.VarChar).Value = M04 == "" ? "N/A" : M04;
                cmd.Parameters.Add("pM05", MySqlDbType.VarChar).Value = M05 == "" ? "N/A" : M05;
                cmd.Parameters.Add("pM06", MySqlDbType.VarChar).Value = M06 == "" ? "N/A" : M06;
                cmd.Parameters.Add("pM07", MySqlDbType.VarChar).Value = M07 == "" ? "N/A" : M07;
                cmd.Parameters.Add("pM08", MySqlDbType.VarChar).Value = M08 == "" ? "N/A" : M08;
                cmd.Parameters.Add("pM09", MySqlDbType.VarChar).Value = M09 == "" ? "N/A" : M09;
                cmd.Parameters.Add("pM10", MySqlDbType.VarChar).Value = M10 == "" ? "N/A" : M10;
                cmd.Parameters.Add("pM11", MySqlDbType.VarChar).Value = M11 == "" ? "N/A" : M11;

                cmd.CommandText = "procedure_update_productbase";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.ExecuteNonQuery();

                ret = true;
            }
            catch (Exception ex)
            {
                Error = ex.Message;
                ret = false;
            }
            finally
            {
                if (conn.State == ConnectionState.Open)
                    conn.Close();
            }
            return ret;
        }

        #endregion

        #region Common

            /// <summary>
            /// 获取视图
            /// </summary>
            /// <param name="ViewName"></param>
            /// <param name="Where"></param>
            /// <param name="Error"></param>
            /// <returns></returns>
            public DataTable GetView(string ViewName, string Where, ref string Error)
            {
                DataTable dt_ret_ = null;
                try
                {
                    MySqlDataAdapter da = new MySqlDataAdapter(string.Format("SELECT * FROM {0} {1}", ViewName, Where), conn);
                    DataSet ds = new DataSet();
                    da.Fill(ds);
                    dt_ret_ = ds.Tables[0];
                }
                catch (Exception ex)
                {
                    Error = ex.Message;
                    dt_ret_ = null;
                }
                finally
                {
                    if (conn.State == ConnectionState.Open)
                        conn.Close();
                }

                return dt_ret_;
            }

        /// <summary>
        /// 执行SQL语句
        /// </summary>
        /// <param name="Query"></param>
        /// <param name="Error"></param>
        /// <returns>Null: 执行查询时出错</returns>
        public DataTable ReturnCommonQuery(string Query, ref string Error)
        {
            DataTable dt_ret_ = null;
            try
            {
                MySqlDataAdapter da = new MySqlDataAdapter(Query, conn);
                da.SelectCommand.CommandTimeout = 60;
                DataSet ds = new DataSet();
                da.Fill(ds);
                dt_ret_ = ds.Tables[0];
            }
            catch (Exception ex)
            {
                Error = ex.Message;
                dt_ret_ = null;
            }
            finally
            {
                if (conn.State == ConnectionState.Open)
                    conn.Close();
            }

            return dt_ret_;
        }
        #endregion


    }
}
