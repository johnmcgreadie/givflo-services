using System;
using System.Collections.Generic;

namespace GivFlow.Data;

public partial class Organisation
{
    public long OrganisationId { get; set; }

    public string Name { get; set; } = null!;

    public byte[]? Icon { get; set; }

    public string Guid { get; set; }

    public virtual ICollection<Campaign> Campaigns { get; set; } = new List<Campaign>();

    public virtual ICollection<Donation> Donations { get; set; } = new List<Donation>();

    public virtual ICollection<Account> Accounts { get; set; } = new List<Account>();
}
