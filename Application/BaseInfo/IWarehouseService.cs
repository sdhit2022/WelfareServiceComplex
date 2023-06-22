using Application.Interfaces.Context;
using Application.Interfaces;
using Application.Product;
using AutoMapper;
using Domain.ComplexModels;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Application.BaseInfo
{
    public interface IWarehouseService
    {
        List<WareHouse> GetAll();
    }


    public class WarehouseService : IWarehouseService
    {
        private readonly IAuthHelper _authHelper;
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly ILogger<ProductService> _logger;
        private readonly IMapper _mapper;
        private readonly IComplexContext _complexContext;

        public WarehouseService(IAuthHelper authHelper, IHttpContextAccessor contextAccessor, ILogger<ProductService> logger, IMapper mapper, IComplexContext complexContext)
        {
            _authHelper = authHelper;
            _contextAccessor = contextAccessor;
            _logger = logger;
            _mapper = mapper;
            _complexContext = complexContext;
        }

        public List<WareHouse> GetAll()
        {
            return _complexContext.WareHouses.ToList();
        }
    }
}
