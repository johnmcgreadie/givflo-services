using System.Net.Http.Headers;
using Microsoft.AspNetCore.Mvc;
using Stripe;
using Stripe;
using Stripe.Checkout;
using System.Text;
using System.Text.Json;
using GivFlowAPI.Zai;

namespace GivFlowAPI.Controllers;

[ApiController]
[Route("donations")]
public class DonationController : ControllerBase
{
    private readonly ILogger<DonationController> _logger;

    public DonationController(ILogger<DonationController> logger)
    {
        _logger = logger;
        StripeConfiguration.ApiKey = "sk_test_51PxUczP2UBukSQmvlongUvGzwP0769SBQOeq5zLP6qr3BeFqKzLCdlpYPNTcCy0Ps5zJXniQP9YIFgvcX7Z4Iaxf0065krQtkT";
    }


    [HttpGet]
    public async Task<JsonResult> connection_token()
    {
        HttpClient authClient = new HttpClient();

        authClient.BaseAddress = new Uri("https://au-0000.sandbox.auth.assemblypay.com");

        var authData = new { 
            grant_type = "client_credentials", 
            client_id = "33bhv7ruuoli8krralcn45faaf", 
            client_secret = "1bk30h1e2coq71p587s4dgpevof0o06r1ehao6987uj4p5rlu2ep", 
            scope = "im-au-09/5275a8f0-ecf2-013d-4cec-0a58a9feac03:59dbf14b-15b9-4bcc-b9cc-0313d5a9fc5d:3" };

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

        var response = await c.PostAsync("users", userJsonContent);

        var userResponse = await response.Content.ReadFromJsonAsync<UserResponse>();

        var data = new { token_type = "card", user_id = userResponse.User.Id };

        using StringContent jsonContent = new(
            JsonSerializer.Serialize(data),
            Encoding.UTF8,
            "application/json");


        response = await c.PostAsync("token_auths", jsonContent);

        var tokenAuthResponse = await response.Content.ReadFromJsonAsync<TokenAuthResponse>();
        return new JsonResult(tokenAuthResponse);
    }
}
