using System;
using System.Collections.Generic;

namespace Domain.ComplexModels;

public partial class SalonProduct
{
    public Guid SpId { get; set; }

    public Guid SpFrProduct { get; set; }

    public long SpFrSalon { get; set; }

    public virtual Product SpFrProductNavigation { get; set; } = null!;

    public virtual Salon SpFrSalonNavigation { get; set; } = null!;
}
