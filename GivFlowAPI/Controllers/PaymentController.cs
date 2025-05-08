using System.Net.Http;
using System.Text.Json.Nodes;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Stripe;
using Stripe.Terminal;

using System.Net.Http.Headers;
using System.Text.Json;

namespace GivFlowAPI.Controllers;

[ApiController]
[Route("payment")]
public class PaymentController : ControllerBase
{
    private readonly ILogger<PaymentController> _logger;

    public PaymentController(ILogger<PaymentController> logger)
    {
        _logger = logger;
        StripeConfiguration.ApiKey = "sk_test_51PxUczP2UBukSQmvlongUvGzwP0769SBQOeq5zLP6qr3BeFqKzLCdlpYPNTcCy0Ps5zJXniQP9YIFgvcX7Z4Iaxf0065krQtkT";

        var options = new ConnectionTokenCreateOptions { };
        var service = new ConnectionTokenService();
        var connectionToken = service.Create(options);
    }

    //[HttpPost]
    //public JsonResult CreatePaymentSheet()
    //{
    //    var customerOptions = new CustomerCreateOptions();
    //    var customerService = new CustomerService();
    //    var customer = customerService.Create(customerOptions);
    //    var ephemeralKeyOptions = new EphemeralKeyCreateOptions
    //    {
    //        Customer = customer.Id,
    //        StripeVersion = "2024-06-20",
    //    };
    //    var ephemeralKeyService = new EphemeralKeyService();
    //    var ephemeralKey = ephemeralKeyService.Create(ephemeralKeyOptions);

    //    var paymentIntentOptions = new PaymentIntentCreateOptions
    //    {
    //        Amount = 2000,
    //        Currency = "aud",
    //        Customer = customer.Id,
    //        // In the latest version of the API, specifying the `automatic_payment_methods` parameter
    //        // is optional because Stripe enables its functionality by default.
    //        AutomaticPaymentMethods = new PaymentIntentAutomaticPaymentMethodsOptions
    //        {
    //            Enabled = true,
    //        },
    //    };
    //    var paymentIntentService = new PaymentIntentService();
    //    PaymentIntent paymentIntent = paymentIntentService.Create(paymentIntentOptions);

    //    return new JsonResult(
    //    new {
    //        PaymentIntent = paymentIntent.ClientSecret,
    //        EphemeralKey = ephemeralKey.Secret,
    //        Customer = customer.Id,
    //        PublishableKey =
    //            "pk_test_51PxUczP2UBukSQmvdxwPnMunz3s909VYieT09XAhGiFtQC7k1trbGZSDW0T6yNj2YOre3iU3lduns7XzwGBILU4e00Vwso6VuB",
    //    });
    //}

    [HttpPost]
    public async Task<JsonResult> Zai()
    {
        return new JsonResult(null);
    }

}
