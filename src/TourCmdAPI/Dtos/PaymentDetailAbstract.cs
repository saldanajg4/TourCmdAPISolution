namespace TourCmdAPI.Dtos
{
    public class PaymentDetailAbstract
    {
        public string CardOwnerName { get; set; }

        public string CardNumber { get; set; }
       
        public string ExpirationDate { get; set; } 
  
        public string CVV { get; set; }
    }
}