using Application.BaseInfo;
using Domain.ComplexModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ServiceComplex.Pages.Planing
{
    public class ShiftModel : PageModel
    {
        private readonly IShiftService _shiftService;
        public List<Shift> Shifts;

        public ShiftModel(IShiftService shiftService)
        {
            _shiftService= shiftService;
        }
        public void OnGet()
        {
            Shifts = _shiftService.GetAll();
        }
    }
}
