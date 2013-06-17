using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using haisan.util;
using haisan.domain;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Data;

namespace haisan.dao
{
    class BaseDaoImpl : BaseDao
    { 
        private static Database database = Database.getInstance();
        private static BaseDaoImpl baseDaoImpl = null;

        private BaseDaoImpl() { }

        public static BaseDaoImpl getInstance()
        {
            if (null == baseDaoImpl)
                baseDaoImpl = new BaseDaoImpl();
            return baseDaoImpl;
        }

        public MessageLocal saveOrUpdateDataGridView(User user, string table, DataGridView dataGridView)
        {
            MessageLocal msg = new MessageLocal();
            SqlConnection con = SessionFactory.getConnection();
            SqlTransaction sqlTran = con.BeginTransaction(IsolationLevel.RepeatableRead);

            try
            {
                int tableID = getTableIDByName(table, sqlTran);
                if (tableID < 0)
                {
                    msg.Message = "该表格的信息已经被删除，无法更改";
                    sqlTran.Commit();
                    return msg;
                }

                int i = 0;
                for (i = 0; i < dataGridView.Columns.Count; i++)
                {
                    SqlParameter[] prams = {
									database.MakeInParam("@user",  SqlDbType.Int, 0, user.Id),
                                    database.MakeInParam("@table",  SqlDbType.Int, 0, tableID), 
                                    database.MakeInParam("@original_index",  SqlDbType.Int, 0, i), 
                                    database.MakeInParam("@display_index",  SqlDbType.Int, 0, dataGridView.Columns[i].DisplayIndex), 
                                    database.MakeInParam("@width",  SqlDbType.Int, 0, dataGridView.Columns[i].Width), 
                                    database.MakeInParam("@sort",  SqlDbType.Int, 0, 0), 
			        };
                    // MessageBox.Show("original:[" + i + "] display:[" + dataGridView.Columns[i].DisplayIndex + "]");
                    database.RunProcTran("saveOrUpdate_table", prams, CommandType.StoredProcedure, sqlTran);
                }
                sqlTran.Commit();
            }
            catch (Exception e)
            {
                sqlTran.Rollback();
                MessageBox.Show(e.Message);
                msg.IsSucess = false;
                msg.Message = "在保存表格样式时，出现错误！";
                return msg;
            }
            finally
            {
                con.Close();
            }
            msg.IsSucess = true;
            return msg;
        }

        public MessageLocal fillDataGridView(User user, string table, DataGridView dataGridView)
        {
             MessageLocal msg = new MessageLocal();
             try
             {
                 int tableID = getTableIDByName(table);
                 if (tableID < 0)
                 {
                     msg.Message = "该表格的信息已经被删除，无法更改";
                     return msg;
                 }

                 SqlParameter[] prams = {
									database.MakeInParam("@user",  SqlDbType.Int, 0, user.Id),
                                    database.MakeInParam("@table",  SqlDbType.Int, 0, tableID),
			    };
                 DataSet dataset = database.RunProcReturn("SELECT * FROM tb_table_style WHERE ([user] = @user) AND ([table] = @table)", prams, "tb_table_style", CommandType.Text);

                 int count = 0;
                 if (null != dataset && (count = dataset.Tables[0].Rows.Count) > 0)
                 {
                     int i = 0;
                     for (i = 0; i != count; i++)
                     {
                         int index = int.Parse(dataset.Tables[0].Rows[i]["original_index"].ToString());
                         dataGridView.Columns[index].DisplayIndex = int.Parse(dataset.Tables[0].Rows[i]["display_index"].ToString());
                         dataGridView.Columns[index].Width = int.Parse(dataset.Tables[0].Rows[i]["width"].ToString());
                         //    dataGridView.Columns[index].sort = int.Parse(dataset.Tables[0].Rows[0]["sort"].ToString());
                     }
                 }
             }
             catch (Exception e)
             {
                 MessageBox.Show(e.Message);
                 msg.IsSucess = false;
                 msg.Message = "在读取表格样式时，出现错误！";
                 return msg;
             }
            msg.IsSucess = true;
            return msg;
        }

