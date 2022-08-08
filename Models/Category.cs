using System.ComponentModel.DataAnnotations;
namespace thegioididong.Models
{
    public class Category
    {
        [Key]
        public int Id_Category { get; set; }
        public string Name_Category { get; set; }
        public ICollection<Product> Products { get; set; }
    }
}
