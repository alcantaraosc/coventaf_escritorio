using Api.Model.Modelos;
using Api.Model.View;
using Api.Model.ViewModels;
using Controladores;
using COVENTAF.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace COVENTAF.PuntoVenta
{
    public partial class frmVentas : Form
    {
        public bool cancelarFactura = false;
        public bool facturaGuardada = false;


        #region codigo para mover pantalla
        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();

        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hWnd, int wMsg, int wParam, int lParam);
        #endregion

        #region logica para facturar
        //declaracion de las variables en una sola clase
        public varFacturacion listVarFactura = new varFacturacion();
        //campos del grid
        public List<DetalleFactura> listDetFactura = new List<DetalleFactura>();
        public List<Bodegas> listaBodega = new List<Bodegas>();
        public List<Forma_Pagos> listFormaPago = new List<Forma_Pagos>();
        public List<Tipo_Tarjeta_Pos> listaTipoTarjeta = new List<Tipo_Tarjeta_Pos>();
        public List<Condicion_Pagos> listaCondicionPago = new List<Condicion_Pagos>();
        public Clientes datosCliente = new Clientes();

        private int consecutivoActualFactura;
        private int columnaIndex;
        private decimal cantidadGrid;
        private decimal descuentoGrid;

        private bool AccederEventoCombox;

        private readonly ProcesoFacturacion _procesoFacturacion;
        #endregion

        private string TiendaID;
        private string BodegaID;
        private string NivelPrecio;
        private string MonedaNivel;

        private FacturaController _facturaController;
        private ClientesController _clienteController;
        private ArticulosController _articulosController;

        ViewModelFacturacion _modelFactura = new ViewModelFacturacion();

        public frmVentas()
        {
            InitializeComponent();
            this._facturaController = new FacturaController();
            this._clienteController = new ClientesController();
            this._articulosController = new ArticulosController();
            this._procesoFacturacion = new ProcesoFacturacion();
           
            //modelo de factura para guardar
            _modelFactura.Factura = new Facturas();
            _modelFactura.FacturaLinea = new List<Factura_Linea>();
            _modelFactura.PagoPos = new List<Pago_Pos>();


        }

        #region codigo del diseño del formulario
        //private void barraTitulo_Paint(object sender, PaintEventArgs e)
        //{
        //    ReleaseCapture();
        //    SendMessage(this.Handle, 0x112, 0xf012, 0);
        //}


        private void barraTitulo_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        private async void btnCerrar_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show($"¿ Estas seguro de abandonar factura {listVarFactura.NoFactura} ?", "Sistema COVENTAF", MessageBoxButtons.YesNo) == DialogResult.Yes )
            {
                ResponseModel responseModel = new ResponseModel();
                responseModel = await _facturaController.CancelarNoFacturaBloqueada(listVarFactura.NoFactura);
                cancelarFactura = true;
                this.Close();
            }
        }

        private void btnMaximizar_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
            this.btnMaximizar.Visible = false;
            this.btnRestaurar.Visible = true;
        }

        private void btnRestaurar_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Normal;
            this.btnRestaurar.Visible = false;
            this.btnMaximizar.Visible = true;
        }

        private void btnMinimizar_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }


        #endregion


        private void FrmVentas_Load(object sender, EventArgs e)
        {
            //this.WindowState = WindowState.

            TiendaID = User.TiendaID;
            BodegaID = User.BodegaID;
            NivelPrecio = User.NivelPrecio;
            MonedaNivel = User.MonedaNivel;

            //es una bandera para detener el evento al momento de iniciar el formulario
            AccederEventoCombox = false;
            //llenar los combox de la base de datos
            onLlenarCombox();
            //inicializar todas las variables de la facturacion
            _procesoFacturacion.InicializarTodaslasVariable(listVarFactura);

            //agregar una nueva fila
            AddNewRow(listDetFactura);

            // Initialize and bind the DataGridView.
            this.dgvDetalleFactura.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            this.dgvDetalleFactura.AutoGenerateColumns = true;
            //asignar la lista
            dgvDetalleFactura.DataSource = listDetFactura;

            listDetFactura.RemoveAt(0);
            dgvDetalleFactura.DataSource = null;
            dgvDetalleFactura.DataSource = listDetFactura;

            _procesoFacturacion.configurarDataGridView(this.dgvDetalleFactura);
           

            this.btnCobrar.Enabled = false;
            this.txtDescuentoGeneral.Enabled = this.chkDescuentoGeneral.Checked;                       
        }




        //agregar un registro en el arreglo
        void AddNewRow(List<DetalleFactura> listDetFactura)
        {

            //obtener el numero consecutivo del
            var numConsecutivo = listDetFactura.Count;
            var datosd_ = new DetalleFactura()
            {
                consecutivo = numConsecutivo,
                articuloId = "",
                inputArticuloDesactivado = true,
                codigoBarra = "",
                cantidad = 1.00M,
                unidad = "",
                descripcion = "",
                cantidadExistencia = 0,
                inputCantidadDesactivado = false,
                precioDolar = 0.00M,
                precioCordobas = 0.00M,
                moneda = '-',
                subTotalDolar = 0.00M,
                subTotalCordobas = 0.00M,
                porCentajeDescuentoXArticulo = 0.00M,
                descuentoInactivo = 0.00M,
                descuentoPorLineaDolar = 0.00M,
                descuentoPorLineaCordoba = 0.00M,
                MontoDescGeneralDolar = 0.0000M,
                MontoDescGeneralCordoba = 0.0000M,
                totalDolar = 0.00M,
                totalCordobas = 0.00M,
                inputActivoParaBusqueda = true,
                botonEliminarDesactivado = true
            };

            //agregar push para agregar un nuevo registro en los arreglos.
            listDetFactura.Add(datosd_);

        }

        /// <summary>
        /// llenar los combox de la factura
        /// </summary>
        public async void onLlenarCombox()
        {
            ListarDrownList responseModel = new ListarDrownList();
            try
            {
                responseModel = await _facturaController.llenarComboxFacturaAsync();

                if (responseModel.Exito == 1)
                {
                    listVarFactura.TipoDeCambio = Math.Round(responseModel.tipoDeCambio, 4);
                    this.lblNoFactura.Text =$"No. Factura: {responseModel.NoFactura}";
                    listVarFactura.NoFactura = responseModel.NoFactura;
                    this.lblFecha.Text = $"Fecha: {DateTime.Now.ToString("dd/MM/yyyy")}";
                    //asignar el tipo de cambio
                    this.lblTipoCambio.Text =$"Tipo Cambio: {responseModel.tipoDeCambio.ToString("N4")}";
                    //asignar la lista de bodega
                    this.listaBodega = responseModel.bodega;
                    //asignar la forma de pago
                    this.listFormaPago = responseModel.FormaPagos;
                    //asignar el tipo de tarjeta
                    this.listaTipoTarjeta = responseModel.TipoTarjeta;
                    //asignar las condiciones de pagos
                    this.listaCondicionPago = responseModel.CondicionPago;


                    //llenar el combox de la bodega
                    this.cboBodega.ValueMember = "Bodega";
                    this.cboBodega.DisplayMember = "Nombre";
                    this.cboBodega.DataSource = listaBodega;

                    //llenar el combox forma de pago
                    this.cboFormaPago.ValueMember = "Forma_Pago";
                    this.cboFormaPago.DisplayMember = "Descripcion";
                    this.cboFormaPago.DataSource = listFormaPago;

                    //llenar el combox tipo de tarjeta
                    this.cboTipoTarjeta.ValueMember = "Tipo_Tarjeta";
                    this.cboTipoTarjeta.DisplayMember = "Tipo_Tarjeta";
                    this.cboTipoTarjeta.DataSource = listaTipoTarjeta;

                    //llenar el combox condicion de pago
                    this.cboCondicionPago.ValueMember = "Condicion_Pago";
                    this.cboCondicionPago.DisplayMember = "Descripcion";
                    this.cboCondicionPago.DataSource = this.listaCondicionPago;
                    //asignar la bodega por defecto
                    this.cboBodega.SelectedValue = User.BodegaID;
                    AccederEventoCombox = true;
                                        

                    if (responseModel.tipoDeCambio == 0.0000M )
                    {
                        MessageBox.Show("El Tipo de cambio para hoy no se encontro en la base de datos", "Sistema COVENTAF");                        
                    }

                    this.txtCodigoCliente.SelectionStart = 0;
                    this.txtCodigoCliente.SelectionLength = this.txtCodigoCliente.Text.Length;
                    this.txtCodigoCliente.Focus();
                }
                else
                {
                    MessageBox.Show("Los Datos Principales para facturar no se pudieron cargar");
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Sistema COVENTAF");
            }
        }


        //evento KeyPress para buscar el codigo del cliente.
        private async void txtCodigoCliente_KeyPress(object sender, KeyPressEventArgs e)
        {
            //comprobar si presionaste la tecla enter
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                var responseModel = new ResponseModel();
                responseModel = await this._clienteController.ObtenerClientePorIdAsync(this.txtCodigoCliente.Text);

                if (responseModel.Exito == 1)
                {
                    datosCliente = responseModel.Data as Clientes;
                    //asignar los datos del cliente
                    _procesoFacturacion.asignarDatoClienteParaVisualizarHtml(datosCliente, listVarFactura);


                    this.txtNombreCliente.Text = datosCliente.Nombre;
                    this.txtDisponibleCliente.Text = "C$ " + Convert.ToDecimal(datosCliente.U_SaldoDisponible).ToString("N2");
                    this.txtDescuentoCliente.Text = Convert.ToDecimal(datosCliente.U_Descuento).ToString("N2");
                    
                    //desactivar el input de busqueda de cliente
                    this.txtCodigoCliente.Enabled = false;
                    //poner el focus en el textboxarticulo              
                    this.txtCodigoBarra.Focus();
                }
                //en caso que exito sea 0 (0= no se encontro el cliente en la base de datos)              
                else if (responseModel.Exito == 0)
                {

                    //inicializar los datos del cliente para luego mostrarlo en HTML
                    _procesoFacturacion.inicializarDatosClienteParaVisualizarHTML(listVarFactura);
                    MessageBox.Show(responseModel.Mensaje, "Sistema COVENTAF");
                }
                else
                {
                    MessageBox.Show(responseModel.Mensaje, "Sistema COVENTAF");
                }

            }
        }

        //buscar el articulo en la base de datos
        private async void onBuscarArticulo()
        {
  
            //obtener el codigo de barra del datgrid
            var codigoArticulo = txtCodigoBarra.Text;//dgvDetalleFactura.Rows[consecutivoActualFactura].Cells[3].Value.ToString();
            var responseModel = new ResponseModel();

            try
            {
                responseModel = await this._articulosController.ObtenerArticuloPorIdAsync(codigoArticulo, this.cboBodega.SelectedValue.ToString(), User.NivelPrecio);
                //respuesta exitosa
                if (responseModel.Exito == 1)
                {

                    ViewArticulo articulo = new ViewArticulo();
                    //obtener los datos de la vista del articulo
                    articulo = responseModel.Data as ViewArticulo;
                    this.txtDescripcionArticulo.Text = articulo.Descripcion;

                    //comprobar si hay en existencia
                    if (articulo.Existencia > 0)
                    {
                        //agregar a la tabla del detalle de la factura
                        onAgregarArticuloDetalleFactura(articulo);
                        chkDescuentoGeneral.Enabled = true;
                        //poner el cursor en la siguiente celda de busqueda.
                        //dgvDetalleFactura.CurrentCell = dgvDetalleFactura.Rows[consecutivoActualFactura].Cells[4];

                    }
                    else
                    {
                        MessageBox.Show("No existe en el inventario el articulo", "Sistema COVENTAF");
                        LimpiarTextBoxBusquedaArticulo();
                    }
                }
                //si el servidor responde exito con 0 (0= el articulo no existe en la base de dato)         
                else if (responseModel.Exito == 0)
                {
                    //mostrar un mensaje de notificacion                  
                    MessageBox.Show(responseModel.Mensaje, "Sistema COVENTAF");
                    //limpiar el input del codigo del articulo
                    //let ipuntActual = document.getElementById(this.idActivo) as HTMLInputElement;
                    //ipuntActual.value = '';
                }
                else
                {
                    //notifica cualquier error que el servidor envia
                    MessageBox.Show(responseModel.Mensaje, "Sistema COVENTAF");
                    //limpiar el input del codigo del articulo
                    //let ipuntActual = document.getElementById(this.idActivo) as HTMLInputElement;
                    //ipuntActual.value = '';

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Sistema COVENTAF");
            }
           
        }



        //este evento se ejecuta cuando intenta cambiar un valor en la columna del grid
        private void dgvDetalleFactura_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            ////columna Cantidad
            //if (e.ColumnIndex == 4)
            //{
            //    //verifica si el valor es Double
            //    bool isDouble = double.TryParse(e.FormattedValue.ToString(), out double resultadoNumerico);

            //    if (isDouble)
            //    {

            //    }
            //    else
            //    {
            //        e.Cancel = true;
            //    }
            //}
        }



        //actualizar las cantidades
        void ActualizarCantidades(decimal cantidad)
        {

            //validar la existencia en inventario.
            if (listDetFactura[consecutivoActualFactura].cantidadExistencia < cantidad)
            {
                //poner el focus
                //document.getElementById(idActivo).focus();
                //aplicar un color de fondo a la fila

                MessageBox.Show("La cantidad digitada supera la existencia");
                return;
            }

            listDetFactura[consecutivoActualFactura].cantidad = cantidad;

            //hacer los calculos de totales
            onCalcularTotales();

            //guardarBaseDatosFacturaTemp();
        }

        //metodo para calcular los totales. la variable calculoIsAutomatico es automatico es para que el sistema tome desiciones 
        void onCalcularTotales(bool calculoIsAutomatico = true)
        {

            bool calcularOtraVez;

            do
            {
                //es una bandera para detener el ciclo
                calcularOtraVez = false;
                //inicializar valor de la       
                _procesoFacturacion.InicializarVariableTotales(listVarFactura);
                /* subTotalDolar=0.0000; subTotalCordoba=0.0000;
                 //let descuento:number=0.00;
                 descuentoDolar=0.0000; descuentoCordoba = 0.0000;    
                 subTotalDescuentoDolar=0.0000; subTotalDescuentoCordoba =0.0000;
                 ivaCordoba = 0.0000; ivaDolar= 0.0000;    
                 totalDolar= 0.0000; totalCordobas=0.0000;
                 totalUnidades=0; */

       
                foreach (var detfact in listDetFactura)
                {
                    #region calculos detallados por cada articulo
                    /***************************************************************** calculos detallados por cada articulo  *******************************************************************/
                    /*********************** cantidad por precios dolares y cordobas *************************************************************/
                    //cantidad * precio en Dolares  por cada fila
                    detfact.subTotalDolar = detfact.cantidad * detfact.precioDolar;
                    //precio cordobas 
                    detfact.subTotalCordobas = detfact.cantidad * detfact.precioCordobas;
                    /***************************************************************************************************************************/               

                    /*********************** descuento por cada articulo articulo en dolares y cordobas ****************************************************/
                    //asignar el descuento por cada fila para el descuentoDolar
                    detfact.descuentoPorLineaDolar = (detfact.subTotalDolar * (detfact.porCentajeDescuentoXArticulo / 100));
                    //asignar el descuento por cada fila para el descuentoCordoba
                    detfact.descuentoPorLineaCordoba = (detfact.subTotalCordobas * (detfact.porCentajeDescuentoXArticulo / 100));
                    /*************************************************************************************************************************/

                    /*********************** total (restando el descuento x articulo) por articulo en dolares y cordobas ****************************************************/
                    //la resta del subTotal menos y subTotal de descuento            
                    detfact.totalDolar = detfact.subTotalDolar - detfact.descuentoPorLineaDolar;
                    //la resta del subTotal menos y subTotal de descuento cordoba
                    detfact.totalCordobas = detfact.subTotalCordobas - detfact.descuentoPorLineaCordoba;
                    /*************************************************************************************************************************/

                    /************************ descuento general por linea dolares y cordobas ************************************************/
                    //aplicar el descuento general si existe.
                    detfact.MontoDescGeneralDolar = detfact.totalDolar * (listVarFactura.PorCentajeDescGeneral / 100.00M);
                    //aplicar el descuento general si existe. esto solo aplica para cordobas
                    detfact.MontoDescGeneralCordoba = detfact.totalCordobas * (listVarFactura.PorCentajeDescGeneral / 100.00M);
                    /***********************************************************************************************************************/
                    /***************************************************************** fin *******************************************************************/
                    #endregion


                    /********************** sub totales en cordobas y dolares  ****************************************************************/
                    //suma de los subTotales de la lista de articulos en dolares
                    listVarFactura.SubTotalDolar += detfact.totalDolar;
                    //suma de los subTotales de la lista de articulos en cordobas
                    listVarFactura.SubTotalCordoba += detfact.totalCordobas;
                    /*************************************************************************************************************************/


                    //yo lo desactive ahora 15/02/2023
                    /************************ descuento general por linea dolares y cordobas ************************************************/
                    //aplicar el descuento general si existe. esto solo aplica para cordobas
                    //detfact.descuentoGeneralDolar = detfact.subTotalDolar * (listVarFactura.AplicarDescuentoGeneral/100.00M);                    
                    //aplicar el descuento general si existe. esto solo aplica para cordobas
                    //detfact.descuentoGeneralCordoba = detfact.subTotalCordobas * (listVarFactura.AplicarDescuentoGeneral/100.00M);
                    /***********************************************************************************************************************/


                    ////obtener la suma de los descuento x cada articulo de la lista
                    //listVarFactura.DescuentoDolar += detfact.descuentoDolar  ;
                    ////obtener la suma de los descuento de la lista
                    //listVarFactura.DescuentoCordoba += detfact.descuentoCordoba;

            

           

                    /************************ sumar el descuento general (beneficio del cliente) dolares y cordobas ************************************************/
                    //sumar el descuento general en dolares
                    //listVarFactura.DescuentoGeneralDolar += detfact.descuentoGeneralDolar;
                    ////sumar el descuento general en cordobas
                    //listVarFactura.DescuentoGeneralCordoba += detfact.descuentoGeneralCordoba;
                    /***********************************************************************************************************************/

                    ////suma total dolar
                    //sumaTotalDolar += detfact.totalDolar;
                    ////suma total cordobas
                    //sumaTotalCordoba += detfact.totalCordobas;
                    
                    //sumar el total de las unidades
                    listVarFactura.TotalUnidades += detfact.cantidad;
                    
                    //establecer dos decimales a la variable de tipo decimal
                    detfact.cantidadExistencia = Math.Round(detfact.cantidadExistencia, 2);
                    detfact.precioDolar = Math.Round(detfact.precioDolar, 2);
                    detfact.precioCordobas = Math.Round(detfact.precioCordobas, 4);
                    detfact.subTotalDolar = Math.Round(detfact.subTotalDolar, 2);
                    detfact.subTotalCordobas = Math.Round(detfact.subTotalCordobas, 4);
                    detfact.porCentajeDescuentoXArticulo = Math.Round(detfact.porCentajeDescuentoXArticulo, 2);
                    detfact.descuentoInactivo = Math.Round(detfact.descuentoInactivo, 2);
                    detfact.descuentoPorLineaDolar = Math.Round(detfact.descuentoPorLineaDolar, 2);
                    detfact.descuentoPorLineaCordoba = Math.Round(detfact.descuentoPorLineaCordoba, 4);
                    detfact.MontoDescGeneralDolar = Math.Round(detfact.MontoDescGeneralDolar, 2);
                    detfact.MontoDescGeneralCordoba = Math.Round(detfact.MontoDescGeneralCordoba, 4);
                    detfact.totalDolar = Math.Round(detfact.totalDolar, 2);
                    detfact.totalCordobas = Math.Round(detfact.totalCordobas, 4);
                  
                }

                /******* TEXTBOX SUB TOTALES DOLARES Y CORDOBAS ***************************************************/
                this.txtSubTotalDolares.Text = $"U$ {listVarFactura.SubTotalDolar.ToString("N2") }";
                this.txtSubTotalCordobas.Text = $"C$ {listVarFactura.SubTotalCordoba.ToString("N2") }";
                /*****************************************************************************************************/

                /******* TEXTBOX DESCUENTO GENERAL  DOLAR Y CORDOBA ********************************************/
                //hacer el calculo para el descuento general
                listVarFactura.DescuentoGeneralDolar = listVarFactura.SubTotalDolar * (listVarFactura.PorCentajeDescGeneral / 100);
                listVarFactura.DescuentoGeneralCordoba = listVarFactura.SubTotalCordoba * (listVarFactura.PorCentajeDescGeneral / 100);

                this.txtDescuentoDolares.Text = $"U$ {listVarFactura.DescuentoGeneralDolar.ToString("N2")}";
                this.txtDescuentoCordobas.Text = $"C$ {listVarFactura.DescuentoGeneralCordoba.ToString("N2")}";
                /*****************************************************************************************************/


                /******* TEXTBOX SUB TOTAL DESCUENTO DOLARES Y CORDOBAS *************************************************/
                //restar del subtotal descuento Dolar - descuento del beneficio Dolar
                listVarFactura.SubTotalDescuentoDolar = listVarFactura.SubTotalDolar - listVarFactura.DescuentoGeneralDolar;
                ////restar del subtotal descuento Cordoba - descuento del beneficio Cordoba
                listVarFactura.SubTotalDescuentoCordoba = listVarFactura.SubTotalCordoba - listVarFactura.DescuentoGeneralCordoba;

                this.txtSubTotalDescuentoDolares.Text = $"U$ {listVarFactura.SubTotalDescuentoDolar.ToString("N2")}";
                this.txtSubTotalDescuentoCordoba.Text = $"C$ {listVarFactura.SubTotalDescuentoCordoba.ToString("N2")}";
                /*************************************************************************************************************/

                /******* TEXTBOX IVA DOLARES Y CORDOBAS **********************************************************************************/
                //llamar al metodo obtener iva para identificar si al cliente se le cobra iva
                listVarFactura.IvaDolar = listVarFactura.SubTotalDescuentoDolar * obtenerIVA(datosCliente);
                listVarFactura.IvaCordoba = listVarFactura.SubTotalDescuentoCordoba * obtenerIVA(datosCliente);
                this.txtIVADolares.Text = $"U$ {listVarFactura.IvaDolar.ToString("N2")}";
                this.txtIVACordobas.Text = $"C$ {listVarFactura.IvaCordoba.ToString("N2")}";
                /*************************************************************************************************************************/

                /*********************TEXTBOX TOTAL EN DOLARES Y CORDOBAS ****************************************************************/
                listVarFactura.TotalDolar = listVarFactura.SubTotalDescuentoDolar + listVarFactura.IvaDolar;
                listVarFactura.TotalCordobas = listVarFactura.SubTotalDescuentoCordoba + listVarFactura.IvaCordoba;

                this.txtTotalDolares.Text = "U$ " + listVarFactura.TotalDolar.ToString("N2");
                this.txtTotalCordobas.Text = "C$ " + listVarFactura.TotalCordobas.ToString("N2");
                /*************************************************************************************************************************/


            } while (calcularOtraVez);

            try
            {
                //actualizar el datagridView
                /*
                dgvDetalleFactura.DataSource = null;
                dgvDetalleFactura.DataSource = listDetFactura;*/
                LlenarGridviewDetalleFactura();
                _procesoFacturacion.configurarDataGridView(this.dgvDetalleFactura); 
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Sistema COVENTAF");
            }            
        }

        //actualiza el estado e intercambia con los descuento de los registro del detalle de factura
        void activarIntercambiarDescuentoLinea(varFacturacion listVarFactura, List<DetalleFactura> detallefact)
        {
            //verifico que el estado de la variable descuentoActivo este desactivada
            if (!listVarFactura.DescuentoActivo)
            {
                //activar
                listVarFactura.DescuentoActivo = true;
                //intercarmbiar los descuentos si existiera 
                setAcitvoOrDesactivoDescPorLinea(detallefact, listVarFactura.DescuentoActivo);
            }

        }

        //agregar el articulo al detalle de la factura
        private void onAgregarArticuloDetalleFactura(ViewArticulo articulo)
        {
            //comprobar si existe el articulo
            if (onExisteArticuloDetFactura(listDetFactura, articulo.CodigoBarra, articulo.BodegaID) == "NO_EXIST_ARTICULO")
            {
                //crear una nueva fila en el datagrid
                AddNewRow(listDetFactura);
                consecutivoActualFactura = dgvDetalleFactura.RowCount;


                //cantidad del articulo
                decimal cantidad = Convert.ToDecimal(listDetFactura[consecutivoActualFactura].cantidad);

                //precio del articulo
                decimal precioDolar = 0.0000M;
                decimal precioCordoba = 0.0000M;

                //si la moneda es Dolar
                if (articulo.Moneda == 'D')
                {
                    precioDolar = articulo.Precio;
                    precioCordoba = articulo.Precio * listVarFactura.TipoDeCambio;
                }                    
                else if (articulo.Moneda == 'L')
                {
                    //precio del articulo
                    precioCordoba = articulo.Precio;
                    precioDolar = articulo.Precio / listVarFactura.TipoDeCambio; 
                }
               
                /*
                //calculo del subTotal dolares
                decimal subTotalDolar = cantidad * precioDolar;
                //calculo del subTotal Cordoba
                decimal subTotalCordoba = cantidad * precioCordoba;
                //establecer el descuento del articulo o descuento 
                decimal Descuento = articulo.Descuento;//setDescuentoDetalleFactura(articulo.Descuento, listVarFactura.DescuentoActivo, chkDescuentoGeneral.Checked);

                //obtener el calculo del subtotal del descuento
                decimal descuentoDolar = subTotalDolar * Descuento;
                decimal descuentoCordoba = subTotalCordoba * Descuento;
                //let subTotalDescuentoDolar: number = subTotalDolar - descuentoDolar;
                //let subTotalDescuentoCordoba: number = subTotalCordoba - descuentoCordoba;
                decimal totalDolar = subTotalDolar - descuentoDolar;
                decimal totalCordobas = subTotalCordoba - descuentoCordoba;
                */
                

                //agregar a la lista los calculos realizados
                listDetFactura[consecutivoActualFactura].articuloId = articulo.ArticuloID;
                listDetFactura[consecutivoActualFactura].codigoBarra = articulo.CodigoBarra;
                //bloquear el input de busqueda
                listDetFactura[consecutivoActualFactura].inputArticuloDesactivado = true;
                //el siguiente
                listDetFactura[consecutivoActualFactura].descripcion = articulo.Descripcion;
                listDetFactura[consecutivoActualFactura].unidad = articulo.UnidadVenta;
                listDetFactura[consecutivoActualFactura].cantidad = cantidad;
                //existencia en inventario
                listDetFactura[consecutivoActualFactura].cantidadExistencia = articulo.Existencia;
                listDetFactura[consecutivoActualFactura].inputCantidadDesactivado = true;
                listDetFactura[consecutivoActualFactura].precioDolar = precioDolar;
                listDetFactura[consecutivoActualFactura].precioCordobas = precioCordoba;
                listDetFactura[consecutivoActualFactura].porCentajeDescuentoXArticulo = articulo.Descuento;
                listDetFactura[consecutivoActualFactura].descuentoInactivo = (listVarFactura.DescuentoActivo ? 0.00M : articulo.Descuento);
                //moneda D(Dolar) L(Local =Cordoba)
                listDetFactura[consecutivoActualFactura].moneda = articulo.Moneda;
                //bodega
                listDetFactura[consecutivoActualFactura].BodegaID = articulo.BodegaID;
                //nombre de bodega
                listDetFactura[consecutivoActualFactura].NombreBodega = articulo.NombreBodega;

                /*
                listDetFactura[consecutivoActualFactura].subTotalDolar = subTotalDolar;
                listDetFactura[consecutivoActualFactura].subTotalCordobas = subTotalCordoba;
                                               
                listDetFactura[consecutivoActualFactura].descuentoDolar = descuentoDolar;//articulo.precio * listDetFactura[consecutivoActualFactura].cantidad;  
                listDetFactura[consecutivoActualFactura].descuentoCordoba = descuentoCordoba;
                listDetFactura[consecutivoActualFactura].totalDolar = totalDolar; //listDetFactura[consecutivoActualFactura].subTotal - (listDetFactura[consecutivoActualFactura].subTotal * (articulo.descuento/100.00));
                listDetFactura[consecutivoActualFactura].totalCordobas = totalCordobas;*/

                listDetFactura[consecutivoActualFactura].botonEliminarDesactivado = false;
                //cambio el estado del input indicando que ya no es el input unico del articulo para buscar
                listDetFactura[consecutivoActualFactura].inputActivoParaBusqueda = false;

                //comprobar si no existe el input unico para el proximo articulo
                //if (!onExisteInputUnicoParaProximaArticulo(listDetFactura))
                //{


                //var inputCodigoBarra = document.getElementById(idActivo) as HTMLInputElement;
                //accedo al atributo para deshabilitar y desactivo el input de codigo de barra          
                //inputCodigoBarra.setAttribute("disabled", "false");
                //guardar la factura temporalmente                   
                guardarBaseDatosFacturaTemp();



                //sumar uno mas al consecutivo
                //consecutivoActualFactura += 1;
                //idActivo = 'codigoBarra_' + consecutivoActualFactura.toString();
                //activar la busqueda de inputActivoParaBusqueda
                //listDetFactura[consecutivoActualFactura].inputActivoParaBusqueda = true;
                //llenar el grid detalle Factura
                LlenarGridviewDetalleFactura();

                /*dgvDetalleFactura.DataSource = null;
                dgvDetalleFactura.DataSource = listDetFactura;
                configurarDataGridView();*/

                LimpiarTextBoxBusquedaArticulo();


                //}
            }
            else
            {

                /*
                //limpiar el textbox del grid
                dgvDetalleFactura.Rows[consecutivoActualFactura].Cells[3].Value = "";
                //poner el cursor en la siguiente celda de busqueda.
                dgvDetalleFactura.CurrentCell = dgvDetalleFactura.Rows[consecutivoActualFactura].Cells[3];*/
                LimpiarTextBoxBusquedaArticulo();

            }

            //hacer los calculos
            onCalcularTotales();
        }



        //comprobar si existe el articulo en detalle factura (datagrid)
        private string onExisteArticuloDetFactura(List<DetalleFactura> listDetFactura, string codigoBarra, string BodegaID)
        {
            int consecutivoLocalizado = 0;
            //esta variable indica si existe el articulo en la lista
            string resultado = "NO_EXIST_ARTICULO";

            foreach (var detFact in listDetFactura)
            {
                //comprobar si existe el codigo de barra en la lista y la bodega
                if (detFact.codigoBarra == codigoBarra && detFact.BodegaID ==BodegaID)
                {
                    consecutivoLocalizado = detFact.consecutivo;
                    //asignar el indice localizado de la lista.
                    consecutivoActualFactura = consecutivoLocalizado;

                    //asignar las cantidad 
                    decimal cantidad = detFact.cantidad + 1.00M;
                    if (detFact.cantidadExistencia >= cantidad)
                    {                                             
                        //sumarle un producto mas
                        detFact.cantidad = detFact.cantidad + 1.00M;
                        //obtener el subtotal en dolares
                        detFact.subTotalDolar = Convert.ToDecimal(detFact.precioDolar * detFact.cantidad);
                       
                        //obtener el subtotal en cordoba
                        detFact.subTotalCordobas = detFact.precioCordobas * detFact.cantidad;
                        detFact.totalDolar = detFact.subTotalDolar;
                        detFact.totalCordobas = detFact.subTotalCordobas;
                        detFact.totalCordobas = Math.Round(detFact.totalCordobas, 2);

                        //indicador para saber que existe el articulo en la lista
                        resultado = "ARTICULO_EXISTE";
                        //rompe el ciclo
                    }
                    else
                    {
                        dgvDetalleFactura.CurrentCell = dgvDetalleFactura[3, consecutivoActualFactura];
                        //'Pinta de color azul la fila para indicar al usuario que esa celda está seleccionada (Opcional)
                        dgvDetalleFactura.Rows[consecutivoActualFactura].Selected = true;

                        resultado = "NO_EXISTE_INVENTARIO";
                        MessageBox.Show("Este articulo se ha agotado", "Sistema COVENTAF");
                    }
                    break;
                }
            }

            if (resultado == "ARTICULO_EXISTE")
            {
                //actualizar la informacion del grid
                /*dgvDetalleFactura.DataSource = null;
                dgvDetalleFactura.DataSource = listDetFactura;*/

                LlenarGridviewDetalleFactura();
                //configurar el grid
                _procesoFacturacion.configurarDataGridView(this.dgvDetalleFactura);
                //guardar en base datos informacion del registro actual
                guardarBaseDatosFacturaTemp(consecutivoLocalizado);
            }

            return resultado;
        }

        private void LlenarGridviewDetalleFactura()
        {
            //limpiar el grid
            dgvDetalleFactura.DataSource = null;
            //dgvDetalleFactura.Columns.Clear();
            //asignar la lista detalle del articulo
            dgvDetalleFactura.DataSource = listDetFactura;
            //configurar Grid
            //configurarDataGridView();

            //dgvDetalleFactura.Columns["NombreDeUsuario"].Index;
            //comprobar si tiene registro la lista de detalle de factura
            if (listDetFactura.Count >0)
            {
                // 'Mueve el cursor a dicha fila               
                dgvDetalleFactura.CurrentCell = dgvDetalleFactura[4, consecutivoActualFactura];
                //'Pinta de color azul la fila para indicar al usuario que esa celda está seleccionada (Opcional)
                dgvDetalleFactura.Rows[consecutivoActualFactura].Selected = true;
            }
        }

        //poner el descuento
        //private decimal setDescuentoDetalleFactura(decimal descuento, bool descuentoActivo, bool activarDSD = false)
        //{
        //    decimal valorDescuento = 0.0000M;

        //    //si descuentoActivo es true y activar descuento sobre descuento (activarDSD) es true
        //    if ((descuentoActivo) || (activarDSD))
        //    {
        //        valorDescuento = (descuento / 100.0000M);
        //    }
        //    else if ((descuentoActivo) && !(activarDSD))
        //    {
        //        valorDescuento = 0.00M;
        //    }

        //    return valorDescuento;
        //}

        //activa o desactiva (intercambiar) los descuento por linea
        void setAcitvoOrDesactivoDescPorLinea(List<DetalleFactura> detallefact, bool descuentoActivo)
        {
            foreach (var detFactura in detallefact)
            {
                //comprobar si se van activar los descuentos est
                if (descuentoActivo)
                {
                    var descuentoTemp = detFactura.descuentoInactivo;
                    detFactura.descuentoInactivo = detFactura.porCentajeDescuentoXArticulo;
                    detFactura.porCentajeDescuentoXArticulo = descuentoTemp;
                }
                else
                {
                    var descuentoTemp = detFactura.porCentajeDescuentoXArticulo;
                    detFactura.porCentajeDescuentoXArticulo = detFactura.descuentoInactivo;
                    detFactura.descuentoInactivo = descuentoTemp;
                }

            }
        }

        //verificar el saldo Disponible
        bool montoDescuentoBeneficioIsOk(decimal saldoDisponible, decimal descuentoFactura)
        {
            if (descuentoFactura <= saldoDisponible)
                return true;
            else //if (descuentoFactura > saldoDisponible)
                return false;
        }

        //verificar si el cliente paga IVA
        private decimal obtenerIVA(Clientes datosCliente)
        {
            decimal IVA = 0.0000M;
            if (datosCliente.Codigo_Impuesto == "IVA")
            {
                IVA = 0.15M;
            }

            return IVA;
        }

        //verifico si existe en el detalle de factura el proximo input unico para ingresar el 
        private bool onExisteInputUnicoParaProximaArticulo(List<DetalleFactura> listDetFactura)
        {
            var existeInput = false;
            foreach (var detFact in listDetFactura)
            {
                //verifico si ya existe el unico input para hacer la busqueda en el detalle factura
                if (detFact.inputActivoParaBusqueda)
                {
                    //indico que existe el input unico en el detalle de factura.
                    existeInput = true;
                    //romper el ciclo
                    break;
                }
            }

            return existeInput;
        }

        //guardar el registro temporalmente mientra esta haciendo la factura
        private async void guardarBaseDatosFacturaTemp(int consecutivo = -1)
        {
            //si la variable del parametro es (-1) entoneces toma el valor de la variable consecutivoActualFactura
            //de lo contrario toma el valor que viene del parametro y ese sera el consecutivo
            var consecut = consecutivo == -1 ? consecutivoActualFactura : consecutivo;

            //comprobar si no si es inputUnicoSigArticulo activo
            if (!listDetFactura[consecut].inputActivoParaBusqueda)
            {
                var facturaTemporal = new Facturando
                {
                    Factura = listVarFactura.NoFactura,
                    ArticuloID = listDetFactura[consecut].articuloId,

                    CodigoCliente = this.txtCodigoCliente.Text,
                    //aqui le indico que no es una factura en espera
                    FacturaEnEspera = false,
                    Cajero = User.Usuario,
                    Caja = User.Caja,
                    NumCierre = User.ConsecCierreCT,
                    TiendaID = User.TiendaID,
                    FechaRegistro = DateTime.Now,
                    TipoCambio = listVarFactura.TipoDeCambio,

                    BodegaID = listDetFactura[consecut].BodegaID,
                    Consecutivo = Convert.ToInt32(listDetFactura[consecut].consecutivo),

                    CodigoBarra = listDetFactura[consecut].codigoBarra,

                    Cantidad = listDetFactura[consecut].cantidad,
                    Descripcion = listDetFactura[consecut].descripcion,
                    Unidad = listDetFactura[consecut].unidad,
                    Precio = listDetFactura[consecut].moneda == 'L' ? listDetFactura[consecut].precioCordobas : listDetFactura[consecut].precioDolar,
                    Moneda = listDetFactura[consecut].moneda.ToString(),
                    DescuentoLinea = listDetFactura[consecut].porCentajeDescuentoXArticulo,
                    DescuentoGeneral = listVarFactura.PorCentajeDescGeneral,
                    AplicarDescuento = this.chkDescuentoGeneral.Checked,
                    Observaciones = this .txtObservaciones.Text              

                };

                ResponseModel responseModel = new ResponseModel();
                responseModel = await _facturaController.GuardarDatosFacturaTemporal(facturaTemporal);

            }

        }



        /* private void dgvDetalleFactura_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
         {
             TextBox textbox = e.Control as TextBox;
             if (textbox != null)
             {
                 textbox.KeyPress -= new KeyPressEventHandler(dgvDetalleFactura_KeyPress);
                 textbox.KeyPress += new KeyPressEventHandler(dgvDetalleFactura_KeyPress);
             }
         }*/

        //evento KeyPress del Grid
        /* private void dgvDetalleFactura_KeyPress(object sender, KeyPressEventArgs e)
         {
             //identificar el numero de la columna                 
             int numColumn = dgvDetalleFactura.CurrentCell.ColumnIndex;
             //identificar el numero de la fila
             int numRow = dgvDetalleFactura.CurrentCell.RowIndex;
             //asignar el numero de fila en donde esta ubicado el cursor. 
             consecutivoActualFactura = numColumn;

             //3 es codigo de barra del articulo
            if (numColumn == 3)
             {
                 if (e.KeyChar == (char)13) // Si es un enter
                 {
                     onBuscarArticulo();
                     e.Handled = true; //Interceptamos la pulsación
                     SendKeys.Send("{TAB}"); //Pulsamos la tecla Tabulador por código
                 }
             }

             //if (numColumn == dataGridView1.Columncount - 1)
             //{
             //    if (dataGridView1.RowCount > (numRow + 1))
             //    {
             //        dataGridView1.CurrentCell = dataGridView1[1, numRow + 1];
             //    }
             //}
             //else
             //    dataGridView1.CurrentCell = dataGridView1[numColumn + 1, numRow];

         }*/



        private void txtCodigoBarra_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13) // Si es un enter
            {
                onBuscarArticulo();
            }
        }

 

        //eliminar el articulo de la lista de detalle de factura
        private void onEliminarArticulo(string articuloId, int consecutivo)
        {           
            this.btnCobrar.Enabled = false;

            //asignar el numero consecutivo del articulo
            consecutivoActualFactura = consecutivo;
           
            //proceder a eliminar el articulo        
            eliminarProductoFactura(listDetFactura, listVarFactura.NoFactura, articuloId, consecutivo);
            //calcular los totales        
            onCalcularTotales();

            LimpiarTextBoxBusquedaArticulo();
        }


        public async void eliminarProductoFactura(List<DetalleFactura> detalleFactura, string noFactura, string articuloId, int consecutivo)
        {
            //eliminar el registro de la lista.
            detalleFactura.RemoveAt(consecutivo);
            int rows = 0;

            foreach (var prod in detalleFactura)
            {
                //actualizar el consecutivo de la lista
                prod.consecutivo = rows;
                rows += 1;
            }
            //actualizar el consecutivo
            consecutivoActualFactura = detalleFactura.Count - 1;

            ResponseModel responseModel = new ResponseModel();
            //eliminar de la tabla temporal el articulo
            responseModel = await _facturaController.EliminarArticuloDetalleFacturaAsync(noFactura, articuloId);

            /*dgvDetalleFactura.DataSource = null;
            dgvDetalleFactura.DataSource = listDetFactura;*/
            //lenar el grid 
            LlenarGridviewDetalleFactura();
            _procesoFacturacion.configurarDataGridView(this.dgvDetalleFactura); 
        }

        private void LimpiarTextBoxBusquedaArticulo()
        {
            txtDescripcionArticulo.Text = "";
            //limpiar el buscador del articulo
            txtCodigoBarra.Text = "";
            //poner el focus en txtCodigoBarra
            txtCodigoBarra.Focus();
        }

        private void dgvDetalleFactura_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {

            //si la columna es cantidad (4) o descuento(5)
            if (e.ColumnIndex == 4 || e.ColumnIndex == 5)
            {
                btnCobrar.Enabled = false;
                //asignar el consucutivo para indicar en que posicion estas
                consecutivoActualFactura = e.RowIndex;
                validarCantidadGrid();
                //calcular totales
                onCalcularTotales();
            }
        }


        private void dgvDetalleFactura_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            btnCobrar.Enabled = false;
            //obtener el consecutivo
            int index = e.RowIndex;
            int columna = e.ColumnIndex;

            consecutivoActualFactura = index;
            columnaIndex = columna;

            if (index != -1 && columna != -1)
            {
                //(columna 4) es cantidad
                //columna Cantidad del DataGridView (columna=4)
                if (columnaIndex == 4)
                {
                    //antes de editar guardar temporalmente la cantidad en la variable  cantidadGrid por si la cantidad excede lo que digita el cajero, entonce regresa al valor 
                    cantidadGrid = Convert.ToDecimal(dgvDetalleFactura.Rows[consecutivoActualFactura].Cells[columnaIndex].Value);
                }
                //(columna 5) es descuento
                else if (columnaIndex == 5)
                {
                    //antes de editar guardar temporalmente la cantidad en la variable  cantidadGrid
                    descuentoGrid = Convert.ToDecimal(dgvDetalleFactura.Rows[consecutivoActualFactura].Cells[columnaIndex].Value);
                }
            }
        }

        void validarCantidadGrid()
        {
            //verificar que los consecutivoActualFactura y columnaIndex no tenga
            if (consecutivoActualFactura != -1 && columnaIndex != -1)
            {
                //13
                //columna Cantidad del DataGridView (columna=4)
                /*if (columnaIndex ==4)
                {*/
                switch (columnaIndex)
                {
                    //13
                    //columna Cantidad del DataGridView (columna=4)                   
                    case 4:
                        //obtener la cantidad del DataGridView
                        decimal cantidad = Convert.ToDecimal(dgvDetalleFactura.Rows[consecutivoActualFactura].Cells[columnaIndex].Value);
                        //obtener la existencia del DataGridView
                        decimal existencia = Convert.ToDecimal(dgvDetalleFactura.Rows[consecutivoActualFactura].Cells[8].Value);

                        if (cantidad > existencia)
                        {
                            MessageBox.Show("La cantidad digitada excede a la existencia del articulo", "Sistema COVENTAF", MessageBoxButtons.OK);
                            //asignarle la cantidad que tenia antes de editarla
                            dgvDetalleFactura.Rows[consecutivoActualFactura].Cells[columnaIndex].Value = cantidadGrid;
                        }
                        else if (cantidad < 0)
                        {
                            MessageBox.Show("La cantidad del articulo no puede ser negativo", "Sistema COVENTAF", MessageBoxButtons.OK);
                            //asignarle la cantidad que tenia antes de editarla
                            dgvDetalleFactura.Rows[consecutivoActualFactura].Cells[columnaIndex].Value = cantidadGrid;
                        }
                        else if (cantidad == 0)
                        {
                            MessageBox.Show("La cantidad del articulo no puede ser cero", "Sistema COVENTAF", MessageBoxButtons.OK);
                            //asignarle la cantidad que tenia antes de editarla
                            dgvDetalleFactura.Rows[consecutivoActualFactura].Cells[columnaIndex].Value = cantidadGrid;
                        }
                        break;

                    //columna descuento
                    case 5:
                        //obtener la cantidad del DataGridView
                        decimal descuento = Convert.ToDecimal(dgvDetalleFactura.Rows[consecutivoActualFactura].Cells[columnaIndex].Value);

                        if (descuento < 0)
                        {
                            MessageBox.Show("El descuento del articulo no puede ser negativo", "Sistema COVENTAF", MessageBoxButtons.OK);
                            //asignarle la cantidad que tenia antes de editarla
                            dgvDetalleFactura.Rows[consecutivoActualFactura].Cells[columnaIndex].Value = descuentoGrid;
                        }
                        else if (descuento > 100)
                        {
                            MessageBox.Show("El descuento del articulo no puede ser mayor del 100%", "Sistema COVENTAF", MessageBoxButtons.OK);
                            //asignarle la cantidad que tenia antes de editarla
                            dgvDetalleFactura.Rows[consecutivoActualFactura].Cells[columnaIndex].Value = descuentoGrid;
                        }
                        break;




                }

            }
        }


        #region evento del formulario facturacion

        private void chkDescuentoGeneral_Click(object sender, EventArgs e)
        {
            onChange_CheckDescuentoGeneral();
        }


        //evento cuando cambiar el chech en HTML
        void onChange_CheckDescuentoGeneral()
        {            
            this.btnCobrar.Enabled = false;
            //desactivar el boton guardar           
            var descuentoGeneral = this.chkDescuentoGeneral.Checked;
            this.txtDescuentoGeneral.Enabled = descuentoGeneral;


            //comprobar si el descuento general esta en lectura
            if (this.txtDescuentoGeneral.ReadOnly )
            {
                //asignar el descuento general
                listVarFactura.PorCentajeDescGeneral = descuentoGeneral ? listVarFactura.PorCentajeDescCliente : 0;
                this.txtDescuentoGeneral.Text = listVarFactura.PorCentajeDescGeneral.ToString("P2");
                //_procesoFacturacion.changeCheckDSD(listVarFactura, listDetFactura, activoDSD);
            }
            else
            {
                listVarFactura.PorCentajeDescGeneral = 0.00M;
                this.txtDescuentoGeneral.Text = "0.00 %";
                this.txtDescuentoGeneral.Focus();
            }


            onCalcularTotales();
        }

    


        void onClickValidarDescuento()
        {
            onCalcularTotales();
            this.btnCobrar.Enabled = true;
        }



        //seleccionar la forma de pago
        void onChange()
        {
            //console.log(deviceValue);
            string codigoValue = this.cboFormaPago.SelectedValue.ToString();            
            //desativar el boton guardar
            
            //comprobar si es tarjeta
            if (codigoValue == "0003")
            {
                this.lblLabelTittulo.Text = "Tipo de Tarjeta: ";
                this.lblLabelTittulo.Visible = true;
                this.cboTipoTarjeta.Visible = true;
                this.cboCondicionPago.Visible = false;
            }
            //comprobar si es tarjeta
            else if (codigoValue == "0004")
            {
                this.lblLabelTittulo.Text = "Condicion de Pago: ";
                
                this.lblLabelTittulo.Visible = true;
                this.cboTipoTarjeta.Visible = false;
                this.cboCondicionPago.Visible = true;
            }
            else
            {
                
                this.lblLabelTittulo.Visible = false;
                this.cboTipoTarjeta.Visible = false;
                this.cboCondicionPago.Visible = false;
                this.cboTipoTarjeta.Text = "ND";
            }


        }
        #endregion

     

        private void btnMminizar_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void btnEliminarArticulo_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.dgvDetalleFactura.RowCount >0)
                {
                    int NumeroFilaSeleccionada = dgvDetalleFactura.CurrentRow.Index;
                    if (MessageBox.Show("¿ Estas seguro de eliminar el articulo seleccionado ?", "Sistema COVENTAF", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        var articuloId = dgvDetalleFactura.Rows[NumeroFilaSeleccionada].Cells[1].Value.ToString();
                        onEliminarArticulo(articuloId, NumeroFilaSeleccionada);
                    }
                }      
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Sistema COVENTAF");
            }        
        }

        private void btnValidarDescuento_Click(object sender, EventArgs e)
        {
            // BODEGA b;
            //  b = cboBodega.SelectedItem;

            // object b = cboBodega.SelectedItem;
            //object be = cboBodega.GetItemText(b);

            //MessageBox.Show(cboBodega.SelectedValue.ToString());

            //MessageBox.Show("The value of your selected item is:" + be);

            if (this.txtCodigoCliente.Text.Trim().Length==0)
            {
                MessageBox.Show("Debes de Ingresar el codigo de clientes", "Sistema COVENTAF");
                this.txtCodigoCliente.Focus();
            }
            else if (this.dgvDetalleFactura.RowCount == 0)
            {
                MessageBox.Show("Debes de Ingresar el articulo", "Sistema COVENTAF");
                this.txtCodigoBarra.Focus();
            }
            else
            {
                onClickValidarDescuento();
            }
            
        }

        private void btnCobrar_Click(object sender, EventArgs e)
        {
            bool GuardarFactura = false;

            List<ViewMetodoPago> metodoPago;
                      

            //llamar la ventana de metodo de pago.
            var frmCobrarFactura = new frmMetodoPago();
            frmCobrarFactura.TotalCobrar = Math.Round(listVarFactura.TotalCordobas, 2);
            frmCobrarFactura.listaTipoTarjeta = listaTipoTarjeta;
            frmCobrarFactura.listaCondicionPago = listaCondicionPago;
            frmCobrarFactura.listFormaPago = listFormaPago;
            frmCobrarFactura.tipoCambioOficial = listVarFactura.TipoDeCambio;
       
            frmCobrarFactura.ShowDialog();
            //obtener informacion si el cajero cancelo o dio guardar factura
            GuardarFactura = frmCobrarFactura.GuardarFactura;
            //si el cajero presiono el boton guardar factura obtengo el registro del metodo de pago 
            metodoPago = GuardarFactura ? frmCobrarFactura.metodoPago : null;
            //liberar recursos
            frmCobrarFactura.Dispose();

            //verificar si el sistema va a proceder a guardar la factura
            if (GuardarFactura)
            {
                //primero recolectar la informacion de la factura
                RecolectarDatosFactura();
                //luego recopilar la informacion del metodo de pago que se obtuvo de la ventana metodo de pago
                RecopilarDatosMetodoPago(metodoPago);
                GuardarDatosFacturaBaseDatos();
            }
            
        }

        private async void GuardarDatosFacturaBaseDatos()
        {
            //oscar revisa esto urgente 16/02/2023
            var datoEncabezadoFact = new Encabezado()
            {
                noFactura = listVarFactura.NoFactura,
                fecha = listVarFactura.FechaFactura,
                bodega = this.cboBodega.SelectedValue.ToString(),
                caja = User.Caja,
                tipoCambio = listVarFactura.TipoDeCambio,
                codigoCliente = this.txtCodigoCliente.Text,
                cliente = listVarFactura.NombreCliente,
                subTotalDolar = listVarFactura.SubTotalDolar,
                descuentoDolar = listVarFactura.DescuentoGeneralDolar,
                ivaDolar = listVarFactura.IvaDolar,
                totalDolar = listVarFactura.TotalDolar,
                subTotalCordoba = listVarFactura.SubTotalCordoba,
                descuentoCordoba = listVarFactura.DescuentoPorLineaCordoba,
                ivaCordoba = listVarFactura.IvaCordoba,
                totalCordoba = listVarFactura.TotalCordobas,
                atentidoPor = User.NombreUsuario,
                formaDePago = listVarFactura.TicketFormaPago,
                observaciones = txtObservaciones.Text
            };



            //llamar al servidor para guardar la factura
            var responseModel = new ResponseModel();
            responseModel = await _facturaController.GuardarFacturaAsync(_modelFactura);


            //comprobar si el servidor respondio con exito (1)
            if (responseModel.Exito == 1)
            {
                //imprimir la factura
                _procesoFacturacion.ImprimirTicketFactura(listDetFactura, datoEncabezadoFact);                             
                MessageBox.Show(responseModel.Mensaje, "Sistema COVENTAF");
                facturaGuardada = true;
                this.Close();
            }
            else
            {
                this.btnGuardarFactura.Visible = true;
                this.btnGuardarFactura.Enabled = true;
                MessageBox.Show(responseModel.Mensaje, "Sistema COVENTAF");
            }
        }


        public void RecopilarDatosMetodoPago(List<ViewMetodoPago> metodoPago)
        {
            string TarjetaCredito = "0";
            string Condicion_Pago = "0";

            listVarFactura.TicketFormaPago = "";

            //var modelFactura = new ViewModelFacturacion();
            //modelFactura.Factura = new Facturas();
            //modelFactura.FacturaLinea = new List<Factura_Linea>();
            //_modelFactura.PagoPos = new List<Pago_Pos>();


            foreach (var mMetodoPago in metodoPago)
            {
                //aqui incluye el vuelto del cliente
                if (mMetodoPago.Indice > 0)
                {
                    var datosPagosPos_ = new Pago_Pos();
                    datosPagosPos_.Documento = _modelFactura.Factura.Factura;

                    //consecutivo ej.: 0,1,2
                    datosPagosPos_.Pago = mMetodoPago.Pago;
                    datosPagosPos_.Caja = User.Caja;
                    //F=factura
                    datosPagosPos_.Tipo = "F";
                    //30 dias 
                    datosPagosPos_.Condicion_Pago = mMetodoPago.CondicionPago;

                    //no tomar los valores cuando pago es un vuelto (-1)
                    if (datosPagosPos_.Pago != "-1")
                    {
                        //forma de pago para mostrar al momento de imprimir la Ticket
                        listVarFactura.TicketFormaPago += $"{mMetodoPago.DescripcionFormPago} {mMetodoPago.TipoTarjeta} {mMetodoPago.DescripcionCondicionPago}  ";
                    }
                   

                    //REVISAR
                    datosPagosPos_.Entidad_Financiera = null;
                    datosPagosPos_.Tipo_Tarjeta = mMetodoPago.TipoTarjeta;
                    
                    datosPagosPos_.Forma_Pago = mMetodoPago.FormaPago;
                    datosPagosPos_.Numero = null;
                    datosPagosPos_.Monto_Local = mMetodoPago.Moneda == 'L' ? mMetodoPago.MontoCordoba : 0.0000M;
                    datosPagosPos_.Monto_Dolar = mMetodoPago.Moneda == 'D' ? mMetodoPago.MontoDolar : 0.0000M;
                    datosPagosPos_.Autorizacion = null;
                    datosPagosPos_.Cobro = null;
                        datosPagosPos_.Cliente_Liquidador = null;
                    //revisar
                    datosPagosPos_.Tipo_Cobro = "T";
                    datosPagosPos_.NoteExistsFlag = 0;
                    datosPagosPos_.RecordDate = DateTime.Now;
                    // datosPagosPos_.RowPointer = 098D98DA-038F-4E06-B8E4-B48843DEEC1A
                    datosPagosPos_.CreatedBy = User.Usuario;
                    datosPagosPos_.UpdatedBy = User.Usuario;
                    datosPagosPos_.CreateDate = DateTime.Now;
                                      

                    //seleccionaste tarjeta
                    if (datosPagosPos_.Forma_Pago == "0003")
                    {
                        //banpro, credomatic
                        TarjetaCredito = datosPagosPos_.Tipo_Tarjeta;
                        
                    }

                    //credito
                    if (datosPagosPos_.Forma_Pago == "0004")
                    {
                        Condicion_Pago = datosPagosPos_.Condicion_Pago;
                    }

                    //comprobar si la moneda es Dolar
                    if (mMetodoPago.Moneda == 'D')
                    {
                        datosPagosPos_.Monto_Local = mMetodoPago.MontoCordoba;
                        datosPagosPos_.Monto_Dolar = mMetodoPago.MontoDolar;
                    }

                    //agregar nuevo registro a la clase FacturaLinea.
                    _modelFactura.PagoPos.Add(datosPagosPos_);
                }
            }

            //asingar la tarjeta de credito por el metodo de pago que selecciono el cliente
            _modelFactura.Factura.Tarjeta_Credito = TarjetaCredito;
            _modelFactura.Factura.Condicion_Pago = Condicion_Pago;
        }
              


        private void RecolectarDatosFactura()
        {
            listVarFactura.FechaFactura = DateTime.Now;

            // Getting Ip address of local machine…
            // First get the host name of local machine.
            string strNombreEquipo = string.Empty;
            // Getting Ip address of local machine…
            // First get the host name of local machine.
            strNombreEquipo = Dns.GetHostName();
            
            _modelFactura.Factura.Tipo_Documento = "F";
            _modelFactura.Factura.Factura = listVarFactura.NoFactura;
            _modelFactura.Factura.Caja = User.Caja;
            _modelFactura.Factura.Num_Cierre = User.ConsecCierreCT;
            _modelFactura.Factura.Audit_Trans_Inv = null;
            _modelFactura.Factura.Esta_Despachado = "N";
            _modelFactura.Factura.En_Investigacion = "N";
            _modelFactura.Factura.Trans_Adicionales = "N";
            _modelFactura.Factura.Estado_Remision = "N";
            _modelFactura.Factura.Asiento_Documento = null;
            _modelFactura.Factura.Descuento_Volumen = 0.00000000M;
            _modelFactura.Factura.Moneda_Factura = "L";
            _modelFactura.Factura.Comentario_Cxc = null;
            _modelFactura.Factura.Fecha_Despacho = listVarFactura.FechaFactura;
            _modelFactura.Factura.Clase_Documento = "N";
            _modelFactura.Factura.Fecha_Recibido = listVarFactura.FechaFactura;
            _modelFactura.Factura.Pedido = null;
            _modelFactura.Factura.Factura_Original = null;
            _modelFactura.Factura.Tipo_Original = null;
            _modelFactura.Factura.Comision_Cobrador = 0.00000000M;
            //aqui es la tarjeta (Tipo de tarjeta)
            //_modelFactura.Factura.Tarjeta_Credito = ((this.cboFormaPago.SelectedValue.ToString() == "0003") ? this.cboTipoTarjeta.SelectedValue.ToString() : null);
            
            _modelFactura.Factura.Total_Volumen = 0.00000000M;
            _modelFactura.Factura.Numero_Autoriza = null;
            _modelFactura.Factura.Total_Peso = 0.00000000M;
            _modelFactura.Factura.Monto_Cobrado = 0.00000000M;
            _modelFactura.Factura.Total_Impuesto1 = 0.00000000M;
            _modelFactura.Factura.Fecha = listVarFactura.FechaFactura;
            _modelFactura.Factura.Fecha_Entrega = listVarFactura.FechaFactura;
            _modelFactura.Factura.Total_Impuesto2 = 0.00000000M;
            _modelFactura.Factura.Porc_Descuento2 = 0.00000000M;
            _modelFactura.Factura.Monto_Flete = 0.00000000M;
            _modelFactura.Factura.Monto_Seguro = 0.00000000M;
            _modelFactura.Factura.Monto_Documentacio = 0.00000000M;
            _modelFactura.Factura.Tipo_Descuento1 = "P";
            _modelFactura.Factura.Tipo_Descuento2 = "P";
            //investigando y comparando encontre q este es el monto del descuento general
            _modelFactura.Factura.Monto_Descuento1 = listVarFactura.DescuentoGeneralCordoba;
            //investigando con softland tiene cero
            _modelFactura.Factura.Monto_Descuento2 = 0.00000000M;
            //investigando y comparando con softland encontre q este es el % del descuento general (5%)
            _modelFactura.Factura.Porc_Descuento1 = listVarFactura.PorCentajeDescGeneral;
            //
            _modelFactura.Factura.Total_Factura = listVarFactura.TotalCordobas;
            _modelFactura.Factura.Fecha_Pedido = listVarFactura.FechaFactura;
            _modelFactura.Factura.Fecha_Hora_Anula = null;
            _modelFactura.Factura.Fecha_Orden = listVarFactura.FechaFactura;
            //softland dice en su diccionario: El monto total de la mercadería contempla las cantidades por los precios; menos los descuentos por línea
            _modelFactura.Factura.Total_Mercaderia = listVarFactura.TotalCordobas + listVarFactura.DescuentoGeneralCordoba;
            _modelFactura.Factura.Comision_Vendedor = 0.00000000M;
            _modelFactura.Factura.Orden_Compra = null;
            _modelFactura.Factura.Fecha_Hora = listVarFactura.FechaFactura;
            _modelFactura.Factura.Total_Unidades = listVarFactura.TotalUnidades;
            _modelFactura.Factura.Numero_Paginas = 1;
            _modelFactura.Factura.Tipo_Cambio = listVarFactura.TipoDeCambio;
            _modelFactura.Factura.Anulada = "N";
            _modelFactura.Factura.Modulo = "FA";
            //PREGUNTARAJUAN;
            _modelFactura.Factura.Cargado_Cg = "S";
            //PREGUNTARaJUAN;
            _modelFactura.Factura.Cargado_Cxc = "N";
            _modelFactura.Factura.Embarcar_A = listVarFactura.NombreCliente;
            _modelFactura.Factura.Direc_Embarque = "ND";
            _modelFactura.Factura.Direccion_Factura = "";
            _modelFactura.Factura.Multiplicador_Ev = 1;
            _modelFactura.Factura.Observaciones = this.txtObservaciones.Text;
            _modelFactura.Factura.Rubro1 = null;
            _modelFactura.Factura.Rubro2 = null;
            _modelFactura.Factura.Rubro3 = null;
            _modelFactura.Factura.Rubro4 = null;
            _modelFactura.Factura.Rubro5 = null;
            _modelFactura.Factura.Version_Np = 1;
            _modelFactura.Factura.Moneda = User.MonedaNivel;
            _modelFactura.Factura.Nivel_Precio = User.NivelPrecio;
            _modelFactura.Factura.Cobrador = "ND";
            _modelFactura.Factura.Ruta = "ND";

            _modelFactura.Factura.Usuario = User.Usuario;
            _modelFactura.Factura.Usuario_Anula = null;
            //silacondiciondepagoesCREDITOentoncesseleccionarlacondiciondepagosinopordefectoescontado(0);
            //_modelFactura.Factura.Condicion_Pago = (this.cboFormaPago.SelectedValue.ToString() == "0004" ? this.cboCondicionPago.SelectedValue.ToString() : "0");
            
            _modelFactura.Factura.Zona = this.datosCliente.Zona;
            _modelFactura.Factura.Vendedor = this.cboBodega.SelectedValue.ToString();
            _modelFactura.Factura.Doc_Credito_Cxc = "";
            _modelFactura.Factura.Cliente_Direccion = datosCliente.Cliente;
            _modelFactura.Factura.Cliente_Corporac = datosCliente.Cliente;
            _modelFactura.Factura.Cliente_Origen = datosCliente.Cliente;
            _modelFactura.Factura.Cliente = datosCliente.Cliente;
            _modelFactura.Factura.Pais = "505";
            _modelFactura.Factura.Subtipo_Doc_Cxc = 0;
            //preguntarajuan;
            _modelFactura.Factura.Tipo_Credito_Cxc = null;
            _modelFactura.Factura.Tipo_Doc_Cxc = "FAC";
            //monto_AnticipoESVARIABLE;
            _modelFactura.Factura.Monto_Anticipo = 0.00000000M;
            _modelFactura.Factura.Total_Peso_Neto = 0.00000000M;
            _modelFactura.Factura.Fecha_Rige = listVarFactura.FechaFactura;
            //contrato=null;
            _modelFactura.Factura.Porc_Intcte = 0.00000000M;
            _modelFactura.Factura.Usa_Despachos = "N";
            _modelFactura.Factura.Cobrada = "S";
            _modelFactura.Factura.Descuento_Cascada = "N";
            _modelFactura.Factura.Direccion_Embarque = "ND";
            _modelFactura.Factura.Consecutivo = null;
            _modelFactura.Factura.Reimpreso = 0;
            _modelFactura.Factura.Division_Geografica1 = null;
            _modelFactura.Factura.Division_Geografica2 = null;
            _modelFactura.Factura.Base_Impuesto1 = 0.00000000M;
            _modelFactura.Factura.Base_Impuesto2 = 0.00000000M;
            _modelFactura.Factura.Nombre_Cliente = this.datosCliente.Nombre;
            _modelFactura.Factura.Doc_Fiscal = null;
            _modelFactura.Factura.Nombremaquina = strNombreEquipo;
            _modelFactura.Factura.Serie_Resolucion = null;
            _modelFactura.Factura.Consec_Resolucion = null;
            _modelFactura.Factura.Genera_Doc_Fe = "N";
            _modelFactura.Factura.Tasa_Impositiva = null;
            _modelFactura.Factura.Tasa_Impositiva_Porc = 0.00000000M;
            _modelFactura.Factura.Tasa_Cree1 = null;
            _modelFactura.Factura.Tasa_Cree1_Porc = 0.00000000M;
            _modelFactura.Factura.Tasa_Cree2 = null;
            _modelFactura.Factura.Tasa_Cree2_Porc = 0.00000000M;
            _modelFactura.Factura.Tasa_Gan_Ocasional_Porc = 0.00000000M;
            _modelFactura.Factura.Contrato_Ac = null;
            _modelFactura.Factura.Ajuste_Redondeo = null;
            _modelFactura.Factura.Uso_Cfdi = null;
            _modelFactura.Factura.Forma_Pago = null;
            _modelFactura.Factura.Clave_Referencia_De = null;
            _modelFactura.Factura.Fecha_Referencia_De = null;
            _modelFactura.Factura.Justi_Dev_Haciend = null;
            _modelFactura.Factura.Incoterms = null;
            _modelFactura.Factura.U_Ad_Wm_Numero_Vendedor = null;
            _modelFactura.Factura.U_Ad_Wm_Enviar_Gln = null;
            _modelFactura.Factura.U_Ad_Wm_Numero_Recepcion = null;
            _modelFactura.Factura.U_Ad_Wm_Numero_Reclamo = null;
            _modelFactura.Factura.U_Ad_Wm_Fecha_Reclamo = null;
            _modelFactura.Factura.U_Ad_Pc_Numero_Vendedor = null;
            _modelFactura.Factura.U_Ad_Pc_Enviar_Gln = null;
            _modelFactura.Factura.U_Ad_Gs_Numero_Vendedor = null;
            _modelFactura.Factura.U_Ad_Gs_Enviar_Gln = null;
            _modelFactura.Factura.U_Ad_Gs_Numero_Recepcion = null;
            _modelFactura.Factura.U_Ad_Gs_Fecha_Recepcion = null;
            _modelFactura.Factura.U_Ad_Am_Numero_Proveedor = null;
            _modelFactura.Factura.U_Ad_Am_Enviar_Gln = null;
            _modelFactura.Factura.U_Ad_Am_Numero_Recepcion = null;
            _modelFactura.Factura.U_Ad_Am_Numero_Reclamo = null;
            _modelFactura.Factura.U_Ad_Am_Fecha_Reclamo = null;
            _modelFactura.Factura.U_Ad_Am_Fecha_Recepcion = null;
            _modelFactura.Factura.Tipo_Operacion = null;
            _modelFactura.Factura.NoteExistsFlag = 0;
            _modelFactura.Factura.RecordDate = listVarFactura.FechaFactura;
            //oscar esto tiene que ir, solo lo deje en comentario xq cuestion de errores
            //_modelFactura.Factura.RowPointer = "F1929B9E-5CB4-476D-AFCC-0CC3D7D0D159";
            _modelFactura.Factura.CreatedBy = User.Usuario;
            _modelFactura.Factura.UpdatedBy = User.Usuario;
            _modelFactura.Factura.CreateDate = listVarFactura.FechaFactura;
            _modelFactura.Factura.Clave_De = null;
            _modelFactura.Factura.Actividad_Comercial = null;
            _modelFactura.Factura.Monto_Otro_Cargo = null;
            _modelFactura.Factura.Monto_Total_Iva_Devuelto = null;
            _modelFactura.Factura.Codigo_Referencia_De = null;
            _modelFactura.Factura.Tipo_Referencia_De = null;
            _modelFactura.Factura.Cancelacion = null;
            _modelFactura.Factura.Estado_Cancelacion = null;
            _modelFactura.Factura.Tiene_Relacionados = null;
            _modelFactura.Factura.Prefijo = null;
            _modelFactura.Factura.Fecha_Inicio_Resolucion = null;
            _modelFactura.Factura.Fecha_Final_Resolucion = null;
            _modelFactura.Factura.Clave_Tecnica = null;
            _modelFactura.Factura.Matricula_Mercantil = null;
            _modelFactura.Factura.Es_Factura_Reemplazo = null;
            _modelFactura.Factura.Factura_Original_Reemplazo = null;
            _modelFactura.Factura.Consecutivo_Ftc = null;
            _modelFactura.Factura.Numero_Ftc = null;
            _modelFactura.Factura.Nit_Transportador = null;
            _modelFactura.Factura.Ncf_Modificado = null;
            _modelFactura.Factura.Num_Oc_Exenta = null;
            _modelFactura.Factura.Num_Cons_Reg_Exo = null;
            _modelFactura.Factura.Num_Irsede_Agr_Gan = null;
            _modelFactura.Factura.U_Ad_Wm_Tipo_Nc = null;
            _modelFactura.Factura.Cuenta_Asiento = null;
            _modelFactura.Factura.Tipo_Pago = null;
            _modelFactura.Factura.Tipo_Descuento_Global = null;
            _modelFactura.Factura.Tipo_Factura = null;
            _modelFactura.Factura.Tipo_Nc = null;
            _modelFactura.Factura.Tipo_Detrac = null;
            _modelFactura.Factura.Act_Detrac = null;
            _modelFactura.Factura.Porc_Detrac = null;            

            //detalle de la factura
            foreach (var detFactura in listDetFactura)
            {
                if (detFactura.articuloId != "")
                {

                    var datosModel_ = new Factura_Linea()
                    {
                        Factura = listVarFactura.NoFactura,
                        Tipo_Documento = "F",
                        Linea = (short)detFactura.consecutivo,
                        Bodega = detFactura.BodegaID,
                        //preguntarajuan
                        //ya revise en softland y no hay informacion [COSTO_PROM_DOL] * cantidad
                        Costo_Total_Dolar = 0.00M,
                        //pedido?=string
                        Articulo = detFactura.articuloId,
                        //localizacion?:string
                        //lote?:string
                        Anulada = "N",
                        Fecha_Factura = listVarFactura.FechaFactura,
                        Cantidad = detFactura.cantidad,
                        Precio_Unitario = detFactura.precioCordobas,
                        Total_Impuesto1 = 0.00000000M,
                        Total_Impuesto2 = 0.00000000M,
                        //revisar
                        Desc_Tot_Linea = detFactura.descuentoPorLineaCordoba,
                        //este es el descuento general (el famoso 5% q le dan a los militares)
                        Desc_Tot_General = detFactura.MontoDescGeneralCordoba,
                        //revisar [COSTO_PROM_LOC] * cantidad
                        Costo_Total = 0.000M,
                        //aqui ya tiene restado el descuento. precio_total_x_linea. ya lo verifique con softland
                        Precio_Total = detFactura.totalCordobas,
                        Descripcion = detFactura.descripcion,
                        //comentario?=string
                        Cantidad_Devuelt = 0.00000000M,
                        Descuento_Volumen = 0.00000000M,
                        Tipo_Linea = "N",
                        Cantidad_Aceptada = 0.00000000M,
                        Cant_No_Entregada = 0.00000000M,
                        //revisar [COSTO_PROM_LOC] * cantidad
                        Costo_Total_Local = 0.00M,
                        Pedido_Linea = 0,
                        Multiplicador_Ev = 1,
                        /*serie_Cadena?=number
                        Serie_Cad_No_Acept?=number
                        Serie_Cad_Aceptada?=number
                        Documento_Origen?=string
                        Linea_Origen?=number
                        Tipo_Origen?=string
                        Unidad_Distribucio?=string*/
                        Cant_Despachada = 0.00000000M,
                        Costo_Estim_Local = 0.00000000M,
                        Costo_Estim_Dolar = 0.00000000M,
                        Cant_Anul_Pordespa = 0.00000000M,
                        Monto_Retencion = 0.00000000M,
                        Base_Impuesto1 = 0.00000000M,
                        Base_Impuesto2 = 0.00000000M,
                        /*proyecto?=string
                        Fase?=string
                        Centro_Costo?=string
                        Cuenta_Contable?=string*/
                        //revisar [COSTO_PROM_LOC] * cantidad
                        Costo_Total_Comp = 0,
                        //revisar [COSTO_PROM_LOC] * cantidad
                        Costo_Total_Comp_Local = 0,
                        //revisar [COSTO_PROM_DOL] * cantidad
                        Costo_Total_Comp_Dolar = 0,
                        Costo_Estim_Comp_Local = 0.00000000M,
                        Costo_Estim_Comp_Dolar = 0.00000000M,
                        Cant_Dev_Proceso = 0.00000000M,
                        NoteExistsFlag = 0,
                        RecordDate = listVarFactura.FechaFactura,
                        //RowPointer = "D083E752-86BD-41E9-BDAC-12A0B2D865E7",
                        //revisar
                        CreatedBy = User.Usuario,
                        //revisar
                        UpdatedBy = User.Usuario,
                        CreateDate = listVarFactura.FechaFactura,
                        Porc_Desc_Linea = detFactura.porCentajeDescuentoXArticulo

                        /*tipo_Impuesto1? = string
                        tipo_Tarifa1? = string
                        tipo_Impuesto2? = string
                        tipo_Tarifa2? = string
                        porc_Exoneracion? = number
                        monto_Exoneracion? = number
                        es_Otro_Cargo? = string
                        es_Canasta_Basica? = string
                        es_Servicio_Medico? = string
                        monto_Devuelto_Iva? = number
                        porc_Exoneracion2? = number
                        monto_Exoneracion2? = number
                        tipo_Descuento_Linea? = string */
                    };
                    //agregar nuevo registro a la clase FacturaLinea.
                    _modelFactura.FacturaLinea.Add(datosModel_);
                }
            }
        }

            

        private void FrmVentas_KeyDown(object sender, KeyEventArgs e)
        {
            //comprobar si el usuario presiono la tecla f5 y ademas si el boton esta habilitado
            if (e.KeyCode == Keys.F1 && this.btnValidarDescuento.Enabled)
            {
                btnValidarDescuento_Click(null, null);
            }

            //comprobar si el usuario presiono la tecla f5 y ademas si el boton esta habilitado
            else if (e.KeyCode == Keys.F2 && this.btnCobrar.Enabled)
            {
                btnCobrar_Click(null, null);
            }
            //F6 y chkDescuentoGeneral este habilitado
            else if (e.KeyCode == Keys.F6 && this.chkDescuentoGeneral.Enabled)
            {
                this.chkDescuentoGeneral.Checked = !this.chkDescuentoGeneral.Checked;
                chkDescuentoGeneral_Click(null, null);
            }

            else if (e.KeyCode == Keys.F5)
            {
                this.txtObservaciones.Focus();
            }

            else if (e.KeyCode == Keys.F8)
            {
                btnLimpiarFactura_Click(null, null);
            }

            else if (e.KeyCode == Keys.F10 && this.btnGuardarFactura.Visible)
            {
                btnGuardarFactura_Click(null, null);
            }

            else if (e.KeyCode == Keys.Delete)
            {
                btnEliminarArticulo_Click(null, null);
            }
            //ubicar el cursor el textbox codigo de barra
            else if (e.KeyCode == Keys.F12)
            {
                this.txtCodigoBarra.Focus();
            }

            else if (e.KeyCode == Keys.Escape)
            {
                btnCerrar_Click(null, null);
            }

        }

       
        private void txtDescuentoGeneral_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                AplicarDescuentoGeneralFactura();
                this.txtDescuentoGeneral.Focus();
            }
        }

        private void AplicarDescuentoGeneralFactura()
        {
            var existeCaractePorcentaje = false;

            string valorPorCentaje = _procesoFacturacion.ObtenerNuevoPorCentaje(this.txtDescuentoGeneral.Text, ref existeCaractePorcentaje);
            this.txtDescuentoGeneral.Text = existeCaractePorcentaje ? this.txtDescuentoGeneral.Text : $"{this.txtDescuentoGeneral.Text} %";
            decimal porCentajeDescuento = Convert.ToDecimal(valorPorCentaje);
            listVarFactura.PorCentajeDescGeneral = porCentajeDescuento;
            //realizar calculo
            onCalcularTotales();            
        }



        private void cboBodega_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void btnGuardarFactura_Click(object sender, EventArgs e)
        {
            GuardarDatosFacturaBaseDatos();
        }

        private void btnLimpiarFactura_Click(object sender, EventArgs e)
        {
            //falta eliminar de la tabla facturando
            _procesoFacturacion.InicializarTodaslasVariable(listVarFactura);
            this.txtCodigoCliente.Enabled = true;
            this.txtCodigoCliente.Text = "";
            this.txtNombreCliente.Text = "";
            this.txtDisponibleCliente.Text = "";
            this.txtDescuentoCliente.Text = "";
            this.cboBodega.SelectedValue = User.BodegaID;
            listDetFactura = null;
            listDetFactura = new List<DetalleFactura>();
            this.txtObservaciones.Text = "";
            this.chkDescuentoGeneral.Enabled = false;
            this.chkDescuentoGeneral.Checked = false;
            this.txtDescuentoGeneral.Text = "0.00%";
            this.txtCodigoBarra.Text = "";
            this.txtDescripcionArticulo.Text = "";
            onCalcularTotales();
            this.btnValidarDescuento.Enabled = true;
            this.btnCobrar.Enabled = false;
            this.txtCodigoCliente.Focus();
            this.lblFecha.Text = DateTime.Now.ToString("dd/MM/yyyy");
        }

        private void txtCodigoBarra_Enter(object sender, EventArgs e)
        {
            this.btnCobrar.Enabled = false;
        }

        private void txtDescuentoGeneral_Leave(object sender, EventArgs e)
        {
            AplicarDescuentoGeneralFactura();
        }
    }
}

