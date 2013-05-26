using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using haisan.util;
using haisan.domain;
using System.Data.SqlClient;
using System.Data;

namespace haisan.dao
{
    class TypeOfProcessDaoImpl : CommonDaoImpl, TypeOfProcessDao
    {
        private static Database database = Database.getInstance();
        private static BaseDao baseDao = BaseDaoImpl.getInstance();

        private static TypeOfProcessDaoImpl typeOfProcessDaoImpl = null;

        private TypeOfProcessDaoImpl() { }

        public static TypeOfProcessDaoImpl getInstance()
        {
            if (null == typeOfProcessDaoImpl)
                typeOfProcessDaoImpl = new TypeOfProcessDaoImpl();
            return typeOfProcessDaoImpl;
        }

        public MessageLocal saveOrUpdateTypeOfProcess(TypeOfProcess typeOfProcess)
        {
            SqlParameter[] prams = {
									database.MakeInParam("@id",  SqlDbType.Int, 0,  typeOfProcess.Id),
                                    database.MakeInParam("@name", SqlDbType.NVarChar, 20, typeOfProcess.Name),
                                    database.MakeInParam("@unit", SqlDbType.NVarChar, 10, typeOfProcess.Unit),
                                    new SqlParameter("@message", SqlDbType.NVarChar, 20),
                                    new SqlParameter("rval", SqlDbType.Int, 4) 
			};

            prams[prams.Length - 2].Direction = ParameterDirection.Output;
            prams[prams.Length - 1].Direction = ParameterDirection.ReturnValue;

            return baseDao.runProcedure("saveOrUpdate_tb_typeOfProcess", prams, "tb_typeOfProcess");
        }
    }
}
