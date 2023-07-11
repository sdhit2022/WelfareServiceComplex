using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common
{
    public class SelectListOption
    {
        public Guid Value { get; set; }
        public string? Text { get; set; }
    }
    public class SelectListOptionInt
    {
        public int Value { get; set; }
        public string? Text { get; set; }
    }
    public class SelectListOptionLong
    {
        public long Value { get; set; }
        public string? Text { get; set; }
    }
}
