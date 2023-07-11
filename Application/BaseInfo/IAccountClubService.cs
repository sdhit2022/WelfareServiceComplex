using Application.BaseData;
using Application.BaseData.Dto;
using Application.Common;
using Application.Interfaces.Context;
using Application.Product;
using Application.Product.ProductDto;
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
using ILogger = Microsoft.Build.Framework.ILogger;


namespace Application.BaseInfo
{
    public interface IAccountClubService
    {
    //    List<AccountClubVM> GetAll();
    //    JsonResult GetAllAccountClub(JqueryDatatableParam param);

    //    ResultDto Update(AccountClubVM accountClubVM);
    //    ResultDto Delete(int id);
    //    AccountClubVM GetById(int id);
    //    ResultDto Insert(AccountClubVM accountClubVM);
    //    EditAccountClub GetDetailsAccountClub(Guid id);
    //}

    //internal class AccountClubService : IAccountClubService
    //{
    //    private readonly IComplexContext _complexContext;
    //    private readonly IProductService _productService;
    //    private readonly ILogger<AccountClubService> _logger;
    //    private readonly IMapper _mapper;
    //    private readonly IHttpContextAccessor _contextAccessor;

    //    public AccountClubService(IComplexContext complexContext,
    //        IProductService productService,ILogger<AccountClubService> logger,IHttpContextAccessor contextAccessor,IMapper mapper)
    //    {
    //        _complexContext= complexContext;
    //        _productService= productService;
    //        _contextAccessor=contextAccessor;
    //         _logger = logger;
    //        _mapper= mapper;
    //    }
    //    public ResultDto Delete(int id)
    //    {
    //        throw new NotImplementedException();
    //    }

    //    public List<AccountClubVM> GetAll()
    //    {
    //        return _complexContext.AccountClubs.AsNoTracking().Include(x => x.AccFrJob)
    //            .Include(x => x.AccFrContract).Select(
    //            x => new
    //            {
    //                x.AccClbUid,
    //                x.AccClbBrithday,
    //                x.AccClbName,
    //                x.AccClbCode,
    //                x.AccClbStatus,
    //                x.SysUsrCreatedon,
    //                x.SysUsrCreatedby,
    //                x.SysUsrModifiedon,
    //                x.SysUsrModifiedby,
    //                x.AccUid,
    //                x.AccRateUid,
    //                x.AccClbNationalCode,
    //                x.AccClbPostalCode,
    //                x.AccClbPhone1,
    //                x.AccClbMobile,
    //                x.AccClbParentUid,
    //                x.AccClbTypUid,
    //                x.AccClbSex,
    //                x.AccClbAddress,
    //                x.AccClbDescribtion,
    //                x.AccClbClubCard,
    //                x.AccFrJob,
    //                x.AccFrContract,
    //                x.AccCardSerial,
    //                x.AccRelationType,
    //            }
    //            ).Select(x => new AccountClubVM
    //            {
    //                AccClbUid = x.AccClbUid,
    //                AccClbBrithday = x.AccClbBrithday,
    //                AccClbName = x.AccClbName,
    //                AccClbCode = x.AccClbCode,
    //                AccClbStatus = x.AccClbStatus,
    //                SysUsrCreatedon = x.SysUsrCreatedon,
    //                SysUsrCreatedby = x.SysUsrCreatedby,
    //                SysUsrModifiedon = x.SysUsrModifiedon,
    //                SysUsrModifiedby = x.SysUsrModifiedby,
    //                AccUid = x.AccUid,
    //                AccRateUid = x.AccRateUid,
    //                AccClbNationalCode = x.AccClbNationalCode,
    //                AccClbPostalCode = x.AccClbPostalCode,
    //                AccClbPhone1 = x.AccClbPhone1,
    //                AccClbMobile = x.AccClbMobile,
    //                AccClbParentUid = x.AccClbParentUid,
    //                AccClbTypUid = x.AccClbTypUid,
    //                AccClbSex = x.AccClbSex,
    //                AccClbAddress = x.AccClbAddress,
    //                AccClbDescribtion = x.AccClbDescribtion,
    //                AccClbClubCard = x.AccClbClubCard,
    //                AccFrJob = x.AccFrJob,
    //                AccFrContract = x.AccFrContract,
    //                AccCardSerial = x.AccCardSerial,
    //                AccRelationType = x.AccRelationType,
    //            }).ToList();
      
       
    //    }

