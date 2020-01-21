using JoinstarCard.Model;
using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
namespace JoinstarCard
{
    public partial class NewCardForm : Form
    {

     private   string cardNo = "";
        public NewCardForm(string cad)
        {
            InitializeComponent();

            this.cardNo = cad;

            this.label6.Text = this.cardNo;
        }

        public NewCardForm(string cardNo, string email, string phone, string name,Image im)
        {
            InitializeComponent();

            textBox1.Text = name;
            textBox2.Text = phone;
            textBox3.Text = email;
            label6.Text = cardNo;
            
            pictureBox1.Image = im;
        }


        public NewCardForm()
        {
            InitializeComponent();

        }



        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void NewCardForm_Load(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
        
        }

        private void label5_Click(object sender, EventArgs e)
        {
            CaptureImageForm cap = new CaptureImageForm(label6.Text,textBox3.Text,textBox2.Text,textBox1.Text);

            cap.Visible = true;

            this.Visible = false;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            NewCard newCard = new NewCard();
            newCard.CardNo = label6.Text;
            newCard.Name = textBox1.Text;
            newCard.Phone=textBox2.Text ;
            newCard.Email= textBox3.Text;
            newCard.IsAgree = checkBox1.Checked;
            newCard.MemberImage=ImageToByteArray(pictureBox1.Image);
            var json = Newtonsoft.Json.JsonConvert.SerializeObject(newCard);
            Console.WriteLine(json);
            Console.ReadLine();
            MessageBox.Show("Json Convert Done! See into Console");
        }
        public static byte[] ImageToByteArray(Image x)
        {
            ImageConverter _imageConverter = new ImageConverter();
            byte[] xByte = (byte[])_imageConverter.ConvertTo(x, typeof(byte[]));
            return xByte;
        }
    }
}
