using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoopApplication.Domain.Enums
{
    public enum LoanStatus
    {
        Pending = 0,
        Approved = 1,
        Ongoing = 2,
        PaidOff = 3,
        Defaulted = 4
    }
}