    //    public JsonResult GetAllAccountClub(JqueryDatatableParam param)
    //    {
    //        var list = _complexContext.AccountClubs.Include(x => x.AccClbTypU).Include(x=>x.AccFrJob).AsNoTracking()
    //            .Select(x => new
    //        {
    //                x.AccClbUid,
    //                x.AccClbBrithday,
    //                x.AccClbName,
    //                x.AccClbCode,
    //                x.AccClbStatus,
    //                x.SysUsrCreatedon,
    //                x.SysUsrCreatedby,
    //                x.SysUsrModifiedon,
    //                x.SysUsrModifiedby,
    //                x.AccUid,
    //                x.AccRateUid,
    //                x.AccClbNationalCode,
    //                x.AccClbPostalCode,
    //                x.AccClbPhone1,
    //                x.AccClbMobile,
    //                x.AccClbParentUid,
    //                x.AccClbTypUid,
    //                x.AccClbSex,
    //                x.AccClbAddress,
    //                x.AccClbDescribtion,
    //                x.AccClbClubCard,
    //                x.AccFrJob,
    //                x.AccFrContract,
    //                x.AccCardSerial,
    //                x.AccRelationType,
    //            }).Select(x => new AccountClubVM
    //            {
    //                AccClbUid = x.AccClbUid,
    //                AccClbBrithday = x.AccClbBrithday,
    //                AccClbName = x.AccClbName,
    //                AccClbCode = x.AccClbCode,
    //                AccClbStatus = x.AccClbStatus,
    //                SysUsrCreatedon = x.SysUsrCreatedon,
    //                SysUsrCreatedby = x.SysUsrCreatedby,
    //                SysUsrModifiedon = x.SysUsrModifiedon,
    //                SysUsrModifiedby = x.SysUsrModifiedby,
    //                AccUid = x.AccUid,
    //                AccRateUid = x.AccRateUid,
    //                AccClbNationalCode = x.AccClbNationalCode,
    //                AccClbPostalCode = x.AccClbPostalCode,
    //                AccClbPhone1 = x.AccClbPhone1,
    //                AccClbMobile = x.AccClbMobile,
    //                AccClbParentUid = x.AccClbParentUid,
    //                AccClbTypUid = x.AccClbTypUid,
    //                AccClbSex = x.AccClbSex,
    //                AccClbAddress = x.AccClbAddress,
    //                AccClbDescribtion = x.AccClbDescribtion,
    //                AccClbClubCard = x.AccClbClubCard,
    //                AccFrJob = x.AccFrJob,
    //                AccFrContract = x.AccFrContract,
    //                AccCardSerial = x.AccCardSerial,
    //                AccRelationType = x.AccRelationType,
    //                JobName=x.AccFrJob.JobName
    //            });


    //        var list1 = _complexContext.AccountClubs.Include(x => x.AccClbTypU).AsNoTracking().ToList();
    //      //  var list = _complexContext.AccountClubs.Include(x => x.AccClbTypU).AsNoTracking();

    //        if (!string.IsNullOrEmpty(param.SSearch))
    //        {
    //            list = list.Where(x =>
    //                x.AccClbName.ToLower().Contains(param.SSearch.ToLower())
    //                || x.AccClbCode.ToLower().Contains(param.SSearch.ToLower())
    //                || x.AccClbMobile.ToLower().Contains(param.SSearch.ToLower()));
    //        }

    //        var sortColumnIndex = Convert.ToInt32(_contextAccessor.HttpContext.Request.Query["iSortCol_0"]);
    //        var sortDirection = _contextAccessor.HttpContext.Request.Query["sSortDir_0"];

    //        switch (sortColumnIndex)
    //        {
    //            case 0:
    //                list = sortDirection == "asc" ? list.OrderBy(c => c.AccClbName) : list.OrderByDescending(c => c.AccClbName);
    //                break;
    //            case 1:
    //                list = sortDirection == "asc" ? list.OrderBy(c => c.AccClbCode) : list.OrderByDescending(c => c.AccClbCode);
    //                break;
    //            case 2:
    //                list = sortDirection == "asc" ? list.OrderBy(c => c.AccClbBrithday) : list.OrderByDescending(c => c.AccClbBrithday);
    //                break;
    //            case 5:
    //                list = sortDirection == "asc" ? list.OrderBy(c => c.AccClbMobile) : list.OrderByDescending(c => c.AccClbMobile);
    //                break;


