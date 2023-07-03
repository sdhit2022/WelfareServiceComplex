using Application.Common;
using Application.Product;
using Application.Product.Category;
using Application.Product.ProductDto;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using static Application.Product.Category.ProductCategory;

namespace ServiceComplex.Pages.Products
{
    public class AssignmentModel : PageModel
    {
        private readonly IProductService _productService;
        private readonly IProductCategory _category;
        public List<ProductList> Products;
        public List<ProductAssign> AssignedProducts;
        public List<ProductLevelDto> List;

        public AssignmentModel(IProductService productService, IProductCategory category)
        {
            _productService = productService;
            _category = category;
        }

        public void OnGet()
        {   
            Products = new List<ProductList>();
            List = _category.GetLevelList();
            for(int i=0;i<List.Count;i++)
            {
                Products.Add(new ProductList { Id = List[i].Id, products = _productService.GetProductsByCategory(List[i].Id)});
            }
            //todo get AssignedProducts by salonid
        }

        public IActionResult OnGetMoveSelectedProducts(List<Guid> products)
        {
            AssignedProducts = new List<ProductAssign>();
            for (int i = 0; i < products.Count; i++)
            {
                var product = _productService.GetProductsById(products[i]);
                AssignedProducts.Add(product);
            }
            return Redirect("/Products/Assignment");

        }
    }
}
