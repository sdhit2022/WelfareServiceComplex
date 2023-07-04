using Application.Common;
using Application.Product;
using Application.Product.Category;
using Application.Product.ProductDto;
using Domain.ComplexModels;
using Domain.SaleInModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using static Application.Product.Category.ProductCategory;

namespace ServiceComplex.Pages.Products
{
    public class AssignmentModel : PageModel
    {
        private readonly IProductService _productService;
        private readonly IProductCategory _category;
        public List<ProductAssign> Products;
        public List<SalonProduct> SalonProducts;
        public List<ProductAssign> AssignedProducts;
        public List<ProductLevelDto> List;
        public long salonId = Global.SalonId["salonId"];

        public AssignmentModel(IProductService productService, IProductCategory category)
        {
            _productService = productService;
            _category = category;
        }

        public void OnGet()
        {   
            Products = new List<ProductAssign>();
            List = _category.GetLevelList();
            SalonProducts = _productService.GetSalonProducts();

            var notAssigned = _productService.GetNotAssignedPrd(SalonProducts);

            foreach (var item in SalonProducts)
            {
                var product = _productService.GetProductsById(item.SpFrProduct);
                AssignedProducts.Add(product);

            }


            //todo get AssignedProducts by salonid
        }

        public IActionResult OnGetMoveSelectedProducts(List<Guid> products)
        {
            //var test = HttpContext?.Session.GetJson<List<ProductAssign>>("ProductAss");

            AssignedProducts = new List<ProductAssign>();
            for (int i = 0; i < products.Count; i++)
            {
                var product = _productService.GetProductsById(products[i]);
                product.IsNew = 1;
                AssignedProducts.Add(product);
            }
            return new JsonResult(AssignedProducts);

        }
    }
}
