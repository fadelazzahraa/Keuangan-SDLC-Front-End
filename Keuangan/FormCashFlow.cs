﻿using Newtonsoft.Json;
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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Net.Http;
using Newtonsoft.Json.Linq;
using System.Security.Cryptography;
using System.Reflection.Emit;
using System.Globalization;

namespace Keuangan
{
    public partial class FormCashFlow : Form
    {

        private User user;
        private BindingList<Record> records;
        private List<List<string>> photos;
        private Record selectedRecord = null;
        private List<User> users;
        private int defaultUser;
        private int selectedUser;

        public FormCashFlow(User loggedinuser)
        {
            user = loggedinuser;
            InitializeComponent();
        }

        private async void LoadRecordData(int userId)
        {
            records = new BindingList<Record>();
            ChangeProgressBarState(true);
            try
            {
                string responseData = "";
                if (userId.Equals(-1))
                {
                    responseData = await Connection.GetAuthorizedDataAsync(Connection.getRecordByUserURL(user.Username), user.Token);
                }
                else
                {
                    responseData = await Connection.GetAuthorizedDataAsync(Connection.getRecordByUserURL(users[userId].Username), user.Token);
                }

                Dictionary<string, object> responseDataDictionary = JsonConvert.DeserializeObject<Dictionary<string, object>>(responseData);

                JArray datas = (JArray)responseDataDictionary["data"];

                foreach (var selectedData in datas)
                {
                    int id = (int)selectedData["id"];
                    string transaction = (string)selectedData["transaction"];
                    float value = (float)selectedData["value"];
                    string detail = (string)selectedData["detail"];
                    DateTime date = (DateTime)selectedData["date"];
                    int? photoRecordId = (int?)selectedData["photoRecordId"];

                    records.Add(new Record(id, transaction, value, detail, date, user.Username, photoRecordId));
                };
                dataGridView1.DataSource = records;
                AdjustDataGridView();

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error occurred while making the request: " + ex.Message);
            }
            ChangeProgressBarState(false);
        }

        private async void LoadPhotoData()
        {
            photos = new List<List<string>>();
            ChangeProgressBarState(true);
            try
            {
                string responseData = await Connection.GetAuthorizedDataAsync(Connection.getPhotoURL, user.Token);

                Dictionary<string, object> responseDataDictionary = JsonConvert.DeserializeObject<Dictionary<string, object>>(responseData);

                JArray datas = (JArray)responseDataDictionary["data"];

                comboBox2.Items.Add("null");
                
                photos.Add(new List<string>
                {
                    "0",
                    "null"
                });

                foreach (var selectedData in datas)
                {
                    string id = (string)selectedData["id"];
                    string detail = (string)selectedData["detail"];
                    photos.Add(new List<string>
                    {
                        id,
                        detail,
                    });

                    comboBox2.Items.Add($"{id} - {detail}");

                };
                comboBox2.SelectedIndex = 0;

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error occurred while making the request: " + ex.Message);
            }
            ChangeProgressBarState(false);
        }

        private async void LoadUsersData()
        {
            users = new List<User>();
            ChangeProgressBarState(true);
            try
            {
                string responseData = await Connection.GetAuthorizedDataAsync(Connection.getUsersURL, user.Token);

                Dictionary<string, object> responseDataDictionary = JsonConvert.DeserializeObject<Dictionary<string, object>>(responseData);

                JArray datas = (JArray)responseDataDictionary["data"];

                int idxx = 0;
                foreach (var selectedData in datas)
                {
                    int id = (int)selectedData["id"];
                    string username = (string)selectedData["username"];
                    string role = (string)selectedData["role"];

                    users.Add(new User(id, username, role));

                    comboBox3.Items.Add($"{id} - {username}");

                    if (id == user.ID)
                    {
                        comboBox3.SelectedIndex = idxx;
                        defaultUser = idxx;
                        selectedUser = idxx;
                    }
                    idxx += 1;
                };


            }
            catch (Exception ex)
            {
                MessageBox.Show("Error occurred while making the request: " + ex.Message);
            }
            ChangeProgressBarState(false);
        }

