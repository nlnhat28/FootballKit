using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;

namespace FootballKit1
{
    public partial class fThoiTiet : Form
    {
        public string newContent;
        public string oldContent { get; set; }
        PictureBox oldPictureBox;

        public fThoiTiet()
        {
            InitializeComponent();
        }
        private void fNote_Load(object sender, EventArgs e)
        {
            fNote_Load();
        }
        private void fNote_Load()
        {
            fNote_Resize();
            txbThoiTiet.Text = oldContent;
            txbThoiTiet.SelectionStart = txbThoiTiet.Text.Length;

            getWeatherImages();
            loadCbbCity();
        }
        private void loadCbbCity()
        {
            cbbCity.DataSource = WeatherController.listTitles;
            try
            {
                cbbCity.SelectedIndex = WeatherController.cbbCityId;
            }
            catch { }       
            cbbCity.AutoCompleteSource = AutoCompleteSource.CustomSource;
            cbbCity.AutoCompleteCustomSource = new AutoCompleteStringCollection();
            cbbCity.AutoCompleteCustomSource.AddRange(WeatherController.listTitles.ToArray());
            cbbCity.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
        }
        private void getWeatherImages()
        {
            try
            {
                foreach (PictureBox ptb in tlpWeatherImage.Controls)
                {
                    try
                    {
                        if ((string)ptb.Tag == WeatherController.id.ToString())
                        {
                            ptb.BackColor = Color.White;
                            oldPictureBox = ptb;
                            break;
                        }
                    }
                    catch { }
                }
            }
            catch { }
        }
        //
        // Close Form
        //
        private void fNote_FormClosing(object sender, FormClosingEventArgs e)
        {
            newContent = txbThoiTiet.Text;
        }
        private void fNote_Resize(object sender, EventArgs e)
        {
            fNote_Resize();
        }
        private void fNote_Resize()
        {
            float fontSize = Math.Max(Width / 50f, 14f);
            Font newFont = new Font(txbThoiTiet.Font.FontFamily, fontSize, txbThoiTiet.Font.Style);
            txbThoiTiet.Font = newFont;
        }
        //
        // Load file txt
        //
        private void ptbLoadFromTxtNote_Click(object sender, EventArgs e)
        {
            ptbLoadFromTxtNote_Click();
        }
        private void ptbLoadFromTxtNote_Click()
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Title = "Load nội dung từ file txt";
                openFileDialog.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";
                openFileDialog.FilterIndex = 1;
                openFileDialog.RestoreDirectory = true;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        string filePath = openFileDialog.FileName;
                        txbThoiTiet.Text = File.ReadAllText(filePath);
                    }
                    catch
                    {
                        MessageBox.Show("File không tồn tại hoặc không đọc được nội dung file.",
                            "Lỗi: Load file txt", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    };
                }
            }
        }
        //
        // Xem thời tiết
        //
        private async Task getWeatherData()
        {
            double latitude = 0;
            double longitude = 0;
            bool isDefaultLonLat = false;

            if (cbbCity.SelectedIndex >= 0)
            {
                int id = cbbCity.SelectedIndex;
                City city = WeatherController.listCities[id];

                latitude = city.coord.lat;
                longitude = city.coord.lon;

                AppDataController.appData.idCity = city.id;
                WeatherController.cbbCityId = id;

            }
            //string city = null;

            //try
            //{
            //    string urlLocation1 = $"http://ip-api.com/json/?fields=61439";

            //    var clientLocation1 = new HttpClient();

            //    // Set a timeout of ... seconds
            //    clientLocation1.Timeout = TimeSpan.FromSeconds(10);

            //    var response1 = await clientLocation1.GetAsync(urlLocation1);
            //    var responseBody1 = await response1.Content.ReadAsStringAsync();

            //    if (response1.IsSuccessStatusCode)
            //    {
            //        var dataLocation = Newtonsoft.Json.JsonConvert.DeserializeObject<dynamic>(responseBody1);
            //        latitude = dataLocation.lat;
            //        longitude = dataLocation.lon;
            //        city = dataLocation.city;
            //    }
            //    else
            //    {
            //        try
            //        {
            //            string urlIp = "http://ipinfo.io/ip";
            //            string ipAddress;

            //            using (WebClient clientIp = new WebClient())
            //            {
            //                ipAddress = clientIp.DownloadString(urlIp);
            //            }

            //            string urlLocation2 = $"https://api.apilayer.com/ip_to_location/{ipAddress}";
            //            var apiKeyLocation = API.apiKeyLocation;

            //            var clientLocation2 = new HttpClient();
            //            clientLocation2.DefaultRequestHeaders.Add("apikey", apiKeyLocation);

            //            // Set a timeout of ... seconds
            //            clientLocation2.Timeout = TimeSpan.FromSeconds(10);

            //            var response2 = await clientLocation2.GetAsync(urlLocation2);
            //            var responseBody2 = await response2.Content.ReadAsStringAsync();

            //            if (response2.IsSuccessStatusCode)
            //            {
            //                var dataLocation = Newtonsoft.Json.JsonConvert.DeserializeObject<dynamic>(responseBody2);
            //                latitude = dataLocation.latitude;
            //                longitude = dataLocation.longitude;
            //                city = dataLocation.city;
            //            }
            //        }
            //        catch { }
            //    }
            //}
            //catch
            //{
            //    try
            //    {
            //        string urlIp = "http://ipinfo.io/ip";
            //        string ipAddress;

            //        using (WebClient clientIp = new WebClient())
            //        {
            //            ipAddress = clientIp.DownloadString(urlIp);
            //        }

            //        string urlLocation2 = $"https://api.apilayer.com/ip_to_location/{ipAddress}";
            //        var apiKeyLocation = API.apiKeyLocation;

            //        var clientLocation2 = new HttpClient();
            //        clientLocation2.DefaultRequestHeaders.Add("apikey", apiKeyLocation);

            //        // Set a timeout of ... seconds
            //        clientLocation2.Timeout = TimeSpan.FromSeconds(10);

            //        var response2 = await clientLocation2.GetAsync(urlLocation2);
            //        var responseBody2 = await response2.Content.ReadAsStringAsync();

            //        if (response2.IsSuccessStatusCode)
            //        {
            //            var dataLocation = Newtonsoft.Json.JsonConvert.DeserializeObject<dynamic>(responseBody2);
            //            latitude = dataLocation.latitude;
            //            longitude = dataLocation.longitude;
            //        }
            //    }
            //    catch { }
            //}

            if ((int)latitude == 0 || (int)latitude == 0)
            {
                latitude = 20.842851;
                longitude = 106.006638;
                isDefaultLonLat = true;
            }
            try
            {
                //txbThoiTiet.Text = Latitude.ToString() + "\r\n" + Longitude.ToString();
                var clientWeather = new HttpClient();

                var lat = latitude; //kinh độ
                var lon = longitude; //vĩ độ
                var apiKeyWeather = API.apiKeyWeather;

                var urlWeather = $"https://api.openweathermap.org/data/2.5/weather?lat={lat}&lon={lon}&appid={apiKeyWeather}&units=metric&lang=vi";

                clientWeather.Timeout = TimeSpan.FromSeconds(10);

                var responseWeather = await clientWeather.GetAsync(urlWeather);
                var jsonWeather = await responseWeather.Content.ReadAsStringAsync();

                if (responseWeather.IsSuccessStatusCode)
                {
                    //txbThoiTiet.Text += "\r\n";
                    var weatherData = JsonConvert.DeserializeObject<WeatherData>(jsonWeather);
                    //txbThoiTiet.Text += $"Địa điểm: {weatherData.Name}" + "\r\n";
                    if (txbThoiTiet.Text == string.Empty
                        || txbThoiTiet.Text.EndsWith("\r\n")
                        || txbThoiTiet.Text.EndsWith(". "))
                    { }
                    else
                    {
                        txbThoiTiet.Text = txbThoiTiet.Text.TrimEnd();
                        if (txbThoiTiet.Text.EndsWith(".")
                            || txbThoiTiet.Text.EndsWith(",")
                            || txbThoiTiet.Text.EndsWith(";")
                            || txbThoiTiet.Text.EndsWith(":"))
                        {
                            txbThoiTiet.Text += " ";
                        }
                        else
                        {
                            txbThoiTiet.Text += ". ";
                        }
                    }

                    //var des = weatherData.Weather[0].Description;
                    //if (des != string.Empty) des = char.ToUpper(des[0]) + des.Substring(1);
                    //txbThoiTiet.Text += $"{des}. ";

                    txbThoiTiet.Text += $"Nhiệt độ: {Math.Round(weatherData.main.temp, 1)}°C. ";
                    txbThoiTiet.Text += $"Sức gió: {Math.Round(weatherData.wind.speed * 3.6)}km/h. ";
                    txbThoiTiet.Text += $"Mây: {weatherData.clouds.all}%. ";
                    txbThoiTiet.Text += $"Độ ẩm: {weatherData.main.humidity}%. ";
                    //city = city ?? "(Hưng Yên)";
                    //txbThoiTiet.Text += $"({weatherData.name}). ";

                    if (isDefaultLonLat)
                        MessageBox.Show("Chỉ xem được thành phố trong danh sách. Vị trí được thiết lập mặc định là TP.Hưng Yên",
                        "Lỗi dữ liệu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    MessageBox.Show("Vui lòng thử lại sau.",
                        "Lỗi kết nối", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch
            {
                MessageBox.Show("Vui lòng thử lại sau.",
                     "Lỗi kết nối", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private async void btnXemThoiTiet_Click(object sender, EventArgs e)
        {
            await getWeatherData();
        }
        private async void cbbCity_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.Handled = true;
                e.SuppressKeyPress = true;

                await getWeatherData();
            }
        }
        //
        // Xoá
        //
        private void btnXoa_Click(object sender, EventArgs e)
        {
            btnXoa_Click();
        }
        private void btnXoa_Click()
        {
            txbThoiTiet.Text = string.Empty;
        }
        //
        // Click image weather
        //
        private void ptbWeather_Click(object sender, EventArgs e)
        {
            ptbWeatherClick(sender);
        }
        private void ptbWeatherClick(object sender)
        {
            try
            {
                try
                {
                    oldPictureBox.BackColor = Color.Transparent;
                }
                catch { }
                PictureBox ptb = (PictureBox)sender;
                ptb.BackColor = Color.White;
                int id = Convert.ToInt32(ptb.Tag);
                txbThoiTiet.Text = WeatherController.listWeatherImages[id].title + ". " + txbThoiTiet.Text;
                WeatherController.id = id;
                oldPictureBox = ptb;

                if (Directory.Exists(SaveController.folderPath))
                {
                    try
                    {
                        File.Copy(WeatherController.listWeatherImages[id].path, SaveController.folderPath + Default.thoiTiet, true);
                    }
                    catch { }
                }
            }
            catch { }
        }      
        //
        // Mouse Hover image weather
        //
        private void ptbWeather_MouseHover(object sender, EventArgs e)
        {
            ptbWeatherMouseHover(sender);
        }
        private void ptbWeatherMouseHover(object sender)
        {
            try
            {
                PictureBox ptb = (PictureBox)sender;
                string content = WeatherController.listWeatherImages[Convert.ToInt32(ptb.Tag.ToString())].title;
                ttThoiTiet.SetToolTip(ptb, content);
            }
            catch { }
        }
    }
}
