using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ATM_Verification_System
{
    public partial class WebCamAmount : Form
    {
        APIConnection connection = new APIConnection();
        string AccountNo;
        double Balance;
        string ImageString;
        public WebCamAmount(string accountNo, double balance, string imageString)
        {
            InitializeComponent();
            AccountNo = accountNo;
            Balance = balance;
            ImageString = imageString;
        }
        public WebCamAmount()
        {
            InitializeComponent();
        }

        private void WebCamAmount_Load(object sender, EventArgs e)
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
                AutoClosingMessageBox.Show("Face Recognition Starts..", "Verifying", 2000);
                verify(ImageString);
                var response = connection.SubmitFace(AccountNo, ImageString, tbAmount.Text);
                if (response.data != null)
                {
                    string receiptMessage = "Account No:\t" + AccountNo + "\n\n" + "Transaction Amount:\t" + tbAmount.Text + "\n\n" + "Available Balance:\t\t" + response.data.balance;
                    MessageBox.Show("Cash Withrawl Successful...\n\n" + receiptMessage);
                    this.Hide();
                    Login f = new Login();
                    f.ShowDialog();
                }
                else
                {
                    MessageBox.Show(response.status + ": " + response.message);
                }
            }
        }
        public void verify(string imageString)
        {
            var response = connection.FaceRecognition(AccountNo, imageString);
            if (response.status != "error")
            {
                AutoClosingMessageBox.Show("Face Authenticated Successfully...", "Success", 1500);
            }
            else
            {
                MessageBox.Show(response.status + ": " + response.message);
                this.Hide();
                Login f = new Login();
                f.ShowDialog();
            }
        }
    }
}
