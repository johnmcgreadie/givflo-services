using Microsoft.AspNetCore.Mvc;
using Stripe;
using Stripe.Terminal;

namespace GivFlowAPI.Controllers;

[ApiController]
[Route("3rdparty/xero")]
public class XeroController : ControllerBase
{
    private readonly ILogger<XeroController> _logger;

    public XeroController(ILogger<XeroController> logger)
    {
        _logger = logger;
    }


    [HttpPost]
    public JsonResult Connect()
    {
        var redirectUrl =
            @"https://login.xero.com/identity/connect/authorize?response_type=code&client_id=YOURCLIENTID&redirect_uri=YOURREDIRECTURI&scope=openid profile email accounting.transactions&state=123";

        return new JsonResult(new { secret = "" });

    }
    
}
