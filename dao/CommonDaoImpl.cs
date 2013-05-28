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
            return int.Parse(dataset.Tables[0].Rows[index][name].ToString());
        }

        protected short getShortValue(DataSet dataset, int index, string name)
        {
            return short.Parse(dataset.Tables[0].Rows[index][name].ToString());
        }

        protected bool getBoolValue(DataSet dataset, int index, string name)
        {
            return bool.Parse(dataset.Tables[0].Rows[index][name].ToString());
        }

        protected Image getImageValue(DataSet dataset, int index, string name)
        {
           return Util.byteArrayToImage((byte[])dataset.Tables[0].Rows[index][name]);
        }
    }
}
