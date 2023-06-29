using Application.BaseInfo;
using Application.Common;
using Domain.ComplexModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;

namespace ServiceComplex.Pages.BaseData
{
    public class SalonModel : PageModel
    {
        private readonly ISalonService _salonService;
        private readonly IWarehouseService _warehouseService;
        public List<Salon> Salons;
        public List<SelectListOption> WareHouses;
        

        public SalonModel(ISalonService salonService, IWarehouseService warehouseService)
        {
            _salonService = salonService;
            _warehouseService = warehouseService;
        }

        public void OnGet()
        {
            Salons=_salonService.GetAll();
          //  WareHouses=_warehouseService.GetAll();
            WareHouses = _warehouseService.GetSelectListItems();
           
        }


        public void OnGetCreate()
        {

        }

        public IActionResult OnPostCreate( string SlnName, short SlnType, Guid FrWarHosUid) {
            Salon salon;

            if (FrWarHosUid == Guid.Empty)
            {
                salon = new Salon
                {
                    SlnName = SlnName,
                    SlnType = SlnType,
                };
            }
            else
            {
                salon = new Salon
                {
                    SlnName = SlnName,
                    SlnType = SlnType,
                    FrWarHosUid = FrWarHosUid,
                };
            }
            _salonService.InsertSalon(salon);
            return Redirect("/BaseData/Salon");

        }

        public IActionResult OnGetEdit(long SLN_ID)
        {
            Salon salon = _salonService.GetSalon(SLN_ID);
            return new JsonResult(JsonConvert.SerializeObject(salon));
        }
        public IActionResult OnPostEdit(long SlnId, string SlnName, short SlnType, Guid FrWarHosUid) 
        {
            Salon salon;
            if (FrWarHosUid == Guid.Empty)
            {
                salon = new Salon
                {
                    SlnId = SlnId,
                    SlnName = SlnName,
                    SlnType = SlnType,
                };
                }
            else {
                salon = new Salon
                {
                    SlnId = SlnId,
                    SlnName = SlnName,
                    SlnType = SlnType,

                    FrWarHosUid = FrWarHosUid
                };
            }
            _salonService.UpdateSalon(salon);
            return Redirect("/BaseData/Salon");

        }

        public IActionResult OnGetRemove(long id)
        {
            return new JsonResult(_salonService.RemoveSalon(id));
        }
    }
}
