using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using haisan.domain;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace haisan.dao
{
    class ModuleDaoImpl :CommonDaoImpl, ModuleDao
    {

        private static Database database = Database.getInstance();
        private static ModuleDaoImpl moduleDaoImpl = null;

        private ModuleDaoImpl() { }

        public static ModuleDaoImpl getInstance()
        {
            if (null == moduleDaoImpl)
                moduleDaoImpl = new ModuleDaoImpl();
            return moduleDaoImpl;
        }
        public Module getModuleById(int id)
        {
            Module module = new Module(id);
            SqlParameter[] prams = {
                                    database.MakeInParam("@id", SqlDbType.Int, 0, id)
			};
            try
            {
                DataSet dataset = database.RunProcReturn("SELECT * FROM tb_module WHERE id = @id", prams, "tb_module", CommandType.Text);
                if (null != dataset && dataset.Tables[0].Rows.Count > 0)
                {
                    module.Name = getValue(dataset, "name");
                    module.DisplayName = getValue(dataset, "display_name");
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
            return module;
        }

        public Module getModuleByParentId(int parent)
        {
            Module module = new Module();
            SqlParameter[] prams = {
                                    database.MakeInParam("@parent", SqlDbType.Int, 0, parent)
			};
            try
            {
                DataSet dataset = database.RunProcReturn("SELECT * FROM tb_module WHERE parent = @parent", prams, "tb_module", CommandType.Text);
                if (null != dataset && dataset.Tables[0].Rows.Count > 0)
                {
                    module.Id = int.Parse(getValue(dataset, "id"));
                    module.Name = getValue(dataset, "name");
                    module.DisplayName = getValue(dataset, "display_name");
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
            return module;
        }

        // 采用递归实现对category的填充，如果数据量太大，递归深度过深，将造成系统缓慢
        // table 无tb前缀
        public void fillSubModule(TreeNode parent, string table)
        {

            SqlParameter[] prams = {
									database.MakeInParam("@parent",  SqlDbType.Int, 0, ((Module)parent.Tag).Id)
			};

            DataSet dataset = database.RunProcReturn("SELECT * FROM tb_" + table + " WHERE (parent = @parent AND is_terminal = 0) ORDER BY [order]", prams, "tb_module");
            int count = 0, i = 0;
            if ((null != dataset) && ((count = dataset.Tables[0].Rows.Count) > 0))
            {
                while (i != count)
                {

                    Module child = new Module(int.Parse(dataset.Tables[0].Rows[i]["id"].ToString()), dataset.Tables[0].Rows[i]["name"].ToString(),
                        dataset.Tables[0].Rows[i]["display_name"].ToString(), (Module)parent.Tag);

                    TreeNode childNode = new TreeNode(child.DisplayName);
                    childNode.Tag = child;
                    parent.Nodes.Add(childNode);
                    fillSubModule(childNode, table);
                    i++;
                }
            }
            return;
        }

        public DataSet getAllPermissionByModuleId(int group, int module)
        {
            SqlParameter[] prams = {
                                            database.MakeInParam("@module",  SqlDbType.Int, 0, module),
									        database.MakeInParam("@group",  SqlDbType.Int, 0, group),
			    };

            return database.RunProcReturn("get_permissionByModule", prams, "tb_group_module", CommandType.StoredProcedure);
        }

        public  void updatePermission(Permission per){
            SqlParameter[] prams = {
                                            database.MakeInParam("@id",  SqlDbType.Int, 0, per.Id),
                                            database.MakeInParam("@query",  SqlDbType.SmallInt, 0, per.Query),
									        database.MakeInParam("@add",  SqlDbType.SmallInt, 0, per.Add),
                                            database.MakeInParam("@modify",  SqlDbType.SmallInt, 0, per.Modify),
                                            database.MakeInParam("@delete",  SqlDbType.SmallInt, 0, per.Delete),
                                            database.MakeInParam("@print",  SqlDbType.SmallInt, 0, per.Print),
                                            database.MakeInParam("@run",  SqlDbType.SmallInt, 0, per.Run),
                                            database.MakeInParam("@save_table",  SqlDbType.SmallInt, 0, per.SaveTable),
                                            database.MakeInParam("@check",  SqlDbType.SmallInt, 0, per.Check),
                                            database.MakeInParam("@anticheck",  SqlDbType.SmallInt, 0, per.Anticheck),
			    };
            string sql = "UPDATE tb_group_module SET query = @query, [add] = @add, [modify] = @modify, [delete] = @delete, "+
                "[print] = @print, run = @run, save_table = @save_table,[check] = @check, anticheck = @anticheck " +
                "WHERE id = @id";

            database.RunProc(sql, prams);
           
        }
    }
}
