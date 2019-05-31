using CefSharp;
using CefSharp.Wpf;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace YTTest
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {

        [DllImport("user32.dll")]
        public static extern void keybd_event(byte bVk, byte bScan, uint dwFlags, uint dwExtraInfo);

        private Dictionary<string, List<string>> ScratchList = new Dictionary<string, List<string>>()
        {
            {"", null },
            {"Shark Attack", new List<string>() {"Jdzu4VCT4ew", "QcuCPHKQ0LA" } },
            {"Draw The Line", new List<string>() { "OaPOm93LAbc", "llFES43SWMk" } },
            {"Highway Traffic", new List<string>() {"P_0v6nBq5Vw", "oBAgGhERebA", "xwowGRwX834"} },
        };

        private int id = 0;

        const uint KEYEVENTF_KEYUP = 0x0002;
        const uint KEYEVENTF_EXTENDEDKEY = 0x0001;

        int Press(uint key)
        {
            //Press the key
            keybd_event((byte)key, 0, KEYEVENTF_EXTENDEDKEY | 0, 0);
            return 0;
        }

        public bool LeftEnabled
        {
            get => id > 0;
        }

        public bool RightEnabled
        {
            get => id < Current?.Count - 1;
        }

        private string _videoId;

        public string VideoId
        {
            get
            {
                return _videoId;
            }

            set
            {
                _videoId = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("VideoId"));
            }

        }

        public List<string> Current
        {
            get => ScratchList[((ComboBoxItem)idtextBox.SelectedItem)?.Content.ToString() ?? ""];
        }

        public MainWindow()
        {
            InitializeComponent();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            id = 0;
            StartVideo();
        }

        public void StartVideo()
        {
            VideoId = string.Format("https://www.youtube.com/embed/{0}?version=3&autoplay=1&fs=0&autohide=1&loop=1&modestbranding=1&showinfo=0", idtextBox.SelectedItem == null ? idtextBox.Text : Current[id]);
            Console.WriteLine(VideoId);

            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("LeftEnabled"));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("RightEnabled"));
        }

        private void Button_Close(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Button_Left(object sender, RoutedEventArgs e)
        {
            id = id > 0 ? id - 1 : id;
            StartVideo();

        }


        private void Button_Right(object sender, RoutedEventArgs e)
        {
            id = id < Current?.Count - 1 ? id + 1 : id;
            StartVideo();

        }


        private void MainWindowElement_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }

    }
}

