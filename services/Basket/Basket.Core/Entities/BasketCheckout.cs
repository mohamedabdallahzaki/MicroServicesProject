
namespace Basket.Core.Entities
{
    public class BasketCheckout
    {
        public string UserName { get; set; }
        public decimal ToatlPrice { get; set; }

        public string FristName { get; set; }

        public string LastName {  get; set; }

        public string EmailAdderss { get;set; }

        public string Country { get; set; }

        public string City { get; set; }

        public string ZipCode { get; set; }


       public string  CardName { get; set; }
       public string CardNumber { get;set;}
       public string Expiration { get; set; }

       public string CVV { get; set; }

      public int DeliveryMethode { get; set; }



         

    }
}
