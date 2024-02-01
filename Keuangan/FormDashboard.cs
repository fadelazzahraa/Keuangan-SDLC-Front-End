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
using OpenAI_API.Chat;
using OpenAI_API;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;
using System.Diagnostics;

namespace Keuangan
{
    public partial class FormDashboard : Form
    {
        private User user;
        private List<Record> records;
        private List<User> users;
        private int selectedUser;
        static readonly OpenAIAPI api = new("");
        Conversation chat = api.Chat.CreateConversation();

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
                    chart.Series[title].Points.DataBindXY(data.Keys, data.Values);

                    chart.ChartAreas[0].AxisX.Interval = 1; // Set the interval as needed
                    chart.ChartAreas[0].AxisX.LabelStyle.Angle = -45; // Rotate labels if needed
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
            comboBoxStatsUser.Items.Clear();
            comboBoxStatisticUsers.Items.Clear();
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
                labelStatsUser.Text = "Showing stats for your account";

                comboBoxStatsUser.Enabled = false;
                comboBoxStatsUser.Visible = false;
                comboBoxStatisticUsers.Enabled = false;
                comboBoxStatisticUsers.Visible = false;

                radioButtonStatsOfUser.Enabled = false;
                radioButtonStatsOfUser.Visible = false;
                radioButtonStatsAllUsers.Enabled = false;
                radioButtonStatsAllUsers.Visible = false;

