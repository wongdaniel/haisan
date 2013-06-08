using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using haisan.domain;
using haisan.util;
using System.Data.SqlClient;
using System.Data;
using System.Windows.Forms;

namespace haisan.dao
{
    class DepartmentDaoImpl : CommonDaoImpl, DepartmentDao
    {

        private static Database database = Database.getInstance();
        private static BaseDao baseDao = BaseDaoImpl.getInstance();

        private static DepartmentDaoImpl departmentDaoImpl = null;

        private DepartmentDaoImpl() 
        { 
        }

        public static DepartmentDaoImpl getInstance()
        {
            if (null == departmentDaoImpl)
                departmentDaoImpl = new DepartmentDaoImpl();
            return departmentDaoImpl;
        }

        public Department getDepartmentById(int id)
        {
            Department department = new Department(id);
            SqlParameter[] prams = {
                                    database.MakeInParam("@id", SqlDbType.Int, 0, id)
			};
            try
            {
                DataSet dataset = database.RunProcReturn("get_departmentById", prams, "tb_department", CommandType.StoredProcedure);
                if (null != dataset && dataset.Tables[0].Rows.Count > 0)
                {
                    department.Name = getValue(dataset, "name");
                    department.Description = getValue(dataset, "description");
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
            return department;
        }

        public MessageLocal saveOrUpdateDepartment(Department department)
        {
            MessageLocal msg = new MessageLocal();
            SqlParameter[] prams = constructParamsForDepartment(department);

            return baseDao.runProcedure("saveOrUpdate_department", prams, "department");
        }

        public SqlParameter[] constructParamsForDepartment(Department department)
        {
            MessageLocal msg = new MessageLocal();

            SqlParameter[] prams = {
									database.MakeInParam("@id",  SqlDbType.Int, 0, department.Id),
                                    database.MakeInParam("@name",  SqlDbType.VarChar, 20, department.Name),
                                    database.MakeInParam("@description",  SqlDbType.VarChar, 100, department.Description),
                                    new SqlParameter("@message", SqlDbType.NVarChar, 50),
                                    new SqlParameter("rval", SqlDbType.Int, 4) 
			};
            prams[prams.Length - 2].Direction = ParameterDirection.Output;
            prams[prams.Length - 1].Direction = ParameterDirection.ReturnValue;

            return prams;
        }

    }
}
