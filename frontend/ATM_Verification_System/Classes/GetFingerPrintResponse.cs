using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATM_Verification_System.Classes
{
    class GetFingerPrintResponse
    {
        public string status { get; set; }
        public string message { get; set; }
        public string base64String { get; set; }
    }
}
