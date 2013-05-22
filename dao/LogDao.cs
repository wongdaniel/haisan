using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using haisan.domain;
using System.Data;

namespace haisan.dao
{
    interface LogDao
    {
        void saveLog(User user, string module);
        DataSet getLog(string module, string oper, DateTime begin, DateTime end);
        void delLog(string module, string oper, DateTime begin, DateTime end);
    }
}
