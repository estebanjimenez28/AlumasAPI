using System;
using System.Collections.Generic;

namespace AlumasAPI.Models
{
    public partial class ProductCategory
    {
        public ProductCategory()
        {
            Products = new HashSet<Product>();
        }

        public int ProductCategoryId { get; set; }
        public string ProductCategoryName { get; set; } = null!;
        public string Description { get; set; } = null!;

        public virtual ICollection<Product> Products { get; set; }
    }
}
