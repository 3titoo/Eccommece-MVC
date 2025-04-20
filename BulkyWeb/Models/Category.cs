using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BulkyWeb.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "The Category Name is required.\r\n"),DisplayName("Category Name")]
        [MaxLength(30, ErrorMessage = "maximum length 3."),MinLength(2,ErrorMessage = "minimum length 2.")]
        public string Name { get; set; }
        [Required(ErrorMessage = "The Display Order is required.\r\n")]
        [DisplayName("Display Order"),Range(1,100,ErrorMessage = "must be between 1 and 100.")]
        public int DisplayOrder { get; set; }


    }
}
