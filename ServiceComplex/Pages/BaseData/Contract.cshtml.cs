using Application.BaseData;
using Application.BaseData.Dto;
using Application.BaseInfo;
using Application.Common;
using Domain.ComplexModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace ServiceComplex.Pages.BaseData
{
    public class ContractModel : PageModel
    {
        private readonly IBaseDataService _baseDataService;
        public List<ContractDto> Contracts;
        public List<SelectListOption> WareHouses;


        public ContractModel(IBaseDataService baseDataService)
        {
            _baseDataService = baseDataService;
        }

        public void OnGet()
        {
        }

        public IActionResult OnGetData(JqueryDatatableParam param)
        {
            var result = _baseDataService.GetAllContracts(param);
            return result;

        }


        public void OnGetCreate()
        {

        }

        public IActionResult OnPostCreate(string CntTitle, string CntStartDateShamsi, string CntEndDateShamsi
            , short CntType, string CntContractNum)
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(CntStartDateShamsi))
                {
                    CntStartDateShamsi = CntStartDateShamsi[..10];
                    CntStartDateShamsi.ToGeorgianDateTime();
                }
                if (!string.IsNullOrWhiteSpace(CntEndDateShamsi))
                {
                    CntEndDateShamsi = CntEndDateShamsi[..10];
                    CntEndDateShamsi.ToGeorgianDateTime();
                }

            }
            catch (Exception)
            {
                var operation = new ResultDto();
                return new JsonResult(operation.Failed("فرمت تاریخ وارد شده درست نمیباشد"));
            }
            var contract = new ContractDto()
            {
                CntId = new Guid(),
                CntTitle = CntTitle,
                CntStartDateShamsi = CntStartDateShamsi,
                CntEndDateShamsi = CntEndDateShamsi,
                CntType = CntType,
                CntContractNum = CntContractNum,
                CntCreateon = DateTime.Now
            };

            return new JsonResult(_baseDataService.CreateContract(contract));
        }


        public IActionResult OnGetEdit(Guid id)
        {
            ContractDto Contract = _baseDataService.GetContract(id);
            return new JsonResult(JsonConvert.SerializeObject(Contract));
        }
        public IActionResult OnPostEdit(Guid CntId, string CntTitle, string CntStartDateShamsi, string CntEndDateShamsi
            , short CntType, string CntContractNum)
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(CntStartDateShamsi))
                {
                    CntStartDateShamsi = CntStartDateShamsi[..10];
                    CntStartDateShamsi.ToGeorgianDateTime();
                }
                if (!string.IsNullOrWhiteSpace(CntEndDateShamsi))
                {
                    CntEndDateShamsi = CntEndDateShamsi[..10];
                    CntEndDateShamsi.ToGeorgianDateTime();
                }

            }
            catch (Exception)
            {
                var operation = new ResultDto();
                return new JsonResult(operation.Failed("فرمت تاریخ وارد شده درست نمیباشد"));
            }
            var contract = new ContractDto()
            {
                CntId = CntId,
                CntTitle = CntTitle,
                CntStartDateShamsi = CntStartDateShamsi,
                CntEndDateShamsi = CntEndDateShamsi,
                CntType = CntType,
                CntContractNum = CntContractNum,
                CntModifiedon = DateTime.Now
            };

            _baseDataService.UpdateContract(contract);
            return Redirect("/BaseData/Contract");

        }

        public IActionResult OnGetRemove(Guid id)
        {
            return new JsonResult(_baseDataService.RemoveContract(id));
        }

        public IActionResult OnGetCheckName(string name)
        {
            return new JsonResult(_baseDataService.GetContractByName(name));
        }
        public IActionResult OnGetCheckContractNameExists(string name, Guid id)
        {
            return new JsonResult(_baseDataService.CheckContractNameExists(name, id));
        }

        public IActionResult OnGetDefineRate(Guid id)
        {
            return Partial("BaseData/Partial/_DefineContractRate", _baseDataService.GetContract(id));
        }




    }
}
