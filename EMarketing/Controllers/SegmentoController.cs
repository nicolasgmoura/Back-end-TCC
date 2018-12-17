using EMarketing.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace EMarketing.Controllers
{
    [RoutePrefix("api/Segmento")]
    public class SegmentoController : ApiController
    {
        SegmentoRepository _SegmentoRepository;

        public SegmentoRepository SegmentoRepository
        {
            get
            {
                if(_SegmentoRepository == null)
                {
                    _SegmentoRepository = new SegmentoRepository();
                }
                return _SegmentoRepository;
            }

            set
            {
                _SegmentoRepository = value;
            }
        }

        [HttpGet]
        [Route("GetAllSegmento")]
        public HttpResponseMessage GetAllSegmento()
        {
            var segmento = SegmentoRepository.GetAllSegmento();
            try
            {
                if (segmento == null)
                {
                    return Request.CreateResponse(HttpStatusCode.NotFound + ". Segmentos não encontrados.");
                }

                return Request.CreateResponse(HttpStatusCode.OK, segmento);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError + "=>" + ex);
            }

        }

    }
}
