using Computer.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.UI.Xaml.Shapes;

namespace Computer
{
    public sealed partial class Specification : Page
    {
        private string[] categories_list = { "Processor", "Motherboard", "Graphic Card", "Storage Device" };
        private Dictionary<string, string[]> titles = new Dictionary<string, string[]>();
        private List<Device> devices = new List<Device>();
        private List<string> chosen = new List<string>();
        private string category;
        private Device device;
        private NavigationHelper navigationHelper;
        int[] gridview_height = { 0, 360, 636, 912, 1188, 1464, 1740, 2016 };
        const string images = "ms-appx:///Assets/Images/Desktop/";
        const string package = "ms-appx:///";
        const string no_image = "ms-appx:///Assets/Images/Icons/generic.png";
        private ObservableDictionary defaultViewModel = new ObservableDictionary();

        public ObservableDictionary DefaultViewModel
        {
            get { return this.defaultViewModel; }
        }

        public NavigationHelper NavigationHelper
        {
            get { return this.navigationHelper; }
        }

        public Specification()
        {
            this.InitializeComponent();
            this.navigationHelper = new NavigationHelper(this);
            this.navigationHelper.LoadState += navigationHelper_LoadState;
            this.navigationHelper.SaveState += navigationHelper_SaveState;
            titles.Add("Processor", new string[] { "Processor Company", "CPU Model", "Processor" });
            titles.Add("Motherboard", new string[] { "Mother Board Company", "MotherBoard" });
            titles.Add("Graphic Card", new string[] { "Graphic Card Type", "Graphic Card Company", "Graphic Card" });
            titles.Add("Storage Device", new string[] { "Storage Device Type", "Storage Device Company", "Storage Device" });
            titles.Add("Laptop", new string[] { "Laptop Company", "Laptop" });
            titles.Add("Tablet", new string[] { "Tablet Company", "Tablet" });
        }

        private void navigationHelper_LoadState(object sender, LoadStateEventArgs e)
        {
            if (e.NavigationParameter != null && e.NavigationParameter.ToString().Equals("Laptop"))
            {
                categories_list = new string[] { "Laptop", "Tablet" };
                pageTitle.Text = "Laptops, All in one PCs or Tablets";
            }
            categories.Items.Clear();
            foreach (string i in categories_list)
            {
                TextBlock text = new TextBlock();
                text.Text = i;
                text.Margin = new Thickness(60, 0, 0, 0);
                text.FontSize = 28;
                text.HorizontalAlignment = HorizontalAlignment.Left;
                text.Foreground = new SolidColorBrush(Color.FromArgb(255, 0, 229, 255));
                ListViewItem item = new ListViewItem();
                item.Content = text;
                categories.Items.Add(item);
            }
        }

        private void pageRoot_Loaded(object sender, RoutedEventArgs e)
        {
            if (categories.SelectedIndex < 0 && categories.Items.Count > 0)
                categories.SelectedIndex = 0;
        }

        private void navigationHelper_SaveState(object sender, SaveStateEventArgs e)
        {
        }

        #region NavigationHelper registration

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            navigationHelper.OnNavigatedTo(e);
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            navigationHelper.OnNavigatedFrom(e);
        }

        #endregion

