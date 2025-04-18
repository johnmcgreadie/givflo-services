using System.Net.Http;
using System.Text.Json.Nodes;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Stripe;
using Stripe.Terminal;
using GivFlowAPI.Zai;
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
        HttpClient authClient = new HttpClient();

        authClient.BaseAddress = new Uri("https://au-0000.sandbox.auth.assemblypay.com");

        var authData = new
        {
            grant_type = "client_credentials",
            client_id = "33bhv7ruuoli8krralcn45faaf",
            client_secret = "",
            scope = "im-au-09/5275a8f0-ecf2-013d-4cec-0a58a9feac03:59dbf14b-15b9-4bcc-b9cc-0313d5a9fc5d:3"
        };

        using StringContent authjsonContent = new(
            JsonSerializer.Serialize(authData),
            Encoding.UTF8,
            "application/json");

        var authResponse = await authClient.PostAsync("tokens", authjsonContent);

        using StringContent userJsonContent = new(
            JsonSerializer.Serialize(new AnonymousUser()),
            Encoding.UTF8,
            "application/json");

        var tokenResponse = await authResponse.Content.ReadFromJsonAsync<TokenResponse>();

        HttpClient c = new HttpClient();
        c.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer", tokenResponse.AccessToken);

        c.BaseAddress = new Uri("https://test.api.promisepay.com");

        var response = await c.PostAsync("items", userJsonContent);

        var userResponse = await response.Content.ReadFromJsonAsync<UserResponse>();

        return new JsonResult(userResponse);
    }

}
