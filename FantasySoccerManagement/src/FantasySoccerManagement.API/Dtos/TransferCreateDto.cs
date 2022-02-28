namespace FantasySoccerManagement.Api.Dtos
{
    public class TransferCreateDto
    {
        public Guid SellerId { get; set; }
        public Guid PlayerId { get; set; }
        public double AskingPrice { get; set; }
    }
}