using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using haisan.domain;
using haisan.util;

namespace haisan.dao
{
    interface PlainDao
    {
        MessageLocal saveOrUpdatePlain(Plain plain, string tableName);
    }
}
