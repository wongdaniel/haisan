using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace haisan.dao
{
    interface ConfigureDao
    {
        string getValueByName(string name);
        int saveOrUpdateValueByName(string name, string value);
    }
}
