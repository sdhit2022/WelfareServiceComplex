﻿using System.Data;
using Application.BaseData.Dto;
using Application.Common;
using Application.Interfaces.Context;
using AutoMapper;
using Domain.ComplexModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Text.Json;
using ILogger = Microsoft.Build.Framework.ILogger;
using Application.Product;
using System.Linq;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;


namespace Application.BaseData
{
    public interface IBaseDataService
    {
        JsonResult GetAllUnit(JqueryDatatableParam param);
        ResultDto CreateUnit(CreateUnit command);
        ResultDto<List<UnitDto>> RemoveUnit(Guid id);
        ResultDto UpdateUnit(EditUnit command);

        JsonResult GetAllWareHouse(JqueryDatatableParam param);
        ResultDto CreateWareHouse(CreateWareHouse command);
        ResultDto RemoveWareHouse(Guid id);
        ResultDto UpdateWareHouse(UpdateWareHouse command);


        JsonResult GetAllContracts(JqueryDatatableParam param);
        List<SelectListOption> SelectOptionContract();
        ResultDto CreateContract(ContractDto contract);
        ContractDto GetContract(Guid id);
        bool GetContractByName(string name);
        bool CheckContractNameExists(string name, Guid id);
       List<ContractDetail> GetContractDetails(Guid id);
        ResultDto InsertProductContract(List<ContractDetail> contractDetails);
        ResultDto RemoveContract(Guid id);
        ResultDto UpdateContract(ContractDto contract);

        JsonResult GetAllAccountClupType(JqueryDatatableParam param);
        ResultDto CreateAccountClubType(CreateAccountClubType command);
        ResultDto RemoveAccountClubType(Guid id);
        ResultDto UpdateAccountClubType(UpdateAccountClubType command);

        JsonResult GetAllAccountRating(JqueryDatatableParam param);
        ResultDto CreateAccountRating(CreateAccountRating command);
        ResultDto RemoveAccountRating(Guid id);
        ResultDto UpdateAccountRating(UpdateAccountRating command);


        List<AccountSelectOption> GetSelectOptionAccounts();
        List<AccountClubType> GetSelectOptionClubTypes();
        List<AccountRating> GetSelectOptionRatings();
        List<SelectListOption> SelectOptionCities(Guid stateId);
        List<SelectListOption> SelectOptionState();
        List<SelectListOptionInt> SelectOptionJob();

        ResultDto CreateAccountClub(CreateAccountClub command);
        ResultDto UpdateAccountClub(EditAccountClub command);
        EditAccountClub GetDetailsAccountClub(Guid id);
        JsonResult GetAllAccountClub(JqueryDatatableParam param);
        ResultDto RemoveAccountClub(Guid id);
        JsonResult GetAllAccountClubProduct(JqueryDatatableParam param, Guid productId);
    }
    internal class BaseDataService : IBaseDataService
    {
        private readonly IComplexContext _complexContext;
        private readonly IProductService _productService;
        private readonly ILogger<BaseDataService> _logger;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _contextAccessor;
        public BaseDataService(IComplexContext complexContext, ILogger<BaseDataService> logger, IMapper mapper, IHttpContextAccessor contextAccessor, IProductService productService)
        {
            _complexContext = complexContext;
            _logger = logger;
            _mapper = mapper;
            _contextAccessor = contextAccessor;
            _productService = productService;
        }

        #region Unit Of Measurements

        public JsonResult GetAllUnit(JqueryDatatableParam param)
        {

            var list = _complexContext.UnitOfMeasurements.AsNoTracking();

            if (!string.IsNullOrEmpty(param.SSearch))
                list = list.Where(x => x.UomName.ToLower().Contains(param.SSearch.ToLower()));

            var sortColumnIndex = Convert.ToInt32(_contextAccessor.HttpContext.Request.Query["iSortCol_0"]);
            var sortDirection = _contextAccessor.HttpContext.Request.Query["sSortDir_0"];

            switch (sortColumnIndex)
            {
                case 3:
                    list = sortDirection == "asc" ? list.OrderBy(c => c.UomCode) : list.OrderByDescending(c => c.UomCode);
                    break;
                case 4:
                    list = sortDirection == "asc" ? list.OrderBy(c => c.UomName) : list.OrderByDescending(c => c.UomName);
                    break;
                default:
                    {
                        string OrderingFunction(UnitOfMeasurement e) => sortColumnIndex == 0 ? e.UomName : e.UomCode;
                        IOrderedEnumerable<UnitOfMeasurement> rr = null;
                        rr = sortDirection == "asc" ? list.AsEnumerable().OrderBy((Func<UnitOfMeasurement, string>)OrderingFunction) : list.AsEnumerable().OrderByDescending((Func<UnitOfMeasurement, string>)OrderingFunction);

                        list = rr.AsQueryable();
                        break;
                    }
            }

            IQueryable<UnitOfMeasurement> displayResult;
            if (param.IDisplayLength != 0)
                displayResult = list.Skip(param.IDisplayStart)
                .Take(param.IDisplayLength);
            else displayResult = list;
            var totalRecords = list.Count();
            var map = _mapper.Map<List<UnitDto>>(displayResult.ToList());

            var result = (new
            {
                param.SEcho,
                iTotalRecords = totalRecords,
                iTotalDisplayRecords = totalRecords,
                aaData = map
            });
            return new JsonResult(result, new JsonSerializerOptions { PropertyNamingPolicy = null });
        }
        public ResultDto CreateUnit(CreateUnit command)
        {
            var result = new ResultDto();
            try
            {
                if (_complexContext.UnitOfMeasurements.Any(x => x.UomName == command.Name.Fix()))
                    return result.Failed(ValidateMessage.DuplicateName);

                if (_complexContext.UnitOfMeasurements.Any(x => x.UomCode == command.Code.Fix()))
                    return result.Failed(ValidateMessage.DuplicateCode);


                var unit = _mapper.Map<UnitOfMeasurement>(command);
                _complexContext.UnitOfMeasurements.Add(unit);
                _complexContext.SaveChanges();
                return result.Succeeded();
            }
            catch (Exception exception)
            {
                _logger.LogError($"هنگام ثبت واحد شمارش خطای زیر رخ داد {exception}");
                return result.Failed("هنگام ثبت عملیات خطای رخ داد");
            }
        }

