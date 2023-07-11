using Application.BaseData.Dto;
using Application.BaseData;
using Application.Common;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Domain.ComplexModels;

namespace ServiceComplex.Pages.AccountClub
{
    public class AccountClubModel : PageModel
    {
        private readonly IBaseDataService _service;
        public CreateAccountClub Command;
        public List<AccountSelectOption> Account;
        public List<AccountRating> Rating;
        public List<AccountClubType> ClupType;
        public List<SelectListOption> States;

        public AccountClubModel(IBaseDataService service)
        {
            _service= service;
        }
        public void OnGet()
        {

        }
        public IActionResult OnGetData(JqueryDatatableParam param) => _service.GetAllAccountClub(param);

    }
}
