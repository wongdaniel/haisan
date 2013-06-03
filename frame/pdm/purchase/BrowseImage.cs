using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using haisan.dao;
using haisan.domain;
using System.Runtime.InteropServices;
using haisan.util;

namespace haisan.frame.pdm.purchase
{
    public partial class BrowseImage : Form
    {
        private ProcessingImageDao proImageDao = ProcessingImageDaoImpl.getInstance();
        private DataGridViewCell dataGridViewCell = null;

        [DllImport("User32.dll")]
        private static extern int SendMessage(int Handle, int wMsg, int wParam, int lParam);

        const int LVM_FIRST = 0x1000;
        const int LVM_SETICONSPACING = LVM_FIRST + 53;

        public void SetListViewSpacing(ListView lst, int x, int y)
        {
            SendMessage(lst.Handle.ToInt32(), LVM_SETICONSPACING, 0, x * 65536 + y);
        }

        public BrowseImage()
        {
            InitializeComponent();
        }

        public BrowseImage(DataGridViewCell dataGridViewCell)
        {
            InitializeComponent();
            this.dataGridViewCell = dataGridViewCell;
        }

        private void BrowseImage_Load(object sender, EventArgs e)
        {
            refreshListView();
        }

        private void refreshListView()
        {
            listViewImage.Items.Clear();
            listViewImage.LargeImageList = imageList1;
            imageList1.ImageSize = new Size(Parameter.THUMBNAIL_LENGTH, Parameter.THUMBNAIL_WIDTH);

            LinkedList<ProcessingImage> proImages = proImageDao.getAllProcessingImage();
            int count = 0;
            foreach (ProcessingImage proImage in proImages)
            {
                imageList1.Images.Add(proImage.Image);
                listViewImage.Items.Add(proImage.Name);
                listViewImage.Items[count].ImageIndex = count;
                listViewImage.Items[count].Tag = proImage;
                count++;
            }

            SetListViewSpacing(listViewImage, 50, 120);//列，行
        }

        private void listViewImage_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            dataGridViewCell.Value = ((ProcessingImage)e.Item.Tag).Image;
            dataGridViewCell.Tag = (ProcessingImage)e.Item.Tag;
            this.Close();
        }

    }
}
