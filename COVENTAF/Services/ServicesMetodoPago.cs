using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace COVENTAF.Services
{
    public class ServicesMetodoPago
    {
        public string ObtenerDetallePago(string formaPago, string descripcionFormaPago, decimal montoDolar, decimal tipoCambio, string documento=null, 
            string entidadFinanciera = null, string tipoTarjeta = null, string DescripcionCondicionPago = null)
        {
            string detallePago = "";
            if (formaPago =="0001" && descripcionFormaPago == "EFECTIVO (DOLAR)")
            {
                detallePago = $"Monto en dolares: U${montoDolar.ToString("N2")} Tipo Cambio: {tipoCambio.ToString("N2")}";
            }
            else if (formaPago == "0002" && descripcionFormaPago == "CHEQUE")
            {
                detallePago = $"Entidad financiera: {entidadFinanciera}, No. cheque: {documento}";
            }
            else if (formaPago == "0002" && descripcionFormaPago == "CHEQUE (DOLAR)")
            {
                detallePago = $"Monto en dolares: U${montoDolar.ToString("N2")} Tipo Cambio: {tipoCambio.ToString("N2")}, Entidad financiera: {entidadFinanciera} numero de cheque: {documento}";
            }
            else if (formaPago == "0003" && descripcionFormaPago == "TARJETA")
            {
                detallePago = $"Tarjeta: { tipoTarjeta } ";
            }

            else if (formaPago == "0003" && descripcionFormaPago == "TARJETA (DOLAR)")
            {
                detallePago = $"Monto en dolares: {montoDolar.ToString("N2")} Tipo Cambio: {tipoCambio.ToString("N2")} Tarjeta: { tipoTarjeta } ";
            }

            else if (formaPago == "0004")
            {
                detallePago = $"Condicion Pago: {DescripcionCondicionPago} Documento: { documento } ";
            }

            else if (formaPago == "0006")
            {
                detallePago = $"Documento: { documento } ";
            }

            else if (formaPago == "FP01" && descripcionFormaPago == "GIFTCARD")
            {
                detallePago = $"Documento: { documento } ";
            }

            else if (formaPago == "FP01" && descripcionFormaPago == "GIFTCARD (DOLAR)")
            {
                detallePago = $"Monto en dólares: {montoDolar.ToString("N2")} Tipo Cambio: {tipoCambio.ToString("N2")} No. Tarjeta: { documento } ";
            }
            else if (formaPago != "0001")
            {
                detallePago = $"Documento: { documento } ";
            }

            return detallePago;
        }
    }
}
