using Application.Salon;
using Application.Warehouse;
using Domain.ComplexModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;

namespace ServiceComplex.Pages.BaseData
{
    public class SalonModel : PageModel
    {
        private readonly ISalonService _salonService;
        private readonly IWarehouseService _warehouseService;
        public List<Domain.ComplexModels.Salon> Salons;
        public List<WareHouse> WareHouses;

        public SalonModel(ISalonService salonService, IWarehouseService warehouseService)
        {
            _salonService = salonService;
            _warehouseService = warehouseService;
        }

        public void OnGet()
        {
            Salons=_salonService.GetAll();
            WareHouses=_warehouseService.GetAll();

        }


        public void OnGetCreate()
        {

        }

        public IActionResult OnPostCreate( string SlnName, short SlnType, Guid FrWarHosUid) {
            Domain.ComplexModels.Salon salon;
            if (FrWarHosUid == Guid.Empty)
            {
                salon = new Domain.ComplexModels.Salon
                {
                    SlnName = SlnName,
                    SlnType = SlnType,
                };
            }
            else
            {
                salon = new Domain.ComplexModels.Salon
                {
                    SlnName = SlnName,
                    SlnType = SlnType,

                    FrWarHosUid = FrWarHosUid,
                };
            }
            _salonService.InsertSalon(salon);
            return Redirect("/Salon/Salon");

        }

        public IActionResult OnGetEdit(long SLN_ID)
        {
            Domain.ComplexModels.Salon salon = _salonService.GetSalon(SLN_ID);
            return new JsonResult(JsonConvert.SerializeObject(salon));
        }
        public IActionResult OnPostEdit(long SlnId, string SlnName, short SlnType, Guid FrWarHosUid) 
        {
            Domain.ComplexModels.Salon salon;
            if (FrWarHosUid == Guid.Empty)
            {
                salon = new Domain.ComplexModels.Salon
                {
                    SlnId = SlnId,
                    SlnName = SlnName,
                    SlnType = SlnType,
                };
                }
            else {
                salon = new Domain.ComplexModels.Salon
                {
                    SlnId = SlnId,
                    SlnName = SlnName,
                    SlnType = SlnType,

                    FrWarHosUid = FrWarHosUid,
                };
            }
            _salonService.UpdateSalon(salon);
            return Redirect("/Salon/Salon");

        }

        public IActionResult OnGetRemove(long id)
        {
            return new JsonResult(_salonService.RemoveSalon(id));
        }
    }
}
