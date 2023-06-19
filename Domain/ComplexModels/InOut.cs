using System;
using System.Collections.Generic;

namespace Domain.ComplexModels;

public partial class InOut
{
    public Guid IoId { get; set; }

    public Guid IoFrAccountclub { get; set; }

    public DateTime IoDate { get; set; }

    public bool IoType { get; set; }

    public int IoTime { get; set; }

    public Guid IoFrCalender { get; set; }

    public long? IoFrCsp { get; set; }

    public Guid? IoFrCalenderDetail { get; set; }

    public Guid IoFrServiceTransactions { get; set; }

    public virtual AccountClub IoFrAccountclubNavigation { get; set; } = null!;

    public virtual CalenderDetail? IoFrCalenderDetailNavigation { get; set; }

    public virtual Calender IoFrCalenderNavigation { get; set; } = null!;

    public virtual ContinuouseServicesPlaning? IoFrCspNavigation { get; set; }

    public virtual ServiceTransaction IoFrServiceTransactionsNavigation { get; set; } = null!;
}
