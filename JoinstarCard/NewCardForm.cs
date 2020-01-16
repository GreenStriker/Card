using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
    }
}
