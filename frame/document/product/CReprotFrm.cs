using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace haisan.frame.document.product
{
    public partial class CReprotFrm : Form
    {
        public CReprotFrm()
        {
            InitializeComponent();
        }
        public CReprotFrm(CrystalReportProduct crp)
        {
            InitializeComponent();
            crystalReportViewer1.ReportSource = crp;
        }

        private void CReprotFrm_Load(object sender, EventArgs e)
        {

            this.crystalReportViewer1.RefreshReport();
        }
    }
}
