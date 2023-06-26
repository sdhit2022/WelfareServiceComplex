using Application.BaseInfo;
using Domain.ComplexModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;

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

        public IActionResult OnPost(TimeSpan ShfStartTime, TimeSpan ShfEndTime, int ShfTelorance,string ShfName)
        {
            
            var start = ((int)ShfStartTime.TotalMinutes);
            var end = ((int)ShfEndTime.TotalMinutes);
            Shift newShift = new Shift()
            {
                ShfStartTime = start,
                ShfEndTime = end,
                ShfName = ShfName,
                ShfTelorance = ShfTelorance
            };
            _shiftService.Insert(newShift);
            return Redirect("/Planing/Shift");
        }

        public IActionResult OnGetRemove(int id)
        {
            return new JsonResult(_shiftService.Remove(id));
        }

        public IActionResult OnGetEdit(int id)
        {
            var shift = _shiftService.GetShift(id);
            return new JsonResult(JsonConvert.SerializeObject(shift));
        }

        public IActionResult OnPostEdit(TimeSpan ShfStartTime, TimeSpan ShfEndTime, int ShfTelorance, string ShfName,int ShfId)
        {
            var shift = new Shift()
            {
                ShfId = ShfId,
                ShfStartTime = ((int)ShfStartTime.TotalMinutes),
                ShfEndTime = ((int)ShfEndTime.TotalMinutes),
                ShfTelorance = ShfTelorance,
                ShfName = ShfName

            };
            _shiftService.Update(shift);
            return Redirect("/Planing/Shift");

        }
    }
}
