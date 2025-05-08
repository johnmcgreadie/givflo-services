using System;
using System.Collections.Generic;

namespace GivFlow.Data;

public partial class Account
{
    public long AccountId { get; set; }

    public long OrganisationId { get; set; }

    public long ZaiId { get; set; }
}
