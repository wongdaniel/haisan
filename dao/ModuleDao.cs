using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using haisan.domain;
using System.Windows.Forms;
using System.Data;

namespace haisan.dao
{
    interface ModuleDao
    {

        Module getModuleById(int id);
        Module getModuleByParentId(int parent);
        void fillSubModule(TreeNode parent, string table);
        DataSet getAllPermissionByModuleId(int group, int module);
        void updatePermission(Permission per);
    }
}
