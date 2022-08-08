
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace thegioididong.Models
{
    public class Image
    {
        [Key]
        public int Id_image { get; set; }
        public string link_image { get; set; }
        [Required]
        [ForeignKey("Product")]
        public int Id_pro { get; set; }

        public Product Product { get; set; }    
    }
}