        public ResultDto UpdateUnit(EditUnit command)
        {
            var result = new ResultDto();
            try
            {
                var unit = _complexContext.UnitOfMeasurements.Find(command.Id);
                if (unit == null)
                {
                    _logger.LogWarning($"Don't Find Any Record With Id {command.Id} On Table UnitOfMeasurements");
                    return result.Failed("خطای رخ داد، لطفا با پشتیبانی تماس بگرید");
                }

                if (_complexContext.UnitOfMeasurements.Any(x => x.UomName == command.Name.Fix() && x.UomUid != command.Id))
                    return result.Failed(ValidateMessage.DuplicateName);
                var addUnit = _mapper.Map(command, unit);
                _complexContext.UnitOfMeasurements.Update(addUnit);
                _complexContext.SaveChanges();
                return result.Succeeded();
            }
            catch (Exception exception)
            {
                _logger.LogError($"هنگام ویرایش واحد شمارش خطای زیر رخ داد {exception}");
                return result.Failed("هنگام ثبت عملیات خطای رخ داد");
            }
        }


        public ResultDto<List<UnitDto>> RemoveUnit(Guid id)
        {
            var result = new ResultDto<List<UnitDto>>();
            try
            {
                var unit = _complexContext.UnitOfMeasurements.Find(id);
                if (unit == null)
                {
                    _logger.LogWarning($"Don't Find Any Record With Id {id} On Table UnitOfMeasurements");
                    return result.Failed("خطای رخ داد، لطفا با پشتیبانی تماس بگرید");
                }

                _complexContext.UnitOfMeasurements.Remove(unit);
                _complexContext.SaveChanges();
                return result.Succeeded(null);
            }

            catch (DbUpdateException ex)
            {
                return result.Failed("این رکورد در جا های دیگری از برنامه استفاده شده و قابل حذف نمیباشد.");
            }
            catch (Exception exception)
            {
                _logger.LogError($"هنگام حذف واحد شمارش خطای زیر رخ داد {exception}");
                return result.Failed("هنگام ثبت عملیات خطای رخ داد");
            }
        }

        #endregion

        #region Unit Of WareHouse


        public JsonResult GetAllWareHouse(JqueryDatatableParam param)
        {

            var list = _complexContext.WareHouses.AsNoTracking();

            if (!string.IsNullOrEmpty(param.SSearch))
                list = list.Where(x => x.WarHosName.ToLower().Contains(param.SSearch.ToLower()));

            var sortColumnIndex = Convert.ToInt32(_contextAccessor.HttpContext.Request.Query["iSortCol_0"]);
            var sortDirection = _contextAccessor.HttpContext.Request.Query["sSortDir_0"];

            switch (sortColumnIndex)
            {
                case 3:
                    list = sortDirection == "asc" ? list.OrderBy(c => c.WarHosCode) : list.OrderByDescending(c => c.WarHosCode);
                    break;
                case 4:
                    list = sortDirection == "asc" ? list.OrderBy(c => c.WarHosName) : list.OrderByDescending(c => c.WarHosName);
                    break;
                default:
                    {
                        string OrderingFunction(WareHouse e) => sortColumnIndex == 0 ? e.WarHosName : e.WarHosCode;
                        IOrderedEnumerable<WareHouse> rr = null;
                        rr = sortDirection == "asc" ? list.AsEnumerable().OrderBy((Func<WareHouse, string>)OrderingFunction) : list.AsEnumerable().OrderByDescending((Func<WareHouse, string>)OrderingFunction);

                        list = rr.AsQueryable();
                        break;
                    }
            }

            IQueryable<WareHouse> displayResult;
            if (param.IDisplayLength != 0)
                displayResult = list.Skip(param.IDisplayStart)
                .Take(param.IDisplayLength);
            else displayResult = list;
            var totalRecords = list.Count();
            var map = _mapper.Map<List<WareHouseDto>>(displayResult.ToList());

            var result = (new
            {
                param.SEcho,
                iTotalRecords = totalRecords,
                iTotalDisplayRecords = totalRecords,
                aaData = map
            });
            return new JsonResult(result, new JsonSerializerOptions { PropertyNamingPolicy = null });
        }

