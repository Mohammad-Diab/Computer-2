using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Basic Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234237
namespace Computer_2
{

struct Laptop_Name
{
    public string Company;
    public string Name;
}


    /// <summary>
    /// A basic page that provides characteristics common to most applications.
    /// </summary>
    public sealed partial class laptop : Computer_2.Common.LayoutAwarePage
    {
        List<Laptop_Name> Laptop_Search = new List<Laptop_Name>();
        string[] Your_Laptop = new string[10];
        bool Locked = false;
        public laptop()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// Populates the page with content passed during navigation.  Any saved state is also
        /// provided when recreating a page from a prior session.
        /// </summary>
        /// <param name="navigationParameter">The parameter value passed to
        /// <see cref="Frame.Navigate(Type, Object)"/> when this page was initially requested.
        /// </param>
        /// <param name="pageState">A dictionary of state preserved by this page during an earlier
        /// session.  This will be null the first time a page is visited.</param>
        protected override void LoadState(Object navigationParameter, Dictionary<String, Object> pageState)
        {
            // TODO: Create an appropriate data model for your problem domain to replace the sample data
        }

        /// <summary>
        /// Preserves state associated with this page in case the application is suspended or the
        /// page is discarded from the navigation cache.  Values must conform to the serialization
        /// requirements of <see cref="SuspensionManager.SessionState"/>.
        /// </summary>
        /// <param name="pageState">An empty dictionary to be populated with serializable state.</param>
        protected override void SaveState(Dictionary<String, Object> pageState)
        {
        }

        private void ComboBox1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Locked = true;
            ListView1.Items.Clear();
            Your_Laptop[0] = (String)ComboBox1.SelectedItem;
            for (int i = 0; i < ItemsPage.Laptops.Count; i++)
                if (ItemsPage.Laptops[i].Company.Equals((String)ComboBox1.SelectedItem))
                {
                    ListViewItem x = new ListViewItem();
                    x.Content = "        " + ItemsPage.Laptops[i].Name;
                    x.FontSize = 20;
                    ListView1.Items.Add(x);
                }
            Locked = false;
        }

        private void pageRoot_Loaded(object sender, RoutedEventArgs e)
        {
            List<string> list = new List<string>();
            ComboBox1.Items.Clear();
            foreach (var i in ItemsPage.Laptops)
                if (!Functions.item_Existing(list, i.Company))
                {
                    list.Add(i.Company);
                    ComboBox1.Items.Add(i.Company);
                }
        }

        private void ListView1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (!Locked && ListView1.SelectedIndex > -1)
            {
                ListViewItem m = (ListViewItem)ListView1.SelectedItem;
                Your_Laptop[1] = ((String)m.Content).Remove(0, 8);
                int x = Functions.Get_Index("Laptop", Your_Laptop[1]);
                TextBlock1.Text = "Processor: " + ItemsPage.Laptops[x].Processor + " " + ItemsPage.Laptops[x].Processor_Name + " " + ItemsPage.Laptops[x].Processor_Speed;
                if (!ItemsPage.Laptops[x].Processor_Max_Speed.Equals("-"))
                    TextBlock1.Text = TextBlock1.Text + " Up to " + ItemsPage.Laptops[x].Processor_Max_Speed;
                TextBlock1.Text = TextBlock1.Text + " " + ItemsPage.Laptops[x].Cashe_Memory + " Cashe";
                TextBlock2.Text = "Memory: " + ItemsPage.Laptops[x].RAM;
                TextBlock3.Text = "Hard Disk: " + ItemsPage.Laptops[x].Hard_Disk + " (" + ItemsPage.Laptops[x].Hard_Disk_Rotation + ")";
                TextBlock4.Text = "Graphic Card: " + ItemsPage.Laptops[x].Graphic_Card + " ";
                if (!ItemsPage.Laptops[x].Graphic_Card_Size.Equals("-"))
                    TextBlock4.Text = TextBlock4.Text + ItemsPage.Laptops[x].Graphic_Card_Size + " ";
                TextBlock4.Text = TextBlock4.Text + "(Up to " + ItemsPage.Laptops[x].Graphic_Card_Max_Size + ")";
                TextBlock5.Text = "Screen: " + ItemsPage.Laptops[x].Screen_Size + " (" + ItemsPage.Laptops[x].Screen_Resolution + ")";
                TextBlock6.Text = "USB Port: " + ItemsPage.Laptops[x].USB2.ToString() + " x USB 2";
                if (!(ItemsPage.Laptops[x].USB3 == 0))
                    TextBlock6.Text = TextBlock6.Text + " ," + ItemsPage.Laptops[x].USB3.ToString() + " x USB 3";
                if (!(ItemsPage.Laptops[x].eSata == "-"))
                    TextBlock6.Text = TextBlock6.Text + " ," + ItemsPage.Laptops[x].eSata;
                if (ItemsPage.Laptops[x].Bluetooth == "-")
                    TextBlock7.Text = "Bluetooth: NO";
                else
                    TextBlock7.Text = "Bluetooth: " + ItemsPage.Laptops[x].Bluetooth;
                if (ItemsPage.Laptops[x].OS == "-")
                    TextBlock8.Text = "OS: NO";
                else
                    TextBlock8.Text = "OS: " + ItemsPage.Laptops[x].OS;
                TextBlock9.Text = "WebCam: " + ItemsPage.Laptops[x].Camera;
                if (ItemsPage.Laptops[x].Optical_Driver.Equals("-"))
                    ToggleSwitch1.IsOn = false;
                else
                    ToggleSwitch1.IsOn = true;
                ToggleSwitch2.IsOn = ItemsPage.Laptops[x].HDMI;
                ToggleSwitch3.IsOn = ItemsPage.Laptops[x].VGA;
                ToggleSwitch4.IsOn = ItemsPage.Laptops[x].Card_Reader;
                ToggleSwitch5.IsOn = ItemsPage.Laptops[x].Touch_Screen;
            }
        }

        private void Button2_Click(object sender, RoutedEventArgs e)
        {
            if (ListView2.SelectedIndex > -1)
            {
                int index = Functions.Get_Index("Laptop", Laptop_Search[ListView2.SelectedIndex].Name);
                ListView2.Items.Clear();
                Laptop_Search.Clear();
                Grid1.Visibility = Visibility.Collapsed;
                View_Laptop(index);
            }
        }



        private void Button3_Click(object sender, RoutedEventArgs e)
        {
            ListView2.Items.Clear();
            Grid1.Visibility = Visibility.Collapsed;
        }
    }
}