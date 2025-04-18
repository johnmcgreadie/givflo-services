using System;
using System.Collections.Generic;
using GivFlow.Data;

namespace GivFlowAPI.Dtos;

public class CampaignDto
{
    public long CampaignId { get; set; }
    public long OrganisationId { get; set; }

    public string Name { get; set; }

    public string Description { get; set; }
}