        public ResultDto CreateWareHouse(CreateWareHouse command)
        {
            var result = new ResultDto();
            try
            {
                if (_complexContext.WareHouses.Any(x => x.WarHosName == command.Name.Fix()))
                    return result.Failed(ValidateMessage.DuplicateName);

                if (_complexContext.WareHouses.Any(x => x.WarHosCode == command.Code.Fix()))
                    return result.Failed(ValidateMessage.DuplicateCode);

                var unit = _mapper.Map<WareHouse>(command);
                _complexContext.WareHouses.Add(unit);
                _complexContext.SaveChanges();
                return result.Succeeded();
            }
            catch (Exception exception)
            {
                _logger.LogError($"هنگام ثبت واحد شمارش خطای زیر رخ داد {exception}");
                return result.Failed("هنگام ثبت عملیات خطای رخ داد");
            }
        }

        public ResultDto UpdateWareHouse(UpdateWareHouse command)
        {
            var result = new ResultDto();
            try
            {
                var wareHouse = _complexContext.WareHouses.Find(command.Id);
                if (wareHouse == null)
                {
                    _logger.LogWarning($"Don't Find Any Record With Id {command.Id} On Table WareHouse");
                    return result.Failed("خطای رخ داد، لطفا با پشتیبانی تماس بگرید");
                }

                if (_complexContext.WareHouses.Any(x => x.WarHosName == command.Name.Fix() && x.WarHosUid != command.Id))
                    return result.Failed(ValidateMessage.DuplicateName);

                if (_complexContext.WareHouses.Any(x => x.WarHosCode == command.Code.Fix() && x.WarHosUid != command.Id))
                    return result.Failed(ValidateMessage.DuplicateCode);


                var map = _mapper.Map(command, wareHouse);
                _complexContext.WareHouses.Update(map);
                _complexContext.SaveChanges();
                return result.Succeeded();
            }
            catch (Exception exception)
            {
                _logger.LogError($"هنگام ویرایش واحد شمارش خطای زیر رخ داد {exception}");
                return result.Failed("هنگام ثبت عملیات خطای رخ داد");
            }
        }


        public ResultDto RemoveWareHouse(Guid id)
        {
            var result = new ResultDto();
            try
            {
                var unit = _complexContext.WareHouses.Find(id);
                if (unit == null)
                {
                    _logger.LogWarning($"Don't Find Any Record With Id {id} On Table WareHouse");
                    return result.Failed("خطای رخ داد، لطفا با پشتیبانی تماس بگرید");
                }
                if(_complexContext.Salons.Any(x=>x.FrWarHosUid== unit.WarHosUid))
                {
                    return result.Failed("انبار به سالن تخصیص داده شده است و نمیتوان آن را حذف کرد");
                }
                _complexContext.WareHouses.Remove(unit);
                _complexContext.SaveChanges();
                return result.Succeeded();
            }
            catch (Exception exception)
            {
                _logger.LogError($"هنگام حذف واحد شمارش خطای زیر رخ داد {exception}");
                return result.Failed("هنگام ثبت عملیات خطای رخ داد");
            }
        }
        #endregion
        #region Unit Of AccountClubType

        public JsonResult GetAllAccountClupType(JqueryDatatableParam param)
        {

            var list = _complexContext.AccountClubTypes.AsNoTracking();

            if (!string.IsNullOrEmpty(param.SSearch))
            {
                list = list.Where(x =>
                    x.AccClbTypName.ToLower().Contains(param.SSearch.ToLower()));

                if (!list.Any())
                    list = list.Where(x => x.AccClbTypPercentDiscount.ToString().ToLower().Contains(param.SSearch.Fix()));

                if (!list.Any())
                    list = list.Where(x => x.AccClbTypDetDiscount.ToString().ToLower().Contains(param.SSearch.Fix()));

                if (!list.Any())
                    list = list.Where(x => x.AccClbTypDefaultPriceInvoice.ToString().ToLower().Contains(param.SSearch.Fix()));

            }

            var sortColumnIndex = Convert.ToInt32(_contextAccessor.HttpContext.Request.Query["iSortCol_0"]);
            var sortDirection = _contextAccessor.HttpContext.Request.Query["sSortDir_0"];

            switch (sortColumnIndex)
            {
                case 3:
                    list = sortDirection == "asc" ? list.OrderBy(c => c.AccClbTypName) : list.OrderByDescending(c => c.AccClbTypName);
                    break;
                case 4:
                    list = sortDirection == "asc" ? list.OrderBy(c => c.AccClbTypDefaultPriceInvoice) : list.OrderByDescending(c => c.AccClbTypDefaultPriceInvoice);
                    break;

                case 5:
                    list = sortDirection == "asc" ? list.OrderBy(c => c.AccClbTypDiscountType) : list.OrderByDescending(c => c.AccClbTypDiscountType);
                    break;

                case 6:
                    list = sortDirection == "asc" ? list.OrderBy(c => c.AccClbTypDetDiscount) : list.OrderByDescending(c => c.AccClbTypDetDiscount);
                    break;


                default:
                    {
                        string OrderingFunction(AccountClubType e) => sortColumnIndex == 0 ? e.AccClbTypName : "";
                        IOrderedEnumerable<AccountClubType> rr = null;

                        rr = sortDirection == "asc"
                            ? list.AsEnumerable().OrderBy((Func<AccountClubType, string>)OrderingFunction)
                            : list.AsEnumerable().OrderByDescending((Func<AccountClubType, string>)OrderingFunction);

                        list = rr.AsQueryable();
                        break;
                    }
            }

            IQueryable<AccountClubType> displayResult;
            if (param.IDisplayLength != 0)
                displayResult = list.Skip(param.IDisplayStart)
                .Take(param.IDisplayLength);
            else displayResult = list;
            var totalRecords = list.Count();
            var map = _mapper.Map<List<AccountClubTypeDto>>(displayResult.ToList());

            foreach (var clubTypeDto in map)
            {
                clubTypeDto.DiscountTypeText = clubTypeDto.DiscountType switch
                {
                    "0" => "کسر از فاکتور",
                    "1" => "شارژ باشگاه",
                    _ => clubTypeDto.DiscountType
                };

                clubTypeDto.PriceInvoiceText = clubTypeDto.PriceInvoice switch
                {
                    0 => "صفر",
                    1 => "سطح 1",
                    2 => "سطح 2",
                    3 => "سطح 3",
                    4 => "سطح 4",
                    5 => "سطح 5",
                    _ => clubTypeDto.PriceInvoiceText
                };
            }

            var result = (new
            {
                param.SEcho,
                iTotalRecords = totalRecords,
                iTotalDisplayRecords = totalRecords,
                aaData = map
            });
            return new JsonResult(result, new JsonSerializerOptions { PropertyNamingPolicy = null });
        }





