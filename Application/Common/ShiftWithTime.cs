using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common
{
    public class ShiftWithTime
    {
        public int ShfId { get; set; }

        public string ShfName { get; set; } = null!;

        public TimeSpan ShfStartTime { get; set; }

        public TimeSpan ShfEndTime { get; set; }

        public int ShfTelorance { get; set; }

    }
}
