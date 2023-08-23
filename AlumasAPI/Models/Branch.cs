using System;
using System.Collections.Generic;

namespace AlumasAPI.Models
{
    public partial class Branch
    {
        public Branch()
        {
            Clients = new HashSet<Client>();
            Employees = new HashSet<Employee>();
        }

        public int BranchId { get; set; }
        public string Name { get; set; } = null!;
        public string PhoneNumber { get; set; } = null!;
        public string Address { get; set; } = null!;
        public string Email { get; set; } = null!;
        public bool Active { get; set; }

        public virtual ICollection<Client> Clients { get; set; }
        public virtual ICollection<Employee> Employees { get; set; }
    }
}
