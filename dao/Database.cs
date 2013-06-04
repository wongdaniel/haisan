using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using System.Windows.Forms;

namespace haisan.dao
{
    class Database
    {

        private static Database database = null;

        private Database()
        {
        }

        #region 单例模式
        public static Database getInstance()
        {
            if (null == database)
                database = new Database();
            return database;
        }
        #endregion


        #region   传入参数并且转换为SqlParameter类型
        /// <summary>
        /// 转换参数
        /// </summary>
        /// <param name="ParamName">存储过程名称或命令文本</param>
        /// <param name="DbType">参数类型</param></param>
        /// <param name="Size">参数大小</param>
        /// <param name="Value">参数值</param>
        /// <returns>新的 parameter 对象</returns>
        public SqlParameter MakeInParam(string ParamName, SqlDbType DbType, int Size, object Value)
        {
            return MakeParam(ParamName, DbType, Size, ParameterDirection.Input, Value);//创建SQL参数
        }

        /// <summary>
        /// 初始化参数值
        /// </summary>
        /// <param name="ParamName">存储过程名称或命令文本</param>
        /// <param name="DbType">参数类型</param>
        /// <param name="Size">参数大小</param>
        /// <param name="Direction">参数方向</param>
        /// <param name="Value">参数值</param>
        /// <returns>新的 parameter 对象</returns>
        public SqlParameter MakeParam(string ParamName, SqlDbType DbType, Int32 Size, ParameterDirection Direction, object Value)
        {
            SqlParameter param;//声明SQL参数对象
            if (Size > 0)//判断参数字段是否大于0
                param = new SqlParameter(ParamName, DbType, Size);//根据指定的类型和大小创建SQL参数
            else
                param = new SqlParameter(ParamName, DbType);//根据指定的类型创建SQL参数
            param.Direction = Direction;//设置SQL参数的类型
            if (!(Direction == ParameterDirection.Output && Value == null))//判断是否为输出参数
                param.Value = Value;//设置参数返回值
            return param;//返回SQL参数
        }
        #endregion

        #region   执行参数命令文本(无数据库中数据返回)

        public int RunProc(string procName, SqlParameter[] prams)
        {
            return RunProc(procName, prams, CommandType.Text);
        }

        /// <summary>
        /// 执行命令
        /// </summary>
        /// <param name="procName">命令文本</param>
        /// <param name="prams">参数对象</param>
        /// <returns></returns>
        public int RunProc(string procName, SqlParameter[] prams, CommandType ct)
        {
            SqlCommand cmd = CreateCommand(procName, prams, ct);//创建SqlCommand命令对象
            if (null == cmd) return 0;

            try
            {
                cmd.ExecuteNonQuery();//执行SQL命令
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return -1;
            }
            finally
            {
                SessionFactory.Close();//关闭数据库连接
            }
            
            return (int)cmd.Parameters["ReturnValue"].Value;//得到执行成功返回值
        }
        /// <summary>
        /// 直接执行SQL语句
        /// </summary>
        /// <param name="procName">命令文本</param>
        /// <returns></returns>
        public int RunProc(string procName)
        {
            if (null == SessionFactory.getConnection()) return 0;

            SqlCommand cmd = new SqlCommand(procName, SessionFactory.getConnection());//创建SqlCommand命令对象
            cmd.ExecuteNonQuery();//执行SQL命令
            SessionFactory.Close();//关闭数据库连接
            return 1;//返回1，表示执行成功
        }

        #endregion

        #region 将命令文本添加到SqlDataAdapter

