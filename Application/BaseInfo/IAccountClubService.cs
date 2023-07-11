using Application.BaseData;
using Application.BaseData.Dto;
using Application.Common;
using Application.Interfaces.Context;
using Application.Product;
using AutoMapper;
using Domain.ComplexModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Application.BaseInfo
{
    public interface IAccountClubService
    {
        List<AccountClubVM> GetAll();
        JsonResult GetAllAccountClub(JqueryDatatableParam param);

        ResultDto Update(AccountClubVM accountClubVM);
        ResultDto Delete(int id);
        AccountClubVM GetById(int id);
        ResultDto Insert(AccountClubVM accountClubVM);
    }

    public class AccountClubService : IAccountClubService
    {
        private readonly IComplexContext _complexContext;
        private readonly IProductService _productService;
        private readonly ILogger<BaseDataService> _logger;
        private readonly IHttpContextAccessor _contextAccessor;

        public AccountClubService(IComplexContext complexContext,
            IProductService productService,ILogger logger,IHttpContextAccessor contextAccessor)
        {
            _complexContext= complexContext;
            _productService= productService;
            _contextAccessor=contextAccessor;
            logger = _logger;
        }
        public ResultDto Delete(int id)
        {
            throw new NotImplementedException();
        }

        public List<AccountClubVM> GetAll()
        {
            return _complexContext.AccountClubs.AsNoTracking().Include(x => x.AccFrJob)
                .Include(x => x.AccFrContract).Select(
                x => new
                {
                    x.AccClbUid,
                    x.AccClbBrithday,
                    x.AccClbName,
                    x.AccClbCode,
                    x.AccClbStatus,
                    x.SysUsrCreatedon,
                    x.SysUsrCreatedby,
                    x.SysUsrModifiedon,
                    x.SysUsrModifiedby,
                    x.AccUid,
                    x.AccRateUid,
                    x.AccClbNationalCode,
                    x.AccClbPostalCode,
                    x.AccClbPhone1,
                    x.AccClbMobile,
                    x.AccClbParentUid,
                    x.AccClbTypUid,
                    x.AccClbSex,
                    x.AccClbAddress,
                    x.AccClbDescribtion,
                    x.AccClbClubCard,
                    x.AccFrJob,
                    x.AccFrContract,
                    x.AccCardSerial,
                    x.AccRelationType,
                }
                ).Select(x => new AccountClubVM
                {
                    AccClbUid = x.AccClbUid,
                    AccClbBrithday = x.AccClbBrithday,
                    AccClbName = x.AccClbName,
                    AccClbCode = x.AccClbCode,
                    AccClbStatus = x.AccClbStatus,
                    SysUsrCreatedon = x.SysUsrCreatedon,
                    SysUsrCreatedby = x.SysUsrCreatedby,
                    SysUsrModifiedon = x.SysUsrModifiedon,
                    SysUsrModifiedby = x.SysUsrModifiedby,
                    AccUid = x.AccUid,
                    AccRateUid = x.AccRateUid,
                    AccClbNationalCode = x.AccClbNationalCode,
                    AccClbPostalCode = x.AccClbPostalCode,
                    AccClbPhone1 = x.AccClbPhone1,
                    AccClbMobile = x.AccClbMobile,
                    AccClbParentUid = x.AccClbParentUid,
                    AccClbTypUid = x.AccClbTypUid,
                    AccClbSex = x.AccClbSex,
                    AccClbAddress = x.AccClbAddress,
                    AccClbDescribtion = x.AccClbDescribtion,
                    AccClbClubCard = x.AccClbClubCard,
                    AccFrJob = x.AccFrJob,
                    AccFrContract = x.AccFrContract,
                    AccCardSerial = x.AccCardSerial,
                    AccRelationType = x.AccRelationType,
                }).ToList();
      
       
        }

        public JsonResult GetAllAccountClub(JqueryDatatableParam param)
        {

            var list = _complexContext.AccountClubs.Include(x => x.AccClbTypU).AsNoTracking();

            if (!string.IsNullOrEmpty(param.SSearch))
            {
                list = list.Where(x =>
                    x.AccClbName.ToLower().Contains(param.SSearch.ToLower())
                    || x.AccClbCode.ToLower().Contains(param.SSearch.ToLower())
                    || x.AccClbMobile.ToLower().Contains(param.SSearch.ToLower()));
            }

            var sortColumnIndex = Convert.ToInt32(_contextAccessor.HttpContext.Request.Query["iSortCol_0"]);
            var sortDirection = _contextAccessor.HttpContext.Request.Query["sSortDir_0"];

            switch (sortColumnIndex)
            {
                case 0:
                    list = sortDirection == "asc" ? list.OrderBy(c => c.AccClbName) : list.OrderByDescending(c => c.AccClbName);
                    break;
                case 1:
                    list = sortDirection == "asc" ? list.OrderBy(c => c.AccClbCode) : list.OrderByDescending(c => c.AccClbCode);
                    break;
                case 2:
                    list = sortDirection == "asc" ? list.OrderBy(c => c.AccClbBrithday) : list.OrderByDescending(c => c.AccClbBrithday);
                    break;
                case 5:
                    list = sortDirection == "asc" ? list.OrderBy(c => c.AccClbMobile) : list.OrderByDescending(c => c.AccClbMobile);
                    break;


                default:
                    {
                        string OrderingFunction(Domain.ComplexModels.AccountClub e) => sortColumnIndex == 0 ? e.AccClbName : "";
                        IOrderedEnumerable<Domain.ComplexModels.AccountClub> rr = null;

                        rr = sortDirection == "asc"
                            ? list.AsEnumerable().OrderBy((Func<Domain.ComplexModels.AccountClub, string>)OrderingFunction)
                            : list.AsEnumerable().OrderByDescending((Func<Domain.ComplexModels.AccountClub, string>)OrderingFunction);

                        list = rr.AsQueryable();
                        break;
                    }
            }

            IQueryable<AccountClub> displayResult;
            if (param.IDisplayLength != 0)
                displayResult = list.Skip(param.IDisplayStart)
                .Take(param.IDisplayLength);
            else displayResult = list;
            var totalRecords = list.Count();
           

            var result = (new
            {
                param.SEcho,
                iTotalRecords = totalRecords,
                iTotalDisplayRecords = totalRecords,
                aaData = list
            });
            return new JsonResult(result, new JsonSerializerOptions { PropertyNamingPolicy = null });
        }


        public AccountClubVM GetById(int id)
        {
            throw new NotImplementedException();
        }

        public ResultDto Insert(AccountClubVM accountClubVM)
        {
            throw new NotImplementedException();
        }

        public ResultDto Update(AccountClubVM accountClubVM)
        {
            throw new NotImplementedException();
        }
    }
}
