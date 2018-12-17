using EMarketing.Repository;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace EMarketing.Controllers
{
    [RoutePrefix("api/Publicacao")]
    public class PublicacaoController : ApiController
    {
        PublicacaoRepository _PublicacaoRepository;

        public PublicacaoRepository PublicacaoRepository
        {
            get
            {
                if (_PublicacaoRepository == null)
                {
                    _PublicacaoRepository = new PublicacaoRepository();
                }
                return _PublicacaoRepository;
            }

            set
            {
                _PublicacaoRepository = value;
            }
        }

        [HttpGet]
        [Route("Details/{id}")]
        public HttpResponseMessage Details(int id)
        {
            try
            {
                var publicacao = PublicacaoRepository.GetOnePublicacao(id);

                if (publicacao == null)
                {

                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Publicacao não encontrado");
                }

                //var usuario = PublicacaoRepository.DataModel.Usuario.Find(publicacao.IdUsuario);
                //var empresa = PublicacaoRepository.DataModel.Empresa.Find(usuario.IdEmpresa);

                return Request.CreateResponse(HttpStatusCode.OK, publicacao);

            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, "Erro inesperado no servidor =>" + ex);
            }

        }

        [HttpPost]
        [Route("Create")]
        public HttpResponseMessage Create(Publicacao publicacao)
        {
            string path = HttpContext.Current.Server.MapPath("~/Imagens/");
            var bits = Convert.FromBase64String(publicacao.ImagemPublicacao);
            string nomeImagem = Guid.NewGuid().ToString() + DateTime.Now.ToString("ddMMyyHHmmss") + ".jpg";
            string imgPath = Path.Combine(path, nomeImagem);
            File.WriteAllBytes(imgPath, bits);
            publicacao.ImagemPublicacao = nomeImagem;
            publicacao.DataPublicacao = DateTime.Now;

            try
            {
                if (ModelState.IsValid)
                {
                    PublicacaoRepository.Save(publicacao);

                    return Request.CreateResponse(HttpStatusCode.Created, ". Publicação efetuada.");
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.BadRequest, ModelState + ". Não foi possível fazer a publicação");
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
        [Route("Change/{idPublicacao}")]
        public HttpResponseMessage Change(int idPublicacao, Publicacao publicacao)
        {
            try
            {
                if (publicacao.ImagemPublicacao.Length > 1000)
                {
                    string path = HttpContext.Current.Server.MapPath("~/Imagens/");
                    var bits = Convert.FromBase64String(publicacao.ImagemPublicacao);
                    string nomeImagem = Guid.NewGuid().ToString() + DateTime.Now.ToString("ddMMyyHHmmss") + ".jpg";
                    string imgPath = Path.Combine(path, nomeImagem);
                    File.WriteAllBytes(imgPath, bits);
                    publicacao.ImagemPublicacao = nomeImagem;
                }
                PublicacaoRepository.EditPublicacao(publicacao, idPublicacao);
                return Request.CreateResponse(HttpStatusCode.Created + ". Publicacao alterada");
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
        [Route("Delete/{idPublicacao}")]
        public HttpResponseMessage Delete(int idPublicacao)
        {
            try
            {
                var publicacao = PublicacaoRepository.DataModel.Publicacao.Find(idPublicacao);


                PublicacaoRepository.DeletePublicacao(publicacao, idPublicacao);

                return Request.CreateResponse(HttpStatusCode.OK + ". Publicação excluída !");
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest + ". Não foi possível excluir a publicação =>" + ex);
            }

        }


        [HttpDelete]
        [Route("DeletePublicacaoDenunciaComentario/{idPublicacao}")]
        public HttpResponseMessage DeletePublicacao(int idPublicacao)
        {
            try
            {

                PublicacaoRepository.DeletePublicacaoDenuncia(idPublicacao);

                return Request.CreateResponse(HttpStatusCode.OK + ". Dados excluídos !");
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest + ". Não foi possível excluir a publicação =>" + ex);
            }

        }

        [HttpGet]
        [Route("VerPublicacao/{idConsumidor}")]
        public HttpResponseMessage VerPublicacao(int idConsumidor)
        {
            try
            {
                var publicacoes = PublicacaoRepository.GetAllPublicacaoByIdConsumidor(idConsumidor);

                var usuarios = PublicacaoRepository.DataModel.Usuario.ToList();
                var empresas = PublicacaoRepository.DataModel.Empresa.ToList();

                if (publicacoes == null)
                {

                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Nenhuma publicação encontrada.");
                }

                return Request.CreateResponse(HttpStatusCode.OK, publicacoes);

            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, "Erro inesperado no servidor =>" + ex);
            }

        }
    
        [HttpGet]
        [Route("MinhasPublicacoes/{idConsumidor}")]
        public HttpResponseMessage MinhasPublicacoes(int idConsumidor)
        {
            try
            {
                var publicacoes = PublicacaoRepository.GetAllMyPublicacao(idConsumidor);

                var usuarios = PublicacaoRepository.DataModel.Usuario.ToList();
                var empresas = PublicacaoRepository.DataModel.Empresa.ToList();

                if (publicacoes == null)
                {

                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Nenhuma publicação encontrada.");
                }

                return Request.CreateResponse(HttpStatusCode.OK, publicacoes);

            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, "Erro inesperado no servidor =>" + ex);
            }

        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                PublicacaoRepository.Dispose();
            }
            base.Dispose(disposing);
        }

    }
}
