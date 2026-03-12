using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoopApplication.Domain.DTOs.RequestModels
{
    public record LoanTakenRequest
    {
        public Guid UserId { get; set; }

        public Guid LoanTypeId { get; set; }

        public decimal PrincipalAmount { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }
    }
}
