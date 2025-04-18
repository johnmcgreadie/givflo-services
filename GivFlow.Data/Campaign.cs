using System;
using System.Collections.Generic;

namespace GivFlow.Data;

public partial class Campaign
{
    public long CampaignId { get; set; }

    public long OrganisationId { get; set; }

    public string Name { get; set; } = null!;

    public string Description { get; set; } = null!;

    public string? Qrcode { get; set; }

    public string? Guid { get; set; }

    public virtual ICollection<CampaignImage> CampaignImages { get; set; } = new List<CampaignImage>();

    public virtual ICollection<Donation> Donations { get; set; } = new List<Donation>();

    public virtual Organisation Organisation { get; set; } = null!;
}
