using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using GivFlo.Services.Zai.Models;
using GivFlow.Data.Configuration;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace GivFlo.Services.Zai
{
    public class ZaiClient
    {
        public ZaiClient(IOptions<ZaiSettings> options)
        {

        }

        public async Task<TokenResponse?> Authenticate()
        {
            HttpClient authClient = new HttpClient();

            authClient.BaseAddress = new Uri("https://au-0000.sandbox.auth.assemblypay.com");

            var authData = new
            {
                grant_type = "client_credentials",
                client_id = "33bhv7ruuoli8krralcn45faaf",
                client_secret = "1bk30h1e2coq71p587s4dgpevof0o06r1ehao6987uj4p5rlu2ep",
                scope = "im-au-09/5275a8f0-ecf2-013d-4cec-0a58a9feac03:59dbf14b-15b9-4bcc-b9cc-0313d5a9fc5d:3"
            };

            using StringContent authjsonContent = new(
                JsonSerializer.Serialize(authData),
                Encoding.UTF8,
                "application/json");

            var authResponse = await authClient.PostAsync("tokens", authjsonContent);

            return await authResponse.Content.ReadFromJsonAsync<TokenResponse>();
        }

        public async Task<UserResponse?> CreateAnonymousUser()
        {

            using StringContent userJsonContent = new(
                JsonSerializer.Serialize(new AnonymousUser()),
                Encoding.UTF8,
                "application/json");

            var tokenResponse = await Authenticate();

            HttpClient c = new HttpClient();
            c.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", tokenResponse.AccessToken);

            c.BaseAddress = new Uri("https://test.api.promisepay.com");

            var response = await c.PostAsync("users", userJsonContent);

            return await response.Content.ReadFromJsonAsync<UserResponse?>();
        }

        public async Task<TokenAuthResponse?> CardToken(UserResponse userResponse)
        {
            var data = new { token_type = "card", user_id = userResponse.User.Id };

            using StringContent jsonContent = new(
                JsonSerializer.Serialize(data),
                Encoding.UTF8,
                "application/json");
            var tokenResponse = await Authenticate();

            HttpClient c = new HttpClient();
            c.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", tokenResponse.AccessToken);
            c.BaseAddress = new Uri("https://test.api.promisepay.com");

            var response = await c.PostAsync("token_auths", jsonContent);

            return await response.Content.ReadFromJsonAsync<TokenAuthResponse?>();
        }

        public async Task<ItemsResponse?> CreateItem(string buyerId, string sellerId, int amountCents)
        {

            var id = Guid.NewGuid().ToString();

            var item = new Item()
            {
                AmountCents = amountCents,
                BuyerId = buyerId,
                Id = id, Name = id,
                SellerId = sellerId,
                PaymentType = 2
            };

            using StringContent jsonContent = new(
                JsonSerializer.Serialize(item),
                Encoding.UTF8,
                "application/json");
            var tokenResponse = await Authenticate();

            HttpClient c = new HttpClient();
            c.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", tokenResponse.AccessToken);
            c.BaseAddress = new Uri("https://test.api.promisepay.com");

            var response = await c.PostAsync("items", jsonContent);

            return await response.Content.ReadFromJsonAsync<ItemsResponse?>();
        }

        public async Task<ItemWrapperResponse?> MakePayment(string itemId, string accountId)
        {

            var id = Guid.NewGuid().ToString();

            var data = new { account_id = accountId };

            using StringContent jsonContent = new(
                JsonSerializer.Serialize(data),
                Encoding.UTF8,
                "application/json");
            var tokenResponse = await Authenticate();

            HttpClient c = new HttpClient();
            c.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", tokenResponse.AccessToken);
            c.BaseAddress = new Uri("https://test.api.promisepay.com");

            var response = await c.PatchAsync($"items/{itemId}/make_payment", jsonContent);

            return await response.Content.ReadFromJsonAsync<ItemWrapperResponse?>();
        }
    }
}
