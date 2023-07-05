using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common
{
    public class ProductAssign
    {
        public Guid PrdUid { get; set; }
        public Guid PrdLvlUid3 { get; set; }
        public string? PrdName { get; set; }
        public int? Type { get; set; }
        public bool? PrdStatus { get; set; }
        public string? PrdLevelId { get; set; }
        public int IsNew { get; set; } = 0;
        //public long? Salon { get; set; }
    }
}
