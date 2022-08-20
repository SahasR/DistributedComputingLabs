using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Drawing;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using BankServer;
using System.Windows.Interop;
using System.IO;
using BankBusinessTier;

namespace BankClient
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private BusinessServerInterface bank;
        public MainWindow()
        {
            InitializeComponent();
            //This is a factory that generates remote connections to our remote class. This is what hides the RPC stuff!
            ChannelFactory<BusinessServerInterface> bankFactory;
            NetTcpBinding tcp = new NetTcpBinding();

            //Set the URL and create the connection!
            string URL = "net.tcp://localhost:8200/BankBusinessService";
            EndpointAddress address = new EndpointAddress(URL);
            bankFactory = new ChannelFactory<BusinessServerInterface>(tcp, address);
            bank = bankFactory.CreateChannel();
            //Also, tell me how many entries are there in the DB
            TotalRecs.Text = bank.GetNumEntries().ToString();

        }

        private void Go_Click(object sender, RoutedEventArgs e)
        {
            int index = 0;
            string fName = null, lName = null;
            byte[] bitmapBytes;
            int bal = 0;
            uint acct = 0, pin = 0;
            int count = Int32.Parse(Request_Counter.Text);
            count++;

            try
            {
                //On click, Get the index...
                index = Int32.Parse(Index.Text);
                if (index > 0 && index <= Int32.Parse(TotalRecs.Text))
                {
                    bank.GetValuesForEntry(index - 1, out acct, out pin, out bal, out fName, out lName, out bitmapBytes);
                    FNameBox.Text = fName;
                    LNameBox.Text = lName;
                    Balance.Text = bal.ToString("C");
                    AcctNo.Text = acct.ToString();
                    Pin.Text = pin.ToString("D4");
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
            string searchText;
            string fName = null, lName = null;
            byte[] bitmapBytes;
            int bal = 0;
            uint acct = 0, pin = 0;
            int count = Int32.Parse(Request_Counter.Text);
            count++;


            try
            {
                //On click, Get the index...
                searchText = Search.Text;
                
                    bank.GetValuesForSearch(searchText, out acct, out pin, out bal, out fName, out lName, out bitmapBytes);
                    FNameBox.Text = fName;
                    LNameBox.Text = lName;
                    Balance.Text = bal.ToString("C");
                    AcctNo.Text = acct.ToString();
                    Pin.Text = pin.ToString("D4");
                    MemoryStream ms = new MemoryStream(bitmapBytes);
                    Bitmap image = (Bitmap)Bitmap.FromStream(ms);
                    PictureBox.Source = converter(image);
                    //Picture Boxes only use ImageSource format so I have a function that creates a ImageSource from BitMap
                    Request_Counter.Text = count.ToString();
                    //Just to see how many requests you have made
            }
            catch (FaultException<ServerFailureException> ex)
            {
                Request_Counter.Text = ex.Detail.Operation;
            }
            catch (FormatException)
            {
            }
        }
    }
}
