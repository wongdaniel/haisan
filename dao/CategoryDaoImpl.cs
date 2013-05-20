using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using haisan.util;
using System.Windows.Forms;
using System.Data;
using System.Data.SqlClient;
using haisan.domain;

namespace haisan.dao
{
    class CategoryDaoImpl : CategoryDao
    {
        private static Database database = Database.getInstance();
        private static BaseDao baseDao = BaseDaoImpl.getInstance();

        private static CategoryDaoImpl categoryDaoImpl = null;

        private CategoryDaoImpl(){ }

        public static CategoryDaoImpl getInstance()
        {
            if(null == categoryDaoImpl)
                categoryDaoImpl = new CategoryDaoImpl();
            return categoryDaoImpl;
        }

        // 采用递归实现对category的填充，如果数据量太大，递归深度过深，将造成系统缓慢
        // table 无tb前缀
        public void fillSubCategory(TreeNode parent, string table)
        {
  
            SqlParameter[] prams = {
									database.MakeInParam("@parent",  SqlDbType.Int, 0, ((Category)parent.Tag).Id)
			};

            DataSet dataset = database.RunProcReturn("SELECT * FROM tb_" + table + " WHERE (parent = @parent) ORDER BY id", prams, "tb_categoryOfProduct");
            int count = 0, i = 0;
            if ((null != dataset) && ((count = dataset.Tables[0].Rows.Count) > 0))
            {
                while (i != count)
                {

                    Category child = new Category(int.Parse(dataset.Tables[0].Rows[i]["id"].ToString()), 
                        dataset.Tables[0].Rows[i]["name"].ToString(), (Category)parent.Tag);

                    TreeNode childNode = new TreeNode(child.Name);
                    childNode.Tag = child;
                    parent.Nodes.Add(childNode);
                    fillSubCategory(childNode, table);
                    i++;
                }
            }

            return;
        }

        public MessageLocal saveCategory(string name, Category parent, string table)
        {
            SqlParameter[] prams = {
									database.MakeInParam("@parent",  SqlDbType.Int, 0, parent.Id),
                                    database.MakeInParam("@name", SqlDbType.NVarChar, Category.NAME_SIZE, name),
                                    new SqlParameter("@message", SqlDbType.NVarChar, 20),
                                    new SqlParameter("rval", SqlDbType.Int, 4) 
			};

            prams[2].Direction = ParameterDirection.Output;
            prams[3].Direction = ParameterDirection.ReturnValue;

            return baseDao.runProcedure("save_" + table, prams, table);
        }

        public MessageLocal updateCategoryByName(string name, Category cur, string table)
        {
            MessageLocal msg = new MessageLocal();
            SqlParameter[] prams = {
									database.MakeInParam("@id",  SqlDbType.Int, 0, cur.Id),
                                    database.MakeInParam("@name", SqlDbType.NVarChar, Category.NAME_SIZE, name),
                                    new SqlParameter("@message", SqlDbType.NVarChar, 20),
                                    new SqlParameter("rval", SqlDbType.Int, 4) 
			};

            prams[2].Direction = ParameterDirection.Output;
            prams[3].Direction = ParameterDirection.ReturnValue;

            return baseDao.runProcedure("update_" + table, prams, table);
        }

        public Category getCategoryByName(string name, string table)
        {
            SqlParameter[] prams = {
                                    database.MakeInParam("@name", SqlDbType.NVarChar, Category.NAME_SIZE, name)
			};

            DataSet dataset = database.RunProcReturn("SELECT * FROM tb_" + table + " WHERE name = @name", prams, "tb_categoryOfProduct");
            if(null != dataset && dataset.Tables[0].Rows.Count > 0)
            {
                return new Category(int.Parse(dataset.Tables[0].Rows[0]["id"].ToString()), 
                        dataset.Tables[0].Rows[0]["name"].ToString());
            }
            return null;
        }

        public int deleteCategoryRecursive(Category cur, string table)
        {
            MessageLocal msg = new MessageLocal();
            SqlParameter[] prams = {
									database.MakeInParam("@id",  SqlDbType.Int, 0, cur.Id),
			};

            return database.RunProc("del_" + table, prams, CommandType.StoredProcedure);

        }

    }
}
