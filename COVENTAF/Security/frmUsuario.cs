using Api.Model.Modelos;
using Api.Model.ViewModels;
using Api.Service.DataService;
using Controladores;
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

namespace COVENTAF.Security
{
    public partial class frmUsuario : Form
    {
        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();

        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hWnd, int wMsg, int wParam, int lParam);
        
        private readonly RolesController _securityRolesController;
        private readonly UsuarioController _securityUsuarioController;
        private readonly GrupoController _grupoController;

        public Usuarios usuario = new Usuarios();
        public List<Roles> roles = new List<Roles>();
        public ViewModelSecurity model;
        private string codigoGrupo = "";
        
       /* var modelUserRol = new ViewModelSecurity();
        modelUserRol. = new Usuarios();
        modelUserRol.RolesUsuarios = new List<RolesUsuarios>();*/


        public frmUsuario()
        {
            InitializeComponent();
            _securityRolesController = new RolesController();
            _securityUsuarioController = new UsuarioController();
            _grupoController = new GrupoController();
            model = new ViewModelSecurity();
            model.Usuarios = new Usuarios();
            model.RolesUsuarios = new List<RolesUsuarios>();
        }

        private void frmUsuario_Load(object sender, EventArgs e)
        {
            //this.tbpDatosUsuario.BackColor=  System.Drawing.Color.FromArgb(58, 52, 95);
            listarGrupo();
            llenarGridRoles();
            
            if (model.Usuarios.NuevoUsuario)
            {
                this.chkActivo.Checked = true;
                this.txtUsuario.SelectAll();
                this.txtUsuario.Focus();
            }
            else
            {
                this.txtUsuario.ReadOnly = true;
                this.lblTitulo.Text = "Editar Datos del Usuario";
                asignarDatos();
            }
           // txtUsuario.DataBindings.Add("Text", usuario.Usuario, "usuario.Usuario");
        }

        void asignarDatos()
        {
            ///asignar los datos del usuario
            this.txtUsuario.Text = model.Usuarios.Usuario;
            this.txtNombreUsuario.Text = model.Usuarios.Nombre;
            this.txtCorreoElectronico.Text = model.Usuarios.Correo_Electronico;
            this.chkActivo.Checked = model.Usuarios.Activo == "S" ? true : false;
            codigoGrupo = model.Usuarios.Grupo is null ? "" : model.Usuarios.Grupo;
            this.txtPassword.Text = model.Usuarios.ClaveCifrada;
            this.txtConfirmarPassword.Text = model.Usuarios.ClaveCifrada;
                                   
        }




        //obtener la lista de roles
        async void llenarGridRoles()
        {
            var responseModel = await _securityRolesController.ListarRolesAsync();
            if (responseModel.Exito == 1)
            {
                roles = responseModel.Data as List<Roles>;
                foreach(var rol in roles)
                {
                    dgvRoles.Rows.Add(rol.RolID, rol.NombreRol);
                }

                //comprobar si esta editando el usuario.
                if (!model.Usuarios.NuevoUsuario)
                {
                    //asignar los roles del usuario
                    foreach (var rolesAsignados in model.RolesUsuarios)
                    {
                        this.dgvRolesAsignados.Rows.Add(rolesAsignados.RolID, rolesAsignados.NombreRol);

                        //quitar de la lista los roles que ya estan asignado.
                        for (int index = 0; index < dgvRoles.RowCount; ++index)
                        {
                            //comprobar si el rolId es igual al rolID de los roles asignados
                            if (rolesAsignados.RolID.ToString() == dgvRoles.Rows[index].Cells[0].Value.ToString())
                            {
                                //quitar el rol de la lista
                                dgvRoles.Rows.RemoveAt(index);
                                break;
                            }
                        }
                    }
                    //
                }


            }

        }


       private async void listarGrupo()
        {
            var responseModel = new ResponseModel();
            responseModel = await _grupoController.ListarGruposAsync();
       
            if (responseModel.Exito == 1)
            {
                this.cboGrupo.ValueMember = "Grupo";
                this.cboGrupo.DisplayMember = "Descripcion";                
                this.cboGrupo.DataSource=responseModel.Data as List<Grupos>;
                this.cboGrupo.SelectedValue = codigoGrupo;
            }
           
        }


        private void tbpDatosUsuario_Click(object sender, EventArgs e)
        {
            //activar el color del roles de usuario
           /* this.tbpRolesUsuario.BackColor = System.Drawing.Color.FromArgb(97, 92, 133);
            this.tbpDatosUsuario.ForeColor = Color.White;
            this.pnlRolesUsuario.Visible = false;

            //cambiar el color de fondo del boton del usuario
            this.tbpDatosUsuario.BackColor = System.Drawing.Color.FromArgb(58, 52, 9); 
            this.tbpDatosUsuario.ForeColor = Color.Gold;
            this.pnlDatosUsuario.Visible = true;     */       
        }


        private void tbpRolesUsuario_Click(object sender, EventArgs e)
        {
            //cambiar el color de fondo del boton del usuario
          /*  this.tbpDatosUsuario.BackColor = System.Drawing.Color.FromArgb(97, 92, 133);
            this.tbpDatosUsuario.ForeColor = Color.White;
            this.pnlDatosUsuario.Visible = false;

            //activar el color del roles de usuario
            this.tbpRolesUsuario.BackColor = System.Drawing.Color.FromArgb(58, 52, 95);
            this.tbpRolesUsuario.ForeColor = Color.Gold;
            this.pnlRolesUsuario.Visible = true;*/

        }

