using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EMarketing.Repository
{
    public class BaseRepository
    {
        DataModel _DataModel;

        public DataModel DataModel
        {
            get
            {
                if(_DataModel == null)
                {
                    _DataModel = new DataModel();
                }
                return _DataModel;
            }

            set
            {
                _DataModel = value;
            }
        }
    }
}