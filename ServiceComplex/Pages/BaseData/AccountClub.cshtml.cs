using Application.BaseData.Dto;
using Application.BaseData;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Domain.ComplexModels;
using Application.Common;
using Microsoft.AspNetCore.Mvc;

namespace ServiceComplex.Pages.BaseData
{
    public class AccountClubModel : PageModel
    {
        private readonly IBaseDataService _service;
        public CreateAccountClub Command;
        public List<AccountSelectOption> Account;
        public List<AccountRating> Rating;
        public List<AccountClubType> ClupType;
        public List<SelectListOption> States;
        public List<AccountClubDto> List;

        public AccountClubModel(IBaseDataService service)
        {
            _service= service;
        }
        public void OnGet()
        {
            Account = _service.GetSelectOptionAccounts();
            Rating = _service.GetSelectOptionRatings();
            ClupType = _service.GetSelectOptionClubTypes();
            States = _service.SelectOptionState();
        }
        public IActionResult OnGetData(JqueryDatatableParam param)
        {
           return _service.GetAllAccountClub(param);
        }


        public IActionResult OnGetCities(Guid stateId)
        {
            return new JsonResult(_service.SelectOptionCities(stateId));
        }
        //public IActionResult OnGetAccountClub(JqueryDatatableParam param)
        //{
        //    return this.Partial("AccountClub", new List<AccountClubDto>());
        //}


        public IActionResult OnPostCreate(CreateAccountClub command)
        {

            try
            {
                if (!string.IsNullOrWhiteSpace(command.ShamsiBirthDay))
                {
                    command.ShamsiBirthDay = command.ShamsiBirthDay[..10];
                    command.ShamsiBirthDay.ToGeorgianDateTime();
                }
            }
            catch (Exception)
            {
                var operation = new ResultDto();
                return new JsonResult(operation.Failed("فرمت تاریخ وارد شده درست نمیباشد"));
            }
            return new JsonResult(_service.CreateAccountClub(command));
        }


        public IActionResult OnGetDetails(Guid id)
        {
            return Partial("EditAccountClub", _service.GetDetailsAccountClub(id));
        }
        public IActionResult OnPostEdit(EditAccountClub command)
        {
            if (!ModelState.IsValid)
                return this.Page();

            try
            {
                if (!string.IsNullOrWhiteSpace(command.ShamsiBirthDay))
                {
                    command.ShamsiBirthDay = command.ShamsiBirthDay[..10];
                    command.ShamsiBirthDay.ToGeorgianDateTime();
                }
            }
            catch (Exception)
            {
                var operation = new ResultDto();
                return new JsonResult(operation.Failed("فرمت تاریخ وارد شده درست نمیباشد"));
            }

            return new JsonResult(_service.UpdateAccountClub(command));
        }


        public IActionResult OnGetRemove(Guid id)
        {
            return new JsonResult(_service.RemoveAccountClub(id));
        }
    }
}
