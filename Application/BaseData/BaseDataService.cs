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

namespace Application.BaseData
{
    public interface IBaseDataService
    {
        JsonResult GetAllUnit(JqueryDatatableParam param);
        ResultDto CreateUnit(CreateUnit command);
        ResultDto<List<UnitDto>> RemoveUnit(Guid id);
        ResultDto UpdateUnit(EditUnit command);

        ResultDto CreateWareHouse(CreateWareHouse command);
        ResultDto RemoveWareHouse(Guid id);
        ResultDto UpdateWareHouse(UpdateWareHouse command);

        ResultDto CreateAccountClubType(CreateAccountClubType command);
        ResultDto RemoveAccountClubType(Guid id);
        ResultDto UpdateAccountClubType(UpdateAccountClubType command);


    }
    internal class BaseDataService : IBaseDataService
    {
        private readonly IShopContext _shopContext;
        private readonly ILogger<BaseDataService> _logger;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _contextAccessor;
        public BaseDataService(IShopContext shopContext, ILogger<BaseDataService> logger, IMapper mapper, IHttpContextAccessor contextAccessor)
        {
            _shopContext = shopContext;
            _logger = logger;
            _mapper = mapper;
            _contextAccessor = contextAccessor;
        }

        #region Unit Of Measurements

        public JsonResult GetAllUnit(JqueryDatatableParam param)
        {

            var list = _shopContext.UnitOfMeasurements.AsNoTracking();

            if (!string.IsNullOrEmpty(param.SSearch))
            {
                list = list.Where(x => x.UomName.ToLower().Contains(param.SSearch.ToLower())
                                                 || x.UomCode.ToLower().Contains(param.SSearch.ToLower()));

            }

            var sortColumnIndex = Convert.ToInt32(_contextAccessor.HttpContext.Request.Query["iSortCol_0"]);
            var sortDirection = _contextAccessor.HttpContext.Request.Query["sSortDir_0"];

            if (sortColumnIndex == 3)
            {
                list = sortDirection == "asc" ? list.OrderBy(c => c.UomCode) : list.OrderByDescending(c => c.UomCode);
            }
            else if (sortColumnIndex == 4)
            {
                list = sortDirection == "asc" ? list.OrderBy(c => c.UomName) : list.OrderByDescending(c => c.UomName);
            }

            else
            {
                Func<UnitOfMeasurement, string> orderingFunction = e => sortColumnIndex == 0 ? e.UomName :
                                                               sortColumnIndex == 1 ? e.UomCode :
                                                               e.UomCode;
                IOrderedEnumerable<UnitOfMeasurement> rr = null;
                rr = sortDirection == "asc" ? list.AsEnumerable().OrderBy(orderingFunction) : list.AsEnumerable().OrderByDescending(orderingFunction);

                list = rr.AsQueryable();
            }

            IQueryable<UnitOfMeasurement> displayResult;
            if (param.IDisplayLength != 0)
                displayResult = list.Skip(param.iDisplayStart)
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
                if (_shopContext.UnitOfMeasurements.Any(x => x.UomName == command.Name.Fix()))
                    return result.Failed(ValidateMessage.Duplicate);
                var unit = _mapper.Map<UnitOfMeasurement>(command);
                _shopContext.UnitOfMeasurements.Add(unit);
                _shopContext.SaveChanges();
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
                var unit = _shopContext.UnitOfMeasurements.Find(command.Id);
                if (unit == null)
                {
                    _logger.LogWarning($"Don't Find Any Record With Id {command.Id} On Table UnitOfMeasurements");
                    return result.Failed("خطای رخ داد، لطفا با پشتیبانی تماس بگرید");
                }

                if (_shopContext.UnitOfMeasurements.Any(x => x.UomName == command.Name.Fix() && x.UomUid != command.Id))
                    return result.Failed(ValidateMessage.Duplicate);
                var addUnit = _mapper.Map(command, unit);
                _shopContext.UnitOfMeasurements.Update(addUnit);
                _shopContext.SaveChanges();
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
                var unit = _shopContext.UnitOfMeasurements.Find(id);
                if (unit == null)
                {
                    _logger.LogWarning($"Don't Find Any Record With Id {id} On Table UnitOfMeasurements");
                    return result.Failed("خطای رخ داد، لطفا با پشتیبانی تماس بگرید");
                }

                _shopContext.UnitOfMeasurements.Remove(unit);
                _shopContext.SaveChanges();
                return result.Succeeded(null);
            }

