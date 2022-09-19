using System;
using System.Windows;
using System.Drawing;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Interop;
using System.IO;
using System.Threading.Tasks;
using Newtonsoft.Json;
using RestSharp;
using APIClasses;
using System.Net.Http;
using WebDatabaseAPI.Models;

namespace AsyncClient
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Go_Click(object sender, RoutedEventArgs e)
        { 
            try
            {
                //On click, Get the index...
                Account account = null;
                int index = Int32.Parse(Index.Text);
                if (index > 0)
                {
                    RestClient restClient = new RestClient("http://localhost:51641/");
                    RestRequest request = new RestRequest("api/values/" + index.ToString());
                    RestResponse response = restClient.Get(request);
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        account = JsonConvert.DeserializeObject<Account>(response.Content);
                    }
                    FNameBox.Text = account.FirstName;
                    LNameBox.Text = account.LastName;
                    Balance.Text = account.Balance.ToString("C");
                    AcctNo.Text = account.AcctNo.ToString();
                    Pin.Text = account.Pin.ToString();
                    byte[] bitmapBytes = account.Image;
                   // MemoryStream ms = new MemoryStream(bitmapBytes);
                   // Bitmap image = (Bitmap)Bitmap.FromStream(ms);
                    // PictureBox.Source = converter(image);
                    //Picture Boxes only use ImageSource format so I have a function that creates a ImageSource from BitMa
                    //Just to see how many requests you have made
                }
            }
            catch (FormatException)
            {
            }
        }

        private ImageSource converter(Bitmap image)
        {
            var handle = image.GetHbitmap();
            return Imaging.CreateBitmapSourceFromHBitmap(handle, IntPtr.Zero, Int32Rect.Empty, BitmapSizeOptions.FromEmptyOptions());
        }

    }
}
