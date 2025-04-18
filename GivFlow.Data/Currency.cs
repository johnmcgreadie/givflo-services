using System;
using System.Collections.Generic;

namespace GivFlow.Data;

public partial class Currency
{
    public int CurrencyId { get; set; }

    public string Name { get; set; } = null!;

    public string Code { get; set; } = null!;

    public virtual ICollection<Donation> Donations { get; set; } = new List<Donation>();
}
