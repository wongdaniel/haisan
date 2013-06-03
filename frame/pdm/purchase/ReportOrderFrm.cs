using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CrystalDecisions.Shared;

namespace haisan.frame.pdm.purchase
{
    public partial class ReportOrderFrm : Form
    {
        public ReportOrderFrm()
        {
            InitializeComponent();
        }

        public ReportOrderFrm(CrystalReportOrderItem crp)
        {
            InitializeComponent();
            crystalReportViewerOrder.ReportSource = crp;
            
        }

        public void setParameterFields(ParameterFields paramFields)
        {
            this.crystalReportViewerOrder.ParameterFieldInfo = paramFields;

           // crystalReportViewerOrder.DataDefinition.ParameterFields["sn"].ApplyCurrentValues(paramFields);
            
        }

        private void ReportOrder_Load(object sender, EventArgs e)
        {
            
        }

        public void refreshReport()
        {
            this.crystalReportViewerOrder.RefreshReport();
        }
    }
}
