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

        public DataSet getLog(string module, string oper, DateTime begin, DateTime end)
        {
            string condition = "";
            LinkedList<SqlParameter> prams = new LinkedList<SqlParameter>();
            if (!"".Equals(oper))
            {
                prams.AddLast(database.MakeInParam("@operator", SqlDbType.NVarChar, 20, oper));
                condition = "operator = @operator AND ";
            }
            prams.AddLast(database.MakeInParam("@module", SqlDbType.NVarChar, 20, "%" + module + "%"));
            prams.AddLast(database.MakeInParam("@begin", SqlDbType.DateTime, 0, begin));
            prams.AddLast(database.MakeInParam("@end", SqlDbType.DateTime, 0, end));

            return database.RunProcReturn("SELECT * FROM tb_log WHERE (" + condition + "module LIKE @module AND " +
                   "([time] BETWEEN @begin AND @end))", prams.ToArray<SqlParameter>(), "tb_log");
        }

        public void delLog(string module, string oper, DateTime begin, DateTime end)
        {
            string condition = "";
            LinkedList<SqlParameter> prams = new LinkedList<SqlParameter>();
            if (!"".Equals(oper))
            {
                prams.AddLast(database.MakeInParam("@operator", SqlDbType.NVarChar, 20, oper));
                condition = "operator = @operator AND ";
            }
            prams.AddLast(database.MakeInParam("@module", SqlDbType.NVarChar, 20, "%" + module + "%"));
            prams.AddLast(database.MakeInParam("@begin", SqlDbType.DateTime, 0, begin));
            prams.AddLast(database.MakeInParam("@end", SqlDbType.DateTime, 0, end));

            database.RunProc("DELETE FROM tb_log WHERE (" + condition + "module LIKE @module AND " +
                   "([time] BETWEEN @begin AND @end))", prams.ToArray<SqlParameter>(), CommandType.Text);
        }

    }
}
