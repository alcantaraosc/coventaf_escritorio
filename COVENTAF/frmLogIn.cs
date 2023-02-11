using Api.Model.Modelos;
using Api.Model.Request;
using Api.Model.View;
using Api.Model.ViewModels;
using Api.Setting;
using Controladores;
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
    public partial class frmLogIn : Form
    {
        private readonly LoginController _loginController;
       
        public frmLogIn()
        {
            InitializeComponent();
            this._loginController = new LoginController();
        }

        private void btnLogIn_Click(object sender, EventArgs e)
        {
            /*
            if (this.txtUser.Text.Length == 0)
            {
                MessageBox.Show("Ingrese el Usuario");
                return;
            }
            else if (this.txtPassword.Text.Length == 0)
            {
                MessageBox.Show("Ingrese el password");
                return;
            }


            var crendenciales = new AuthRequest() { Usuario = this.txtUser.Text, Password = this.txtPassword.Text };
            var responseModel = new ResponseModel();

            responseModel = await this._loginController.Authenticate(crendenciales);
            if (responseModel.Exito == 1)
            {
                //ocultar el form de Login
                this.Hide();
                //var formDashboard = new frmDashboard(this, responseModel );
                //formDashboard.Show();
                var formDashboard = new formMenuPrincipal(responseModel);
                //formDashboard.user = this.txtUser.Text;
                formDashboard.Show();
            }
            else
            {
                MessageBox.Show(responseModel.Mensaje);
            }*/
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
