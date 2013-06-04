using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using haisan.util;
using haisan.frame.pdm.purchase;
using System.Data.SqlClient;
using System.Data;
using haisan.domain;
using System.Windows.Forms;

namespace haisan.dao
{
    class PurchaseOrderDaoImpl : CommonDaoImpl, PurchaseOrderDao
    {

        private static Database database = Database.getInstance();
        private static BaseDao baseDao = BaseDaoImpl.getInstance();

        private static PurchaseOrderDaoImpl purchaseOrderDaoImpl = null;

        private PurchaseOrderDaoImpl() { }

        public static PurchaseOrderDaoImpl getInstance()
        {
            if (null == purchaseOrderDaoImpl)
                purchaseOrderDaoImpl = new PurchaseOrderDaoImpl();

            return purchaseOrderDaoImpl;
        }

        public MessageLocal saveOrUpdatePurchaseOrder(Order order)
        {
            MessageLocal msg = new MessageLocal();

            SqlConnection con = SessionFactory.getConnection();
            SqlTransaction sqlTran = con.BeginTransaction(IsolationLevel.RepeatableRead);
            LinkedList<SqlParameter> prams = new LinkedList<SqlParameter>();

            try
            {

                msg = baseDao.runProcedureTran("saveOrUpdate_order", constructParamsForOrder(order), "order", sqlTran);
                if (!msg.IsSucess)
                    throw new Exception(msg.Message);

                if (order.Id < 0)
                {
                    DataSet dataset = database.RunProcReturnTran("SELECT id FROM tb_order WHERE sn = '" + order.Sn + "'", "tb_order", CommandType.Text, sqlTran);
                    if (null != dataset && dataset.Tables[0].Rows.Count > 0)
                    {
                        order.Id = int.Parse(getValue(dataset, "id"));
                    }
                    else
                    {
                        throw new Exception("查询订单编号【"+order.Sn+"】失败！");
                    }
                }

                foreach (OrderItem item in order.OrderItems)
                {
                    msg = baseDao.runProcedureTran("saveOrUpdate_order_item", constructParamsForOrderItem(item), "order_item", sqlTran);
                    if (!msg.IsSucess)
                        throw new Exception(msg.Message);
                }

                foreach (OrderStats stats in order.OrderStats)
                {
                    msg = baseDao.runProcedureTran("saveOrUpdate_order_stats", constructParamsForOrderStats(stats), "order_stats", sqlTran);
                    if (!msg.IsSucess)
                        throw new Exception(msg.Message);
                }           
                sqlTran.Commit();

            }catch (Exception e)
            {
                sqlTran.Rollback();
                msg.Message = "保存失败！" + e.Message;
                msg.IsSucess = false;
                Console.WriteLine(e.StackTrace);
                return msg;
            }finally
            {
                con.Close();
            }
            msg.IsSucess = true;

            return msg;
        }

        public DataSet getPurchaseOrder(string sn, string customName, DateTime begin, DateTime end)
        {
            string sql = "SELECT * FROM tb_order_view WHERE (sn LIKE @sn AND name LIKE @name AND " +
                   "(createDate BETWEEN @begin AND @end))";

            LinkedList<SqlParameter> prams = new LinkedList<SqlParameter>();

            prams.AddLast(database.MakeInParam("@sn", SqlDbType.NVarChar, 20, "%" + sn + "%"));
            prams.AddLast(database.MakeInParam("@name", SqlDbType.NVarChar, 20, "%" + customName + "%"));
            prams.AddLast(database.MakeInParam("@begin", SqlDbType.DateTime, 0, begin));
            prams.AddLast(database.MakeInParam("@end", SqlDbType.DateTime, 0, end));

            return database.RunProcReturn(sql, prams.ToArray<SqlParameter>(), "tb_order_view");
        }

        public Order getOrderById(int id)
        {
            Order order = loadOrderById(id);

            if (null != order)
            {
                fillOrderItems(order);
                fillOrderStats(order);
            }

            return order;
        }

        private void fillOrderStats(Order order)
        {
             SqlParameter[] prams = {database.MakeInParam("@order", SqlDbType.Int, 0, order.Id),};
            string sql = "SELECT tos.*, tt.name AS name1, tt.unit AS unit1 FROM tb_order_stats AS tos, tb_typeOfProcess AS tt " + 
                " WHERE tos.[order] = @order AND tos.type_of_process = tt.id ";
            try
            {
                DataSet dataset = database.RunProcReturn(sql, prams, "tb_order_stats");
                int count = 0;
                if (null != dataset && (count = dataset.Tables[0].Rows.Count) > 0)
                {
                    int i = 0;
                    for (i = 0; i < count; i++)
                    {
                        OrderStats stats = parseOrderStats(dataset, i);
                        stats.Order = order;
                        order.OrderStats.AddLast(stats);
                    }
                }
            }
            catch (Exception e)
            {
                Util.showError(e.Message);
                Console.WriteLine("141 of purDaoImpl: " + e.StackTrace);
            }
        }

