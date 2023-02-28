using Api.Model.Modelos;
using Api.Model.ViewModels;
using Api.Service.DataService;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Forms;
using TextBox = System.Windows.Controls.TextBox;

namespace COVENTAF.PuntoVenta
{
    public partial class frmCierreCaja : Form
    {
        private bool existeEfectivoDolar = false;
        private bool existeEfectivoCordoba = false;

        public bool exitoCierreCaja = false;
        List<Denominacion> denominacion = new List<Denominacion>();
        ServiceCaja_Pos _serviceCajaPos;
        List<ViewModelCierreCaja> _datosCierreCaja;
        VariableCierreCaja _listVarCierreCaja = new VariableCierreCaja();
        private string idActual = "";
        private decimal cantidadGrid;
        private decimal totatCajeroCordobas = 0.00M;
        private decimal totalCajeroDolares = 0.00M;

        public frmCierreCaja()
        {
            InitializeComponent();
            _serviceCajaPos = new ServiceCaja_Pos();
        }

        private void frmCierreCaja_Load(object sender, EventArgs e)
        {
            try
            {
                //
                PrepararCajaParaCierre(User.Caja, User.Usuario, User.ConsecCierreCT);
            }
            catch (Exception ex)
            {

            }

            this.lblTitulo.Text = $"Cierre de Caja: {User.Caja}";
            this.lblTituloCaja.Text = $"Cierre de Caja {User.Caja}. con el Numero de Cierre: {User.ConsecCierreCT}";
        }



