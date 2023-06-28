namespace Application.Product.ProductDto;

public class ProductDetails
{
    public Guid PrdUid { get; set; }
    public string PrdName { get; set; }
    public int PrdMaxSale { get; set; }
    public decimal PrdDiscount { get; set; }
    public decimal PrdDiscountType { get; set; }
    public decimal PrdPricePerUnit1 { get; set; }
    public decimal PrdPricePerUnit2 { get; set; }
    public decimal PrdPricePerUnit3 { get; set; }
    public decimal PrdPricePerUnit4 { get; set; }
    public decimal PrdTaxValue { get; set; }
    public string Unit1 { get; set; }
    public string Unit2 { get; set; }
    public string Weight { get; set; }
    public string Volume { get; set; }
    public string IranCode { get; set; }
    public string BarCode { get; set; }
    public int? Type { get; set; }
    public bool? PrdHasTiming { get; set; }
    public short? PrdBaseTime { get; set; }
    public decimal? PrdBaseCost { get; set; }
    public short? PrdExtraTime { get; set; }
    public decimal? PrdExtraCost { get; set; }
    public decimal? PrdMinCharge { get; set; }
    public List<ProductPropertiesDto> Properties { get; set; }
    public List<ProductPicturesDto> Pictures { get; set; }
}