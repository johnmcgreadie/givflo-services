namespace GivFlowAPI.Dtos
{
    public class MakeDonationDto
    {
        public string BuyerId { get; set; }
        public string AccountId { get; set; }
        public string CampaignId { get; set; }
        public int Amount { get; set; }
    }
}
