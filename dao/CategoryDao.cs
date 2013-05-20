using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using haisan.util;
using System.Windows.Forms;
using haisan.domain;

namespace haisan.dao
{
    interface CategoryDao
    {
        // 采用递归实现对category的填充，如果数据量太大，递归深度过深，将造成系统缓慢
        // table 无tb前缀
        void fillSubCategory(TreeNode parent, string table);

        // table 无tb前缀
        MessageLocal saveCategory(string name, Category parent, string table);

        // table 无tb前缀
        Category getCategoryByName(string name, string table);

        // table 无tb前缀
        MessageLocal updateCategoryByName(string name, Category cur, string table);

        // table 无tb前缀
        int deleteCategoryRecursive(Category cur, string table);

    }
}
