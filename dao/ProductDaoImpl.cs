using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using haisan.util;
using haisan.domain;
using System.Windows.Forms;

namespace haisan.dao
{
    class ProductDaoImpl : ProductDao
    {
        private static Database database = Database.getInstance();
        private static BaseDao baseDao = BaseDaoImpl.getInstance();

        private static ProductDaoImpl productDaoImpl = null;

        private ProductDaoImpl() { }

        public static ProductDaoImpl getInstance()
        {
            if (null == productDaoImpl)
                return new ProductDaoImpl();
            return productDaoImpl;
        }

        public DataSet getAllProduct(string queryStr, int categoryId)
        {
            SqlParameter[] prams = {
                                            database.MakeInParam("@category",  SqlDbType.Int, 0, categoryId),
									        database.MakeInParam("@queryStr",  SqlDbType.NVarChar, 50, "%" + queryStr + "%"),
			    };

            return database.RunProcReturn("get_productByQueryStr",prams, "tb_product_view", CommandType.StoredProcedure);
        }

        public MessageLocal saveOrUpdateProduct(Product product)
        {
            SqlParameter[] prams = constructParamsForProduct(product);

            return baseDao.runProcedure("saveOrUpdate_product", prams, "product");
        }

        public Product getProductById(int id)
        {
            Product pro = new Product(id);
            SqlParameter[] prams = {
                                    database.MakeInParam("@id", SqlDbType.Int, 0, id)
			};
            try
            {
                DataSet dataset = database.RunProcReturn("get_productById", prams, "tb_categoryOfProduct", CommandType.StoredProcedure);
                if (null != dataset && dataset.Tables[0].Rows.Count > 0)
                {
                    pro.Name = getValue(dataset, "name");
                    pro.Code = getValue(dataset, "code");
                    pro.Spec = getValue(dataset, "spec");
                    Provider provider = new Provider(int.Parse(getValue(dataset, "provider")),
                            getValue(dataset, "company_name"));
                    pro.Provider = provider;

                    Category cate = new Category(int.Parse(getValue(dataset, "category")),
                      getValue(dataset, "category_name"));
                    pro.Category = cate;

                    pro.Address = getValue(dataset, "address");
                    pro.Unit = getValue(dataset, "unit");
                    pro.Purchase_price = decimal.Parse(getValue(dataset, "purchase_price"));
                    pro.Sale_price = decimal.Parse(getValue(dataset, "sale_price"));
                    pro.Wholesale_price = decimal.Parse(getValue(dataset, "wholesale_price"));
                    pro.Description = getValue(dataset, "description");
                    pro.Image = null; //skip image 
                  
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
            return pro;
        }

        SqlParameter[] constructParamsForProduct(Product product)
        {
            MessageLocal msg = new MessageLocal();
            byte[] buffByte = null == product.Image ? new byte[0] : Util.imageToByteArray(product.Image);

            SqlParameter[] prams = {
									database.MakeInParam("@id",  SqlDbType.Int, 0, product.Id),
                                    database.MakeInParam("@name", SqlDbType.NVarChar, 50, product.Name),
                                    database.MakeInParam("@code", SqlDbType.VarChar, 50, product.Code),
                                    database.MakeInParam("@spec", SqlDbType.VarChar, 20, product.Spec),
                                    database.MakeInParam("@provider", SqlDbType.Int, 0, product.Provider.Id),
                                    database.MakeInParam("@category", SqlDbType.Int, 0, product.Category.Id),
                                    database.MakeInParam("@address", SqlDbType.NVarChar, 20, product.Address),
                                    database.MakeInParam("@unit", SqlDbType.NVarChar, 10, product.Unit),
                                    database.MakeInParam("@purchase_price", SqlDbType.Money, 0, product.Purchase_price),
                                    database.MakeInParam("@sale_price", SqlDbType.Money, 0, product.Sale_price),
                                    database.MakeInParam("@wholesale_price", SqlDbType.Money, 0, product.Wholesale_price),
                                    database.MakeInParam("@description", SqlDbType.NVarChar, 100, product.Description),
                                    database.MakeInParam("@image", SqlDbType.Image, 0, buffByte),
                                    new SqlParameter("@message", SqlDbType.NVarChar, 50),
                                    new SqlParameter("rval", SqlDbType.Int, 4) 
			};
            prams[prams.Length - 2].Direction = ParameterDirection.Output;
            prams[prams.Length - 1].Direction = ParameterDirection.ReturnValue;

            return prams;
        }

        string getValue(DataSet dataset, string name)
        {
            return dataset.Tables[0].Rows[0][name].ToString();
        }
    }
}