        private void barraTitulo_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnMinimizar_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void btnLeft_Click(object sender, EventArgs e)
        {
            //validar que el datagrid de roles tenga roles.
            if (dgvRoles.RowCount >0)
            {
                //obtener el valor seleccionado del grid
                var numeroRol = dgvRoles.Rows[dgvRoles.CurrentRow.Index].Cells[0].Value.ToString();
                var nombreRol = dgvRoles.Rows[dgvRoles.CurrentRow.Index].Cells[1].Value.ToString();

                //asignar el rol
                this.dgvRolesAsignados.Rows.Add(numeroRol, nombreRol);
                //eliminar el rol del grid Rol 
                dgvRoles.Rows.RemoveAt(dgvRoles.CurrentRow.Index);
            }
        }

        private void btnRight_Click(object sender, EventArgs e)
        {
            //validar que el datagrid de roles asignados tenga roles.
            if (dgvRolesAsignados.RowCount > 0)
            {
                //obtener el valor seleccionado del grid
                var rolID = dgvRolesAsignados.Rows[dgvRolesAsignados.CurrentRow.Index].Cells[0].Value.ToString();
                var nombreRol = dgvRolesAsignados.Rows[dgvRolesAsignados.CurrentRow.Index].Cells[1].Value.ToString();

                //asignar el rol
                this.dgvRoles.Rows.Add(rolID, nombreRol);
                //eliminar el rol del grid de Rol asignado
                dgvRolesAsignados.Rows.RemoveAt(dgvRolesAsignados.CurrentRow.Index);
            }
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            onGuardar();
        }

        //boton guardar de la ventana modal usuario
        async void onGuardar()
        {
            var modelSecurity = new ViewModelSecurity();
            modelSecurity.Usuarios = new Usuarios();
            modelSecurity.RolesUsuarios = new List<RolesUsuarios>();

            modelSecurity.Usuarios.Usuario = this.txtUsuario.Text;
            modelSecurity.Usuarios.NuevoUsuario = model.Usuarios.NuevoUsuario;
            modelSecurity.Usuarios.Nombre = txtNombreUsuario.Text;
            modelSecurity.Usuarios.Tipo = model.Usuarios.Tipo;
            modelSecurity.Usuarios.Activo = (this.chkActivo.Checked ? "S" : "N");
            modelSecurity.Usuarios.Req_Cambio_Clave = model.Usuarios.Req_Cambio_Clave;
            modelSecurity.Usuarios.Frecuencia_Clave = model.Usuarios.Frecuencia_Clave;
            modelSecurity.Usuarios.Fecha_Ult_Clave = DateTime.Now;
            modelSecurity.Usuarios.Max_Intentos_Conex = model.Usuarios.Max_Intentos_Conex;
            modelSecurity.Usuarios.Clave = model.Usuarios.Clave;
            modelSecurity.Usuarios.Correo_Electronico = this.txtCorreoElectronico.Text;
            modelSecurity.Usuarios.Tipo_Acceso = model.Usuarios.Tipo_Acceso;
            modelSecurity.Usuarios.NoteExistsFlag = model.Usuarios.NoteExistsFlag;
            modelSecurity.Usuarios.RecordDate = DateTime.Now;
            //modelSecurity.Usuarios.RowPointer = 
            modelSecurity.Usuarios.CreatedBy = User.Usuario;
            modelSecurity.Usuarios.UpdatedBy = User.Usuario;
            modelSecurity.Usuarios.CreateDate = DateTime.Now;
            modelSecurity.Usuarios.ClaveCifrada = this.txtPassword.Text;
            modelSecurity.Usuarios.ConfirmarClaveCifrada = this.txtConfirmarPassword.Text;
            modelSecurity.Usuarios.Grupo = this.cboGrupo.SelectedValue.ToString();

            //roles usuarios


            for (var index = 0; index < this.dgvRolesAsignados.RowCount; ++index)
            {
                var datosd_ = new RolesUsuarios()
                {
                    RolID = Convert.ToInt32(dgvRolesAsignados.Rows[index].Cells[0].Value),
                    NombreRol = dgvRolesAsignados.Rows[index].Cells[1].Value.ToString(),
                    UsuarioID = this.txtUsuario.Text,                  
                    FechaModificacion = (model.Usuarios.NuevoUsuario ? null : DateTime.Now)
                };
                modelSecurity.RolesUsuarios.Add(datosd_);
            }


            //activar el boton Oportunidad
            //this.valorEstadoBotonRolUsuario = false;

            var mensaje = model.Usuarios.NuevoUsuario ? "¿ Estas seguro de guardar los Datos del usuario ?" : "¿ Estas seguro de actualizar los datos del usuario ?";
            if (MessageBox.Show(mensaje, "Sistema COVENTAF", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                var responseModel = model.Usuarios.NuevoUsuario ? await _securityUsuarioController.GuardarUsuarioAsync(modelSecurity) : await _securityUsuarioController.ActualizarUsuarioAsync(modelSecurity.Usuarios.Usuario, modelSecurity);

                if (responseModel.Exito == 1)
                {
                    MessageBox.Show(responseModel.Mensaje, "Sistema COVENTAF");
                    this.Close();
                }
                else
                {
                    MessageBox.Show(responseModel.Mensaje, "Sistema COVENTAF");
                }
            }
           
        }


      

    }
}
