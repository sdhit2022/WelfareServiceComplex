﻿using System;
using System.Collections.Generic;

namespace Domain.ComplexModels;

public partial class ProductProperty
{
    public Guid Id { get; set; }

    public Guid PropertyId { get; set; }

    public Guid ProductId { get; set; }

    public string Value { get; set; }

    public virtual Product Product { get; set; }

    public virtual Property Property { get; set; }
}
