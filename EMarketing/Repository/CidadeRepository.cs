using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EMarketing.Repository
{
    public class CidadeRepository : BaseRepository
    {
        public List<Cidade> GetAllCidade()
        {
            return DataModel.Cidade.Include("Estado").ToList();
        }
    }
}