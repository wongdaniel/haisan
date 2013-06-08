using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using haisan.util;

namespace haisan.frame.test
{
    public partial class ShowDWGFrm : Form
    {
        public ShowDWGFrm()
        {
            InitializeComponent();
        }

        private void ShowDWGFrm_Load(object sender, EventArgs e)
        {
            string fileName = "H:\\share\\Drawing2.dwg";
            pictureBox1.Image = CAD.getThumbnailDWG(fileName);
        }
    }
}
