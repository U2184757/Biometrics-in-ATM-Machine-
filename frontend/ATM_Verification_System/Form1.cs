using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using PatternRecognition.FingerprintRecognition.Core;
using PatternRecognition.FingerprintRecognition.FeatureExtractors;
using PatternRecognition.FingerprintRecognition.Matchers;
using MySql.Data.MySqlClient;
using System.Configuration;
using System.IO;
using System.Data.SqlClient;
using System.Threading;

namespace ATM_Verification_System
{
    public partial class Form1 : Form
    {
        APIConnection connection = new APIConnection();
        string AccountNo ="";
        double Balance = 0;
        string AccountHolderImageString="";
        public Form1(string accountNo, double balance, string base64String)
        {
            InitializeComponent();
            AccountNo = accountNo;
            Balance = balance;
            AccountHolderImageString = base64String;
        }
        public Form1()
        {
            InitializeComponent();
        }
        
        private void userEnroll_Click(object sender, EventArgs e)
        {
            //AutoClosingMessageBox.Show("Scanning Fingerprint", "Scan Finger", 1500);
            //var response = connection.GetFingerPrintAuth(AccountNo);
            //if (response.message != null && response.message == "authorized")
            //{
            //    string base64String = "";
            //    Thread.Sleep(2000);
            //    AutoClosingMessageBox.Show("Fingerprint Verified..", "Success", 2000);
            //    this.Hide();
            //    AccountTypes f = new AccountTypes(AccountNo, Balance, 1, base64String);
            //    f.ShowDialog();
            //}
            //else
            //{
            //    MessageBox.Show("Account Holder fingerprint not found!!");
            //}
            if (AccountHolderImageString == null || AccountHolderImageString == "")
            {
                MessageBox.Show("Account Holder fingerprint not found");

            }
            else
            {
                var scanner = new FingerPrintScanner2();
                scanner.EnrollAndSavePicture(AccountNo, AccountHolderImageString ,Balance.ToString(), this);
                AutoClosingMessageBox.Show("Scanning Fingerprint", "Scan Finger", 1000);

            }
        }

        private Bitmap Change_Resolution(string file)
        {
            using (Bitmap bitmap = (Bitmap)Image.FromFile(file))
            {
                using (Bitmap newBitmap = new Bitmap(bitmap))
                {
                    newBitmap.SetResolution(500,500);
                    return newBitmap;
                }
            }
        }
        
       
        private void Form1_Load(object sender, EventArgs e)
        {
            lblAccount.Text = AccountNo;
            //lblBalance.Text = Balance.ToString();
        }

        delegate IntPtr GetFormHandleDelegate();

        private IntPtr GetFormHandle()
        {

            if (this.InvokeRequired)
            {

                return (IntPtr)this.Invoke((GetFormHandleDelegate)delegate () {

                    return GetFormHandle();

                });
            }

            return this.Handle;
        }

    }
}
