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

        protected string getValue(DataSet dataset, int index, string name)
        {
            return dataset.Tables[0].Rows[index][name].ToString();
        }

        protected int getIntValue(DataSet dataset, int index, string name)
        {
            return int.Parse(dataset.Tables[0].Rows[index][name].ToString());
        }

        protected short getShortValue(DataSet dataset, int index, string name)
        {
            return short.Parse(dataset.Tables[0].Rows[index][name].ToString());
        }
    }
}
