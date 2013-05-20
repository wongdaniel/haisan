using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using haisan.util;
using haisan.domain;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace haisan.dao
{
    interface BaseDao
    {
        MessageLocal saveOrUpdateDataGridView(User user, string table, DataGridView dataGridView);
        MessageLocal fillDataGridView(User user, string table, DataGridView dataGridView);
        MessageLocal runProcedure(string procedure, SqlParameter[] prams, string table);
        MessageLocal deleteEntities(string table, string ids);
    }
}
