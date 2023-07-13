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
            return new JsonResult( _baseDataService.GetAllContracts(param));

        }


        public void OnGetCreate()
        {

        }

        public IActionResult OnPostCreate(string CntTitle,string CntStartDateShamsi,string CntEndDateShamsi
            ,short CntType,string CntContractNum)
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
                CntId=new Guid(),
                CntTitle= CntTitle,
                CntStartDateShamsi= CntStartDateShamsi,
                CntEndDateShamsi = CntEndDateShamsi,
                CntType = CntType,
                CntContractNum = CntContractNum,
                CntCreateon=DateTime.Now
            };
            
            return new JsonResult(_baseDataService.CreateContract(contract));
        }


        public IActionResult OnGetEdit(Guid id)
        {
            ContractDto Contract = _baseDataService.GetContract(id);
            return new JsonResult(JsonConvert.SerializeObject(Contract));
        }
        public IActionResult OnPostEdit(Guid id,string CntTitle, string CntStartDateShamsi, string CntEndDateShamsi
            , short CntType, string CntContractNum)
        {
            Contract Contract;
            if (FrWarHosUid == Guid.Empty)
            {
                Contract = new Contract
                {
                    SlnId = SlnId,
                    SlnName = SlnName,
                    SlnType = SlnType,
                };
            }
            else
            {
                Contract = new Contract
                {
                    SlnId = SlnId,
                    SlnName = SlnName,
                    SlnType = SlnType,

                    FrWarHosUid = FrWarHosUid
                };
            }
            _baseDataService.UpdateContract(Contract);
            return Redirect("/BaseData/Contract");

        }

        public IActionResult OnGetRemove(long id)
        {
            return new JsonResult(_baseDataService.RemoveContract(id));
        }

        public IActionResult OnGetCheckName(string name)
        {
            return new JsonResult(_baseDataService.GetContractByName(name));
        }
        public IActionResult OnGetCheckContractNameExists(string name, long id)
        {
            return new JsonResult(_baseDataService.CheckContractNameExists(name, id));
        }
        public IActionResult OnGetChangeContract(long Contractid)
        {
            var Contract = _baseDataService.GetContract(Contractid);
            Global.ContractId["ContractId"] = Contract.SlnId;
            Global.ContractName["ContractName"] = Contract.SlnName;
            return new JsonResult(Contract.SlnName);
        }
    }
}
