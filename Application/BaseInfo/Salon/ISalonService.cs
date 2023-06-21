

using Application.Interfaces.Context;
using Application.Interfaces;
using Application.Product;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using Application.Common;

namespace Application.BaseInfo.Salon
{
    public interface ISalonService
    {
        List<Domain.ComplexModels.Salon> GetAll();
        Domain.ComplexModels.Salon GetSalon(long id);
        bool UpdateSalon(Domain.ComplexModels.Salon salon);
        void saveChanges();
        bool InsertSalon(Domain.ComplexModels.Salon salon);
        ResultDto RemoveSalon(long id);
    }



    public class SalonService : ISalonService
    {

        private readonly IAuthHelper _authHelper;
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly ILogger<ProductService> _logger;
        private readonly IMapper _mapper;
        private readonly IComplexContext _complexContext;

        public SalonService(IAuthHelper authHelper, IHttpContextAccessor contextAccessor, ILogger<ProductService> logger, IMapper mapper, IComplexContext complexContext)
        {
            _authHelper = authHelper;
            _contextAccessor = contextAccessor;
            _logger = logger;
            _mapper = mapper;
            _complexContext = complexContext;
        }

        public List<Domain.ComplexModels.Salon> GetAll()
        {
            return _complexContext.Salons.ToList();
        }

        public Domain.ComplexModels.Salon GetSalon(long id)
        {
            return _complexContext.Salons.Find(id);
        }

        public bool InsertSalon(Domain.ComplexModels.Salon salon)
        {
            try
            {
                _complexContext.Salons.Add(salon);
                saveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public ResultDto RemoveSalon(long id)
        {
            var result = new ResultDto();
            Domain.ComplexModels.Salon salon = _complexContext.Salons.Find(id);
            if (salon != null)
            {
                try
                {
                    _complexContext.Salons.Remove(salon);
                    saveChanges();
                    return result.Succeeded();
                }
                catch (Exception ex)
                {
                    _logger.LogError($"حین حذف سالن با آیدی {id} خطای زیر رخ داد {ex}");
                    return result.Failed("عملیات با خطا مواجه شد");
                }
            }
            _logger.LogError($"Don't Any Record width Id {id} of Table Salon");
            return result.Failed("سالن وجود ندارد");
        }

        public void saveChanges()
        {
            _complexContext.SaveChanges();
        }

        public bool UpdateSalon(Domain.ComplexModels.Salon salon)
        {
            Domain.ComplexModels.Salon sln = _complexContext.Salons.Find(salon.SlnId);
            if (sln != null)
            {
                sln.SlnName = salon.SlnName;
                sln.FrWarHosUid = salon.FrWarHosUid;
                sln.SlnType = salon.SlnType;

                try
                {
                    _complexContext.Salons.Update(sln);
                    saveChanges();
                    return true;
                }
                catch (Exception ex)
                {
                    _logger.LogError($"حین به روز رسانی سالن با آیدی {salon.SlnId} خطای زیر رخ داد {ex}");
                    return false;
                }
            }
            else
            {
                _logger.LogError($"Don't Any Record width Id {salon.SlnId} of Table Salon");
                return false;
            }

        }
    }
}
