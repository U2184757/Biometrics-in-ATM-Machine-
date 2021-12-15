using AForge.Video;
using AForge.Video.DirectShow;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ATM_Verification_System
{
    public partial class WebCam : Form
    {
        FilterInfoCollection filterInfoCollection;
        VideoCaptureDevice videoCaptureDevice;
        APIConnection connection = new APIConnection();
        string AccountNo;
        double Balance;
        string PhoneNo;
        public WebCam(string accountNo, double balance, string phoneNo)
        {
            InitializeComponent();
            AccountNo = accountNo;
            Balance = balance;
            PhoneNo = phoneNo;
        }
        public WebCam()
        {
            InitializeComponent();
        }

        private void WebCam_Load(object sender, EventArgs e)
        {
            filterInfoCollection = new FilterInfoCollection(FilterCategory.VideoInputDevice);
            foreach (FilterInfo Device in filterInfoCollection)
            {
                cboCamera.Items.Add(Device.Name);
            }
            cboCamera.SelectedIndex = 0;
            videoCaptureDevice = new VideoCaptureDevice();
        }
        private void WebCam_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (videoCaptureDevice.IsRunning == true)
            {
                videoCaptureDevice.Stop();
            }
        }
        private void btnStart_Click(object sender, EventArgs e)
        {
            videoCaptureDevice = new VideoCaptureDevice(filterInfoCollection[cboCamera.SelectedIndex].MonikerString);
            videoCaptureDevice.NewFrame += FinalFrame_NewFrame;
            videoCaptureDevice.Start();
        }
        private void FinalFrame_NewFrame(object sender, NewFrameEventArgs eventArgs)
        {
            picCamera.Image = (Bitmap)eventArgs.Frame.Clone();
        }
        
        private void Capture_Click(object sender, EventArgs e)
        {
            if (picCamera.Image != null)
            {
                //Bitmap varBmp = new Bitmap(picCamera.Image);
                //Bitmap newBitmap = new Bitmap(varBmp);
                //varBmp.Save(@"C:\image\a.png", ImageFormat.Png);
                ////Now Dispose to free the memory
                //varBmp.Dispose();
                //varBmp = null;
                pictureBox1.Image = (Bitmap)picCamera.Image.Clone();
                using (var memoryStream = new MemoryStream())
                {
                    pictureBox1.Image.Save(memoryStream, ImageFormat.Jpeg);
                    var imagebytes = memoryStream.ToArray();
                    string imageBase64 = Convert.ToBase64String(imagebytes);
                    verify(imageBase64);
                }
            }
            else
            {
                MessageBox.Show("No Camera Video Found!!");
            }
        }
        public void verify(string imageString)
        {
            var response = connection.FaceRecognition(AccountNo, imageString);
            if(response.status != "error")
            {
                MessageBox.Show("Face Authenticated Successfully...");
                this.Hide();
                //WebCamAmount f = new WebCamAmount(AccountNo, Balance, imageString);
                AccountTypes f = new AccountTypes(AccountNo, Balance, 2, imageString);
                f.ShowDialog();
            }
            else
            {
                MessageBox.Show(response.status + ": " + response.message);
            }
        }
        private void ResetAll_Click(object sender, EventArgs e)
        {
            videoCaptureDevice.Stop();
            picCamera.Image = null;
            picCamera.Invalidate();
            pictureBox1.Image = null;
            pictureBox1.Invalidate();
        }

        private void Pause_Click(object sender, EventArgs e)
        {
            videoCaptureDevice.Stop();
        }

        private void Exit_Click(object sender, EventArgs e)
        {
            if(videoCaptureDevice.IsRunning == true)
            {
                videoCaptureDevice.Stop();
            }
            Application.Exit(null);
        }
    }
}