        private OrderStats parseOrderStats(DataSet dataset, int index)
        {
            OrderStats stats = new OrderStats();
            stats.Id = getIntValue(dataset, index, "id");
            stats.TypeOfProcess = new TypeOfProcess(getIntValue(dataset, index, "type_of_process"),
                getValue(dataset, index, "name1"), getValue(dataset, index, "unit1"));

            stats.Image = getImageValue(dataset, index, "image");
            stats.Unit = getValue(dataset, index, "unit");
            stats.TotalNumber = getDecimalValue(dataset, index, "total_number");
            stats.UnitPrice = getDecimalValue(dataset, index, "unit_price");
            stats.AmountOfMoney = getDecimalValue(dataset, index, "amount_of_money");

            return stats;
        }

        private void fillOrderItems(Order order)
        {
            string sql = "SELECT * FROM tb_order_item_view WHERE [order] = " + order.Id;
            try
            {
                DataSet dataset = database.RunProcReturn(sql, "tb_order_item_view");
                int count = 0;
                if (null != dataset && (count = dataset.Tables[0].Rows.Count) > 0)
                {
                    int i = 0;
                    for (i = 0; i < count; i++)
                    {
                        OrderItem item = parseOrderItem(dataset, i);
                        item.Order = order;
                        order.OrderItems.AddLast(item);
                    }
                }
            }catch(Exception  e){
                Util.showError(e.Message);
                Console.WriteLine("178 purchaseDaoImpl: " + e.StackTrace);
            }

        }

        public DataSet getOrderItems(Order order)
        {
            SqlParameter[] prams = {
									    database.MakeInParam("@order",  SqlDbType.VarChar, 20, order.Id)
			};

            return database.RunProcReturn("get_reportOfOrderItem", prams, "ReportOrderItem", CommandType.StoredProcedure);
        }

        public DataSet getOrderStats(Order order)
        {
            SqlParameter[] prams = {
									    database.MakeInParam("@order",  SqlDbType.VarChar, 20, order.Id)
			};

            return database.RunProcReturn("get_reportOfOrderStats", prams, "ReportOrderStats", CommandType.StoredProcedure);
        }


        private OrderItem parseOrderItem(DataSet dataset, int index)
        {
            OrderItem item = new OrderItem();
            item.Id = getIntValue(dataset, index, "id");
            item.CategoryOfStone = new CategoryOfStone(getIntValue(dataset, index, "category_stone"),
                getValue(dataset, index,"category_stone_name"));
            item.ProductName = new ProductName(getIntValue(dataset, index, "product_name"),
                getValue(dataset, index, "product_name_name"));
            item.Length = getValue(dataset, index, "length");
            item.Width = getValue(dataset, index, "width");
            item.Thickness = getValue(dataset, index, "thickness");
            item.Package = getIntValue(dataset, index, "package");
            item.Unit = getValue(dataset, index, "unit");
            item.Number = getDecimalValue(dataset, index, "number");
            item.UnitPrice = getDecimalValue(dataset, index, "unit_price");
            item.Cost = getDecimalValue(dataset, index, "cost");

            if (dataset.Tables[0].Rows[index]["working_name_1"] == DBNull.Value)
            {
                item.WorkingDiagram1 = null;
                item.WorkingName1 = null;
                item.WorkingNumber1 = 0;
            }
            else
            {

                item.WorkingDiagram1 = getImageValue(dataset, index, "working_diagram_1");
                item.WorkingName1 = new TypeOfProcess(getIntValue(dataset, index, "working_name_1"),
                    getValue(dataset, index, "name1"), getValue(dataset, index, "unit1"));
                item.WorkingNumber1 = getDecimalValue(dataset, index, "working_number_1");
            }



            if (dataset.Tables[0].Rows[index]["working_name_2"] == DBNull.Value)
            {
                item.WorkingDiagram2 = null;
                item.WorkingName2 = null;
                item.WorkingNumber2 = 0;
            }
            else
            {

                item.WorkingDiagram2 = getImageValue(dataset, index, "working_diagram_2");
                item.WorkingName2 = new TypeOfProcess(getIntValue(dataset, index, "working_name_2"),
                    getValue(dataset, index, "name2"), getValue(dataset, index, "unit2"));
                item.WorkingNumber2 = getDecimalValue(dataset, index, "working_number_2");
            }


           
            if (dataset.Tables[0].Rows[index]["working_name_3"] == DBNull.Value)
            {
                item.WorkingDiagram3 = null;
                item.WorkingName3 = null;
                item.WorkingNumber3 = 0;
            }
            else
            {
                item.WorkingDiagram3 = getImageValue(dataset, index, "working_diagram_3");
                item.WorkingName3 = new TypeOfProcess(getIntValue(dataset, index, "working_name_3"),
                    getValue(dataset, index, "name3"), getValue(dataset, index, "unit3"));
                item.WorkingNumber3 = getDecimalValue(dataset, index, "working_number_3");
            }


            return item;
        }

