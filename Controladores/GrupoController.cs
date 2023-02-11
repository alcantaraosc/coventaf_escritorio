using Api.Model.Modelos;
using Api.Model.ViewModels;
using Api.Service.DataService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Controladores
{
    public class GrupoController
    {
        //private readonly IGrupo _serviceGrupo;
        private readonly ServiceGrupo _serviceGrupo;


        public GrupoController()
        {
            _serviceGrupo = new ServiceGrupo();
        }


        //[HttpGet("ListarGruposAsync")]
        public async Task<ResponseModel> ListarGruposAsync()
        {

            var responseModel = new ResponseModel() { Exito = 0 };
            responseModel.Data = new List<Grupos>();

            try
            {
                responseModel.Data = await _serviceGrupo.ListarGruposAsync(responseModel);
            }

            catch (Exception ex)
            {
                responseModel.Exito = -1;
                responseModel.Mensaje = ex.Message;
            }

            return responseModel;
        }
    }
}
