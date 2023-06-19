﻿using System;
using System.Collections.Generic;

namespace Domain.SaleInModels;

public partial class AccountClub
{
    public Guid AccClbUid { get; set; }

    public Guid? BusUnitUid { get; set; }

    public Guid? FisPeriodUid { get; set; }

    public string? AccClbName { get; set; }

    public string? AccClbCode { get; set; }

    public bool? AccClbSms { get; set; }

    public bool? AccClbStatus { get; set; }

    public DateTime? SysUsrCreatedon { get; set; }

    public Guid? SysUsrCreatedby { get; set; }

    public DateTime? SysUsrModifiedon { get; set; }

    public Guid? SysUsrModifiedby { get; set; }

    public Guid? AccParentUid { get; set; }

    public Guid? AccUid { get; set; }

    public Guid? AccRateUid { get; set; }

    public string? AccClbNationalCode { get; set; }

    public string? AccClbPostalCode { get; set; }

    public string? AccClbPhone1 { get; set; }

    public string? AccClbPhone2 { get; set; }

    public string? AccClbMobile { get; set; }

    public string? AccClbMobile2 { get; set; }

    public DateTime? AccClbBrithday { get; set; }

    public Guid? AccClbParentUid { get; set; }

    public int? AccClbSync { get; set; }

    public decimal? AccClbRemainCredit { get; set; }

    public Guid? CityUid { get; set; }

    public string? AccClbOldNumber { get; set; }

    public Guid? AccClbTypUid { get; set; }

    public int? AccClbNationality { get; set; }

    public int? AccClbSex { get; set; }

    public string? AccClbAddress { get; set; }

    public string? AccClbDescribtion { get; set; }

    public long? AccClbScore { get; set; }

    public decimal? AccClbCredit { get; set; }

    public string? AccClbAgentName { get; set; }

    public string? AccClbCompanyName { get; set; }

    public Guid? CntUid { get; set; }

    public string? AccClbClubCard { get; set; }

    public string? AccClbFolder { get; set; }

    public string? AccClbAddress2 { get; set; }

    public Guid? AccFloatUid { get; set; }

    public int? AccClbCreditType { get; set; }

    public string? AccClbDefaultAddress { get; set; }

    public string? AccClbPassword { get; set; }

    public string? AccClbLat { get; set; }

    public string? AccClbLong { get; set; }

    public string? AccClbLat1 { get; set; }

    public string? AccClbLong1 { get; set; }

    public bool? AccClbStatusApp { get; set; }

    public virtual Account? AccU { get; set; }

    public virtual ICollection<Invoice> Invoices { get; set; } = new List<Invoice>();
}
