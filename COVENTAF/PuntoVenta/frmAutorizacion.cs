using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace COVENTAF.PuntoVenta
{
    public partial class frmAutorizacion : Form
    {
        public frmAutorizacion()
        {
            InitializeComponent();
        }

        private void frmAutorizacion_Load(object sender, EventArgs e)
        {
            this.txtUser.Text = "";
            this.txtUser.Focus();
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {

        }
    }
}
