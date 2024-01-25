using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace Keuangan
{
    public partial class FormSignup : Form
    {
        public FormSignup()
        {
            InitializeComponent();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                textBox3.Enabled = true;
                button1.Enabled = true;
            }
            else
            {
                textBox3.Enabled = false;
                textBox3.Clear();
                button1.Enabled = false;
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Signin();
        }

        private void label4_Click(object sender, EventArgs e)
        {
            Signin();
        }

        private async void Signin()
        {
            string username = textBox1.Text;
            string password = textBox2.Text;
            string adminpassword = textBox3.Text;

            pictureBox1.Visible = false;
            label4.Visible = false;
            label5.Visible = true;
            textBox1.Enabled = false;
            textBox2.Enabled = false;
            textBox3.Enabled = false;
            checkBox1.Enabled = false;

            if (username == "")
            {
                MessageBox.Show("Username cannot be empty!");
            } else
            {
                try
                {
                    // Create a dictionary to represent the data you want to send in the request body
                    Dictionary<string, string> data = new Dictionary<string, string>
                {
                    { "username", username },
                    { "password", password }
                };

                    if (checkBox1.Checked)
                    {
                        data.Add("adminPass", adminpassword);
                    }

                    // Serialize the data dictionary to JSON string
                    string requestBody = System.Text.Json.JsonSerializer.Serialize(data);

                    string responseData = await Connection.PostDataAsync(Connection.signupURL, requestBody);

                    Dictionary<string, string> responseDataDictionary = JsonConvert.DeserializeObject<Dictionary<string, string>>(responseData);
                    if (responseDataDictionary["status"] == "true")
                    {
                        MessageBox.Show("Signup success!");

                    }
                    else
                    {
                        MessageBox.Show("Signup failed!");
                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error occurred while making the request: " + ex.Message);
                }
            }

            pictureBox1.Visible = true;
            label4.Visible = true;
            label5.Visible = false;
            textBox1.Enabled = true;
            textBox2.Enabled = true;
            textBox3.Enabled = false;
            checkBox1.Enabled = true;
            checkBox1.Checked = false;
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            textBox1.Focus();  
        }

        private void button3_Click(object sender, EventArgs e)
        {
            textBox2.UseSystemPasswordChar = textBox2.UseSystemPasswordChar != true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            textBox3.UseSystemPasswordChar = textBox3.UseSystemPasswordChar != true;
        }
    }
}
