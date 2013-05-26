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

namespace haisan.frame.system.user
{
    public partial class AddUserFrm : Form
    {

        private int userId = -1;
        UserDao userDao = UserDaoImpl.getInstance();
        BaseDao baseDao = BaseDaoImpl.getInstance();

        public AddUserFrm()
        {
            InitializeComponent();
            fillGroups();
        }

        private void buttonSaveAdd_Click(object sender, EventArgs e)
        {
           
            User user = new User(userId);
            if (!checkParameter())
                return;
            fillUser(user);

            this.Enabled = false;
            MessageLocal msg = userDao.saveOrUpdateUser(user);
            if (!msg.IsSucess)
            {
                MessageBox.Show(msg.Message, "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                this.Enabled = true;
                return;
            }

            if (!checkBoxIsCopy.Checked)
                clearFieds();

            userId = -1;
            this.Enabled = true;


        }

        private bool checkParameter()
        {
            bool success = true;
            string errorMsg = "";
             labelErrorStatus.Text = "";

            if ("".Equals(textBoxUsername.Text))
            {
                errorMsg += "【用户名】 ";
                success = false;
            }

            if(null == comboBoxGroup.SelectedItem || "".Equals(comboBoxGroup.SelectedItem.ToString()))
            {
                errorMsg += "【所属组】 ";
                success = false;
            }

            if(!success){
                labelErrorStatus.Text = errorMsg + " 不能为空";
            }

            return success;
        }

        private void fillUser(User user)
        {
            user.Username = textBoxUsername.Text;
            user.Name = textBoxName.Text;
            user.Password = textBoxPwd.Text;
            user.Email = textBoxEmail.Text;
            user.Phone = textBoxPwd.Text;
            user.IsLock = checkBoxIsLock.Checked;
            user.Description = textBoxDescription.Text;
            user.Group = (Group)comboBoxGroup.SelectedItem;
        }

        public void fillField()
        {
            User user = userDao.getUserById(userId);
            if (null == user) return;

            textBoxUsername.Text = user.Username;
            textBoxName.Text = user.Name;
            textBoxPwd.Text = user.Password;
            textBoxEmail.Text = user.Email;
            textBoxPhone.Text = user.Phone;
            checkBoxIsLock.Checked = user.IsLock;
            textBoxDescription.Text = user.Description;
            comboBoxGroup.SelectedIndex = comboBoxGroup.FindString(user.Group.Name); 
        }

        private void clearFieds()
        {
            textBoxUsername.Text = "";
            textBoxPwd.Text = "";
            textBoxEmail.Text = "";
            textBoxPhone.Text = "";
            checkBoxIsLock.Checked = false;
            textBoxDescription.Text = "";
            comboBoxGroup.SelectedItem = "";
        }

        private void fillGroups()
        {
            DataSet dataset = baseDao.getAllEntities("tb_group");
            int count = 0;
            if (null != dataset && (count = dataset.Tables[0].Rows.Count) > 0)
            {
                int i = 0;
                for (i = 0; i < count; i++)
                {
                    comboBoxGroup.Items.Add(new Group(int.Parse(dataset.Tables[0].Rows[i]["id"].ToString()),
                        dataset.Tables[0].Rows[i]["name"].ToString()));  
                }
            }
        }

        private void buttonSaveQuit_Click(object sender, EventArgs e)
        {

            User user = new User(userId);
            if (!checkParameter())
                return;
            fillUser(user);

            this.Enabled = false;
            MessageLocal msg = userDao.saveOrUpdateUser(user);
            if (!msg.IsSucess)
            {
                MessageBox.Show(msg.Message, "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                this.Enabled = true;
                return;
            }

            userId = -1;
            this.Enabled = true;

            this.Close();
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        public void setUserId(int id)
        {
            userId = id;
        }
    }
}
