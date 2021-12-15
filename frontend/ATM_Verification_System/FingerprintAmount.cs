using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ATM_Verification_System
{
    public partial class FingerprintAmount : Form
    {
        APIConnection connection = new APIConnection();
        string AccountNo;
        double Balance;
        string AccountHolderImageString;
        public FingerprintAmount(string accountNo, double balance, string accountHolderImageString)
        {
            InitializeComponent();
            AccountNo = accountNo;
            Balance = balance;
            AccountHolderImageString = accountHolderImageString;
        }
        public FingerprintAmount()
        {
            InitializeComponent();
        }

        private void FingerprintAmount_Load(object sender, EventArgs e)
        {
            lblAccount.Text = AccountNo;
            //lblBalance.Text = Balance.ToString();
        }

        private void btnWithDraw_Click(object sender, EventArgs e)
        {
            if (Convert.ToDouble(tbAmount.Text) > Balance)
            {
                MessageBox.Show("Amount Can't be greater than Available Balance!!");
            }
            else
            {
                var scanner2 = new FingerPrintScanner();
                scanner2.EnrollAndSavePicture2(AccountNo, AccountHolderImageString, tbAmount.Text);
                AutoClosingMessageBox.Show("Scanning Fingerprint", "Scan Finger", 1000);
                //AutoClosingMessageBox.Show("Scanning Fingerprint", "Scan Finger", 1500);
                //var response = connection.GetFingerPrintAuth(AccountNo);
                //if (response.message != null && response.message == "authorized")
                //{
                //    //string base64String = "";
                //    Thread.Sleep(2000);
                //    AutoClosingMessageBox.Show("Fingerprint Verified..", "Success", 2000);
                //    var response2 = connection.deductBalance(AccountNo, tbAmount.Text);
                //    if (response2.data != null)
                //    {
                //        string receiptMessage = "Account No:\t" + AccountNo + "\n\n" + "Transaction Amount:\t" + tbAmount.Text + "\n\n" + "Available Balance:\t\t" +  Balance;
                //        MessageBox.Show("Cash Withrawl Successful...\n\n" + receiptMessage);

                //        this.Hide();
                //        Login f = new Login();
                //        f.ShowDialog();
                //    }
                //    else
                //    {
                //        MessageBox.Show(response.status + ": " + response.message);
                //    }
                //}
                //else
                //{
                //    MessageBox.Show("Account Holder fingerprint not found!!");
                //}

            }
        }
    }
}
