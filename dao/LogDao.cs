using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using haisan.domain;

namespace haisan.dao
{
    interface LogDao
    {
        void saveLog(User user, string module);
    }
}
