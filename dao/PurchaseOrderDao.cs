using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using haisan.frame.pdm.purchase;
using haisan.util;
using haisan.domain;
using System.Data;

namespace haisan.dao
{
    interface PurchaseOrderDao
    {
        MessageLocal saveOrUpdatePurchaseOrder(Order order);
        DataSet getPurchaseOrder(string sn, string customName, DateTime begin, DateTime end);
        Order getOrderById(int id);
        DataSet getOrderItems(Order order);
        DataSet getOrderStats(Order order);
        Order loadOrderById(int id);
        OrderItem parseOrderItem(DataSet dataset, int index);
        void fillOrderStats(Order order);

    }
}
