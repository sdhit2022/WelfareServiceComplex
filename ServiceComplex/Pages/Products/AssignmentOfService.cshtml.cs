using Application.Product;
using Application.Product.ProductDto;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ServiceComplex.Pages.Products
{
    public class AssignmentOfServiceModel : PageModel
    {
        private readonly IProductService _productService;
        public List<CreateProduct> AllProducts;
        public List<CreateProduct> AssignedProducts;

        public AssignmentOfServiceModel(IProductService productService)
        {
            _productService = productService;
        }

        public void OnGet()
        {

        }
    }
}
