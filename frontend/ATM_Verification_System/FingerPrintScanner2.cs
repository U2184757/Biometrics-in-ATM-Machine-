using DPFP.Capture;
using DPFP;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using System.Configuration;
using System.IO;
using System.Drawing.Imaging;
using System.Data;
using PatternRecognition.FingerprintRecognition.FeatureExtractors;
using PatternRecognition.FingerprintRecognition.Matchers;
using ATM_Verification_System.Classes;

namespace ATM_Verification_System
{
    class FingerPrintScanner2 : DPFP.Capture.EventHandler
    {
        APIConnection connection = new APIConnection();
        public static string AccountNo;
        public static string AccountHolderImageString;
        Form1 myForm = null;
        string myAmount;
        public Capture capture { get; set; } = new Capture();
        
        public void EnrollAndSavePicture(string accountNo, string accountHolderImageString, string amount, Form1 form)
        {
            //capture.StopCapture();
            AccountNo = accountNo;
            AccountHolderImageString = accountHolderImageString;
            myForm = form;
            myAmount = amount;
            capture.EventHandler = this;
            try
            {
                capture.StartCapture();
            }
            catch
            {
                MessageBox.Show("Cant Start Fingerprint Scan");
            }
        }
        public void EnrollAndSavePicture2(string accountNo, string accountHolderImageString, string amount)
        {
            //capture.StopCapture();
            AccountNo = accountNo;
            AccountHolderImageString = accountHolderImageString;
            myAmount = amount;
            capture.EventHandler = this;
            try
            {
                capture.StartCapture();
            }
            catch(Exception ex)
            {
                MessageBox.Show("Cant Start Fingerprint Scan");
            }
        }
        public void OnComplete(object capture, string readerSerialNumber, Sample sample)
        {
            try
            {
                var sampleConvertor = new SampleConversion();
                Bitmap bitmap = null;
                sampleConvertor.ConvertToPicture(sample, ref bitmap);
                try
                {
                    string path = @"C:\temp.jpg";
                    bitmap.Save(path);
                }
                catch { }
                
                MemoryStream tmpStream = new MemoryStream();
                bitmap.Save(tmpStream, ImageFormat.Jpeg); // change to other format
                var imagebytes = tmpStream.ToArray();
                string imageBase64 = Convert.ToBase64String(imagebytes);
                AutoClosingMessageBox.Show("Fingerprint Scanned ...", "Success", 2000);
                if(myForm != null)
                {
                    verify(imageBase64, AccountHolderImageString);
                }
                else
                {
                    verify2(imageBase64, AccountHolderImageString);
                }
                StopCapture();

            }
            catch(Exception ex)
            {
                MessageBox.Show("Fingerprint Verification Error!!");
                return;
            }
        }
        public void StopCapture()
        {
            if (capture != null)
            {
                try
                {
                    capture.StopCapture();
                }
                catch (Exception e)
                {
                    MessageBox.Show("ERROR AL DETENER DIGITAL-PERSONA");
                }
            }
        }
        public void verify2(string scannedImageString, string AccountHolderImageString)
        {
            AutoClosingMessageBox.Show("Verifying fingerprints", "Verifying", 2000);
            double score = match(scannedImageString, AccountHolderImageString);
            if (score >= 20.000)
            {
                AutoClosingMessageBox.Show("Fingerprint verified...", "Success", 2000);
                var response = connection.deductBalance(AccountNo, myAmount);
                if (response.data != null)
                {
                    string receiptMessage = "Account No:\t" + AccountNo + "\n\n" + "Transaction Amount:\t" + myAmount + "\n\n" + "Available Balance:\t\t" + response.data.balance;
                    MessageBox.Show("Cash Withrawl Successful...\n\n" + receiptMessage);
                    //var current = Form.ActiveForm;
                    //current.Hide();
                    Login f = new Login();
                    f.ShowDialog();
                    return;
                }
                else
                {
                    MessageBox.Show(response.status + ": " + response.message);
                }
            }
            else
            {
                MessageBox.Show(string.Format("Account not verified.\r\nFingerprints do not match"));
            }
        }
        public void verify(string scannedImageString , string AccountHolderImageString)
        {
            AutoClosingMessageBox.Show("Verifying fingerprints", "Verifying", 2000);
            double score = match(scannedImageString, AccountHolderImageString);
            if (score >= 20.000)
            {
                AutoClosingMessageBox.Show("Fingerprint verified...", "Success", 2000);
                //capture.StopCapture();
                //var current = Form.ActiveForm;
                //current.Hide();
                //myForm.Hide();
                AccountTypes f = new AccountTypes(AccountNo, Convert.ToDouble(myAmount), 1, AccountHolderImageString);
                f.ShowDialog();
                return;
            }
            else
            {
                MessageBox.Show(string.Format("Account not verified.\r\nFingerprints do not match"));
            }
        }
        public Bitmap Base64ToBitmap(string base64String)
        {
            byte[] imageAsBytes = Convert.FromBase64String(base64String);
            using (var ms = new MemoryStream(imageAsBytes))
            {
                return (Bitmap)Image.FromStream(ms);
            }
        }
        private double match(string query, string template)
        {
            // Loading fingerprints
            Bitmap fingerprintImg1 = Base64ToBitmap(query);
            Bitmap fingerprintImg2 = Base64ToBitmap(template);
            //// Building feature extractor and extracting features
            var featExtractor = new PNFeatureExtractor() { MtiaExtractor = new Ratha1995MinutiaeExtractor() };
            var features1 = featExtractor.ExtractFeatures(fingerprintImg1);
            var features2 = featExtractor.ExtractFeatures(fingerprintImg2);

            // Building matcher and matching
            var matcher = new PN();
            double similarity = matcher.Match(features1, features2);
            return similarity;
        }
        public byte[] imageToByte(Image img)
        {
            using (var ms = new MemoryStream())
            {
                img.Save(ms, ImageFormat.Png);
                return ms.ToArray();
            }
        }

        public void OnFingerGone(object capture, string readerSerialNumber) { }
        public void OnFingerTouch(object capture, string readerSerialNumber) { }
        public void OnReaderConnect(object capture, string readerSerialNumber) { }
        public void OnReaderDisconnect(object capture, string readerSerialNumber) { }
        public void OnSampleQuality(object capture, string readerSerialNumber, CaptureFeedback captureFeedback) { }
    }
    public class User
    {
        public int UserID { get; set; }
        public string UserName { get; set; }

        public int UserRecordID { get; set; }
        /// <summary>
        /// Redundant property, becoz need to check if everything is ok
        /// </summary>
        public string UserRecordName { get; set; }
        public string image { get; set; }
    }
}
