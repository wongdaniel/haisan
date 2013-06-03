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

namespace haisan.frame.document.warehouse
{
    public partial class AddWarehouseFrm : Form
    {
        private WarehouseDao warehouseDao = WarehouseDaoImpl.getInstance();
        private int warehouseId = -1;

        public AddWarehouseFrm()
        {
            InitializeComponent();
        }

        public void setId(int id)
        {
            this.warehouseId = id;
        }

        public void fillField()
        {
            Warehouse warehouse = warehouseDao.getWarehouseById(warehouseId);
            if (null == warehouse) return;

            textBoxName.Text = warehouse.Name;
            textBoxDescription.Text = warehouse.Description;
            checkBoxIsLocked.Checked = warehouse.IsLocked;
        }

        public void fillField(Warehouse warehouse)
        {
            warehouse.Name = textBoxName.Text;
            warehouse.Description = textBoxDescription.Text;
            warehouse.IsLocked = checkBoxIsLocked.Checked;
        }

        private void clearFieds()
        {
            textBoxName.Text = "";
            textBoxDescription.Text = "";
            checkBoxIsLocked.Checked = false;
        }

        private void buttonSaveAdd_Click(object sender, EventArgs e)
        {
            Warehouse warehouse = new Warehouse(warehouseId);
            if (!checkParameter())
                return;
            fillField(warehouse);

            this.Enabled = false;
            MessageLocal msg = warehouseDao.saveOrUpdateWarehouse(warehouse);
            if (!msg.IsSucess)
            {
                MessageBox.Show(msg.Message, "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                this.Enabled = true;
                return;
            }

            warehouseId = -1;
            this.Enabled = true;
            clearFieds();
        }

        private bool checkParameter()
        {
            bool success = true;
            string errorMsg = "";
            labelErrorStatus.Text = "";

            if ("".Equals(textBoxName.Text))
            {
                errorMsg += "【仓库名】 ";
                success = false;
            }

            if (!success)
            {
                labelErrorStatus.Text = errorMsg + " 不能为空";
            }

            return success;
        }

        private void buttonSaveQuit_Click(object sender, EventArgs e)
        {
            Warehouse warehouse = new Warehouse(warehouseId);
            if (!checkParameter())
                return;
            fillField(warehouse);

            this.Enabled = false;
            MessageLocal msg = warehouseDao.saveOrUpdateWarehouse(warehouse);
            if (!msg.IsSucess)
            {
                MessageBox.Show(msg.Message, "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                this.Enabled = true;
                return;
            }

            warehouseId = -1;
            this.Enabled = true;
            this.Close();
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
