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
    public partial class AccountTypes : Form
    {
        APIConnection connection = new APIConnection();
        string AccountNo;
        double Balance;
        string AuthData;
        int AuthMethod;
        public AccountTypes(string accountNo, double balance, int authMethod, string authData="")
        {
            InitializeComponent();
            AccountNo = accountNo;
            Balance = balance;
            AuthMethod = authMethod;
            AuthData = authData;
        }
        public AccountTypes()
        {
            InitializeComponent();
        }

        private void Current_Click(object sender, EventArgs e)
        {
            if (AuthMethod == 1)
            {
                this.Hide();
                FingerprintAmount finger = new FingerprintAmount(AccountNo, Balance,AuthData);
                finger.ShowDialog();
            }
            else if (AuthMethod == 2)
            {
                this.Hide();
                WebCamAmount f = new WebCamAmount(AccountNo, Balance, AuthData);
                f.ShowDialog();
            }
            else if (AuthMethod == 3)
            {
                this.Hide();
                OTPForm form = new OTPForm(AccountNo,Balance);
                form.ShowDialog();
            }
        }

        private void AccountTypes_Load(object sender, EventArgs e)
        {
            lblAccount.Text = AccountNo;
        }
    }
}
