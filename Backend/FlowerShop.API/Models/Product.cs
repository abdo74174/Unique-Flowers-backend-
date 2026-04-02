using System;
using System.ComponentModel.DataAnnotations;

namespace FlowerShop.API.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(200)]
        public string TitleEn { get; set; }

        [Required]
        [StringLength(200)]
        public string TitleAr { get; set; }

        public string DescriptionEn { get; set; }
        public string DescriptionAr { get; set; }

        [Range(0, 10000.00)]
        public decimal Price { get; set; }

        public string ImageUrl { get; set; }

        [Required]
        public string Category { get; set; } // e.g., natural, artificial, vases

        public bool IsDeleted { get; set; } = false; // Enhancement: Soft Delete

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