        public ResultDto CreateAccountClubType(CreateAccountClubType command)
        {
            var result = new ResultDto();
            try
            {
                if (_complexContext.AccountClubTypes.Any(x => x.AccClbTypName == command.Name.Fix()))
                    return result.Failed(ValidateMessage.DuplicateName);


                var account = _mapper.Map<AccountClubType>(command);
                _complexContext.AccountClubTypes.Add(account);
                _complexContext.SaveChanges();
                return result.Succeeded();
            }
            catch (Exception exception)
            {
                _logger.LogError($"هنگام ثبت واحد شمارش خطای زیر رخ داد {exception}");
                return result.Failed("هنگام ثبت عملیات خطای رخ داد");
            }
        }

        public ResultDto UpdateAccountClubType(UpdateAccountClubType command)
        {
            var result = new ResultDto();
            try
            {
                var account = _complexContext.AccountClubTypes.Find(command.Id);
                if (account == null)
                {
                    _logger.LogWarning($"Don't Find Any Record With Id {command.Id} On Table AccountClubType");
                    return result.Failed("خطای رخ داد، لطفا با پشتیبانی تماس بگرید");
                }

                if (_complexContext.AccountClubTypes.Any(x => x.AccClbTypName == command.Name.Fix() && x.AccClbTypUid != command.Id))
                    return result.Failed(ValidateMessage.DuplicateName);
                var map = _mapper.Map(command, account);
                _complexContext.AccountClubTypes.Update(map);
                _complexContext.SaveChanges();
                return result.Succeeded();
            }
            catch (Exception exception)
            {
                _logger.LogError($"هنگام ویرایش واحد شمارش خطای زیر رخ داد {exception}");
                return result.Failed("هنگام ثبت عملیات خطای رخ داد");
            }
        }


        public ResultDto RemoveAccountClubType(Guid id)
        {
            var result = new ResultDto();
            try
            {
                var unit = _complexContext.AccountClubTypes.Find(id);
                if (unit == null)
                {
                    _logger.LogWarning($"Don't Find Any Record With Id {id} On Table AccountClubType");
                    return result.Failed("خطای رخ داد، لطفا با پشتیبانی تماس بگرید");
                }

                _complexContext.AccountClubTypes.Remove(unit);
                _complexContext.SaveChanges();
                return result.Succeeded();
            }
            catch (Exception exception)
            {
                _logger.LogError($"هنگام حذف واحد شمارش خطای زیر رخ داد {exception}");
                return result.Failed("هنگام ثبت عملیات خطای رخ داد");
            }
        }
        #endregion

        #region  AccountRating


        public JsonResult GetAllAccountRating(JqueryDatatableParam param)
        {

            var list = _complexContext.AccountRatings.AsNoTracking();

            if (!string.IsNullOrEmpty(param.SSearch))
                list = list.Where(x => x.AccRateName.ToLower().Contains(param.SSearch.ToLower()));

            var sortColumnIndex = Convert.ToInt32(_contextAccessor.HttpContext.Request.Query["iSortCol_0"]);
            var sortDirection = _contextAccessor.HttpContext.Request.Query["sSortDir_0"];

            switch (sortColumnIndex)
            {
                case 3:
                    list = sortDirection == "asc" ? list.OrderBy(c => c.AccRateName) : list.OrderByDescending(c => c.AccRateName);
                    break;

                default:
                    {
                        string OrderingFunction(AccountRating e) => sortColumnIndex == 0 ? e.AccRateName : "";
                        IOrderedEnumerable<AccountRating> rr = null;
                        rr = sortDirection == "asc" ? list.AsEnumerable().OrderBy((Func<AccountRating, string>)OrderingFunction) : list.AsEnumerable().OrderByDescending((Func<AccountRating, string>)OrderingFunction);

                        list = rr.AsQueryable();
                        break;
                    }
            }

            IQueryable<AccountRating> displayResult;
            if (param.IDisplayLength != 0)
                displayResult = list.Skip(param.IDisplayStart)
                .Take(param.IDisplayLength);
            else displayResult = list;
            var totalRecords = list.Count();
            var map = _mapper.Map<List<AccountRatingDto>>(displayResult.ToList());

            var result = (new
            {
                param.SEcho,
                iTotalRecords = totalRecords,
                iTotalDisplayRecords = totalRecords,
                aaData = map
            });
            return new JsonResult(result, new JsonSerializerOptions { PropertyNamingPolicy = null });
        }

