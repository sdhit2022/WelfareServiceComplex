﻿using System;
using System.Collections.Generic;

namespace Domain.SaleInModels;

public partial class ObjectDescription
{
    public Guid DscUid { get; set; }

    public Guid? ObjUid { get; set; }

    public string? DscDescription { get; set; }
}
