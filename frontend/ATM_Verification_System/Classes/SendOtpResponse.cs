using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATM_Verification_System.Classes
{
    public class OTPData
    {
        public string account_id { get; set; }
        public string phone_number { get; set; }
        public double balance { get; set; }
        public string last_authentication { get; set; }
    }

    public class SendOtpResponse
    {
        public string status { get; set; }
        public string message { get; set; }
        public OTPData data { get; set; }
    }
}