            catch (DbUpdateException ex)
            {
                
                return result.Failed("این رکورد در جا های دیگری از برنامه استفاده شده است ");

            }
            catch (Exception exception)
            {
                _logger.LogError($"هنگام حذف واحد شمارش خطای زیر رخ داد {exception}");
                return result.Failed("هنگام ثبت عملیات خطای رخ داد");
            }
        }

        #endregion

        #region Unit Of WareHouse

        public ResultDto CreateWareHouse(CreateWareHouse command)
        {
            var result = new ResultDto();
            try
            {
                if (_shopContext.WareHouses.Any(x => x.WarHosName == command.Name.Fix()))
                    return result.Failed(ValidateMessage.Duplicate);
                var unit = _mapper.Map<WareHouse>(command);
                _shopContext.WareHouses.Add(unit);
                _shopContext.SaveChanges();
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
                var wareHouse = _shopContext.WareHouses.Find(command.Id);
                if (wareHouse == null)
                {
                    _logger.LogWarning($"Don't Find Any Record With Id {command.Id} On Table WareHouse");
                    return result.Failed("خطای رخ داد، لطفا با پشتیبانی تماس بگرید");
                }

                if (_shopContext.WareHouses.Any(x => x.WarHosName == command.Name.Fix() && x.WarHosUid != command.Id))
                    return result.Failed(ValidateMessage.Duplicate);
                var map = _mapper.Map(command, wareHouse);
                _shopContext.WareHouses.Update(map);
                _shopContext.SaveChanges();
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
                var unit = _shopContext.WareHouses.Find(id);
                if (unit == null)
                {
                    _logger.LogWarning($"Don't Find Any Record With Id {id} On Table WareHouse");
                    return result.Failed("خطای رخ داد، لطفا با پشتیبانی تماس بگرید");
                }

                _shopContext.WareHouses.Remove(unit);
                _shopContext.SaveChanges();
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

        public ResultDto CreateAccountClubType(CreateAccountClubType command)
        {
            var result = new ResultDto();
            try
            {
                if (_shopContext.AccountClubTypes.Any(x => x.AccClbTypName == command.Name.Fix()))
                    return result.Failed(ValidateMessage.Duplicate);
                var account = _mapper.Map<AccountClubType>(command);
                _shopContext.AccountClubTypes.Add(account);
                _shopContext.SaveChanges();
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
                var account = _shopContext.AccountClubTypes.Find(command.Id);
                if (account == null)
                {
                    _logger.LogWarning($"Don't Find Any Record With Id {command.Id} On Table AccountClubType");
                    return result.Failed("خطای رخ داد، لطفا با پشتیبانی تماس بگرید");
                }

                if (_shopContext.AccountClubTypes.Any(x => x.AccClbTypName == command.Name.Fix() && x.AccClbTypUid != command.Id))
                    return result.Failed(ValidateMessage.Duplicate);
                var map = _mapper.Map(command, account);
                _shopContext.AccountClubTypes.Update(map);
                _shopContext.SaveChanges();
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
                var unit = _shopContext.AccountClubTypes.Find(id);
                if (unit == null)
                {
                    _logger.LogWarning($"Don't Find Any Record With Id {id} On Table AccountClubType");
                    return result.Failed("خطای رخ داد، لطفا با پشتیبانی تماس بگرید");
                }

                _shopContext.AccountClubTypes.Remove(unit);
                _shopContext.SaveChanges();
                return result.Succeeded();
            }
            catch (Exception exception)
            {
                _logger.LogError($"هنگام حذف واحد شمارش خطای زیر رخ داد {exception}");
                return result.Failed("هنگام ثبت عملیات خطای رخ داد");
            }
        }

        #endregion



    }




}

