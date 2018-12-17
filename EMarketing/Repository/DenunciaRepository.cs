using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace EMarketing.Repository
{
    public class DenunciaRepository:BaseRepository
    {
        public void Save(Denuncia denuncia)
        {
            var publicacao = DataModel.Publicacao.Find(denuncia.IdPublicacao);

            if(denuncia.IdComentario == null)
            {
                Denuncia novaDenuncia = new Denuncia();
                novaDenuncia.IdConsumidor = denuncia.IdConsumidor;
                novaDenuncia.IdPublicacao = denuncia.IdPublicacao;
                novaDenuncia.DataDenuncia = denuncia.DataDenuncia;
                novaDenuncia.IdComentario = null;
                novaDenuncia.Mensagem = denuncia.Mensagem;

                DataModel.Denuncia.Add(novaDenuncia);
                publicacao.QuantidadeDenuncias++;
                DataModel.SaveChanges();

                
            }
            if(denuncia.IdPublicacao == null)
            {
                Denuncia novaDenuncia = new Denuncia();
                novaDenuncia.IdConsumidor = denuncia.IdConsumidor;
                novaDenuncia.IdPublicacao = denuncia.IdPublicacao;
                novaDenuncia.IdPublicacao = null;
                novaDenuncia.Mensagem = denuncia.Mensagem;

                DataModel.Denuncia.Add(novaDenuncia);
                DataModel.SaveChanges();
            }
        }



        public List<Denuncia> GetAllMyDenuncia(int idConsumidor)
        {
            return DataModel.Denuncia.Include("Publicacao").Where(e => e.IdConsumidor == idConsumidor).ToList();
        }

        public Denuncia GetOneDenuncia(int idDenuncia)
        {
            return DataModel.Denuncia.Find(idDenuncia);
        }

        public void Edit(Denuncia denuncia, int idDenuncia)
        {

            Denuncia denunciaAlterada = new Denuncia();

            denunciaAlterada.IdDenuncia = denuncia.IdDenuncia;
            denunciaAlterada.IdConsumidor = denuncia.IdConsumidor;
            denunciaAlterada.IdPublicacao = denuncia.IdPublicacao;
            denunciaAlterada.Mensagem = denuncia.Mensagem;
            denunciaAlterada.DataDenuncia = DateTime.Now;
            denunciaAlterada.IdComentario = denuncia.IdComentario;

            DataModel.Entry(denunciaAlterada).State = idDenuncia == 0 ? EntityState.Added : System.Data.Entity.EntityState.Modified;
            DataModel.SaveChanges();

        }



        public void DeleteDenuncia(Denuncia denuncia, int idDenuncia)
        {
            var publicacao = DataModel.Publicacao.Find(denuncia.IdPublicacao);
            DataModel.Denuncia.Remove(denuncia);
            
            publicacao.QuantidadeDenuncias--;

            DataModel.SaveChanges();
        }
    }
}