using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using haisan.util;
using haisan.domain;
using System.Windows.Forms;

namespace haisan.dao
{
    interface GroupDao
    {
        MessageLocal saveOrUpdateGroup(Group group);
        Dictionary<Module, Permission> getAllPermissions(Group group);

    }
}
