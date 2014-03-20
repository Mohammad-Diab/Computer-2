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

namespace Computer
{
    public sealed partial class Compare : Page
    {
        private string[] categories_list = { "Laptop", "Tablet" };
        private List<Device> devices = new List<Device>();
        private bool locked = false;
        private NavigationHelper navigationHelper;
        private ObservableDictionary defaultViewModel = new ObservableDictionary();

        public ObservableDictionary DefaultViewModel
        {
            get { return this.defaultViewModel; }
        }

        public NavigationHelper NavigationHelper
        {
            get { return this.navigationHelper; }
        }

        public Compare()
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
            if (categories.SelectedIndex < 0 || first_device == null || second_device == null || table == null)
                return;
            locked = true;
            devices = General.Devices(categories_list[categories.SelectedIndex]);
            first_device.Items.Clear();
            second_device.Items.Clear();
            foreach (Device i in devices)
            {
                first_device.Items.Add(General.Title(i));
                second_device.Items.Add(General.Title(i));
            }
            if (devices.Count > 1)
            {
                first_device.SelectedIndex = 0;
                second_device.SelectedIndex = 1;
            }
            locked = false;
            Fill_Table();
        }

        private void page_scroll_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            page_grid.Width = Math.Max(1000, e.NewSize.Width);
        }

        private void Device_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (!locked)
                Fill_Table();
        }

        private void Fill_Table()
        {
            table.Children.Clear();
            if (first_device.SelectedIndex < 0 || second_device.SelectedIndex < 0)
                return;
            string[][] subTitles;
            string[][] first;
            string[][] second;
            devices[first_device.SelectedIndex].Specification(out subTitles, out first);
            devices[second_device.SelectedIndex].Specification(out subTitles, out second);
            Grid pictures = new Grid();
            pictures.Margin = new Thickness(0, 15, 0, 0);
            ColumnDefinition column = new ColumnDefinition();
            column.Width = new GridLength(250);
            pictures.ColumnDefinitions.Add(column);
            pictures.ColumnDefinitions.Add(new ColumnDefinition());
            pictures.ColumnDefinitions.Add(new ColumnDefinition());
            Device[] both = { devices[first_device.SelectedIndex], devices[second_device.SelectedIndex] };
            for (int i = 0; i < both.Length; i++)
            {
                Image picture = Desktop.CreateImage("ms-appx:///" + General.Picture(both[i]), "ms-appx:///" + General.Icon(both[i]));
                picture.Width = 160;
                picture.Height = 160;
                picture.Stretch = Stretch.Uniform;
                picture.HorizontalAlignment = HorizontalAlignment.Left;
                Button save = new Button();
                save.Content = "Save";
                save.FontSize = 18;
                save.Margin = new Thickness(0, 10, 0, 0);
                save.Tag = both[i];
                save.Click += Save_Click;
                StackPanel column_panel = new StackPanel();
                column_panel.Children.Add(picture);
                column_panel.Children.Add(save);
                Grid.SetColumn(column_panel, i + 1);
                pictures.Children.Add(column_panel);
            }
            table.Children.Add(pictures);
            for (int i = 0; i < subTitles.Length; i++)
            {
                TextBlock title = new TextBlock();
                title.Text = subTitles[i][0];
                title.FontSize = 28;
                title.Margin = new Thickness(0, 15, 0, 5);
                title.Foreground = new SolidColorBrush(Color.FromArgb(255, 0, 229, 255));
                table.Children.Add(title);
                for (int j = 1; j < subTitles[i].Length; j++)
                    if (subTitles[i][j] != null)
                        table.Children.Add(CreateRow(subTitles[i][j], Specification.Text(first[i][j]), Specification.Text(second[i][j])));
            }
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            Device device = (Device)((Button)sender).Tag;
            string category = categories_list[categories.SelectedIndex];
            string[][] subTitles;
            string[][] informations;
            device.Specification(out subTitles, out informations);
            General.SaveFile(General.MakeFullFile(category, subTitles, informations), category);
        }

        private Grid CreateRow(string title, string first, string second)
        {
            Color color = first.Equals(second) ? Colors.White : Color.FromArgb(255, 153, 255, 47);
            Grid grid = Specification.CreateRow(title, first, color);
            grid.ColumnDefinitions.Add(new ColumnDefinition());
            TextBlock value = new TextBlock();
            value.Text = second;
            value.FontSize = 20;
            value.TextWrapping = TextWrapping.Wrap;
            value.Margin = new Thickness(20, 0, 0, 0);
            value.Foreground = new SolidColorBrush(color);
            value.IsTextSelectionEnabled = true;
            Grid.SetColumn(value, 2);
            grid.Children.Add(value);
            return grid;
        }
    }
}
