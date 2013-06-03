using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace haisan.frame.pdm.purchase
{
    public partial class ShowImageFrm : Form
    {
        public ShowImageFrm()
        {
            InitializeComponent();
        }

        public ShowImageFrm(Image image)
        {
            InitializeComponent();
            pictureBox1.Image = image;
        }
    }
}
