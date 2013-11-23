using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.Storage.Pickers;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Core;
using Windows.UI;

namespace Computer_2
{
    public sealed partial class SearchPage : Computer_2.Common.LayoutAwarePage
    {
        int counter = 1;
        List<Laptop> laptopList = new List<Laptop>();
        List<TabletPC> tabletList = new List<TabletPC>();
        List<int> Search_Result = new List<int>();
        bool locked = true, expand = true, firstUse = true;
        Byte searchIn = 0;
        public SearchPage()
        {
            this.InitializeComponent();
        }

        protected override void LoadState(Object navigationParameter, Dictionary<String, Object> pageState)
        {
            byte.TryParse(navigationParameter.ToString(), out searchIn);
        }

        protected override void SaveState(Dictionary<String, Object> pageState)
        {
        }

        private void pageRoot_Loaded(object sender, RoutedEventArgs e)
        {
            switch (searchIn)
            {
                //DesktopPC = 1; //LaptopPC = 2; //Tablet = 3; //MacComputer = 4; //Mobile = 5; //All = 6;
                case 1:
                    break;
                case 2:
                    laptopList.Clear();
                    foreach (var i in ItemsPage.Laptops)
                        laptopList.Add(i);
                    FillGridViewWithLaptop();
                    FillListViewWithLaptop();
                    FillFlipViewWithLaptop();
                    AddLaptopSpicification();
                    break;
                case 3:
                    tabletList.Clear();
                    foreach (var i in ItemsPage.Tablet_PCs)
                        tabletList.Add(i);
                    FillListViewWithTablet();
                    FillGridViewWithTablet();
                    FillFlipViewWithTablet();
                    AddTabletSpicification();
                    break;
                case 4:
                    break;
                case 5:
                    break;
                case 6:
                    break;
            }
            locked = false;
            ExpandButton.Content = "Shrink➡";
            Window.Current.SizeChanged += this.WindowSizeChanged;
            Initialisation();
            firstUse = false;
        }

        private void DesignSpicification()
        {
            SP1ComboBox.Items.Add("Any");
            SP2ComboBox.Items.Add("Any");
            SP3ComboBox.Items.Add("Any");
            SP4ComboBox.Items.Add("Any");
            SP5ComboBox.Items.Add("Any");
            SP6ComboBox.Items.Add("Any");
            SP1ComboBox.SelectedIndex = 0;
            SP2ComboBox.SelectedIndex = 0;
            SP3ComboBox.SelectedIndex = 0;
            SP4ComboBox.SelectedIndex = 0;
            SP5ComboBox.SelectedIndex = 0;
            SP6ComboBox.SelectedIndex = 0;
        }

        private void Initialisation()
        {
            ApplyStyleSearchRefine(20, 400, 200);
            ApplicationViewState viewState = ApplicationView.Value;
            if (viewState == ApplicationViewState.Snapped)
            {
                ApplyStyle(Visibility.Collapsed, Visibility.Visible, new Thickness(100, 10, 0, 10), new Thickness(0, 10, 0, 0), 32);
                ResultViewer.Visibility = SearchRefine.Visibility = Visibility.Collapsed;
                if (firstUse)
                {
                    ApplyStyleSearchRefine(16, 320, 166);
                    SearchRefine.Visibility = Visibility.Visible;
                }
            }
            else if (viewState == ApplicationViewState.Filled || Window.Current.Bounds.Width < 1300)
                ApplyStyle(Visibility.Visible, Visibility.Collapsed, new Thickness(700, 10, 0, 10), new Thickness(100, 10, 400, 0), 36);
            else if (viewState == ApplicationViewState.FullScreenLandscape)
                ApplyStyle(Visibility.Visible, Visibility.Collapsed, new Thickness(900, 10, 0, 10), new Thickness(100, 10, 400, 0), 36);
        }

        private void ApplySpicification(FrameworkElement element)
        {
            element.HorizontalAlignment = HorizontalAlignment.Left;
            element.VerticalAlignment = VerticalAlignment.Top;
        }

