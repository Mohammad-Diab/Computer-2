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
    public sealed partial class Tablet : Computer_2.Common.LayoutAwarePage
    {
        string[] Your_Tablet = new string[10];
        bool Locked = false;
        int Your_Tablet_Index;

        public Tablet()
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
            Your_Tablet[0] = (String)ComboBox1.SelectedItem;
            for (int i = 0; i < ItemsPage.Tablet_PCs.Count; i++)
                if (ItemsPage.Tablet_PCs[i].Company.Equals((String)ComboBox1.SelectedItem))
                {
                    ListViewItem x = new ListViewItem();
                    x.Content = "        " + ItemsPage.Tablet_PCs[i].Name;
                    x.FontSize = 20;
                    ListView1.Items.Add(x);
                }
            Locked = false;
        }

        private void Button1_Click(object sender, RoutedEventArgs e)
        {
            Functions.SaveFile(ItemsPage.Tablet_PCs[Your_Tablet_Index].ToHTML(), "Tablet");
        }

        private void Button2_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(SearchPage), 3);
        }

        private void pageRoot_Loaded(object sender, RoutedEventArgs e)
        {
            List<string> list = new List<string>();
            Window.Current.SizeChanged += this.WindowSizeChanged;
            Initialisation();
            for (int i = 0; i < ItemsPage.Tablet_PCs.Count; i++)
                if (!Functions.item_Existing(list, ItemsPage.Tablet_PCs[i].Company))
                {
                    ComboBox1.Items.Add(ItemsPage.Tablet_PCs[i].Company);
                    list.Add(ItemsPage.Tablet_PCs[i].Company);
                }
        }

        private void ListView1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ApplicationView.TryUnsnap();
            if (!Locked && ListView1.SelectedIndex > -1)
            {
                Button1.IsEnabled = true;
                ListViewItem m = (ListViewItem)ListView1.SelectedItem;
                Your_Tablet[1] = ((String)m.Content).Remove(0, 8);
                Your_Tablet_Index = Functions.Get_Index("Tablet",Your_Tablet[1]);
                int Core;
                int.TryParse(ItemsPage.Tablet_PCs[Your_Tablet_Index].Cores, out Core);
                TextBlock1.Text = "Size: " + ItemsPage.Tablet_PCs[Your_Tablet_Index].Size;
                TextBlock2.Text = "Weight: " + ItemsPage.Tablet_PCs[Your_Tablet_Index].Weight;
                TextBlock3.Text = "Screen: " + ItemsPage.Tablet_PCs[Your_Tablet_Index].Screen_Size + " (" + ItemsPage.Tablet_PCs[Your_Tablet_Index].Screen_Resolution + ")";
                TextBlock4.Text = "OS: " + ItemsPage.Tablet_PCs[Your_Tablet_Index].OS;
                TextBlock5.Text = "Processor: " + ItemsPage.Tablet_PCs[Your_Tablet_Index].Processor_Name + " " + ItemsPage.Tablet_PCs[Your_Tablet_Index].Processor_Speed;
                if (Core > 1)
                    TextBlock5.Text = TextBlock5.Text + " " + Functions.bool2string(Core < 4, "Dual Core", "Quad Core");
                TextBlock6.Text = "Internal Memory: " + ItemsPage.Tablet_PCs[Your_Tablet_Index].Internal_Memory;
                ToggleSwitch1.IsOn = ItemsPage.Tablet_PCs[Your_Tablet_Index].Memory_Card;
                TextBlock7.Text = "Camera: " + ItemsPage.Tablet_PCs[Your_Tablet_Index].Camera;
                ToggleSwitch2.IsOn = ItemsPage.Tablet_PCs[Your_Tablet_Index].Flash;
                TextBlock8.Text = "Communication Network Generation: " + Functions.bool2string(ItemsPage.Tablet_PCs[Your_Tablet_Index].G.Contains("-"), "NO", ItemsPage.Tablet_PCs[Your_Tablet_Index].G);
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
            ScrollViewer1.FontSize =ToggleSwitch1.FontSize = ToggleSwitch2.FontSize = FontSize;
            TextBlock0.FontSize = TextBlock0FontSize;
            ListView1.Width = ListView1Width;
            ListView1.Margin = ListView1Margin;
            ComboBox1.Width = ComboBox1Width;
            ComboBox1.Margin = ComboBox1Margin;
        }
    }
}//150 //135 //140