        public ResultDto CreateAccountRating(CreateAccountRating command)
        {
            var result = new ResultDto();
            try
            {
                if (_complexContext.AccountRatings.Any(x => x.AccRateName == command.Name.Fix()))
                    return result.Failed(ValidateMessage.DuplicateName);


                var unit = _mapper.Map<AccountRating>(command);
                _complexContext.AccountRatings.Add(unit);
                _complexContext.SaveChanges();
                return result.Succeeded();
            }
            catch (Exception exception)
            {
                _logger.LogError($"هنگام ثبت رتبه بندی مشترکین خطای زیر رخ داد {exception}");
                return result.Failed("هنگام ثبت عملیات خطای رخ داد");
            }
        }

        public ResultDto UpdateAccountRating(UpdateAccountRating command)
        {
            var result = new ResultDto();
            try
            {
                var AccountRating = _complexContext.AccountRatings.Find(command.Id);
                if (AccountRating == null)
                {
                    _logger.LogWarning($"Don't Find Any Record With Id {command.Id} On Table AccountRating");
                    return result.Failed("خطای رخ داد، لطفا با پشتیبانی تماس بگرید");
                }

                if (_complexContext.AccountRatings.Any(x => x.AccRateName == command.Name.Fix() && x.AccRateUid != command.Id))
                    return result.Failed(ValidateMessage.DuplicateName);

                var map = _mapper.Map(command, AccountRating);
                _complexContext.AccountRatings.Update(map);
                _complexContext.SaveChanges();
                return result.Succeeded();
            }
            catch (Exception exception)
            {
                _logger.LogError($"هنگام ویرایش رتبه بندی مشترکین خطای زیر رخ داد {exception}");
                return result.Failed("هنگام ثبت عملیات خطای رخ داد");
            }
        }


        public ResultDto RemoveAccountRating(Guid id)
        {
            var result = new ResultDto();
            try
            {
                var unit = _complexContext.AccountRatings.Find(id);
                if (unit == null)
                {
                    _logger.LogWarning($"Don't Find Any Record With Id {id} On Table AccountRating");
                    return result.Failed("خطای رخ داد، لطفا با پشتیبانی تماس بگرید");
                }

                _complexContext.AccountRatings.Remove(unit);
                _complexContext.SaveChanges();
                return result.Succeeded();
            }
            catch (Exception exception)
            {
                _logger.LogError($"هنگام حذف ربته بندی مشترکین خطای زیر رخ داد {exception}");
                return result.Failed("هنگام ثبت عملیات خطای رخ داد");
            }
        }

        #endregion

        #region Account Club
        public JsonResult GetAllAccountClub(JqueryDatatableParam param)
        {

            var list = _complexContext.AccountClubs.Include(x => x.AccClbTypU).
                Include(x=>x.AccFrJobNavigation)
                .AsNoTracking();

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
            List<AccountClubDto> map;

            try
            {
                map = _mapper.Map<List<AccountClubDto>>(displayResult);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }


            foreach (var clubTypeDto in map)
            {
                clubTypeDto.AccClbSexText = clubTypeDto.AccClbSex switch
                {
                    1 => "مرد",
                    0 => "زن",
                    _ => clubTypeDto.AccClbSexText
                };


                clubTypeDto.AccTypePriceLevelText = clubTypeDto.AccTypePriceLevel switch
                {
                    null => string.Empty,
                    0 => "صفر",
                    1 => "سطح 1",
                    2 => "سطح 2",
                    3 => "سطح 3",
                    4 => "سطح 4",
                    5 => "سطح 5",
                    _ => throw new ArgumentOutOfRangeException()
                };

                if (clubTypeDto.AccClbTypUid != null)
                {
                    var accType = _complexContext.AccountClubTypes.Find(clubTypeDto.AccClbTypUid);
                    if (accType != null)
                    {
                        clubTypeDto.AccClubType = accType.AccClbTypName;
                        clubTypeDto.AccClubDiscount = accType.AccClbTypDetDiscount ?? 0;
                    }

                }

                if (clubTypeDto.AccRateUid != null)
                    clubTypeDto.AccRatioText = _complexContext.AccountRatings.Find(clubTypeDto.AccRateUid)?.AccRateName;

            }

            var result = (new
            {
                param.SEcho,
                iTotalRecords = totalRecords,
                iTotalDisplayRecords = totalRecords,
                aaData = map
            });
            return new JsonResult(result, new JsonSerializerOptions { PropertyNamingPolicy = null });
        }

        public JsonResult GetAllAccountClubProduct(JqueryDatatableParam param, Guid productId)
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
            var map = _mapper.Map<List<AccountClubDto>>(displayResult.ToList());