        private void ApplyStyleSearchRefine(double SearchRefineFontSize,double SearchRefineGridWidth,double ComboBoxWidth)
        {
            SearchRefine.FontSize = SP7.FontSize = SP8.FontSize = SP9.FontSize = SearchRefineFontSize;
            SearchRefineGrid.Width = SearchRefineGridWidth;
            SP1ComboBox.Width = SP2ComboBox.Width = SP3ComboBox.Width = SP4ComboBox.Width = SP5ComboBox.Width = SP6ComboBox.Width = ComboBoxWidth;
        }

        private void ApplyStyle(Visibility SearchResultGridViewAndExpandButtonVisibility, Visibility SearchResultListViewVisibility, Thickness Button1Margin, Thickness textBlock19Margin, Double textBlock19FontSize)
        {
            SearchResultGridView.Visibility = ExpandButton.Visibility = SearchResultGridViewAndExpandButtonVisibility;
            SearchResultListView.Visibility = SearchButton.Visibility = SearchResultListViewVisibility;
            OKButton.Margin = Button1Margin;
            NoResultTitle.Margin = textBlock19Margin;
            NoResultTitle.FontSize = textBlock19FontSize;
            if(ExpandButton.Content.Equals("Shrink➡"))
                SearchRefine.Visibility = Visibility.Visible;
        }

        private void Expand(bool _expand, Thickness SearchResultGridViewMargin, Thickness ExpandButtonMargin, string ExpandButtonContent, Visibility SearchRefineVisibility)
        {
            expand = _expand;
            SearchResultGridView.Margin = SearchResultGridViewMargin;
            ExpandButton.Margin = ExpandButtonMargin;
            ExpandButton.Content = ExpandButtonContent;
            SearchRefine.Visibility = SearchRefineVisibility;
            //DoubleAnimation b = new DoubleAnimation();
            //b.From = 100;
            //b.To = 1000;
            //b.SpeedRatio = 10;
            //b.SetValue(null,SearchResultGridView);
        }

        private Grid MakeGridForListView(string title, string content1, string content2, Uri imagePath)
        {
            Grid Gridi = new Grid();
            Gridi.Width = 300;
            Gridi.Height = 100;
            Gridi.Background = new SolidColorBrush(Colors.White);

            Image image1 = new Image();
            image1.Height = 90;
            image1.Margin = new Thickness(5, 5, 0, 0);
            image1.Width = 90;
            image1.Source = new BitmapImage(imagePath);
            ApplySpicification(image1);

            TextBlock textBlock1 = new TextBlock();
            textBlock1.Margin = new Thickness(100, 10, 10, 0);
            textBlock1.Text = title;
            textBlock1.FontSize = 16;
            textBlock1.TextWrapping = TextWrapping.Wrap;
            textBlock1.Foreground = new SolidColorBrush(Colors.Black);
            ApplySpicification(textBlock1);

            TextBlock textBlock2 = new TextBlock();
            textBlock2.Margin = new Thickness(100, 55, 10, 0);
            textBlock2.Text = content1;
            SolidColorBrush ff=new SolidColorBrush();
            textBlock2.Foreground = new SolidColorBrush(Colors.Black);
            ApplySpicification(textBlock2);

            TextBlock textBlock3 = new TextBlock();
            textBlock3.Margin = new Thickness(100, 75, 10, 0);
            textBlock3.Text = content2;
            textBlock3.Foreground = new SolidColorBrush(Colors.Black);
            ApplySpicification(textBlock3);

            Gridi.Children.Add(image1);
            Gridi.Children.Add(textBlock1);
            Gridi.Children.Add(textBlock2);
            Gridi.Children.Add(textBlock3);

            return Gridi;
        }

