using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace haisan.dao
{
    class CommonDaoImpl
    {
        protected string getValue(DataSet dataset, string name)
        {
            return dataset.Tables[0].Rows[0][name].ToString();
        }
    }
}
