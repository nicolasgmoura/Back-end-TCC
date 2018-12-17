using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace EMarketing.Repository
{
    public class ComentarioRepository : BaseRepository
    {
        public void Save(Comentario comentario)
        {
            var publicacao = DataModel.Publicacao.Find(comentario.IdPublicacao);

            Comentario novoComentario = new Comentario();

            novoComentario.IdPublicacao = comentario.IdPublicacao;
            novoComentario.Descricao = comentario.Descricao;
            novoComentario.DataComentario = comentario.DataComentario;
            novoComentario.IdUsuario = comentario.IdUsuario;

            publicacao.QuantidadeComentarios++;

            DataModel.Comentario.Add(novoComentario);
            DataModel.SaveChanges();
            
        }

        public void DeleteComenatario(Comentario comentario, int idComentario)
        {
            var publicacao = DataModel.Publicacao.Find(comentario.IdPublicacao);

            DataModel.Comentario.Remove(comentario);

            publicacao.QuantidadeComentarios--;
            DataModel.SaveChanges();
        }

        public Comentario GetOneComentario(int idComentario)
        {
            return DataModel.Comentario.Find(idComentario);
        }

        public void Edit(Comentario comentario, int idComentario)
        {
            Comentario comentarioAlterado = new Comentario();

            comentarioAlterado.Descricao = comentario.Descricao;
            comentarioAlterado.DataComentario = DateTime.Now;
            comentarioAlterado.IdComentario = comentario.IdComentario;
            comentarioAlterado.IdUsuario = comentario.IdUsuario;
            comentarioAlterado.IdPublicacao = comentario.IdPublicacao;

            DataModel.Entry(comentarioAlterado).State = idComentario == 0 ? EntityState.Added : System.Data.Entity.EntityState.Modified;
            DataModel.SaveChanges();

        }

        public List<Comentario> ComentariosRecebidos(int idEmpresa)
        {

            var publicacoes = DataModel.Publicacao.Where(e => e.IdUsuario == idEmpresa).ToList();

            var comentarios = DataModel.Comentario.Include("Usuario").ToList();

            List<Publicacao> listaPublicacoes = new List<Publicacao>();
            List<Comentario> listaComentarios = new List<Comentario>();


            foreach (var publicacao in publicacoes)
            {
                foreach (var comentario in comentarios)
                {
                    if(comentario.IdPublicacao == publicacao.IdPublicacao)
                    {
                        listaComentarios.Add(comentario);
                    }
                }
            }
            return listaComentarios;
        }

        public List<Comentario> MeusComentarios(int idUsuario)
        {

            var comentarios = DataModel.Comentario.Include("Publicacao").Include("Usuario").ToList();
            var empresas = DataModel.Empresa.ToList();

            List<Comentario> listaComentarios = new List<Comentario>();

            
                foreach (var comentario in comentarios)
                {
                   if(comentario.IdUsuario == idUsuario)
                    {
                        listaComentarios.Add(comentario);
                    }
                }
            return listaComentarios;
        }


    }
}