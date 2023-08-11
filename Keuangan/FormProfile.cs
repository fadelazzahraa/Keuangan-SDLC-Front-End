using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.AxHost;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Keuangan
{
    public partial class FormProfile : Form
    {
        private User user;
        public FormProfile(User loggedinUser)
        {
            user = loggedinUser;
            InitializeComponent();
        }

        private void ChangeLoadingState(bool stats)
        {
            if (stats)
            {
                groupBox2.Visible = false;
                groupBox1.Visible = false;
                label1.Visible = true;
                label1.Enabled = true;
                
            } else
            {
                groupBox2.Visible = true;
                groupBox1.Visible = true;
                label1.Visible = false;
                label1.Enabled = false;
            }
        }      

        private void button2_Click(object sender, EventArgs e)
        {
            ChangeUsername();
        }

        private async void ChangeUsername()
        {
            ChangeLoadingState(true);
            DialogResult result = MessageBox.Show("Do you want to change your username?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                ChangeLoadingState(true);
                try
                {
                    string username = textBox2.Text;

                    Dictionary<string, string> data = new Dictionary<string, string>
                    {
                        { "username", username },
                    };

                    string requestBody = System.Text.Json.JsonSerializer.Serialize(data);

                    string responseData = await Connection.PostAuthorizedDataAsync(Connection.postUserProfileURL, requestBody, user.Token);

                    Dictionary<string, object> responseDataDictionary = JsonConvert.DeserializeObject<Dictionary<string, object>>(responseData);

                    MessageBox.Show(responseDataDictionary["message"].ToString());
                    if (responseDataDictionary["status"].ToString() == "True")
                    {
                        try
                        {
                            string responseData2 = await Connection.GetAuthorizedDataAsync(Connection.getRecordByUserURL(user.Username), user.Token);

                            Dictionary<string, object> responseDataDictionary2 = JsonConvert.DeserializeObject<Dictionary<string, object>>(responseData2);

                            JArray datas = (JArray)responseDataDictionary2["data"];

                            foreach (var selectedData in datas)
                            {
                                int id = (int)selectedData["id"];

                                Dictionary<string, string> data2 = new Dictionary<string, string>
                                {
                                    { "tag", username},
                                };

                                string requestBody3 = System.Text.Json.JsonSerializer.Serialize(data2);

                                string responseData3 = await Connection.PostAuthorizedDataAsync(Connection.editRecordURL(id), requestBody3, user.Token);

                                Dictionary<string, object> responseDataDictionary3 = JsonConvert.DeserializeObject<Dictionary<string, object>>(responseData3);

                                user.Username = textBox2.Text;
                            };

                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Error occurred while making the request: " + ex.Message);
                            textBox2.Text = user.Username;
                        }
                    }
                    else
                    {
                        textBox2.Text = user.Username;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error occurred while making the request: " + ex.Message);
                }
                ChangeLoadingState(false);

            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ChangePassword();   
        }

        private async void ChangePassword()
        {
            ChangeLoadingState(true);
            DialogResult result = MessageBox.Show("Do you want to change your password?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                ChangeLoadingState(true);
                try
                {
                    string oldpassword = textBox4.Text;
                    string newpassword = textBox1.Text;

                    Dictionary<string, string> data = new Dictionary<string, string>
                    {
                        { "oldpassword", oldpassword },
                        { "newpassword", newpassword },
                    };

                    string requestBody = System.Text.Json.JsonSerializer.Serialize(data);

                    string responseData = await Connection.PostAuthorizedDataAsync(Connection.changePasswordURL, requestBody, user.Token);

                    Dictionary<string, object> responseDataDictionary = JsonConvert.DeserializeObject<Dictionary<string, object>>(responseData);

                    MessageBox.Show(responseDataDictionary["message"].ToString());
                    textBox4.Text = "";
                    textBox1.Text = "";
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error occurred while making the request: " + ex.Message);
                }
                ChangeLoadingState(false);
            }
        }

        private void FormProfile_Load(object sender, EventArgs e)
        {
            textBox2.Text = user.Username;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            textBox4.UseSystemPasswordChar = textBox4.UseSystemPasswordChar != true;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            textBox1.UseSystemPasswordChar = textBox1.UseSystemPasswordChar != true;
        }
    }
}
