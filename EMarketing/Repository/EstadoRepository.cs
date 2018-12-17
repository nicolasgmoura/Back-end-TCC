﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EMarketing.Repository
{
    public class EstadoRepository:BaseRepository
    {
        public List<Estado> GetAllEstado()
        {
            return DataModel.Estado.ToList();
        }
    }
}