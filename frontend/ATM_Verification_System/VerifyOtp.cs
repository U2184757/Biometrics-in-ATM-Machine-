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
    public partial class VerifyOtp : Form
    {
        APIConnection connection = new APIConnection();
        string AccountNo;
        double Balance;
        string PhoneNo;
        public VerifyOtp(string accountNo, double balance, string phoneNo)
        {
            InitializeComponent();
            AccountNo = accountNo;
            Balance = balance;
            PhoneNo = phoneNo;
        }
        public VerifyOtp()
        {
            InitializeComponent();
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void btnVerify_Click(object sender, EventArgs e)
        {
            var response = connection.SendOtp(AccountNo, tbOtp.Text);
            if (response.status == "ok")
            {
                this.Hide();
                AccountTypes f = new AccountTypes(AccountNo,Balance,3,"");
                f.ShowDialog();
            }
            else
            {
                MessageBox.Show(response.status + ": " + response.message);
            }
        }

        private void VerifyOtp_Load(object sender, EventArgs e)
        {
            lblAccount.Text = AccountNo;
        }
    }
}
