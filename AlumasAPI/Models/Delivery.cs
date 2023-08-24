using System;
using System.Collections.Generic;

namespace AlumasAPI.Models
{
    public partial class Delivery
    {
        public int DeliveryId { get; set; }
        public string Address { get; set; } = null!;
        public string Description { get; set; } = null!;
        public int ClientsClientId { get; set; }
        public bool Active { get; set; }

        public virtual Client? ClientsClient { get; set; } = null!;
    }
}
