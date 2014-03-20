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
    public sealed partial class Choose : Page
    {
        private List<Device> result = new List<Device>();
        private Device device;
        private bool locked = true;
        private string category = "Laptop";
        private NavigationHelper navigationHelper;
        int[] gridview_height = { 0, 360, 636, 912, 1188, 1464, 1740, 2016 };
        const string package = "ms-appx:///";
        private ObservableDictionary defaultViewModel = new ObservableDictionary();

        public ObservableDictionary DefaultViewModel
        {
            get { return this.defaultViewModel; }
        }

        public NavigationHelper NavigationHelper
        {
            get { return this.navigationHelper; }
        }

        public Choose()
        {
            this.InitializeComponent();
            this.navigationHelper = new NavigationHelper(this);
            this.navigationHelper.LoadState += navigationHelper_LoadState;
            this.navigationHelper.SaveState += navigationHelper_SaveState;
        }

        private void navigationHelper_LoadState(object sender, LoadStateEventArgs e)
        {
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

        private void pageRoot_Loaded(object sender, RoutedEventArgs e)
        {
            if (categories.SelectedIndex < 0)
                categories.SelectedIndex = 0;
        }

        private void Categories_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (categories.SelectedIndex < 0 || container == null || SP9 == null)
                return;
            locked = true;
            category = categories.SelectedIndex == 0 ? "Laptop" : "Tablet";
            if (category.Equals("Laptop"))
                AddLaptopSpicification();
            else
                AddTabletSpicification();
            locked = false;
            Search();
        }

        private void DesignSpicification()
        {
            ComboBox[] list = { SP1ComboBox, SP2ComboBox, SP3ComboBox, SP4ComboBox, SP5ComboBox, SP6ComboBox };
            foreach (ComboBox i in list)
            {
                i.Items.Clear();
                i.Items.Add("Any");
            }
            SP7.IsOn = SP8.IsOn = SP9.IsOn = false;
        }

        private void Fill(ComboBox combo, List<string> values)
        {
            foreach (string i in General.Distinct(values))
                combo.Items.Add(i);
            combo.SelectedIndex = 0;
        }

        private void AddLaptopSpicification()
        {
            DesignSpicification();
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
            SP9.Visibility = Visibility.Visible;
            List<string> graphic = new List<string>();
            foreach (Laptop i in General.Laptops)
                foreach (string j in General.AddToList(i.Graphic_Card))
                    if (!j.Contains("Graphic"))
                        graphic.Add(j);
            SP4ComboBox.Items.Add("Internal Graphic");
            Fill(SP1ComboBox, General.Laptops.Select(i => i.Processor).ToList());
            Fill(SP2ComboBox, General.Laptops.Select(i => i.Processor_Speed).ToList());
            Fill(SP3ComboBox, General.Laptops.Select(i => i.RAM).ToList());
            Fill(SP4ComboBox, graphic);
            Fill(SP5ComboBox, General.Laptops.Select(i => i.Hard_Disk).ToList());
            Fill(SP6ComboBox, General.Laptops.Select(i => i.Screen_Size).ToList());
        }

        private void AddTabletSpicification()
        {
            DesignSpicification();
            SearchTitle.Text = "Tablet Specification";
            SP1TextBlock.Text = "Screen";
            SP2TextBlock.Text = "OS";
            SP3TextBlock.Text = "Weight";
            SP4TextBlock.Text = "Processor";
            SP5TextBlock.Text = "Internal Memory";
            SP6TextBlock.Text = "Camera";
            SP7.Header = "Flash";
            SP8.Header = "Memory Card";
            SP9.Visibility = Visibility.Collapsed;
            Fill(SP1ComboBox, General.Tablet_PCs.Select(i => i.Screen_Size).ToList());
            Fill(SP2ComboBox, General.Tablet_PCs.Select(i => i.OS).ToList());
            Fill(SP3ComboBox, General.Tablet_PCs.Select(i => i.Weight).ToList());
            Fill(SP4ComboBox, General.Tablet_PCs.Select(i => i.Processor_Name).ToList());
            Fill(SP5ComboBox, General.Tablet_PCs.Select(i => i.Internal_Memory).ToList());
            Fill(SP6ComboBox, General.Tablet_PCs.Select(i => i.Camera).ToList());
        }

        private void Search_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Search();
        }

        private void Search_Toggled(object sender, RoutedEventArgs e)
        {
            Search();
        }

        private void Search()
        {
            if (locked)
                return;
            result.Clear();
            if (category.Equals("Laptop"))
            {
                string Graphic;
                if (SP4ComboBox.SelectedIndex == 1)
                    Graphic = "Graphic";
                else
                    Graphic = General.Combo2String((String)SP4ComboBox.SelectedItem);
                foreach (Laptop i in General.Find_Laptop(General.Combo2String((String)SP1ComboBox.SelectedItem), General.Combo2String((String)SP2ComboBox.SelectedItem), General.Combo2String((String)SP3ComboBox.SelectedItem), Graphic, General.Combo2String((String)SP5ComboBox.SelectedItem), General.Combo2String((String)SP6ComboBox.SelectedItem), SP7.IsOn, SP8.IsOn, SP9.IsOn))
                    result.Add(i);
            }
            else
                foreach (TabletPC i in General.Find_Tablet(General.Combo2String((String)SP1ComboBox.SelectedItem), General.Combo2String((String)SP2ComboBox.SelectedItem), General.Combo2String((String)SP3ComboBox.SelectedItem), General.Combo2String((String)SP4ComboBox.SelectedItem), General.Combo2String((String)SP5ComboBox.SelectedItem), General.Combo2String((String)SP6ComboBox.SelectedItem), SP7.IsOn, SP8.IsOn))
                    result.Add(i);
            FillResult();
        }

        private void FillResult()
        {
            container.Children.Clear();
            device = null;
            Rectangle rec = new Rectangle();
            rec.StrokeThickness = 3;
            rec.Stroke = new SolidColorBrush(Colors.LightCyan);
            TextBlock text = new TextBlock();
            text.Text = result.Count + " " + category + General.bool2string(result.Count == 1, "", "s");
            text.FontSize = 40;
            text.VerticalAlignment = VerticalAlignment.Top;
            text.TextAlignment = TextAlignment.Center;
            text.Margin = new Thickness(0, 5, 0, 0);
            GridView GV = new GridView();
            GV.Margin = new Thickness(0, 40, 0, 20);
            GV.Padding = new Thickness(10);
            foreach (Device i in result)
                GV.Items.Add(Desktop.CreateGridViewItem(package + General.Picture(i), General.Title(i), package + General.Icon(i)));
            GV.SelectionChanged += Result_SelectionChanged;
            Grid grid = new Grid();
            grid.VerticalAlignment = VerticalAlignment.Top;
            grid.Children.Add(rec);
            grid.Children.Add(text);
            grid.Children.Add(GV);
            grid.Margin = new Thickness(10, 10, 20, 0);
            int rows = GV.Items.Count / Desktop.Per_Row(container) + (GV.Items.Count % Desktop.Per_Row(container) > 0 ? 1 : 0);
            grid.Height = rows == 0 ? 100 : (rows < gridview_height.Length ? gridview_height[rows] : 84 + rows * 276);
            container.Children.Add(grid);
            container_scroll.ChangeView(null, 0, null);
        }

        private void page_scroll_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            page_grid.Width = Math.Max(1000, e.NewSize.Width);
        }

        private void container_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (e.NewSize.Width != e.PreviousSize.Width)
                Desktop.Relayout(container, 100);
        }

        private void Result_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            GridView GV = (GridView)sender;
            if (GV.SelectedIndex < 0)
                return;
            while (container.Children.Count > 1)
                container.Children.RemoveAt(1);
            device = result[GV.SelectedIndex];
            Grid last = (Grid)container.Children[0];
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
            StackPanel panel = Specification.CreateSpecification(device);
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
            grid.Margin = new Thickness(10, last.Height + last.Margin.Top + 10, 20, 20);
            container.Children.Add(grid);
            container.UpdateLayout();
            container_scroll.ChangeView(null, grid.Margin.Top - 10, null);
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
    }
}