                labelStatsUser.TextAlign = ContentAlignment.TopCenter;
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
                comboBoxStatisticUsers.Enabled = true;
                selectedUser = comboBoxStatisticUsers.SelectedIndex;
                LoadRecordDataForStatistics(users[selectedUser].ID);
            }
            else if (radioButtonStatsAllUsers.Checked)
            {
                comboBoxStatisticUsers.Enabled = false;
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

            }
            else if (tabControl1.SelectedIndex == 1)
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
            else if (tabControl1.SelectedIndex == 2)
            {
                if (user.Role == "user")
                {
                    LoadRecordDataForChatBot(user.ID);
                }
                else
                {
                    LoadRecordDataForChatBot(0, true);
                }
            }
        }

        private async void LoadRecordDataForChatBot(int userId, bool allData = false)
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

                listBoxChat.Items.Clear();
                listBoxChat.Enabled = false;
                richTextBoxChatBox.Enabled = false;
                buttonSendChatBox.Enabled = false;

                string userList = "";
                string recordList = "";
                string systemMessage = "";
                if (user.Role == "user")
                {

                    userList = $"{user.Username} sebagai {user.Role} dan pengguna aplikasi aktif yang sekarang menggunakan aplikasi.";

                    foreach (Record record in records)
                    {
                        recordList = $"{recordList} ({user.Username}: {record.Date} => {record.Transaction} Rp{record.ValueRecord} {record.Detail})";
                    }
                    systemMessage = "Anda adalah Aplikasi Pencatatan Keuangan Personal Keluarga. Sapa pengguna sebagai anak dengan nama " + user.Username +
                                    "Berikan saran keuangan berdasarkan data keuangan berikut: " + recordList + ". Data tersebut mencakup nama pengguna, tanggal, jenis transaksi(debit untuk pemasukan, credit untuk pengeluaran), nominal transaksi dalam rupiah, dan detail transaksi." +
                                    "Berikan saran dengan melihat detail transaksinya, frekuensi transaksi berdasarkan harinya, frekuensi transaksi berdasarkan bulan, statistik transaksi dari hari atau bulan sebelumnya, dsb. Saran bisa berupa rencana pengelolaan keuangan masa depan, hal-hal yang perlu diperhatikan berdasarkan pengeluaran yang sudah ada, dsb." +
                                    "Pastikan menggunakan bahasa Indonesia dalam berinteraksi dengan pengguna. Sertakan salam pembuka: 'Selamat datang di Aplikasi Pencatatan Keuangan Personal. Apa yang bisa saya bantu?'";
                }
                else
                {
                    foreach (User i in users)
                    {
                        if (i.ID == user.ID)
                        {
                            userList = $"{userList} ({i.Username} sebagai {i.Role})";
                        }
                        else
                        {
                            userList = $"{userList} ({i.Username} sebagai {i.Role}. Ini adalah pengguna aktif yang sekarang menggunakan aplikasi)";
                        }
                    }
                    foreach (Record record in records)
                    {
                        foreach (User i in users)
                        {
                            if (record.ActorId == i.ID)
                            {
                                recordList = $"{recordList} ({i.Username}: {record.Date} => {record.Transaction} Rp{record.ValueRecord} {record.Detail})";
                            }
                        }
                    }
                    /*systemMessage = "Anda adalah Aplikasi Pencatatan Keuangan Personal Keluarga, layanan untuk pendataan catatan keuangan dan rekomendasi rencana keuangan masa depan" +
                    " Pertama-tama, Anda menyapa pengguna, yang mana pengguna itu adalah Admin, berupa orang tua dengan nama " + user.Username + "." +
                    " Setelah itu, Anda akan diminta untuk memberikan saran keuangan berdasarkan data dari pengguna aplikasi. Pengguna aplikasi ini terdiri dari: " + userList + "." +
                    " Anda akan diminta untuk memberikan saran keuangan berdasarkan data dari pengguna aplikasi. Data dari pengguna aplikasi ini terdiri dari: " + recordList + "." +
                    " Data tersebut terdiri dari nama pengguna, tanggal, jenis transaksi (debit untuk pemasukan, credit untuk pengeluaran), nominal transaksi dalam rupiah, dan detail transaksi." +
                    " Anda dapat memberikan saran dengan melihat detail transaksinya, frekuensi transaksi berdasarkan harinya, frekuensi transaksi berdasarkan bulan, statistik transaksi dari hari atau bulan sebelumnya, dsb." +
                    " Saran yang Anda berikan bisa berupa rencana pengelolaan keuangan masa depan, hal-hal yang perlu diperhatikan berdasarkan pengeluaran yang sudah ada, dsb." +
                    " Saran yang Anda berikan bisa untuk pengguna aplikasi yang mana saja, atau untuk semua pengguna aplikasi." +
                    " Harap perhatikan bahwa akan ada dua tipe akun, yaitu Admin dan User. Admin adalah orang tua yang mengelola keuangan keluarga, sedangkan User adalah anggota keluarga yang mengelola keuangannya sendiri." +
                    " Gunakan bahasa Indonesia dalam berinteraksi dengan pengguna aplikasi. Gunakan salam pembuka: Selamat datang di Aplikasi Pencatatan Keuangan Personal. Apa yang bisa saya bantu?"
                    ;*/
                    systemMessage = "Anda adalah Aplikasi Pencatatan Keuangan Personal Keluarga, sebuah layanan untuk pendataan catatan keuangan dan rekomendasi rencana keuangan masa depan. "
                                    + "Pertama-tama, Anda menyapa pengguna, yang merupakan Admin, berupa orang tua dengan nama " + user.Username + ". "
                                    + "Setelah itu, Anda akan diminta untuk memberikan saran keuangan berdasarkan data dari pengguna aplikasi. "
                                    + "Pengguna aplikasi ini terdiri dari dua tipe akun, yaitu Admin dan User. "
                                    + "Admin adalah orang tua yang mengelola keuangan keluarga, sedangkan User adalah anggota keluarga yang mengelola keuangannya sendiri. "
                                    + "Data dari pengguna aplikasi ini terdiri dari: " + userList + ". "
                                    + "Data tersebut mencakup nama pengguna, tanggal, jenis transaksi (debit untuk pemasukan, credit untuk pengeluaran), nominal transaksi dalam rupiah, dan detail transaksi. "
                                    + "Anda dapat memberikan saran dengan melihat detail transaksinya, frekuensi transaksi berdasarkan harinya, frekuensi transaksi berdasarkan bulan, statistik transaksi dari hari atau bulan sebelumnya, dsb. "
                                    + "Saran yang Anda berikan bisa berupa rencana pengelolaan keuangan masa depan, hal-hal yang perlu diperhatikan berdasarkan pengeluaran yang sudah ada, dsb. "
                                    + "Saran yang Anda berikan bisa untuk pengguna aplikasi yang mana saja, atau untuk semua pengguna aplikasi. "
                                    + "Harap perhatikan bahwa Admin dan User memiliki peran dan tanggung jawab keuangan yang berbeda. "
                                    + "Gunakan bahasa Indonesia dalam berinteraksi dengan pengguna aplikasi. "
                                    + "Gunakan salam pembuka: Selamat datang di Aplikasi Pencatatan Keuangan Personal. "
                                    + "Apa yang bisa saya bantu?";
                }

                chat.AppendSystemMessage(systemMessage);
                chat.AppendUserInput("Hello!");
                string response = await chat.GetResponseFromChatbotAsync();
                AddMessage("Chatbot", response);

                listBoxChat.Enabled = true;
                richTextBoxChatBox.Enabled = true;
                buttonSendChatBox.Enabled = true;

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error occurred while making the request: " + ex.Message);
            }
            ChangeProgressBarState(false);
        }

        private void AddMessage(string sender, string message)
        {
            string formattedMessage = $"{sender}: {message}";
            listBoxChat.Items.Add(formattedMessage);
        }

        private async void buttonSendChatBox_Click(object sender, EventArgs e)
        {
            ChangeProgressBarState(true);
            richTextBoxChatBox.Enabled = false;
            buttonSendChatBox.Enabled = false;

            AddMessage(user.Username, richTextBoxChatBox.Text);
            chat.AppendUserInput(richTextBoxChatBox.Text);
            string response = await chat.GetResponseFromChatbotAsync();
            AddMessage("Chatbot", response);

            richTextBoxChatBox.Text = String.Empty;
            richTextBoxChatBox.Enabled = true;
            buttonSendChatBox.Enabled = true;
            ChangeProgressBarState(false);
        }

        private void richTextBoxChatBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.Handled = true;
                buttonSendChatBox_Click(sender, e);
            }
        }
    }
}
