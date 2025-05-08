using GivFlo.Services.Zai;
using GivFlow.Data;
using GivFlow.Data.Configuration;
using GivFlowAPI.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Stripe;
using Stripe.Terminal;

namespace GivFlowAPI.Controllers;

[ApiController]
[Route("donate")]
public class DonateController : ControllerBase
{
    private readonly ILogger<DonationController> _logger;
    private readonly IOptions<ConnectionStrings> _connectionStrings;
    private readonly IOptions<Website> _websiteOptions;
    private readonly IOptions<ZaiSettings> _zaiOptions;

    public DonateController(ILogger<DonationController> logger, IOptions<ConnectionStrings> options, IOptions<Website> websiteOptions, IOptions<ZaiSettings> zaiOptions)
    {
        _logger = logger;
        _connectionStrings = options;
        _websiteOptions = websiteOptions;
        _zaiOptions = zaiOptions;
    }


    [HttpGet]
    [Route("{organisationGuid}/{campaignGuid}")]
    public ActionResult Donation(string organisationGuid, string campaignGuid)
    {
        using (var context = new GivFlowContext(_connectionStrings.Value.GivFlowAuthConnection))
        {
            var organisation = context.Organisations.FirstOrDefault(o => o.Guid == organisationGuid);

            if (organisation != null)
            {
                var campaign = context.Campaigns.FirstOrDefault(o => o.Guid == campaignGuid);

                if (campaign != null)
                    return new RedirectResult($"{_websiteOptions.Value.BaseUrl}/donate?Org={organisationGuid}&Campaign={campaignGuid}");
            }

            return NotFound();
        }
    }

    [HttpPost]
    public async Task<JsonResult> Donate(MakeDonationDto donation)
    {
        var client = new ZaiClient(_zaiOptions);

        var result = await client.CreateItem(donation.BuyerId, "givflo-anon638823110896050081", donation.Amount);

        if (result != null)
        {
            var cardToken = await client.MakePayment(result.Items.Id, donation.AccountId);

            return new JsonResult(cardToken);
        }

        return new JsonResult(null);
    }
}
