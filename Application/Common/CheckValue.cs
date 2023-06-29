using Application.BaseInfo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common
{
    public class CheckValue
    {
        private readonly ISalonService _salonService;
        public CheckValue(ISalonService salonService)
        {
            _salonService= salonService;
        }

        public bool CheckName(string name,int service)
        {
            switch(service)
            {
                case 1 :
                  return _salonService.GetSalonByName(name);
                default : return false;
            }

        }

    }
}