        public Order loadOrderById(int id)
        {
            Order order = new Order(id);
            string sql = "SELECT * FROM tb_order_view WHERE id = " + id;
            try
            {
                DataSet dataset = database.RunProcReturn(sql, "tb_order_view");
                if (null != dataset && dataset.Tables[0].Rows.Count > 0)
                {
                    order.Sn = getValue(dataset, "sn");
                    order.Company = new Company(Util.getIntValue(getValue(dataset, "company")), 
                        getValue(dataset, "name"));
                    order.WayOfPayment = getValue(dataset, "way_of_payment");
                    order.Phone = getValue(dataset, "phone");
                    order.CreateDate = DateTime.Parse(getValue(dataset, "createDate"));
                    order.TotalNumber = Util.getDecimalValue(getValue(dataset, "total_number"));
                    order.TotalPackages = Util.getIntValue(getValue(dataset, "total_packages"));
                    order.Payment = Util.getDecimalValue(getValue(dataset, "payment"));
                    order.ProcessingCharges = Util.getDecimalValue(getValue(dataset, "processing_charges"));
                    order.TotalCost = Util.getDecimalValue(getValue(dataset, "total_cost"));
                    order.AdvancesReceived = Util.getDecimalValue(getValue(dataset, "advances_received"));
                    order.Operatr = new User(Util.getIntValue(getValue(dataset, "operator")),
                        getValue(dataset, "username"));
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }

            return order;
        }

        private SqlParameter[] constructParamsForOrder(Order order)
        {
            SqlParameter[] prams = { 
                                     database.MakeInParam("@id", SqlDbType.Int, 0, order.Id),
                                     database.MakeInParam("@sn", SqlDbType.VarChar, 20, order.Sn), 
                                     database.MakeInParam("@company", SqlDbType.Int, 0, order.Company.Id),
                                     database.MakeInParam("@way_of_payment", SqlDbType.NVarChar, 20, order.WayOfPayment),
                                     database.MakeInParam("@phone", SqlDbType.VarChar, 20, order.Phone),
                                     database.MakeInParam("@createDate", SqlDbType.DateTime, 0, order.CreateDate),
                                     database.MakeInParam("@operator", SqlDbType.Int, 4, order.Operatr.Id),
                                     database.MakeInParam("@total_number", SqlDbType.Decimal, 0, order.TotalNumber),
                                     database.MakeInParam("@total_packages", SqlDbType.Int, 0, order.TotalPackages),
                                     database.MakeInParam("@payment", SqlDbType.Money, 0, order.Payment),
                                     database.MakeInParam("@processing_charges", SqlDbType.Money, 0, order.ProcessingCharges),
                                     database.MakeInParam("@total_cost", SqlDbType.Money, 0, order.ProcessingCharges),
                                     database.MakeInParam("@advances_received", SqlDbType.Money, 0, order.AdvancesReceived),
                                     new SqlParameter("@message", SqlDbType.NVarChar, 50),
                                     new SqlParameter("rval", SqlDbType.Int, 4) 
                                   };

            prams[prams.Length - 2].Direction = ParameterDirection.Output;
            prams[prams.Length - 1].Direction = ParameterDirection.ReturnValue;

            return prams;
        }

       private SqlParameter[] constructParamsForOrderItem(OrderItem orderItem)
        {

            SqlParameter[] prams = {
                                   database.MakeInParam("@id", SqlDbType.Int, 0, orderItem.Id), 
                                   database.MakeInParam("@order", SqlDbType.Int, 0, orderItem.Order.Id),
                                   database.MakeInParam("@category_stone", SqlDbType.Int, 0, orderItem.CategoryOfStone.Id),
                                   database.MakeInParam("@product_name", SqlDbType.Int, 0, orderItem.ProductName.Id),
                                   database.MakeInParam("@length", SqlDbType.VarChar, 20, orderItem.Length),
                                   database.MakeInParam("@width", SqlDbType.VarChar, 20, orderItem.Width),
                                   database.MakeInParam("@thickness", SqlDbType.VarChar, 20, orderItem.Thickness),
                                   database.MakeInParam("@package", SqlDbType.Int, 0, orderItem.Package),
                                   database.MakeInParam("@unit", SqlDbType.NVarChar, 10, orderItem.Unit),
                                   database.MakeInParam("@number", SqlDbType.Decimal, 0, orderItem.Number),
                                   database.MakeInParam("@unit_price", SqlDbType.Money, 0, orderItem.UnitPrice),
                                   database.MakeInParam("@cost", SqlDbType.Money, 0, orderItem.Cost),
                                   database.MakeInParam("@working_diagram_1", SqlDbType.Image, 0,(null == orderItem.WorkingDiagram1 ? DBNull.Value : (object)Util.imageToByteArray(orderItem.WorkingDiagram1))),
                                   database.MakeInParam("@working_name_1", SqlDbType.Int, 0, (null == orderItem.WorkingName1) ? DBNull.Value : (object)orderItem.WorkingName1.Id),
                                   database.MakeInParam("@working_number_1", SqlDbType.Decimal, 0, orderItem.WorkingNumber1),
                                   database.MakeInParam("@working_diagram_2", SqlDbType.Image, 0, (null == orderItem.WorkingDiagram2 ? DBNull.Value : (object)Util.imageToByteArray(orderItem.WorkingDiagram2))),
                                   database.MakeInParam("@working_name_2", SqlDbType.Int, 0, (null == orderItem.WorkingName2) ? DBNull.Value : (object)orderItem.WorkingName2.Id),
                                   database.MakeInParam("@working_number_2", SqlDbType.Decimal, 0, orderItem.WorkingNumber2),
                                   database.MakeInParam("@working_diagram_3", SqlDbType.Image, 0, (null == orderItem.WorkingDiagram3 ? DBNull.Value : (object)Util.imageToByteArray(orderItem.WorkingDiagram3))),
                                   database.MakeInParam("@working_name_3", SqlDbType.Int, 0, (null == orderItem.WorkingName3) ? DBNull.Value : (object)orderItem.WorkingName2.Id),
                                   database.MakeInParam("@working_number_3", SqlDbType.Decimal, 0, orderItem.WorkingNumber3),
                                   new SqlParameter("@message", SqlDbType.NVarChar, 50),
                                   new SqlParameter("rval", SqlDbType.Int, 4) 
                                   };

            prams[prams.Length - 2].Direction = ParameterDirection.Output;
            prams[prams.Length - 1].Direction = ParameterDirection.ReturnValue;

            return prams;
        }

       private SqlParameter[] constructParamsForOrderStats(OrderStats orderStats)
        {
            SqlParameter[] prams = {
                                       database.MakeInParam("@id", SqlDbType.Int, 0, orderStats.Id), 
                                       database.MakeInParam("@order", SqlDbType.Int, 0, orderStats.Order.Id),
                                       database.MakeInParam("@type_of_process", SqlDbType.Int, 0, orderStats.TypeOfProcess.Id), 
                                       database.MakeInParam("@image", SqlDbType.Image, 0, null == orderStats.Image ? DBNull.Value : (object)Util.imageToByteArray(orderStats.Image)),
                                       database.MakeInParam("@unit", SqlDbType.NVarChar, 10, orderStats.Unit), 
                                       database.MakeInParam("@total_number", SqlDbType.Decimal, 0, orderStats.TotalNumber),
                                       database.MakeInParam("@unit_price", SqlDbType.Money, 0, orderStats.UnitPrice),
                                       database.MakeInParam("@amount_of_money", SqlDbType.Money, 0, orderStats.AmountOfMoney), 
                                       new SqlParameter("@message", SqlDbType.NVarChar, 50),
                                       new SqlParameter("rval", SqlDbType.Int, 4) 
                                   };

            prams[prams.Length - 2].Direction = ParameterDirection.Output;
            prams[prams.Length - 1].Direction = ParameterDirection.ReturnValue;

            return prams;
        }
    }
}