        private void Categories_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (categories.SelectedIndex < 0 || container == null)
                return;
            category = categories_list[categories.SelectedIndex];
            devices = General.Devices(category);
            chosen.Clear();
            device = null;
            container.Children.Clear();
            CreateNextGrid(0);
        }

        private void Level_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            GridView GV = (GridView)sender;
            if (GV.SelectedItem == null)
                return;
            int level = (int)GV.Tag;
            int column = container.Children.Count;
            for (int i = 0; i < container.Children.Count; i++)
                if (container.Children[i] == GV.Parent)
                {
                    column = i + 1;
                    break;
                }
            while (container.Children.Count > column)
                container.Children.RemoveAt(column);
            while (chosen.Count > level)
                chosen.RemoveAt(level);
            chosen.Add(((GridViewItem)GV.SelectedItem).Tag.ToString());
            if (level + 1 < titles[category].Length)
                CreateNextGrid(level + 1);
            else
                ShowDevice();
        }

        private string Image_Of(Device item, int level)
        {
            string[] keys = General.Keys(item);
            if (item is HD && level == 0)
                return package + General.Icon(item);
            if (level == 0 || (level == 1 && (item is VGA || item is HD)))
                return package + General.Logo_Picture(keys[level]);
            return package + General.Picture(item);
        }

        private void CreateNextGrid(int level)
        {
            Rectangle rec = new Rectangle();
            rec.StrokeThickness = 3;
            rec.Stroke = new SolidColorBrush(Colors.LightCyan);
            TextBlock text = new TextBlock();
            text.Text = titles[category][level];
            text.FontSize = 40;
            text.VerticalAlignment = VerticalAlignment.Top;
            text.TextAlignment = TextAlignment.Center;
            text.Margin = new Thickness(0, 5, 0, 0);
            GridView GV = new GridView();
            GV.Margin = new Thickness(0, 40, 0, 20);
            GV.Padding = new Thickness(10);
            GV.Tag = level;
            List<string> list = new List<string>();
            foreach (Device i in devices)
            {
                string[] keys = General.Keys(i);
                if (General.Matches(i, chosen) && !General.item_Existing(list, keys[level]))
                {
                    list.Add(keys[level]);
                    GV.Items.Add(Desktop.CreateGridViewItem(Image_Of(i, level), keys[level], package + General.Icon(i)));
                }
            }
            GV.SelectionChanged += Level_SelectionChanged;
            Grid grid = new Grid();
            grid.VerticalAlignment = VerticalAlignment.Top;
            grid.Children.Add(rec);
            grid.Children.Add(text);
            grid.Children.Add(GV);
            grid.Margin = new Thickness(10, Top(), 20, 0);
            int rows = GV.Items.Count / Desktop.Per_Row(container) + (GV.Items.Count % Desktop.Per_Row(container) > 0 ? 1 : 0);
            grid.Height = rows < gridview_height.Length ? gridview_height[rows] : 84 + rows * 276;
            container.Children.Add(grid);
            ScrollTo(grid);
        }

        private void page_scroll_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            page_grid.Width = Math.Max(1000, e.NewSize.Width);
        }

        private void container_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (e.NewSize.Width != e.PreviousSize.Width)
                Desktop.Relayout(container, 0);
        }

        private double Top()
        {
            if (container.Children.Count == 0)
                return 10;
            Grid last = (Grid)container.Children[container.Children.Count - 1];
            return last.Height + last.Margin.Top + 10;
        }

        private void ScrollTo(Grid grid)
        {
            container.UpdateLayout();
            container_scroll.ChangeView(null, grid.Margin.Top - 10, null);
        }

        private void ShowDevice()
        {
            device = null;
            foreach (Device i in devices)
                if (General.Matches(i, chosen))
                {
                    device = i;
                    break;
                }
            if (device == null)
                return;
            Rectangle rec = new Rectangle();
            rec.StrokeThickness = 3;
            rec.Stroke = new SolidColorBrush(Colors.LightCyan);
            TextBlock text = new TextBlock();
            text.Text = General.Title(device);
            text.FontSize = 40;
            text.VerticalAlignment = VerticalAlignment.Top;
            text.TextAlignment = TextAlignment.Center;
            text.TextTrimming = TextTrimming.CharacterEllipsis;
            text.Margin = new Thickness(10, 5, 10, 0);
            StackPanel panel = CreateSpecification(device);
            panel.Margin = new Thickness(30, 60, 30, 20);
            Button save = new Button();
            save.Content = "Save";
            save.FontSize = 22;
            save.HorizontalAlignment = HorizontalAlignment.Right;
            save.Margin = new Thickness(0, 20, 0, 0);
            save.Click += Save_Click;
            panel.Children.Add(save);
            Grid grid = new Grid();
            grid.VerticalAlignment = VerticalAlignment.Top;
            grid.Children.Add(rec);
            grid.Children.Add(text);
            grid.Children.Add(panel);
            grid.Margin = new Thickness(10, Top(), 20, 20);
            container.Children.Add(grid);
            ScrollTo(grid);
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            if (device == null)
                return;
            string[][] subTitles;
            string[][] informations;
            device.Specification(out subTitles, out informations);
            General.SaveFile(General.MakeFullFile(category, subTitles, informations), category);
        }

        public static StackPanel CreateSpecification(Device item)
        {
            string[][] subTitles;
            string[][] informations;
            item.Specification(out subTitles, out informations);
            StackPanel panel = new StackPanel();
            Image picture = Desktop.CreateImage(package + General.Picture(item), package + General.Icon(item));
            picture.HorizontalAlignment = HorizontalAlignment.Left;
            picture.Margin = new Thickness(0, 0, 0, 10);
            panel.Children.Add(picture);
            for (int i = 0; i < subTitles.Length; i++)
            {
                TextBlock title = new TextBlock();
                title.Text = subTitles[i][0];
                title.FontSize = 28;
                title.Margin = new Thickness(0, 15, 0, 5);
                title.Foreground = new SolidColorBrush(Color.FromArgb(255, 0, 229, 255));
                panel.Children.Add(title);
                for (int j = 1; j < subTitles[i].Length; j++)
                    if (subTitles[i][j] != null)
                        panel.Children.Add(CreateRow(subTitles[i][j], Text(informations[i][j]), Colors.White));
            }
            return panel;
        }

        public static string Text(string information)
        {
            if (information == null || information.Trim().Length == 0)
                return "-";
            return information.Replace("<br/>", "\n").Replace(";", " / ").Trim();
        }

        public static Grid CreateRow(string title, string information, Color color)
        {
            Grid grid = new Grid();
            grid.Margin = new Thickness(0, 3, 0, 3);
            ColumnDefinition first = new ColumnDefinition();
            first.Width = new GridLength(250);
            grid.ColumnDefinitions.Add(first);
            grid.ColumnDefinitions.Add(new ColumnDefinition());
            TextBlock text = new TextBlock();
            text.Text = title;
            text.FontSize = 20;
            text.TextWrapping = TextWrapping.Wrap;
            text.Foreground = new SolidColorBrush(Colors.LightGray);
            TextBlock value = new TextBlock();
            value.Text = information;
            value.FontSize = 20;
            value.TextWrapping = TextWrapping.Wrap;
            value.Foreground = new SolidColorBrush(color);
            value.IsTextSelectionEnabled = true;
            Grid.SetColumn(value, 1);
            grid.Children.Add(text);
            grid.Children.Add(value);
            return grid;
        }
    }
}