        public SqlCommand CreateCommand(string procName, SqlParameter[] prams)
        {
            return CreateCommand(procName, prams, CommandType.Text);
        }
        /// <summary>
        /// 创建一个SqlCommand对象以此来执行命令文本
        /// </summary>
        /// <param name="procName">命令文本</param>
        /// <param name="prams"命令文本所需参数</param>
        /// <returns>返回SqlCommand对象</returns>
        public SqlCommand CreateCommand(string procName, SqlParameter[] prams, CommandType ct)
        {
            if (null == SessionFactory.getConnection()) return null;

            SqlCommand cmd = new SqlCommand(procName, SessionFactory.getConnection());//创建SqlCommand命令对象
            cmd.CommandType = ct;//指定要执行的类型为命令文本
            // 依次把参数传入命令文本
            if (prams != null)//判断SQL参数是否不为空
            {
                foreach (SqlParameter parameter in prams)//遍历传递的每个SQL参数
                    cmd.Parameters.Add(parameter);//将SQL参数添加到执行命令对象中
            }
            //加入返回参数
            cmd.Parameters.Add(new SqlParameter("ReturnValue", SqlDbType.Int, 4,
                ParameterDirection.ReturnValue, false, 0, 0, string.Empty, DataRowVersion.Default, null));
            return cmd;//返回SqlCommand命令对象
        }
        #endregion

        #region   执行参数命令文本(有返回值)
        public DataSet RunProcReturn(string procName, SqlParameter[] prams, string tbName)
        {
            return RunProcReturn(procName, prams, tbName, CommandType.Text);
        }
        /// <summary>
        /// 执行查询命令文本，并且返回DataSet数据集
        /// </summary>
        /// <param name="procName">命令文本</param>
        /// <param name="prams">参数对象</param>
        /// <param name="tbName">数据表名称</param>
        /// <returns></returns>
        public DataSet RunProcReturn(string procName, SqlParameter[] prams, string tbName, CommandType ct)
        {
            SqlDataAdapter dap = CreateDataAdaper(procName, prams, ct);//创建桥接器对象
            if (null == dap) return null;

            DataSet ds = new DataSet();//创建数据集对象
            dap.Fill(ds, tbName);//填充数据集
            SessionFactory.Close();//关闭数据库连接
            return ds;//返回数据集
        }


        /// <summary>
        /// 执行查询命令文本，并且返回DataSet数据集
        /// </summary>
        /// <param name="procName">命令文本</param>
        /// <param name="prams">参数对象</param>
        /// <param name="tbName">数据表名称</param>
        /// <returns></returns>
        public DataSet RunProcReturn(DataSet ds, string procName, SqlParameter[] prams, string tbName, CommandType ct)
        {
            SqlDataAdapter dap = CreateDataAdaper(procName, prams, ct);//创建桥接器对象
            if (null == dap) return null;
            if (null == ds)
                ds = new DataSet();
            dap.Fill(ds, tbName);//填充数据集
            SessionFactory.Close();//关闭数据库连接
            return ds;//返回数据集
        }


        public DataSet RunProcReturn(string procName, string tbName)
        {
            return RunProcReturn(procName, tbName, CommandType.Text);
        }
        /// <summary>
        /// 执行命令文本，并且返回DataSet数据集
        /// </summary>
        /// <param name="procName">命令文本</param>
        /// <param name="tbName">数据表名称</param>
        /// <returns>DataSet</returns>
        public DataSet RunProcReturn(string procName, string tbName, CommandType ct)
        {
            SqlDataAdapter dap = CreateDataAdaper(procName, null, ct);//创建桥接器对象
            if (null == dap) return null;

            DataSet ds = new DataSet();//创建数据集对象
            dap.Fill(ds, tbName);//填充数据集
            SessionFactory.Close();//关闭数据库连接
            return ds;//返回数据集
        }

        #endregion

                #region 将命令文本添加到SqlDataAdapter
        /// <summary>
        /// 创建一个SqlDataAdapter对象以此来执行命令文本
        /// </summary>
        /// <param name="procName">命令文本</param>
        /// <param name="prams">参数对象</param>
        /// <returns></returns>
        private SqlDataAdapter CreateDataAdaper(string procName, SqlParameter[] prams, CommandType ct)
        {
            if (null == SessionFactory.getConnection()) return null;

            SqlDataAdapter dap = new SqlDataAdapter(procName, SessionFactory.getConnection());//创建桥接器对象
            
            dap.SelectCommand.CommandType = ct;//指定要执行的类型为命令文本
            if (prams != null)//判断SQL参数是否不为空
            {
                foreach (SqlParameter parameter in prams)//遍历传递的每个SQL参数
                    dap.SelectCommand.Parameters.Add(parameter);//将SQL参数添加到执行命令对象中
            }
            //加入返回参数
            dap.SelectCommand.Parameters.Add(new SqlParameter("ReturnValue", SqlDbType.Int, 4,
                ParameterDirection.ReturnValue, false, 0, 0, string.Empty, DataRowVersion.Default, null));
            return dap;//返回桥接器对象
        }
        #endregion