    //            default:
    //                {
    //                    string OrderingFunction(AccountClubVM e) => sortColumnIndex == 0 ? e.AccClbName : "";
    //                    IOrderedEnumerable<AccountClubVM> rr = null;

    //                    rr = sortDirection == "asc"
    //                        ? list.AsEnumerable().OrderBy((Func<AccountClubVM, string>)OrderingFunction)
    //                        : list.AsEnumerable().OrderByDescending((Func<AccountClubVM, string>)OrderingFunction);

    //                    list = rr.AsQueryable();
    //                    break;
    //                }
    //        }

    //        IQueryable<AccountClubVM> displayResult;
    //        if (param.IDisplayLength != 0)
    //            displayResult = list.Skip(param.IDisplayStart)
    //            .Take(param.IDisplayLength);
    //        else displayResult = list;
    //        var totalRecords = list.Count();
            

    //        var result = (new
    //        {
    //            param.SEcho,
    //            iTotalRecords = totalRecords,
    //            iTotalDisplayRecords = totalRecords,
    //            aaData = displayResult
    //        });
    //        return new JsonResult(result, new JsonSerializerOptions { PropertyNamingPolicy = null });
    //    }


    //    public AccountClubVM GetById(int id)
    //    {
    //        throw new NotImplementedException();
    //    }

    //    public ResultDto Insert(AccountClubVM accountClubVM)
    //    {
    //        throw new NotImplementedException();
    //    }

    //    public ResultDto Update(AccountClubVM accountClubVM)
    //    {
    //        throw new NotImplementedException();
    //    }

    //    public List<AccountSelectOption> GetSelectOptionAccounts()
    //    {
    //        var account = _complexContext.AccountClubs.Select(x => new { x.AccClbName, x.AccClbUid }).AsNoTracking().Select(x => new AccountSelectOption()
    //        {
    //            Id = x.AccClbUid,
    //            Name = x.AccClbName
    //        }).ToList();
    //        return account;
    //    }


    //    public List<AccountClubType> GetSelectOptionClubTypes()
    //    {
    //        var account = _complexContext.AccountClubTypes.Select(x => new { x.AccClbTypName, x.AccClbTypUid }).AsNoTracking().Select(x => new AccountClubType()
    //        {
    //            AccClbTypUid = x.AccClbTypUid,
    //            AccClbTypName = x.AccClbTypName
    //        }).ToList();
    //        return account;
    //    }

    //    public List<AccountRating> GetSelectOptionRatings()
    //    {
    //        var account = _complexContext.AccountRatings.Select(x => new { x.AccRateName, x.AccRateUid }).AsNoTracking().Select(x => new AccountRating()
    //        {
    //            AccRateUid = x.AccRateUid,
    //            AccRateName = x.AccRateName
    //        }).ToList();
    //        return account;
    //    }


    //    public List<SelectListOption> SelectOptionState()
    //    {
    //        return _complexContext.States.Select(x => new { x.SttName, x.SttUid }).Select(x => new SelectListOption() { Value = x.SttUid, Text = x.SttName }).AsNoTracking().ToList();
    //    }

    //    public List<SelectListOption> SelectOptionCities(Guid stateId)
    //    {
    //        return _complexContext.Cities.Where(x => x.SttUid == stateId).Select(x => new { x.CityName, x.CityUid }).Select(x => new SelectListOption() { Value = x.CityUid, Text = x.CityName }).AsNoTracking().ToList();
    //    }
    //    public EditAccountClub GetDetailsAccountClub(Guid id)
    //    {
    //        var accClub = _complexContext.AccountClubs.Find(id);
    //        if (accClub != null)
    //        {
    //            var map = _mapper.Map<EditAccountClub>(accClub);
    //            map.Account = this.GetSelectOptionAccounts();
    //            map.ClupType = this.GetSelectOptionClubTypes();
    //            map.Rating = this.GetSelectOptionRatings();
    //            map.States = this.SelectOptionState();
    //            map.SateUid = _complexContext.Cities.Include(x => x.SttU)
    //                .SingleOrDefault(x => x.CityUid == accClub.CityUid)?.SttUid;

    //            map.Cities = SelectOptionCities(map.SateUid ?? Guid.Empty);
    //            return map;
    //        }
    //        _logger.LogError($"هیچ رکوردی با این شناسه {id} یافت نشد");
    //        throw new NullReferenceException("عملیات با خطا مواجه شد لطفا با پشتیبانی تماس بگیرید.");

    //    }
    }
}