            foreach (var clubTypeDto in map)
            {

                var discount = Convert.ToDouble(_productService.CalculateDiscount(productId, clubTypeDto.AccClbTypUid,
                    clubTypeDto.AccTypePriceLevel ?? 0));
                // clubTypeDto.AccClubType = accType.AccClbTypName;
                clubTypeDto.AccClubDiscount = discount;

                if (clubTypeDto.AccRateUid != null)
                    clubTypeDto.AccRatioText = _complexContext.AccountRatings.Find(clubTypeDto.AccRateUid)?.AccRateName;

            }

            var result = (new
            {
                param.SEcho,
                iTotalRecords = totalRecords,
                iTotalDisplayRecords = totalRecords,
                aaData = map
            });
            return new JsonResult(result, new JsonSerializerOptions { PropertyNamingPolicy = null });
        }

        public ResultDto CreateAccountClub(CreateAccountClub command)
        {
            var result = new ResultDto();
            try
            {
                if (_complexContext.AccountClubs.Any(x => x.AccClbName == command.AccClbName.Fix()))
                    return result.Failed(ValidateMessage.DuplicateName);

                if (_complexContext.AccountClubs.Any(x => x.AccClbCode == command.AccClbCode.Fix()))
                    return result.Failed(ValidateMessage.DuplicateCode);

                if (command.AccClbMobile != "1" && _complexContext.AccountClubs.Any(x => x.AccClbMobile == command.AccClbMobile.Fix() && x.AccClbMobile != "1"))
                    return result.Failed(ValidateMessage.DuplicateMobile);


                var map = _mapper.Map<AccountClub>(command);
                _complexContext.AccountClubs.Add(map);
                _complexContext.SaveChanges();
                return result.Succeeded();

            }
            catch (Exception e)
            {
                _logger.LogError($"حین ثبت کردن مشترک خطای زیر رخ داد {e}");
                return result.Failed("عملیات با خطا مواجه شد، لطفا با پشتیبانی تماس بگیرید.");
            }
        }


        public ResultDto UpdateAccountClub(EditAccountClub command)
        {
            var result = new ResultDto();
            try
            {
                var accClub = _complexContext.AccountClubs.Find(command.AccClbUid);
                if (accClub == null)
                {
                    _logger.LogError($"هیچ رکوردی با این شناسه {command.AccClbUid} یافت نشد");
                    return result.Failed("عملیات با خطا مواجه شد لطفا با پشتیبانی تماس بگیرید.");
                }

                if (_complexContext.AccountClubs.Any(x => x.AccClbName == command.AccClbName.Fix() && x.AccClbUid != command.AccClbUid))
                    return result.Failed(ValidateMessage.DuplicateName);

                if (_complexContext.AccountClubs.Any(x => x.AccClbCode == command.AccClbCode.Fix() && x.AccClbUid != command.AccClbUid))
                    return result.Failed(ValidateMessage.DuplicateCode);

                if (_complexContext.AccountClubs.Any(x => x.AccClbMobile == command.AccClbMobile.Fix() && x.AccClbUid != command.AccClbUid))
                    return result.Failed(ValidateMessage.DuplicateMobile);

                var map = _mapper.Map(command, accClub);
                _complexContext.AccountClubs.Update(map);
                _complexContext.SaveChanges();
                return result.Succeeded();
            }
            catch (Exception e)
            {
                _logger.LogError($"حین ثبت کردن مشترک خطای زیر رخ داد {e}");
                return result.Failed("عملیات با خطا مواجه شد، لطفا با پشتیبانی تماس بگیرید.");
            }
        }



        public ResultDto RemoveAccountClub(Guid id)
        {
            var result = new ResultDto();
            try
            {
                var accountClub = _complexContext.AccountClubs.Find(id);
                if (accountClub == null)
                {
                    _logger.LogWarning($"Don't Find Any Record With Id {id} On Table AccountClub");
                    return result.Failed("خطای رخ داد، لطفا با پشتیبانی تماس بگرید");
                }

                _complexContext.AccountClubs.Remove(accountClub);
                _complexContext.SaveChanges();
                return result.Succeeded();
            }
            catch (Exception exception)
            {
                _logger.LogError($"هنگام حذف واحد شمارش خطای زیر رخ داد {exception}");
                return result.Failed("هنگام ثبت عملیات خطای رخ داد");
            }
        }


        public EditAccountClub GetDetailsAccountClub(Guid id)
        {
            var accClub = _complexContext.AccountClubs.Find(id);
            if (accClub != null)
            {
                var map = _mapper.Map<EditAccountClub>(accClub);
                map.Account = this.GetSelectOptionAccounts();
                map.ClupType = this.GetSelectOptionClubTypes();
                map.Rating = this.GetSelectOptionRatings();
                map.States = this.SelectOptionState();
                map.Contracts = this.SelectOptionContract();
                map.Jobs = this.SelectOptionJob();               
                map.SateUid = _complexContext.Cities.Include(x => x.SttU)
                    .SingleOrDefault(x => x.CityUid == accClub.CityUid)?.SttUid;

                map.Cities = SelectOptionCities(map.SateUid ?? Guid.Empty);
                return map;
            }
            _logger.LogError($"هیچ رکوردی با این شناسه {id} یافت نشد");
            throw new NullReferenceException("عملیات با خطا مواجه شد لطفا با پشتیبانی تماس بگیرید.");

        }


