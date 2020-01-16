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



namespace JoinstarCard
{
    public partial class CaptureImageForm : Form
    {
        private NewCardForm nw = new NewCardForm();

        public CaptureImageForm(NewCardForm now)
        {
            InitializeComponent();

            this.nw = now;
        }
        FilterInfoCollection filterInfoCollection;
        VideoCaptureDevice captureDevice;


        private void button2_Click(object sender, EventArgs e)
        {
            nw.Visible = true;

            this.Visible = false;




        }

        private void CaptureImageForm_Load(object sender, EventArgs e)
        {

            filterInfoCollection = new FilterInfoCollection(FilterCategory.VideoInputDevice);
            foreach (FilterInfo filterInfo in filterInfoCollection)
            {
                comboBox1.Items.Add(filterInfo.Name);
            }


            comboBox1.SelectedIndex = 0;
            captureDevice = new VideoCaptureDevice(filterInfoCollection[comboBox1.SelectedIndex].MonikerString);
            captureDevice.NewFrame += captureDevice_NewFrame;
            captureDevice.Start();

        }

        private void captureDevice_NewFrame(object sender, NewFrameEventArgs eventArgs)
        {
            pictureBox1.Image = (Bitmap)eventArgs.Frame.Clone();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            pictureBox2.Image = (Bitmap)pictureBox1.Image;
        }
    }
}