        public async void PrepararCajaParaCierre(string caja, string cajero, string numeroCierre)
        {
            ResponseModel responseModel = new ResponseModel();
            _datosCierreCaja = new List<ViewModelCierreCaja>();

            try
            {
                _datosCierreCaja = await _serviceCajaPos.ObtenerDatosParaCierreCaja(caja, cajero, numeroCierre, responseModel);
                if (responseModel.Exito == 1)
                {
                    //llenar el grid reportado por el sistema
                    LlenarGridReportadoXSistema(_datosCierreCaja);
                    LlenarGridReportadoCajero(_datosCierreCaja);
                    CalcularTotalReportadoCajero();
                    ListarDenomincaciones();
                }
                else
                {
                    MessageBox.Show(responseModel.Mensaje, "Sistema COVENTAF");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void LlenarGridReportadoXSistema(List<ViewModelCierreCaja> _datosCierreCaja)
        {
            decimal totalCordobas = 0.00M;
            decimal totalDolares = 0.00M;
            foreach (var itemSistema in _datosCierreCaja)
            {
               
                this.dgvGridReportadoPorSistema.Rows.Add(itemSistema.Id,itemSistema.Descripcion, 
                    (itemSistema.Moneda == "L" ? $"C$ {itemSistema.Monto.ToString("N2")}" : $"U$ {itemSistema.Monto.ToString("N2")}"), 
                    itemSistema.Moneda);
                //comprobar si la moneda es Local =L (C$)
                if (itemSistema.Moneda == "L")
                {
                    totalCordobas += itemSistema.Monto;
                }
                else if (itemSistema.Moneda == "D")
                {
                    totalDolares += itemSistema.Monto;
                }

                //comprobar si existe forma de pago 0001=Efectivo cordoba
                if (itemSistema.Forma_Pago == "0001" && itemSistema.Moneda == "L")
                {
                    existeEfectivoCordoba = true;
                }
                //comprobar si existe forma de pago 0001=Efectivo dolar
                if (itemSistema.Forma_Pago == "0001" && itemSistema.Moneda == "D")
                {
                    existeEfectivoDolar = true;
                }

            }
           
            this.txtTotalCordobasSistema.Text = totalCordobas.ToString("N2");
            this.txtTotalDolaresSistema.Text = totalDolares.ToString("N2");

        }

        private void LlenarGridReportadoCajero(List<ViewModelCierreCaja> _datosCierreCaja)
        {
            foreach (var itemSistema in _datosCierreCaja)
            {
                this.dgvGridRportadoXCajero.Rows.Add($"{itemSistema.Forma_Pago}{itemSistema.Moneda}" ,itemSistema.Descripcion, (itemSistema.Moneda == "L" ? "C$ 0.00" : "U$ 0.00"), itemSistema.Moneda);
            }
        }

        void CalcularTotalReportadoCajero()
        {
            totatCajeroCordobas = 0.00M;
            totalCajeroDolares = 0.00M;

            for (var rows = 0; rows < dgvGridRportadoXCajero.RowCount; rows++)
            {
                //comprobar si la moneda es Local =L (C$)
                if (dgvGridRportadoXCajero.Rows[rows].Cells["Monedac"].Value.ToString() == "L")
                {

                    totatCajeroCordobas += Convert.ToDecimal(dgvGridRportadoXCajero.Rows[rows].Cells["Montoc"].Value.ToString().Replace("C$", ""));
                }
                else if (dgvGridRportadoXCajero.Rows[rows].Cells["Monedac"].Value.ToString() == "D")
                {
                    totalCajeroDolares += Convert.ToDecimal(dgvGridRportadoXCajero.Rows[rows].Cells["Montoc"].Value.ToString().Replace("U$", ""));
                }
            }

            this.txtTotalCordobasCajero.Text = totatCajeroCordobas.ToString("N2");
            this.txtTotalDolaresCajero.Text = totalCajeroDolares.ToString("N2");
        }

        private async void ListarDenomincaciones()
        {
            ResponseModel responseModel = new ResponseModel();

            try
            {
                denominacion = await _serviceCajaPos.ObtenerListaDenominacion(responseModel);
                if (responseModel.Exito ==1)
                {
                    LlenarGridDenominaciones();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        private void btnCierre_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        public void  LlenarGridDenominaciones()
        {
            try
            {
                //comprobar si existe el efectivo en cordobas
                if (existeEfectivoCordoba)
                {
                    foreach (var itemDenominacion in denominacion)
                    {
                        this.dgvReportePagoCajero.Rows.Add("0001L", itemDenominacion.Tipo, $"C${itemDenominacion.Denom_Monto.ToString("N2")}", 0, "L");
                    }
                }

                //comprobar si existe el efectivo en cordobas
                if (existeEfectivoDolar)
                {
                    foreach (var itemDenominacion in denominacion)
                    {
                        this.dgvReportePagoCajero.Rows.Add("0001D", itemDenominacion.Tipo, $"U${itemDenominacion.Denom_Monto.ToString("N2")}", 0, "D");
                    }
                }

                foreach(var itemDenomincacion in _datosCierreCaja)
                {
                    if (itemDenomincacion.Forma_Pago != "0001")
                    {
                        //switch (itemDenomincacion.Monto)
                        //{
                        //    case "0.01000000":
                        //}

                        this.dgvReportePagoCajero.Rows.Add($"{itemDenomincacion.Forma_Pago}{itemDenomincacion.Moneda}", "ND", itemDenomincacion.Descripcion, 0, itemDenomincacion.Moneda);
                    }
                }
                
            }
            catch (Exception ex)
            {

            }
        }
           

        private void dgvReportePagoCajero_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            //si la columna es cantidad (4) o descuento(5)
            if (e.ColumnIndex == 3)
            {
                int filaGrid = e.RowIndex;

                //btnCobrar.Enabled = false;
                //asignar el consucutivo para indicar en que posicion estas
                //consecutivoActualFactura = e.RowIndex;
                //validar la infor
                ValidarCantidaddelGridDenominacion(filaGrid);

                //calcular el grid de denominacion y enviar idActua y la fila
                CalcularGridDenominacion(idActual, filaGrid);
                //calcular totales
                CalcularTotalReportadoCajero();
            }
        }



        private void ValidarCantidaddelGridDenominacion(int rows)
        {
            //obtener el valor del grid
            var cantidad = this.dgvReportePagoCajero.Rows[rows].Cells["Cantidadd"].Value;
            //validar si el valor es null o tiene datos
            string valorCantidad = cantidad is null ? "" : cantidad.ToString().Trim();
            //probar si la fila corresponde a los codigo 0001L or 0001D
            bool permitirPuntDecimal = (this.dgvReportePagoCajero.Rows[rows].Cells["Idd"].Value.ToString() == "0001L" || this.dgvReportePagoCajero.Rows[rows].Cells["Idd"].Value.ToString() == "0001D") ? false : true;

            //  bool isNumeric = double.TryParse(cantidad);

            if (valorCantidad.Length == 0)
            {
                MessageBox.Show("Debes Digitar un numero en la columna cantidad", "Sistema COVENTAF");
                this.dgvReportePagoCajero.Rows[rows].Cells["Cantidadd"].Value = cantidadGrid;
                cantidad = 0;
            }
            else if (CountPuntoDecimal(valorCantidad)>=2)
            {
                MessageBox.Show("El numero que digitaste no es un numero correcto", "COVENTAF");
                this.dgvReportePagoCajero.Rows[rows].Cells["Cantidadd"].Value = cantidadGrid;
                cantidadGrid = 0;
            }
            else if (!IsDigit(valorCantidad, permitirPuntDecimal))
            {
                MessageBox.Show("La Columna Cantidad solo permite numeros enteros positivos", "COVENTAF");
                this.dgvReportePagoCajero.Rows[rows].Cells["Cantidadd"].Value = cantidadGrid;
                cantidadGrid = 0;
            }
            
        }


        private int CountPuntoDecimal(string cantidad)
        {
            byte contadorDecimales = 0;
            for (var rows = 0; rows < cantidad.Length; rows++)
            {
                //comprobar si es un punto decimal 
                if (cantidad[rows] == '.')
                {
                    //contar los puntos decimal
                    contadorDecimales +=1;
                }               
            }

            return contadorDecimales;
        }

              
        private bool IsDigit(string cantidad, bool puntoDecimalPermitid)
        {
            bool tieneDigitado = true;
            for(var rows=0; rows < cantidad.Length; rows ++)
            {
                //comprobar si es un punto decimal y ademas si se permite para esta consulta el punto decimal
                if (cantidad[rows] =='.' && puntoDecimalPermitid)
                {
                    continue;
                }
                else if (!char.IsDigit(cantidad[rows]))
                {
                    tieneDigitado = false;
                    break;
                }                
            }

            return tieneDigitado;
        }


        private void CalcularGridDenominacion(string Id, int filaGrid)
        {
            decimal sumaDenominacion = 0;
            string simboloBuscar = "";
            

            if (Id== "0001L" || Id == "0001D")
            {
                //simbolo a buscar
                simboloBuscar = Id == "0001L" ? "C$" : "U$";

                for (var rows = 0; rows < dgvReportePagoCajero.Rows.Count; rows++)
                {
                    //verificar si el id="0001L" or id="0001D"
                    if (dgvReportePagoCajero.Rows[rows].Cells["Idd"].Value.ToString() == Id)
                    {
                        //obtener el valor
                        var denominacion = dgvReportePagoCajero.Rows[rows].Cells["Denominaciond"].Value.ToString();
                        //quitar el simbolo C$ o U$
                        decimal valorDenominacion = Convert.ToDecimal(denominacion.Replace(simboloBuscar, ""));
                        dgvReportePagoCajero.Rows[rows].Cells["Resultado"].Value = valorDenominacion * Convert.ToInt32(dgvReportePagoCajero.Rows[rows].Cells["Cantidadd"].Value);
                        //sumar la lista de denominacion C$ o U$
                        sumaDenominacion += Convert.ToDecimal(dgvReportePagoCajero.Rows[rows].Cells["Resultado"].Value);
                    }                   
                }

            }
            else
            {              
                //simbolo a buscar
                simboloBuscar = dgvReportePagoCajero.Rows[filaGrid].Cells["Monedad"].Value.ToString() == "L" ? "C$" : "U$";            
                //sumar la lista de denominacion C$ o U$
                sumaDenominacion = Convert.ToDecimal(dgvReportePagoCajero.Rows[filaGrid].Cells["Cantidadd"].Value);
            }

            AsignarValorGridReportadoXCajero(Id, sumaDenominacion, simboloBuscar);
            
            //dgvReportePagoCajero.Rows[rows].Cells["Result"] = Convert.ToDecimal(dgvReportePagoCajero.Rows[rows].Cells["datRepPagCajeroDenominacion"].Value.ToString()) * Convert.ToDecimal(dgvReportePagoCajero.Rows[rows].Cells["datRepPagCajeroCantidad"].Value);        
        }

        private void AsignarValorGridReportadoXCajero(string Id, decimal value, string simbolo)
        {
            for (var rows = 0; rows < dgvGridRportadoXCajero.Rows.Count; rows++)
            {
                //buscar el Id en el gridview del cajero reportado
                if (dgvGridRportadoXCajero.Rows[rows].Cells["Idc"].Value.ToString() == Id)
                {
                    //asignar el simbolo del C$ o U$ y el monto
                    dgvGridRportadoXCajero.Rows[rows].Cells["Montoc"].Value =$"{simbolo} {value.ToString("N2")}";
                    break;
                }
               
            }
        }

        private void dgvReportePagoCajero_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //obtener el consecutivo
            int index = e.RowIndex;
            int columna = e.ColumnIndex;
           

            var columnaIndex = columna;

            if (index != -1 && columna != -1)
            {
                //(columna 3) es cantidad
                //columna Cantidad del DataGridView (columna=3)
                if (columnaIndex == 3)
                {
                    idActual = dgvReportePagoCajero.Rows[index].Cells[0].Value.ToString();
                    //antes de editar guardar temporalmente la cantidad en la variable  cantidadGrid por si la cantidad que digita el cajero le agrega otra cosa, entonce regresa al ultimo valor 
                    cantidadGrid = Convert.ToDecimal(dgvReportePagoCajero.Rows[index].Cells[columnaIndex].Value);
                }              
            }
        }

        private void dgvReportePagoCajero_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            int index = e.RowIndex;
            int columna = e.ColumnIndex;


            var columnaIndex = columna;

            if (index != -1 && columna != -1)
            {
                //(columna 3) es cantidad
                //columna Cantidad del DataGridView (columna=3)
                if (columnaIndex == 3)
                {
                    idActual = dgvReportePagoCajero.Rows[index].Cells[0].Value.ToString();
                    //antes de editar guardar temporalmente la cantidad en la variable  cantidadGrid por si la cantidad que digita el cajero le agrega otra cosa, entonce regresa al ultimo valor 
                    cantidadGrid = Convert.ToDecimal(dgvReportePagoCajero.Rows[index].Cells[columnaIndex].Value);
                }
            }
        }

        private void btnGuardarCierre_Click(object sender, EventArgs e)
        {
            ViewCierreCaja viewCierreCaja = new ViewCierreCaja();
            viewCierreCaja.Cierre_Det_Pago = new List<Cierre_Det_Pago>();
            try
            {
                
            }
            catch (Exception ex)
            {

            }
        }

        void CargarClaseCierreCaja(ViewCierreCaja viewCierreCaja)
        {
            viewCierreCaja.Cajero = User.Usuario;
            viewCierreCaja.NumCierre = User.ConsecCierreCT;
            viewCierreCaja.Caja = User.Caja;
            //total en cordoba que el cajero reporto
            viewCierreCaja.Total_Local = totatCajeroCordobas;
            //total en dolares que el cajero reporto
            viewCierreCaja.Total_Dolar = totalCajeroDolares;
            viewCierreCaja.Ventas_Efectivo = 0.00M;
            viewCierreCaja.Notas = this.txtNotas.Text;
            viewCierreCaja.Cobro_Efectivo_Rep = 0.000M;
            viewCierreCaja.Num_Cierre_Caja = "Num_Cierre_Caja";

            

            //foreach(var itemSistemReportad in _datosCierreCaja)
            //{
                //obtener el id
                //string id = $"{itemSistemReportad.Forma_Pago}{itemSistemReportad.Moneda}";
                for (int rows = 0; rows < this.dgvGridRportadoXCajero.Rows.Count; rows++)
                {
                    var Id = dgvGridRportadoXCajero.Rows[rows].Cells["Idc"].Value.ToString();
                var datSistema = _datosCierreCaja.Where(x => x.Id == Id).FirstOrDefault();

                    var simbolo = dgvGridRportadoXCajero.Rows[rows].Cells["Monedac"].Value.ToString() == "L" ? "C$" : "U$";
                    var montoUsuario = dgvGridRportadoXCajero.Rows[rows].Cells["Montoc"].Value.ToString().Replace(simbolo, "");

                    var _dataCierreDetPago = new Cierre_Det_Pago()
                    {
                        Identificacion = dgvGridRportadoXCajero.Rows[rows].Cells["Idc"].Value.ToString(),
                        Tipo_Pago = dgvGridRportadoXCajero.Rows[rows].Cells["TipoPagoc"].Value.ToString(),
                        Total_Usuario = Convert.ToDecimal(montoUsuario),
                        Total_Sistema = datSistema.Monto,
                        Diferencia = Convert.ToDecimal(montoUsuario) - datSistema.Monto,
                        //inicia desde cero (0)
                        Orden = rows,

                        Num_Cierre = User.ConsecCierreCT,
                        Cajero = User.Usuario,
                        Caja = User.Caja

                    };

                    viewCierreCaja.Cierre_Det_Pago.Add(_dataCierreDetPago);

                }

            


            for (int rows = 0; rows < this.dgvGridReportadoPorSistema.Rows.Count; rows++)
            {
                var simbolo = dgvGridRportadoXCajero.Rows[rows].Cells["Monedac"].Value.ToString() == "L" ? "C$" : "U$";
                var montoUsuario = dgvGridRportadoXCajero.Rows[rows].Cells["Montoc"].Value.ToString().Replace(simbolo, "");

                var _dataCierreDetPago = new Cierre_Det_Pago()
                {

                    Identificacion = dgvGridRportadoXCajero.Rows[rows].Cells["Idc"].Value.ToString(),
                    Tipo_Pago = dgvGridRportadoXCajero.Rows[rows].Cells["TipoPagoc"].Value.ToString(),
                    Total_Usuario = Convert.ToDecimal(montoUsuario),
                    Total_Sistema = 0.00M,
                    Diferencia = 0.00M,
                    //inicia desde cero (0)
                    Orden = rows,

                    Num_Cierre = User.ConsecCierreCT,
                    Cajero = User.Usuario,
                    Caja = User.Caja

                };

                viewCierreCaja.Cierre_Det_Pago.Add(_dataCierreDetPago);

            }

        }
    }
}