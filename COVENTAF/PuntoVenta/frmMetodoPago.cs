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
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace COVENTAF.PuntoVenta
{
    public partial class frmMetodoPago : Form
    {
        //esta variable me indica si el cajero presiono la tecla guardar factura
        public bool GuardarFactura = false;
        public List<ViewMetodoPago> metodoPago;

        //public List<Vendedores> listaBodega = new List<Vendedores>();
        public List<Forma_Pagos> listFormaPago = new List<Forma_Pagos>();
        public List<Tipo_Tarjeta_Pos> listaTipoTarjeta = new List<Tipo_Tarjeta_Pos>();
        public List<Condicion_Pagos> listaCondicionPago = new List<Condicion_Pagos>();


        public decimal TotalCobrar = 1695.58M;
        public decimal tipoCambioOficial = 36.2924M;
        public decimal nuevoTipoCambioAproximado;
        //esta variable lleva el control si el cliente ya hizo el pago de la factura de dos forma x ejemplo
        private bool bloquearMetodoPago = false;
        //esta variable lleva el control de la tecla que el cajero presiono. x ejemplo el cajero presiono F1 y luego F3, entonces si primero presion F1 y despues F3
        //el sistema tiene que saber que el usuario ahora quiere el metodo de pago F3, quizas porque se confundio, por lo tanto el sistema tiene que ser capaz de limpiar el textbox
        private string teclaPresionadaXCajero = "";
        bool bandera = true;
        private bool cobrasteCordobas = false;
        private decimal montoFinalCobrarDolar;

        private string codigoTipoPago = "";
        private string tipoPago = "";
        private char moneda = 'L';

        private int Index = -1;
        private decimal totalCobrarCordoba = 0.0000M;
        private decimal totalCobrarDolar = 0.00M;
        private decimal montoPagadoCordoba = 0.0000M;
        private decimal montoPagadoDolar = 0.00M;
        private decimal diferenciaCordoba = 0.0000M;
        private decimal diferenciaDolar = 0.00M;

        /*private ViewModelFacturacion _modelFactura = new ViewModelFacturacion();
        private Encabezado _datoEncabezadoFact = new Encabezado();
        private FacturaController _facturaController = new FacturaController();
        public List<DetalleFactura> _listDetFactura = new List<DetalleFactura>();*/

        public frmMetodoPago()
        {
            InitializeComponent();
            metodoPago = new List<ViewMetodoPago>();
            /* this._modelFactura = modelFactura;            
             this._datoEncabezadoFact = datoEncabezadoFact;
             this._facturaController = facturaController;
             this._listDetFactura = listDetFactura;*/
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            this.Close();
        }


        void InicializarMontos()
        {
            totalCobrarCordoba = TotalCobrar;
            totalCobrarDolar = Math.Round((TotalCobrar / tipoCambioOficial), 2);
            montoPagadoCordoba = 0.0000M;
            montoPagadoDolar = 0.00M;
            diferenciaCordoba = TotalCobrar - montoPagadoCordoba;
            diferenciaDolar = totalCobrarDolar - montoPagadoDolar;

            /*
            metodoPago[0].Pago = "-2"; //PagoID=-2 es el total a Cobrar
            metodoPago[0].FormaPago = "-2";
            metodoPago[0].DescripcionFormPago = "Total a Cobrar";
            metodoPago[0].MontoCordoba = TotalCobrar;
            metodoPago[0].Moneda = 'L'; //L=Local (C$)
            metodoPago[0].MontoDolar = Math.Round((TotalCobrar / tipoCambioOficial), 2);
            metodoPago[0].MontoPagado = 0.0000M;
            metodoPago[0].DiferenciaCordoba = metodoPago[0].MontoCordoba;
            metodoPago[0].DiferenciaDolar = metodoPago[0].MontoDolar;
            metodoPago[0].TeclaPresionaXCajero = false;
            metodoPago[0].DescripcionTecla = "No Existe Tecla";*/

            this.lblTotalPagar.Text = $"C${TotalCobrar.ToString("N2")} = U${(TotalCobrar / tipoCambioOficial).ToString("N2")}";
            this.txtPendientePagarCliente.Text = TotalCobrar.ToString("N2");

            this.txtEfectivoCordoba.Text = string.Format("{0:n}", this.txtEfectivoCordoba.Text);//1.234,35
        }


        void AddNuevaFilaMetodoPago()
        {
            var datosd_ = new ViewMetodoPago()
            {                                                        
                MontoCordoba = 0.0000M,              
                MontoDolar = 0.0000M,
                TeclaPresionaXCajero = false,                
                Monto = 0.00M,                
            };

        //agregar push para agregar un nuevo registro
        metodoPago.Add(datosd_);
            //aumentar el idice
            Index = Index + 1;
        }

        private void btnCierre_Click(object sender, EventArgs e)
        {
            this.Close();
        }


        private void frmMetodoPago_KeyDown(object sender, KeyEventArgs e)
        {
            //comprobar si el usuario presiono la tecla f5 y ademas si el boton esta habilitado
            if (e.KeyCode == Keys.F1)
            {
                //llamar al evento efectivo
                btnEfectivoCordoba_Click(null, null);
            }
            if (e.KeyCode == Keys.F2)
            {
                //llamar al evento efectivo
                btnChequeCordoba_Click(null, null);
            }

            //comprobar si el usuario presiono la tecla f5 y ademas si el boton esta habilitado
            else if (e.KeyCode == Keys.F3)
            {
                btnTarjetaCordoba_Click(null, null);
            }

            else if (e.KeyCode == Keys.F4)
            {
                btnCredito_Click(null, null);
            }

            else if (e.KeyCode == Keys.F5)
            {
                btnBono_Click(null, null);
            }
            else if (e.KeyCode == Keys.F6)
            {
                btnGiftCardCordobar_Click(null, null);
            }
            //comprobar si presiono la tecla F11 y el label efectivo dolar esta visible
            else if (e.KeyCode == Keys.F11 && this.lblEfectivoDolar.Visible)
            {
                lblEfectivoDolar_Click(null, null);
            }
            //comprobar si presiono la tecla F11 y el label Cheque dolar esta visible
            else if (e.KeyCode == Keys.F11 && this.lblChequeDolar.Visible)
            {
                lblChequeDolar_Click(null, null);
            }
            //comprobar si presiono la tecla F11 y el label Tarjeta dolar esta visible
            else if (e.KeyCode == Keys.F11 && this.lblTarjetaDolar.Visible)
            {
                lblTarjetaDolar_Click(null, null);
            }
            //comprobar si presiono la tecla F11 y el label GiftCard dolar esta visible
            else if (e.KeyCode == Keys.F11 && this.lblGiftCardDolar.Visible)
            {
                lblGiftCardDolar_Click(null, null);
            }

            else if (e.KeyCode == Keys.F8)
            {
                btnGuardarFactura_Click(null, null);
            }

            else if (e.KeyCode == Keys.F12)
            {
                btnReInicioCobro_Click(null, null);
            }

            else if (e.KeyCode == Keys.Escape)
            {
                btnCerrar_Click(null, null);
            }
        }

        //void VerificarMontoDolar(decimal montoDolarCobrar)
        //{

        //      //  metodoPago[0].MontoCordoba

        //    //comprobar si el cliente pago en cordoba y adema si el monto a pagar es el monto final en dolares
        //        if (cobrasteCordobas & montoDolarCobrar == montoFinalCobrarDolar)
        //    {
        //        //metodoPago[0].Diferencia


        //        //este es el monto con error
        //        decimal montoConError = (montoDolarCobrar * tipoCambioOficial);
        //        montoConError = Math.Round(montoConError, 2);

        //        //este es el monto al que tengo q llegar
        //        decimal montoTotalCobrar = metodoPago[0].Diferencia;

        //        if (montoConError == montoTotalCobrar)
        //        {
        //            nuevoTipoCambioAproximado = tipoCambioOficial;
        //        }
        //        else
        //        {
        //            nuevoTipoCambioAproximado = ObtenerNuevoTipoCambioExcto(montoTotalCobrar, montoConError, montoDolarCobrar);
        //        }

        //    }
        //}

        void VerificarMontoDolar(decimal montoDolarCobrar)
        {

            if (montoDolarCobrar == diferenciaDolar)
            {
                //este es el monto con error
                decimal montoConError = (montoDolarCobrar * tipoCambioOficial);
                montoConError = Math.Round(montoConError, 2);

                //este es el monto al que tengo q llegar en cordobas
                decimal montoTotalCobrar = Math.Round(diferenciaCordoba, 2);

                if (montoConError == montoTotalCobrar)
                {
                    nuevoTipoCambioAproximado = tipoCambioOficial;
                }
                else
                {
                    nuevoTipoCambioAproximado = ObtenerNuevoTipoCambioExcto(montoTotalCobrar, montoConError, montoDolarCobrar);
                }
            }
            /*
            //comprobar si el cliente pago en cordoba y adema si el monto a pagar es el monto final en dolares
            if (cobrasteCordobas & montoDolarCobrar == montoFinalCobrarDolar)
            {
                //metodoPago[0].Diferencia


                //este es el monto con error
                decimal montoConError = (montoDolarCobrar * tipoCambioOficial);
                montoConError = Math.Round(montoConError, 2);

                //este es el monto al que tengo q llegar
                decimal montoTotalCobrar = metodoPago[0].Diferencia;

                if (montoConError == montoTotalCobrar)
                {
                    nuevoTipoCambioAproximado = tipoCambioOficial;
                }
                else
                {
                    nuevoTipoCambioAproximado = ObtenerNuevoTipoCambioExcto(montoTotalCobrar, montoConError, montoDolarCobrar);
                }

            }*/
        }

        void VerificarMonto(decimal montoDolarCobrar)
        {
            //var valorMontoDolar = Math.Round((metodoPago[0].MontoCordoba / tipoCambioOficial),2);

            //comprobar si el cliente pago en cordoba y adema si el monto a pagar es el monto final en dolares
            if (cobrasteCordobas & montoDolarCobrar == montoFinalCobrarDolar)
            {
                //metodoPago[0].Diferencia


                //este es el monto con error
                decimal montoConError = (montoDolarCobrar * tipoCambioOficial);
                montoConError = Math.Round(montoConError, 2);

                //este es el monto al que tengo q llegar
                decimal montoTotalCobrar = diferenciaCordoba;

                if (montoConError == montoTotalCobrar)
                {
                    nuevoTipoCambioAproximado = tipoCambioOficial;
                }
                else
                {
                    nuevoTipoCambioAproximado = ObtenerNuevoTipoCambioExcto(montoTotalCobrar, montoConError, montoDolarCobrar);
                }

            }
        }

        //esto hay q revisarlo.. q pasa si el cliente hay un pago con 2 tarjeta de cordobas y 2 tarjeta de dolares con diferente banco.. 
        void AsginarMetodoPago(string formaPago, string nombreMetodoPago, decimal monto, char moneda, bool TeclaPresionaXCajero, string tecla, string condicionPago, string DescripcionCondicionPago = "", string tipoTarjeta = null)
        {
            decimal montoDolar = 0;
            decimal montoCordoba = 0;

            switch (moneda)
            {
                //comprobar si la monedad es local(L=Local=C$)
                case 'L':
                    monto = Math.Round(monto, 2);
                    //si el cliente paga con cordobas y luego dolares, entonces en el tipo de cambio existe un restante
                    //o faltante de centavo por el tipo de cambio, de modo que aqui controlo si el usuario hizo pago en cordobas y reajusto el nuevo tipo de cambio para que sea exacto.
                    //aqui le indico al sistema que el cliente realizo un pago en cordobas.
                    cobrasteCordobas = true;
                    montoCordoba = monto;
                    montoDolar = monto / tipoCambioOficial;
                    montoDolar = Math.Round(montoDolar, 2);
                    break;

                //comprobar si la monedad es Dolaar(D=Dolar=U$)
                case 'D':


                    monto = Math.Round(monto, 2);
                    //aqui tendria que ver q tipo de cambio usar
                    VerificarMontoDolar(monto);
                    montoDolar = monto;
                    montoCordoba = monto * nuevoTipoCambioAproximado;
                    montoCordoba = Math.Round(montoCordoba, 2);
                    break;
            }

            //obtener el indice si ya existe el metodo de pago
            var datoMetPago = metodoPago.Where(mp => mp.Pago != "-1" && mp.DescripcionTecla == tecla && mp.TipoTarjeta == tipoTarjeta).Select(mp => new { mp.Indice, mp.Pago }).FirstOrDefault();

            if (datoMetPago is null)
            {
                AddNuevaFilaMetodoPago();
            }
            else
            {
                //obtener el indice de la consulta
                Index = datoMetPago.Indice;
            }


            metodoPago[Index].Indice = Index;
            metodoPago[Index].Pago = Index .ToString();
            metodoPago[Index].FormaPago = formaPago;
            metodoPago[Index].DescripcionFormPago = nombreMetodoPago;
            metodoPago[Index].TipoTarjeta = tipoTarjeta;
            metodoPago[Index].CondicionPago = condicionPago;
            metodoPago[Index].DescripcionCondicionPago = DescripcionCondicionPago;

            metodoPago[Index].MontoCordoba += montoCordoba;
            metodoPago[Index].Moneda = moneda;
            metodoPago[Index].MontoDolar += montoDolar;  
            metodoPago[Index].TeclaPresionaXCajero = TeclaPresionaXCajero;
            metodoPago[Index].DescripcionTecla = tecla;
            /*
            metodoPago[Index].TipoPago = tecla;
            metodoPago[Index].Monto = tecla;
            metodoPago[Index].Detalle = tecla;*/


            ///// realizar los calculos
            ///sumar el monto pagado
            montoPagadoCordoba += montoCordoba;
            montoPagadoDolar += montoDolar;
            //this.txtPendientePagarCliente.Text = montoPagadoCordoba.ToString("N2");
            //calcular la diferencia, en este caso si existe una diferencia
            var diferencia = totalCobrarCordoba - montoPagadoCordoba;

            diferencia = Math.Round(diferencia, 2);
            diferenciaCordoba = diferencia;
            diferenciaDolar = Math.Round((diferencia / tipoCambioOficial), 2);
            //validar si la diferencia es negativa entonces existe un cambio para el cliente
            if (diferencia < 0.00M)
            {
                decimal Cambio = (-1) * diferencia;
                lblCambioCliente.Text = $"C${Cambio.ToString("N2")} = U${(Cambio / tipoCambioOficial).ToString("N2")}";
                this.txtPendientePagarCliente.Text = "0.00";
                bloquearMetodoPago = true;
                this.btnGuardarFactura.Enabled = bloquearMetodoPago;

                //llamar al metodo para asignar el vuelto en la clase q se lleva el control del metodo de pago
                AsginarMetodoPagoDiferencial(diferencia);
            }
            else
            {

                var valorPendientePagar = GetMontoCobrar();
                bloquearMetodoPago = (valorPendientePagar > 0 ? false : true);
                this.btnGuardarFactura.Enabled = bloquearMetodoPago;
                this.txtPendientePagarCliente.Text = $"C${valorPendientePagar.ToString("N2")}";
            }

            this.dgvDetallePago.DataSource = null;
            //bloquear el monto
            this.dgvDetallePago.DataSource = metodoPago;

        }

        //asignar e,
        void AsginarMetodoPagoDiferencial(decimal diferencial)
        {
            //obtene el maximo indice
            Index = metodoPago.Max(mp => mp.Indice);
            //agregar una fila.
            AddNuevaFilaMetodoPago();
            metodoPago[Index].Indice = Index;
            metodoPago[Index].Pago = "-1"; //Pago =-1 es el vuelto del cliente
            //si existe vuelto para el cliente entonce quiere decir que es efectivo 
            metodoPago[Index].FormaPago = "0001";
            metodoPago[Index].DescripcionFormPago = "Vuelto";
            metodoPago[Index].MontoCordoba = diferencial;        
            metodoPago[Index].TeclaPresionaXCajero = false;
            metodoPago[Index].DescripcionTecla = "No Existe Tecla";
        }


        decimal GetMontoCobrar()
        {
            var montoPagado = totalCobrarCordoba - montoPagadoCordoba;
            montoPagado = Math.Round(montoPagado, 2);
            //si el monto pagado es negativo significa que el cliente ya pago la factura
            if (montoPagado < 0)
            {
                return 0.00M;
            }
            else
            {               
                return montoPagado;
            }
        }




        //boton reiniciar el cobro
        private void btnReInicioCobro_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("¿ Estas seguro de Reiniciar el metodo de pago ?", "Sistema COVENTAF", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {

                this.lblConvertidorDolares.Visible = false;

                montoFinalCobrarDolar = 0;
                cobrasteCordobas = false;
                this.btnGuardarFactura.Enabled = false;
                bloquearMetodoPago = false;
                teclaPresionadaXCajero = "";
                metodoPago = null;
                Index = -1;

                SetCambiarEstadoVisibleLableF11Dolar("", false);

                metodoPago = new List<ViewMetodoPago>();
                this.dgvDetallePago.DataSource = null;
                this.dgvDetallePago.DataSource = metodoPago;
                
                InicializarMontos();

                this.txtEfectivoCordoba.Text = "C$0.00";
                this.txtEfectivoCordoba.Enabled = false;
                this.txtEfectivoDolar.Text = "U$0.00";
                this.txtEfectivoDolar.Enabled = false;
                this.lblEfectivoDolar.Visible = false;

                this.txtChequeCordoba.Text = "C$0.00";
                this.txtChequeCordoba.Enabled = false;
                this.txtChequeDolar.Text = "U$0.00";
                this.txtChequeCordoba.Enabled = false;
                this.lblChequeDolar.Visible = false;

                this.txtTarjetaCordoba.Text = "C$0.00";
                this.txtTarjetaCordoba.Enabled = false;
                this.txtTarjetaDolar.Text = "U$0.00";
                this.txtTarjetaDolar.Enabled = false;
                this.lblTarjetaDolar.Visible = false;

                this.txtCredito.Text = "C$0.00";
                this.txtCredito.Enabled = false;


                this.txtBono.Text = "C$0.00";
                this.txtBono.Enabled = false;

                this.txtGiftCardCordoba.Text = "C$0.00";
                this.txtGiftCardCordoba.Enabled = false;
                this.txtGiftCardDolar.Text = "U$0.00";
                this.txtGiftCardDolar.Enabled = false;
                this.lblGiftCardDolar.Visible = false;

                txtPendientePagarCliente.Text = TotalCobrar.ToString("N2");

                this.lblCambioCliente.Text = "C$0.00";
            }
        }



        private void frmMetodoPago_Load(object sender, EventArgs e)
        {

            tipoCambioOficial = Math.Round(tipoCambioOficial, 4);
            nuevoTipoCambioAproximado = tipoCambioOficial;

            var montoTotalCobrar = Math.Round(TotalCobrar, 2);

            lblTitulo.Text = $"Cobrar Factura. Tipo de Cambio: {tipoCambioOficial}";            
            //inicializar los datos
            InicializarMontos();

            /*
            decimal montoDolares = (montoTotalCobrar / tipoCambioOficial);
            montoDolares = Math.Round(montoDolares, 2);
            var nuevomontoDol = montoDolares;

            decimal montoConError = (nuevomontoDol * tipoCambioOficial);
            montoConError = Math.Round(montoConError, 2);
            if (montoConError == montoTotalCobrar)
            {
                nuevoTipoCambioAproximado = Math.Round(tipoCambioOficial, 2);
                tipoCambioOficial = Math.Round(tipoCambioOficial, 2);
            }
            else
            {
                nuevoTipoCambioAproximado = ObtenerNuevoTipoCambioExcto(montoTotalCobrar, montoConError, montoDolares);
                //nuevoTipoCambioAproximado = tipoCambioOficial;
            }*/


            //llenar el combox tipo de tarjeta
            this.cboTipoTarjeta.ValueMember = "Tipo_Tarjeta";
            this.cboTipoTarjeta.DisplayMember = "Tipo_Tarjeta";
            this.cboTipoTarjeta.DataSource = listaTipoTarjeta;

            //llenar el combox condicion de pago
            this.cboCondicionPago.ValueMember = "Condicion_Pago";
            this.cboCondicionPago.DisplayMember = "Descripcion";
            this.cboCondicionPago.DataSource = this.listaCondicionPago;

            this.cboFormaPago.ValueMember = "Forma_Pago";
            this.cboFormaPago.DisplayMember = "Descripcion";
            this.cboFormaPago.DataSource = listFormaPago;
            
        }





        private void txtEfectivoCordoba_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (this.txtEfectivoCordoba.Enabled)
                {
                    //obtener el valor del textbox, ademas valida por si el textbox esta vacio
                    decimal valor = this.txtEfectivoCordoba.Text.Trim().Length == 0 ? 0.00M : Convert.ToDecimal(this.txtEfectivoCordoba.Text);
                    //hacer la conversion al tipo de  cambio del dia
                    valor = valor / tipoCambioOficial;
                    // this.txtEfectivoDolar.Text = $"U${valor.ToString("N2")}";
                }
            }
            catch
            {

            }
        }

        private void txtTarjetaCordoba_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtEfectivoDolar_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (this.txtEfectivoDolar.Enabled)
                {
                    //obtener el valor del textbox, ademas valida por si el textbox esta vacio
                    decimal valor = this.txtEfectivoDolar.Text.Trim().Length == 0 ? 0.00M : Convert.ToDecimal(this.txtEfectivoDolar.Text);
                    //hacer la conversion al tipo de  cambio del dia             
                    this.lblConvertidorDolares.Text = $"U${valor.ToString("N2")} = C${(valor * tipoCambioOficial).ToString("N2")}";
                }
            }
            catch
            {

            }
        }

        /*************************************************   EVENTO CLICK ***********************************************************************************************************************/
        #region Evento Click
        private void btnEfectivoCordoba_Click(object sender, EventArgs e)
        {
            //comprobar si no esta bloqueado el metodo de pago
            if (!bloquearMetodoPago)
            {
                this.lblTituloMontoGeneral.Text = "Monto - Efectivo C$:";
                this.lblTituloCombox.Visible = false;
                this.lblTituloDocumento.Visible = false;
                this.lblConvertidorDolares.Visible = false;

                //poner visible el label F11 Tarjeta Dolar
                SetCambiarEstadoVisibleLableF11Dolar("F11_ED", true);
                //cambiar de estado solo si el cajero decidio cambiar el modo de pago
                setCambiarEstadoTextBoxMetodoPago(teclaPresionadaXCajero, false);
                //indicar que el cajero presiono F1;
                teclaPresionadaXCajero = "F1";
                //cambiar el estado del textbox y habilitarlo
                setCambiarEstadoTextBoxMetodoPago(teclaPresionadaXCajero, true);

                txtEfectivoCordoba.SelectionStart = 0;
                txtEfectivoCordoba.SelectionLength = this.txtEfectivoCordoba.Text.Length;
                txtEfectivoCordoba.Focus();

                ActivarfocusMontoGeneral();
            }
            else
            {
                MessageBox.Show("Ya se cobró el total de la factura", "Sistema COVENTAF");
            }
        }

        private void lblEfectivoCordoba_Click(object sender, EventArgs e)
        {
            btnEfectivoCordoba_Click(null, null);
        }

        private void lblEfectivoDolar_Click(object sender, EventArgs e)
        {
            //comprobar si no esta bloqueado el metodo de pago
            if (!bloquearMetodoPago)
            {
                this.lblTituloMontoGeneral.Text = "Monto - Efectivo U$:";
                //desactivar el texbox y mostrar la suma pagado por el cliente en caso que existen varios tipo de pago
                setCambiarEstadoTextBoxMetodoPago(teclaPresionadaXCajero, false);
                //indicar la ultima tecla ejecutado por el cajero
                teclaPresionadaXCajero = "F11_ED";
                //
                SetCambiarEstadoVisibleLableF11Dolar(teclaPresionadaXCajero, false);
                //actualizar el estado del textbox ademas mostrar el monto a pagar en dolares
                setCambiarEstadoTextBoxMetodoPago(teclaPresionadaXCajero, true);

                this.lblConvertidorDolares.Visible = true;
                this.txtEfectivoDolar.SelectionStart = 0;
                this.txtEfectivoDolar.SelectionLength = this.txtEfectivoDolar.Text.Length;
                this.txtEfectivoDolar.Focus();

                ActivarfocusMontoGeneral();
            }
            else
            {
                MessageBox.Show("Ya se cobró el total de la factura", "Sistema COVENTAF");
            }
        }

        private void btnChequeCordoba_Click(object sender, EventArgs e)
        {
            //comprobar si no esta bloqueado el metodo de pago
            if (!bloquearMetodoPago)
            {
                this.lblTituloCombox.Text = "Entidad Financiera:";
                this.lblTituloMontoGeneral.Text = "Monto Cheque C$:";
                //  this.lblTituloCom
                //this.pnlTipoTarjeta.Visible = false;
                //this.pnlTipoCredito.Visible = false;
                this.lblConvertidorDolares.Visible = false;


                //cambiar de estado solo si el cajero decidio cambiar el modo de pago
                setCambiarEstadoTextBoxMetodoPago(teclaPresionadaXCajero, false);

                //poner visible el label F11 Tarjeta Dolar
                SetCambiarEstadoVisibleLableF11Dolar("F11_CHD", true);
                //indicar que el cajero presiono F2;
                teclaPresionadaXCajero = "F2";
                //cambiar el estado del textbox y habilitarlo
                setCambiarEstadoTextBoxMetodoPago(teclaPresionadaXCajero, true);

                this.txtChequeCordoba.SelectionStart = 0;
                this.txtChequeCordoba.SelectionLength = this.txtChequeCordoba.Text.Length;
                this.txtChequeCordoba.Focus();

                ActivarfocusMontoGeneral();
            }
            else
            {
                MessageBox.Show("Ya se cobró el total de la factura", "Sistema COVENTAF");
            }
        }
        private void lblChequeCordoba_Click(object sender, EventArgs e)
        {
            btnChequeCordoba_Click(null, null);
        }

        private void lblChequeDolar_Click(object sender, EventArgs e)
        {
            //comprobar si no esta bloqueado el metodo de pago
            if (!bloquearMetodoPago)
            {
                this.lblTituloMontoGeneral.Text = "Monto Cheque U$:";
                //desactivar el texbox y mostrar la suma pagado por el cliente en caso que existen varios tipo de pago
                setCambiarEstadoTextBoxMetodoPago(teclaPresionadaXCajero, false);
                //indicar la ultima tecla ejecutado por el cajero
                teclaPresionadaXCajero = "F11_CHD";
                //
                SetCambiarEstadoVisibleLableF11Dolar(teclaPresionadaXCajero, false);
                //actualizar el estado del textbox ademas mostrar el monto a pagar en dolares
                setCambiarEstadoTextBoxMetodoPago(teclaPresionadaXCajero, true);

                this.lblConvertidorDolares.Visible = true;
                this.txtChequeDolar.SelectionStart = 0;
                this.txtChequeDolar.SelectionLength = this.txtChequeDolar.Text.Length;
                this.txtChequeDolar.Focus();

                ActivarfocusMontoGeneral();
            }
            else
            {
                MessageBox.Show("Ya se cobró el total de la factura", "Sistema COVENTAF");
            }
        }

        private void btnTarjetaCordoba_Click(object sender, EventArgs e)
        {

            if (!bloquearMetodoPago)
            {
                this.lblTituloMontoGeneral.Text = "Monto en Tarjeta C$:";
                //this.pnlTipoTarjeta.Visible = true;
                //this.pnlTipoCredito.Visible = false;
                this.lblConvertidorDolares.Visible = false;

                //cambiar el estado de visible al label F11 Tarjeta Dolar
                setCambiarEstadoTextBoxMetodoPago(teclaPresionadaXCajero, false);

                //activo obligatoriamente por haber presionado F11 para Tarjeta para Dolares (F11 )
                SetCambiarEstadoVisibleLableF11Dolar("F11_TD", true);

                //cambiar el estado e indicar que se presiono F3
                teclaPresionadaXCajero = "F3";
                setCambiarEstadoTextBoxMetodoPago(teclaPresionadaXCajero, true);

                //poner el foco en el textbox
                this.txtTarjetaCordoba.SelectionStart = 0;
                this.txtTarjetaCordoba.SelectionLength = this.txtTarjetaCordoba.Text.Length;
                this.txtTarjetaCordoba.Focus();

                ActivarfocusMontoGeneral();
            }
            else
            {
                MessageBox.Show("Ya se cobró el total de la factura", "Sistema COVENTAF");
            }
        }

        private void lblTarjetaCordoba_Click(object sender, EventArgs e)
        {
            btnTarjetaCordoba_Click(null, null);
        }

        private void lblTarjetaDolar_Click(object sender, EventArgs e)
        {
            if (!bloquearMetodoPago)
            {
                this.lblTituloMontoGeneral.Text = "Monto en Tarjeta U$:";
                //desactivar el texbox y mostrar la suma pagado por el cliente en caso que existen varios tipo de pago
                setCambiarEstadoTextBoxMetodoPago(teclaPresionadaXCajero, false);
                //actualizar el estado. indicar la ultima tecla ejecutado por el cajero
                teclaPresionadaXCajero = "F11_TD";
                SetCambiarEstadoVisibleLableF11Dolar(teclaPresionadaXCajero, false);
                //actualizar el estado del textbox ademas mostrar el monto a pagar en Cordobas y dolares
                setCambiarEstadoTextBoxMetodoPago(teclaPresionadaXCajero, true);
                this.lblConvertidorDolares.Visible = true;

                this.txtTarjetaDolar.SelectionStart = 0;
                this.txtTarjetaDolar.SelectionLength = this.txtTarjetaDolar.Text.Length;
                this.txtTarjetaDolar.Focus();

                ActivarfocusMontoGeneral();
            }
            else
            {
                MessageBox.Show("Ya se cobró el total de la factura", "Sistema COVENTAF");
            }
        }

        private void btnCredito_Click(object sender, EventArgs e)
        {
            if (!bloquearMetodoPago)
            {
                this.lblTituloMontoGeneral.Text = "Monto Credito C$:";
                //this.pnlTipoTarjeta.Visible = false;
                //this.pnlTipoCredito.Visible = true;
                this.lblConvertidorDolares.Visible = false;
                SetCambiarEstadoVisibleLableF11Dolar("", false);

                //cambiar el estado de visible al label F11 Tarjeta Dolar
                setCambiarEstadoTextBoxMetodoPago(teclaPresionadaXCajero, false);
                //cambiar el estado e indicar que se presiono F3
                teclaPresionadaXCajero = "F4";
                setCambiarEstadoTextBoxMetodoPago(teclaPresionadaXCajero, true);

                this.txtCredito.SelectionStart = 0;
                this.txtCredito.SelectionLength = this.txtCredito.Text.Length;
                this.txtCredito.Focus();

                ActivarfocusMontoGeneral();

            }
            else
            {
                MessageBox.Show("Ya se cobró el total de la factura", "Sistema COVENTAF");
            }
        }

        private void lblCredito_Click(object sender, EventArgs e)
        {
            btnCredito_Click(null, null);
        }


        private void btnBono_Click(object sender, EventArgs e)
        {
            if (!bloquearMetodoPago)
            {
                this.lblTituloMontoGeneral.Text = "Monto del Bono C$:";
                //this.pnlTipoTarjeta.Visible = false;
                //this.pnlTipoCredito.Visible = false;
                this.lblConvertidorDolares.Visible = false;
                SetCambiarEstadoVisibleLableF11Dolar("", false);

                //cambiar el estado de visible 
                setCambiarEstadoTextBoxMetodoPago(teclaPresionadaXCajero, false);
                //cambiar el estado e indicar que se presiono F5
                teclaPresionadaXCajero = "F5";
                setCambiarEstadoTextBoxMetodoPago(teclaPresionadaXCajero, true);


                this.txtBono.SelectionStart = 0;
                this.txtBono.SelectionLength = this.txtBono.Text.Length;
                this.txtBono.Focus();

                ActivarfocusMontoGeneral();

            }
            else
            {
                MessageBox.Show("Ya se cobró el total de la factura", "Sistema COVENTAF");
            }
        }

        private void lblBono_Click(object sender, EventArgs e)
        {
            btnBono_Click(null, null);
        }

        private void btnGiftCardCordobar_Click(object sender, EventArgs e)
        {
            if (!bloquearMetodoPago)
            {
                this.lblTituloMontoGeneral.Text = "Monto Gift Card C$:";
                //this.pnlTipoTarjeta.Visible = false;
                //this.pnlTipoCredito.Visible = false;
                this.lblConvertidorDolares.Visible = false;

                //cambiar el estado de visible al label F11 Tarjeta Dolar
                setCambiarEstadoTextBoxMetodoPago(teclaPresionadaXCajero, false);

                //activo por defecto el evento F11 para Tarjeta para Dolares (F11 )
                SetCambiarEstadoVisibleLableF11Dolar("F11_GCD", true);

                //cambiar el estado e indicar que se presiono F6
                teclaPresionadaXCajero = "F6";
                setCambiarEstadoTextBoxMetodoPago(teclaPresionadaXCajero, true);

                //poner el foco en el textbox
                this.txtGiftCardCordoba.SelectionStart = 0;
                this.txtGiftCardCordoba.SelectionLength = this.txtGiftCardCordoba.Text.Length;
                this.txtGiftCardCordoba.Focus();

                ActivarfocusMontoGeneral();
            }
            else
            {
                MessageBox.Show("Ya se cobró el total de la factura", "Sistema COVENTAF");
            }
        }

        private void lblGiftCardCordoba_Click(object sender, EventArgs e)
        {
            btnGiftCardCordobar_Click(null, null);
        }

        private void lblGiftCardDolar_Click(object sender, EventArgs e)
        {
            if (!bloquearMetodoPago)
            {
                this.lblTituloMontoGeneral.Text = "Monto Gift Card U$:";
                //desactivar el texbox y mostrar la suma pagado por el cliente en caso que existen varios tipo de pago
                setCambiarEstadoTextBoxMetodoPago(teclaPresionadaXCajero, false);
                //actualizar el estado. indicar la ultima tecla ejecutado por el cajero
                teclaPresionadaXCajero = "F11_GCD";
                SetCambiarEstadoVisibleLableF11Dolar(teclaPresionadaXCajero, false);
                //actualizar el estado del textbox ademas mostrar el monto a pagar en Cordobas y dolares
                setCambiarEstadoTextBoxMetodoPago(teclaPresionadaXCajero, true);
                this.lblConvertidorDolares.Visible = true;

                this.txtGiftCardDolar.SelectionStart = 0;
                this.txtGiftCardDolar.SelectionLength = this.txtGiftCardDolar.Text.Length;
                this.txtGiftCardDolar.Focus();

                ActivarfocusMontoGeneral();
            }
            else
            {
                MessageBox.Show("Ya se cobró el total de la factura", "Sistema COVENTAF");
            }
        }

        #endregion fin Click
        /***************************************************************************************************************************************************************************************/



        /*************************************** EVENTOS KEYPRESS ************************************************************************************************************************************/
        #region eventos de los textBox KEYPRRESS

        //txtEfectivoCordoba_KeyPress
        private void txtEfectivoCordoba_KeyPress(object sender, KeyPressEventArgs e)
        {
            Services.Utilidades.UnPunto(e, this.txtEfectivoCordoba.Text.Trim(), ref bandera);

            if (e.KeyChar == 13 && this.txtEfectivoCordoba.Text.Trim().Length > 0)
            {
                //llamar el metodo asignar pago
                AsginarMetodoPago("0001", "Efectivo", Convert.ToDecimal(this.txtEfectivoCordoba.Text), 'L', true, "F1", null);
                setCambiarEstadoTextBoxMetodoPago(teclaPresionadaXCajero, false);
                this.txtEfectivoCordoba.Enabled = false;
                teclaPresionadaXCajero = "";
                this.btnGuardarFactura.Focus();
            }

        }

        //txtEfectivoDolar_KeyPress
        private void txtEfectivoDolar_KeyPress(object sender, KeyPressEventArgs e)
        {
            Services.Utilidades.UnPunto(e, this.txtEfectivoDolar.Text.Trim(), ref bandera);
            if (e.KeyChar == 13)
            {

                //asignar el metodo de pago
                AsginarMetodoPago("0001", "Efectivo (Dolar)", Convert.ToDecimal(this.txtEfectivoDolar.Text), 'D', true, "F11_ED", null);

                //desactivar el texbox y mostrar la suma pagado por el cliente en caso que existen varios tipo de pago
                setCambiarEstadoTextBoxMetodoPago(teclaPresionadaXCajero, false);

                SetCambiarEstadoVisibleLableF11Dolar("F11_ED", false);
                this.lblConvertidorDolares.Visible = false;
                //cambiar el estado ya que fue utilizado
                teclaPresionadaXCajero = "";
            }
        }

        private void txtChequeCordoba_KeyPress(object sender, KeyPressEventArgs e)
        {
            Services.Utilidades.UnPunto(e, this.txtChequeCordoba.Text.Trim(), ref bandera);

            if (e.KeyChar == 13 && this.txtChequeCordoba.Text.Trim().Length > 0)
            {
                //if ( this.txtEfectivoCordoba.Text =="0.00")
                //{
                //llamar el metodo asignar pago
                AsginarMetodoPago("0002", "Cheque", Convert.ToDecimal(this.txtChequeCordoba.Text), 'L', true, "F2", null);
                setCambiarEstadoTextBoxMetodoPago(teclaPresionadaXCajero, false);
                this.txtEfectivoCordoba.Enabled = false;
                teclaPresionadaXCajero = "";
                //}
                //else
                //{
                //    MessageBox.Show("El Monto debe ser superior a cero", "Sistema COVENTAF");
                //}

            }
        }

        private void txtChequeDolar_KeyPress(object sender, KeyPressEventArgs e)
        {
            Services.Utilidades.UnPunto(e, this.txtChequeDolar.Text.Trim(), ref bandera);
            if (e.KeyChar == 13)
            {
                //asignar el metodo de pago
                AsginarMetodoPago("0002", "Cheque (Dolar)", Convert.ToDecimal(this.txtChequeDolar.Text), 'D', true, "F11_ED", null);

                //desactivar el texbox y mostrar la suma pagado por el cliente en caso que existen varios tipo de pago
                setCambiarEstadoTextBoxMetodoPago(teclaPresionadaXCajero, false);

                SetCambiarEstadoVisibleLableF11Dolar("F11_ED", false);
                this.lblConvertidorDolares.Visible = false;
                //cambiar el estado ya que fue utilizado
                teclaPresionadaXCajero = "";
            }
        }

        private void txtTarjetaCordoba_KeyPress(object sender, KeyPressEventArgs e)
        {
            Services.Utilidades.UnPunto(e, this.txtTarjetaCordoba.Text.Trim(), ref bandera);

            if (e.KeyChar == 13 && txtTarjetaCordoba.Text.Trim().Length > 0)
            {

                //llamar el metodo asignar pago
                AsginarMetodoPago("0003", "Tarjeta", Convert.ToDecimal(this.txtTarjetaCordoba.Text), 'L', true, "F3", null, "", this.cboTipoTarjeta.SelectedValue.ToString());
                setCambiarEstadoTextBoxMetodoPago(teclaPresionadaXCajero, false);
                //cambiar el estado ya que fue utilizado
                teclaPresionadaXCajero = "";
            }
        }

        private void txtTarjetaDolar_KeyPress(object sender, KeyPressEventArgs e)
        {
            Services.Utilidades.UnPunto(e, this.txtTarjetaDolar.Text.Trim(), ref bandera);

            if (e.KeyChar == 13 && this.txtTarjetaDolar.Text.Length > 0 && !char.IsDigit(e.KeyChar))
            {

                //llamar el metodo asignar pago
                //oscar esta es la correcta
                //AsginarMetodoPago("Tarjeta (Dolar)", Convert.ToDecimal(this.txtTarjetaDolar.Text), 'D', true, "F11_TD", this.cboTipoTarjeta.SelectedValue.ToString());
                //aqui falta el parametro el tipo de tarjeta
                AsginarMetodoPago("0003", "Tarjeta (Dolar)", Convert.ToDecimal(this.txtTarjetaDolar.Text), 'D', true, "F11_TD", null, "", this.cboTipoTarjeta.SelectedValue.ToString());
                //cambiar el estado
                setCambiarEstadoTextBoxMetodoPago(teclaPresionadaXCajero, false);
                //cambiar el estado ya que fue utilizado
                teclaPresionadaXCajero = "";
                this.lblConvertidorDolares.Visible = false;
            }

        }

        private void txtCredito_KeyPress(object sender, KeyPressEventArgs e)
        {
            Services.Utilidades.UnPunto(e, this.txtCredito.Text.Trim(), ref bandera);

            if (e.KeyChar == 13 && this.txtCredito.Text.Length > 0 && !char.IsDigit(e.KeyChar))
            {
                //llamar el metodo asignar pago                                           
                AsginarMetodoPago("0004", "Credito", Convert.ToDecimal(this.txtCredito.Text), 'L', true, teclaPresionadaXCajero, this.cboCondicionPago.SelectedValue.ToString(), this.cboCondicionPago.Text, null);
                //cambiar el estado
                setCambiarEstadoTextBoxMetodoPago(teclaPresionadaXCajero, false);
                //cambiar el estado ya que fue utilizado
                teclaPresionadaXCajero = "";
                this.lblConvertidorDolares.Visible = false;
            }
        }

        private void txtBono_KeyPress(object sender, KeyPressEventArgs e)
        {
            Services.Utilidades.UnPunto(e, this.txtBono.Text.Trim(), ref bandera);
            if (e.KeyChar == 13 && this.txtBono.Text.Length > 0 && !char.IsDigit(e.KeyChar))
            {
                //llamar el metodo asignar pago                                           
                AsginarMetodoPago("0006", "Bono", Convert.ToDecimal(this.txtBono.Text), 'L', true, teclaPresionadaXCajero, null);
                //cambiar el estado
                setCambiarEstadoTextBoxMetodoPago(teclaPresionadaXCajero, false);
                //cambiar el estado ya que fue utilizado
                teclaPresionadaXCajero = "";
                this.lblConvertidorDolares.Visible = false;
            }
        }

        private void txtGiftCardCordoba_KeyPress(object sender, KeyPressEventArgs e)
        {
            Services.Utilidades.UnPunto(e, this.txtGiftCardCordoba.Text.Trim(), ref bandera);
            if (e.KeyChar == 13 && this.txtGiftCardCordoba.Text.Length > 0 && !char.IsDigit(e.KeyChar))
            {
                //llamar el metodo asignar pago                                           
                AsginarMetodoPago("FP01", "GiftCard", Convert.ToDecimal(this.txtGiftCardCordoba.Text), 'L', true, teclaPresionadaXCajero, null);
                //cambiar el estado
                setCambiarEstadoTextBoxMetodoPago(teclaPresionadaXCajero, false);
                //cambiar el estado ya que fue utilizado
                teclaPresionadaXCajero = "";
                this.lblConvertidorDolares.Visible = false;
            }
        }

        private void txtGiftCardDolar_KeyPress(object sender, KeyPressEventArgs e)
        {
            Services.Utilidades.UnPunto(e, this.txtGiftCardDolar.Text.Trim(), ref bandera);
            if (e.KeyChar == 13 && this.txtGiftCardDolar.Text.Length > 0 && !char.IsDigit(e.KeyChar))
            {
                //llamar el metodo asignar pago                                           
                AsginarMetodoPago("FP01", "GiftCard (Dolar)", Convert.ToDecimal(this.txtGiftCardDolar.Text), 'D', true, teclaPresionadaXCajero, null);
                //cambiar el estado
                setCambiarEstadoTextBoxMetodoPago(teclaPresionadaXCajero, false);
                //cambiar el estado ya que fue utilizado
                teclaPresionadaXCajero = "";
                this.lblConvertidorDolares.Visible = false;
            }
        }

        #endregion
        /********************************************************************************************************************************************************************************************/

        /// <summary>
        /// cmabiar el estado de visibilidad a los label F11
        /// </summary>
        /// <param name="labelName"></param>
        /// <param name="visible"></param>
        void SetCambiarEstadoVisibleLableF11Dolar(string labelName, bool visible)
        {
            this.lblEfectivoDolar.Visible = false;
            this.lblChequeDolar.Visible = false;
            this.lblTarjetaDolar.Visible = false;
            this.lblGiftCardDolar.Visible = false;

            switch (labelName)
            {
                case "F11_ED":
                    this.lblEfectivoDolar.Visible = visible;
                    break;
                case "F11_CHD":
                    this.lblChequeDolar.Visible = visible;
                    break;
                case "F11_TD":
                    this.lblTarjetaDolar.Visible = visible;
                    break;

                case "F11_GCD":
                    this.lblGiftCardDolar.Visible = visible;
                    break;
            }
        }


        /// <summary>
        /// cambiar el estado 
        /// </summary>
        /// <param name="textBoxName"></param>
        /// <param name="enable"></param>
        void setCambiarEstadoTextBoxMetodoPago(string textBoxName, bool enable)
        {
            decimal valorMonto = 0.0000M;

            //en caso textBoxName igual a cero terminar el proceso 
            if (textBoxName.Length == 0) return;

            this.txtEfectivoCordoba.Enabled = false;
            this.txtEfectivoDolar.Enabled = false;
            this.txtChequeCordoba.Enabled = false;
            this.txtChequeDolar.Enabled = false;
            this.txtTarjetaCordoba.Enabled = false;
            this.txtTarjetaDolar.Enabled = false;
            this.txtCredito.Enabled = false;
            this.txtBono.Enabled = false;
            this.txtGiftCardCordoba.Enabled = false;
            this.txtGiftCardDolar.Enabled = false;
            this.lblTituloCombox.Visible = false;
            this.lblTituloDocumento.Visible = false;
            this.txtDocumento.Visible = false;

            this.cboCondicionPago.Visible = false;
            this.cboTipoTarjeta.Visible = false;
            this.cboEntidadFinanciera.Visible = false;
            //
            switch (textBoxName)
            {
                //F1= Efectivo Cordoba
                case "F1":
                    valorMonto = (enable ? GetMontoCobrar() : GetMontoMontoPorMetodoPagoX(textBoxName));
                    //revisar si estoy habilitando para obtener el monto a pagar, de lo contrario revisar si ya pago                   
                    this.txtEfectivoCordoba.Text = (enable ? valorMonto.ToString("N2") : $"C${ valorMonto.ToString("N2")}");
                    //this.txtEfectivoDolar.Text =$"U${(valorMonto / tipoCambio).ToString("N2")}";
                    this.txtEfectivoCordoba.Enabled = enable;
                    codigoTipoPago = "0001";
                    tipoPago = "Efectivo";
                    moneda = 'L';
                    break;

                //F11=Efectivo Dolar
                case "F11_ED":
                    //obtener el monto a pagar o el monto pagado por el cliente siempre en cordobas
                    valorMonto = (enable ? GetMontoCobrar() : GetMontoMontoPorMetodoPagoX(textBoxName));
                    montoFinalCobrarDolar = Math.Round((valorMonto / tipoCambioOficial), 2);
                    this.txtEfectivoDolar.Text = (enable ? (valorMonto / tipoCambioOficial).ToString("N2") : $"U${(valorMonto / tipoCambioOficial).ToString("N2")}");
                    this.lblConvertidorDolares.Text = $"U${(valorMonto / tipoCambioOficial).ToString("N2")} = C${valorMonto.ToString("N2")} ";
                    //this.txtEfectivoCordoba.Text = $"C${ valorMonto.ToString("N2")}";
                    this.txtEfectivoDolar.Enabled = enable;
                    valorMonto = valorMonto / tipoCambioOficial;
                    codigoTipoPago = "0001";
                    tipoPago = "Efectivo (Dolar)";
                    moneda = 'D';
                    break;

                //F2=Efectivo Cordoba
                case "F2":
                    valorMonto = (enable ? GetMontoCobrar() : GetMontoMontoPorMetodoPagoX(textBoxName));
                    //obtener el monto a pagar o el monto pagado por el cliente                
                    this.txtChequeCordoba.Text = (enable ? GetMontoCobrar().ToString("N2") : $"C${ GetMontoMontoPorMetodoPagoX(textBoxName).ToString("N2")}");
                    this.txtChequeCordoba.Enabled = enable;
                    this.lblTituloCombox.Text = "Entidad Financiera:";
                    this.lblTituloCombox.Visible = enable;
                    this.cboEntidadFinanciera.Visible = enable;
                    this.lblTituloDocumento.Text = "Numero de cheque:";
                    this.lblTituloDocumento.Visible = enable;
                    this.txtDocumento.Visible = enable;
                    codigoTipoPago = "0002";
                    tipoPago = "Cheque";
                    moneda = 'L';

                    break;

                //F11  = cheque Dolar
                case "F11_CHD":
                    //obtener el monto a pagar o el monto pagado por el cliente
                    valorMonto = (enable ? GetMontoCobrar() : GetMontoMontoPorMetodoPagoX(textBoxName));
                    montoFinalCobrarDolar = Math.Round((valorMonto / tipoCambioOficial), 2);
                    this.txtChequeDolar.Text = (enable ? (valorMonto / tipoCambioOficial).ToString("N2") : $"U${ (valorMonto / tipoCambioOficial).ToString("N2")}");
                    this.lblConvertidorDolares.Text = $"U${(valorMonto / tipoCambioOficial).ToString("N2")} = C${valorMonto.ToString("N2")} ";
                    this.txtChequeDolar.Enabled = enable;
                    valorMonto = valorMonto / tipoCambioOficial;
                    this.lblTituloCombox.Text = "Entidad Financiera:";
                    this.lblTituloCombox.Visible = enable;
                    this.cboEntidadFinanciera.Visible = enable;
                    this.lblTituloDocumento.Text = "Numero de cheque:";
                    this.lblTituloDocumento.Visible = enable;
                    this.txtDocumento.Visible = enable;

                    codigoTipoPago = "0002";
                    tipoPago = "Cheque (Dolar)";
                    moneda = 'D';
                    break;

                //F3=Tarjeta Cordoba
                case "F3":
                    //obtener el monto a pagar o el monto pagado por el cliente
                    valorMonto = (enable ? GetMontoCobrar() : GetMontoMontoPorMetodoPagoX(textBoxName));
                    this.txtTarjetaCordoba.Text = (enable ? valorMonto.ToString("N2") : $"C${ valorMonto.ToString("N2")}");
                    //this.txtTarjetaDolar.Text = $"U${(valorMonto / tipoCambio).ToString("N2")}";
                    this.txtTarjetaCordoba.Enabled = enable;

                    this.lblTituloCombox.Text = "Tipo de Tarjeta:";
                    this.lblTituloCombox.Visible = enable;
                    this.cboTipoTarjeta.Visible = enable;
                    this.lblTituloDocumento.Text = "Numero de Tarjeta:";
                    this.lblTituloDocumento.Visible = enable;
                    this.txtDocumento.Visible = enable;

                    codigoTipoPago = "0003";
                    tipoPago = "Tarjeta";
                    moneda = 'L';

                    break;

                //F11_TD=Telca F11 Tarjeta Dolar
                case "F11_TD":
                    //obtener el monto a pagar o el monto pagado por el cliente
                    valorMonto = (enable ? GetMontoCobrar() : GetMontoMontoPorMetodoPagoX(textBoxName));
                    montoFinalCobrarDolar = Math.Round((valorMonto / tipoCambioOficial), 2);
                    this.txtTarjetaDolar.Text = (enable ? (valorMonto / tipoCambioOficial).ToString("N2") : $"U${(valorMonto / tipoCambioOficial).ToString("N2")}");
                    this.lblConvertidorDolares.Text = $"U${(valorMonto / tipoCambioOficial).ToString("N2")} = C${valorMonto.ToString("N2")} ";
                    this.txtTarjetaDolar.Enabled = enable;
                    valorMonto = valorMonto / tipoCambioOficial;

                    this.lblTituloCombox.Text = "Tipo de Tarjeta:";
                    this.lblTituloCombox.Visible = enable;
                    this.cboTipoTarjeta.Visible = enable;
                    this.lblTituloDocumento.Text = "Numero de Tarjeta:";
                    this.lblTituloDocumento.Visible = enable;
                    this.txtDocumento.Visible = enable;

                    codigoTipoPago = "0003";
                    tipoPago = "Tarjeta (Dolar)";
                    moneda = 'D';

                    break;

                //F4 =Credito cordoba
                case "F4":

                    //obtener el monto a pagar o el monto pagado por el cliente
                    valorMonto = (enable ? GetMontoCobrar() : GetMontoMontoPorMetodoPagoX(textBoxName));
                    this.txtCredito.Text = (enable ? valorMonto.ToString("N2") : $"C${ valorMonto.ToString("N2")}");
                    this.txtCredito.Enabled = enable;

                    this.lblTituloCombox.Text = "Condicion de pago:";
                    this.lblTituloCombox.Visible = enable;
                    this.cboTipoTarjeta.Visible = enable;
                    this.lblTituloDocumento.Text = "No. de documento:";
                    this.lblTituloDocumento.Visible = enable;
                    this.txtDocumento.Visible = enable;

                    codigoTipoPago = "0004";
                    tipoPago = "Credito";
                    moneda = 'L';

                    break;

                //F5=Bono cordoba
                case "F5":
                    //obtener el monto a pagar o el monto pagado por el cliente
                    valorMonto = (enable ? GetMontoCobrar() : GetMontoMontoPorMetodoPagoX(textBoxName));
                    this.txtBono.Text = (enable ? valorMonto.ToString("N2") : $"C${ valorMonto.ToString("N2")}");
                    this.txtBono.Enabled = enable;

                    this.lblTituloDocumento.Text = "No. de documento:";
                    this.lblTituloDocumento.Visible = enable;
                    this.txtDocumento.Visible = enable;

                    codigoTipoPago = "0006";
                    tipoPago = "Bono";
                    moneda = 'L';
                    break;

                //F6=GiftCard Cordobas
                case "F6":
                    valorMonto = (enable ? GetMontoCobrar() : GetMontoMontoPorMetodoPagoX(textBoxName));
                    //revisar si estoy habilitando para obtener el monto a pagar, de lo contrario revisar si ya pagp                       
                    this.txtGiftCardCordoba.Text = (enable ? GetMontoCobrar().ToString("N2") : $"C${ GetMontoMontoPorMetodoPagoX(textBoxName).ToString("N2")}");
                    this.txtGiftCardCordoba.Enabled = enable;

                    this.lblTituloDocumento.Text = "Numero de Tarjeta:";
                    this.lblTituloDocumento.Visible = enable;
                    this.txtDocumento.Visible = enable;

                    codigoTipoPago = "FP01";
                    tipoPago = "GiftCard";
                    moneda = 'L';

                    break;

                //F11_GCD= GiftCard Dolar
                case "F11_GCD":

                    //obtener el monto a pagar o el monto pagado por el cliente siempre en cordobas
                    valorMonto = (enable ? GetMontoCobrar() : GetMontoMontoPorMetodoPagoX(textBoxName));
                    montoFinalCobrarDolar = Math.Round((valorMonto / tipoCambioOficial), 2);
                    this.txtGiftCardDolar.Text = (enable ? (valorMonto / tipoCambioOficial).ToString("N2") : $"U${(valorMonto / tipoCambioOficial).ToString("N2")}");
                    this.lblConvertidorDolares.Text = $"U${(valorMonto / tipoCambioOficial).ToString("N2")} = C${valorMonto.ToString("N2")} ";
                    this.txtGiftCardDolar.Enabled = enable;
                    valorMonto = valorMonto / tipoCambioOficial;

                    this.lblTituloDocumento.Text = "Numero de Tarjeta:";
                    this.lblTituloDocumento.Visible = enable;
                    this.txtDocumento.Visible = enable;

                    codigoTipoPago = "FP01";
                    tipoPago = "GiftCard (Dolar)";
                    moneda = 'D';

                    break;
            }

            this.txtMontoGeneral.Enabled = enable;
            //limpiar el monto
            this.txtMontoGeneral.Text = (enable ? valorMonto.ToString("N2") : "");
            this.txtDocumento.Text = "";
                    
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="nombreTeclaEjecutada"></param>
        /// <param name="retornarMonto"> le indico si retorna el monto en corodobas(L=Local) o _Dolares (D=Dolares). Por defecto es en Cordoba. </param>
        /// <returns></returns>

        decimal GetMontoMontoPorMetodoPagoX(string nombreTeclaEjecutada)
        {
            decimal montoPagado = 0;
            //sumar el monto pagado por el cliente solo que se mayor q cero y sea de la tecla presiona F1, F2, F3
            montoPagado = metodoPago.Where(mp => mp.Pago != "-1" && mp.DescripcionTecla == nombreTeclaEjecutada).Sum(x => x.MontoCordoba);

            return montoPagado;
        }


        decimal ObtenerNuevoTipoCambioExcto(decimal montoExacto, decimal montoConError, decimal montoDolares)
        {
            montoExacto = Math.Round(montoExacto, 2);

            decimal nuevoTipoCambio = tipoCambioOficial;
            //si el monto con error es mayor entonces hay que ir restando el tipo de cambio

            decimal nuevoMonto = 0;
            bool continuarCiclo = true;
            bool sumarTipoCambio = (montoConError < montoExacto ? true : false);

            while (continuarCiclo)
            {

                nuevoTipoCambio = sumarTipoCambio ? nuevoTipoCambio + 0.0001M : nuevoTipoCambio - 0.0001M;
                //nuevoTipoCambio = Math.Round(tipoCambio, 4);
                nuevoMonto = montoDolares * nuevoTipoCambio;
                var nuevoMontoConDosDecimal = Math.Round(nuevoMonto, 2);
                if (montoExacto == nuevoMontoConDosDecimal)
                {
                    continuarCiclo = false;
                    break;
                }

                if (nuevoMontoConDosDecimal < montoExacto)
                {
                    sumarTipoCambio = true;
                }
                else
                {
                    sumarTipoCambio = false;
                }

            }


            return nuevoTipoCambio;
        }

        private void txtTarjetaDolar_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (this.txtTarjetaDolar.Enabled)
                {

                    //obtener el valor del textbox, ademas valida por si el textbox esta vacio
                    decimal valor = this.txtTarjetaDolar.Text.Trim().Length == 0 ? 0.00M : Convert.ToDecimal(this.txtTarjetaDolar.Text);
                    if (Convert.ToDecimal(this.txtTarjetaDolar.Text) == diferenciaDolar)
                    {
                        //hacer la conversion al tipo de  cambio del dia             
                        this.lblConvertidorDolares.Text = $"U${valor.ToString("N2")} = C${diferenciaCordoba.ToString("N2")}";
                    }
                    else
                    {
                        //hacer la conversion al tipo de  cambio del dia             
                        this.lblConvertidorDolares.Text = $"U${valor.ToString("N2")} = C${(valor * tipoCambioOficial).ToString("N2")}";
                    }

                }
            }
            catch
            {

            }
        }




        /** evento ***/
        private void txtEfectivoCordoba_Enter(object sender, EventArgs e)
        {
            bandera = true;
        }

        private void txtEfectivoDolar_Enter(object sender, EventArgs e)
        {
            bandera = true;
        }

        private void txtChequeCordoba_Enter(object sender, EventArgs e)
        {
            bandera = true;
        }

        private void txtChequeDolar_Enter(object sender, EventArgs e)
        {
            bandera = true;
        }

        private void txtTarjetaCordoba_Enter(object sender, EventArgs e)
        {
            bandera = true;
        }

        private void txtTarjetaDolar_Enter(object sender, EventArgs e)
        {
            bandera = true;
        }

        private void txtCredito_Enter(object sender, EventArgs e)
        {
            bandera = true;
        }

        private void txtBono_Enter(object sender, EventArgs e)
        {
            bandera = true;
        }

        private void txtGiftCardCordoba_Enter(object sender, EventArgs e)
        {
            bandera = true;
        }

        private void txtGiftCardDolar_Enter(object sender, EventArgs e)
        {
            bandera = true;
        }


        private void btnGuardarFactura_Click(object sender, EventArgs e)
        {
            //indicar al sistema que el cajero va a guardar la factura
            GuardarFactura = true;
            //cerrar la ventana de metodo de pagos
            this.Close();
        }

        private void btnVerDetallePago_Click(object sender, EventArgs e)
        {
            var Detalleform = new frmDetallePago();
            Detalleform.metodoPago = metodoPago;
            Detalleform.ShowDialog();
            Detalleform.Dispose();
        }


        private void ActivarfocusMontoGeneral()
        {
            txtMontoGeneral.SelectionStart = 0;
            txtMontoGeneral.SelectionLength = this.txtMontoGeneral.Text.Length;
            txtMontoGeneral.Focus();
        }

        private void CursorUbicadoTextBox(string textBox)
        {
            bool result = false;
            switch (teclaPresionadaXCajero)
            {
                //F1= Efectivo Cordoba
                case "F1":


                    break;

                //F11=Efectivo Dolar
                case "F11_ED":
                    result = true;
                    //obtener el monto a pagar o el monto pagado por el cliente siempre en cordobas
                    //valorMonto = (enable ? GetMontoCobrar() : GetMontoMontoPorMetodoPagoX(textBoxName));
                    //montoFinalCobrarDolar = Math.Round((valorMonto / tipoCambioOficial), 2);
                    //this.txtEfectivoDolar.Text = (enable ? (valorMonto / tipoCambioOficial).ToString("N2") : $"U${(valorMonto / tipoCambioOficial).ToString("N2")}");
                    //this.lblConvertidorDolares.Text = $"U${(valorMonto / tipoCambioOficial).ToString("N2")} = C${valorMonto.ToString("N2")} ";
                    ////this.txtEfectivoCordoba.Text = $"C${ valorMonto.ToString("N2")}";
                    //this.txtEfectivoDolar.Enabled = enable;
                    //valorMonto = valorMonto / tipoCambioOficial;
                    break;

                //F2=Efectivo Cordoba
                case "F2":
                    //valorMonto = (enable ? GetMontoCobrar() : GetMontoMontoPorMetodoPagoX(textBoxName));
                    ////obtener el monto a pagar o el monto pagado por el cliente                
                    //this.txtChequeCordoba.Text = (enable ? GetMontoCobrar().ToString("N2") : $"C${ GetMontoMontoPorMetodoPagoX(textBoxName).ToString("N2")}");
                    //this.txtChequeCordoba.Enabled = enable;
                    break;

                //F11  = cheque Dolar
                case "F11_CHD":
                    result = true;
                    //obtener el monto a pagar o el monto pagado por el cliente
                    //valorMonto = (enable ? GetMontoCobrar() : GetMontoMontoPorMetodoPagoX(textBoxName));
                    //montoFinalCobrarDolar = Math.Round((valorMonto / tipoCambioOficial), 2);
                    //this.txtChequeDolar.Text = (enable ? (valorMonto / tipoCambioOficial).ToString("N2") : $"U${ (valorMonto / tipoCambioOficial).ToString("N2")}");
                    //this.lblConvertidorDolares.Text = $"U${(valorMonto / tipoCambioOficial).ToString("N2")} = C${valorMonto.ToString("N2")} ";
                    //this.txtChequeDolar.Enabled = enable;
                    //valorMonto = valorMonto / tipoCambioOficial;
                    break;

                //F3=Tarjeta Cordoba
                case "F3":
                    result = true;
                    ////obtener el monto a pagar o el monto pagado por el cliente
                    //valorMonto = (enable ? GetMontoCobrar() : GetMontoMontoPorMetodoPagoX(textBoxName));
                    //this.txtTarjetaCordoba.Text = (enable ? valorMonto.ToString("N2") : $"C${ valorMonto.ToString("N2")}");
                    ////this.txtTarjetaDolar.Text = $"U${(valorMonto / tipoCambio).ToString("N2")}";
                    //this.txtTarjetaCordoba.Enabled = enable;
                    break;

                //F11_TD=Telca F11 Tarjeta Dolar
                case "F11_TD":
                    result = true;
                    //obtener el monto a pagar o el monto pagado por el cliente
                    //valorMonto = (enable ? GetMontoCobrar() : GetMontoMontoPorMetodoPagoX(textBoxName));
                    //montoFinalCobrarDolar = Math.Round((valorMonto / tipoCambioOficial), 2);
                    //this.txtTarjetaDolar.Text = (enable ? (valorMonto / tipoCambioOficial).ToString("N2") : $"U${(valorMonto / tipoCambioOficial).ToString("N2")}");
                    //this.lblConvertidorDolares.Text = $"U${(valorMonto / tipoCambioOficial).ToString("N2")} = C${valorMonto.ToString("N2")} ";
                    //this.txtTarjetaDolar.Enabled = enable;
                    //valorMonto = valorMonto / tipoCambioOficial;
                    break;

                //F4 =Credito cordoba
                case "F4":
                    result = true;
                    ////obtener el monto a pagar o el monto pagado por el cliente
                    //valorMonto = (enable ? GetMontoCobrar() : GetMontoMontoPorMetodoPagoX(textBoxName));
                    //this.txtCredito.Text = (enable ? valorMonto.ToString("N2") : $"C${ valorMonto.ToString("N2")}");
                    //this.txtCredito.Enabled = enable;
                    break;

                //F5=Bono cordoba
                case "F5":
                    result = true;
                    //obtener el monto a pagar o el monto pagado por el cliente
                    //valorMonto = (enable ? GetMontoCobrar() : GetMontoMontoPorMetodoPagoX(textBoxName));
                    //this.txtBono.Text = (enable ? valorMonto.ToString("N2") : $"C${ valorMonto.ToString("N2")}");
                    //this.txtBono.Enabled = enable;
                    break;

                //F6=GiftCard Cordobas
                case "F6":
                    result = true;
                    //valorMonto = (enable ? GetMontoCobrar() : GetMontoMontoPorMetodoPagoX(textBoxName));
                    ////revisar si estoy habilitando para obtener el monto a pagar, de lo contrario revisar si ya pagp                       
                    //this.txtGiftCardCordoba.Text = (enable ? GetMontoCobrar().ToString("N2") : $"C${ GetMontoMontoPorMetodoPagoX(textBoxName).ToString("N2")}");
                    //this.txtGiftCardCordoba.Enabled = enable;
                    break;

                //F11_GCD= GiftCard Dolar
                case "F11_GCD":

                    //obtener el monto a pagar o el monto pagado por el cliente siempre en cordobas
                    //valorMonto = (enable ? GetMontoCobrar() : GetMontoMontoPorMetodoPagoX(textBoxName));
                    //montoFinalCobrarDolar = Math.Round((valorMonto / tipoCambioOficial), 2);
                    //this.txtGiftCardDolar.Text = (enable ? (valorMonto / tipoCambioOficial).ToString("N2") : $"U${(valorMonto / tipoCambioOficial).ToString("N2")}");
                    //this.lblConvertidorDolares.Text = $"U${(valorMonto / tipoCambioOficial).ToString("N2")} = C${valorMonto.ToString("N2")} ";
                    //this.txtGiftCardDolar.Enabled = enable;
                    //valorMonto = valorMonto / tipoCambioOficial;
                    break;
            }

        }

        private void txtMontoGeneral_TextChanged(object sender, EventArgs e)
        {

            try
            {
                //F11_ED =Efectivo Dolar ||F11_CHD = cheque Dolar, F11_TD=Tarjeta Dolar. F11_GCD= GiftCard Dolar
                if (teclaPresionadaXCajero == "F11_ED" || teclaPresionadaXCajero == "F11_CHD" || teclaPresionadaXCajero == "F11_TD" || teclaPresionadaXCajero == "F11_GCD")
                {

                    //obtener el valor del textbox, ademas valida por si el textbox esta vacio
                    decimal valor = this.txtMontoGeneral.Text.Trim().Length == 0 ? 0.00M : Convert.ToDecimal(this.txtMontoGeneral.Text);
                    if (Convert.ToDecimal(valor) == diferenciaDolar)
                    {
                        //hacer la conversion al tipo de  cambio del dia             
                        this.lblConvertidorDolares.Text = $"U${valor.ToString("N2")} = C${diferenciaCordoba.ToString("N2")}";
                    }
                    else
                    {
                        //hacer la conversion al tipo de  cambio del dia             
                        this.lblConvertidorDolares.Text = $"U${valor.ToString("N2")} = C${(valor * tipoCambioOficial).ToString("N2")}";
                    }
                }
            }
            catch
            {

            }
        }

        private void txtMontoGeneral_KeyPress(object sender, KeyPressEventArgs e)
        {
            Services.Utilidades.UnPunto(e, this.txtMontoGeneral.Text.Trim(), ref bandera);
            if (e.KeyChar == 13 && this.txtMontoGeneral.Text.Trim().Length > 0)
            {
                if (teclaPresionadaXCajero == "F1" || teclaPresionadaXCajero == "F11_ED")
                {
                    //llamar el metodo asignar pago
                    AsginarMetodoPago(codigoTipoPago, tipoPago, Convert.ToDecimal(this.txtMontoGeneral.Text), moneda, true, teclaPresionadaXCajero, null);
                    setCambiarEstadoTextBoxMetodoPago(teclaPresionadaXCajero, false);                    
                    teclaPresionadaXCajero = "";
                }
            }
        }

        private void txtMontoGeneral_Leave(object sender, EventArgs e)
        {
            bandera = true;
        }

        private void txtDocumento_KeyPress(object sender, KeyPressEventArgs e)
        {

            if (e.KeyChar == 13)
            {
                if (teclaPresionadaXCajero == "F2" || teclaPresionadaXCajero == "F11_CHD" || teclaPresionadaXCajero == "F3" || teclaPresionadaXCajero == "F11_ED" ||
                    teclaPresionadaXCajero == "F11_CHD" || teclaPresionadaXCajero == "F11_TD" || teclaPresionadaXCajero == "F4" || teclaPresionadaXCajero == "F5" ||
                    teclaPresionadaXCajero == "F6" || teclaPresionadaXCajero == "F11_GCD")
                {

                    //llamar el metodo asignar pago
                    AsginarMetodoPago(codigoTipoPago, tipoPago, Convert.ToDecimal(this.txtMontoGeneral.Text), moneda, true, teclaPresionadaXCajero, null);
                    setCambiarEstadoTextBoxMetodoPago(teclaPresionadaXCajero, false);                    
                    teclaPresionadaXCajero = "";
                }
            }
        }
    }
}





       




