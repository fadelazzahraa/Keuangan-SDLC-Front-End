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
        private Dictionary<string, List<List<string>>> categories;
        private List<List<string>> categoriesComboBoxList;
        private Record selectedRecord = null;
        private List<User> users;
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
                string responseData = await Connection.GetAuthorizedDataAsync(Connection.getRecordByUserURL(userId), user.Token);

                Dictionary<string, object> responseDataDictionary = JsonConvert.DeserializeObject<Dictionary<string, object>>(responseData);

                JArray datas = (JArray)responseDataDictionary["data"];

                foreach (var selectedData in datas)
                {
                    int id = (int)selectedData["id"];
                    int actorId = (int)selectedData["actorId"];
                    string transaction = (string)selectedData["transaction"];
                    float value = (float)selectedData["value"];
                    string detail = (string)selectedData["detail"];
                    DateTime date = (DateTime)selectedData["date"];
                    string tag = (string)selectedData["tag"];
                    int? categoryRecordId = (int?)selectedData["categoryRecordId"];
                    int? photoRecordId = (int?)selectedData["photoRecordId"];

                    records.Add(new Record(id, actorId, transaction, value, detail, date, tag, categoryRecordId, photoRecordId));
                };
                dataGridViewRecord.DataSource = records;
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

                comboBoxPhoto.Items.Add("null");

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

                    comboBoxPhoto.Items.Add($"{id} - {detail}");

                };
                comboBoxPhoto.SelectedIndex = 0;

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error occurred while making the request: " + ex.Message);
            }
            ChangeProgressBarState(false);
        }

        private async void LoadCategoryData()
        {
            categories = new Dictionary<string, List<List<string>>>
            {
                { "debit", new List<List<string>>() },
                { "credit", new List<List<string>>() }
            };
            categoriesComboBoxList = new List<List<string>>
            {
                new List<string>
                {
                    "0 - null"
                },
                new List<string>
                {
                    "0 - null"
                },
            };

            ChangeProgressBarState(true);
            try
            {
                string responseData = await Connection.GetAuthorizedDataAsync(Connection.getCategoriesURL, user.Token);

                Dictionary<string, object> responseDataDictionary = JsonConvert.DeserializeObject<Dictionary<string, object>>(responseData);

                JArray datas = (JArray)responseDataDictionary["data"];

                categories["debit"].Add(new List<string>
                {
                    "0",
                    "null"
                });

                categories["credit"].Add(new List<string>
                {
                    "0",
                    "null"
                });

                foreach (var selectedData in datas)
                {
                    string id = (string)selectedData["id"];
                    string categoryType = (string)selectedData["categoryType"];
                    string category = (string)selectedData["category"];
                    if (categoryType == "debit")
                    {
                        categories["debit"].Add(new List<string>
                        {
                            id,
                            category,
                        });
                        categoriesComboBoxList[0].Add($"{categoriesComboBoxList[0].Count} - {category}");

                    }
                    else if (categoryType == "credit")
                    {
                        categories["credit"].Add(new List<string>
                        {
                            id,
                            category,
                        });
                        categoriesComboBoxList[1].Add($"{categoriesComboBoxList[1].Count} - {category}");

                    }
                    else
                    {
                        throw new Exception("Error! Category type is not valid");
                    }

                };
                comboBoxCategory.DataSource = categoriesComboBoxList[0];
                comboBoxTransaction.SelectedIndex = 0;
                comboBoxCategory.SelectedIndex = 0;

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

                    comboBoxUser.Items.Add($"{id} - {username}");

                    if (id == user.ID)
                    {
                        comboBoxUser.SelectedIndex = idxx;
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
            LoadCategoryData();
            if (user.Role == "user")
            {
                label2.Enabled = false;
                label2.Visible = false;
                comboBoxUser.Enabled = false;
                comboBoxUser.Visible = false;
                users = null;
                LoadRecordData(user.ID);
            }
            else
            {
                LoadUsersData();
            }
            LoadPhotoData();
        }

        private void dataGridViewRecord_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.RowIndex < dataGridViewRecord.Rows.Count)
            {
                ChangeProgressBarState(true);
                selectedRecord = records[e.RowIndex];
                comboBoxTransaction.SelectedIndex = selectedRecord.Transaction == "debit" ? 0 : 1;
                textBoxValue.Text = selectedRecord.ValueRecord.ToString();
                textBoxDetail.Text = selectedRecord.Detail;
                dateTimePicker1.Value = selectedRecord.Date;
                textBoxTag.Text = selectedRecord.Tag;

                if (selectedRecord.CategoryRecordId != null)
                {
                    for (int i = 0; i < categories[selectedRecord.Transaction].Count; i++)
                    {
                        if (categories[selectedRecord.Transaction][i][0].Contains(selectedRecord.CategoryRecordId.ToString()))
                        {
                            comboBoxCategory.DataSource = categoriesComboBoxList[selectedRecord.Transaction == "debit" ? 0 : 1];
                            comboBoxCategory.SelectedIndex = i;
                            break;
                        }
                    }

                }
                else
                {
                    comboBoxCategory.DataSource = categoriesComboBoxList[selectedRecord.Transaction == "debit" ? 0 : 1];
                    comboBoxCategory.SelectedIndex = 0;

                }

                if (selectedRecord.PhotoRecordId != null)
                {
                    for (int i = 0; i < photos.Count; i++)
                    {
                        if (photos[i].Contains(selectedRecord.PhotoRecordId.ToString()))
                        {
                            comboBoxPhoto.SelectedIndex = i;
                            break;
                        }
                    }

                }
                else
                {
                    comboBoxPhoto.SelectedIndex = 0;
                }

                ChangeProgressBarState(false);

                SetPictureBoxImage(pictureBoxPhoto);
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
                    string transaction = comboBoxTransaction.SelectedIndex == 0 ? "debit" : "credit";
                    float value = float.Parse(textBoxValue.Text);
                    string detail = textBoxDetail.Text;
                    DateTime date = dateTimePicker1.Value;
                    string dateformatted = date.ToString("yyyy-MM-dd");
                    string tag = textBoxTag.Text;
                    int categoryRecordId = comboBoxCategory.SelectedIndex;

                    Dictionary<string, string> data = new Dictionary<string, string>
                    {
                        { "actorId", user.Role == "user" ? user.ID.ToString() : users[selectedUser].ID.ToString() },
                        { "transaction", transaction },
                        { "value", value.ToString() },
                        { "detail", detail },
                        { "date", $"{dateformatted}" },
                        { "tag", tag },
                        { "categoryRecordId", comboBoxCategory.SelectedIndex != 0 ? categories[transaction][comboBoxCategory.SelectedIndex][0] : null },
                        { "photoRecordId", comboBoxPhoto.SelectedIndex != 0 ? photos[comboBoxPhoto.SelectedIndex][0] : null},
                    };

                    string requestBody = System.Text.Json.JsonSerializer.Serialize(data);

                    string responseData = await Connection.PostAuthorizedDataAsync(Connection.addRecordURL, requestBody, user.Token);

                    Dictionary<string, object> responseDataDictionary = JsonConvert.DeserializeObject<Dictionary<string, object>>(responseData);

                    MessageBox.Show(responseDataDictionary["message"].ToString());
                    LoadRecordData(user.Role == "user" ? user.ID : users[selectedUser].ID);
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
                    string transaction = comboBoxTransaction.SelectedIndex == 0 ? "debit" : "credit";
                    float value = float.Parse(textBoxValue.Text);
                    string detail = textBoxDetail.Text;
                    DateTime date = dateTimePicker1.Value;
                    string dateformatted = date.ToString("yyyy-MM-dd");
                    string tag = textBoxTag.Text;
                    int categoryRecordId = comboBoxCategory.SelectedIndex;

                    Dictionary<string, string> data = new Dictionary<string, string>
                    {
                        { "actorId", user.Role == "user" ? user.ID.ToString() : users[selectedUser].ID.ToString()  },
                        { "transaction", transaction },
                        { "value", value.ToString() },
                        { "detail", detail },
                        { "date", $"{dateformatted}" },
                        { "tag", tag },
                        { "categoryRecordId", comboBoxCategory.SelectedIndex != 0 ? categories[transaction][comboBoxCategory.SelectedIndex][0] : null },
                        { "photoRecordId", comboBoxPhoto.SelectedIndex != 0 ? photos[comboBoxPhoto.SelectedIndex][0] : null},
                    };

                    string requestBody = System.Text.Json.JsonSerializer.Serialize(data);

                    string responseData = await Connection.PostAuthorizedDataAsync(Connection.editRecordURL(selectedRecord.ID), requestBody, user.Token);

                    Dictionary<string, object> responseDataDictionary = JsonConvert.DeserializeObject<Dictionary<string, object>>(responseData);

                    MessageBox.Show(responseDataDictionary["message"].ToString());
                    LoadRecordData(user.Role == "user" ? user.ID : users[selectedUser].ID);
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
                    LoadRecordData(user.Role == "user" ? user.ID : users[selectedUser].ID);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error occurred while making the request: " + ex.Message);
                }
                ChangeProgressBarState(false);
            }
        }

        private void comboBoxPhoto_SelectedIndexChanged(object sender, EventArgs e)
        {
            SetPictureBoxImage(pictureBoxPhoto);

        }

        private async void SetPictureBoxImage(PictureBox pictureBox4)
        {
            pictureBox4.Image = null;
            if (comboBoxPhoto.SelectedIndex != 0)
            {
                ChangeProgressBarState(true);
                try
                {
                    int indexImage = Int32.Parse(photos[comboBoxPhoto.SelectedIndex][0]);
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
            if (textBoxSearch.Text == "" || textBoxSearch.Text == null)
            {
                dataGridViewRecord.DataSource = records;
            }
            else
            {
                string searchTerm = textBoxSearch.Text.ToLower();

                var filteredData = records.Where(item =>
                    item.Transaction.ToLower().Contains(searchTerm) ||
                    item.ValueRecord.ToString().Contains(searchTerm) ||
                    item.Detail.ToLower().Contains(searchTerm) ||
                    item.Date.ToString("dd/MM/yyyy").Contains(searchTerm) ||
                    item.Tag.ToLower().Contains(searchTerm)
                    ).ToList();

                dataGridViewRecord.DataSource = filteredData;
            }
            AdjustDataGridView();
        }

        private void AdjustDataGridView()
        {
            if (!dataGridViewRecord.Columns.Contains("Category"))
            {
                DataGridViewTextBoxColumn categoryColumn = new DataGridViewTextBoxColumn();
                categoryColumn.Name = "category";
                categoryColumn.HeaderText = "Category";
                dataGridViewRecord.Columns.Add(categoryColumn);
                dataGridViewRecord.Columns["category"].DisplayIndex = 6;
            }

            dataGridViewRecord.Columns["id"].Visible = false;
            dataGridViewRecord.Columns["actorId"].Visible = false;
            dataGridViewRecord.Columns["photoRecordId"].Visible = false;
            dataGridViewRecord.Columns["categoryRecordId"].Visible = false;
            dataGridViewRecord.Columns["transaction"].HeaderText = "Transaction";
            dataGridViewRecord.Columns["valueRecord"].HeaderText = "Value";
            dataGridViewRecord.Columns["date"].HeaderText = "Date";
            dataGridViewRecord.Columns["detail"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            foreach (DataGridViewRow row in dataGridViewRecord.Rows)
            {
                if (row.Cells["categoryRecordId"].Value != null)
                {
                    for (int i = 0; i < categories[row.Cells["transaction"].Value.ToString()].Count; i++)
                    {
                        if (categories[row.Cells["transaction"].Value.ToString()][i][0].Contains(row.Cells["categoryRecordId"].Value.ToString()))
                        {
                            row.Cells["category"].Value = categories[row.Cells["transaction"].Value.ToString()][i][1];
                            break;
                        }
                    }
                }
            }
        }

        private void comboBoxUser_SelectedIndexChanged(object sender, EventArgs e)
        {
            selectedUser = comboBoxUser.SelectedIndex;
            LoadRecordData(users[selectedUser].ID);
        }

        private void comboBoxTransaction_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBoxCategory.DataSource = categoriesComboBoxList[comboBoxTransaction.SelectedIndex];
            comboBoxCategory.SelectedIndex = 0;
        }

        private void comboBoxCategory_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}

