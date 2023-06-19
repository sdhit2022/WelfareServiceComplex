using Application.Product.ProductDto;
using AutoMapper;
using Domain.ComplexModels;

namespace Application.Product;

public class ProductMapping : Profile
{
    public ProductMapping()
    {
        this.CreateMap<ProductDto.ProductDto, Domain.ComplexModels.Product>();
        this.CreateMap<CreateProduct, Domain.ComplexModels.Product>().ReverseMap();
        this.CreateMap<ProductProperty, ProductPropertiesDto>();
        this.CreateMap<ProductPicture, ProductPicturesDto>().ReverseMap();
        this.CreateMap<ProductProperty, PropertySelectOptionDto>().ReverseMap();
        this.CreateMap<Domain.ComplexModels.Product, EditProduct>().ReverseMap();
    }  
}