        private Grid MakeGridForGridView(string title, string content1, string content2, string content3, Uri imagePath)
        {
            Grid Gridi = new Grid();
            Gridi.Width = 450;
            Gridi.Height = 140;
            Gridi.Background = new SolidColorBrush(Colors.White);

            Image image1 = new Image();
            image1.Height = 110;
            image1.Margin = new Thickness(10, 15, 0, 0);
            image1.Width = 110;
            image1.Source = new BitmapImage(imagePath);
            ApplySpicification(image1);

            TextBlock textBlock1 = new TextBlock();
            textBlock1.Margin = new Thickness(130, 10, 10, 0);
            textBlock1.Text = title;
            textBlock1.FontSize = 18;
            textBlock1.TextWrapping = TextWrapping.Wrap;
            textBlock1.Foreground = new SolidColorBrush(Colors.Black);
            ApplySpicification(textBlock1);

            TextBlock textBlock2 = new TextBlock();
            textBlock2.Margin = new Thickness(130, 50, 10, 0);
            textBlock2.Text = content1;
            textBlock2.Foreground = new SolidColorBrush(Colors.Black);
            ApplySpicification(textBlock2);

            TextBlock textBlock3 = new TextBlock();
            textBlock3.Margin = new Thickness(130, 75, 10, 0);
            textBlock3.Text = content2;
            textBlock3.Foreground = new SolidColorBrush(Colors.Black);
            ApplySpicification(textBlock3);

            TextBlock textBlock4 = new TextBlock();
            textBlock4.Margin = new Thickness(130, 100, 10, 0);
            textBlock4.Text = content3;
            textBlock4.Foreground = new SolidColorBrush(Colors.Black);
            ApplySpicification(textBlock4);

            Gridi.Children.Add(image1);
            Gridi.Children.Add(textBlock1);
            Gridi.Children.Add(textBlock2);
            Gridi.Children.Add(textBlock3);
            Gridi.Children.Add(textBlock4);

            return Gridi;
        }

        private Grid MakeGridForFlipView(string name, string sp1, string sp2, string sp3, string sp4, string sp5, string sp6, string sp7, string sp8, Uri imagePath)
        {
            Grid Gridi = new Grid();
            Gridi.HorizontalAlignment = HorizontalAlignment.Left;
            counter++;
            Gridi.Height = 568;
            Gridi.Margin = new Thickness(0, 0, 0, 0);
            Gridi.VerticalAlignment = VerticalAlignment.Top;
            Gridi.Width = 1166;

            Image Imagei = new Image();
            Imagei.Margin = new Thickness(10, 75, 0, 0);
            Imagei.Width = 400;
            Imagei.Height = 300;
            Imagei.Source = new BitmapImage(imagePath);
            ApplySpicification(Imagei);
            Gridi.Children.Add(Imagei);

            TextBlock TextBlock_1 = new TextBlock();
            TextBlock_1.Margin = new Thickness(25, 450, 0, 25);
            TextBlock_1.TextTrimming = TextTrimming.WordEllipsis;
            TextBlock_1.Width = 350;
            TextBlock_1.TextAlignment = TextAlignment.Center;
            TextBlock_1.FontSize = 32;
            TextBlock_1.Text = name;
            ApplySpicification(TextBlock_1);
            Gridi.Children.Add(TextBlock_1);

            TextBlock TextBlock_2 = new TextBlock();
            TextBlock_2.Margin = new Thickness(425, 50, 25, 0);
            TextBlock_2.TextTrimming = TextTrimming.WordEllipsis;
            TextBlock_2.Text = sp1;
            ApplySpicification(TextBlock_2);
            Gridi.Children.Add(TextBlock_2);

            TextBlock TextBlock_3 = new TextBlock();
            TextBlock_3.Margin = new Thickness(425, 100, 25, 0);
            TextBlock_3.TextTrimming = TextTrimming.WordEllipsis;
            TextBlock_3.Text = sp2;
            ApplySpicification(TextBlock_3);
            Gridi.Children.Add(TextBlock_3);

            TextBlock TextBlock_4 = new TextBlock();
            TextBlock_4.Margin = new Thickness(425, 150, 25, 0);
            TextBlock_4.TextTrimming = TextTrimming.WordEllipsis;
            TextBlock_4.Text = sp3;
            ApplySpicification(TextBlock_4);
            Gridi.Children.Add(TextBlock_4);

            TextBlock TextBlock_5 = new TextBlock();
            TextBlock_5.Margin = new Thickness(425, 200, 25, 0);
            TextBlock_5.TextTrimming = TextTrimming.WordEllipsis;
            TextBlock_5.Text = sp4;
            ApplySpicification(TextBlock_5);
            Gridi.Children.Add(TextBlock_5);

            TextBlock TextBlock_6 = new TextBlock();
            TextBlock_6.Margin = new Thickness(425, 250, 25, 0);
            TextBlock_6.TextTrimming = TextTrimming.WordEllipsis;
            TextBlock_6.Text = sp5;
            ApplySpicification(TextBlock_6);
            Gridi.Children.Add(TextBlock_6);

            TextBlock TextBlock_7 = new TextBlock();
            TextBlock_7.Margin = new Thickness(425, 300, 25, 0);
            TextBlock_7.TextTrimming = TextTrimming.WordEllipsis;
            TextBlock_7.Text = sp6;
            ApplySpicification(TextBlock_7);
            Gridi.Children.Add(TextBlock_7);

            TextBlock TextBlock_8 = new TextBlock();
            TextBlock_8.Margin = new Thickness(425, 350, 25, 0);
            TextBlock_8.TextTrimming = TextTrimming.WordEllipsis;
            TextBlock_8.Text = sp7;
            ApplySpicification(TextBlock_8);
            Gridi.Children.Add(TextBlock_8);

            TextBlock TextBlock_9 = new TextBlock();
            TextBlock_9.Margin = new Thickness(425, 400, 25, 0);
            TextBlock_9.TextTrimming = TextTrimming.WordEllipsis;
            TextBlock_9.Text = sp8;
            ApplySpicification(TextBlock_9);
            Gridi.Children.Add(TextBlock_9);

            return Gridi;
        }

