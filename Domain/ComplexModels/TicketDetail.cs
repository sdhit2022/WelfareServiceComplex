﻿using System;
using System.Collections.Generic;

namespace Domain.ComplexModels;

public partial class TicketDetail
{
    public Guid TdId { get; set; }

    public long TdFrTicket { get; set; }

    public string TdSerial { get; set; } = null!;

    public DateTime TdExpireDate { get; set; }

    public short TdStatus { get; set; }

    public virtual ICollection<ServiceTransaction> ServiceTransactions { get; set; } = new List<ServiceTransaction>();

    public virtual Ticket TdFrTicketNavigation { get; set; } = null!;
}
