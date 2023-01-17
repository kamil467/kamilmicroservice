using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Pnk.Services.ProductAPI.Models
{
    public class Product
    {
        [Key]
        public virtual int ProductId { get; set; }

        [Required]
        public virtual string Name { get; set; }

        [Range(1,1000)]
        public virtual double Price { get; set; }

        public virtual string Description { get; set; }

        public virtual string CategoryName { get; set; }

        public virtual string ImageUrl { get; set; }
    }
}
