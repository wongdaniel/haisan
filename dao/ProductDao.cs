using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using haisan.domain;
using haisan.util;

namespace haisan.dao
{
    interface ProductDao
    {
        DataSet getAllProduct(string queryStr, int categoryId);
        MessageLocal saveOrUpdateProduct(Product product);
        Product getProductById(int id);
    }
}
