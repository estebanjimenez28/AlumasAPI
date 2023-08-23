using System;
using System.Collections.Generic;

namespace AlumasAPI.Models
{
    public partial class Employee
    {
        public int EmployeeId { get; set; }
        public string EmployeeName { get; set; } = null!;
        public string BackUpEmail { get; set; } = null!;
        public string EmployeeAddress { get; set; } = null!;
        public string EmployeePhoneNumber { get; set; } = null!;
        public bool Active { get; set; }
        public int BranchBranchId { get; set; }

        public virtual Branch? BranchBranch { get; set; } = null!;
    }
}
