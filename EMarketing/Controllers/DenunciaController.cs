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
    [RoutePrefix("api/Denuncia")]
    public class DenunciaController : ApiController
    {
        DenunciaRepository _DenunciaRepository;

        public DenunciaRepository DenunciaRepository
        {
            get
            {
                if(_DenunciaRepository == null)
                {
                    _DenunciaRepository = new DenunciaRepository();
                }
                return _DenunciaRepository;
            }

            set
            {
                _DenunciaRepository = value;
            }
        }

        [HttpPost]
        [Route("Create")]
        public HttpResponseMessage Create(Denuncia denuncia)
        {
            try
            {
                denuncia.DataDenuncia = DateTime.Now;
                if (ModelState.IsValid)
                {

                    DenunciaRepository.Save(denuncia);

                    return Request.CreateResponse(HttpStatusCode.Created, "Denúncia realiazada.");
                }
                else
                {
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState + "Não foi possível realizar sua denúncia.");
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

        [HttpPut]
        [Route("Change/{idDenuncia}")]
        public HttpResponseMessage Change(Denuncia denuncia, int idDenuncia)
        {
            try
            {

                DenunciaRepository.Edit(denuncia, idDenuncia);
                return Request.CreateResponse(HttpStatusCode.Created + ". Denuncia alterada com sucesso !");
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


        [HttpDelete]
        [Route("Delete/{idDenuncia}")]
        public HttpResponseMessage Delete(int idDenuncia)
        {
            try
            {
                var denuncia = DenunciaRepository.GetOneDenuncia(idDenuncia);

                DenunciaRepository.DeleteDenuncia(denuncia, idDenuncia);

                return Request.CreateResponse(HttpStatusCode.OK + " Denúncia excluida com sucesso  !");
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest + ". Não foi possível excluir a denúncia =>" + ex);
            }
        }

        [HttpGet]
        [Route("MinhasDenuncias/{idConsumidor}")]
        public HttpResponseMessage MinhasDenuncias(int idConsumidor)
        {
            try
            {
                var denuncias = DenunciaRepository.GetAllMyDenuncia(idConsumidor);


                if (denuncias == null)
                {

                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Nenhuma denúncia encontrada.");
                }

                return Request.CreateResponse(HttpStatusCode.OK, denuncias);

            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, "Erro inesperado no servidor =>" + ex);
            }

        }


        [HttpGet]
        [Route("Denuncias")]
        public HttpResponseMessage Denuncias()
        {
            try
            {
                var denuncias = DenunciaRepository.DataModel.Denuncia.Include("Publicacao").ToList();


                if (denuncias == null)
                {

                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Nenhuma denúncia encontrada.");
                }

                return Request.CreateResponse(HttpStatusCode.OK, denuncias);

            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, "Erro inesperado no servidor =>" + ex);
            }

        }


    }
}
