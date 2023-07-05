using Application.Common;
using Application.Product;
using Application.Product.Category;
using Application.Product.ProductDto;
using Domain.ComplexModels;
using Domain.SaleInModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using static Application.Product.Category.ProductCategory;

namespace ServiceComplex.Pages.Products
{
    public class AssignmentModel : PageModel
    {
        private readonly IProductService _productService;
        private readonly IProductCategory _category;
        private readonly IHttpContextAccessor _contextAccessor;
        public List<ProductAssign> Products=new List<ProductAssign>();
        public List<ProductAssign> SalonProducts;
        public List<ProductLevelDto> List;
        public ProductList model;

        public long salonId = 28; //Global.SalonId["salonId"];

        public AssignmentModel(IProductService productService, IProductCategory category, IHttpContextAccessor contextAccessor)
        {
            _productService = productService;
            _category = category;
            _contextAccessor = contextAccessor;
        }

        public void OnGet()
        {   
            List = _category.GetLevelList();
            //باید بر اساس ایدی سالن انتخاب شده دریافت شوند
            SalonProducts = _productService.GetSalonProducts(28);
            Products = _productService.GetNotAssignedPrd();
            model = new ProductList()
            {
                products = Products,
                salonProducts = SalonProducts
            };
            //todo get AssignedProducts by salonid
        }

        public IActionResult OnGetRemoveSelectedProducts(List<Guid> products)
        {
            var assigned = HttpContext?.Session.GetComplexData<List<ProductAssign>>("Assigned");
            var notAssigned = HttpContext?.Session.GetComplexData<List<ProductAssign>>("NotAssigned");

            var NotAssigned = new List<ProductAssign>();
            for (int i = 0; i < products.Count; i++)
            {
                var product = _productService.GetProductsById(products[i]);
                //add to assigned list
                product.IsNew = -1;
                NotAssigned.Add(product);
                //remove from not assigned list
                HttpContext?.Session.SetComplexData("Assigned", assigned.Where(x => x.PrdUid != product.PrdUid).ToList());
            }
            //update models
            notAssigned.AddRange(NotAssigned);
            _contextAccessor.HttpContext.Session.SetComplexData("NotAssigned", notAssigned);
            Products = HttpContext?.Session.GetComplexData<List<ProductAssign>>("NotAssigned");

            SalonProducts = HttpContext?.Session.GetComplexData<List<ProductAssign>>("Assigned");
            var model = new ProductList()
            {
                products = Products,
                salonProducts = SalonProducts
            };
            return Partial("Products/Partial/_Assignment", model);

        }
        public IActionResult OnGetMoveSelectedProducts(List<Guid> products)
        {
            var assigned = HttpContext?.Session.GetComplexData<List<ProductAssign>>("Assigned");
            var notAssigned = HttpContext?.Session.GetComplexData<List<ProductAssign>>("NotAssigned");

            var AssignedProducts = new List<ProductAssign>();
            for (int i = 0; i < products.Count; i++)
            {
                var product = _productService.GetProductsById(products[i]);
                //add to assigned list
                product.IsNew = 1;
                AssignedProducts.Add(product);
                //remove from not assigned list
                var list = HttpContext?.Session.GetComplexData<List<ProductAssign>>("NotAssigned");
                HttpContext?.Session.SetComplexData("NotAssigned", list.Where(x => x.PrdUid != product.PrdUid).ToList());
            }
            //update models
            Products = HttpContext?.Session.GetComplexData<List<ProductAssign>>("NotAssigned");
            assigned.AddRange(AssignedProducts);     
            
            _contextAccessor.HttpContext.Session.SetComplexData("Assigned", assigned);
            SalonProducts = HttpContext?.Session.GetComplexData<List<ProductAssign>>("Assigned");
            var model = new ProductList()
            {
                products = Products,
                salonProducts = SalonProducts
            };
            return Partial("Products/Partial/_Assignment", model);

        }
    }
}
