using System;
using System.Collections.Generic;

namespace AlumasAPI.Models
{
    public partial class Product
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; } = null!;
        public decimal Quantity { get; set; }
        public int Price { get; set; }
        public bool Active { get; set; }
        public int BranchBranchId { get; set; }
        public int ClientsClientId { get; set; }
        public int ProductCategoryProductCategoryId { get; set; }

        public virtual Client? ClientsClient { get; set; } = null!;
        public virtual ProductCategory ProductCategoryProductCategory { get; set; } = null!;
    }
}
