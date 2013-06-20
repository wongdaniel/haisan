using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using haisan.domain;
using System.Data;
using haisan.util;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Drawing;

namespace haisan.dao
{
    class XialiaoOrderDaoImpl : CommonDaoImpl, XialiaoOrderDao
    {
        private static Database database = Database.getInstance();
        private static BaseDao baseDao = BaseDaoImpl.getInstance();
        private static PurchaseOrderDao purchaseOrderDao = PurchaseOrderDaoImpl.getInstance();
        private static UserDao userDao = UserDaoImpl.getInstance();

        private static XialiaoOrderDaoImpl xialiaoOrderDaoImpl = null;

       // private static readonly string SN_FORMAT = "{0:00000}";

        private XialiaoOrderDaoImpl() { }

        public static XialiaoOrderDaoImpl getInstance()
        {
            if (null == xialiaoOrderDaoImpl)
                xialiaoOrderDaoImpl = new XialiaoOrderDaoImpl();

            return xialiaoOrderDaoImpl;
        }

        public LinkedList<Order> getAllOrders(Company company)
        {
            LinkedList<Order> orders = new LinkedList<Order>();

            string sql = "SELECT id, sn FROM tb_order WHERE company = " + company.Id;
            try
            {
                DataSet dataset = database.RunProcReturn(sql, "tb_order");
                int count = 0;
                if (null != dataset && (count = dataset.Tables[0].Rows.Count) > 0)
                {
                    int i = 0;
                    for (i = 0; i < count; i++)
                    {
                        Order order = new Order(getIntValue(dataset, i, "id"));
                        order.Sn = getValue(dataset, i, "sn");
                        orders.AddLast(order);
                    }
                }
            }
            catch (Exception e)
            {
                Util.showError(e.Message);
                Console.WriteLine("178 xialiaoDaoImpl: " + e.StackTrace);
            }

            return orders;
        }

        public XialiaoOrder getXialiaoOrderById(int id)
        {
            XialiaoOrder xialiaoOrder = loadXialiaoOrderById(id);
            if (null == xialiaoOrder)
                return null;

            fillXialiaoOrderItemsForGET(xialiaoOrder);
            fillXialiaoOrderStatsForGET(xialiaoOrder);

            return xialiaoOrder;
        }

        public XialiaoOrder getXialiaoOrderByOrderId(int id)
        {
            XialiaoOrder xialiaoOrder = loadXialiaoOrderByOrderId(id);
            if (null == xialiaoOrder)
                return null;
            fillXialiaoOrderItems(xialiaoOrder);
            fillXialiaoOrderStats(xialiaoOrder);


            return xialiaoOrder;
        }

        public XialiaoOrder loadXialiaoOrderByOrderId(int id)
        {
            XialiaoOrder xialiaoOrder = new XialiaoOrder();
            try
            {
                Order order = purchaseOrderDao.loadOrderById(id);
                xialiaoOrder.Order = order;
                xialiaoOrder.CreateDate = DateTime.Now;
                xialiaoOrder.Operatr = Parameter.user;

                // 需要重新初始化
                xialiaoOrder.TotalPackages = 0;
                xialiaoOrder.Payment = 0;
                xialiaoOrder.ProcessingCharges = 0;
                xialiaoOrder.TotalCost = 0;
                xialiaoOrder.Status = Parameter.XIALIAO_STATUS_INIT;
                
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }

            return xialiaoOrder;
        }

