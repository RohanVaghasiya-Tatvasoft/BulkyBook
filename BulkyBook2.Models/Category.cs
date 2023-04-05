using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BulkyBook2.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [DisplayName("Display Order :)")]
        [Range(1,100,ErrorMessage ="Bro it should be only between 1 to 100... stupid fucker")]
        public int DisplayOrder { get; set; }
        public DateTime CreatedDateTime { get; set; } = DateTime.Now;
    }
}
