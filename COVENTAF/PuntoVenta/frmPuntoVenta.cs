using Api.Context;
using Api.Model.Modelos;
using Api.Model.ViewModels;
using Controladores;
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

namespace COVENTAF.PuntoVenta
{
    public partial class frmPuntoVenta : Form
    {
        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();


        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hwnd, int wmsg, int wparam, int lparam);

        private readonly FacturaController _facturaController;
        public RolesDelSistema _rolesDelSistema;


        //clase para enviar el filtro.
        FiltroFactura filtroFactura = new FiltroFactura();

        CoreDBContext _db = new CoreDBContext();
        private readonly CajaPosController _cajaPosController;

        public frmPuntoVenta()
        {
            InitializeComponent();
            this._facturaController = new FacturaController();
            this._cajaPosController = new CajaPosController();
        }


        private void btnNuevo_Click(object sender, EventArgs e)
        {

        }

           /*  1- El cajero se logea y obtiene el informacion de la tienda que esta relacionado
            *  2-cuando el cajero haga apertura de caja entonces el sistema le asigna la bodega que tienen la caja.
            *  3-si el cajero ya hizo apertura de caja entonces el sistema obtiene la bodega
             */

        private void frmPuntoVenta_Load(object sender, EventArgs e)
        {

            //seleccionar el primer index de la lista del combox tipo de filtro
            this.cboTipoFiltro.SelectedIndex = 0;
                      

            VerificarsiExisteAperturaCaja();

            //asignar los valores por defectos para iniciar el form
            filtroFactura.Busqueda = User.ConsecCierreCT;
            filtroFactura.FechaInicio = this.dtpFechaInicio.Value;
            filtroFactura.FechaFinal = this.dtpFechaFinal.Value;
            filtroFactura.Tipofiltro = this.cboTipoFiltro.Text;
            filtroFactura.Cajero = User.Usuario;


            //listar las facturas en el Grid
            onListarGridFacturas(filtroFactura);                        
        }

        private void VerificarsiExisteAperturaCaja()
        
        {
            ResponseModel responseModel = new ResponseModel();
            responseModel = _cajaPosController.VerificarsiExisteAperturaCaja(User.Usuario, User.TiendaID);
            if (responseModel.Exito == 1)
            {
                this.btnNuevaFactura1.Enabled = true;
                var cierre_Pos = responseModel.Data as Cierre_Pos;
                User.Caja = cierre_Pos.Caja;
                User.ConsecCierreCT = cierre_Pos.Num_Cierre;
                //asignar la bodega encontrado 
                User.BodegaID = responseModel.DataAux as string;

                this.lblCajaApertura.Text = "Caja de Apertura: " + User.Caja;
                this.lblNoCierre.Text = "No. Cierre: " + User.ConsecCierreCT;
                this.btnAperturaCaja.Enabled = false;
            }
            else
            {
                this.btnNuevaFactura1.Enabled = false;
                this.btnAperturaCaja.Enabled = true;
                this.btnCierreCaja.Enabled = false;
            }
        }

        private async void onListarGridFacturas(FiltroFactura filtroFactura)
        {
            var responseModel = new ResponseModel();
            responseModel = await this._facturaController.ListarFacturas(filtroFactura);
            this.dgvPuntoVenta.DataSource = responseModel.Data;
        }