        private void FormCashFlow_Load(object sender, EventArgs e)
        {
            if (user.Role == "user")
            {
                label2.Enabled = false;
                label2.Visible = false;
                comboBox3.Enabled = false;
                comboBox3.Visible = false;
                users = null;
                defaultUser = -1;
                selectedUser = -1;
                LoadRecordData(-1);
            }
            else
            {
                LoadUsersData();
            }
            LoadPhotoData();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.RowIndex < dataGridView1.Rows.Count)
            {
                ChangeProgressBarState(true);
                selectedRecord = records[e.RowIndex];
                comboBox1.SelectedIndex = selectedRecord.Transaction == "debit" ? 0 : 1;
                textBox3.Text = selectedRecord.ValueRecord.ToString();
                textBox4.Text = selectedRecord.Detail;
                dateTimePicker1.Value = selectedRecord.Date;

                if (selectedRecord.PhotoRecordId != null)
                {
                    for (int i = 0; i < photos.Count; i++)
                    {
                        if (photos[i].Contains(selectedRecord.PhotoRecordId.ToString()))
                        {
                            comboBox2.SelectedIndex = i;
                            break;
                        }
                    }

                } else
                {
                    comboBox2.SelectedIndex = 0;
                }
                ChangeProgressBarState(false);

                SetPictureBoxImage(pictureBox4);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            AddRecord();
        }

