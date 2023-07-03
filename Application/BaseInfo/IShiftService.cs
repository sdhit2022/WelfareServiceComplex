
using Application.Interfaces.Context;
using Application.Interfaces;
using Application.Product;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Domain.ComplexModels;
using Application.Common;

namespace Application.BaseInfo
{
    public interface IShiftService
    {
        List<Shift> GetAll();
        bool Insert(Shift shift);
        ResultDto Remove(int id);
        ResultDto Update(Shift shift);
        Shift GetShift(int id);
        void saveChanges();

        bool GetShiftByName(string name);
        bool CheckShiftNameExists(string name, int id);
    }

    public class ShiftService : IShiftService 
    {
        private readonly IAuthHelper _authHelper;
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly ILogger<ProductService> _logger;
        private readonly IMapper _mapper;
        private readonly IComplexContext _complexContext;

        public ShiftService(IAuthHelper authHelper, IHttpContextAccessor contextAccessor, ILogger<ProductService> logger, IMapper mapper, IComplexContext complexContext)
        {
            _authHelper = authHelper;
            _contextAccessor = contextAccessor;
            _logger = logger;
            _mapper = mapper;
            _complexContext = complexContext;
        }

        public List<Shift> GetAll()
        {
            return _complexContext.Shifts.ToList();
        }

        public Shift GetShift(int id)
        {
            return _complexContext.Shifts.Find(id);
        }

        public bool Insert(Shift shift)
        {
                try
                {
                    _complexContext.Shifts.Add(shift);
                    saveChanges();
                    return true;
                }
                catch (Exception ex)
                {
                    _logger.LogError($"حین ذخیره شیفت خطای {ex} رخ داد");
                    return false;
                }
        }

        public ResultDto Remove(int id)
        {
            var result = new ResultDto();
            if (GetShift(id) != null)
            {
                try
                {
                    _complexContext.Shifts.Remove(GetShift(id));
                    saveChanges();
                    return result.Succeeded();
                }
                catch (Exception ex)
                {
                    _logger.LogError($"حین حذف شیفت با آیدی {id} خطای زیر رخ داد {ex}");
                    return result.Failed("عملیات با خطا مواجه شد");
                }
            }
            _logger.LogError($"Don't Any Record width Id {id} of Table Shift");
            return result.Failed("شیفت وجود ندارد");
        }

        public void saveChanges()
        {
            _complexContext.SaveChanges();
        }

        public ResultDto Update(Shift shift)
        {
            var result = new ResultDto();
            var oldshift = _complexContext.Shifts.Find(shift.ShfId);
            if (oldshift != null)
            {
                oldshift.ShfName = shift.ShfName;
                oldshift.ShfTelorance= shift.ShfTelorance;
                oldshift.ShfStartTime = shift.ShfStartTime;
                oldshift.ShfEndTime = shift.ShfEndTime;
                try
                {
                    _complexContext.Shifts.Update(oldshift);
                    saveChanges();
                    return result.Succeeded();
                }
                catch (Exception ex)
                {
                    _logger.LogError($"حین به روز رسانی شیفت با آیدی {shift.ShfId} خطای زیر رخ داد {ex}");
                    return result.Failed($"{ex}");
                }
            }
            return result.Failed("شیفت پیدا نشد!");
        }
        public bool CheckShiftNameExists(string name, int id)
        {
            var result = _complexContext.Shifts.Any(u => u.ShfName == name && u.ShfId != id);
            return result;
        }
        public bool GetShiftByName(string name)
        {
            var result = _complexContext.Shifts.Any(u => u.ShfName == name);
            return result;

        }
    }
}
