using EMarketing.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace EMarketing.Controllers
{
    [RoutePrefix("api/Cidade")]
    public class CidadeController : ApiController
    {
        CidadeRepository _CidadeRepository;

        public CidadeRepository CidadeRepository
        {
            get
            {
                if(_CidadeRepository == null)
                {
                    _CidadeRepository = new CidadeRepository();
                }
                return _CidadeRepository;
            }

            set
            {
                _CidadeRepository = value;
            }

        }
        [HttpGet]
        [Route("GetAllCidade")]
        public HttpResponseMessage GetAllCidade()
       {
            var cidades = CidadeRepository.GetAllCidade();
            try
            {
                if(cidades == null)
                {
                    return Request.CreateResponse(HttpStatusCode.NotFound + ". Cidades não encontradas.");
                }

                return Request.CreateResponse(HttpStatusCode.OK, cidades);
            }
            catch(Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError+"=>"+ex);
            }
            
        }

    }
}
