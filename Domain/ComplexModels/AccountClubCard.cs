using System;
using System.Collections.Generic;

namespace Domain.ComplexModels;

public partial class AccountClubCard
{
    public long AccId { get; set; }

    public string AccCardSerial { get; set; } = null!;

    public short AccType { get; set; }

    public string AccDesc { get; set; } = null!;

    public Guid AccFrAccountclub { get; set; }

    public Guid AccFrCreateBy { get; set; }

    public DateTime AccCreateOn { get; set; }

    public virtual AccountClub AccFrAccountclubNavigation { get; set; } = null!;

    public virtual SystemUser AccFrCreateByNavigation { get; set; } = null!;
}
