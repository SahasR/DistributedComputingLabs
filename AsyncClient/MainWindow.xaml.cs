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
using System.Net.Http;

namespace AsyncClient
{
    public delegate ResultStruct Search(string value);
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        RestClient client;
        private string searchText;
        private string baseURL;

        public MainWindow()
        {
            InitializeComponent();
            Go.IsEnabled = false;
            SearchButton.IsEnabled = false;
        }

        private void connect()
        {
            try
            {
                client = new RestClient(baseURL);
                RestRequest request = new RestRequest("api/values");
                RestResponse numEntries = client.Get(request);
                //Also, tell me how many entries are there in the DB
                TotalRecs.Text = numEntries.Content.ToString();
                Go.IsEnabled = true;
                SearchButton.IsEnabled = true;
            } catch (UriFormatException e)
            {
                baseURLBox.Text = "Invalid URL";
            } catch (HttpRequestException e1)
            {
                baseURLBox.Text = "Couldn't connect to Client";
            }
            
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

        private async void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            searchText = Search.Text;
            Task<DataIntermed> task = new Task<DataIntermed>(fetchResults);
            task.Start();
            statusLabel.Content = "Loading Results!";
            DataIntermed dataIntermed = await task;
            statusLabel.Content = "Loaded";
            UpdateGUI(dataIntermed);
        }

        private void UpdateGUI(DataIntermed dataIntermed)
        {
            if (dataIntermed != null)
            {
                FNameBox.Dispatcher.BeginInvoke(new Action(() => FNameBox.Text = dataIntermed.fname));
                LNameBox.Dispatcher.BeginInvoke(new Action(() => LNameBox.Text = dataIntermed.lname));
                Balance.Dispatcher.BeginInvoke(new Action(() => Balance.Text = dataIntermed.bal.ToString("C")));
                AcctNo.Dispatcher.BeginInvoke(new Action(() => AcctNo.Text = dataIntermed.acct.ToString()));
                Pin.Dispatcher.BeginInvoke(new Action(() => Pin.Text = dataIntermed.pin.ToString("D4")));
                byte[] bitmapBytes = dataIntermed.image;
                MemoryStream ms = new MemoryStream(bitmapBytes);
                Bitmap image = (Bitmap)Bitmap.FromStream(ms);

                PictureBox.Dispatcher.BeginInvoke(new Action(() => PictureBox.Source = converter(image)));
            }
        }

        private DataIntermed fetchResults()
        {
            DataIntermed dataIntermed = null;
            SearchData search = new SearchData(searchText);
            RestRequest request = new RestRequest("api/search");
            request.AddJsonBody(JsonConvert.SerializeObject(search));
            RestResponse resp = client.Post(request);
            if (resp.StatusCode == System.Net.HttpStatusCode.NotFound){
                NotFoundException e = JsonConvert.DeserializeObject<NotFoundException>(resp.Content);
                Search.Dispatcher.BeginInvoke(new Action(() => Search.Text = e.Message));
            } else
            {
                dataIntermed = JsonConvert.DeserializeObject<DataIntermed>(resp.Content);
            }   
            return dataIntermed;
        }

        private void setURL_Click(object sender, RoutedEventArgs e)
        {
            baseURL = baseURLBox.Text;
            connect();
        }
    }
}
