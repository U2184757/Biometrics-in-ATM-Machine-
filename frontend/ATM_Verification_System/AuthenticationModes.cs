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
    public partial class AuthenticationModes : Form
    {
        APIConnection connection = new APIConnection();
        string AccountNo;
        double Balance;
        string PhoneNo;
        public AuthenticationModes(string accountNo, double balance, string phoneNo)
        {
            InitializeComponent();
            AccountNo = accountNo;
            Balance = balance;
            PhoneNo = phoneNo;
        }
        public AuthenticationModes()
        {
            InitializeComponent();
        }
        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            var response = connection.GetFingerPrint(AccountNo);
            if (response.base64String != null && response.base64String != "")
            {
                string base64String = response.base64String;
                this.Hide();
                Form1 f = new Form1(AccountNo, Balance, base64String);
                f.ShowDialog();
            }
            else
            {
                MessageBox.Show("Failed To get Fingerprint From Server!!");
            }
            //this.Hide();
            //Form1 f = new Form1(AccountNo, Balance, "");
            //f.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            WebCam f = new WebCam(AccountNo, Balance, PhoneNo);
            f.ShowDialog();
        }

        private void AuthenticationModes_Load(object sender, EventArgs e)
        {
            lblAccount.Text = AccountNo;
            //lblBalance.Text = Balance.ToString();
        }

        private void btnOtp_Click(object sender, EventArgs e)
        {
            var response = connection.SendOtp(AccountNo);
            if(response != null)
            {
                if(response.data != null)
                {
                    this.Hide();
                    VerifyOtp f = new VerifyOtp(response.data.account_id, Balance, response.data.phone_number);
                    f.ShowDialog();
                }
                else
                {
                    MessageBox.Show(response.status + ": " + response.message);
                }
            }
            else
            {
                MessageBox.Show("Server Error: Failed To get Response!!");
            }
            
        }
    }
}
