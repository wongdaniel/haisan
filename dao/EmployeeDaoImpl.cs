using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using haisan.domain;
using haisan.util;
using System.Windows.Forms;

namespace haisan.dao
{
    class EmployeeDaoImpl : CommonDaoImpl, EmployeeDao
    {
        private static Database database = Database.getInstance();
        private static BaseDao baseDao = BaseDaoImpl.getInstance();

        private static EmployeeDaoImpl employeeDaoImpl = null;

        private EmployeeDaoImpl() 
        { 
        }
        public static EmployeeDaoImpl getInstance()
        {
            if (null == employeeDaoImpl)
                employeeDaoImpl = new EmployeeDaoImpl();
            return employeeDaoImpl;
        }

        public DataSet getAllEmployee(string queryStr)
        {
            string sql = "SELECT te.id, te.name, te.code, te.sex, te.birthday,te.id_card, te.phone, te.join_date, " +
                            "te.contract_date, td.name AS department, te.position, te.address, te.is_locked " +
                            "FROM tb_employee te, tb_department td " + 
                            "WHERE (te.id LIKE @queryStr OR te.name LIKE @queryStr OR te.code LIKE @queryStr OR td.name LIKE @queryStr) AND te.department = td.id";

            SqlParameter[] prams = {
                                    database.MakeInParam("@queryStr", SqlDbType.NVarChar, 20, "%" + queryStr + "%")
			};

            return database.RunProcReturn(sql, prams, "tb_employee");
        }

        public MessageLocal saveOrUpdateEmployee(Employee employee)
        {
            MessageLocal msg = new MessageLocal();
            SqlParameter[] prams = constructParamsForEmployee(employee);

            return baseDao.runProcedure("saveOrUpdate_employee", prams, "employee");
        }

        public Employee getEmployeeById(int id)
        {
            Employee employee = new Employee(id);
            SqlParameter[] prams = {
                                    database.MakeInParam("@id", SqlDbType.Int, 0, id)
			};

            string sql = "SELECT te.id, te.name, te.code, te.sex, te.birthday,te.id_card, te.phone, te.join_date, " +
                "te.contract_date, te.department, td.name AS department_name, te.position, te.address, te.is_locked " +
                "FROM tb_employee te, tb_department td " +
                "WHERE te.id = @id AND te.department = td.id";

            try
            {
                DataSet dataset = database.RunProcReturn(sql, prams, "tb_employee", CommandType.Text);
                if (null != dataset && dataset.Tables[0].Rows.Count > 0)
                {
                    int index = 0;

                    employee.Name = getValue(dataset, "name");
                    employee.Code = getValue(dataset, "code");
                    employee.Sex = getBoolValue(dataset, index, "sex");
                    employee.Birthday = getDateTime(dataset, index, "birthday");
                    employee.IdCard = getValue(dataset, "id_card");
                    employee.Phone = getValue(dataset, "phone");
                    employee.JoinDate = getDateTime(dataset, index, "join_date");
                    employee.ContractDate = getDateTime(dataset, index, "contract_date");
                    employee.Department = new Department(getIntValue(dataset, index, "department"), getValue(dataset, "department_name"));
                    employee.Position = getValue(dataset, index, "position");
                    employee.Address = getValue(dataset, index, "address");
                    employee.IsLocked = getBoolValue(dataset, index, "is_locked");
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
            return employee;
        }


        SqlParameter[] constructParamsForEmployee(Employee employee)
        {
            MessageLocal msg = new MessageLocal();

            SqlParameter[] prams = {
									database.MakeInParam("@id",  SqlDbType.Int, 0, employee.Id),
                                    database.MakeInParam("@name",  SqlDbType.NVarChar, 20, employee.Name),
                                    database.MakeInParam("@code",  SqlDbType.VarChar, 20, employee.Code),
                                    database.MakeInParam("@sex",  SqlDbType.Bit, 0, employee.Sex),
                                    database.MakeInParam("@birthday",  SqlDbType.DateTime, 0, employee.Birthday),
                                    database.MakeInParam("@id_card",  SqlDbType.VarChar, 20, employee.IdCard),
                                    database.MakeInParam("@phone",  SqlDbType.VarChar, 20, employee.Phone),
                                    database.MakeInParam("@join_date",  SqlDbType.DateTime, 0, employee.JoinDate),
                                    database.MakeInParam("@contract_date",  SqlDbType.DateTime, 0, employee.ContractDate),
                                    database.MakeInParam("@department",  SqlDbType.Int, 0, employee.Department.Id),
                                    database.MakeInParam("@position",  SqlDbType.NVarChar, 20, employee.Position),
                                    database.MakeInParam("@address",  SqlDbType.NVarChar, 50, employee.Address),
                                    database.MakeInParam("@is_locked",  SqlDbType.Bit, 0, employee.IsLocked),
                                    new SqlParameter("@message", SqlDbType.NVarChar, 50),
                                    new SqlParameter("rval", SqlDbType.Int, 4) 
			};
            prams[prams.Length - 2].Direction = ParameterDirection.Output;
            prams[prams.Length - 1].Direction = ParameterDirection.ReturnValue;

            return prams;
        }

    }
}
