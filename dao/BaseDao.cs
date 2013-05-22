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
    interface BaseDao
    {
        MessageLocal saveOrUpdateDataGridView(User user, string table, DataGridView dataGridView);

        //从数据库中读取dataGridView的显示样式
        MessageLocal fillDataGridView(User user, string table, DataGridView dataGridView);
        MessageLocal runProcedure(string procedure, SqlParameter[] prams, string table);
        MessageLocal deleteEntities(string table, string ids);

        //@table is the true table name
        DataSet getAllEntities(string table);
    }
}