        #endregion

        public List<AccountSelectOption> GetSelectOptionAccounts()
        {
            var account = _complexContext.AccountClubs.Select(x => new { x.AccClbName, x.AccClbUid }).AsNoTracking().Select(x => new AccountSelectOption()
            {
                Id = x.AccClbUid,
                Name = x.AccClbName
            }).ToList();
            return account;
        }


        public List<AccountClubType> GetSelectOptionClubTypes()
        {
            var account = _complexContext.AccountClubTypes.Select(x => new { x.AccClbTypName, x.AccClbTypUid }).AsNoTracking().Select(x => new AccountClubType()
            {
                AccClbTypUid = x.AccClbTypUid,
                AccClbTypName = x.AccClbTypName
            }).ToList();
            return account;
        }

        public List<AccountRating> GetSelectOptionRatings()
        {
            var account = _complexContext.AccountRatings.Select(x => new { x.AccRateName, x.AccRateUid }).AsNoTracking().Select(x => new AccountRating()
            {
                AccRateUid = x.AccRateUid,
                AccRateName = x.AccRateName
            }).ToList();
            return account;
        }


        public List<SelectListOption> SelectOptionState()
        {
            return _complexContext.States.Select(x => new { x.SttName, x.SttUid }).Select(x => new SelectListOption() { Value = x.SttUid, Text = x.SttName }).AsNoTracking().ToList();
        }
        public List<SelectListOptionInt> SelectOptionJob()
        {
            return _complexContext.Jobs.Select(x => new { x.JobName, x.JobId }).Select(x => new SelectListOptionInt() { Value = x.JobId, Text = x.JobName }).AsNoTracking().ToList();
        }

        public List<SelectListOption> SelectOptionCities(Guid stateId)
        {
            return _complexContext.Cities.Where(x => x.SttUid == stateId).Select(x => new { x.CityName, x.CityUid }).Select(x => new SelectListOption() { Value = x.CityUid, Text = x.CityName }).AsNoTracking().ToList();
        }

        #region Contracts 
        public JsonResult GetAllContracts(JqueryDatatableParam param)
        {
            //var list = _complexContext.Contracts.AsNoTracking();
            var list = _complexContext.Contracts.FromSqlInterpolated($"select * from Contract c where c.CNT_FR_CONTRACT is not null or( c.CNT_FR_CONTRACT is null and c.CNT_ID not in(select Contract.CNT_FR_CONTRACT from Contract where Contract.CNT_FR_CONTRACT is not null))");

            if (!string.IsNullOrEmpty(param.SSearch))
                list = list.Where(x => x.CntTitle.ToLower().Contains(param.SSearch.ToLower()));

            var sortColumnIndex = Convert.ToInt32(_contextAccessor.HttpContext.Request.Query["iSortCol_0"]);
            var sortDirection = _contextAccessor.HttpContext.Request.Query["sSortDir_0"];

            switch (sortColumnIndex)
            {
                case 3:
                    list = sortDirection == "asc" ? list.OrderBy(c => c.CntContractNum) : list.OrderByDescending(c => c.CntContractNum);
                    break;
                case 4:
                    list = sortDirection == "asc" ? list.OrderBy(c => c.CntTitle) : list.OrderByDescending(c => c.CntTitle);
                    break;
                default:
                    {
                        string OrderingFunction(Contract e) => sortColumnIndex == 0 ? e.CntTitle : e.CntContractNum;
                        IOrderedEnumerable<Contract> rr = null;
                        rr = sortDirection == "asc" ? list.AsEnumerable().OrderBy((Func<Contract, string>)OrderingFunction) : list.AsEnumerable().OrderByDescending((Func<Contract, string>)OrderingFunction);

                        list = rr.AsQueryable();
                        break;
                    }
            }

            IQueryable<Contract> displayResult;
            if (param.IDisplayLength != 0)
                displayResult = list.Skip(param.IDisplayStart)
                .Take(param.IDisplayLength);
            else displayResult = list;
            var totalRecords = list.Count();
            var map = _mapper.Map<List<ContractDto>>(displayResult.ToList());

            foreach (var clubTypeDto in map)
            {
                clubTypeDto.CntTypeText = clubTypeDto.CntType switch
                {
                    0 => "سقف اعتبار",
                    1 => "تخفیف درصدی",
                    2 => "تخفیف ریالی",
                    _ => clubTypeDto.CntTypeText
                };
            }


                var result = (new
            {
                param.SEcho,
                iTotalRecords = totalRecords,
                iTotalDisplayRecords = totalRecords,
                aaData = map
            });
            return new JsonResult(result, new JsonSerializerOptions { PropertyNamingPolicy = null });
        }

        public ResultDto CreateContract(ContractDto contract)
        {
            var result = new ResultDto();
            try
            {
                if (_complexContext.Contracts.Any(x => x.CntTitle == contract.CntTitle.Fix()))
                    return result.Failed(ValidateMessage.DuplicateName);

                if (_complexContext.Contracts.Any(x => x.CntContractNum == contract.CntContractNum.Fix()))
                    return result.Failed(ValidateMessage.DuplicateCode);

                var unit = _mapper.Map<Contract>(contract);
                _complexContext.Contracts.Add(unit);
                _complexContext.SaveChanges();
                return result.Succeeded();
            }
            catch (Exception exception)
            {
                _logger.LogError($"هنگام ثبت قرارداد خطای زیر رخ داد {exception}");
                return result.Failed("هنگام ثبت عملیات خطای رخ داد");
            }
        }

