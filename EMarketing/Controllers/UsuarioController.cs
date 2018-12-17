using EMarketing.Repository;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Runtime.Serialization.Json;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;

namespace EMarketing.Controllers
{
    [RoutePrefix("api/Usuario")]

    public class UsuarioController : ApiController
    {
        UsuarioRepository _UsuarioRepository;

        public UsuarioRepository UsuarioRepository
        {
            get
            {
                if(_UsuarioRepository == null)
                {
                    _UsuarioRepository = new UsuarioRepository();
                }
                return _UsuarioRepository;
            }

            set
            {
                _UsuarioRepository = value;
            }
        }

        [HttpGet]
        [Route("Details/{id}")]
        public HttpResponseMessage Details(int id)
        {
            try
            {
                var usuario = UsuarioRepository.GetOneUsuario(id);
                
                if (usuario == null)
                {
                  
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Usuário não encontrado");
                }

                var cidade = UsuarioRepository.DataModel.Cidade.Find(usuario.IdCidade);
                var estado = UsuarioRepository.DataModel.Estado.Find(usuario.Cidade.IdEstado);
                var consumidor = UsuarioRepository.DataModel.Consumidor.Find(usuario.IdUsuario);
                var empresa = UsuarioRepository.DataModel.Empresa.Find(usuario.IdUsuario);


                return Request.CreateResponse(HttpStatusCode.OK, usuario );
                

            }
            catch(Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, "Erro inesperado no servidor =>" + ex);
            }
            
        }
       
