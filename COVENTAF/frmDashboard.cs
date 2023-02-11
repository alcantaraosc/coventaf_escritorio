using Api.Model.ViewModels;
using COVENTAF.PuntoVenta;
using COVENTAF.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace COVENTAF
{
    public partial class frmDashboard : Form
    {
        private readonly frmLogIn _formLogIn;
        //roles del usuario actual
        private readonly ResponseModel _rolesUsuarioActual;
        private readonly RolesDelSistema _rolesDelSistema;
        public string user = "";

        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();
        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hwnd, int wmsg, int wparam, int lparam);
        
        public frmDashboard(frmLogIn frmlogIn, ResponseModel responseModel)
        {
            InitializeComponent();
            this._formLogIn = frmlogIn;
            this._rolesUsuarioActual = responseModel;
            //instanciar la clase roles del sistema y pasar por parametro los roles
            this._rolesDelSistema = new RolesDelSistema(this._rolesUsuarioActual);
        }

        private void frmDashboard_Load(object sender, EventArgs e)
        {
            /*roles disponible para postventa*/
            var rolesDisponibleParaPostVenta = new List<string>() {  "ADMIN", "CAJERO", "SUPERVISOR" };
            this.btnPostVenta.Enabled = this._rolesDelSistema.TieneAccesoSistema(rolesDisponibleParaPostVenta);

            var rolesDisponibleParaSeguridad = new List<string>() { "ADMIN" };
            this.btnSeguridad.Enabled = this._rolesDelSistema.TieneAccesoSistema(rolesDisponibleParaSeguridad);
        }


        private void btnCerrar_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnMaximizar_Click(object sender, EventArgs e)
        {
            this.btnMaximizar.Visible = false;
            this.btnRestaurar.Visible = true;
            this.WindowState = FormWindowState.Maximized;

        }

        private void btnRestaurar_Click(object sender, EventArgs e)
        {
            btnRestaurar.Visible = false;
            btnMaximizar.Visible = true;
            this.WindowState = FormWindowState.Normal;
        }

        private void btnMinimizar_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void tmOcultarMenu_Tick(object sender, EventArgs e)
        {
            if (this.PanelMenu.Width <= 60)
            {
                this.tmOcultarMenu.Enabled = false;
                this.btnPostVenta.Text = "";
            }
            else
            {
                this.PanelMenu.Width = PanelMenu.Width - 20;
            }

        }

        private void tmMostrarMenu_Tick(object sender, EventArgs e)
        {
            if (this.PanelMenu.Width >= 220)
            {
                this.tmMostrarMenu.Enabled = false;
                this.btnPostVenta.Text = "         Consulta GifCard";

            }
            else
            {
                this.PanelMenu.Width = PanelMenu.Width + 20;
            }
        }

        private void btnMenu_Click(object sender, EventArgs e)
        {
            if (PanelMenu.Width == 220)
            {
                this.tmOcultarMenu.Enabled = true;
            }
            else if (this.PanelMenu.Width == 60)
            {
                this.tmMostrarMenu.Enabled = true;
            }
        }
        /*
        private void AbrirFormHija(object formHija)
        {
            if (this.panelContenedor.Controls.Count > 0)
            {
                this.panelContenedor.Controls.RemoveAt(0);
            }
            Form fh = formHija as Form;
            fh.TopLevel = false;
            fh.Dock = DockStyle.Fill;
            this.panelContenedor.Controls.Add(fh);
            fh.Show();
        }*/




        private void btnUsuario_Click(object sender, EventArgs e)
        {
            //AbrirFormHija(new RegUsuario());
            //RegUsuario newMDIChild = new RegUsuario();
            //// Set the Parent Form of the Child window.
            //newMDIChild.MdiParent = this;
            //// Display the new form.
            //newMDIChild.Show();
        }

        private void pictureBox1_LoadCompleted(object sender, AsyncCompletedEventArgs e)
        {

        }

  


        private void ConfigurarAccesoSistema()
        {
          
        }

         



  

        private void btnSalir_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void barraTitulo_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        private void lblTitulo_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        /* private void label1_MouseDown(object sender, MouseEventArgs e)
         {
             ReleaseCapture();
             SendMessage(this.Handle, 0x112, 0xf012, 0);
         }
        */

        private void FrmDasboard_FormClosing(Object sender, FormClosingEventArgs e)
        {

            System.Text.StringBuilder messageBoxCS = new System.Text.StringBuilder();
            messageBoxCS.AppendFormat("{0} = {1}", "CloseReason", e.CloseReason);
            messageBoxCS.AppendLine();
            messageBoxCS.AppendFormat("{0} = {1}", "Cancel", e.Cancel);
            messageBoxCS.AppendLine();
            MessageBox.Show(messageBoxCS.ToString(), "FormClosing Event");
        }

        private void btnSeguridad_Click(object sender, EventArgs e)
        {
            //AbrirFormHija(new RegUsuario());
            var formListaUsuarios = new frmListaUsuarios();
            // Set the Parent Form of the Child window.
            formListaUsuarios.MdiParent = this;
            // Display the new form.
            formListaUsuarios.Show();
        }

        private void BtnPostVenta_Click(object sender, EventArgs e)
        {
            //AbrirFormHija(new RegUsuario());
            //frmConsultarGifCard newMDIChild = new frmConsultarGifCard();
            //// Set the Parent Form of the Child window.
            //newMDIChild.MdiParent = this;
            //// Display the new form.
            //newMDIChild.Show();

            //AbrirFormHija(new RegUsuario());
            var PuntoDeVenta = new frmPuntoVenta();
            PuntoDeVenta.WindowState = FormWindowState.Maximized;
            // Set the Parent Form of the Child window.
            PuntoDeVenta.MdiParent = this;
            // Display the new form.
            PuntoDeVenta.Show();
        }
    }

}
