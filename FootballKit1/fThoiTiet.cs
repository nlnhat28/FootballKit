using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net;
using Newtonsoft.Json;
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
            catch
            { }
        }
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
        private async void btnXemThoiTiet_Click(object sender, EventArgs e)
        {
            double latitude = 0;
            double longitude = 0;
            bool isDefaultLonLat = false;
            string city = null;

            try
            {
                string urlLocation1 = $"http://ip-api.com/json/?fields=61439";

                var clientLocation1 = new HttpClient();

                // Set a timeout of ... seconds
                clientLocation1.Timeout = TimeSpan.FromSeconds(10);

                var response1 = await clientLocation1.GetAsync(urlLocation1);
                var responseBody1 = await response1.Content.ReadAsStringAsync();

                if (response1.IsSuccessStatusCode)
                {
                    var dataLocation = Newtonsoft.Json.JsonConvert.DeserializeObject<dynamic>(responseBody1);
                    latitude = dataLocation.lat;
                    longitude = dataLocation.lon;
                    city = dataLocation.city;
                }
                else
                {
                    try
                    {
                        string urlIp = "http://ipinfo.io/ip";
                        string ipAddress;

                        using (WebClient clientIp = new WebClient())
                        {
                            ipAddress = clientIp.DownloadString(urlIp);
                        }

                        string urlLocation2 = $"https://api.apilayer.com/ip_to_location/{ipAddress}";
                        var apiKeyLocation = API.apiKeyLocation;

                        var clientLocation2 = new HttpClient();
                        clientLocation2.DefaultRequestHeaders.Add("apikey", apiKeyLocation);

                        // Set a timeout of ... seconds
                        clientLocation2.Timeout = TimeSpan.FromSeconds(10);

                        var response2 = await clientLocation2.GetAsync(urlLocation2);
                        var responseBody2 = await response2.Content.ReadAsStringAsync();

                        if (response2.IsSuccessStatusCode)
                        {
                            var dataLocation = Newtonsoft.Json.JsonConvert.DeserializeObject<dynamic>(responseBody2);
                            latitude = dataLocation.latitude;
                            longitude = dataLocation.longitude;
                            city = dataLocation.city;
                        }
                    }
                    catch { }
                }
            }
            catch
            {
                try
                {
                    string urlIp = "http://ipinfo.io/ip";
                    string ipAddress;

                    using (WebClient clientIp = new WebClient())
                    {
                        ipAddress = clientIp.DownloadString(urlIp);
                    }

                    string urlLocation2 = $"https://api.apilayer.com/ip_to_location/{ipAddress}";
                    var apiKeyLocation = API.apiKeyLocation;

                    var clientLocation2 = new HttpClient();
                    clientLocation2.DefaultRequestHeaders.Add("apikey", apiKeyLocation);

                    // Set a timeout of ... seconds
                    clientLocation2.Timeout = TimeSpan.FromSeconds(10);

                    var response2 = await clientLocation2.GetAsync(urlLocation2);
                    var responseBody2 = await response2.Content.ReadAsStringAsync();

                    if (response2.IsSuccessStatusCode)
                    {
                        var dataLocation = Newtonsoft.Json.JsonConvert.DeserializeObject<dynamic>(responseBody2);
                        latitude = dataLocation.latitude;
                        longitude = dataLocation.longitude;
                    }
                }
                catch { }
            }

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
                        || txbThoiTiet.Text.EndsWith(". ") )
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

                    txbThoiTiet.Text += $"Nhiệt độ: {Math.Round(weatherData.Main.Temp, 1)}°C. ";
                    txbThoiTiet.Text += $"Sức gió: {Math.Round(weatherData.Wind.Speed * 3.6)}km/h. ";
                    txbThoiTiet.Text += $"Mây: {weatherData.Clouds.All}%. ";
                    txbThoiTiet.Text += $"Độ ẩm: {weatherData.Main.Humidity}% ";
                    city = city ?? "(Hưng Yên)";
                    txbThoiTiet.Text += $"({city}). ";

                    if (isDefaultLonLat)
                        MessageBox.Show("Không xác định được vị trí chính xác của thiết bị.\nVị trí được thiết lập mặc định là TP.Hưng Yên",
                        "Vị trí có thể sai lệch", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
