using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.BaseInfo
{
    public class AccountClubVM
    {
        public Guid AccClbUid { get; set; }
        public DateTime? AccClbBrithday { get; set; }
        public string AccClbName { get; set; }

        public string AccClbCode { get; set; }
        public bool? AccClbStatus { get; set; }
        public DateTime? SysUsrCreatedon { get; set; }

        public Guid? SysUsrCreatedby { get; set; }

        public DateTime? SysUsrModifiedon { get; set; }

        public Guid? SysUsrModifiedby { get; set; }

        public Guid? AccUid { get; set; }

        public Guid? AccRateUid { get; set; }

        public string AccClbNationalCode { get; set; }

        public string AccClbPostalCode { get; set; }

        public string AccClbPhone1 { get; set; }
        public string AccClbMobile { get; set; }
        public Guid? AccClbParentUid { get; set; }
        public Guid? AccClbTypUid { get; set; }
        public int? AccClbSex { get; set; }

        public string AccClbAddress { get; set; }
        public string AccClbDescribtion { get; set; }
        public string AccClbClubCard { get; set; }
        public int? AccFrJob { get; set; }

        public Guid? AccFrContract { get; set; }
        public string AccCardSerial { get; set; }

        public short? AccRelationType { get; set; }

    }
}
