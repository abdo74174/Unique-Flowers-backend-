using System.ComponentModel.DataAnnotations;

namespace FlowerShop.API.DTOs
{
    public class CreateProductDto
    {
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
        public string Category { get; set; }
    }
}
