using GivFlow.Data;
using GivFlow.Data.Configuration;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Stripe;
using Stripe.Terminal;

namespace GivFlowAPI.Controllers;

[ApiController]
[Route("organisation")]
public class OrganisationController : ControllerBase
{
    private readonly ILogger<DonationController> _logger;
    private readonly IOptions<ConnectionStrings> _connectionStrings;
    public OrganisationController(ILogger<DonationController> logger, IOptions<ConnectionStrings> options)
    {
        _logger = logger;
        _connectionStrings = options;
    }

    [HttpGet]
    public List<Organisation> Get()
    {
        using (var context = new GivFlowContext(_connectionStrings.Value.GivFlowAuthConnection))
        {
            return context.Organisations.ToList();
        }

    }
}