        [HttpPost]
        [Route("Create")]
        public HttpResponseMessage Create(Usuario usuario)
        {

            
            try
            {
                if (usuario.IsEmpresa)
                {
                    string path = HttpContext.Current.Server.MapPath("~/Imagens/");
                    var bits = Convert.FromBase64String(usuario.Empresa.Logo);
                    string nomeImagem = Guid.NewGuid().ToString() + DateTime.Now.ToString("ddMMyyHHmmss") + ".jpg";
                    string imgPath = Path.Combine(path, nomeImagem);
                    File.WriteAllBytes(imgPath, bits);
                    usuario.Empresa.Logo = nomeImagem;


                }


                if (UsuarioRepository.existeNomeUsuario(usuario.NomeUsuario))
                {
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Nome de Usuário já existe");
                }
                if (usuario.IsEmpresa)
                {
                    if (UsuarioRepository.existeCNPJ(usuario.Empresa.CNPJ))
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Já existe este CNPJ cadastrado !");
                    }

                }

                if (ModelState.IsValid)
                {
                    
                    UsuarioRepository.Save(usuario);

                    return Request.CreateResponse(HttpStatusCode.Created, "Usuário cadastrado com sucesso.");
                }
                else
                {
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest,ModelState+"Não foi possível cadastrar o usuário.");
                }

            }catch(DbEntityValidationException ex)
            {
                foreach (var eve in ex.EntityValidationErrors)
                {
                    foreach (var ve in eve.ValidationErrors)
                    {
                       return Request.CreateResponse(HttpStatusCode.BadRequest+". Solicitação não atendida. "+ ve.ErrorMessage);
                    }
                }
                throw;
            }
            
        }

        [HttpPut]
        [Route("Change/{idUsuario}")]
        public HttpResponseMessage Change(int idUsuario, Usuario usuario)
        {
            try
            {
                if(usuario.Empresa.Logo.Length > 1000)
                {
                    string path = HttpContext.Current.Server.MapPath("~/Imagens/");
                    var bits = Convert.FromBase64String(usuario.Empresa.Logo);
                    string nomeImagem = Guid.NewGuid().ToString() + DateTime.Now.ToString("ddMMyyHHmmss") + ".jpg";
                    string imgPath = Path.Combine(path, nomeImagem);
                    File.WriteAllBytes(imgPath, bits);
                    usuario.Empresa.Logo = nomeImagem;
                }

                UsuarioRepository.Edit(usuario, idUsuario);
                return Request.CreateResponse(HttpStatusCode.Created + ". Usuário alterado com sucesso !");
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
        [Route("Delete/{idUsuario}")]
        public HttpResponseMessage Delete(int idUsuario)
        {
            try
            {
                var usuario = UsuarioRepository.GetOneUsuario(idUsuario);

                if (usuario.IsEmpresa)
                {
                    var empresa = UsuarioRepository.GetOneEmpresa(idUsuario);
                    UsuarioRepository.DeleteEmpresa(empresa, idUsuario);
                }
                else
                {
                    var consumidor = UsuarioRepository.GetOneConsumidor(usuario.IdUsuario);
                    UsuarioRepository.DeleteConsumidor(consumidor, idUsuario);
                }
                return Request.CreateResponse(HttpStatusCode.OK + " Usuário desativado com sucesso !");
            }
            catch(Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest + ". Não foi possível excluir o usuário =>"+ex);
            }
            
        }

        [HttpGet]
        [Route("MinhasEmpresas/{idConsumidor}")]
        public HttpResponseMessage VerPublicacao(int idConsumidor)
        {
            try
            {
                var empresas = UsuarioRepository.GetAllEmpresaByIdConsumidor(idConsumidor);

                
                var usuarios = UsuarioRepository.DataModel.Usuario.ToList();

                if (empresas == null)
                {

                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Nenhuma publicação encontrada.");
                }

                return Request.CreateResponse(HttpStatusCode.OK, empresas);

            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, "Erro inesperado no servidor =>" + ex);
            }

        }




        [HttpGet]
        [Route("Empresas")]
        public HttpResponseMessage Empresas()
        {
            try
            {
                var empresas = UsuarioRepository.DataModel.Empresa.ToList();
                

                if (empresas == null)
                {

                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Nenhuma empresa encontrada.");
                }

                return Request.CreateResponse(HttpStatusCode.OK, empresas);

            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, "Erro inesperado no servidor =>" + ex);
            }

        }



        [HttpGet]
        [Route("Consumidores")]
        public HttpResponseMessage Consumidores()
        {
            try
            {
                var consumidores = UsuarioRepository.DataModel.Consumidor.Include("Usuario").ToList();

                var cidades = new Cidade();

                foreach (var consumidor in consumidores)
                {
                    cidades = UsuarioRepository.DataModel.Cidade.Find(consumidor.Usuario.IdCidade);
                }


                if (consumidores == null)
                {

                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Nenhum consumidor encontrado.");
                }

                return Request.CreateResponse(HttpStatusCode.OK, consumidores);
            }

            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, "Erro inesperado no servidor =>" + ex);
            }

        }


        [HttpGet]
        [Route("MeusSeguidores/{idEmpresa}")]
        public HttpResponseMessage MeusSeguidores(int idEmpresa)
        {
            try
            {
                var consumidores = UsuarioRepository.GetAllConsumidorByIdEmpresa(idEmpresa);




                if (consumidores == null)
                {

                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Nenhum Seguidor.");
                }

                return Request.CreateResponse(HttpStatusCode.OK, consumidores);

            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, "Erro inesperado no servidor =>" + ex);
            }

        }


        [HttpGet]
        [Route("EmpresasDestaque/{idConsumidor}")]
        public HttpResponseMessage EmpresasDestaque(int idConsumidor)
        {
            try
            {
                var empresas = UsuarioRepository.EmpresasDesque();

                var usuarios = UsuarioRepository.DataModel.Usuario.ToList();

                if (empresas == null)
                {

                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Nenhuma empresas encontrada.");
                }

                return Request.CreateResponse(HttpStatusCode.OK, empresas);

            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, "Erro inesperado no servidor =>" + ex);
            }

        }


        [HttpPost]
        [Route("Logar")]
        public HttpResponseMessage Logar(Usuario usuario)
        {

            try
            {
                if (ModelState.IsValid)
                {
                    bool logou = UsuarioRepository.Logar(usuario.NomeUsuario,usuario.Senha);
                    

                    if (logou)
                    {
                        var usuarioLogado = UsuarioRepository.UsuarioLogado(usuario.NomeUsuario);

                        var cidade = UsuarioRepository.DataModel.Cidade.Find(usuarioLogado.IdCidade);

                        var estado = UsuarioRepository.DataModel.Estado.Find(cidade.IdEstado);
                       
                        var empresa = UsuarioRepository.DataModel.Empresa.Find(usuarioLogado.IdUsuario);
                        
                        var consumidor = UsuarioRepository.GetOneConsumidor(usuarioLogado.IdUsuario);

                        if (usuarioLogado.IsEmpresa)
                        {
                            var segmento = UsuarioRepository.DataModel.Segmento.Find(empresa.IdSegmento);
                        }
                        
                       
                        return Request.CreateResponse(HttpStatusCode.OK, usuarioLogado);
                    }else
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Usuário ou senha incorretos.");
                    }
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.BadRequest, ModelState + "Não foi possível cadastrar o usuário.");
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
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                UsuarioRepository.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