        private async void AddRecord()
        {
            DialogResult result = MessageBox.Show("Do you want to proceed to add record?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                ChangeProgressBarState(true);
                try
                {
                    string transaction = comboBox1.SelectedIndex == 0 ? "debit" : "credit";
                    float value = float.Parse(textBox3.Text);
                    string detail = textBox4.Text;
                    DateTime date = dateTimePicker1.Value;
                    string dateformatted = date.ToString("yyyy-MM-dd");



                    Dictionary<string, string> data = new Dictionary<string, string>
                    {
                        { "actor", "me" },
                        { "transaction", transaction },
                        { "value", value.ToString() },
                        { "detail", detail },
                        { "date", $"{dateformatted}" },
                        { "tag", defaultUser == -1 ? user.Username : users[selectedUser].Username },
                        { "sourceRecordId", "2" },
                        { "photoRecordId", comboBox2.SelectedIndex != 0 ? photos[comboBox2.SelectedIndex][0] : null},
                    };

                    string requestBody = System.Text.Json.JsonSerializer.Serialize(data);

                    string responseData = await Connection.PostAuthorizedDataAsync(Connection.addRecordURL, requestBody, user.Token);

                    Dictionary<string, object> responseDataDictionary = JsonConvert.DeserializeObject<Dictionary<string, object>>(responseData);

                    MessageBox.Show(responseDataDictionary["message"].ToString());
                    LoadRecordData(selectedUser);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error occurred while making the request: " + ex.Message);
                }
                ChangeProgressBarState(false);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            EditRecord();
        }

        private async void EditRecord()
        {
            DialogResult result = MessageBox.Show("Do you want to proceed to edit record?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                ChangeProgressBarState(true);
                try
                {
                    string transaction = comboBox1.SelectedIndex == 0 ? "debit" : "credit";
                    float value = float.Parse(textBox3.Text);
                    string detail = textBox4.Text;
                    DateTime date = dateTimePicker1.Value;
                    string dateformatted = date.ToString("yyyy-MM-dd");

                    Dictionary<string, string> data = new Dictionary<string, string>
                    {
                        { "actor", "me" },
                        { "transaction", transaction },
                        { "value", value.ToString() },
                        { "detail", detail },
                        { "date", $"{dateformatted}" },
                        { "tag", defaultUser == -1 ? user.Username : users[selectedUser].Username},
                        { "sourceRecordId", "2" },
                        { "photoRecordId", comboBox2.SelectedIndex != 0 ? photos[comboBox2.SelectedIndex][0] : null},
                    };

                    string requestBody = System.Text.Json.JsonSerializer.Serialize(data);

                    string responseData = await Connection.PostAuthorizedDataAsync(Connection.editRecordURL(selectedRecord.ID), requestBody, user.Token);

                    Dictionary<string, object> responseDataDictionary = JsonConvert.DeserializeObject<Dictionary<string, object>>(responseData);

                    MessageBox.Show(responseDataDictionary["message"].ToString());
                    LoadRecordData(selectedUser);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error occurred while making the request: " + ex.Message);
                }
                ChangeProgressBarState(false);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            DeleteRecord();
        }

        private async void DeleteRecord()
        {
            DialogResult result = MessageBox.Show("Do you want to proceed to delete record?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                ChangeProgressBarState(true);
                try
                {
                    Dictionary<string, string> data = new Dictionary<string, string>
                    {
                    };

                    string responseData = await Connection.DeleteAuthorizedDataAsync(Connection.deleteRecordURL(selectedRecord.ID), user.Token);

                    Dictionary<string, object> responseDataDictionary = JsonConvert.DeserializeObject<Dictionary<string, object>>(responseData);

                    MessageBox.Show(responseDataDictionary["message"].ToString());
                    LoadRecordData(selectedUser);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error occurred while making the request: " + ex.Message);
                }
                ChangeProgressBarState(false);
            }
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            SetPictureBoxImage(pictureBox4);

        }

        private async void SetPictureBoxImage(PictureBox pictureBox4)
        {
            pictureBox4.Image = null;
            if (comboBox2.SelectedIndex != 0)
            {
                ChangeProgressBarState(true);
                try
                {
                    int indexImage = Int32.Parse(photos[comboBox2.SelectedIndex][0]);
                    using (HttpClient httpClient = new HttpClient())
                    {
                        httpClient.DefaultRequestHeaders.Add("Authorization", user.Token);
                        byte[] imageData = await httpClient.GetByteArrayAsync(Connection.getPhotoImageWithIndexURL(indexImage));
                        using (var stream = new System.IO.MemoryStream(imageData))
                        {
                            Image fetchedImage = Image.FromStream(stream);
                            pictureBox4.Image = fetchedImage;
                        }
                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Failed to load the image. Error: {ex.Message}");
                    pictureBox4.Image.Dispose();
                }
                ChangeProgressBarState(false);
            }
        }

        private void ChangeProgressBarState(bool isActivated = true)
        {
            if (isActivated)
            {
                toolStripStatusLabel1.Text = "Loading...";
                toolStripProgressBar1.Style = ProgressBarStyle.Marquee;
                toolStripProgressBar1.MarqueeAnimationSpeed = 100;
            }
            else
            {
                toolStripStatusLabel1.Text = "Ready";
                toolStripProgressBar1.Style = ProgressBarStyle.Continuous;
                toolStripProgressBar1.MarqueeAnimationSpeed = 0;
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            DoSearch();
        }

        private void DoSearch()
        {
            if (textBox1.Text == "" || textBox1.Text == null)
            {
                dataGridView1.DataSource = records;
            }
            else
            {
                string searchTerm = textBox1.Text.ToLower();

                var filteredData = records.Where(item =>
                    item.Transaction.ToLower().Contains(searchTerm) ||
                    item.ValueRecord.ToString().Contains(searchTerm) ||
                    item.Detail.ToLower().Contains(searchTerm) ||
                    item.Date.ToString("dd/MM/yyyy").Contains(searchTerm) ||
                    item.Tag.ToLower().Contains(searchTerm)
                    ).ToList();

                dataGridView1.DataSource = filteredData;
            }
            AdjustDataGridView();
        }

        private void AdjustDataGridView()
        {
            dataGridView1.Columns["id"].Visible = false;
            dataGridView1.Columns["photoRecordId"].Visible = false;
            dataGridView1.Columns["transaction"].HeaderText = "Transaction";
            dataGridView1.Columns["valueRecord"].HeaderText = "Value";
            dataGridView1.Columns["date"].HeaderText = "Date";
            dataGridView1.Columns["detail"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridView1.Columns["tag"].Visible = false;
        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            selectedUser = comboBox3.SelectedIndex;
            LoadRecordData(selectedUser);
        }
    }
}

