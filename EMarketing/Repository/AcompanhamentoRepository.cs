using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EMarketing.Repository
{
    public class AcompanhamentoRepository:BaseRepository
    {
        public void Create(Acompanhamento acompanhamento)
        {
            Acompanhamento novoAcompanhamento = new Acompanhamento();
            novoAcompanhamento.IdConsumidor = acompanhamento.IdConsumidor;
            novoAcompanhamento.IdEmpresa = acompanhamento.IdEmpresa;

            DataModel.Acompanhamento.Add(novoAcompanhamento);
            DataModel.SaveChanges();
        }


        public void DeletarAcompanhamento(Acompanhamento acompanhamento)
        {
            var acompanhamentos = DataModel.Acompanhamento.ToList();
            int idAcompanhamento = 0;
            foreach (var item in acompanhamentos)
            {
                if(acompanhamento.IdConsumidor == item.IdConsumidor && acompanhamento.IdEmpresa== item.IdEmpresa)
                {
                    idAcompanhamento = item.IdAcompanhamento;
                }
            }
            acompanhamento = DataModel.Acompanhamento.Find(idAcompanhamento);
            DataModel.Acompanhamento.Remove(acompanhamento);
            DataModel.SaveChanges();
        }


        public bool existeAcompanhamento(Acompanhamento acompanhamento)
        {
            if(DataModel.Acompanhamento.Where(e=>e.IdEmpresa == acompanhamento.IdEmpresa && e.IdConsumidor == acompanhamento.IdConsumidor).Count() > 0)
            {
                return true;
            }else
            {
                return false;
            }
        }

        public Acompanhamento GetOne(int idAcompanhamento)
        {
            return DataModel.Acompanhamento.Find(idAcompanhamento);
        }
    }
}