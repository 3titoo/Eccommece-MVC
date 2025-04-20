using BulkyWeb.Migrations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BulkyWeb.Models
{
    public class ShoppingCart
    {
        [Key]
        public int? Id { get; set; }

        [Required]
        [Range(minimum:1 ,maximum:1000, ErrorMessage = "Please enter a value between 1 and 1000")]
        
        public int Count { get; set; }

        public int? ProductId { get; set; }

        public string? ApplicationUserId { get; set; }


        [ForeignKey("ProductId")]
        [ValidateNever]
        public Product? Product { get; set; }

        [ForeignKey("ApplicationUserId")]
        [ValidateNever]
        public AppUser? ApplicationUser { get; set; }

        [NotMapped]
        public double Price { get; set; }
    }
}
