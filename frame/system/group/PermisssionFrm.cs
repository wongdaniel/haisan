using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using haisan.domain;
using haisan.dao;
using haisan.util;

namespace haisan.frame.system.group
{
    public partial class PermisssionFrm : Form
    {
       
        private static readonly string PREFIX_CHECKBOX = "threeState"; 
        ModuleDao moduleDao = ModuleDaoImpl.getInstance();
        private TreeNode clicked = null;
        private int group = -1;

        public PermisssionFrm()
        {
            InitializeComponent();
        }

        private void PermisssionFrm_Load(object sender, EventArgs e)
        {
            refreshTreeView();
            refreshDataGridView();
   //       permissionListHeadText();
        }

        private void refreshTreeView()
        {
            treeViewModule.Nodes.Clear();
            Module root = moduleDao.getModuleByParentId(-1);
            TreeNode all = treeViewModule.Nodes.Add(root.DisplayName);
            all.Tag = root;
            moduleDao.fillSubModule(all, "module");
            all.Expand();

            clicked = all;
        }

        private void treeViewModule_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (clicked != e.Node)
            {
                clicked = e.Node;
                refreshDataGridView();
            }
        }

        private void refreshDataGridView()
        {
            dataGridViewPermission.Rows.Clear();
            DataTable dt = moduleDao.getAllPermissionByModuleId(group, ((Module)clicked.Tag).Id).Tables[0];
            foreach (DataRow dr in dt.Rows)
            {
                string[] str = {dr["id"].ToString(), dr["display_name"].ToString(),
                                getValue(dr,"query"),  getValue(dr,"add"), getValue(dr,"modify"),
                                getValue(dr,"delete"), getValue(dr,"print"), getValue(dr,"run"),
                                getValue(dr,"save_table"), getValue(dr,"check"), getValue(dr,"anticheck")};
                  
                dataGridViewPermission.Rows.Add(str);
            } 

        }

        private string getValue(DataRow dr, string name)
        {
            string str = dr[name].ToString();
            if ("0".Equals(str)) return "False";
            if("1".Equals(str)) return "True";

            return str;
        }

        //并没有使用
        private void permissionListHeadText()
        {
            int i = 0;

            dataGridViewPermission.Columns[i].Visible = false;
            dataGridViewPermission.Columns[i++].HeaderText = "权限ID";
            dataGridViewPermission.Columns[i].Width = 100;
            dataGridViewPermission.Columns[i++].HeaderText = "模块名";
           // dataGridViewPermission.Columns[i].Width = 60;
            //dataGridViewPermission.Columns[i].
            dataGridViewPermission.Columns[i++].HeaderText = "查询";
           // dataGridViewPermission.Columns[i].Width = 60;
            dataGridViewPermission.Columns[i++].HeaderText = "新增";
           // dataGridViewPermission.Columns[i].Width = 60;
            dataGridViewPermission.Columns[i++].HeaderText = "修改";
          //  dataGridViewPermission.Columns[i].Width = 60;
            dataGridViewPermission.Columns[i++].HeaderText = "删除";
          //  dataGridViewPermission.Columns[i].Width = 60;
            dataGridViewPermission.Columns[i++].HeaderText = "打印";
         //   dataGridViewPermission.Columns[i].Width = 60;
            dataGridViewPermission.Columns[i++].HeaderText = "运行";
            dataGridViewPermission.Columns[i++].HeaderText = "保表";
          //  dataGridViewPermission.Columns[i].Width = 60;
            dataGridViewPermission.Columns[i++].HeaderText = "审查";
            dataGridViewPermission.Columns[i++].HeaderText = "反审查";
        }

        private void dataGridViewPermission_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (this.dataGridViewPermission.Columns[e.ColumnIndex].Name.IndexOf(PREFIX_CHECKBOX) == 0)
            {
                if (e.Value != null)
                {
                    DataGridViewCheckBoxCell checkbox = this.dataGridViewPermission.Rows[e.RowIndex].Cells[e.ColumnIndex] as DataGridViewCheckBoxCell;
                   
                    if (GetCheckState(checkbox.Value.ToString()) == CheckState.Indeterminate)
                    {
                        checkbox.ThreeState = true;
                        checkbox.ReadOnly = true;
                        return;
                    }

                }
            }
        }

        private void dataGridViewPermission_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (this.dataGridViewPermission.Columns[e.ColumnIndex].Name.IndexOf(PREFIX_CHECKBOX) == 0)
            {
                DataGridViewCheckBoxCell checkbox = this.dataGridViewPermission.Rows[e.RowIndex].Cells[e.ColumnIndex] as DataGridViewCheckBoxCell;
                if(GetCheckState(checkbox.Value.ToString()) != CheckState.Indeterminate)
                     this.dataGridViewPermission.Rows[e.RowIndex].Tag = true;

                if (GetCheckState(checkbox.Value.ToString()) == CheckState.Unchecked)
                {
                    checkbox.Value = CheckState.Checked;
                    dataGridViewPermission.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = "true";
                }else if(GetCheckState(checkbox.Value.ToString()) == CheckState.Checked)
                {
                    checkbox.Value = CheckState.Unchecked;
                    dataGridViewPermission.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = "false";
                }
            }
        }

        private CheckState GetCheckState(string s)
        {
            switch (s.ToLower())
            {
                case "false":
                case "unchecked":
                case "0":
                    return CheckState.Unchecked;
                case "true":
                case "checked":
                case "1":
                    return CheckState.Checked;
                case "2":
                case "indeterminate":
                    return CheckState.Indeterminate;
            }
            return CheckState.Checked;
        }

        private void 确定CToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Enabled = false;
            foreach (DataGridViewRow row in dataGridViewPermission.Rows)
            {
                if (null != row.Tag && bool.Parse(row.Tag.ToString()))
                {
                    moduleDao.updatePermission(constructPermission(row));
                }
            }
            this.Enabled = true;
        }

        private Permission constructPermission(DataGridViewRow row)
        {

            Permission per = new Permission();

            per.Id = int.Parse(row.Cells["ColumnID"].Value.ToString());
            per.Query = (short)GetCheckState(row.Cells["threeStateQuery"].FormattedValue.ToString());
            per.Add = (short)GetCheckState(row.Cells["threeStateAdd"].FormattedValue.ToString());
            per.Modify = (short)GetCheckState(row.Cells["threeStateModify"].FormattedValue.ToString());
            per.Delete = (short)GetCheckState(row.Cells["threeStateDelete"].FormattedValue.ToString());
            per.Run = (short)GetCheckState(row.Cells["threeStateRun"].FormattedValue.ToString());
            per.Print = (short)GetCheckState(row.Cells["threeStatePrint"].FormattedValue.ToString());
            per.SaveTable = (short)GetCheckState(row.Cells["threeStateSaveTable"].FormattedValue.ToString());
            per.Check = (short)GetCheckState(row.Cells["threeStateCheck"].FormattedValue.ToString());
            per.Anticheck = (short)GetCheckState(row.Cells["threeStateAnticheck"].FormattedValue.ToString());

            return per;
        }

        public void setGroupId(int id)
        {
            this.group = id;
        }
    }
}
