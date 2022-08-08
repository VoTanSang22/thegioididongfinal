using System.ComponentModel.DataAnnotations;
namespace thegioididong.Models
{
    public class Manufacturer
    {
        [Key]
        public int Id_manufacturer { get; set; } 
        public string Name_manufacturer { get; set; }
        public ICollection<Product> Products { get; set; }
    }
}
