using Application.Common;
using Domain.ComplexModels;
using System.ComponentModel.DataAnnotations;

namespace Application.BaseData.Dto
{
    public class CreateUnit
    {
        public CreateUnit() => Id = Guid.NewGuid();
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public bool Status { get; set; } = true;
    }

    public class EditUnit : CreateUnit
    {
        public EditUnit() => Id = Guid.NewGuid();
    }
    public class UnitDto : CreateUnit { }

    public class CreateWareHouse
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; }
        public string Code { get; set; }
        public bool Status { get; set; } = true;
    }

    public class UpdateWareHouse : CreateWareHouse
    {
        public UpdateWareHouse() => Id = Guid.NewGuid();
    }


    public class WareHouseDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public bool Status { get; set; } = true;
    }

    public class ContractDto
    {
        public Guid CntId { get; set; }

        public string CntTitle { get; set; } = null!;

        public string CntStartDateShamsi { get; set; }

        public string CntEndDateShamsi { get; set; }

        public Guid? CntFrCreatedby { get; set; }

        public Guid? CntFrModifiedby { get; set; }

        public string CntCreateonShamsi { get; set; }
        public DateTime? CntCreateon { get; set; }

        public DateTime? CntModifiedon { get; set; }

        public string CntTypeText { get; set; }
        public short CntType { get; set; }

        public string CntContractNum { get; set; } = null!;

        public string CntFrContractName { get; set; }
    }

    public class ContractVM
    {
        public ContractDto contractDto{get;set;}
        public List<ContractDetail> contractDetails{get;set;}
        public List<ProductAssign> products { get; set; }
    }
    public class CreateAccountClubType
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; }
        public string DiscountType { get; set; }
        public bool Status { get; set; } = true;
        /// <summary>
        /// قیمت کالا
        /// </summary>

        [Range(1, Int32.MaxValue, ErrorMessage = "این مقدار نمیتواند خالی باشد")]
        public int? PriceInvoice { get; set; }
        /// <summary>
        /// درصد تخفیف
        /// </summary>
        public double? PercentDiscount { get; set; }
        /// <summary>
        /// تخفیف شعبه
        /// </summary>
        public double? DetDiscount { get; set; }
    }

    public class UpdateAccountClubType : CreateAccountClubType
    {
        public UpdateAccountClubType()
        {
            Id = new Guid();
        }
    }

    public class AccountClubTypeDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string DiscountType { get; set; }
        public string DiscountTypeText { get; set; }
        public bool Status { get; set; }
        /// <summary>
        /// قیمت کالا
        /// </summary>
        public int PriceInvoice { get; set; }
        public string PriceInvoiceText { get; set; }
        /// <summary>
        /// درصد تخفیف
        /// </summary>
        public double? PercentDiscount { get; set; }
        /// <summary>
        /// تخفیف شعبه
        /// </summary>
        public double? DetDiscount { get; set; }
    }



    public class CreateAccountRating
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; }
    }
    public class UpdateAccountRating : CreateAccountRating
    {
        public UpdateAccountRating()
        {
            Id = new Guid();
        }

    }
    public class AccountRatingDto
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; }
    }
}
