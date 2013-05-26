using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using haisan.util;
using haisan.domain;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace haisan.dao
{
    class GroupDaoImpl : CommonDaoImpl, GroupDao
    {

        private static Database database = Database.getInstance();
        private static BaseDao baseDao = BaseDaoImpl.getInstance();

        private static GroupDaoImpl groupDaoImpl = null;

        private GroupDaoImpl() { }

        public static GroupDaoImpl getInstance()
        {
            if (null == groupDaoImpl)
                groupDaoImpl = new GroupDaoImpl();
            return groupDaoImpl;
        }

        public MessageLocal saveOrUpdateGroup(Group group)
        {
            SqlParameter[] prams = {
									database.MakeInParam("@id",  SqlDbType.Int, 0, group.Id),
                                    database.MakeInParam("@name", SqlDbType.NVarChar, 20, group.Name),
                                    new SqlParameter("@message", SqlDbType.NVarChar, 20),
                                    new SqlParameter("rval", SqlDbType.Int, 4) 
			};

            prams[2].Direction = ParameterDirection.Output;
            prams[3].Direction = ParameterDirection.ReturnValue;

            return baseDao.runProcedure("saveOrUpdate_group", prams, "group");
        }

        public Dictionary<Module, Permission> getAllPermissions(Group group)
        {
            Dictionary<Module, Permission> permissions = new Dictionary<Module, Permission>(new ModuleComparer());

            SqlParameter[] prams = {
									database.MakeInParam("@group",  SqlDbType.Int, 0, group.Id)
			};

            DataSet dataset = database.RunProcReturn("get_permissionByGroup", prams, "tb_group_module", CommandType.StoredProcedure);

            int count = 0;
            if (null != dataset && (count = dataset.Tables[0].Rows.Count) > 0)
            {
                int i = 0;
                for (i = 0; i < count; i++)
                {
                    Module module = new Module();
                    module.Name = getValue(dataset, i, "name");

                    Permission permission = new Permission();
                    permission.Id = getIntValue(dataset, i, "id");
                    permission.Query = getShortValue(dataset, i, "query");
                    permission.Add = getShortValue(dataset, i, "add");
                    permission.Modify = getShortValue(dataset, i, "modify");
                    permission.Delete = getShortValue(dataset, i, "delete");
                    permission.Print = getShortValue(dataset, i, "print");
                    permission.Run = getShortValue(dataset, i, "run");
                    permission.SaveTable = getShortValue(dataset, i, "save_table");
                    permission.Check = getShortValue(dataset, i, "check");
                    permission.Anticheck = getShortValue(dataset, i, "anticheck");

                    permissions.Add(module, permission);
                }
            }

            return permissions;
        }
      
    }
}
