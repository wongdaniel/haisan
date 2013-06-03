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
    class WarehouseDaoImpl : CommonDaoImpl, WarehouseDao
    {

        private static Database database = Database.getInstance();
        private static BaseDao baseDao = BaseDaoImpl.getInstance();

        private static WarehouseDaoImpl warehouseDaoImpl = null;

        private WarehouseDaoImpl() 
        { 
        }

        public static WarehouseDaoImpl getInstance()
        {
            if (null == warehouseDaoImpl)
                warehouseDaoImpl = new WarehouseDaoImpl();
            return warehouseDaoImpl;
        }

        public Warehouse getWarehouseById(int id)
        {
            Warehouse warehouse = new Warehouse(id);
            SqlParameter[] prams = {
                                    database.MakeInParam("@id", SqlDbType.Int, 0, id)
			};
            try
            {
                DataSet dataset = database.RunProcReturn("get_warehouseById", prams, "tb_warehouse", CommandType.StoredProcedure);
                if (null != dataset && dataset.Tables[0].Rows.Count > 0)
                {
                    warehouse.Name = getValue(dataset, "name");
                    warehouse.Description = getValue(dataset, "description");
                    warehouse.IsLocked = bool.Parse(getValue(dataset, "is_locked"));
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
            return warehouse;
        }

        public MessageLocal saveOrUpdateWarehouse(Warehouse warehouse)
        {
            MessageLocal msg = new MessageLocal();
            SqlParameter[] prams = constructParamsForWarehouse(warehouse);

            return baseDao.runProcedure("saveOrUpdate_warehouse", prams, "warehouse");
        }

        public SqlParameter[] constructParamsForWarehouse(Warehouse warehouse)
        {
            MessageLocal msg = new MessageLocal();

            SqlParameter[] prams = {
									database.MakeInParam("@id",  SqlDbType.Int, 0, warehouse.Id),
                                    database.MakeInParam("@name",  SqlDbType.VarChar, 20, warehouse.Name),
                                    database.MakeInParam("@description",  SqlDbType.VarChar, 50, warehouse.Description),
                                    database.MakeInParam("@is_locked",  SqlDbType.Bit, 0, warehouse.IsLocked),
                                    new SqlParameter("@message", SqlDbType.NVarChar, 50),
                                    new SqlParameter("rval", SqlDbType.Int, 4) 
			};
            prams[prams.Length - 2].Direction = ParameterDirection.Output;
            prams[prams.Length - 1].Direction = ParameterDirection.ReturnValue;

            return prams;
        }

    }
}
