using EMarketing.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace EMarketing.Controllers
{
    [RoutePrefix("api/Estado")]
    public class EstadoController : ApiController
    {
        EstadoRepository _EstadoRepository;

        public EstadoRepository EstadoRepository
        {
            get
            {
                if (_EstadoRepository == null)
                {
                    _EstadoRepository = new EstadoRepository();
                }
                return _EstadoRepository;
            }

            set
            {
                _EstadoRepository = value;
            }

        }
        [HttpGet]
        [Route("GetAllEstado")]
        public HttpResponseMessage GetAllCidade()
        {
            var estados = EstadoRepository.GetAllEstado();
            try
            {
                if (estados == null)
                {
                    return Request.CreateResponse(HttpStatusCode.NotFound + ". Estados não encontrados.");
                }
                return Request.CreateResponse(HttpStatusCode.OK, estados);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError + "=>" + ex);
            }

        }
    }
}
