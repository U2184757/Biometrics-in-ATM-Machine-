using ATM_Verification_System.Classes;
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
    public partial class OTPForm : Form
    {
        APIConnection connection = new APIConnection();
        string AccountNo;
        double Balance;
        //string PhoneNo;
        public OTPForm(string accountNo, double balance)
        {
            InitializeComponent();
            AccountNo = accountNo;
            Balance = balance;
            //PhoneNo = phoneNo;
        }
        public OTPForm()
        {
            InitializeComponent();
        }

        private void OTPForm_Load(object sender, EventArgs e)
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
                string otpValue = Prompt.ShowDialog("Enter Otp Code Again", "Verify");
                var response = connection.SendOtp(AccountNo, otpValue, tbAmount.Text);
                if(response.data != null)
                {
                    string receiptMessage = "Account No:\t" + AccountNo + "\n\n" + "Transaction Amount:\t" + tbAmount.Text + "\n\n" + "Available Balance:\t\t"+response.data.balance;
                    MessageBox.Show("Cash Withrawl Successful...\n\n"+ receiptMessage);
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
    }
}
