using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using haisan.domain;
using System.Data;
using System.Data.SqlClient;

namespace haisan.dao
{
    class LogDaoImpl : LogDao
    {
        private static Database database = Database.getInstance();
        private static LogDaoImpl logDaoImpl = null;

        private LogDaoImpl() { }

        public static LogDaoImpl getInstance()
        {
            if (null == logDaoImpl)
                logDaoImpl = new LogDaoImpl();
            return logDaoImpl;
        }

        public void saveLog(User user, string module)
        {
            SqlParameter[] prams = {
									database.MakeInParam("@operator",  SqlDbType.NVarChar, 20, user.Username),
                                    database.MakeInParam("@time", SqlDbType.DateTime, 0, DateTime.Now),
                                    database.MakeInParam("@module", SqlDbType.NVarChar, 20, module),
			};

            database.RunProc("INSERT INTO tb_log(operator, [time], module) VALUES(@operator,@time,@module)", prams);
        }
    }
}
