using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Core;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

namespace Computer_2
{
    public sealed partial class laptop : Computer_2.Common.LayoutAwarePage
    {
        string[] Your_Laptop = new string[10];
        bool Locked = false;
        int Your_Laptop_Index;

        public laptop()
        {
            this.InitializeComponent();
        }

        protected override void LoadState(Object navigationParameter, Dictionary<String, Object> pageState)
        {
        }

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

        private void Button1_Click(object sender, RoutedEventArgs e)
        {
            Functions.SaveFile(ItemsPage.Laptops[Your_Laptop_Index].ToHTML(), "Laptop");
        }

        private void Button2_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(SearchPage), 2);
        }

        private void pageRoot_Loaded(object sender, RoutedEventArgs e)
        {
            List<string> list = new List<string>();
            Window.Current.SizeChanged += this.WindowSizeChanged;
            Initialisation();
            foreach (var i in ItemsPage.Laptops)
                if (!Functions.item_Existing(list, i.Company))
                {
                    list.Add(i.Company);
                    ComboBox1.Items.Add(i.Company);
                }
        }

        private void ListView1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ApplicationView.TryUnsnap();
            if (!Locked && ListView1.SelectedIndex > -1)
            {
                Button1.IsEnabled = true;
                ListViewItem m = (ListViewItem)ListView1.SelectedItem;
                Your_Laptop[1] = ((String)m.Content).Remove(0, 8);
                Your_Laptop_Index = Functions.Get_Index("Laptop", Your_Laptop[1]);
                TextBlock1.Text = "Processor: " + ItemsPage.Laptops[Your_Laptop_Index].Processor + " " + ItemsPage.Laptops[Your_Laptop_Index].Processor_Name + " " + ItemsPage.Laptops[Your_Laptop_Index].Processor_Speed;
                TextBlock1.Text = TextBlock1.Text + Functions.bool2string(!ItemsPage.Laptops[Your_Laptop_Index].Processor_Max_Speed.Equals("-"), " Up to " + ItemsPage.Laptops[Your_Laptop_Index].Processor_Max_Speed, "");
                TextBlock1.Text = TextBlock1.Text + " " + ItemsPage.Laptops[Your_Laptop_Index].Cashe_Memory + " Cashe";
                TextBlock2.Text = "Memory: " + ItemsPage.Laptops[Your_Laptop_Index].RAM;
                TextBlock3.Text = "Hard Disk: " + ItemsPage.Laptops[Your_Laptop_Index].Hard_Disk + " (" + ItemsPage.Laptops[Your_Laptop_Index].Hard_Disk_Rotation + ")";
                TextBlock4.Text = "Graphic Card: " + ItemsPage.Laptops[Your_Laptop_Index].Graphic_Card + " ";
                TextBlock4.Text = TextBlock4.Text + Functions.bool2string(!ItemsPage.Laptops[Your_Laptop_Index].Graphic_Card_Size.Equals("-"), ItemsPage.Laptops[Your_Laptop_Index].Graphic_Card_Size + " ", "");
                TextBlock4.Text = TextBlock4.Text + "(Up to " + ItemsPage.Laptops[Your_Laptop_Index].Graphic_Card_Max_Size + ")";
                TextBlock5.Text = "Screen: " + ItemsPage.Laptops[Your_Laptop_Index].Screen_Size + " (" + ItemsPage.Laptops[Your_Laptop_Index].Screen_Resolution + ")";
                TextBlock6.Text = "OS: " + Functions.bool2string(ItemsPage.Laptops[Your_Laptop_Index].OS == "-", "NO", ItemsPage.Laptops[Your_Laptop_Index].OS);
                ToggleSwitch1.IsOn = !ItemsPage.Laptops[Your_Laptop_Index].Optical_Driver.Equals("-");
               /* ToggleSwitch2.IsOn = ItemsPage.Laptops[Your_Laptop_Index].Ports.Contains("HDMI");*/
                ToggleSwitch3.IsOn = ItemsPage.Laptops[Your_Laptop_Index].Touch_Screen;
            }
            else 
                Button1.IsEnabled = false;
        }

        private void WindowSizeChanged(object sender, WindowSizeChangedEventArgs e)
        {
            Initialisation();
        }

        private void Initialisation()
        {
            bool LandScape = false;
            ApplicationViewState viewState = ApplicationView.Value;
            LandScape = (viewState == ApplicationViewState.FullScreenLandscape);
            if (!LandScape)
                Optimizing(18, 40, new Thickness(320, 0, 0, 0), 705, 18, 20, 320, new Thickness(0, 85, 0, 0), 200, new Thickness(95, 35, 0, 0));
            else
                Optimizing(20, 48, new Thickness(475, 0, 0, 0), 890, 24, 28, 455, new Thickness(0, 65, 0, 0), 263, new Thickness(192, 10, 0, 0));
        }

        private void Optimizing(double ListViewItemFontSize, double ListViewItemHeight, Thickness ScrollViewer1Margin, double ScrollViewer1Width, double FontSize, double TextBlock0FontSize, double ListView1Width, Thickness ListView1Margin, double ComboBox1Width, Thickness ComboBox1Margin)
        {
            for (int i = 0; i < ListView1.Items.Count; i++)
            {
                ((ListViewItem)(ListView1.Items[i])).FontSize = ListViewItemFontSize;
                ((ListViewItem)(ListView1.Items[i])).Height = ListViewItemHeight;
            }
            ScrollViewer1.Margin = ScrollViewer1Margin;
            ScrollViewer1.Width = ScrollViewer1Width;
            ScrollViewer1.FontSize = ToggleSwitch1.FontSize = ToggleSwitch2.FontSize = ToggleSwitch3.FontSize = FontSize;
            TextBlock0.FontSize = TextBlock0FontSize;
            ListView1.Width = ListView1Width;
            ListView1.Margin = ListView1Margin;
            ComboBox1.Width = ComboBox1Width;
            ComboBox1.Margin = ComboBox1Margin;
        }
    }
}//151 //135 //140