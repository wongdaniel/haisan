using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using haisan.domain;
using haisan.util;

namespace haisan.dao
{
    class PlainDaoImpl: CommonDaoImpl, PlainDao
    {

         private static Database database = Database.getInstance();
        private static BaseDao baseDao = BaseDaoImpl.getInstance();

        private static PlainDaoImpl plainDaoImpl = null;

        private PlainDaoImpl() { }

        public static PlainDaoImpl getInstance()
        {
            if (null == plainDaoImpl)
                plainDaoImpl = new PlainDaoImpl();
            return plainDaoImpl;
        }

        public MessageLocal saveOrUpdatePlain(Plain plain, string tableName)
        {
            SqlParameter[] prams = {
									database.MakeInParam("@id",  SqlDbType.Int, 0,  plain.Id),
                                    database.MakeInParam("@name", SqlDbType.NVarChar, 20, plain.Name),
                                    new SqlParameter("@message", SqlDbType.NVarChar, 20),
                                    new SqlParameter("rval", SqlDbType.Int, 4) 
			};

            prams[2].Direction = ParameterDirection.Output;
            prams[3].Direction = ParameterDirection.ReturnValue;

            return baseDao.runProcedure("saveOrUpdate_" + tableName, prams, tableName);
        }
    }
}
