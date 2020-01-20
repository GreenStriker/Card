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
        private string name;
        private Image img;
        private string phone;
        private string email;
        private string cardNo;

        public CaptureImageForm(string cardNo, string email, string phone ,string name)
        {
            InitializeComponent();

            this.name = name;
            this.phone = phone;
            this.email = email;
            this.cardNo = cardNo;


        }
        FilterInfoCollection filterInfoCollection;
        VideoCaptureDevice captureDevice;


        private void button2_Click(object sender, EventArgs e)
        {
            pictureBox2.MouseDown += new MouseEventHandler(pictureBox2_MouseDown);

            pictureBox2.MouseMove += new MouseEventHandler(pictureBox2_MouseMove);

            pictureBox2.MouseEnter += new EventHandler(pictureBox2_MouseEnter);
            //int y = (this.pictureBox2.Size.Width * 3) / 4;
            //this.pictureBox2.Size = new System.Drawing.Size(this.pictureBox2.Size.Width, y);
            Controls.Add(pictureBox2);

            // this.pictureBox2.Size.Height = (this.pictureBox2.Size.Width * 0.75);


         

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
       //     pictureBox1.Image.Dispose();

            pictureBox1.Image = (Bitmap)eventArgs.Frame.Clone();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            pictureBox2.Image = (Bitmap)pictureBox1.Image;
            captureDevice.Stop();

        }
        public Pen crpPen = new Pen(Color.White);
        int crpX, crpY, rectW, rectH;

        private void button3_Click(object sender, EventArgs e)
        {

            try
            {
                pictureBox2.SizeMode = PictureBoxSizeMode.StretchImage;
                //  label2.Text = "Dimensions :" + rectW + "," + rectH;
                Cursor = Cursors.Default;
                //Now we will draw the cropped image into pictureBox2
                Bitmap bmp2 = new Bitmap(pictureBox2.Width, pictureBox2.Height);
                pictureBox2.DrawToBitmap(bmp2, pictureBox2.ClientRectangle);

                Bitmap crpImg = new Bitmap(rectW, rectH);

                for (int i = 0; i < rectW; i++)
                {
                    for (int y = 0; y < rectH; y++)
                    {
                        Color pxlclr = bmp2.GetPixel(crpX + i, crpY + y);
                        crpImg.SetPixel(i, y, pxlclr);
                    }
                }

                pictureBox2.Image = (Image)crpImg;
                pictureBox2.SizeMode = PictureBoxSizeMode.CenterImage;


                NewCardForm nowIsTheTime = new NewCardForm(cardNo, email, phone, name, pictureBox2.Image);
                captureDevice.Stop();
                nowIsTheTime.Visible = true;
                this.Visible = false;



            }
            catch(Exception e1 )
            {

                NewCardForm nowIsTheTime = new NewCardForm(cardNo, email, phone, name, pictureBox2.Image);
                captureDevice.Stop();
                nowIsTheTime.Visible = true;
                this.Visible = false;


            }
           
        }

        private void pictureBox2_MouseDown(object sender, MouseEventArgs e)
        {
            base.OnMouseDown(e);

            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                Cursor = Cursors.Cross;
                crpPen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dot;
                // set initial x,y co ordinates for crop rectangle
                //this is where we firstly click on image
                crpX = e.X;
                crpY = e.Y;

            }
        }

        private void pictureBox2_MouseEnter(object sender, EventArgs e)
        {
            base.OnMouseEnter(e);
            Cursor = Cursors.Cross;
        }

        private void pictureBox2_MouseMove(object sender, MouseEventArgs e)
        {
            base.OnMouseMove(e);
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                pictureBox2.Refresh();
                //set width and height for crop rectangle.
                rectW = e.X - crpX;
                rectH = e.Y - crpY;
                Graphics g = pictureBox2.CreateGraphics();
                g.DrawRectangle(crpPen, crpX, crpY, rectW, rectH);
                g.Dispose();
            }
        }

        protected override void OnMouseEnter(EventArgs e)
        {
            base.OnMouseEnter(e);
            Cursor = Cursors.Default;
        }
    }
}
