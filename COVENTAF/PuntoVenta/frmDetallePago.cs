using Api.Model.ViewModels;
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
    public partial class frmDetallePago : Form
    {
        public List<ViewMetodoPago> metodoPago;
        public frmDetallePago()
        {
            InitializeComponent();
        }

        private void frmDetallePago_Load(object sender, EventArgs e)
        {
            this.dgvDetallePago.DataSource = metodoPago;
        }
    }
}
