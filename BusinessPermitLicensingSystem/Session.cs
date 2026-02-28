using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessPermitLicensingSystem
{
    public static class Session
    {
        public static long? CurrentUserId { get; set; }
        public static string? CurrentUsername { get; set; }
    }
}

