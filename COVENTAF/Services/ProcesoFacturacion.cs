using Api.Model.Modelos;
using Api.Model.ViewModels;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace COVENTAF.Services
{
    public class ProcesoFacturacion
    {
      

        //desactivar el intercambio de descuento de linea del producto
        private void desactivarIntercambioDescuentoLinea(varFacturacion listVarFactura, List<DetalleFactura> detalleFact)
        {
            //verifico que el estado de la variable descuentoActivo este desactivada
            if (listVarFactura.DescuentoActivo)
            {
                //desactivar
                listVarFactura.DescuentoActivo = false;
                //intercarmbiar los descuentos si existiera
                setAcitvoOrDesactivoDescPorLinea(detalleFact, listVarFactura.DescuentoActivo);
            }

        }

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

        //eliminar 
        /*
        public void changeCheckDSD(varFacturacion listVarFactura, List<DetalleFactura> detalleFact, bool activoDSD)
        {
            if (activoDSD)
            {
                //actualizar el estado DescBeneficioOrDescLinea
                listVarFactura.DescBeneficioOrDescLinea = "Descuento_DSD";
                //actualiza el estado e intercambia con los descuento de los registro del detalle de factura
                activarIntercambiarDescuentoLinea(listVarFactura, detalleFact);
            }
            else
            {
                listVarFactura.DescBeneficioOrDescLinea = "Descuento_Beneficio";
                //actualiza el estado e intercambia con los descuento de los registro del detalle de factura
                desactivarIntercambioDescuentoLinea(listVarFactura, detalleFact);
            }
        }
        /*


        //inicializar las variables totales
        //void inicializarVariableTotales(varFacturacion listVarFactura)
        //{
        //    /**Totales */
        //    listVarFactura.SubTotalDolar = 0.0000; listVarFactura.subTotalCordoba = 0.0000;
        //    //descuento
        //    listVarFactura.DescuentoDolar = 0.0000; listVarFactura.descuentoCordoba = 0.0000; listVarFactura.descuentoGeneral = 0.0000;
        //    //subtotales 
        //    listVarFactura.SubTotalDescuentoDolar = 0.0000; listVarFactura.subTotalDescuentoCordoba = 0.0000;
        //    //iva
        //    listVarFactura.ivaCordoba = 0.0000; listVarFactura.ivaDolar = 0.0000;
        //    //total
        //    listVarFactura.totalDolar = 0.0000; listVarFactura.totalCordobas = 0.0000;
        //    listVarFactura.totalUnidades = 0.0000;
        //}


        //agregar un registro en el arreglo
    //    addNewRow(listDetFactura: detalleFactura[])
    //    {

    //        //obtener el numero consecutivo del
    //        const numConsecutivo= listDetFactura.length;
    //        var datosd_: detalleFactura = {
    //        consecutivo: numConsecutivo,
    //  articuloId: "",
    //  inputArticuloDesactivado: true,
    //  codigoBarra: "",
    //  descripcion: "",
    //  unidad: "unidad",
    //  cantidad: 1.00,
    //  cantidadExistencia: 0,
    //  inputCantidadDesactivado: false,
    //  precioDolar: 0.00,
    //  precioCordobas: 0.00,
    //  subTotalDolar: 0.00,
    //  subTotalCordobas: 0.00,
    //  porCentajeDescuento: 0.00,
    //  descuentoInactivo: 0.00,
    //  descuentoDolar: 0.00,
    //  descuentoCordoba: 0.00,  
    //  descuentoTotalGeneral: 0.0000,                    
    //  totalDolar: 0.00,
    //  totalCordobas: 0.00,      
    //  inputActivoParaBusqueda: false,
    //  botonEliminarDesactivado: true,
      
    //}

            //agregar push para agregar un nuevo registro en los arreglos.
        //    listDetFactura.push(datosd_);
        //}

        //asignar datos del cliente para mostrarlo en HTML
        public void asignarDatoClienteParaVisualizarHtml(Clientes datosCliente, varFacturacion listVarFactura)
        {
            //nombre del cliente
            listVarFactura.NombreCliente = datosCliente.Nombre;
            //saldo disponible del cliente
            listVarFactura.SaldoDisponible =Convert.ToDecimal(datosCliente.U_SaldoDisponible is null ? 0.00M : datosCliente.U_SaldoDisponible);
            //porcentaje del cliente
            listVarFactura.PorCentajeDescCliente = Convert.ToDecimal(datosCliente.U_Descuento is null ? 0.00M : datosCliente.U_Descuento);
            listVarFactura.PorCentajeDescGeneral = 0.00M;
        }

        public void inicializarDatosClienteParaVisualizarHTML(varFacturacion listVarFactura)
        {
            //nombre del cliente
            listVarFactura.NombreCliente = "********";
            //saldo disponible del cliente
            listVarFactura.SaldoDisponible = 0;
          //el porcentaje del cliente, descrito 5.20 %
            listVarFactura.PorCentajeDescCliente = 0.00M;
            listVarFactura.PorCentajeDescGeneral = 0.00M;
        }

        //despues de obtener los datos del cliente del servidor el sistema inicia valores
        /*public void asignarValoresDespuesConsultarCliente(varFacturacion listVarFactura, bool descuentoSobreDescuento)
        {
            //asignar el valor de Descuento del Beneficiario
            listVarFactura.DescBeneficioOrDescLinea = this.getValorDescuentoDelBeneficiario(listVarFactura, descuentoSobreDescuento);
            //activar o desactivar los descuento por linea.
            listVarFactura.DescuentoActivo = this.DescuentoLineaActivoIsOk(listVarFactura.DescBeneficioOrDescLinea);
        }*/

        //toma de decision del sistema si obtiene descuento de Linea del producto o el descuento del Beneficio
        string getValorDescuentoDelBeneficiario(varFacturacion listVarFactura, bool descuentoSobreDescuento  )
        {
            string valor = "";

            if (descuentoSobreDescuento)
                valor = "Descuento_DSD";
            //si el saldo disponible del cliente es mayor que cero
            else if (listVarFactura.SaldoDisponible > 0)
                valor = "Descuento_Beneficio";
            else
                valor = "Descuento_Linea";

            /*
          //si es Descuento_DSD or Descuento_Beneficio y tiene saldo disponible y el metodo montoDescuentoBeneficioIsOk no es Ok entonces cambia automanticamente de descuento
          if (((valor == "Descuento_DSD") || (valor =="Descuento_Beneficio")) && (SaldoDisponible !=0) && (!this.montoDescuentoBeneficioIsOk(SaldoDisponible, descuentoFactura)))
            valor="Descuento_Linea";*/

            return valor;
        }

        /*
        //activar o desactivar el boton DSD(Descuento Sobre Descuento)
        public bool activarBotonDSD(decimal porcentajeDescTitular )
        {
            bool activar = false;
            //si el porcentaje es mayor que cero entonces
            if (porcentajeDescTitular > 0)
                //activar el boton 
                activar = true;
            else
                //desactivar el boton DSD
                activar = false;


            return activar;
        }*/


        /* 0001	EFECTIVO, 0002	CHEQUE, 0003	TARJETA, 0004 CREDITO*/
        //este metodo verifica si tiene derecho al descuento 
        public decimal obtenerDescuento(Clientes datoCliente, string formaPago)
        {
            decimal descuento = 0.000M;

            ///si eres militar entonces tiene derecho descuento independientemente si es al credito
            if (datoCliente.U_EsMilitar == "S")
            {
                descuento = Convert.ToDecimal(datoCliente.U_Descuento);

            }
            //si eres empleado y la forma de pago no es al credito (0004) entonces tienes derecho al descuento
            else if (datoCliente.U_EsEmpleado == "S" && formaPago != "0004")
            {
                descuento = Convert.ToDecimal(datoCliente.U_Descuento);
            }

            return descuento;
        }

         //verificar si el cliente paga IVA
        public decimal obtenerIVA(Clientes datosCliente)
        {
            var IVA = 0.0000M;

            if (datosCliente.Codigo_Impuesto == "IVA")
            {
                IVA = 0.15M;
            }

            return IVA;
        }


        //  
        bool aplicaMontoParaDescuento(Clientes datosCliente, decimal monto )
        {
            bool aplicarDescuento = false;

            if (monto > datosCliente.U_MontoInicial)
            {
                aplicarDescuento = true;
            }

            return aplicarDescuento;
        }

        //verificar si debes de activar la linea de descuento o desactivar la linea de descuento
        bool DescuentoLineaActivoIsOk(string descBeneficioOrDescLinea)
        {
            if (descBeneficioOrDescLinea == "Descuento_Linea" || descBeneficioOrDescLinea == "Descuento_DSD")
                return true;
            else
                return false;
        }


        //verificar si DSD(Descuento Sobre Descuento) esta activado o no
        public bool isActivoDSD(string descBeneficioOrDescLinea )
        {
            return (descBeneficioOrDescLinea == "Descuento_DSD" ? true : false);
        }

        //anilizar y cambiar el estado de los descuentos
        /*
       public void setAnalizar_CambiarEstadoDelDescuento(varFacturacion listVarFactura, List<DetalleFactura> detalleFact, Clientes datoCliente, string formaPago, bool estadoDSD)
        {
            //obtener el descuento del Beneficio
            var porcentajeDescBeneficio = obtenerDescuento(datoCliente, formaPago);

            //si existe descuento entonces el cliente tiene el beneficio
            if ((porcentajeDescBeneficio > 0) && (estadoDSD))
            {
                //actualizar
                listVarFactura.DescBeneficioOrDescLinea = "Descuento_DSD";
                //activar el estado del descuento del beneficio si lo amerita
                this.activarIntercambiarDescuentoLinea(listVarFactura, detalleFact);
            }
            else if ((porcentajeDescBeneficio > 0) && (!estadoDSD))
            {
                //actualizar
                listVarFactura.DescBeneficioOrDescLinea = "Descuento_Beneficio";
                //desactivar los intercambio de descuento si existiera
                this.desactivarIntercambioDescuentoLinea(listVarFactura, detalleFact);


            }
            else
            {
                //de lo contrario el cliente no tiene derecho al descuento del beneficio, solo al descuento del articulo(solo si existiera el descuento)
                listVarFactura.DescBeneficioOrDescLinea = "Descuento_Linea";
                //activar (mostrar) la linea del descuento del producto solo si existiera
                this.activarIntercambiarDescuentoLinea(listVarFactura, detalleFact);
            }
        }*/


        /// <summary>
        /// inicializar todas las variables
        /// </summary>
        /// <param name="listVarFactura"></param>
        public void InicializarTodaslasVariable(varFacturacion listVarFactura)
        {
            listVarFactura.TotalRetencion = 0;
            listVarFactura.inputActivo = "";
            listVarFactura.IdActivo = "";
            //indica si el descuento esta aplicado o no esta aplicado
            listVarFactura.DescuentoActivo = true;
            //descuento que el cliente
            listVarFactura.TipoDeCambio = 0.0000M;
            listVarFactura.BodegaId = "";

            /**Totales */
            listVarFactura.SubTotalDolar = 0.0000M;
            listVarFactura.SubTotalCordoba = 0.0000M;
            //descuento
            listVarFactura.DescuentoPorLineaDolar = 0.0000M;
            listVarFactura.DescuentoPorLineaCordoba = 0.0000M;
            listVarFactura.DescuentoGeneralCordoba = 0.0000M;

            //descuento General
            listVarFactura.DescuentoGeneralDolar = 0.0000M;
            listVarFactura.DescuentoGeneralCordoba = 0.0000M;

            //subtotales 
            listVarFactura.SubTotalDescuentoDolar = 0.0000M;
            listVarFactura.SubTotalDescuentoCordoba = 0.0000M;
            listVarFactura.IvaCordoba = 0.0000M;
            listVarFactura.IvaDolar = 0.0000M;
            listVarFactura.TotalDolar = 0.0000M;
            listVarFactura.TotalCordobas = 0.0000M;
            listVarFactura.TotalUnidades = 0.0000M;
            //fecha de hoy
            listVarFactura.FechaFactura = DateTime.Now;
        }

        public bool desactivarBotonVerificarDescuento(varFacturacion listVarFactura, List<DetalleFactura> detalleFactura, string forma_Pago)
        {
            //verifico que existe el cliente y si existe almenos un codigo de barra
            if ((listVarFactura.NombreCliente.Length > 0) && (detalleFactura[0].codigoBarra.Length > 0) && (forma_Pago.Length > 0))
                return true;
            else
                return false;
        }

        //validar el boton validar descuento
        public string validarAntesActivarBotonValidarDesc(varFacturacion listVarFactura, List<DetalleFactura> detalleFactura, string forma_Pago, string tipoTarjeta, string condicionPago)
        {
            //si la validacion esta correcta entonces devuelve un OK
            string mensaje = "OK";
            string drownListVisible = "";

            if (forma_Pago == "0003")
                drownListVisible = "tipo_tarjeta";

            else if (forma_Pago == "0004")
                drownListVisible = "condicion_pago";


            //comprobar si tiene el 
            if (listVarFactura.NombreCliente.Length == 0)
            {
                mensaje = "Debes de ingresar el codigo del cliente";
            }
            //verificar si almenos la primer fila del detalle de la factura tiene un producto
            else if (detalleFactura.Count == 0)
                mensaje = "Debes de ingresar al menos un articulo";

            //verificar si es tarjeta y el tipo de tarjeta es igual al vacio
            else if ((forma_Pago == "0003") && (tipoTarjeta.Length == 0) && (drownListVisible == "tipo_tarjeta"))
                mensaje = "Debes de seleccionar el tipo de tarjeta";

            //verificar si es credito y la condicion de pago es igual al vacio
            else if ((forma_Pago == "0004") && (condicionPago.Length == 0) && drownListVisible == "condicion_pago")
                mensaje = "Debes de seleccionar la condicion de pago";

            return mensaje;
        }

        //inicializar las variables totales
        public void InicializarVariableTotales(varFacturacion listVarFactura)
        {
            /**Totales */
            listVarFactura.SubTotalDolar = 0.0000M; listVarFactura.SubTotalCordoba = 0.0000M;
            //descuento
            listVarFactura.DescuentoPorLineaDolar = 0.0000M; listVarFactura.DescuentoPorLineaCordoba = 0.0000M; 
            //descuento general
            listVarFactura.DescuentoGeneralCordoba = 0.0000M; listVarFactura.DescuentoGeneralDolar = 0.0000M;
            //subtotales 
            listVarFactura.SubTotalDescuentoDolar = 0.0000M; listVarFactura.SubTotalDescuentoCordoba = 0.0000M;
            //iva
            listVarFactura.IvaCordoba = 0.0000M; listVarFactura.IvaDolar = 0.0000M;
            //total
            listVarFactura.TotalDolar = 0.0000M; listVarFactura.TotalCordobas = 0.0000M;
            listVarFactura.TotalUnidades = 0.0000M;
        }

        //obtener el nombre del forma de pago
        /*string getNombreFormaPago(string codigoFormaPago, List<FORMA_PAGOS> listFormaPago, string tipo_Tarjeta, 
            string condicion_Pago, List<CONDICION_PAGO> listaCondicionPago)
        {
            var description = "";
            var desciptCondicionPago = "";

            description = listFormaPago.Where(lf => lf.Forma_Pago == codigoFormaPago).Select(x => x.Descripcion).FirstOrDefault();

            foreach (var datFormaPag in listFormaPago)
            {
                //comprobar si existe el codigo de forma de pago
                if (datFormaPag.Forma_Pago == codigoFormaPago)
                {
                    description = datFormaPag.Descripcion;
                }
            }

            foreach (var datCondicionPago in listaCondicionPago)
            {
                //comprobar si existe el codigo de forma de pago
                if (datCondicionPago.Condicion_Pago == condicion_Pago)
                {
                    desciptCondicionPago = datCondicionPago.Descripcion;
                }
            }

            //verificar si es tarjeta
            if (description == "TARJETA")
                //agregar el tipo de tarjeta
                description = description + " " + tipo_Tarjeta;

            //verificar si es credito
            else if (description == "CREDITO")
                //agregar la condicion de pago
                description = description + " " + desciptCondicionPago;

            return description;
        }*/

        //this.cboFormaPago.Text, this.cboTipoTarjeta.Text, this.cboCondicionPago.Text
        /*  public string getNombreFormaPago(string forma_Pago,  string tipo_Tarjeta, string condicion_Pago)
          {
              var description = forma_Pago;

              //verificar si es tarjeta
              if (forma_Pago == "TARJETA")
              {
                  //agregar el tipo de tarjeta
                  description = description + " " + tipo_Tarjeta;
              }
              //verificar si es credito
              else if (description == "CREDITO")
              {
                  //agregar la condicion de pago
                  description = description + " " + condicion_Pago;
              }
              else
              {
                  description = forma_Pago;
              }

              return description;
          }*/

        public void configurarDataGridView(DataGridView dgvDetalleFactura)
        {
            //dgvDetalleFactura.Columns["consecutivo"].Visible = false;
            //dgvDetalleFactura.Columns["inputArticuloDesactivado"].Visible = false;
            //dgvDetalleFactura.Columns["moneda"].Visible = false;
            //dgvDetalleFactura.Columns["inputCantidadDesactivado"].Visible = false;
            //dgvDetalleFactura.Columns["inputCantidadDesactivado"].Visible = false;
            //dgvDetalleFactura.Columns["descuentoInactivo"].Visible = false;
            //dgvDetalleFactura.Columns["descuentoGeneralCordoba"].Visible = false;
            //dgvDetalleFactura.Columns["descuentoGeneralDolar"].Visible = false;
            //dgvDetalleFactura.Columns["inputActivoParaBusqueda"].Visible = false;
            //dgvDetalleFactura.Columns["botonEliminarDesactivado"].Visible = false;
            //dgvDetalleFactura.Columns["BodegaID"].Visible = false;
            //dgvDetalleFactura.Columns["NombreBodega"].Visible = false;

            dgvDetalleFactura.Columns["totalDolar"].HeaderText = "Total U$";
            dgvDetalleFactura.Columns["descuentoPorLineaDolar"].HeaderText = "Descuento U$";
            dgvDetalleFactura.Columns["precioDolar"].HeaderText = "Precio U$";
            dgvDetalleFactura.Columns["codigoBarra"].HeaderText = "Codigo Barra";
            dgvDetalleFactura.Columns["descripcion"].HeaderText = "Descripcion";
            dgvDetalleFactura.Columns["cantidad"].HeaderText = "Cantidad";
            dgvDetalleFactura.Columns["cantidadExistencia"].HeaderText = "Existencia";
            dgvDetalleFactura.Columns["precioCordobas"].HeaderText = "Precio C$";
            dgvDetalleFactura.Columns["descuentoPorLineaCordoba"].HeaderText = "Descuento C$";
            dgvDetalleFactura.Columns["subTotalCordobas"].HeaderText = "Sub Total C$";
            dgvDetalleFactura.Columns["porCentajeDescuentoXArticulo"].HeaderText = "Descuento %";
            dgvDetalleFactura.Columns["totalCordobas"].HeaderText = "Total C$";

        }


        public string ObtenerNuevoPorCentaje(string cadena, ref bool existeCaractePorcentaje)
        {
            string caracter = "";
            string nuevaCadena = "";
            for (int n = 0; n < cadena.Length; n++)
            {
                caracter = cadena.Substring(n, 1);
                if (caracter == "%")
                {
                    existeCaractePorcentaje = true;
                    break;
                }
                else
                {
                    nuevaCadena += caracter;
                }
            }


            return nuevaCadena;
        }



        private PrintDocument doc = new PrintDocument();
        private PrintPreviewDialog vista = new PrintPreviewDialog();
        // Construct the PrintPreviewControl.
        PrintPreviewControl PrintPreviewControl1 = new PrintPreviewControl();

        private List<DetalleFactura> _listDetFactura;
        private  Encabezado _encabezadoFact;
       

        public void ImprimirTicketFactura(List<DetalleFactura> listDetFactura, Encabezado encabezadoFact)
        {
            this._listDetFactura = new List<DetalleFactura>();
            _listDetFactura = listDetFactura;

            this._encabezadoFact = new Encabezado();
            this._encabezadoFact = encabezadoFact;
            
            
            doc.PrinterSettings.PrinterName = doc.DefaultPageSettings.PrinterSettings.PrinterName;
          
            doc.PrintPage += new PrintPageEventHandler(ImprimirTicket);
            // Set the zoom to 25 percent.
            //this.PrintPreviewControl1.Zoom = 0.25;            
            //vista.Controls.Add(this.PrintPreviewControl1);
            
           vista.Document = doc;
           //doc.Print();
            vista.ShowDialog();
        }



        public void ImprimirTicket(object sender, PrintPageEventArgs e)
        {
            int posX, posY;
            Font fuente = new Font("consola", 8, FontStyle.Bold);
            Font fuenteRegular = new Font("consola", 8, FontStyle.Regular);
            Font fuenteRegular_7 = new Font("consola", 7, FontStyle.Regular);
            
            var direccion = "Km 6 Carretera Norte, 700 Mts al norte";
            var direccion2 = "Puente a Desnivel";
            var telefono = "Tel:(505)22493187 Fax: 22493184";


            try
            {
                posX = 2;
                posY = 0;
                posY += 20;
                e.Graphics.DrawString("EJERCITO DE NICARAGUA " , fuente, Brushes.Black, posX+53, posY);
                posY += 15;
                e.Graphics.DrawString(User.NombreTienda, fuente, Brushes.Black, posX+45, posY);
                posY += 15;
                e.Graphics.DrawString(direccion, fuente, Brushes.Black, posX+ 35, posY);
                posY += 15;
                e.Graphics.DrawString(direccion2, fuente, Brushes.Black, posX + 80, posY);
                posY += 15;
                e.Graphics.DrawString("Managua, Nicaragua", fuente, Brushes.Black, posX + 80, posY);
                posY += 15;
                e.Graphics.DrawString(telefono, fuente, Brushes.Black, posX + 40, posY);
                posY += 15;
                e.Graphics.DrawString("N° RUC: J1330000001272", fuente, Brushes.Black, posX + 55, posY);
               
                //factura
                posY += 24;
                e.Graphics.DrawString("N° Factura: " + _encabezadoFact.noFactura , fuenteRegular, Brushes.Black, posX, posY);
                posY += 15;
                e.Graphics.DrawString("Codigo Cliente: " + _encabezadoFact.codigoCliente, fuenteRegular, Brushes.Black, posX, posY);
                posY += 15;
                e.Graphics.DrawString("Cliente: " + _encabezadoFact.cliente, fuenteRegular, Brushes.Black, posX, posY);
                posY += 15;
                e.Graphics.DrawString("Fecha: " + _encabezadoFact.fecha, fuenteRegular, Brushes.Black, posX, posY);
                posY += 15;
                e.Graphics.DrawString("Bodega: " + _encabezadoFact.bodega, fuenteRegular, Brushes.Black, posX, posY);
                posY += 15;
                e.Graphics.DrawString("Caja: " + _encabezadoFact.caja, fuenteRegular, Brushes.Black, posX, posY);
                posY += 15;
                e.Graphics.DrawString("Tipo Cambio: " + _encabezadoFact.tipoCambio.ToString("N4"), fuenteRegular, Brushes.Black, posX, posY);
                posY += 18;
                e.Graphics.DrawString("-------------------------------------------------------------------------" , fuente, Brushes.Black, posX, posY);
                posY += 10;
                e.Graphics.DrawString("Codigo", fuente, Brushes.Black, posX, posY);
                //posY += 15;
                posX += 60;
                e.Graphics.DrawString("Cant", fuente, Brushes.Black, posX, posY);
                //posY += 15;
                posX += 50;
                e.Graphics.DrawString("Precio", fuente, Brushes.Black, posX, posY);
                // posY += 15;
                posX += 50;
                e.Graphics.DrawString("Desc", fuente, Brushes.Black, posX, posY);
                //posY += 15;
                posX += 40;
                e.Graphics.DrawString("Monto", fuente, Brushes.Black, posX, posY);
                posY += 10;
                //reiniciar la posicionX
                posX = 2;
                e.Graphics.DrawString("-------------------------------------------------------------------------", fuente, Brushes.Black, posX, posY);

               
               
                //e.Graphics.DrawString("8721160000939", fuenteRegular, Brushes.Black, posX, posY);
                //_listDetFactura

                foreach(var detalleFactura in _listDetFactura)
                {
                    posY += 10;
                    e.Graphics.DrawString(detalleFactura.articuloId, fuenteRegular, Brushes.Black, posX, posY);
                    
                    posX += 60;
                    e.Graphics.DrawString(detalleFactura.cantidad.ToString("N2"), fuenteRegular, Brushes.Black, posX, posY);
                    
                    posX += 45;
                    e.Graphics.DrawString( detalleFactura.precioCordobas.ToString("N2"), fuenteRegular, Brushes.Black, posX, posY);
                   
                    posX += 60;
                    e.Graphics.DrawString(Convert.ToDecimal(detalleFactura.descuentoPorLineaCordoba).ToString("N2") , fuenteRegular, Brushes.Black, posX, posY);
                   
                    posX += 50;
                    e.Graphics.DrawString(detalleFactura.totalCordobas.ToString("N2"), fuenteRegular, Brushes.Black, posX, posY);
                    
                    //salto a la siguiente linea
                    posY += 15;
                    posX = 2;
                    e.Graphics.DrawString(detalleFactura.descripcion, fuenteRegular_7, Brushes.Black, posX, posY);

                    posY += 7;

                }

                posY += 5;
                e.Graphics.DrawString("-------------------------------------------------------------------------", fuente, Brushes.Black, posX, posY);

                posY += 15;
                posX = 2;
                posX += 140;
                e.Graphics.DrawString("Sub Total:", fuente, Brushes.Black, posX, posY);
                               
               
                posX += 65;
                e.Graphics.DrawString("C$ " + _encabezadoFact.subTotalCordoba.ToString("N2"), fuenteRegular, Brushes.Black, posX, posY);

                

                posY += 15;
                posX = 2;
                posX += 140;
                e.Graphics.DrawString("Descuento:", fuente, Brushes.Black, posX, posY);

                posX += 65;
                e.Graphics.DrawString("C$ " + _encabezadoFact.descuentoCordoba.ToString("N2"), fuenteRegular, Brushes.Black, posX, posY);

                /************************* RETENCIONES ************************************************************************/
               if (_encabezadoFact.MontoRetencion >0)
                {
                    posY += 15;
                    posX = 2;
                    posX += 140;
                    e.Graphics.DrawString("Retencion:", fuente, Brushes.Black, posX, posY);

                    posX += 65;
                    e.Graphics.DrawString("C$ " + _encabezadoFact.MontoRetencion.ToString("N2"), fuenteRegular, Brushes.Black, posX, posY);                    
                }
                /****************************************************************************************************************/


                posY += 15;
                posX = 2;
                posX += 140;
                e.Graphics.DrawString("IVA:", fuente, Brushes.Black, posX, posY);

                posX += 65;
                e.Graphics.DrawString("C$ " + _encabezadoFact.ivaCordoba.ToString("N2"), fuenteRegular, Brushes.Black, posX, posY);

                posY += 15;
                posX = 2;
                posX += 140;
                e.Graphics.DrawString("Total:", fuente, Brushes.Black, posX, posY);

                posX += 65;
                e.Graphics.DrawString("C$ " + _encabezadoFact.totalCordoba.ToString("N2"), fuenteRegular, Brushes.Black, posX, posY);


                posY += 15;
                posX = 2;
                posX += 140;
                e.Graphics.DrawString("Total:", fuente, Brushes.Black, posX, posY);

                posX += 65;
                e.Graphics.DrawString("U$ " + _encabezadoFact.totalDolar.ToString("N2"), fuenteRegular, Brushes.Black, posX, posY);

                /************************************************************************************/

                //convertir el registro en arreglo
                string[] newformaDePago = _encabezadoFact.formaDePago.Split("\r\n");
                
                //reiniciar en la posicion X
                posX = 2;               
                //comprobar si tiene mas de 2 registro el arreglo                               
                if (newformaDePago.Length >= 2)
                {
                    posY += 20;
                    e.Graphics.DrawString($"Forma de Pago: {newformaDePago[0]}", fuenteRegular, Brushes.Black, posX, posY);

                    for (var rows = 1; rows < newformaDePago.Length; rows ++)
                    {
                        posY += 20;
                        e.Graphics.DrawString(newformaDePago[rows], fuenteRegular, Brushes.Black, posX, posY);
                    }
                }
                else
                {
                    posY += 20;
                    e.Graphics.DrawString("Forma de Pago: " + _encabezadoFact.formaDePago, fuenteRegular, Brushes.Black, posX, posY);
                }

                posY += 18;
                string[] newObservacion = _encabezadoFact.observaciones.Split("\r\n");

                e.Graphics.DrawString("Observaciones: ", fuenteRegular, Brushes.Black, posX, posY);

                if (newObservacion.Length >= 2)
                {
                    for (var fila = 0; fila < newObservacion.Length; fila++)
                    {
                        posY += 15;
                        e.Graphics.DrawString(newObservacion[fila], fuenteRegular, Brushes.Black, posX + 10, posY);
                    }
                }
                else
                {
                    posY += 15;
                    e.Graphics.DrawString(_encabezadoFact.observaciones, fuenteRegular, Brushes.Black, posX, posY);
                }


                posY += 20;
                e.Graphics.DrawString($"Atendido Por: {_encabezadoFact.atentidoPor} ", fuenteRegular, Brushes.Black, posX, posY);

                posY += 50;
                e.Graphics.DrawString("ENTREGADO: ", fuenteRegular, Brushes.Black, posX, posY);

                posY += 50;
                e.Graphics.DrawString("RECIBIDO: ", fuenteRegular, Brushes.Black, posX, posY);

                posY += 50;
                posX = 30;
                e.Graphics.DrawString("NO SE ACEPTAN CAMBIOS DESPUES DE", fuenteRegular, Brushes.Black, posX, posY);
                posY += 15;
                e.Graphics.DrawString("48 HORAS. *APLICAN RESTRICCIONES*", fuenteRegular, Brushes.Black, posX, posY);

                posY += 40;
                posX += 23;
                e.Graphics.DrawString("GRACIAS POR SU COMPRA", fuenteRegular, Brushes.Black, posX, posY);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }


        private List<string> ObtenerDatosRegistro(string dato, int longitudMax)
        {
            string nuevoDato = "";
            List<string> nuevaLista = new List<string>();
            for(int row=0; row < dato.Length; row ++)
            {
                if (nuevoDato.Length == longitudMax)
                {
                    nuevaLista.Add(nuevoDato);
                }
                else
                {
                    nuevoDato = nuevoDato + dato[row];
                }
            }

            return nuevaLista;        
        }
               

        public void ImprimirTicketFacturaDemo(List<DetalleFactura> listDetFactura, Encabezado encabezadoFact)
        {
            this._listDetFactura = new List<DetalleFactura>();
            _listDetFactura = listDetFactura;

            this._encabezadoFact = new Encabezado();
            this._encabezadoFact = encabezadoFact;


            doc.PrinterSettings.PrinterName = doc.DefaultPageSettings.PrinterSettings.PrinterName;

            doc.PrintPage += new PrintPageEventHandler(ImprimirTicket);
            // Set the zoom to 25 percent.
            //this.PrintPreviewControl1.Zoom = 0.25;            
            //vista.Controls.Add(this.PrintPreviewControl1);

            vista.Document = doc;
            //doc.Print();
            vista.ShowDialog();
        }

        public void ImprimirTicketDemo(object sender, PrintPageEventArgs e)
        {
            int posX, posY;
            Font fuente = new Font("consola", 8, FontStyle.Bold);
            Font fuenteRegular = new Font("consola", 8, FontStyle.Regular);
            Font fuenteRegular_7 = new Font("consola", 7, FontStyle.Regular);

            var direccion = "Km 6 Carretera Norte, 700 Mts al norte";
            var direccion2 = "Puente a Desnivel";
            var telefono = "Tel:(505)22493187 Fax: 22493184";


            try
            {
                posX = 2;
                posY = 0;
                posY += 20;
                e.Graphics.DrawString("EJERCITO DE NICARAGUA ", fuente, Brushes.Black, posX + 53, posY);
                posY += 15;
                e.Graphics.DrawString(User.NombreTienda, fuente, Brushes.Black, posX + 45, posY);
                posY += 15;
                e.Graphics.DrawString(direccion, fuente, Brushes.Black, posX + 35, posY);
                posY += 15;
                e.Graphics.DrawString(direccion2, fuente, Brushes.Black, posX + 80, posY);
                posY += 15;
                e.Graphics.DrawString("Managua, Nicaragua", fuente, Brushes.Black, posX + 80, posY);
                posY += 15;
                e.Graphics.DrawString(telefono, fuente, Brushes.Black, posX + 40, posY);
                posY += 15;
                e.Graphics.DrawString("N° RUC: J1330000001272", fuente, Brushes.Black, posX + 55, posY);

                //factura
                posY += 24;
                e.Graphics.DrawString("N° Factura: " + _encabezadoFact.noFactura, fuenteRegular, Brushes.Black, posX, posY);
                posY += 15;
                e.Graphics.DrawString("Codigo Cliente: " + _encabezadoFact.codigoCliente, fuenteRegular, Brushes.Black, posX, posY);
                posY += 15;
                e.Graphics.DrawString("Cliente: " + _encabezadoFact.cliente, fuenteRegular, Brushes.Black, posX, posY);
                posY += 15;
                e.Graphics.DrawString("Fecha: " + _encabezadoFact.fecha, fuenteRegular, Brushes.Black, posX, posY);
                posY += 15;
                e.Graphics.DrawString("Bodega: " + _encabezadoFact.bodega, fuenteRegular, Brushes.Black, posX, posY);
                posY += 15;
                e.Graphics.DrawString("Caja: " + _encabezadoFact.caja, fuenteRegular, Brushes.Black, posX, posY);
                posY += 15;
                e.Graphics.DrawString("Tipo Cambio: " + _encabezadoFact.tipoCambio.ToString("N4"), fuenteRegular, Brushes.Black, posX, posY);
                posY += 18;
                e.Graphics.DrawString("-------------------------------------------------------------------------", fuente, Brushes.Black, posX, posY);
                posY += 10;
                e.Graphics.DrawString("Codigo", fuente, Brushes.Black, posX, posY);
                //posY += 15;
                posX += 60;
                e.Graphics.DrawString("Cant", fuente, Brushes.Black, posX, posY);
                //posY += 15;
                posX += 50;
                e.Graphics.DrawString("Precio", fuente, Brushes.Black, posX, posY);
                // posY += 15;
                posX += 50;
                e.Graphics.DrawString("Desc", fuente, Brushes.Black, posX, posY);
                //posY += 15;
                posX += 40;
                e.Graphics.DrawString("Monto", fuente, Brushes.Black, posX, posY);
                posY += 10;
                //reiniciar la posicionX
                posX = 2;
                e.Graphics.DrawString("-------------------------------------------------------------------------", fuente, Brushes.Black, posX, posY);



                //e.Graphics.DrawString("8721160000939", fuenteRegular, Brushes.Black, posX, posY);
                //_listDetFactura

                foreach (var detalleFactura in _listDetFactura)
                {
                    posY += 10;
                    e.Graphics.DrawString(detalleFactura.articuloId, fuenteRegular, Brushes.Black, posX, posY);

                    posX += 60;
                    e.Graphics.DrawString(detalleFactura.cantidad.ToString("N2"), fuenteRegular, Brushes.Black, posX, posY);

                    posX += 45;
                    e.Graphics.DrawString(detalleFactura.precioCordobas.ToString("N2"), fuenteRegular, Brushes.Black, posX, posY);

                    posX += 60;
                    e.Graphics.DrawString(Convert.ToDecimal(detalleFactura.descuentoPorLineaCordoba).ToString("N2"), fuenteRegular, Brushes.Black, posX, posY);

                    posX += 50;
                    e.Graphics.DrawString(detalleFactura.totalCordobas.ToString("N2"), fuenteRegular, Brushes.Black, posX, posY);

                    //salto a la siguiente linea
                    posY += 15;
                    posX = 2;
                    e.Graphics.DrawString(detalleFactura.descripcion, fuenteRegular_7, Brushes.Black, posX, posY);

                    posY += 7;

                }

                posY += 5;
                e.Graphics.DrawString("-------------------------------------------------------------------------", fuente, Brushes.Black, posX, posY);

                posY += 15;
                posX = 2;
                posX += 140;
                e.Graphics.DrawString("Sub Total:", fuente, Brushes.Black, posX, posY);


                posX += 65;
                e.Graphics.DrawString("C$ " + _encabezadoFact.subTotalCordoba.ToString("N2"), fuenteRegular, Brushes.Black, posX, posY);

                posY += 15;
                posX = 2;
                posX += 140;
                e.Graphics.DrawString("Descuento:", fuente, Brushes.Black, posX, posY);

                posX += 65;
                e.Graphics.DrawString("C$ " + _encabezadoFact.descuentoCordoba.ToString("N2"), fuenteRegular, Brushes.Black, posX, posY);

                posY += 15;
                posX = 2;
                posX += 140;
                e.Graphics.DrawString("IVA:", fuente, Brushes.Black, posX, posY);

                posX += 65;
                e.Graphics.DrawString("C$ " + _encabezadoFact.ivaCordoba.ToString("N2"), fuenteRegular, Brushes.Black, posX, posY);

                posY += 15;
                posX = 2;
                posX += 140;
                e.Graphics.DrawString("Total:", fuente, Brushes.Black, posX, posY);

                posX += 65;
                e.Graphics.DrawString("C$ " + _encabezadoFact.totalCordoba.ToString("N2"), fuenteRegular, Brushes.Black, posX, posY);


                posY += 15;
                posX = 2;
                posX += 140;
                e.Graphics.DrawString("Total:", fuente, Brushes.Black, posX, posY);

                posX += 65;
                e.Graphics.DrawString("U$ " + _encabezadoFact.totalDolar.ToString("N2"), fuenteRegular, Brushes.Black, posX, posY);

                /************************************************************************************/

                posY += 20;
                //reiniciar en la posicion X
                posX = 2;
                e.Graphics.DrawString("Forma de Pago: " + _encabezadoFact.formaDePago, fuenteRegular, Brushes.Black, posX, posY);

                posY += 18;
                string[] newObservacion = _encabezadoFact.observaciones.Split("\r\n");

                e.Graphics.DrawString("Observaciones: ", fuenteRegular, Brushes.Black, posX, posY);

                if (newObservacion.Length >= 2)
                {
                    for (var fila = 0; fila < newObservacion.Length; fila++)
                    {
                        posY += 15;
                        e.Graphics.DrawString(newObservacion[fila], fuenteRegular, Brushes.Black, posX + 10, posY);
                    }
                }
                else
                {
                    posY += 15;
                    e.Graphics.DrawString(_encabezadoFact.observaciones, fuenteRegular, Brushes.Black, posX, posY);
                }


                posY += 20;
                e.Graphics.DrawString("Atendido Por: ", fuenteRegular, Brushes.Black, posX, posY);

                posY += 50;
                e.Graphics.DrawString("ENTREGADO: ", fuenteRegular, Brushes.Black, posX, posY);

                posY += 50;
                e.Graphics.DrawString("RECIBIDO: ", fuenteRegular, Brushes.Black, posX, posY);

                posY += 50;
                posX = 30;
                e.Graphics.DrawString("NO SE ACEPTAN CAMBIOS DESPUES DE", fuenteRegular, Brushes.Black, posX, posY);
                posY += 15;
                e.Graphics.DrawString("48 HORAS. *APLICAN RESTRICCIONES*", fuenteRegular, Brushes.Black, posX, posY);

                posY += 40;
                posX += 23;
                e.Graphics.DrawString("GRACIAS POR SU COMPRA", fuenteRegular, Brushes.Black, posX, posY);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }


    }
}
