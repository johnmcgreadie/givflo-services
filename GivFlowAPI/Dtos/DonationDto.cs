using System;
using System.Collections.Generic;
using GivFlow.Data;

namespace GivFlowAPI.Dtos;

public partial class DonationDto
{
    public long DonationId { get; set; }

    public long OrganisationId { get; set; }

    public long? CampaignId { get; set; }

    public string UserId { get; set; } = null!;

    public decimal Amount { get; set; }

    public int CurrencyId { get; set; }

    public virtual Campaign? Campaign { get; set; }

    public virtual Currency Currency { get; set; } = null!;

    public virtual Organisation Organisation { get; set; } = null!;

    public virtual AspNetUser User { get; set; } = null!;
}
