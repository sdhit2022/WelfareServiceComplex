using Application.Product;
using Application.Product.ProductDto;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ServiceComplex.Pages.Products;

public class IndexModel : PageModel
{
    private readonly IProductService _productService;
    public ProductDetails Details;

    public List<ProductDto> Products;

    public IndexModel(IProductService productService)
    {
        _productService = productService;
    }

    public void OnGet()
    {
        Products = _productService.GetAll();
    }

    public IActionResult OnGetDetails(Guid id)
    {
        var details = _productService.GetDetails(id);
        details.Properties = _productService.GetProductProperty(id);
        details.Pictures = _productService.GetProductPictures(id);
        return new JsonResult(details);
    }

    public IActionResult OnGetPictures(Guid id)
    {
        return new JsonResult(_productService.GetProductPictures(id));
    }

    public IActionResult OnGetRemove(Guid id)
    {
        return new JsonResult(_productService.Remove(id));
    }
}