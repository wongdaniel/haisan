using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace haisan.dao
{
    class SessionFactory
    {
        private static SqlConnection con;  //创建连接对象

        #region   打开数据库连接
        /// <summary>
        /// 打开数据库连接.
        /// </summary>
        public static SqlConnection getConnection()
        {
            if (con == null)//判断连接对象是否为空
            {
                con = new SqlConnection("Data Source=.;DataBase=db_sanhai;User ID=sa;PWD=sa");//创建数据库连接对象
            }
            try
            {
                if (con.State == System.Data.ConnectionState.Closed)//判断数据库连接是否关闭
                    con.Open();//打开数据库连接
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
                return null;
            }

            return con;
        }
        #endregion

        #region  关闭连接
        /// <summary>
        /// 关闭数据库连接, 暂时采用，查询后马上关闭的策略。！！！！ add by zhicai on 2013-05-10
        /// </summary>
        public static void Close()
        {
            if (con != null)//判断连接对象是否不为空
                con.Close();//关闭数据库连接
        }
        #endregion

        #region 释放数据库连接资源
        /// <summary>
        /// 释放资源
        /// </summary>
        public static void Dispose()
        {
            if (con != null)//判断连接对象是否不为空
            {
                con.Dispose();//释放数据库连接资源
                con = null;//设置数据库连接为空
            }
        }
        #endregion
    }
}
