using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace Keuangan
{
    public partial class FormDashboard : Form
    {
        private User user;
        private List<Record> records;
        private List<User> users;
        private int selectedUser;

        public FormDashboard(User loggedinuser)
        {
            user = loggedinuser;
            InitializeComponent();
        }

        private async void LoadRecordDataForDashboard(int userId)
        {
            records = new List<Record>();
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
                }

                float balance = 0;
                float debitThisMonth = 0;
                float creditThisMonth = 0;

                if (records.Count != 0)
                {
                    foreach (Record record in records)
                    {
                        if (record.Transaction == "debit")
                        {
                            balance += record.ValueRecord;
                        }
                        else
                        {
                            balance -= record.ValueRecord;
                        }

                        if (record.Transaction == "debit" && record.Date.Month == DateTime.Now.Month)
                        {
                            debitThisMonth += record.ValueRecord;
                        }
                        else if (record.Transaction == "credit" && record.Date.Month == DateTime.Now.Month)
                        {
                            creditThisMonth += record.ValueRecord;
                        }
                    }
                }

                CultureInfo info = new CultureInfo("id-ID");
                CultureInfo infoEn = new CultureInfo("en-US");

                label11.Text = $"Debit {DateTime.Now.ToString("MMMM", infoEn)}";
                label3.Text = $"Credit {DateTime.Now.ToString("MMMM", infoEn)}";

                label7.Text = balance.ToString("C", info);
                label7.ForeColor = balance > 0 ? Color.Green : Color.Red;
                label8.Text = debitThisMonth.ToString("C", info);
                label8.ForeColor = Color.Green;
                label9.Text = creditThisMonth.ToString("C", info);
                label9.ForeColor = Color.Red;

                if (records.Count != 0)
                {
                    label2.Text = records[records.Count - 1].ValueRecord.ToString("C", info);
                    label2.ForeColor = records[records.Count - 1].Transaction == "debit" ? Color.Green : Color.Red;
                    label12.Text = records[records.Count - 1].Detail;
                    label13.Text = records[records.Count - 1].Date.ToString("dd/MM/yyyy");
                }
                else
                {
                    label2.Text = "";
                    label2.ForeColor = Color.Black;
                    label12.Text = "No transaction yet";
                    label13.Text = "";
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error occurred while making the request: " + ex.Message);
            }
            ChangeProgressBarState(false);
        }

        private async void LoadRecordDataForStatistics(int userId, bool allData = false)
        {
            records = new List<Record>();
            ChangeProgressBarState(true);
            try
            {
                string responseData = await Connection.GetAuthorizedDataAsync(allData ? Connection.getRecordsURL : Connection.getRecordByUserURL(userId), user.Token);

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
                }

                string[] daysOfWeek = { "Sunday", "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday" };

                string[] monthsOfYear = { "January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December" };

                Dictionary<string, float> debitByDay = new Dictionary<string, float>();
                Dictionary<string, float> debitByMonth = new Dictionary<string, float>();
                Dictionary<string, float> creditByDay = new Dictionary<string, float>();
                Dictionary<string, float> creditByMonth = new Dictionary<string, float>();

                debitByDay = daysOfWeek.ToDictionary(day => day, _ => 0f);
                debitByMonth = monthsOfYear.ToDictionary(month => month, _ => 0f);
                creditByDay = daysOfWeek.ToDictionary(day => day, _ => 0f);
                creditByMonth = monthsOfYear.ToDictionary(month => month, _ => 0f);

                foreach (var record in records)
                {

                    string dayOfWeek = record.Date.DayOfWeek.ToString();
                    string month = record.Date.ToString("MMMM", CultureInfo.InvariantCulture);

                    if (record.Transaction == "debit")
                    {
                        debitByDay[dayOfWeek] += record.ValueRecord;

                        debitByMonth[month] += record.ValueRecord;
                    }
                    else if (record.Transaction == "credit")
                    {
                        creditByDay[dayOfWeek] += record.ValueRecord;

                        creditByMonth[month] += record.ValueRecord;
                    }

                }

                void SetChart(Chart chart, Dictionary<string, float> data, string title)
                {
                    chart.Series[title].Points.Clear();
                    foreach (var entry in data)
                    {
                        chart.Series[title].Points.AddXY(entry.Key, entry.Value);
                    }
                }


                SetChart(chartDebitDays, debitByDay, "Debit by Day");
                SetChart(chartDebitMonths, debitByMonth, "Debit by Month");
                SetChart(chartCreditDays, creditByDay, "Credit by Day");
                SetChart(chartCreditMonths, creditByMonth, "Credit by Month");

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

                    comboBoxStatsUser.Items.Add($"{id} - {username}");
                    comboBoxStatisticUsers.Items.Add($"{id} - {username}");

                    if (id == user.ID)
                    {
                        selectedUser = idxx;
                        comboBoxStatsUser.SelectedIndex = idxx;
                        comboBoxStatisticUsers.SelectedIndex = idxx;
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

        private void FormDashboard_Load(object sender, EventArgs e)
        {
            if (user.Role == "user")
            {
                labelStatsUser.Text = "                            Showing stats for your account";

                comboBoxStatsUser.Enabled = false;
                comboBoxStatsUser.Visible = false;
                comboBoxStatisticUsers.Enabled = false;
                comboBoxStatisticUsers.Visible = false;

                radioButtonStatsOfUser.Enabled = false;
                radioButtonStatsOfUser.Visible = false;
                radioButtonStatsAllUsers.Enabled = false;
                radioButtonStatsAllUsers.Visible = false;

                labelStatisticYourAccount.Visible = true;

                users = null;
                LoadRecordDataForDashboard(user.ID);
                LoadRecordDataForStatistics(user.ID);
            }
            else
            {
                LoadUsersData();
            }
        }

        private void pictureBoxLogout_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Logout success!");
            this.Hide();
            FormLogin formLogin = new FormLogin();
            formLogin.Closed += (s, args) => this.Close();
            formLogin.Show();
        }

        private void pictureBoxCashFlow_Click(object sender, EventArgs e)
        {
            this.Hide();
            FormCashFlow formCashFlow = new FormCashFlow(user);
            formCashFlow.Closed += (s, args) =>
            {
                this.Show();
                if (user.Role == "user")
                {
                    tabControl1.SelectedIndex = 0;
                    radioButtonStatsOfUser.Checked = true;
                    LoadRecordDataForDashboard(user.ID);
                    LoadRecordDataForStatistics(user.ID);
                }
                else
                {
                    LoadUsersData();
                }
            };
            formCashFlow.Show();
        }

        private void pictureBoxUploadBill_Click(object sender, EventArgs e)
        {
            this.Hide();
            FormUploadBill formUploadBill = new FormUploadBill(user);
            formUploadBill.Closed += (s, args) =>
            {
                this.Show();
                if (user.Role == "user")
                {
                    tabControl1.SelectedIndex = 0;
                    radioButtonStatsOfUser.Checked = true;
                    LoadRecordDataForDashboard(user.ID);
                    LoadRecordDataForStatistics(user.ID);
                }
                else
                {
                    LoadUsersData();
                }
            };
            formUploadBill.Show();
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

        private void comboBoxStatsUser_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControl1.SelectedIndex == 0)
            {
                selectedUser = comboBoxStatsUser.SelectedIndex;
                LoadRecordDataForDashboard(users[selectedUser].ID);
            }
        }

        private void pictureBoxProfile_Click(object sender, EventArgs e)
        {
            this.Hide();
            FormProfile formProfile = new FormProfile(user);
            formProfile.Closed += (s, args) =>
            {
                this.Show();
                if (user.Role == "user")
                {
                    tabControl1.SelectedIndex = 0;
                    radioButtonStatsOfUser.Checked = true;
                    LoadRecordDataForDashboard(user.ID);
                    LoadRecordDataForStatistics(user.ID);

                }
                else
                {
                    LoadUsersData();
                }
            };
            formProfile.Show();
        }

        private void radioButtonStatistic_CheckedChanged(object sender, EventArgs e)
        {

            if (radioButtonStatsOfUser.Checked)
            {
                selectedUser = comboBoxStatisticUsers.SelectedIndex;
                LoadRecordDataForStatistics(users[selectedUser].ID);
            }
            else if (radioButtonStatsAllUsers.Checked)
            {
                LoadRecordDataForStatistics(0, true);
            }
        }

        private void comboBoxStatisticUsers_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControl1.SelectedIndex == 1)
            {
                selectedUser = comboBoxStatisticUsers.SelectedIndex;
                LoadRecordDataForStatistics(users[selectedUser].ID);
            }
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControl1.SelectedIndex == 0)
            {
                if (user.Role == "user")
                {
                    LoadRecordDataForDashboard(user.ID);
                }
                else
                {
                    selectedUser = comboBoxStatsUser.SelectedIndex;
                    LoadRecordDataForDashboard(users[selectedUser].ID);
                }

            } else if (tabControl1.SelectedIndex == 1)
            {
                if (user.Role == "user")
                {
                    LoadRecordDataForStatistics(user.ID);
                }
                else
                {
                    if (radioButtonStatsOfUser.Checked)
                    {
                        selectedUser = comboBoxStatisticUsers.SelectedIndex;
                        LoadRecordDataForStatistics(users[selectedUser].ID);
                    }
                    else if (radioButtonStatsAllUsers.Checked)
                    {
                        LoadRecordDataForStatistics(0, true);
                    }
                }
                
            }
        }
    }
}
