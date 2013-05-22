using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using haisan.domain;
using haisan.dao;
using haisan.util;
using System.Data;

namespace haisan.dao
{
    interface UserDao
    {
        MessageLocal login(User user);
        int loginOut(User user);
        MessageLocal updatePwd(User user);
        DataSet getAllUser(string queryStr);
        MessageLocal saveOrUpdateUser(User user);
        User getUserById(int id);
    }
}
