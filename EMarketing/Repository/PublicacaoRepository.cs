using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace EMarketing.Repository
{
    public class PublicacaoRepository : BaseRepository,IDisposable
    {

        public void Save(Publicacao publicacao)
        {
            Publicacao NewPubicacao = new Publicacao();

            NewPubicacao.IdUsuario = publicacao.IdUsuario;
            NewPubicacao.ImagemPublicacao = publicacao.ImagemPublicacao;
            NewPubicacao.Titulo = publicacao.Titulo;
            NewPubicacao.AreaPublicacao = publicacao.AreaPublicacao;
            NewPubicacao.DataPublicacao = publicacao.DataPublicacao;

            NewPubicacao.QuantidadeComentarios = publicacao.QuantidadeComentarios;
            NewPubicacao.QuantidadeDenuncias = publicacao.QuantidadeDenuncias;
            NewPubicacao.QuantidadeVisualizacoes = publicacao.QuantidadeVisualizacoes;

            DataModel.Publicacao.Add(NewPubicacao);
            DataModel.SaveChanges();
        }

        public Publicacao GetOnePublicacao(int id)
        {
            return DataModel.Publicacao.Find(id);
        }

        public List<Publicacao> GetAllMyPublicacao(int idConsumidor)
        {
            return DataModel.Publicacao.Where(e => e.IdUsuario == idConsumidor).ToList();
        }

        public List<Publicacao> GetAllPublicacaoByIdConsumidor(int idConsumidor)
        {
            List<Publicacao> listPublicacao = new List<Publicacao>();

            var listAcompanhamento = DataModel.Acompanhamento.Where(e => e.IdConsumidor == idConsumidor).ToList();

            var publicacoes = DataModel.Publicacao.ToList();

            foreach (var item in listAcompanhamento)
            {
                foreach (var publicacao in publicacoes)
                {
                    if(item.IdEmpresa == publicacao.IdUsuario)
                    {
                        listPublicacao.Add(publicacao);
                    }
                }
                   
            }
            return listPublicacao;

        }

        public void DeletePublicacao(Publicacao publicacao, int idPublicacao)
        {
            DataModel.Publicacao.Remove(publicacao);
            DataModel.SaveChanges();
        }

        public void DeletePublicacaoDenuncia(int idPublicacao)
        {
            var denuncias = DataModel.Denuncia.ToList();
            var comentarios = DataModel.Comentario.ToList();

            foreach (var denuncia in denuncias)
            {
                if(denuncia.IdPublicacao ==idPublicacao)
                {
                    DataModel.Denuncia.Remove(denuncia);
                    DataModel.SaveChanges();
                }
            }

            foreach (var comentario in comentarios)
            {
                if(comentario.IdPublicacao == idPublicacao)
                {
                    DataModel.Comentario.Remove(comentario);
                    DataModel.SaveChanges();
                }
            }

            var publi = GetOnePublicacao(idPublicacao);

            DataModel.Publicacao.Remove(publi);
            DataModel.SaveChanges();

        }
        public void EditPublicacao(Publicacao publicacao, int idPublicacao)
        {
            
            Publicacao publicacaoAlterada = new Publicacao();

            publicacaoAlterada.IdPublicacao = idPublicacao;
            publicacaoAlterada.IdUsuario = publicacao.IdUsuario;
            publicacaoAlterada.ImagemPublicacao = publicacao.ImagemPublicacao;
            publicacaoAlterada.DataPublicacao = publicacao.DataPublicacao;
            publicacaoAlterada.Titulo = publicacao.Titulo;
            publicacaoAlterada.AreaPublicacao = publicacao.AreaPublicacao;
            
            DataModel.Entry(publicacaoAlterada).State = idPublicacao == 0 ? EntityState.Added : System.Data.Entity.EntityState.Modified;
            DataModel.SaveChanges();
        }

        public void Dispose()
        {
            DataModel.Dispose();
        }
    }
}