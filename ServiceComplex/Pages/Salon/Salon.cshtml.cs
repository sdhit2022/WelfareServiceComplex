using Application.Salon;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ServiceComplex.Pages.BaseData
{
    public class SalonModel : PageModel
    {
        private readonly ISalonService _salonService;
        public List<Domain.ComplexModels.Salon> Salons;
        public SalonModel(ISalonService salonService)
        {
            _salonService = salonService;
        }

        public void OnGet()
        {
            Salons=_salonService.GetAll();
        }


        public Domain.ComplexModels.Salon onGetEdit(long SLN_ID)
        {
            Domain.ComplexModels.Salon salon = _salonService.GetSalon(SLN_ID);
            return salon;
        }
        public void OnPostEdit(Domain.ComplexModels.Salon sln) 
        {
            Domain.ComplexModels.Salon salon = new Domain.ComplexModels.Salon
            {
                SlnId = sln.SlnId,
                SlnName = sln.SlnName,
                SlnType = sln.SlnType,
                FrWarHosUid = sln.FrWarHosUid,
            };

        }
    }
}
