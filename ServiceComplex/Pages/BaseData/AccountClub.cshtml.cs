using Application.BaseData.Dto;
using Application.BaseData;
using Application.Common;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Domain.ComplexModels;
using Application.BaseInfo;

namespace ServiceComplex.Pages.BaseData
{
    public class AccountClubModel : PageModel
    {
        private readonly IAccountClubService _service;
        public CreateAccountClub Command;
        public List<AccountSelectOption> Account;
        public List<AccountRating> Rating;
        public List<AccountClubType> ClupType;
        public List<SelectListOption> States;

        public List<AccountClubVM> Accounts;

        public AccountClubModel(IAccountClubService service)
        {
            _service= service;
        }
        public void OnGet()
        {

        }
        public IActionResult OnGetData(JqueryDatatableParam param)
        {
            var result = _service.GetAllAccountClub(param);
            return result;
        } 

        public IActionResult OnGetEdit(Guid id)
        {

            return Partial("BaseData/Partial/_EditAccountClub", _service.GetDetailsAccountClub(id));
        }

    }
}
