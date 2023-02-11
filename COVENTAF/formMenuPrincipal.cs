using Api.Context;
using Api.Model.Modelos;
using Api.Model.ViewModels;
using Api.Setting;
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
    public partial class formMenuPrincipal : Form
    {
        private readonly frmLogIn _formLogIn;
        //roles del usuario actual
        private readonly ResponseModel _rolesUsuarioActual;
        private readonly RolesDelSistema _rolesDelSistema;
        //var _db = new CoreDBContext();
        //IAuthService _authService = new AuthService(_db);
        //LoginController _loginController = new LoginController(_authService);



        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();

        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hWnd, int wMsg, int wParam, int lParam);

        //Constructor
        public formMenuPrincipal(ResponseModel responseModel)
        {
            
            InitializeComponent();
            //panel1.BackColor = Color.FromArgb(125, Color.MediumSlateBlue);
           

            this._rolesUsuarioActual = responseModel;
            //instanciar la clase roles del sistema y pasar por parametro los roles
            this._rolesDelSistema = new RolesDelSistema(this._rolesUsuarioActual);

       
            //Estas lineas eliminan los parpadeos del formulario o controles en la interfaz grafica (Pero no en un 100%)
            this.SetStyle(ControlStyles.ResizeRedraw, true);
            this.DoubleBuffered = true;
            //_user = new User();
            
            lblUsuario.Text = User.Usuario;

            this.lblInformacion.Text = $"Servidor: { ConectionContext.Server }.  Base de Datos: { ConectionContext.DataBase }";
           


        }




        //RESIZE METODO PARA REDIMENCIONAR/CAMBIAR TAMAÑO A FORMULARIO EN TIEMPO DE EJECUCION ----------------------------------------------------------
        private int tolerance = 12;
        private const int WM_NCHITTEST = 132;
        private const int HTBOTTOMRIGHT = 17;
        private Rectangle sizeGripRectangle;

        protected override void WndProc(ref Message m)
        {
            switch (m.Msg)
            {
                case WM_NCHITTEST:
                    base.WndProc(ref m);
                    var hitPoint = this.PointToClient(new Point(m.LParam.ToInt32() & 0xffff, m.LParam.ToInt32() >> 16));
                    if (sizeGripRectangle.Contains(hitPoint))
                        m.Result = new IntPtr(HTBOTTOMRIGHT);
                    break;
                default:
                    base.WndProc(ref m);
                    break;
            }
        }
        //----------------DIBUJAR RECTANGULO / EXCLUIR ESQUINA PANEL 
        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);
            var region = new Region(new Rectangle(0, 0, this.ClientRectangle.Width, this.ClientRectangle.Height));

            sizeGripRectangle = new Rectangle(this.ClientRectangle.Width - tolerance, this.ClientRectangle.Height - tolerance, tolerance, tolerance);

            region.Exclude(sizeGripRectangle);
            this.PanelContenedor.Region = region;
            this.Invalidate();
        }
        //----------------COLOR Y GRIP DE RECTANGULO INFERIOR
        protected override void OnPaint(PaintEventArgs e)
        {
            SolidBrush blueBrush = new SolidBrush(Color.FromArgb(244, 244, 244));
            e.Graphics.FillRectangle(blueBrush, sizeGripRectangle);

            base.OnPaint(e);
            ControlPaint.DrawSizeGrip(e.Graphics, Color.Transparent, sizeGripRectangle);
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        //Capturar posicion y tamaño antes de maximizar para restaurar
        int lx, ly;
        int sw, sh;


        private void btnMaximizar_Click(object sender, EventArgs e)
        {
            lx = this.Location.X;
            ly = this.Location.Y;
            sw = this.Size.Width;
            sh = this.Size.Height;
            btnMaximizar.Visible = false;
            btnRestaurar.Visible = true;
            this.Size = Screen.PrimaryScreen.WorkingArea.Size;
            this.Location = Screen.PrimaryScreen.WorkingArea.Location;
        }

        private void btnRestaurar_Click(object sender, EventArgs e)
        {
            btnMaximizar.Visible = true;
            btnRestaurar.Visible = false;
            this.Size = new Size(sw, sh);
            this.Location = new Point(lx, ly);
        }

        private void formMenuPrincipal_Load(object sender, EventArgs e)
        {
           
            btnMaximizar_Click(null, null);


            /*roles disponible para postventa*/
            var rolesDisponibleParaPostVenta = new List<string>() { "ADMIN", "CAJERO", "SUPERVISOR" };
            this.btnPuntoVenta.Enabled = this._rolesDelSistema.TieneAccesoSistema(rolesDisponibleParaPostVenta);

            var rolesDisponibleParaSeguridad = new List<string>() { "ADMIN" };
            this.btnSeguridad.Enabled = this._rolesDelSistema.TieneAccesoSistema(rolesDisponibleParaSeguridad);
        }

        private void btnMinimizar_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void btnSeguridad_Click(object sender, EventArgs e)
        {
            AbrirFormulario<frmListaUsuarios>();
        }

        private void btnPuntoVenta_Click(object sender, EventArgs e)
        {
            this.panelMenu.Visible = false;
           
            //_rolesDelSistema, _user
            AbrirFormulario<frmPuntoVenta>();
            //AbrirFormulario<FrmVentas>();

        }

        private void btnMenu_Click(object sender, EventArgs e)
        {
            this.panelMenu.Visible = !this.panelMenu.Visible;
        }

        private void panelBarraTitulo_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panelBarraTitulo_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        private void AbrirFormulario<MiForm>() where MiForm : Form, new()
        {
            Form formulario;
            formulario = panelformularios.Controls.OfType<MiForm>().FirstOrDefault();//Busca en la colecion el formulario
                                                                                     //si el formulario/instancia no existe
            if (formulario == null)
            {
                formulario = new MiForm();
                formulario.TopLevel = false;                
                formulario.FormBorderStyle = FormBorderStyle.None;
                formulario.Dock = DockStyle.Fill;
                //formulario.LoadDatos();
                panelformularios.Controls.Add(formulario);
                panelformularios.Tag = formulario;
                formulario.Show();
                formulario.BringToFront();
            }
            //si el formulario/instancia existe
            else
            {
                formulario.BringToFront();
            }
        }



    }
}
