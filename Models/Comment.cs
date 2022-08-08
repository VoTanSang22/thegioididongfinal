using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace thegioididong.Models
{
    public class Comment
    {
        [Key]
        public int Id_Com { get; set; }
        [Required]
        [ForeignKey("Product")]
        public int Id_pro { get; set; }
        public string Name_cus { get; set; }
        public string   phone { get; set; }
        public string mail { get; set; }
        public string content { get; set; }
        public Product Product { get; set; }
    }
}
