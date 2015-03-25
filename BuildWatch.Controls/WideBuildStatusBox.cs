using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BuildWatch.Controls
{
    public partial class WideBuildStatusBox: UserControl
    {
        public string BuildName
        {
            get { return buildNameTxt.Text; }
            set { buildNameTxt.Text = value; }
        }

        public string BuildState
        {
            get { return buildStatusTxt.Text; }
            set { buildStatusTxt.Text = value; }
        }

        public string BuildFinish
        {
            get { return buildFinishTxt.Text; }
            set { buildFinishTxt.Text = value; }
        }

        public string BuildUser
        {
            get { return buildUserTxt.Text; }
            set { buildUserTxt.Text = value; }
        }

        public Color BuildForeColor
        {
            get { return ctlGrid.ForeColor; }
            set { ctlGrid.ForeColor = value; }
        }

        public Color BuildBackColor
        {
            get { return ctlGrid.BackColor; }
            set { ctlGrid.BackColor = value; }
        }

        public WideBuildStatusBox()
        {
            InitializeComponent();
        }
    }
}
