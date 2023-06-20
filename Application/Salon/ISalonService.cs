

using Application.Interfaces.Context;
using Application.Interfaces;
using Application.Product;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;

namespace Application.Salon
{
    public interface ISalonService
    {
        List<Domain.ComplexModels.Salon> GetAll();
        Domain.ComplexModels.Salon GetSalon(long id);
        bool UpdateSalon();
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

        public bool UpdateSalon()
        {
            return false;
            
        }
    }
}
