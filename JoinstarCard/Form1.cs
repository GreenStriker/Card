using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;

// ===============================
// AUTHOR : Peter Nagy # github.com/petusa # peter.x.nagy@gmail.com
// CREATE DATE : 18 March 2018
// PURPOSE : minimal sample app for CroppablePictureBox component
// SPECIAL NOTES : licensed under MIT License
// CREDITS : sample image from pixabay.com
// Change History : 
//  Initial version - 18 March 2018
// ===============================
namespace JoinstarCard
{
    public partial class Form1 : Form
    {
        private Image originalImage;
        private string name;
        private Image img;
        private string phone;
        private string email;
        private string cardNo;
        public static Image resizeImage(Image imgToResize, Size size)
        {
            return (Image)(new Bitmap(imgToResize, size));
        }


        public Form1(string cardNo, string email, string phone, string name, Image img)
        {
            InitializeComponent();
            this.originalImage = this.croppablePictureBox1.Image;
            this.name = name;
            this.phone = phone;
            this.email = email;
            this.cardNo = cardNo;
          
          
            this.croppablePictureBox1.Image = resizeImage(img, new Size(635, 477)); 

        }


        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                this.croppablePictureBox1.Image = Image.FromFile(dialog.FileName);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Bitmap croppedImage = this.croppablePictureBox1.GetCroppedBitmap();

          //  MessageBox.Show(""+croppedImage);
            if (croppedImage == null) return;
            
            //SaveFileDialog dialog = new SaveFileDialog();
            //if (dialog.ShowDialog() == DialogResult.OK)
            //{
                this.croppablePictureBox1.Image = croppedImage;
            //this.croppablePictureBox1.SizeMode = PictureBoxSizeMode.Normal;
            NewCardForm nowIsTheTime = new NewCardForm(cardNo, email, phone, name, croppablePictureBox1.Image);
            //captureDevice.Stop();
            nowIsTheTime.Visible = true;
            this.Visible = false;

        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.croppablePictureBox1.Image.Dispose();
            this.croppablePictureBox1.Image = this.originalImage;
        }

    }
}