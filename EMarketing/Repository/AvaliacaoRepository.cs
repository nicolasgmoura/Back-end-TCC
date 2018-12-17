using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace EMarketing.Repository
{
    public class AvaliacaoRepository:BaseRepository
    {

        public void Save(Avaliacao avalicao)
        {
            var empresa = DataModel.Empresa.Find(avalicao.IdEmpresa);
            
            var listaAvaliacoes = DataModel.Avaliacao.ToList();

            bool editou = false;


            

            var avaliacoes = DataModel.Avaliacao.ToList();
            foreach (var item in avaliacoes)
            {
                if (item.IdEmpresa == avalicao.IdEmpresa && item.IdConsumidor == avalicao.IdConsumidor)
                {
                    Edit(avalicao, item.IdAvaliacao);
                    editou = true;
                }

            }

            if(editou == false)
            {
                empresa.QuantidadeAvaliacao++;
                float somaAvaliacoes = avalicao.ValorAvaliacao;

                Avaliacao novaAvaliacao = new Avaliacao();

                novaAvaliacao.IdConsumidor = avalicao.IdConsumidor;
                novaAvaliacao.IdEmpresa = avalicao.IdEmpresa;
                novaAvaliacao.ValorAvaliacao = avalicao.ValorAvaliacao;
                novaAvaliacao.DataAvaliacao = DateTime.Now;
                DataModel.Avaliacao.Add(novaAvaliacao);

                foreach (var i in listaAvaliacoes)
                {
                    if (i.IdEmpresa == avalicao.IdEmpresa)
                    {
                        somaAvaliacoes = somaAvaliacoes + i.ValorAvaliacao;
                    }
                }
                empresa.MediaAvaliacao = somaAvaliacoes / empresa.QuantidadeAvaliacao;
                DataModel.SaveChanges();

            }else
            {
                float somaAValiacoes = 0;

                foreach (var i in listaAvaliacoes)
                {
                    if (i.IdEmpresa == avalicao.IdEmpresa)
                    {
                        somaAValiacoes = somaAValiacoes + i.ValorAvaliacao;
                    }
                }
                empresa.MediaAvaliacao = somaAValiacoes / empresa.QuantidadeAvaliacao;
                DataModel.SaveChanges();
            }

        }

        public void Edit(Avaliacao avaliacao, int idAvaliacao)
        {
            var aval = DataModel.Avaliacao.Find(idAvaliacao);

            aval.IdConsumidor = avaliacao.IdConsumidor;
            aval.IdEmpresa = avaliacao.IdEmpresa;
            aval.IdAvaliacao = idAvaliacao;
            aval.ValorAvaliacao = avaliacao.ValorAvaliacao;
            aval.DataAvaliacao = DateTime.Now;
            DataModel.Entry(aval).State = idAvaliacao == 0 ? EntityState.Added : EntityState.Modified;
            DataModel.SaveChanges();

        }



    }
}