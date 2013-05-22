using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;

namespace haisan.dao
{
    class ConfigureDaoImpl : ConfigureDao
    {
        private static Database database = Database.getInstance();
        private static ConfigureDaoImpl configureDaoImpl = null;

        private ConfigureDaoImpl() { }

        public static ConfigureDaoImpl getInstance()
        {
            if (null == configureDaoImpl)
                configureDaoImpl = new ConfigureDaoImpl();
            return configureDaoImpl;
        }
        public string getValueByName(string name)
        {
            SqlParameter[] prams = {
									    database.MakeInParam("@name",  SqlDbType.VarChar, 20, name)
			};

            DataSet dataset = database.RunProcReturn("SELECT [value] FROM tb_configure WHERE ([name] = @name)", prams, "tb_configure");
            if (null != dataset && dataset.Tables[0].Rows.Count > 0)
            {
                return dataset.Tables[0].Rows[0]["value"].ToString();
            }

            return "";
        }

        public int saveOrUpdateValueByName(string name, string value)
        {
            SqlParameter[] prams = {
									    database.MakeInParam("@name",  SqlDbType.VarChar, 20, name),
                                        database.MakeInParam("@value",  SqlDbType.NVarChar, 50, value)
			};

           return database.RunProc("saveOrUpdate_configure", prams, CommandType.StoredProcedure);
        }
    }
}
