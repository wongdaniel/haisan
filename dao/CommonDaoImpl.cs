using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Drawing;
using haisan.util;

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
            if ("".Equals(dataset.Tables[0].Rows[index][name].ToString()))
                return 0;
            return int.Parse(dataset.Tables[0].Rows[index][name].ToString());
        }

        protected short getShortValue(DataSet dataset, int index, string name)
        {
            if ("".Equals(dataset.Tables[0].Rows[index][name].ToString()))
                return 0;
            return short.Parse(dataset.Tables[0].Rows[index][name].ToString());
        }

        protected bool getBoolValue(DataSet dataset, int index, string name)
        {
            if("".Equals(dataset.Tables[0].Rows[index][name].ToString()))
                return false;
            return bool.Parse(dataset.Tables[0].Rows[index][name].ToString());
        }

        protected decimal getDecimalValue(DataSet dataset, int index, string name)
        {
            if ("".Equals(dataset.Tables[0].Rows[index][name].ToString()))
                return 0;
            return decimal.Parse(dataset.Tables[0].Rows[index][name].ToString());
        }

        protected Image getImageValue(DataSet dataset, int index, string name)
        {
            if (DBNull.Value == dataset.Tables[0].Rows[index][name])
                return null;
           return Util.byteArrayToImage((byte[])dataset.Tables[0].Rows[index][name]);
        }

        protected DateTime getDateTime(DataSet dataset, int index, string name)
        {
            if (dataset.Tables[0].Rows[index][name] == DBNull.Value)
            {
                return DateTime.Now;
            }
            return DateTime.Parse(dataset.Tables[0].Rows[index][name].ToString());
        }
    }
}
