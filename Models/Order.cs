using System.ComponentModel.DataAnnotations;
namespace thegioididong.Models
{
    public class Order
    {
        [Key]
        public int order_id { get; set; }
        public string pro_name { get; set; }
        public decimal pro_price { get; set; }
        public string pro_image { get; set; }
        public string namecus { get; set; }
        public string phone { get; set; }
        public string address { get; set; }
    }
}
