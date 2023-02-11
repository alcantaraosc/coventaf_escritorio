using Api.Model.Modelos;
using Api.Model.ViewModels;
using Api.Service.DataService;
using Controladores;
using COVENTAF.Security;
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
    public partial class frmListaUsuarios : Form
    {
        private readonly UsuarioController _securityUsuarioController;
        public frmListaUsuarios()
        {
            InitializeComponent();
            this._securityUsuarioController = new UsuarioController();

        }

        private void frmListaUsuarios_Load(object sender, EventArgs e)
        {
            //seleccionar el primero de la lista
            this.cboTipoConsulta.SelectedIndex = 0;
            //listar todos los usuarios
            LlenarListarUsuariosGrid();
        }


        async void LlenarListarUsuariosGrid()
        {
            var responseModel = new ResponseModel();
            try
            {
                responseModel = await this._securityUsuarioController.ListarUsuariosAsync();
                this.dgvListaUser.DataSource = null;
                this.dgvListaUser.DataSource = responseModel.Data;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        private void btnNuevoUsuario_Click(object sender, EventArgs e)
        {
            var model = new ViewModelSecurity();
            model.Usuarios = new Usuarios()
            {
                NuevoUsuario = true,
                Tipo = "",
                Req_Cambio_Clave = "N",
                Frecuencia_Clave = 0,
                Fecha_Ult_Clave = DateTime.Now,
                Max_Intentos_Conex = 5,
                Clave = "sinClave",
                Tipo_Acceso = "C",
                NoteExistsFlag = 0,
                RecordDate = DateTime.Now,
                CreatedBy = User.Usuario,
                UpdatedBy = User.Usuario
            };


            model.RolesUsuarios = new List<RolesUsuarios>();

            using (frmUsuario frmUser = new frmUsuario())
            {
                frmUser.model = model;
                frmUser.ShowDialog();
                LlenarListarUsuariosGrid();
            }
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private async void dgvListaUser_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (MessageBox.Show("¿Estas seguro de Editar los datos del Usuario ?", "Sistema COVENTAF", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                //obtener el login del usuario.
                int Index = dgvListaUser.CurrentRow.Index;
                string usuario = dgvListaUser.Rows[Index].Cells[0].Value.ToString();

                var model = new ViewModelSecurity();
                model.Usuarios = new Usuarios();
                model.RolesUsuarios = new List<RolesUsuarios>();
                //using (var _serviceSecurityUsuario = new ServiceSecurityUsuario())
                //{

                //}

                ResponseModel responseModel = new ResponseModel();
                responseModel = await _securityUsuarioController.ObtenerUsuarioPorIdAsync(usuario);

                //si la respuesta del servidor es 1 es exito
                if (responseModel.Exito == 1)
                {
                    model = responseModel.Data as ViewModelSecurity;

                    using (frmUsuario frmUser = new frmUsuario())
                    {
                        model.Usuarios.NuevoUsuario = false;
                        frmUser.model = model;
                        frmUser.Text = "Editar datos del Usuario";
                        frmUser.ShowDialog();
                        LlenarListarUsuariosGrid();
                    }

                }
                else
                {
                    MessageBox.Show(responseModel.Mensaje, "Sistema COVENTAF", MessageBoxButtons.OK);
                }

            }
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            var responseModel = new ResponseModel();
            responseModel = this._securityUsuarioController.ObtenerDatosUsuarioPorFiltroX(this.cboTipoConsulta.Text, this.txtBusqueda.Text);
            if (responseModel.Exito == 1)
            {
                this.dgvListaUser.DataSource = null;
                this.dgvListaUser.DataSource = responseModel.Data;
            }
            else
            {
                MessageBox.Show(responseModel.Mensaje, "Sistema COVENTAF");
            }
        }

        private void btnRefrescar_Click(object sender, EventArgs e)
        {
            LlenarListarUsuariosGrid();
        }

        private void cboTipoConsulta_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.txtBusqueda.Focus();
        }
    }
}
