using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Reflection;

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
            SetDoubleBuffered(ctlGrid, true);
            SetDoubleBuffered(ctlRow1, true);
            SetDoubleBuffered(ctlRow2, true);
            SetDoubleBuffered(buildNameTxt, true);
            SetDoubleBuffered(buildStatusTxt, true);
            SetDoubleBuffered(buildFinishTxt, true);
            SetDoubleBuffered(buildUserTxt, true);
            SetDoubleBuffered(buildUserPic, true);
        }

        private static void SetDoubleBuffered(Control control, bool enable)
        {
            var doubleBufferPropertyInfo = control.GetType().GetProperty("DoubleBuffered", BindingFlags.Instance | BindingFlags.NonPublic);
            doubleBufferPropertyInfo.SetValue(control, enable, null);
        }

        public void SetUserPicture(Image image)
        {
            if (image != null)
                buildUserPic.Image = image;
            else
                ResetUserPicture();
        }

        public void ResetUserPicture()
        {
            buildUserPic.Image = global::BuildWatch.Controls.Properties.Resources.dpc_Silhouette;
        }
    }
}
