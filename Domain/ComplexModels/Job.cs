using System;
using System.Collections.Generic;

namespace Domain.ComplexModels;

public partial class Job
{
    public int JobId { get; set; }

    public string JobName { get; set; } = null!;

    public virtual ICollection<AccountClub> AccountClubs { get; set; } = new List<AccountClub>();
}
