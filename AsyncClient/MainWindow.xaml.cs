﻿using System;
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

        private async void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            Task<ResultStruct> task = new Task<ResultStruct>(SearchDB);
            task.Start();
            disableUI();
            statusLabel.Content = "Loading result";
            ResultStruct result = await task;
            UpdateGUI(result);
            statusLabel.Content = "Loaded!";
        }

        private void disableUI()
        {
            ProgressBar.Dispatcher.BeginInvoke(new Action(() => ProgressBar.IsIndeterminate = true));
            FNameBox.Dispatcher.BeginInvoke(new Action(() => FNameBox.IsReadOnly = true));
            LNameBox.Dispatcher.BeginInvoke(new Action(() => LNameBox.IsReadOnly = true));
            Balance.Dispatcher.BeginInvoke(new Action(() => Balance.IsReadOnly = true));
            AcctNo.Dispatcher.BeginInvoke(new Action(() => AcctNo.IsReadOnly = true));
            Pin.Dispatcher.BeginInvoke(new Action(() => Pin.IsReadOnly = true));
            Go.Dispatcher.BeginInvoke(new Action(() => Go.IsEnabled = false));
            SearchButton.Dispatcher.BeginInvoke(new Action(() => SearchButton.IsEnabled = false));
        }

        private ResultStruct SearchDB()
        {
            string fName = null, lName = null;
            byte[] bitmapBytes;
            int bal = 0;
            uint acct = 0, pin = 0;

            try
            {
                //On click, Get the search value...
                Search.Dispatcher.Invoke(new Action(() => searchText = Search.Text));

               // bank.GetValuesForSearch(searchText, out acct, out pin, out bal, out fName, out lName, out bitmapBytes);
                if (fName != null)
                {
                    ResultStruct returnValue = new ResultStruct();
                    returnValue.acctNo = acct;
                    returnValue.pin = pin;
                    returnValue.balance = bal;
                    returnValue.firstName = fName;
                    returnValue.lastName = lName;
                  //  MemoryStream ms = new MemoryStream(bitmapBytes);
                  //  Bitmap image = (Bitmap)Bitmap.FromStream(ms);
                   // returnValue.image = image;
                    return returnValue;
                } 
            }
            catch (FaultException<ServerFailureException> ex)
            {
                Request_Counter.Text = ex.Detail.Operation;
            }
            catch (FormatException)
            {
            }
            catch (TimeoutException e)
            {
                Search.Dispatcher.Invoke(new Action(() => Search.Text = "Timeout!"));
            //    bankFactory.Abort();
            }
            return null;
        }

        private void UpdateGUI(ResultStruct resultStruct)
        {
            if (resultStruct != null)
            {
                FNameBox.Dispatcher.BeginInvoke(new Action(() => FNameBox.Text = resultStruct.firstName));
                LNameBox.Dispatcher.BeginInvoke(new Action(() => LNameBox.Text = resultStruct.lastName));
                Balance.Dispatcher.BeginInvoke(new Action(() => Balance.Text = resultStruct.balance.ToString("C")));
                AcctNo.Dispatcher.BeginInvoke(new Action(() => AcctNo.Text = resultStruct.acctNo.ToString()));
                Pin.Dispatcher.BeginInvoke(new Action(() => Pin.Text = resultStruct.pin.ToString("D4")));
                PictureBox.Dispatcher.BeginInvoke(new Action(() => PictureBox.Source = converter(resultStruct.image)));
                Request_Counter.Dispatcher.BeginInvoke(new Action(() => Request_Counter.Text = (int.Parse(Request_Counter.Text) + 1).ToString()));
            }
            else
            {
                Search.Dispatcher.Invoke(new Action(() => Search.Text = "Not Found!"));
            }
            
            //Picture Boxes only use ImageSource format so I have a function that creates a ImageSource from BitMap   
            //Just to see how many requests you have made
            FNameBox.Dispatcher.BeginInvoke(new Action(() => FNameBox.IsReadOnly = false));
            LNameBox.Dispatcher.BeginInvoke(new Action(() => LNameBox.IsReadOnly = false));
            Balance.Dispatcher.BeginInvoke(new Action(() => Balance.IsReadOnly = false));
            AcctNo.Dispatcher.BeginInvoke(new Action(() => AcctNo.IsReadOnly = false));
            Pin.Dispatcher.BeginInvoke(new Action(() => Pin.IsReadOnly = false));
            Go.Dispatcher.BeginInvoke(new Action(() => Go.IsEnabled = true));
            SearchButton.Dispatcher.BeginInvoke(new Action(() => SearchButton.IsEnabled = true));
            ProgressBar.Dispatcher.BeginInvoke(new Action(() => ProgressBar.IsIndeterminate = false));
            ProgressBar.Dispatcher.BeginInvoke(new Action(() => ProgressBar.Value = 100));
        }
    }
}
