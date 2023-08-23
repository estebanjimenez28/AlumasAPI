using System;
using System.Collections.Generic;

namespace AlumasAPI.Models
{
    public partial class Client
    {
        public Client()
        {
            Deliveries = new HashSet<Delivery>();
            Products = new HashSet<Product>();
        }

        public int ClientId { get; set; }
        public string ClientName { get; set; } = null!;
        public string Address { get; set; } = null!;
        public string PhoneNumber { get; set; } = null!;
        public string BackUpEmail { get; set; } = null!;
        public bool Active { get; set; }
        public int BranchBranchId { get; set; }

        public virtual Branch? BranchBranch { get; set; } = null!;
        public virtual ICollection<Delivery> Deliveries { get; set; }
        public virtual ICollection<Product> Products { get; set; }
    }
}
