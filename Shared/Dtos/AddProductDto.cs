using System;
using System.Collections.Generic;

namespace Shared.Dtos
{
    public class AddProductDto
    {
        public string ArticleNumber { get; set; } = null!;
        public string Title { get; set; } = null!;
        public string Description { get; set; } = null!;
        public decimal Price { get; set; }
        public string CategoryName { get; set; } = null!;
    }
}