        public ContractDto GetContract(Guid id)
        {
            var contract = _complexContext.Contracts.Find(id);
            if (contract != null)
            {
                var map = _mapper.Map<ContractDto>(contract);
                return map;
            }
            _logger.LogError($"هیچ رکوردی با این شناسه {id} یافت نشد");
            throw new NullReferenceException("عملیات با خطا مواجه شد لطفا با پشتیبانی تماس بگیرید.");
        }

        public ResultDto UpdateContract(ContractDto contract)
        {
            var result = new ResultDto();
            try
            {
                var contract1 = _complexContext.Contracts.Find(contract.CntId);
                if (contract1 == null)
                {
                    _logger.LogWarning($"Don't Find Any Record With Id {contract.CntId} On Table WareHouse");
                    return result.Failed("خطای رخ داد، لطفا با پشتیبانی تماس بگرید");
                }

                if (_complexContext.Contracts.Any(x => x.CntTitle == contract.CntTitle.Fix() && x.CntId != contract.CntId))
                    return result.Failed(ValidateMessage.DuplicateName);

                if (_complexContext.Contracts.Any(x => x.CntContractNum == contract.CntContractNum.Fix() && x.CntId != contract.CntId))
                    return result.Failed(ValidateMessage.DuplicateCode);

                contract.CntCreateon = contract1.CntCreateon;
                contract.CntFrCreatedby = contract1.CntFrCreatedby;
                var map = _mapper.Map(contract, contract1);
                _complexContext.Contracts.Update(map);
                _complexContext.SaveChanges();
                return result.Succeeded();
            }
            catch (Exception exception)
            {
                _logger.LogError($"هنگام ویرایش واحد شمارش خطای زیر رخ داد {exception}");
                return result.Failed("هنگام ثبت عملیات خطای رخ داد");
            }
        }

        public bool GetContractByName(string name)
        {
            var result = _complexContext.Contracts.Any(u => u.CntTitle == name);
            return result;

        }

        public bool CheckContractNameExists(string name, Guid id)
        {
            var result = _complexContext.Contracts.Any(u => u.CntTitle == name && u.CntId != id);
            return result;
        }

        public ResultDto RemoveContract(Guid id)
        {
            var result = new ResultDto();
            try
            {
                var unit = _complexContext.Contracts.Find(id);
                if (unit == null)
                {
                    _logger.LogWarning($"Don't Find Any Record With Id {id} On Table Contracts");
                    return result.Failed("خطای رخ داد، لطفا با پشتیبانی تماس بگرید");
                }
                if (_complexContext.AccountClubs.Any(x => x.AccFrContract == unit.CntId))
                {
                    return result.Failed("این قرارداد در بخش مشترکین در حال استفاده است .لطفا از فسخ قرارداد استفاده نمایید");
                }
                _complexContext.Contracts.Remove(unit);
                _complexContext.SaveChanges();
                return result.Succeeded();
            }
            catch (Exception exception)
            {
                _logger.LogError($"هنگام حذف قرارداد خطای زیر رخ داد {exception}");
                return result.Failed("هنگام ثبت عملیات خطای رخ داد");
            }
        }

        public List<ContractDetail> GetContractDetails(Guid id)
        {
            var result = _complexContext.ContractDetails.Where(u => u.CdFrContract==id).Include(x=>x.CdFrContractNavigation).ToList();
            return result;
        }

        public ResultDto InsertProductContract(List<ContractDetail> contractDetails)
        {
            var result = new ResultDto();

            foreach (var item in contractDetails) {
               if( _complexContext.ContractDetails
                    .Any(x => x.CdFrContract ==item.CdFrContract && x.CdFrProduct==item.CdFrProduct))
                {
                    return result.Failed(ValidateMessage.DuplicateCode);
                }
                    }
            try {
                _complexContext.ContractDetails.AddRange(contractDetails);
                _complexContext.SaveChanges();
                return result.Succeeded();
            }
            catch(Exception e)
            {
                _logger.LogError($"هنگام ثبت قرارداد خطای زیر رخ داد {e}");
                return result.Failed("هنگام ثبت عملیات خطای رخ داد");
            };
            
        }

        public List<SelectListOption> SelectOptionContract()
        {
            List<SelectListOption> list=new List<SelectListOption>();
            var result = _complexContext.Contracts.FromSqlInterpolated($"select * from Contract c where c.CNT_FR_CONTRACT is not null or( c.CNT_FR_CONTRACT is null and c.CNT_ID not in(select Contract.CNT_FR_CONTRACT from Contract where Contract.CNT_FR_CONTRACT is not null))").ToList();
            foreach(var item in result)
            {
                list.Add(new SelectListOption() { Value = item.CntId, Text = item.CntTitle });
            }
            return list;
                }

        #endregion

    }

    public class AccountSelectOption
    {
        public string Name { get; set; }
        public Guid Id { get; set; }

    }

    //public class SelectListOption
    //{
    //    public string Name { get; set; }
    //    public Guid Id { get; set; }
    //}



}

