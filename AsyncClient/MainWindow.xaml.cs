using System;
using System.ServiceModel;
using System.Windows;
using System.Drawing;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Runtime.Remoting.Messaging;
using BankServer;
using System.Windows.Interop;
using System.IO;
using BankBusinessTier;
using DatabaseLib;
using System.Threading.Tasks;
using Newtonsoft.Json;
using RestSharp;
using APIClasses;

namespace AsyncClient
{
    public delegate ResultStruct Search(string value);
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        RestClient client;
        private Search search;
        private string searchText;

        public MainWindow()
        {
            InitializeComponent();
            string URL = "http://localhost:51641/";
            client = new RestClient(URL);
            RestRequest request = new RestRequest("api/values");
            RestResponse numEntries = client.Get(request);

            //Also, tell me how many entries are there in the DB
            TotalRecs.Text = numEntries.Content.ToString();
        }

        private void Go_Click(object sender, RoutedEventArgs e)
        {
        
            int count = Int32.Parse(Request_Counter.Text);
            count++;

            try
            {
                //On click, Get the index...
                int index = Int32.Parse(Index.Text);
                if (index > 0 && index <= Int32.Parse(TotalRecs.Text))
                {
                    RestRequest request = new RestRequest("api/values/" + index.ToString());
                    RestResponse response = client.Get(request);
                    DataIntermed dataIntermed = JsonConvert.DeserializeObject<DataIntermed>(response.Content);
                    FNameBox.Text = dataIntermed.fname;
                    LNameBox.Text = dataIntermed.lname;
                    Balance.Text = dataIntermed.bal.ToString("C");
                    AcctNo.Text = dataIntermed.acct.ToString();
                    Pin.Text = dataIntermed.pin.ToString("D4");
                    byte[] bitmapBytes = dataIntermed.image;
                    MemoryStream ms = new MemoryStream(bitmapBytes);
                    Bitmap image = (Bitmap)Bitmap.FromStream(ms);
                    PictureBox.Source = converter(image);
                    //Picture Boxes only use ImageSource format so I have a function that creates a ImageSource from BitMap
                    Request_Counter.Text = count.ToString();
                    //Just to see how many requests you have made
                }
            }
            catch (FaultException<ServerFailureException> ex)
            {
                Request_Counter.Text = ex.Detail.Operation;
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

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            SearchData mySearch = new SearchData(Search.Text);
            RestRequest request = new RestRequest("api/search");
            request.AddJsonBody(JsonConvert.SerializeObject(mySearch));
            RestResponse resp = client.Post(request);
            DataIntermed dataIntermed = JsonConvert.DeserializeObject<DataIntermed>(resp.Content);
            statusLabel.Content = "Loaded!";
            if (dataIntermed != null)
            {
                FNameBox.Text = dataIntermed.fname;
                LNameBox.Text = dataIntermed.lname;
                Balance.Text = dataIntermed.bal.ToString("C");
                FNameBox.Text = dataIntermed.fname;
                LNameBox.Text = dataIntermed.lname;
                Balance.Text = dataIntermed.bal.ToString("C");
                AcctNo.Text = dataIntermed.acct.ToString();
                Pin.Text = dataIntermed.pin.ToString("D4");
                byte[] bitmapBytes = dataIntermed.image;
                MemoryStream ms = new MemoryStream(bitmapBytes);
                Bitmap image = (Bitmap)Bitmap.FromStream(ms);
                PictureBox.Source = converter(image);
            } else
            {
                Search.Text = "Not Found";
            }
            
        }

       
    }
}
