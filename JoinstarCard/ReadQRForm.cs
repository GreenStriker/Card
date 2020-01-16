using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AForge.Video;
using AForge.Video.DirectShow;
using ZXing;

namespace JoinstarCard
{
    public partial class ReadQRForm : Form
    {
        public ReadQRForm()
        {
            InitializeComponent();
        }

        FilterInfoCollection filterInfoCollection;
        VideoCaptureDevice captureDevice;

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void ReadQRForm_Load(object sender, EventArgs e)
        {
            filterInfoCollection = new FilterInfoCollection(FilterCategory.VideoInputDevice);
            foreach(FilterInfo filterInfo in filterInfoCollection)
            {
                cboDevice.Items.Add(filterInfo.Name);
            }


            cboDevice.SelectedIndex = 0;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            captureDevice = new VideoCaptureDevice(filterInfoCollection[cboDevice.SelectedIndex].MonikerString);
            captureDevice.NewFrame += captureDevice_NewFrame;
            captureDevice.Start();
            timer1.Start();
        }

        private void captureDevice_NewFrame(object sender, NewFrameEventArgs eventArgs)
        {
            pictureBox1.Image = (Bitmap)eventArgs.Frame.Clone();
        }

        private void ReadQRForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if(captureDevice.IsRunning)
            {
                captureDevice.Stop();
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if(pictureBox1.Image !=null)
            {

                BarcodeReader barcodeReader = new BarcodeReader();
                Result result = barcodeReader.Decode((Bitmap)pictureBox1.Image);
                if(result != null)
                {


                    NewCardForm nw = new NewCardForm(result.ToString());

                    nw.Visible = true;
                    this.Visible = false;
                   // MessageBox.Show(""+result.ToString());
                    timer1.Stop();
                    if (captureDevice.IsRunning)
                    {
                        captureDevice.Stop();
                    }
                }
            }
        }
    }
}