        private void WindowSizeChanged(object sender, WindowSizeChangedEventArgs e)
        {
            Initialisation();
        }

        private void SearchResultGridView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (SearchResultGridView.SelectedIndex >= 0)
            {
                ResultViewer.Visibility = Visibility.Visible;
                FlipViewResult.SelectedIndex = SearchResultListView.SelectedIndex = SearchResultGridView.SelectedIndex;
                FlipViewResult.Visibility = Visibility.Visible;
            }
        }

        private void SearchResultListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if ((SearchResultListView.SelectedIndex >= 0) && (ApplicationView.TryUnsnap()))
            {
                ResultViewer.Visibility = Visibility.Visible;
                FlipViewResult.SelectedIndex = SearchResultGridView.SelectedIndex = SearchResultListView.SelectedIndex;
                FlipViewResult.Visibility = Visibility.Visible;
            }
        }

        private void ExpandButton_Click(object sender, RoutedEventArgs e)
        {
            if (expand)
                Expand(false, new Thickness(0, 0, 0, 0), new Thickness(0, 0, 10, 16), "⬅Expand", Visibility.Collapsed);
            else
                if (!(ApplicationView.Value == ApplicationViewState.Snapped && !ApplicationView.TryUnsnap()))
                    Expand(true, new Thickness(0, 0, 400, 0), new Thickness(0, 0, 410, 16), "Shrink➡", Visibility.Visible);
        }

        private void OKButton_Click(object sender, RoutedEventArgs e)
        {
            NoResultGrid.Visibility = Visibility.Collapsed;
            NoResultTextBlock.Visibility = Visibility.Visible;
        }

        private void SaveFileButton_Click(object sender, RoutedEventArgs e)
        {
            if (FlipViewResult.SelectedIndex > -1)
                switch (searchIn)
                {
                    case 1:
                        break;
                    case 2:
                        Functions.SaveFile(laptopList[FlipViewResult.SelectedIndex].ToHTML(), "Laptop");
                        break;
                    case 3:
                        Functions.SaveFile(tabletList[FlipViewResult.SelectedIndex].ToHTML(), "Tablet");
                        break;
                    case 4:
                        break;
                    case 5:
                        break;
                    case 6:
                        break;
                }
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            ResultViewer.Visibility = Visibility.Collapsed;
            FlipViewResult.Visibility = Visibility.Collapsed;
        }

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            SearchRefine.FontSize = 20;
            SearchRefineGrid.Width = 400;
            SearchResultGridView.Visibility = Visibility.Collapsed;
            SearchResultListView.Visibility = Visibility.Visible;
            SP7.FontSize = SP8.FontSize = SP9.FontSize = 20;
            SP1ComboBox.Width = SP2ComboBox.Width = SP3ComboBox.Width = SP4ComboBox.Width = SP5ComboBox.Width = SP6ComboBox.Width = 200;
            SearchButton.Visibility = Visibility.Collapsed;
            Expand(false, new Thickness(0, 0, 0, 0), new Thickness(0, 0, 10, 16), "⬅Expand", Visibility.Collapsed);
        }

        private void GridViewVisibility()
        {
            ApplicationViewState viewState = ApplicationView.Value;
            if ((viewState == ApplicationViewState.FullScreenLandscape) || (viewState == ApplicationViewState.Filled) || (Window.Current.Bounds.Width < 1300))
            {
                SearchResultGridView.Visibility = Visibility.Visible;
                SearchResultListView.Visibility = Visibility.Collapsed;
            }
            else if (viewState == ApplicationViewState.Snapped)
            {
                SearchResultGridView.Visibility = Visibility.Collapsed;
                SearchResultListView.Visibility = Visibility.Visible;
            }
        }

        #region Laptop Search
        private void AddLaptopSpicification()
        {
            DesignSpicification();
            List<string> tmp = new List<string>();
            SearchTitle.Text = "Laptop Specification";
            SP1TextBlock.Text = "Processor";
            SP2TextBlock.Text = "Processor Speed";
            SP3TextBlock.Text = "Memory Size";
            SP4TextBlock.Text = "Graphic Card";
            SP5TextBlock.Text = "Hard Disk";
            SP6TextBlock.Text = "Screen Size";
            SP7.Header = "Bluetooth";
            SP8.Header = "Card Reader";
            SP9.Header = "Touch Screen";
            SP1ComboBox.SelectionChanged += SearchLaptop;
            SP2ComboBox.SelectionChanged += SearchLaptop;
            SP3ComboBox.SelectionChanged += SearchLaptop;
            SP4ComboBox.SelectionChanged += SearchLaptop;
            SP5ComboBox.SelectionChanged += SearchLaptop;
            SP6ComboBox.SelectionChanged += SearchLaptop;
            SP7.Toggled += SearchLaptop;
            SP8.Toggled += SearchLaptop;
            SP9.Toggled += SearchLaptop;
            SP9.Visibility = Visibility.Visible;
            tmp.Clear();
            for (int i = 0; i < ItemsPage.Laptops.Count; i++)
                if (!Functions.item_Existing(tmp, ItemsPage.Laptops[i].Processor))
                    tmp.Add(ItemsPage.Laptops[i].Processor);
            tmp.Sort();
            foreach (var i in tmp)
                SP1ComboBox.Items.Add(i);
            tmp.Clear();
            for (int i = 0; i < ItemsPage.Laptops.Count; i++)
                if (!Functions.item_Existing(tmp, ItemsPage.Laptops[i].Processor_Speed))
                    tmp.Add(ItemsPage.Laptops[i].Processor_Speed);
            tmp.Sort();
            foreach (var i in tmp)
                SP2ComboBox.Items.Add(i);
            tmp.Clear();
            for (int i = 0; i < ItemsPage.Laptops.Count; i++)
                if (!Functions.item_Existing(tmp, ItemsPage.Laptops[i].RAM))
                    tmp.Add(ItemsPage.Laptops[i].RAM);
            tmp.Sort();
            foreach (var i in tmp)
                SP3ComboBox.Items.Add(i);
            tmp.Clear();
            for (int i = 0; i < ItemsPage.Laptops.Count; i++)
                if (!Functions.item_Existing(tmp, ItemsPage.Laptops[i].Graphic_Card) &&! ItemsPage.Laptops[i].Graphic_Card.Contains("Graphic"))
                    tmp.Add(ItemsPage.Laptops[i].Graphic_Card);
            tmp.Sort();
            SP4ComboBox.Items.Add("Internal Graphic");
            foreach (var i in tmp)
                SP4ComboBox.Items.Add(i);
            tmp.Clear();
            for (int i = 0; i < ItemsPage.Laptops.Count; i++)
                if (!Functions.item_Existing(tmp, ItemsPage.Laptops[i].Hard_Disk))
                    tmp.Add(ItemsPage.Laptops[i].Hard_Disk);
            tmp.Sort();
            foreach (var i in tmp)
                SP5ComboBox.Items.Add(i);
            tmp.Clear();
            for (int i = 0; i < ItemsPage.Laptops.Count; i++)
                if (!Functions.item_Existing(tmp, ItemsPage.Laptops[i].Screen_Size))
                    tmp.Add(ItemsPage.Laptops[i].Screen_Size);
            tmp.Sort();
            foreach (var i in tmp)
                SP6ComboBox.Items.Add(i);
        }

        private void FillListViewWithLaptop()
        {
            SearchResultListView.Items.Clear();
            if (laptopList.Count > 0)
                foreach (var i in laptopList)
                    SearchResultListView.Items.Add(MakeGridForListView("Name: " + i.Company + " " + i.Name, i.Processor + " " + i.Processor_Name, i.Graphic_Card, new Uri("ms-appx:///Assets/unavailable.png")));
            else
            {
                SearchResultListView.Visibility = Visibility.Collapsed;
                NoResultGrid.Visibility = Visibility.Visible;
            }
        }

        private void FillGridViewWithLaptop()
        {
            SearchResultGridView.Items.Clear();
            if (laptopList.Count > 0)
            {
                foreach (var i in laptopList)
                    SearchResultGridView.Items.Add(MakeGridForGridView("Name: " + i.Company + " " + i.Name, "Processor: " + i.Processor + " " + i.Processor_Name, "RAM: " + i.RAM, "Graphic Card: " + i.Graphic_Card, new Uri("ms-appx:///Assets/unavailable.png")));
                GridViewVisibility();
            }
            else
            {
                SearchResultGridView.Visibility = Visibility.Collapsed;
                NoResultGrid.Visibility = Visibility.Visible;
            }
        }

        private void FillFlipViewWithLaptop()
        {
            FlipViewResult.Items.Clear();
            foreach (var i in laptopList)
                FlipViewResult.Items.Add(MakeGridForFlipView(i.Company + "\n" + i.Name, i.Processor + " " + i.Processor_Name,
                    i.RAM + " RAM", i.Hard_Disk + " Disk Size", i.Graphic_Card/*[Functions.GetIndexOfStr(i.Graphic_Card,(String)SP4ComboBox.SelectedItem)]*/, i.Screen_Size,
                    i.Camera + " Camera", Functions.bool2string(i.OS.Contains("-"), "No Oprating System", i.OS),
                    ""/*Functions.bool2string(i.Ports.Contains("-"), "No Bluetooth", i.Ports)*/, new Uri("ms-appx:///Assets/unavailablelarge.png")));
        }

        private void Find_Laptop()
        {
            Search_Result.Clear();
            string Processor = Functions.Combo2String((String)SP1ComboBox.SelectedItem);
            string ProcessorSpeed = Functions.Combo2String((String)SP2ComboBox.SelectedItem);
            string Memory = Functions.Combo2String((String)SP3ComboBox.SelectedItem);
            string Graphic;
            if (SP4ComboBox.SelectedIndex == 0)
                Graphic = "";
            else if (SP4ComboBox.SelectedIndex == 1)
                Graphic = "Graphic";
            else
                Graphic = Functions.Combo2String((String)SP4ComboBox.SelectedItem);
            string HardDisk = Functions.Combo2String((String)SP5ComboBox.SelectedItem);
            string Screen = Functions.Combo2String((String)SP6ComboBox.SelectedItem);
            bool Bluetooth = false;
            bool CardReader = false;
            bool TouchScreen = false;
            Bluetooth = !SP7.IsOn;
            CardReader = !SP8.IsOn;
            TouchScreen = !SP9.IsOn;
            foreach (Laptop i in ItemsPage.Laptops)
            {
                if (SP7.IsOn)
                    Bluetooth = SP7.IsOn & i.Ports.Contains("Bluetooth");
                if (SP8.IsOn)
                    CardReader = SP8.IsOn & i.Ports.Contains("Card Reader");
                if (SP9.IsOn)
                    TouchScreen = SP9.IsOn & i.Touch_Screen;
                if (i.Processor.Contains(Processor) && i.Processor_Speed.Contains(ProcessorSpeed) && i.RAM.Contains(Memory) && i.Graphic_Card.Contains(Graphic) && i.Screen_Size.Contains(Screen) && i.Hard_Disk.Contains(HardDisk) && CardReader && TouchScreen && Bluetooth)
                    Search_Result.Add(i.ID - 1);
            }
            laptopList.Clear();
            foreach (int i in Search_Result)
                laptopList.Add(ItemsPage.Laptops[i]);
        }

        private void SearchLaptop(object sender, SelectionChangedEventArgs e)
        {
            SearchLaptop();
        }

        private void SearchLaptop(object sender, RoutedEventArgs e)
        {
            SearchLaptop();
        }

        private void SearchLaptop()
        {
            if (!locked)
            {
                NoResultGrid.Visibility = Visibility.Collapsed;
                NoResultTextBlock.Visibility = Visibility.Collapsed;
                Find_Laptop();
                FillGridViewWithLaptop();
                FillFlipViewWithLaptop();
                FillListViewWithLaptop();
            }
        }
        #endregion

        #region Tablet Search
        private void AddTabletSpicification()
        {
            DesignSpicification();
            List<string> tmp = new List<string>();
            SearchTitle.Text = "Tablet Specification";
            SP1TextBlock.Text = "Screen";
            SP2TextBlock.Text = "OS";
            SP3TextBlock.Text = "Weight";
            SP4TextBlock.Text = "Processor";
            SP5TextBlock.Text = "Internal Memory";
            SP6TextBlock.Text = "Camera";
            SP7.Header = "Flash";
            SP8.Header = "Memory Card";
            SP1ComboBox.SelectionChanged += SearchTablet;
            SP2ComboBox.SelectionChanged += SearchTablet;
            SP3ComboBox.SelectionChanged += SearchTablet;
            SP4ComboBox.SelectionChanged += SearchTablet;
            SP5ComboBox.SelectionChanged += SearchTablet;
            SP6ComboBox.SelectionChanged += SearchTablet;
            SP7.Toggled += SearchTablet;
            SP8.Toggled += SearchTablet;
            SP9.Visibility = Visibility.Collapsed;
            SearchButton.Margin = new Thickness(20, 580, 20, 0);
            tmp.Clear();
            for (int i = 0; i < ItemsPage.Tablet_PCs.Count; i++)
                if (!Functions.item_Existing(tmp, ItemsPage.Tablet_PCs[i].Screen_Size))
                    tmp.Add(ItemsPage.Tablet_PCs[i].Screen_Size);
            tmp.Sort();
            foreach (var i in tmp)
                SP1ComboBox.Items.Add(i);
            tmp.Clear();
            for (int i = 0; i < ItemsPage.Tablet_PCs.Count; i++)
                if (!Functions.item_Existing(tmp, ItemsPage.Tablet_PCs[i].OS))
                    tmp.Add(ItemsPage.Tablet_PCs[i].OS);
            tmp.Sort();
            foreach (var i in tmp)
                SP2ComboBox.Items.Add(i);
            tmp.Clear();
            for (int i = 0; i < ItemsPage.Tablet_PCs.Count; i++)
                if (!Functions.item_Existing(tmp, ItemsPage.Tablet_PCs[i].Weight))
                    tmp.Add(ItemsPage.Tablet_PCs[i].Weight);
            tmp.Sort();
            foreach (var i in tmp)
                SP3ComboBox.Items.Add(i);
            tmp.Clear();
            for (int i = 0; i < ItemsPage.Tablet_PCs.Count; i++)
                if (!Functions.item_Existing(tmp, ItemsPage.Tablet_PCs[i].Processor_Name))
                    tmp.Add(ItemsPage.Tablet_PCs[i].Processor_Name);
            tmp.Sort();
            foreach (var i in tmp)
                SP4ComboBox.Items.Add(i);
            tmp.Clear();
            for (int i = 0; i < ItemsPage.Tablet_PCs.Count; i++)
                if (!Functions.item_Existing(tmp, ItemsPage.Tablet_PCs[i].Internal_Memory))
                    tmp.Add(ItemsPage.Tablet_PCs[i].Internal_Memory);
            tmp.Sort();
            foreach (var i in tmp)
                SP5ComboBox.Items.Add(i);
            tmp.Clear();
            for (int i = 0; i < ItemsPage.Tablet_PCs.Count; i++)
                if (!Functions.item_Existing(tmp, ItemsPage.Tablet_PCs[i].Camera))
                    tmp.Add(ItemsPage.Tablet_PCs[i].Camera);
            tmp.Sort();
            foreach (var i in tmp)
                SP6ComboBox.Items.Add(i); 
        }

        private void FillListViewWithTablet()
        {
            SearchResultListView.Items.Clear();
            if (tabletList.Count > 0)
                foreach (var i in tabletList)
                    SearchResultListView.Items.Add(MakeGridForListView("Name: " + i.Company + " " + i.Name, i.Screen_Size + " (" + i.Screen_Resolution + ")", i.OS, new Uri("ms-appx:///Assets/unavailable.png")));
            else
            {
                SearchResultListView.Visibility = Visibility.Collapsed;
                NoResultGrid.Visibility = Visibility.Visible;
            }
        }

        private void FillGridViewWithTablet()
        {
            SearchResultGridView.Items.Clear();
            if (tabletList.Count > 0)
            {
                foreach (var i in tabletList)
                    SearchResultGridView.Items.Add(MakeGridForGridView("Name: " + i.Company + " " + i.Name, "Screen: " + i.Screen_Size + " (" + i.Screen_Resolution + ")", "OS: " + i.OS, "Camera: " + i.Camera, new Uri("ms-appx:///Assets/unavailable.png")));
                SearchResultGridView.Visibility = Visibility.Visible;
            }
            else
            {
                NoResultGrid.Visibility = Visibility.Visible;
                SearchResultGridView.Visibility = Visibility.Collapsed;
            }
        }

        private void FillFlipViewWithTablet()
        {
            FlipViewResult.Items.Clear();
            foreach (var i in tabletList)
                FlipViewResult.Items.Add(MakeGridForFlipView(i.Company + "\n" + i.Name, i.Processor_Name + " " + i.Processor_Speed, i.Memory_Size + " RAM",
                    i.Internal_Memory + " Disk Size", Functions.bool2string(i.Memory_Card, "Up to " + i.Max_Memory_Card_Size, "No") + " Memory Card",
                    i.OS, i.Camera + " Camera" + Functions.bool2string(i.Flash, " With Flash", ""), i.Screen_Size + "\"(" + i.Screen_Resolution + ")",
                    Functions.bool2string(i.Bluetooth.Contains("-"), i.Bluetooth, "No Bluetooth"), new Uri("ms-appx:///Assets/unavailablelarge.png")));
        }

        private void Find_Tablet()
        {
            Search_Result.Clear();
            string Screen = Functions.Combo2String((String)SP1ComboBox.SelectedItem);
            string OS = Functions.Combo2String((String)SP2ComboBox.SelectedItem);
            string Weight = Functions.Combo2String((String)SP3ComboBox.SelectedItem);
            string Processor = Functions.Combo2String((String)SP4ComboBox.SelectedItem);
            string InternalMemory = Functions.Combo2String((String)SP5ComboBox.SelectedItem);
            string Camera = Functions.Combo2String((String)SP6ComboBox.SelectedItem);
            bool Flash = false;
            bool MemoryCard = false;
            Flash = !SP7.IsOn;
            MemoryCard = !SP8.IsOn;
            foreach (var i in ItemsPage.Tablet_PCs)
            {
                if (SP7.IsOn)
                    Flash = SP7.IsOn & i.Flash;
                if (SP8.IsOn)
                    MemoryCard = SP8.IsOn & i.Memory_Card;
                if (i.Screen_Size.Contains(Screen) && i.OS.Contains(OS) && i.Weight.Contains(Weight) && i.Processor_Name.Contains(Processor) && i.Internal_Memory.Contains(InternalMemory) && i.Camera.Contains(Camera) && Flash && MemoryCard)
                    Search_Result.Add(i.ID - 1);
            }
            tabletList.Clear();
            foreach (int i in Search_Result)
                tabletList.Add(ItemsPage.Tablet_PCs[i]);
        }

        private void SearchTablet(object sender, RoutedEventArgs e)
        {
            SearchTablet();
        }

        private void SearchTablet(object sender, SelectionChangedEventArgs e)
        {
            SearchTablet();
        }

        private void SearchTablet()
        {
            if (!locked)
            {
                NoResultGrid.Visibility = Visibility.Collapsed;
                NoResultTextBlock.Visibility = Visibility.Collapsed;
                Find_Tablet();
                FillGridViewWithTablet();
                FillFlipViewWithTablet();
                FillListViewWithTablet();
            }
        }
        #endregion
    }
}//777 //757 //632 //761