        //以下函数，数据库连接将不自动关闭，且提供事务支持

        public DataSet RunProcReturnTran(string procName, SqlParameter[] prams, string tbName, SqlTransaction sqlLevel)
        {
            return RunProcReturnTran(procName, prams, tbName, CommandType.Text, sqlLevel);
        }

        public DataSet RunProcReturnTran(string procName, string tbName, CommandType ct, SqlTransaction sqlLevel)
        {
            SqlDataAdapter dap = CreateDataAdaper(procName, null, ct, sqlLevel);//创建桥接器对象
            if (null == dap) return null;

            DataSet ds = new DataSet();//创建数据集对象
            dap.Fill(ds, tbName);//填充数据集
            return ds;//返回数据集
        }

        public DataSet RunProcReturnTran(string procName, SqlParameter[] prams, string tbName, CommandType ct, SqlTransaction sqlLevel)
        {
            SqlDataAdapter dap = CreateDataAdaper(procName, prams, ct, sqlLevel);//创建桥接器对象
            if (null == dap) return null;

            DataSet ds = new DataSet();//创建数据集对象
            dap.Fill(ds, tbName);//填充数据集
            return ds;//返回数据集
        }

        private SqlDataAdapter CreateDataAdaper(string procName, SqlParameter[] prams, CommandType ct, SqlTransaction sqlLevel)
        {
            if (null == SessionFactory.getConnection()) return null;

            SqlDataAdapter dap = new SqlDataAdapter(procName, SessionFactory.getConnection());//创建桥接器对象

            dap.SelectCommand.CommandType = ct;//指定要执行的类型为命令文本
            dap.SelectCommand.Transaction = sqlLevel;

            if (prams != null)//判断SQL参数是否不为空
            {
                foreach (SqlParameter parameter in prams)//遍历传递的每个SQL参数
                    dap.SelectCommand.Parameters.Add(parameter);//将SQL参数添加到执行命令对象中
            }
            //加入返回参数
            dap.SelectCommand.Parameters.Add(new SqlParameter("ReturnValue", SqlDbType.Int, 4,
                ParameterDirection.ReturnValue, false, 0, 0, string.Empty, DataRowVersion.Default, null));
            return dap;//返回桥接器对象
        }

        public int RunProcTran(string procName, SqlParameter[] prams, SqlTransaction sqlLevel)
        {
            return RunProcTran(procName, prams, CommandType.Text, sqlLevel);
        }

        /// <summary>
        /// 执行命令
        /// </summary>
        /// <param name="procName">命令文本</param>
        /// <param name="prams">参数对象</param>
        /// <returns></returns>
        public int RunProcTran(string procName, SqlParameter[] prams, CommandType ct, SqlTransaction sqlLevel)
        {
            SqlCommand cmd = CreateCommand(procName, prams, ct);//创建SqlCommand命令对象
            cmd.Transaction = sqlLevel;

            if (null == cmd) return 0;
            try
            {
                cmd.ExecuteNonQuery();//执行SQL命令
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return -1;
            }

            return (int)cmd.Parameters["ReturnValue"].Value;//得到执行成功返回值
        }
        /// <summary>
        /// 直接执行SQL语句
        /// </summary>
        /// <param name="procName">命令文本</param>
        /// <returns></returns>
        public int RunProcTran(string procName, SqlTransaction sqlLevel)
        {
            if (null == SessionFactory.getConnection()) return 0;

            SqlCommand cmd = new SqlCommand(procName, SessionFactory.getConnection());//创建SqlCommand命令对象
            cmd.Transaction = sqlLevel;
            cmd.ExecuteNonQuery();//执行SQL命令
            return 1;//返回1，表示执行成功
        }


    }
}
