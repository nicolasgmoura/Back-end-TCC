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
    [RoutePrefix("api/Acompanhamento")]
    public class AcompanhamentoController : ApiController
    {
        AcompanhamentoRepository _AcompanhamentoRepository;

        public AcompanhamentoRepository AcompanhamentoRepository
        {
            get
            {
                if(_AcompanhamentoRepository == null)
                {
                    _AcompanhamentoRepository = new AcompanhamentoRepository();
                }
                return _AcompanhamentoRepository;
            }

            set
            {
                _AcompanhamentoRepository = value;
            }
        }

        [HttpPost]
        [Route("Create")]
        public HttpResponseMessage Create(Acompanhamento acompanhamento)
        {

            try
            {
                if (ModelState.IsValid)
                {
                    if (AcompanhamentoRepository.existeAcompanhamento(acompanhamento))
                    {
                        return Request.CreateResponse(HttpStatusCode.BadRequest, "Você já está acompanhando esta empresa.");
                    }

                    AcompanhamentoRepository.Create(acompanhamento);

                    return Request.CreateResponse(HttpStatusCode.Created, "Acompanhamento realizado");
                }
                else
                {
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState + "Não foi possível cadastrar o usuário.");
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

        [HttpPost]
        [Route("DeixarDeAcompanhar")]
        public HttpResponseMessage Delete(Acompanhamento acompanhamento )
        {
            try
            {
                AcompanhamentoRepository.DeletarAcompanhamento(acompanhamento);
                return Request.CreateResponse(HttpStatusCode.OK + "Você não acompanha mais a empresa");
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest + ". Não foi possível fazer requisição =>" + ex);
            }

        }

    }
}
