using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace COVENTAF
{
    public partial class frmMessageBox : Form
    {
   
        public DialogResult respuesta;
        public frmMessageBox( string mensaje, string barraTitulo="Sistema COVENTAF")
        {
            InitializeComponent();
            
            this.lblMensaje.Text = mensaje;
            this.lblTituloBarraTarea.Text = barraTitulo;
        }

        private void MessageBox_Load(object sender, EventArgs e)
        {

        }

     

        private void btnYes_Click(object sender, EventArgs e)
        {
            respuesta = DialogResult.Yes;
        }
    }
}
