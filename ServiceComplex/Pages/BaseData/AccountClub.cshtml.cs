using Application.BaseData.Dto;
using Application.BaseData;
using Application.Common;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Domain.ComplexModels;

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
        public List<SelectListOptionInt> Jobs;
        public List<SelectListOption> Contracts;


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
            Jobs = _service.SelectOptionJob();
            Contracts = _service.SelectOptionContract();
        }
        public IActionResult OnGetData(JqueryDatatableParam param)
        {
            var result = _service.GetAllAccountClub(param);
            return result;
        } 

        public IActionResult OnGetEdit(Guid id)
        {
            var result = _service.GetDetailsAccountClub(id);
            return Partial("BaseData/Partial/_EditAccountClub",result);
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

        public IActionResult OnGetCreateAccount()
        {
            return Partial("BaseData/Partial/_CreateAccountClub");
        }

        public IActionResult OnPostCreateAccount(CreateAccountClub command)
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(command.ShamsiBirthDay))
                {
                    command.ShamsiBirthDay = command.ShamsiBirthDay[..10];
                    command.ShamsiBirthDay.ToGeorgianDateTime();
                }
            }
            catch(Exception)
            {
                var operation = new ResultDto();
                return new JsonResult(operation.Failed("فرمت تاریخ وارد شده درست نمیباشد"));
            }
            return new JsonResult(_service.CreateAccountClub(command));
        }

        public IActionResult OnGetRemove(Guid id)
        {
            return new JsonResult(_service.RemoveAccountClub(id));
        }


    }
}
