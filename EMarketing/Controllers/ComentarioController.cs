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
    [RoutePrefix("api/Comentario")]
    public class ComentarioController : ApiController
    {
        ComentarioRepository _ComentarioRepository;

        public ComentarioRepository ComentarioRepository
        {
            get
            {
                if(_ComentarioRepository == null)
                {
                    _ComentarioRepository = new ComentarioRepository();
                }
                return _ComentarioRepository;
            }

            set
            {
                _ComentarioRepository = value;
            }
        }

       
        [HttpPost]
        [Route("Create")]
        public HttpResponseMessage Create(Comentario comentario)
        {

            try
            {
                if (ModelState.IsValid)
                {
                    var publicacao = ComentarioRepository.DataModel.Publicacao.Find(comentario.IdPublicacao);

                    publicacao.QuantidadeComentarios++;
                    comentario.DataComentario = DateTime.Now;

                    ComentarioRepository.Save(comentario);
                   
                    return Request.CreateResponse(HttpStatusCode.Created, "Comentário efetuado com sucesso.");
                }
                else
                {
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Não foi possível realizar seu comentário ");
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
        [Route("Change/{idComentario}")]
        public HttpResponseMessage Change(Comentario comentario, int idComentario)
        {
            try
            {
                
                ComentarioRepository.Edit(comentario, idComentario);
                return Request.CreateResponse(HttpStatusCode.Created + ". Coméntário alterado com sucesso !");
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
        [Route("Delete/{idComentario}")]
        public HttpResponseMessage Delete(int idComentario)
        {
            try
            {
                var comentario = ComentarioRepository.GetOneComentario(idComentario);

                var publicacao = ComentarioRepository.DataModel.Publicacao.Find(comentario.IdPublicacao);

                publicacao.QuantidadeComentarios--;
                
                ComentarioRepository.DeleteComenatario(comentario, idComentario);
                
                return Request.CreateResponse(HttpStatusCode.OK + " Comentário excluido com sucesso  !");
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest + ". Não foi possível excluir o comentário =>" + ex);
            }


        }


        [HttpGet]
        [Route("ComentariosRecebidos/{idEmpresa}")]
        public HttpResponseMessage ComentariosRecebidos(int idEmpresa)
        {
            try
            {
                var comentarios = ComentarioRepository.ComentariosRecebidos(idEmpresa);


                if (comentarios == null)
                {

                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Nenhum comentário.");
                }

                return Request.CreateResponse(HttpStatusCode.OK, comentarios);

            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, "Erro inesperado no servidor =>" + ex);
            }

        }


        [HttpGet]
        [Route("MeusComentarios/{idUsuario}")]
        public HttpResponseMessage MeusComentarios(int idUsuario)
        {
            try
            {
                var comentarios = ComentarioRepository.MeusComentarios(idUsuario);


                if (comentarios == null)
                {

                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Nenhum comentário.");
                }

                return Request.CreateResponse(HttpStatusCode.OK, comentarios);

            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, "Erro inesperado no servidor =>" + ex);
            }

        }



    }
}
