﻿using Application.Product.ProductDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common
{
    public class ProductList
    {
        public Guid Id { get; set; }
        public List<ProductAssign>? products { get; set; }
    }
}
