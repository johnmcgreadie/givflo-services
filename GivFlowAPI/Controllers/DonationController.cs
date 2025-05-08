using System.Net.Http.Headers;
using GivFlo.Services.Zai;
using GivFlow.Data.Configuration;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
namespace GivFlowAPI.Controllers;

[ApiController]
[Route("donations")]
public class DonationController : ControllerBase
{
    private readonly ILogger<DonationController> _logger;
    private readonly IOptions<ZaiSettings> _options;

    public DonationController(ILogger<DonationController> logger, IOptions<ZaiSettings> options)
    {
        _logger = logger;
        _options= options;
    }

    [HttpGet]
    public async Task<JsonResult> Initialise()
    {
        var client = new ZaiClient(_options);

        var user = await client.CreateAnonymousUser();

        if (user != null)
        {
            var cardToken = await client.CardToken(user);

            return new JsonResult(cardToken);
        }

        return new JsonResult(null);
    }
}