        private int getTableIDByName(string table, SqlTransaction sqlTran)
        {
            MessageLocal msg = new MessageLocal();
            SqlParameter[] prams = {
									    database.MakeInParam("@name",  SqlDbType.VarChar, 50, table)
			};

            DataSet dataset = database.RunProcReturnTran("SELECT id FROM tb_table WHERE (name = @name)", prams, "tb_table", CommandType.Text, sqlTran);
            if (null != dataset && dataset.Tables[0].Rows.Count > 0)
            {
                return int.Parse(dataset.Tables[0].Rows[0]["id"].ToString());
            }

            return -1;
        }

        private int getTableIDByName(string table)
        {
            MessageLocal msg = new MessageLocal();
            SqlParameter[] prams = {
									    database.MakeInParam("@name",  SqlDbType.VarChar, 50, table)
			};

            DataSet dataset = database.RunProcReturn("SELECT id FROM tb_table WHERE (name = @name)", prams, "tb_table", CommandType.Text);
            if (null != dataset && dataset.Tables[0].Rows.Count > 0)
            {
                return int.Parse(dataset.Tables[0].Rows[0]["id"].ToString());
            }

            return -1;
        }

        public MessageLocal deleteEntities(string table, string ids)
        {
            MessageLocal msg = new MessageLocal();
            try
            {
                database.RunProc("DELETE FROM "+ table + " WHERE id IN (" + ids + ")");
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                msg.Message = e.Message;
                return msg;
            }
            msg.IsSucess = true;
            return msg;
        }

        public MessageLocal deleteEntity(string procedure, string table, int id)
        {
            MessageLocal msg = new MessageLocal();
            SqlParameter[] prams = {
			    database.MakeInParam("@id",  SqlDbType.Int, 0, id),
                new SqlParameter("rval", SqlDbType.Int, 0)
			};
            prams[prams.Length - 1].Direction = ParameterDirection.ReturnValue;

            SqlConnection con = SessionFactory.getConnection();
            SqlTransaction sqlTran = con.BeginTransaction(IsolationLevel.RepeatableRead);
            try
            {
                DataSet dataSet = database.RunProcReturnTran(procedure, prams, table, CommandType.StoredProcedure, sqlTran);
                if ("0".Equals(prams[prams.Length - 1].Value.ToString()))
                {
                    msg.IsSucess = true;
                }
                sqlTran.Commit();
             }
            catch (Exception e)
            {
                sqlTran.Rollback();
                msg.IsSucess = false;
                msg.Message = e.Message;
                MessageBox.Show(e.Message, "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            finally
            {
                con.Close();
            }

            return msg;
        }


        // 在传入table参数的时候，记得runProcedu自动在table前面加了tb_前缀。
        public MessageLocal runProcedure(string procedure, SqlParameter[] prams, string table)
        {
            MessageLocal msg = new MessageLocal();
            DataSet dataset = database.RunProcReturn(procedure, prams, "tb_" + table, CommandType.StoredProcedure);

            if ("0".Equals(prams[prams.Length - 1].Value.ToString()))
            {
                msg.IsSucess = true;
            }
            msg.Message = prams[prams.Length - 2].Value.ToString();

            return msg;
        }

        // 在传入table参数的时候，记得runProcedu自动在table前面加了tb_前缀。
        public MessageLocal runProcedureTran(string procedure, SqlParameter[] prams, string table, SqlTransaction sqlTran)
        {
            MessageLocal msg = new MessageLocal();
            DataSet dataset = database.RunProcReturnTran(procedure, prams, "tb_" + table, CommandType.StoredProcedure, sqlTran);

            if ("0".Equals(prams[prams.Length - 1].Value.ToString()))
            {
                msg.IsSucess = true;
            }
            msg.Message = prams[prams.Length - 2].Value.ToString();
            return msg;
        }

        public DataSet getAllEntities(string table)
        {
            string sql = "SELECT * FROM " + table;
            return database.RunProcReturn(sql, table);
        }

        public int getSequenceTran(SqlTransaction sqlTran)
        {
            SqlParameter[] prams = {
                                   new SqlParameter("rval", SqlDbType.Int, 4)};

            prams[0].Direction = ParameterDirection.ReturnValue;

            DataSet dataset = database.RunProcReturnTran("get_sequence", prams, "tb_sequence", CommandType.StoredProcedure, sqlTran);

            return  int.Parse(prams[prams.Length - 1].Value.ToString());
        }
    }
}
