using EMarketing.Repository;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace EMarketing.Controllers
{

    [RoutePrefix("api/Avaliacao")]
    public class AvaliacaoController : ApiController
    {
        AvaliacaoRepository _AvaliacaoRepository;

        public AvaliacaoRepository AvaliacaoRepository
        {
            get
            {
                if(_AvaliacaoRepository == null)
                {
                    _AvaliacaoRepository = new AvaliacaoRepository();
                }
                return _AvaliacaoRepository;
            }

            set
            {
                _AvaliacaoRepository = value;
            }
        }

        [HttpPost]
        [Route("Create")]
        public HttpResponseMessage Create(Avaliacao avaliacao)
        {


            try
            {   if (ModelState.IsValid)
                {

                    AvaliacaoRepository.Save(avaliacao);

                    return Request.CreateResponse(HttpStatusCode.Created, "Avaliação efetuada.");
                }
                else
                {
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState + "Não foi possível realizar avaliação.");
                }

            }
            catch (DbEntityValidationException ex)
            {
                foreach (var eve in ex.EntityValidationErrors)
                {
                    foreach (var ve in eve.ValidationErrors)
                    {
                        return Request.CreateResponse(HttpStatusCode.BadRequest + ". Solicitação não atendida. " + ve.ErrorMessage);
                    }
                }
                throw;
            }

        }

        // POST: api/Avaliacao
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Avaliacao/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Avaliacao/5
        public void Delete(int id)
        {
        }
    }
}
