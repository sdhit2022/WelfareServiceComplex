using System;
using System.Collections.Generic;

namespace Domain.ComplexModels;

public partial class PersonelCalender
{
    public Guid PcId { get; set; }

    public Guid PcFrCalender { get; set; }

    public Guid PcFrAccountclub { get; set; }

    public virtual AccountClub PcFrAccountclubNavigation { get; set; }

    public virtual Calender PcFrCalenderNavigation { get; set; }
}
