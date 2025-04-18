using Microsoft.AspNetCore.Mvc;
using Stripe;
using Stripe.Terminal;

namespace GivFlowAPI.Controllers;

[ApiController]
[Route("registration")]
public class RegistrationController : ControllerBase
{
    private readonly ILogger<RegistrationController> _logger;

    public RegistrationController(ILogger<RegistrationController> logger)
    {
        _logger = logger;
    }


    [HttpPost]
    public JsonResult PostRegister()
    {
        return new JsonResult (new {});
    }
    
}
