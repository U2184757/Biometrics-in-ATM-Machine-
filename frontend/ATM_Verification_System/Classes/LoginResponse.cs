using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATM_Verification_System.Classes
{
    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse); 
    public class Data
    {
        public string account_number { get; set; }
        public string phone_number { get; set; }
        public double balance { get; set; }
        public string last_authentication { get; set; }
    }

    public class LoginResponse
    {
        public string status { get; set; }
        public string message { get; set; }
        public Data data { get; set; }
    }
}
