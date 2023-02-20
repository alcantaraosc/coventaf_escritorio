using Api.Context;
using Api.Model.Modelos;
using Api.Model.ViewModels;
using Api.Service.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.Service.DataService
{
    public class ServiceFormaPago
    {
        
        public ServiceFormaPago()
        {
            
        }

        public async Task<IList<Forma_Pagos>> ListarFormaDePago(ResponseModel responseModel)
        {
            var listaFormaPago = new List<Forma_Pagos>();
            try
            {
                using (var _db = new CoreDBContext())
                {
                    listaFormaPago = await _db.Forma_Pagos.Where(fp => fp.Activo == "S").ToListAsync();
                }
               
                if (listaFormaPago.Count > 0)
                {
                    responseModel.Exito = 1;
                    responseModel.Mensaje = "Consulta exitosa";
                }
                else
                {
                    responseModel.Exito = 0;
                    responseModel.Mensaje = "consulta no encontrada";
                }                        
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return listaFormaPago;
        }

        public async Task<ListarDrownList> llenarComboxMetodoPagoAsync()
        {

            var listarDrownListModel = new ListarDrownList();
            listarDrownListModel.FormaPagos = new List<Forma_Pagos>();
            listarDrownListModel.CondicionPago = new List<Condicion_Pagos>();
            listarDrownListModel.TipoTarjeta = new List<Tipo_Tarjeta_Pos>();
            listarDrownListModel.EntidadFinanciera = new List<Entidad_Financieras>();

            bool consultaExitosa = false;
                    

            try
            {
                //listar 
                listarDrownListModel.FormaPagos = await ListarFormaDePago();

                if (listarDrownListModel.FormaPagos.Count > 0)
                {
                    //obtener la lista de condicione de pago
                    listarDrownListModel.CondicionPago = await ListarCondicionPago();

                    if (listarDrownListModel.CondicionPago.Count >0)
                    {
                        listarDrownListModel.TipoTarjeta = await ListarTipoTarjeta();

                        if (listarDrownListModel.TipoTarjeta.Count > 0)
                        {
                            listarDrownListModel.EntidadFinanciera = await ListarEntidadFinanciera();

                            if (listarDrownListModel.TipoTarjeta.Count > 0)
                            {
                                consultaExitosa = true;
                                listarDrownListModel.Exito = 1;
                                listarDrownListModel.Mensaje = "Consulta exitosa";
                            }
                        }
                    }
                   
                }

                if (!consultaExitosa)
                {                    
                    listarDrownListModel.Exito = 0;
                    listarDrownListModel.Mensaje = "La informacion principal para metodo de pago no se pudo completar";
                    listarDrownListModel.FormaPagos = null;
                    listarDrownListModel.CondicionPago = null;
                    listarDrownListModel.TipoTarjeta = null;
                    listarDrownListModel.EntidadFinanciera = null;
                }
           
            }
            catch (Exception ex)
            {
                //-1 indica que existe algun error del servidor
                listarDrownListModel.Exito = -1;
                listarDrownListModel.Mensaje = ex.Message;
            }

            return listarDrownListModel;
        }


        //forma de pagos
        public async Task<List<Forma_Pagos>> ListarFormaDePago()
        {
            var listFormaPago = new List<Forma_Pagos>();
            try
            {
                using (var _db = new CoreDBContext())
                {
                    listFormaPago = await _db.Forma_Pagos.Where(fp => fp.Activo == "S").ToListAsync();
                }
                           
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return listFormaPago;
        }

        public async Task<List<Condicion_Pagos>> ListarCondicionPago()
        {
            var listaCondicionPago = new List<Condicion_Pagos>();

            try
            {
                using (var _db = new CoreDBContext())
                {
                    listaCondicionPago = await _db.Condicion_Pagos.Where(x => x.Condicion_Pago != "0").ToListAsync();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return listaCondicionPago;
        }

        public async Task<List<Tipo_Tarjeta_Pos>> ListarTipoTarjeta()
        {
            var listaTipoTarjeta = new List<Tipo_Tarjeta_Pos>();

            try
            {
                using (var _db = new CoreDBContext())
                {
                    listaTipoTarjeta = await _db.Tipo_Tarjeta_Pos.ToListAsync();
                }
                
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return listaTipoTarjeta;
        }

        public async Task<List<Entidad_Financieras>> ListarEntidadFinanciera()
        {
            var listaEntidadFinanciera = new List<Entidad_Financieras>();

            try
            {
                using (var _db = new CoreDBContext())
                {
                    listaEntidadFinanciera = await _db.Entidad_Financieras.ToListAsync();
                }

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return listaEntidadFinanciera;
        }
    }
}
