
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace thegioididong.Models
{
    public class Product
    {
        [Key]
        public int Id_pro { get; set; }
        public string Pro_Name { get; set; }
        public string Des { get; set; }
        [Required]
        [ForeignKey("Manufacturer")]
        public int Id_manufacturer { get; set; }
        [Required]
        [ForeignKey("Category")]
        public int Id_Category { get; set; }
        public decimal Price { get; set; }
        public int Ram { get; set; }
        public int Rom {get ;set; }
        public string? Amount { get; set; }
        

        public Manufacturer Manufacturer { get; set; }
        public Category Category { get; set; }
        
        //public ICollection<Order_Detail> Order_Detail { get; set; }
        public ICollection<Image> Images { get; set; }
        public ICollection<Comment> Comments { get; set; }

    }
}
