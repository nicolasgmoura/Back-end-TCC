using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EMarketing.Repository
{
    public class SegmentoRepository : BaseRepository
    {
        public List<Segmento> GetAllSegmento()
        {
            return DataModel.Segmento.ToList();
        }
    }
}