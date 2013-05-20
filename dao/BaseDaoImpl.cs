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

            try
            {
                //上锁
                int tableID = getTableIDByName(table);
                if (tableID < 0)
                {
                    msg.Message = "该表格的信息已经被删除，无法更改";
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
                    database.RunProc("saveOrUpdate_table", prams, CommandType.StoredProcedure);
                }
                //去锁
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
                msg.Message = "在保存表格样式时，出现错误！";
                return msg;
            }
            msg.IsSucess = true;
            return msg;
        }

        public MessageLocal fillDataGridView(User user, string table, DataGridView dataGridView)
        {
             MessageLocal msg = new MessageLocal();

            try
            {
                //上锁
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
                DataSet dataset = database.RunProcReturn("SELECT * FROM tb_table_style WHERE ([user] = @user) AND ([table] = @table)", prams, "tb_table_style");
               
                int count = 0;
                if (null != dataset && (count = dataset.Tables[0].Rows.Count) > 0)
                {
                    int i = 0;
                    for (i = 0; i != count; i++)
                    {
                        int index = int.Parse(dataset.Tables[0].Rows[i]["original_index"].ToString());
                        dataGridView.Columns[index].DisplayIndex = int.Parse(dataset.Tables[0].Rows[i]["display_index"].ToString());
                        dataGridView.Columns[index].Width =  int.Parse(dataset.Tables[0].Rows[i]["width"].ToString());
                    //    dataGridView.Columns[index].sort = int.Parse(dataset.Tables[0].Rows[0]["sort"].ToString());
                    }
                }

                //去锁
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
                msg.Message = "在读取表格样式时，出现错误！";
                return msg;
            }
            msg.IsSucess = true;
            return msg;
        }

        private int getTableIDByName(string table)
        {
            MessageLocal msg = new MessageLocal();
            SqlParameter[] prams = {
									    database.MakeInParam("@name",  SqlDbType.VarChar, 50, table)
			};

            DataSet dataset = database.RunProcReturn("SELECT id FROM tb_table WHERE (name = @name)", prams, "tb_table");
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
    }
}
