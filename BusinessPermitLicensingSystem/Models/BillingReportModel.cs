using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessPermitLicensingSystem.Models
{
    public class BillingReportModel
    {
        public string SIN { get; set; }
        public string FullName { get; set; }
        public string BusinessName { get; set; }
        public string BusinessSection { get; set; }
        public string StallNumber { get; set; }
        public string StallSize { get; set; }
        public double MonthlyRental { get; set; }

        public string PaymentStatus { get; set; }
    }
}
