using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using haisan.domain;
using haisan.util;

namespace haisan.dao
{
    interface XialiaoOrderDao
    {
        LinkedList<Order> getAllOrders(Company company);
        XialiaoOrder getXialiaoOrderByOrderId(int id);
        XialiaoOrder getXialiaoOrderById(int id);
        MessageLocal saveOrUpdateXialiaoOrder(XialiaoOrder xialiaoOrder);
        DataSet getXialiaoOrder(string sn, string customName, DateTime begin, DateTime end);
    }
}
