using ATM_Verification_System.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using System.Windows.Forms;

namespace ATM_Verification_System
{
    class APIConnection
    {
        private string base_url = "https://3b30-103-153-38-115.ngrok.io/";
        public string Call_api(string raw, string Url)
        {
            string responsebody = "";
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3 | SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;
            using (var client = new WebClient())
            {
                try
                {
                    client.Headers[HttpRequestHeader.ContentType] = "application/x-www-form-urlencoded";
                    responsebody = client.UploadString(Url, raw);

                }
                catch
                {
                    MessageBox.Show("Server Error: Failed To get Response!!");
                }
            }
            return responsebody;
        }
        public LoginResponse Login(string accountNo)
        {
            string action = base_url + "login/";
            string form_data = "account=" + accountNo;
            string response = Call_api(form_data, action);
            var serializer = new JavaScriptSerializer();
            var res_obj = serializer.Deserialize<LoginResponse>(response);
            return res_obj;
        }
        public SendOtpResponse SendOtp(string accountNo)
        {
            string action = base_url + "api_send_otp/";
            string form_data = "account_id=" + accountNo;
            string response = Call_api(form_data, action);
            var serializer = new JavaScriptSerializer();
            var res_obj = serializer.Deserialize<SendOtpResponse>(response);
            return res_obj;
        }
        public LoginResponse SendOtp(string accountNo, string otp)
        {
            string action = base_url + "submit_otp/";
            string form_data = "account_id=" + accountNo + "&otp=" + otp;
            string response = Call_api(form_data, action);
            var serializer = new JavaScriptSerializer();
            var res_obj = serializer.Deserialize<LoginResponse>(response);
            return res_obj;
        }
        public LoginResponse SendOtp(string accountNo, string otp, string amount)
        {
            string action = base_url + "submit_otp/";
            string form_data = "account_id=" + accountNo + "&otp=" + otp + "&amount=" + amount;
            string response = Call_api(form_data, action);
            var serializer = new JavaScriptSerializer();
            var res_obj = serializer.Deserialize<LoginResponse>(response);
            return res_obj;
        }
        public LoginResponse FaceRecognition(string accountNo, string base64image)
        {
            string action = base_url + "FaceRegognation/";
            string form_data = "account_id=" + accountNo + "&canvasData=" + base64image;
            string response = Call_api(form_data, action);
            var serializer = new JavaScriptSerializer();
            var res_obj = serializer.Deserialize<LoginResponse>(response);
            return res_obj;
        }
        public LoginResponse SubmitFace(string accountNo, string imageString, string amount)
        {
            string action = base_url + "submitFace/";
            string form_data = "account_id=" + accountNo + "&canvasData=" + imageString + "&amount=" + amount;
            string response = Call_api(form_data, action);
            var serializer = new JavaScriptSerializer();
            var res_obj = serializer.Deserialize<LoginResponse>(response);
            return res_obj;
        }
        public GetFingerPrintResponse GetFingerPrint(string accountNo)
        {
            string action = base_url + "getFingerprint/";
            string form_data = "account_id=" + accountNo;
            string response = Call_api(form_data, action);
            var serializer = new JavaScriptSerializer();
            var res_obj = serializer.Deserialize<GetFingerPrintResponse>(response);
            return res_obj;
        }
        public GetFingerPrintResponse GetFingerPrintAuth(string accountNo)
        {
            string action = base_url + "FingerPrintAuth/";
            string form_data = "account_id=" + accountNo;
            string response = Call_api(form_data, action);
            var serializer = new JavaScriptSerializer();
            var res_obj = serializer.Deserialize<GetFingerPrintResponse>(response);
            return res_obj;
        }
        public LoginResponse deductBalance(string accountNo, string amount)
        {
            string action = base_url + "deductbalance/";
            string form_data = "account_id=" + accountNo + "&amount=" + amount;
            string response = Call_api(form_data, action);
            var serializer = new JavaScriptSerializer();
            var res_obj = serializer.Deserialize<LoginResponse>(response);
            return res_obj;
        }
    }
}
