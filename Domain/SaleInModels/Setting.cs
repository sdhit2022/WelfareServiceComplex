using System;
using System.Collections.Generic;

namespace Domain.SaleInModels;

public partial class Setting
{
    public Guid SetUid { get; set; }

    public string SetKey { get; set; } = null!;

    public string? SetValue { get; set; }

    public string? SetBase { get; set; }
}
