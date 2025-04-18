using System;
using System.Collections.Generic;

namespace GivFlow.Data;

public partial class CampaignImage
{
    public long CampaignImageId { get; set; }

    public long CampaignId { get; set; }

    public byte[] Image { get; set; } = null!;

    public virtual Campaign Campaign { get; set; } = null!;
}