        public XialiaoOrder loadXialiaoOrderById(int id)
        {
            XialiaoOrder xialiaoOrder = new XialiaoOrder();
            string sql = "SELECT * FROM tb_xialiao_order where id = " + id;
            try
            {
                DataSet dataset = database.RunProcReturn(sql, "tb_xialiao_order");
                int count = 0;
                if (null != dataset && (count = dataset.Tables[0].Rows.Count) > 0)
                {
                    int index = 0;
                    xialiaoOrder.Id = getIntValue(dataset, index, "id");
                    xialiaoOrder.Sn = getValue(dataset, index, "sn");
                    xialiaoOrder.Order = purchaseOrderDao.loadOrderById(getIntValue(dataset, index, "order_id"));
                    xialiaoOrder.CreateDate = getDateTime(dataset, index, "createDate");
                    xialiaoOrder.Operatr = userDao.getUserById(getIntValue(dataset, index, "operator"));

                    xialiaoOrder.TotalPackages = getIntValue(dataset, index, "total_packages");
                    xialiaoOrder.TotalNumber = getDecimalValue(dataset, index, "total_number");
                    xialiaoOrder.Payment = getDecimalValue(dataset, index, "payment");
                    xialiaoOrder.ProcessingCharges = getDecimalValue(dataset, index, "processing_charges");
                    xialiaoOrder.TotalCost = getDecimalValue(dataset, index, "total_cost");
                    xialiaoOrder.Status = getIntValue(dataset, index, "status");
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }

            return xialiaoOrder;
        }


        private void fillXialiaoOrderItems(XialiaoOrder xialiaoOrder)
        {
            string sql = "SELECT * FROM tb_order_item_view AS toiv LEFT JOIN " +
                "(SELECT sum(use_package) AS total_use_package, order_item_id FROM tb_xialiao_order_item GROUP BY order_item_id, order_item_id) AS txoi " +
                "ON toiv.id = txoi.order_item_id WHERE toiv.[order] = " + xialiaoOrder.Order.Id;

            try
            {
                DataSet dataset = database.RunProcReturn(sql, "tb_xialiao_order_item_view");
                int count = 0;
                if (null != dataset && (count = dataset.Tables[0].Rows.Count) > 0)
                {
                    int i = 0;
                    for (i = 0; i < count; i++)
                    {
                        OrderItem item = purchaseOrderDao.parseOrderItem(dataset, i);
                        item.Order = xialiaoOrder.Order;

                        XialiaoOrderItem xialiaoOrderItem = new XialiaoOrderItem();
                        xialiaoOrderItem.OrderItem = item;

                        xialiaoOrderItem.OriginalPackage = item.Package;

                        xialiaoOrderItem.RemainPackage = item.Package - getIntValue(dataset, i, "total_use_package");
                        xialiaoOrderItem.UsePackage = xialiaoOrderItem.RemainPackage; //默认全部下完

                        xialiaoOrderItem.WorkingNumber1 = computeWorkingNumberX(xialiaoOrderItem, xialiaoOrderItem.OrderItem.WorkingName1, xialiaoOrderItem.OrderItem.WorkingDiagram1);
                        xialiaoOrderItem.WorkingNumber2 = computeWorkingNumberX(xialiaoOrderItem, xialiaoOrderItem.OrderItem.WorkingName2, xialiaoOrderItem.OrderItem.WorkingDiagram2);
                        xialiaoOrderItem.WorkingNumber3 = computeWorkingNumberX(xialiaoOrderItem, xialiaoOrderItem.OrderItem.WorkingName3, xialiaoOrderItem.OrderItem.WorkingDiagram3);

                        xialiaoOrder.XialiaoOrderItems.AddLast(xialiaoOrderItem);
                    }
                }
            }
            catch (Exception e)
            {
                Util.showError(e.Message);
                Console.WriteLine("178 purchaseDaoImpl: " + e.StackTrace);
            }

        }


        private void fillXialiaoOrderItemsForGET(XialiaoOrder xialiaoOrder)
        {
            string sql = "SELECT * FROM tb_xialiao_order_item WHERE xialiao_order_id = " + xialiaoOrder.Id;

            try
            {
                DataSet dataset = database.RunProcReturn(sql, "tb_xialiao_order_item");
                int count = 0;
                if (null != dataset && (count = dataset.Tables[0].Rows.Count) > 0)
                {
                    int i = 0;
                    for (i = 0; i < count; i++)
                    {
                        OrderItem item = purchaseOrderDao.getOrderItemById(getIntValue(dataset, i, "order_item_id"));
                        item.Order = xialiaoOrder.Order;

                        XialiaoOrderItem xialiaoOrderItem = new XialiaoOrderItem(getIntValue(dataset, i, "id"));
                        xialiaoOrderItem.OrderItem = item;

                        xialiaoOrderItem.OriginalPackage = getIntValue(dataset, i, "original_package");

                        xialiaoOrderItem.RemainPackage = getIntValue(dataset, i, "remain_package");
                        xialiaoOrderItem.UsePackage = getIntValue(dataset, i, "use_package"); 

                        xialiaoOrderItem.WorkingNumber1 = getDecimalValue(dataset, i, "working_number_1");
                        xialiaoOrderItem.WorkingNumber2 = getDecimalValue(dataset, i, "working_number_2");
                        xialiaoOrderItem.WorkingNumber3 = getDecimalValue(dataset, i, "working_number_3");

                        xialiaoOrder.XialiaoOrderItems.AddLast(xialiaoOrderItem);
                    }
                }
            }
            catch (Exception e)
            {
                Util.showError(e.Message);
                Console.WriteLine("178 purchaseDaoImpl: " + e.StackTrace);
            }

        }


        private decimal computeWorkingNumberX(XialiaoOrderItem xialiaoOrderItem, TypeOfProcess workingNameX, ProcessingImage workingDiagramX)
        {
            decimal value = 0;

            OrderItem orderItem =  xialiaoOrderItem.OrderItem;
            if (null == workingNameX || null == workingDiagramX)
            {
                return 0;
            }
            else
            {
                TypeOfProcess typePro = workingNameX;
                if (Parameter.PACKAGE.Equals(typePro.Unit))
                {
                    value = orderItem.Package;
                }
                else if (Parameter.METER.Equals(typePro.Unit))
                {
                    ProcessingImage proImage = workingDiagramX;
                    int length = 0, width = 0, package = 0;
                    length = Util.getValueOfLWT2(orderItem.Length);
                    width = Util.getValueOfLWT2(orderItem.Width);
                    package = xialiaoOrderItem.UsePackage;

                    decimal sum = 0;
                    if (proImage.Up)
                        sum += ((decimal)length) / 1000 * package;
                    if (proImage.Down)
                        sum += ((decimal)length) / 1000 * package;
                    if (proImage.Left)
                        sum += ((decimal)width) / 1000 * package;
                    if (proImage.Right)
                        sum += ((decimal)width) / 1000 * package;
                    value = decimal.Round(sum, Parameter.NUMBER_MANTISSA);

                }
                else
                {
                    MessageBox.Show("不认识的加工类型", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }   
                return value;
        }

        private void fillXialiaoOrderStats(XialiaoOrder xialiaoOrder)
        {
            purchaseOrderDao.fillOrderStats(xialiaoOrder.Order);
            foreach (OrderStats stat in xialiaoOrder.Order.OrderStats)
            {
                XialiaoOrderStats stats = new XialiaoOrderStats();
                stats.XialiaoOrder = xialiaoOrder;
                stats.OrderStats = stat;

                decimal totalNumber = 0;
                foreach (XialiaoOrderItem item in xialiaoOrder.XialiaoOrderItems)
                {
                    if(item.OrderItem.Thickness.Equals(stat.ThicknessStats))
                    {
                        if (null != item.OrderItem.WorkingName1 && item.OrderItem.WorkingName1.Id == stat.TypeOfProcess.Id)
                            totalNumber += item.WorkingNumber1;

                        if (null != item.OrderItem.WorkingName2 && item.OrderItem.WorkingName2.Id == stat.TypeOfProcess.Id)
                            totalNumber += item.WorkingNumber2;

                        if (null != item.OrderItem.WorkingName3 && item.OrderItem.WorkingName3.Id == stat.TypeOfProcess.Id)
                            totalNumber += item.WorkingNumber3;
                    }
                }
                stats.TotalNumber = totalNumber;
                stats.AmountOfMoney = totalNumber * stats.OrderStats.UnitPrice;
                xialiaoOrder.XialiaoOrderstats.AddLast(stats);
            }
            
        }

        private void fillXialiaoOrderStatsForGET(XialiaoOrder xialiaoOrder)
        {
            
            string sql = "SELECT * FROM tb_xialiao_order_stats WHERE xialiao_order_id = " + xialiaoOrder.Id;

            try
            {
                DataSet dataset = database.RunProcReturn(sql, "tb_xialiao_order_stats");
                int count = 0;
                if (null != dataset && (count = dataset.Tables[0].Rows.Count) > 0)
                {
                    int i = 0;
                    for (i = 0; i < count; i++)
                    {

                        XialiaoOrderStats stats = new XialiaoOrderStats(getIntValue(dataset, i, "id"));
                        stats.XialiaoOrder = xialiaoOrder;
                        stats.OrderStats = purchaseOrderDao.getOrderStatsById(getIntValue(dataset, i, "order_stats_id"));

                        stats.TotalNumber = getDecimalValue(dataset, i, "total_number");
                        stats.AmountOfMoney = getDecimalValue(dataset, i, "amount_of_money");
                        xialiaoOrder.XialiaoOrderstats.AddLast(stats);
                    }
                }
            }
            catch (Exception e)
            {
                Util.showError(e.Message);
                Console.WriteLine("178 purchaseDaoImpl: " + e.StackTrace);
            }

        }

        public MessageLocal saveOrUpdateXialiaoOrder(XialiaoOrder xialiaoOrder)
        {
            MessageLocal msg = new MessageLocal();

            SqlConnection con = SessionFactory.getConnection();
            SqlTransaction sqlTran = con.BeginTransaction(IsolationLevel.RepeatableRead);
            LinkedList<SqlParameter> prams = new LinkedList<SqlParameter>();

            try
            {
                msg = baseDao.runProcedureTran("saveOrUpdate_xialiao_order", constructParamsForXialiaoOrder(xialiaoOrder), "xialiao_order", sqlTran);
                if (!msg.IsSucess)
                    throw new Exception(msg.Message);

                // 如果是新增的下料订单，需要更新ID
                string sn = msg.Message;
                if (xialiaoOrder.Id <= 0)
                {
                    DataSet dataset = database.RunProcReturnTran("SELECT id FROM tb_xialiao_order WHERE sn = '" + sn + "'", "tb_xialiao_order", CommandType.Text, sqlTran);
                    if (null != dataset && dataset.Tables[0].Rows.Count > 0)
                    {
                        xialiaoOrder.Id = int.Parse(getValue(dataset, "id"));
                        xialiaoOrder.Sn = sn;
                    }
                    else
                    {
                        throw new Exception("查询订单编号【" + xialiaoOrder.Sn + "】失败！");
                    }
                }

                foreach (XialiaoOrderItem item in xialiaoOrder.XialiaoOrderItems)
                {
                    msg = baseDao.runProcedureTran("saveOrUpdate_xialiao_order_item", constructParamsForXialiaoOrderItem(item), "order_item", sqlTran);
                    if (!msg.IsSucess)
                        throw new Exception(msg.Message);
                    item.Id = int.Parse(msg.Message);
                }

                foreach (XialiaoOrderStats stats in xialiaoOrder.XialiaoOrderstats)
                {
                    msg = baseDao.runProcedureTran("saveOrUpdate_xialiao_order_stats", constructParamsForXialiaoOrderStats(stats), "order_stats", sqlTran);
                    if (!msg.IsSucess)
                        throw new Exception(msg.Message);
                    stats.Id = int.Parse(msg.Message);
                }
                sqlTran.Commit();

            }
            catch (Exception e)
            {
                sqlTran.Rollback();
                msg.Message = "保存失败！" + e.Message;
                msg.IsSucess = false;
                Console.WriteLine(e.StackTrace);
                return msg;
            }
            finally
            {
                con.Close();
            }
            msg.IsSucess = true;

            return msg;
        }

        public DataSet getXialiaoOrder(string sn, string customName, DateTime begin, DateTime end)
        {
            string sql = "SELECT * FROM tb_xialiao_order_view WHERE (sn LIKE @sn AND name LIKE @name AND " +
                   "(createDate BETWEEN @begin AND @end))";

            LinkedList<SqlParameter> prams = new LinkedList<SqlParameter>();

            prams.AddLast(database.MakeInParam("@sn", SqlDbType.NVarChar, 20, "%" + sn + "%"));
            prams.AddLast(database.MakeInParam("@name", SqlDbType.NVarChar, 20, "%" + customName + "%"));
            prams.AddLast(database.MakeInParam("@begin", SqlDbType.DateTime, 0, begin));
            prams.AddLast(database.MakeInParam("@end", SqlDbType.DateTime, 0, end));

            return database.RunProcReturn(sql, prams.ToArray<SqlParameter>(), "tb_xialiao_order_view");
        }


        private SqlParameter[] constructParamsForXialiaoOrder(XialiaoOrder xialiaoOrder)
        {
            SqlParameter[] prams = { 
                                     database.MakeInParam("@id", SqlDbType.Int, 0, xialiaoOrder.Id),
                                     database.MakeInParam("@sn", SqlDbType.VarChar, 20, xialiaoOrder.Sn), 
                                     database.MakeInParam("@order_id", SqlDbType.Int, 0, xialiaoOrder.Order.Id),
                                     database.MakeInParam("@createDate", SqlDbType.DateTime, 0, xialiaoOrder.CreateDate),
                                     database.MakeInParam("@operator", SqlDbType.Int, 4, xialiaoOrder.Operatr.Id),
                                     database.MakeInParam("@total_number", SqlDbType.Decimal, 0, xialiaoOrder.TotalNumber),
                                     database.MakeInParam("@total_packages", SqlDbType.Int, 0, xialiaoOrder.TotalPackages),
                                     database.MakeInParam("@payment", SqlDbType.Money, 0, xialiaoOrder.Payment),
                                     database.MakeInParam("@processing_charges", SqlDbType.Money, 0, xialiaoOrder.ProcessingCharges),
                                     database.MakeInParam("@total_cost", SqlDbType.Money, 0, xialiaoOrder.TotalCost),
                                     new SqlParameter("@message", SqlDbType.NVarChar, 50),
                                     new SqlParameter("rval", SqlDbType.Int, 4) 
                                   };

            prams[prams.Length - 2].Direction = ParameterDirection.Output;
            prams[prams.Length - 1].Direction = ParameterDirection.ReturnValue;

            return prams;
        }

        private SqlParameter[] constructParamsForXialiaoOrderItem(XialiaoOrderItem xialiaoOrderItem)
        {

            SqlParameter[] prams = {
                                   database.MakeInParam("@id", SqlDbType.Int, 0, xialiaoOrderItem.Id), 
                                   database.MakeInParam("@xialiao_order_id", SqlDbType.Int, 0, xialiaoOrderItem.XialiaoOrder.Id),
                                   database.MakeInParam("@order_item_id", SqlDbType.Int, 0, xialiaoOrderItem.OrderItem.Id),
                                   database.MakeInParam("@original_package", SqlDbType.Int, 0, xialiaoOrderItem.OriginalPackage),
                                   database.MakeInParam("@remain_package", SqlDbType.Int, 0, xialiaoOrderItem.RemainPackage),
                                   database.MakeInParam("@use_package", SqlDbType.Int, 0, xialiaoOrderItem.UsePackage),
                                   database.MakeInParam("@working_number_1", SqlDbType.Decimal, 0, xialiaoOrderItem.WorkingNumber1),
                                   database.MakeInParam("@working_number_2", SqlDbType.Decimal, 0, xialiaoOrderItem.WorkingNumber2),
                                   database.MakeInParam("@working_number_3", SqlDbType.Decimal, 0, xialiaoOrderItem.WorkingNumber3),
                                   new SqlParameter("@message", SqlDbType.NVarChar, 50),
                                   new SqlParameter("rval", SqlDbType.Int, 4) 
                                   };

            prams[prams.Length - 2].Direction = ParameterDirection.Output;
            prams[prams.Length - 1].Direction = ParameterDirection.ReturnValue;

            return prams;
        }

        private SqlParameter[] constructParamsForXialiaoOrderStats(XialiaoOrderStats xialiaoOrderStats)
        {
            SqlParameter[] prams = {
                                       database.MakeInParam("@id", SqlDbType.Int, 0, xialiaoOrderStats.Id), 
                                       database.MakeInParam("@xialiao_order_id", SqlDbType.Int, 0, xialiaoOrderStats.XialiaoOrder.Id), 
                                       database.MakeInParam("@order_stats_id", SqlDbType.Int, 0, xialiaoOrderStats.OrderStats.Id),
                                       database.MakeInParam("@total_number", SqlDbType.Decimal, 0, xialiaoOrderStats.TotalNumber),
                                       database.MakeInParam("@amount_of_money", SqlDbType.Money, 0, xialiaoOrderStats.AmountOfMoney), 
                                       new SqlParameter("@message", SqlDbType.NVarChar, 50),
                                       new SqlParameter("rval", SqlDbType.Int, 4) 
                                   };

            prams[prams.Length - 2].Direction = ParameterDirection.Output;
            prams[prams.Length - 1].Direction = ParameterDirection.ReturnValue;

            return prams;
        }

    }
}
