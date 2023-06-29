using Application.Interfaces.Context;
using Application.Interfaces;
using Application.Product;
using AutoMapper;
using Domain.ComplexModels;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Identity.Client;
using Application.Common;
using Microsoft.EntityFrameworkCore;
using static Application.Product.Category.ProductCategory;

namespace Application.BaseInfo
{
    public interface IWarehouseService
    {
        List<WareHouse> GetAll();
        List<SelectListOption> GetSelectListItems();
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

        public List<SelectListOption> GetSelectListItems()
        {
            return _complexContext.WareHouses.Select(x => new { x.WarHosUid, x.WarHosName })
           .Select(x => new SelectListOption
           {
               Text = x.WarHosName,
               Value = x.WarHosUid
           }).ToList();


           
        }
    }
}
