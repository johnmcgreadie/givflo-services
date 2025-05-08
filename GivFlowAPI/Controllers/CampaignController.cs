using System.Text;
using GivFlow.Data;
using GivFlow.Data.Configuration;
using GivFlowAPI.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using QRCoder;
using Stripe.Terminal;
using static System.Net.Mime.MediaTypeNames;
using Service = Stripe.Service;

namespace GivFlowAPI.Controllers;

[ApiController]
[Route("campaign")]
[Authorize]
public class CampaignController : ControllerBase
{
    private readonly ILogger<DonationController> _logger;
    private readonly IOptions<ConnectionStrings> _connectionStringsOptions;
    private readonly IOptions<BackendService> _serviceOptions;

    public CampaignController(ILogger<DonationController> logger, IOptions<ConnectionStrings> connectionStringsOptions, IOptions<BackendService> serviceOptions)
    {
        _logger = logger;
        _connectionStringsOptions = connectionStringsOptions;
        _serviceOptions = serviceOptions;
    }

    [HttpGet]
    [Authorize]
    public List<Campaign> Get(long organisationId)
    {
        using (var context = new GivFlowContext(_connectionStringsOptions.Value.GivFlowAuthConnection))
        {
            return context.Campaigns.Where(c => c.OrganisationId == organisationId).ToList();
        }
    }

    [HttpPost]
    public CampaignDto Post(CampaignDto campaignDto)
    {
        using (var context = new GivFlowContext(_connectionStringsOptions.Value.GivFlowAuthConnection))
        {
            var organisation = context.Organisations.FirstOrDefault(o => o.OrganisationId == campaignDto.OrganisationId);

            if (organisation != null)
            {
                var guid = Guid.NewGuid().ToString();

                var encodeText = $"{_serviceOptions.Value.BaseUrl}/donate/{organisation.Guid}/{guid}";

                using (QRCodeGenerator qrGenerator = new QRCodeGenerator())
                using (QRCodeData qrCodeData = qrGenerator.CreateQrCode(encodeText, QRCodeGenerator.ECCLevel.Q))
                using (PngByteQRCode qrCode = new PngByteQRCode(qrCodeData))
                {
                    byte[] qrCodeImage = qrCode.GetGraphic(20);

                    var campaign = new Campaign()
                    {
                        Name = campaignDto.Name,
                        Description = campaignDto.Description,
                        Organisation = organisation,
                        Guid = guid,
                        Qrcode = "data:image/png;base64," + Convert.ToBase64String(qrCodeImage)
                    };

                    context.Campaigns.Add(campaign);
                    context.SaveChanges();
                }

                return campaignDto;
            }

            return null;
        }
    }
}
