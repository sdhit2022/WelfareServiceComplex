using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Domain.ComplexModels;

public partial class Salon
{
    
    public long SlnId { get; set; }

    public string SlnName { get; set; } = null!;

    public short SlnType { get; set; }

    public Guid? FrWarHosUid { get; set; }

    public virtual ICollection<Calender> Calenders { get; set; } = new List<Calender>();

    public virtual ICollection<CardRechage> CardRechages { get; set; } = new List<CardRechage>();

    public virtual WareHouse? FrWarHosU { get; set; }

    public virtual ICollection<SalonDetail> SalonDetails { get; set; } = new List<SalonDetail>();

    public virtual ICollection<ServiceTransaction> ServiceTransactions { get; set; } = new List<ServiceTransaction>();

    public virtual ICollection<Ticket> Tickets { get; set; } = new List<Ticket>();
}
