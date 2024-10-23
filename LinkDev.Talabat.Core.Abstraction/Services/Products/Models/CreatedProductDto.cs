﻿using Microsoft.AspNetCore.Http;

namespace LinkDev.Talabat.Core.Abstraction.Services.Products.Models
{
    public class CreatedProductDto
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public required string Description { get; set; }
        public IFormFile? Image { get; set; }
        public decimal Price { get; set; }
        public int? BrandId { get; set; }
        public string? Brand { get; set; }
        public int? CategoryId { get; set; }
        public string? Category { get; set; }
        public string? CreatedOn { get; set; }
        public string? LastModifiedOn { get; set; }
    }
}