        //evento para seleccionar el tipo de filtro
        private void cboTipoFiltro_SelectedIndexChanged(object sender, EventArgs e)
        {

            switch (this.cboTipoFiltro.Text)
            {
                case "Factura del dia":
                    this.txtBusqueda.Enabled = false;

                    break;
                case "Recuperar Factura":
                    this.txtBusqueda.Enabled = true;

                    break;
                case "No Factura":
                    this.txtBusqueda.Enabled = true;
                    break;
                case "Devolucion":
                    this.txtBusqueda.Enabled = true;

                    break;
                case "Rango de Fecha":
                    this.txtBusqueda.Visible = false;
                    this.btnBuscar.Visible = false;
                    this.dtpFechaInicio.Visible = true;
                    this.dtpFechaFinal.Visible = true;

                    break;
            }

            this.txtBusqueda.Focus();
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {

        }

        private void btnNuevaFactura_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult;
            using (var frmMessageBox = new frmMessageBox("¿ Estas seguro de crear una nueva factura ?"))
            {
                frmMessageBox.ShowDialog();
                dialogResult = frmMessageBox.respuesta;
            }

            if (dialogResult == DialogResult.Yes)
            {
                var frm = new frmVentas();
                frm.ShowDialog();
            }
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnAperturaCaja_Click(object sender, EventArgs e)
        {
            var frmAperturaCaja = new frmAperturaCaja();
            frmAperturaCaja.ShowDialog();
            if (frmAperturaCaja.ExitoAperturaCaja)
            {
                this.lblCajaApertura.Text = "Caja de Apertura: " + User.Caja;
                this.lblNoCierre.Text = "No. Cierre: " + User.ConsecCierreCT;
                //desactivar la opcion de caja de apertura
                this.btnAperturaCaja.Enabled = false;
                this.btnCierreCaja.Enabled = true;
                this.btnNuevaFactura1.Enabled = true;

            }
            //liberar recurso del form
            frmAperturaCaja.Dispose();
        }

        private void btnNuevaFactura1_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult;
            using (var frmMessageBox = new frmMessageBox("¿ Estas seguro de crear una nueva factura ?"))
            {
                frmMessageBox.ShowDialog();
                dialogResult = frmMessageBox.respuesta;
            }

            if (dialogResult == DialogResult.Yes)
            {
                /* NuevaFactura();
                 var frm = new frmVentas();
                 frm.ShowDialog();
                 //liberar recurso
                 frm.Dispose();

                 //asignar los valores por defectos para iniciar el form
                 filtroFactura.Busqueda = "";
                 filtroFactura.FechaInicio = this.dtpFechaInicio.Value;
                 filtroFactura.FechaFinal = this.dtpFechaFinal.Value;
                 filtroFactura.Tipofiltro = this.cboTipoFiltro.Text;
                 filtroFactura.Cajero = User.Usuario;

                 //listar las facturas en el Grid
                 onListarGridFacturas(filtroFactura);*/
                NuevaFactura();

            }                        
        }

        private void NuevaFactura()
        {
            bool facturaGuardada = false;
            var frm = new frmVentas();
            frm.ShowDialog();
            facturaGuardada = frm.facturaGuardada;                      

            //liberar recurso
            frm.Dispose();

            //asignar los valores por defectos para iniciar el form
            filtroFactura.Busqueda = "";
            filtroFactura.FechaInicio = this.dtpFechaInicio.Value;
            filtroFactura.FechaFinal = this.dtpFechaFinal.Value;
            filtroFactura.Tipofiltro = this.cboTipoFiltro.Text;
            filtroFactura.Cajero = User.Usuario;

            //listar las facturas en el Grid
            onListarGridFacturas(filtroFactura);

            //si la factura se guardo correctamente entonces vuelvo a llamar a la ventana ventas
            if (facturaGuardada)
            {
                NuevaFactura();
            }

        }

        private void btnCierreCaja_Click(object sender, EventArgs e)
        {
            var frmCierreCaja = new frmCierreCaja();
            frmCierreCaja.ShowDialog();
            if (frmCierreCaja.exitoCierreCaja)
            {
                this.lblCajaApertura.Text = "Caja de Apertura: Sin Apertura";
                this.lblNoCierre.Text = "No. Cierre: ";
                //desactivar la opcion de caja de apertura
                this.btnAperturaCaja.Enabled = true;
                this.btnCierreCaja.Enabled = false;
                this.btnNuevaFactura1.Enabled = false;

            }
            //liberar recurso del form
            frmCierreCaja.Dispose();
        }

        
    }
}
