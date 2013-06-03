using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using haisan.util;
using haisan.domain;

namespace haisan.dao
{
    interface  WarehouseDao
    {
        MessageLocal saveOrUpdateWarehouse(Warehouse warehouse);
        Warehouse getWarehouseById(int id);
    }
}
