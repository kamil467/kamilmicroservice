﻿

namespace Pnk.Services.ProductAPI.Models.Dto
{
    public class ProductDto
    {
       
        public virtual int ProductId { get; set; }


        public virtual string Name { get; set; }


        public virtual double Price { get; set; }

        public virtual string Description { get; set; }

        public virtual string CategoryName { get; set; }

        public virtual string ImageUrl { get; set; }
    }
}
