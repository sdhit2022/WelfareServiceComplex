using System;
using System.Collections.Generic;

namespace Domain.ComplexModels;

public partial class WorkYear
{
    public int WyId { get; set; }

    public int WyYear { get; set; }

    public bool WyStatus { get; set; }
}
