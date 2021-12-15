using ATM_Verification_System.Classes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ATM_Verification_System
{
    public partial class Login : Form
    {
        APIConnection connection = new APIConnection();
        public Login()
        {
            InitializeComponent();
            //this.Hide();
            //WebCam f = new WebCam();
            //f.Show();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string accountNo = tbName.Text;
            var loginResponse = connection.Login(accountNo);
            if(loginResponse != null)
            {
                if (loginResponse.data != null)
                {
                    //LogInfo.AccountNo = accountNo;
                    //LogInfo.Balance = loginResponse.data.balance;
                    this.Hide();
                    AuthenticationModes f = new AuthenticationModes(loginResponse.data.account_number, loginResponse.data.balance, loginResponse.data.phone_number);
                    f.ShowDialog();
                }
                else
                {
                    MessageBox.Show(loginResponse.status + ": " + loginResponse.message);
                    tbName.Text = "";
                }
            }
            else
            {
                tbName.Text = "";
            }
        }

        private void Login_Load(object sender, EventArgs e)
        {
            
        }
    }
}
