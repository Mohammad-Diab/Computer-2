using Computer.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.Storage.Streams;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;
using Windows.UI.Xaml.Shapes;

// The Basic Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234237

namespace Computer
{
    /// <summary>
    /// A basic page that provides characteristics common to most applications.
    /// </summary>
    public sealed partial class Desktop : Page
    {

        private List<string> MB_companies = new List<string>();
        private string[] processor_title = { "Platform", "Mother Board Company", "MotherBoard", "CPU Model", "Processor", "RAM Company (generic)", "RAM Size (generic)", "Graphic Card Type", "Graphic Card Company", "Graphic Card", "Storage Device Type", "Storage Device Company", "Storage Device", "Power Supply (generic)", "Case (generic)", "Optical Drive (generic)", "Monitor (generic)", "Mouse (generic)", "Keyboard (generic)" };
        private int[] section_level = { 0, 5, 7, 10, 13, 15, 19 };
        private List<PCItem> your_pc = new List<PCItem>();
        private List<int>[] level_index = new List<int>[19];
        private bool locked = false;
        private bool complete = false;
        private NavigationHelper navigationHelper;
        int[] gridview_height = { 0, 360, 636, 912, 1188, 1464, 1740, 2016 };
        const string images = "ms-appx:///Assets/Images/Desktop/";
        const string package = "ms-appx:///";
        const string no_image = "ms-appx:///Assets/Images/Icons/generic.png";
        private ObservableDictionary defaultViewModel = new ObservableDictionary();

        /// <summary>
        /// This can be changed to a strongly typed view model.
        /// </summary>
        public ObservableDictionary DefaultViewModel
        {
            get { return this.defaultViewModel; }
        }

        /// <summary>
        /// NavigationHelper is used on each page to aid in navigation and 
        /// process lifetime management
        /// </summary>
        public NavigationHelper NavigationHelper
        {
            get { return this.navigationHelper; }
        }


        public Desktop()
        {
            this.InitializeComponent();
            this.navigationHelper = new NavigationHelper(this);
            this.navigationHelper.LoadState += navigationHelper_LoadState;
            this.navigationHelper.SaveState += navigationHelper_SaveState;
        }

        /// <summary>
        /// Populates the page with content passed during navigation. Any saved state is also
        /// provided when recreating a page from a prior session.
        /// </summary>
        /// <param name="sender">
        /// The source of the event; typically <see cref="NavigationHelper"/>
        /// </param>
        /// <param name="e">Event data that provides both the navigation parameter passed to
        /// <see cref="Frame.Navigate(Type, Object)"/> when this page was initially requested and
        /// a dictionary of state preserved by this page during an earlier
        /// session. The state will be null the first time a page is visited.</param>
        private void navigationHelper_LoadState(object sender, LoadStateEventArgs e)
        {
        }

        /// <summary>
        /// Preserves state associated with this page in case the application is suspended or the
        /// page is discarded from the navigation cache.  Values must conform to the serialization
        /// requirements of <see cref="SuspensionManager.SessionState"/>.
        /// </summary>
        /// <param name="sender">The source of the event; typically <see cref="NavigationHelper"/></param>
        /// <param name="e">Event data that provides an empty dictionary to be populated with
        /// serializable state.</param>
        private void navigationHelper_SaveState(object sender, SaveStateEventArgs e)
        {
        }

        #region NavigationHelper registration

        /// The methods provided in this section are simply used to allow
        /// NavigationHelper to respond to the page's navigation methods.
        /// 
        /// Page specific logic should be placed in event handlers for the  
        /// <see cref="GridCS.Common.NavigationHelper.LoadState"/>
        /// and <see cref="GridCS.Common.NavigationHelper.SaveState"/>.
        /// The navigation parameter is available in the LoadState method 
        /// in addition to page state preserved during an earlier session.

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            navigationHelper.OnNavigatedTo(e);
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            navigationHelper.OnNavigatedFrom(e);
        }

        #endregion

        private void Platform_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (((GridView)sender).SelectedItem == null || your_pc.Count == 0)
                return;
            int column = container.Children.Count;
            int level;
            int.TryParse(((GridView)sender).Tag.ToString(), out level);
            object grid = ((GridView)sender).Parent;
            for (int i = 0; i < container.Children.Count; i++)
                if (container.Children[i] == grid)
                {
                    column = i + 1;
                    break;
                }
            for (int i = column; i < container.Children.Count; i++)
            {
                container.Children.RemoveAt(column);
                i--;
            }
            your_pc[level].index = ((GridView)sender).SelectedIndex;
            if (level_index[level] != null)
                your_pc[level].index = level_index[level][your_pc[level].index];
            your_pc[level].item = ((GridViewItem)((GridView)sender).SelectedItem).Tag.ToString();
            for (int i = level + 1; i < your_pc.Count; i++)
            {
                your_pc[i].index = -1;
                your_pc[i].item = null;
            }
            int next = level + 1;
            if (level == 7 && your_pc[7].item.Equals("Built-in GPU"))
            {
                your_pc[8].item = your_pc[9].item = "0";
                next = 10;
            }
            if (next < processor_title.Length)
                CreateNextGrid(next, processor_title[next]);
            Update_Sections();
            //switch (platform.SelectedIndex)
            //{
            //    case 0:
            //        CreateNextGrid(level + 1, processor_title[level + 1]);
            //        break;
            //    case 1:
            //        CreateNextGrid(level + 1, processor_title[level + 1]);
            //        break;
            //    default:
            //        break;
            //}
        }

        private void CreateNextGrid(int level,string next)
        {
            Rectangle rec = new Rectangle();
            rec.StrokeThickness = 3;
            rec.Stroke = new SolidColorBrush(Windows.UI.Colors.LightCyan);
            TextBlock text = new TextBlock();
            text.Text = next;
            text.FontSize = 40;
            text.VerticalAlignment = VerticalAlignment.Top;
            text.TextAlignment = TextAlignment.Center;
            text.Margin = new Thickness(0, 5, 0, 0);
            GridView GV = new GridView();
            GV.Margin = new Thickness(0, 40, 0, 20);
            GV.Padding = new Thickness(10);
            GV.Tag = level;
            GV.SelectionChanged += Platform_SelectionChanged;
            level_index[level] = null;
            int mb = your_pc[2].index;
            int cpu = your_pc[4].index;
            switch (level)
            {
                case 1:
                    foreach (string i in MB_companies)
                        GV.Items.Add(CreateGridViewItem(package + General.Logo_Picture(i), i, package + General.Icon("board")));
                    break;
                case 2:
                    level_index[level] = General.MotherBoards_Of(your_pc[0].item, your_pc[1].item);
                    foreach (int i in level_index[level])
                        GV.Items.Add(CreateGridViewItem(package + General.Picture(General.MotherBoards[i]), General.MotherBoards[i].Name, package + General.Icon("board")));
                    break;
                case 3:
                    foreach (string i in General.CPU_Models(your_pc[0].item, mb))
                        GV.Items.Add(CreateGridViewItem(images + General.Safe(i) + ".png", i, package + General.Logo_Picture(your_pc[0].item)));
                    break;
                case 4:
                    level_index[level] = General.Processors_Of(your_pc[3].item, mb);
                    foreach (int i in level_index[level])
                        GV.Items.Add(CreateGridViewItem(package + General.Picture(General.Processors[i]), General.Processors[i].Name, package + General.Logo_Picture(your_pc[0].item)));
                    break;
                case 5:
                    foreach (string i in new string[] { "ADATA", "Ketron", "Kingston", "Other (Unfamous Company)", "ZEPPLEIN" })
                        GV.Items.Add(CreateGridViewItem(package + General.Icon("ram"), i));
                    break;
                case 6:
                    foreach (string i in General.RAM_Sizes(mb))
                        GV.Items.Add(CreateGridViewItem(package + General.Icon("ram"), i));
                    break;
                case 7:
                    foreach (string i in General.GPU_Types(mb, cpu))
                        GV.Items.Add(CreateGridViewItem(package + General.Logo_Picture(General.bool2string(i.Equals("Built-in GPU"), your_pc[0].item, i)), i, package + General.Icon("gpu")));
                    break;
                case 8:
                    foreach (string i in General.GPU_Companies(your_pc[7].item, mb))
                        GV.Items.Add(CreateGridViewItem(package + General.Logo_Picture(i), i, package + General.Icon("gpu")));
                    break;
                case 9:
                    level_index[level] = General.Graphic_Cards_Of(your_pc[7].item, your_pc[8].item, mb);
                    foreach (int i in level_index[level])
                        GV.Items.Add(CreateGridViewItem(package + General.Picture(General.Graphigc_Cards[i]), General.Graphigc_Cards[i].Name, package + General.Icon("gpu")));
                    break;
                case 10:
                    GV.Items.Add(CreateGridViewItem(package + General.Icon("hdd"), "Hard Disk Drive"));
                    GV.Items.Add(CreateGridViewItem(package + General.Icon("ssd"), "Solid State Drive"));
                    break;
                case 11:
                    string kind = General.bool2string(your_pc[10].item.Equals("Solid State Drive"), "ssd", "hdd");
                    foreach (string i in General.HD_Companies(kind.ToUpper(), mb))
                        GV.Items.Add(CreateGridViewItem(package + General.Logo_Picture(General.Company(i)), i, package + General.Icon(kind)));
                    break;
                case 12:
                    level_index[level] = General.Hard_Disks_Of(General.bool2string(your_pc[10].item.Equals("Solid State Drive"), "SSD", "HDD"), your_pc[11].item);
                    foreach (int i in level_index[level])
                        GV.Items.Add(CreateGridViewItem(package + General.Picture(General.Hard_Disks[i]), General.Hard_Disks[i].Code + " " + General.Hard_Disks[i].Capacity));
                    break;
                case 13:
                    foreach (string i in new string[] { "400 Watt Power Supply", "500 Watt Power Supply", "600 Watt Power Supply", "750 Watt Power Supply" })
                        GV.Items.Add(CreateGridViewItem(package + General.Icon("psu"), i));
                    break;
                case 14:
                    foreach (string i in new string[] { "Mini Tower Case", "Mid Tower Case", "Full Tower Case" })
                        GV.Items.Add(CreateGridViewItem(package + General.Icon("case"), i));
                    break;
                case 15:
                    foreach (string i in new string[] { "DVD RW x24", "BlueRay Combo", "BlueRay RW" })
                        GV.Items.Add(CreateGridViewItem(package + General.Icon("optical"), i));
                    break;
                case 16:
                    foreach (string i in new string[] { "18.5\" Monitor", "21.5\" Monitor", "23\" Monitor" })
                        GV.Items.Add(CreateGridViewItem(package + General.Icon("monitor"), i));
                    break;
                case 17:
                    foreach (string i in new string[] { "Wired Mouse", "Wireless Mouse" })
                        GV.Items.Add(CreateGridViewItem(package + General.Icon("mouse"), i));
                    break;
                case 18:
                    foreach (string i in new string[] { "Wired KeyBoard", "Wireless KeyBoard" })
                        GV.Items.Add(CreateGridViewItem(package + General.Icon("keyboard"), i));
                    break;
                default:
                    break;
            }
            Grid grid = new Grid();
            grid.VerticalAlignment = VerticalAlignment.Top;
            grid.Children.Add(rec);
            grid.Children.Add(text);
            grid.Children.Add(GV);
            grid.Margin = new Thickness(10, ((Grid)container.Children[container.Children.Count - 1]).Height + ((Grid)container.Children[container.Children.Count - 1]).Margin.Top + 10, 20, 0);
            int rows = GV.Items.Count / Per_Row(container) + (GV.Items.Count % Per_Row(container) > 0 ? 1 : 0);
            grid.Height = rows < gridview_height.Length ? gridview_height[rows] : 84 + rows * 276;
            container.Children.Add(grid);
            container.UpdateLayout();
            container_scroll.ChangeView(null, grid.Margin.Top - 10, null);
        }

        public static int Per_Row(Grid container)
        {
            return Math.Max(1, (int)((container.ActualWidth - 60) / 272));
        }

        public static void Relayout(Grid container, double empty)
        {
            double top = 10;
            foreach (UIElement i in container.Children)
            {
                Grid grid = (Grid)i;
                if (grid.Children.Count > 2 && grid.Children[2] is GridView)
                {
                    int count = ((GridView)grid.Children[2]).Items.Count;
                    int rows = count / Per_Row(container) + (count % Per_Row(container) > 0 ? 1 : 0);
                    grid.Height = rows == 0 ? empty : 84 + rows * 276;
                }
                grid.Margin = new Thickness(grid.Margin.Left, top, grid.Margin.Right, grid.Margin.Bottom);
                top = top + (double.IsNaN(grid.Height) ? grid.ActualHeight : grid.Height) + 10;
            }
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

        static GridViewItem CreateGridViewItem(string image_source, string text)
        {
            return CreateGridViewItem(image_source, text, no_image);
        }

        public static Image CreateImage(string image_source, string other_image)
        {
            Image img = new Image();
            img.Source = new BitmapImage(new Uri(image_source));
            img.Stretch = Stretch.None;
            int tries = 0;
            img.ImageFailed += (sender, e) =>
            {
                tries++;
                if (tries == 1 && image_source.EndsWith(".png"))
                    img.Source = new BitmapImage(new Uri(image_source.Substring(0, image_source.Length - 4) + ".jpg"));
                else if (tries < 3 && !other_image.Equals(no_image))
                {
                    tries = 2;
                    img.Source = new BitmapImage(new Uri(other_image));
                }
                else if (tries < 4)
                {
                    tries = 3;
                    img.Source = new BitmapImage(new Uri(no_image));
                }
            };
            return img;
        }

        public static GridViewItem CreateGridViewItem(string image_source, string text, string other_image)
        {
            Image img = CreateImage(image_source, other_image);
            Rectangle rec = new Rectangle();
            rec.Height = 38;
            rec.VerticalAlignment = VerticalAlignment.Bottom;
            rec.Fill = new SolidColorBrush(Windows.UI.Color.FromArgb(153, 116, 116, 116));
            TextBlock title = new TextBlock();
            title.Text = text;
            title.Height = 36;
            title.VerticalAlignment = VerticalAlignment.Bottom;
            title.FontSize = 26;
            title.Foreground = new SolidColorBrush(Windows.UI.Color.FromArgb(255, 153, 255, 47));
            title.TextTrimming = TextTrimming.CharacterEllipsis;
            title.TextAlignment = TextAlignment.Center;
            Grid grid = new Grid();
            grid.Width = 256;
            grid.Height = 256;
            grid.Children.Add(img);
            grid.Children.Add(rec);
            grid.Children.Add(title);
            ToolTipService.SetToolTip(grid, text);
            GridViewItem GVI = new GridViewItem();
            GVI.Content = grid;
            GVI.Tag = text;
            GVI.Padding = new Thickness(5);
            return GVI;
        }

        public static async Task<string> Picture_Data(string path, string other)
        {
            List<string> paths = new List<string>();
            paths.Add(path);
            if (path.EndsWith(".png"))
                paths.Add(path.Substring(0, path.Length - 4) + ".jpg");
            paths.Add(other);
            foreach (string i in paths)
            {
                try
                {
                    StorageFile file = await StorageFile.GetFileFromApplicationUriAsync(new Uri(package + i));
                    IBuffer buffer = await FileIO.ReadBufferAsync(file);
                    return "data:image/" + General.bool2string(i.EndsWith(".png"), "png", "jpeg") + ";base64," + Convert.ToBase64String(buffer.ToArray());
                }
                catch (Exception)
                {
                }
            }
            return "";
        }

        private bool Section_Done(int section)
        {
            for (int j = section_level[section]; j < section_level[section + 1]; j++)
                if (your_pc[j].item == null)
                    return false;
            return true;
        }

        private void Update_Sections()
        {
            Image[] ticks = { tick0, tick1, tick2, tick3, tick4, tick5 };
            bool all = true;
            int current = -1;
            for (int i = 0; i < ticks.Length; i++)
            {
                bool done = Section_Done(i);
                ticks[i].Visibility = done ? Visibility.Visible : Visibility.Collapsed;
                if (!done && current == -1)
                    current = i;
                all = all && done;
            }
            appBar_save.IsEnabled = all;
            locked = true;
            sections.SelectedIndex = current == -1 ? ticks.Length : current;
            locked = false;
            if (all && !complete)
                Show_Summary();
            complete = all;
        }

        private void Sections_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (locked || container == null || container_scroll == null || summary_scroll == null || sections.SelectedIndex < 0)
                return;
            if (sections.SelectedIndex == 6)
            {
                Show_Summary();
                return;
            }
            Show_Section(sections.SelectedIndex);
        }

        private void Show_Section(int section)
        {
            summary_scroll.Visibility = Visibility.Collapsed;
            container_scroll.Visibility = Visibility.Visible;
            locked = true;
            sections.SelectedIndex = section;
            locked = false;
            container.UpdateLayout();
            for (int i = 0; i < container.Children.Count; i++)
            {
                int level;
                int.TryParse(((GridView)((Grid)container.Children[i]).Children[2]).Tag.ToString(), out level);
                if (level >= section_level[section])
                {
                    container_scroll.ChangeView(null, ((Grid)container.Children[i]).Margin.Top - 10, null);
                    return;
                }
            }
            container_scroll.ChangeView(null, ((Grid)container.Children[container.Children.Count - 1]).Margin.Top - 10, null);
        }

        private List<string[]> Summary()
        {
            int vga = your_pc[7].item != null && !your_pc[7].item.Equals("Built-in GPU") ? your_pc[9].index : -1;
            return General.Summary(your_pc[2].index, your_pc[4].index, your_pc[5].item, your_pc[6].item, your_pc[7].item, vga, your_pc[12].index, your_pc[13].item, your_pc[14].item, your_pc[15].item, your_pc[16].item, your_pc[17].item, your_pc[18].item);
        }

        private List<string[]> Compatibility()
        {
            int vga = your_pc[7].item != null && !your_pc[7].item.Equals("Built-in GPU") ? your_pc[9].index : -1;
            return General.Compatibility(your_pc[2].index, your_pc[4].index, your_pc[6].item, your_pc[7].item, vga, your_pc[12].index);
        }

        private void Show_Summary()
        {
            summary.Children.Clear();
            TextBlock title = new TextBlock();
            title.Text = General.bool2string(complete || Section_Done(0) && Section_Done(1) && Section_Done(2) && Section_Done(3) && Section_Done(4) && Section_Done(5), "Your desktop", "Your desktop (not complete yet)");
            title.FontSize = 40;
            title.TextAlignment = TextAlignment.Center;
            title.Margin = new Thickness(0, 5, 0, 15);
            summary.Children.Add(title);
            foreach (string[] i in Summary())
                summary.Children.Add(Create_Part(i));
            TextBlock notes = new TextBlock();
            notes.Text = "Compatibility";
            notes.FontSize = 28;
            notes.Margin = new Thickness(0, 20, 0, 5);
            notes.Foreground = new SolidColorBrush(Windows.UI.Color.FromArgb(255, 0, 229, 255));
            summary.Children.Add(notes);
            foreach (string[] i in Compatibility())
            {
                Grid row = new Grid();
                row.Margin = new Thickness(0, 4, 0, 4);
                Image tick = new Image();
                tick.Source = new BitmapImage(new Uri(package + "Assets/Appearance/tick.png"));
                tick.Stretch = Stretch.None;
                tick.HorizontalAlignment = HorizontalAlignment.Left;
                tick.Opacity = i[0].Equals("1") ? 1 : 0.2;
                TextBlock text = new TextBlock();
                text.Text = General.bool2string(i[0].Equals("1"), "", "Check: ") + i[1];
                text.FontSize = 20;
                text.TextWrapping = TextWrapping.Wrap;
                text.VerticalAlignment = VerticalAlignment.Center;
                text.Margin = new Thickness(60, 0, 0, 0);
                text.Foreground = new SolidColorBrush(i[0].Equals("1") ? Windows.UI.Colors.White : Windows.UI.Colors.Orange);
                row.Children.Add(tick);
                row.Children.Add(text);
                summary.Children.Add(row);
            }
            StackPanel details = new StackPanel();
            details.Visibility = Visibility.Collapsed;
            details.Margin = new Thickness(0, 10, 0, 0);
            Button details_button = new Button();
            details_button.Content = "Full details";
            details_button.FontSize = 22;
            details_button.Margin = new Thickness(0, 20, 0, 0);
            details_button.Click += (sender, e) =>
            {
                if (details.Children.Count == 0)
                    Fill_Details(details);
                details.Visibility = details.Visibility == Visibility.Visible ? Visibility.Collapsed : Visibility.Visible;
                details_button.Content = details.Visibility == Visibility.Visible ? "Hide details" : "Full details";
            };
            summary.Children.Add(details_button);
            summary.Children.Add(details);
            Button save = new Button();
            save.Content = "Save as HTML";
            save.FontSize = 22;
            save.HorizontalAlignment = HorizontalAlignment.Right;
            save.Margin = new Thickness(0, 20, 0, 0);
            save.IsEnabled = appBar_save.IsEnabled;
            save.Click += Save_Click;
            summary.Children.Add(save);
            container_scroll.Visibility = Visibility.Collapsed;
            summary_scroll.Visibility = Visibility.Visible;
            summary_scroll.ChangeView(null, 0, null);
            locked = true;
            sections.SelectedIndex = 6;
            locked = false;
        }

        private void Fill_Details(StackPanel details)
        {
            int mb = your_pc[2].index;
            int cpu = your_pc[4].index;
            if (mb >= 0)
                details.Children.Add(Specification.CreateSpecification(General.MotherBoards[mb]));
            if (cpu >= 0)
                details.Children.Add(Specification.CreateSpecification(General.Processors[cpu]));
            if (your_pc[7].item != null && your_pc[7].item.Equals("Built-in GPU") && mb >= 0 && cpu >= 0)
            {
                TextBlock title = new TextBlock();
                title.Text = "Graphic Card:";
                title.FontSize = 28;
                title.Margin = new Thickness(0, 15, 0, 5);
                title.Foreground = new SolidColorBrush(Windows.UI.Color.FromArgb(255, 0, 229, 255));
                details.Children.Add(title);
                details.Children.Add(Specification.CreateRow("Name", General.Built_in_GPU(mb, cpu), Windows.UI.Colors.White));
                if ((General.Processors[cpu].Kind + " " + General.Processors[cpu].Name).Contains("APU"))
                    details.Children.Add(Specification.CreateRow("DirectX Version", "11", Windows.UI.Colors.White));
            }
            else if (your_pc[9].index >= 0)
                details.Children.Add(Specification.CreateSpecification(General.Graphigc_Cards[your_pc[9].index]));
            if (your_pc[6].item != null)
            {
                RAM memory = new RAM();
                memory.Company = your_pc[5].item;
                memory.Name = memory.Size = your_pc[6].item;
                details.Children.Add(Specification.CreateSpecification(memory));
                details.Children.Add(Specification.CreateRow("", "Generic option, not from the data", Windows.UI.Colors.Orange));
            }
            if (your_pc[12].index >= 0)
                details.Children.Add(Specification.CreateSpecification(General.Hard_Disks[your_pc[12].index]));
        }

        private Grid Create_Part(string[] part)
        {
            Grid grid = new Grid();
            grid.Margin = new Thickness(0, 5, 0, 5);
            grid.Height = 140;
            Rectangle rec = new Rectangle();
            rec.StrokeThickness = 1;
            rec.Stroke = new SolidColorBrush(Windows.UI.Colors.LightCyan);
            Image img = CreateImage(package + part[4], package + part[5]);
            img.Width = 120;
            img.Height = 120;
            img.Stretch = Stretch.Uniform;
            img.HorizontalAlignment = HorizontalAlignment.Left;
            img.Margin = new Thickness(10, 10, 0, 10);
            StackPanel text = new StackPanel();
            text.Margin = new Thickness(145, 10, 150, 10);
            TextBlock component = new TextBlock();
            component.Text = part[1];
            component.FontSize = 20;
            component.Foreground = new SolidColorBrush(Windows.UI.Color.FromArgb(255, 0, 229, 255));
            TextBlock name = new TextBlock();
            name.Text = part[2];
            name.FontSize = 26;
            name.TextTrimming = TextTrimming.CharacterEllipsis;
            name.Foreground = new SolidColorBrush(Windows.UI.Color.FromArgb(255, 153, 255, 47));
            TextBlock specs = new TextBlock();
            specs.Text = part[3];
            specs.FontSize = 18;
            specs.TextWrapping = TextWrapping.Wrap;
            specs.MaxHeight = 50;
            specs.Foreground = new SolidColorBrush(part[6].Equals("1") ? Windows.UI.Colors.Orange : Windows.UI.Colors.LightGray);
            text.Children.Add(component);
            text.Children.Add(name);
            text.Children.Add(specs);
            Button change = new Button();
            change.Content = "Change";
            change.FontSize = 18;
            change.HorizontalAlignment = HorizontalAlignment.Right;
            change.VerticalAlignment = VerticalAlignment.Center;
            change.Margin = new Thickness(0, 0, 15, 0);
            int section;
            int.TryParse(part[0], out section);
            change.Tag = section;
            change.Click += Change_Click;
            grid.Children.Add(rec);
            grid.Children.Add(img);
            grid.Children.Add(text);
            grid.Children.Add(change);
            return grid;
        }

        private void Change_Click(object sender, RoutedEventArgs e)
        {
            Show_Section((int)((Button)sender).Tag);
        }

        private void Review_Click(object sender, RoutedEventArgs e)
        {
            Show_Summary();
            AppBar1.IsOpen = false;
        }

        private void New_Click(object sender, RoutedEventArgs e)
        {
            platform.SelectedItem = null;
            for (int i = container.Children.Count - 1; i > 0; i--)
                container.Children.RemoveAt(i);
            for (int i = 0; i < your_pc.Count; i++)
            {
                your_pc[i].index = -1;
                your_pc[i].item = null;
            }
            complete = false;
            Update_Sections();
            Show_Section(0);
            container_scroll.ChangeView(null, 0, null);
            AppBar1.IsOpen = false;
        }

        private async void Save_Click(object sender, RoutedEventArgs e)
        {
            AppBar1.IsOpen = false;
            if (!appBar_save.IsEnabled)
                return;
            PC computer = new PC();
            computer.MB = General.MotherBoards[your_pc[2].index];
            computer.Processor = General.Processors[your_pc[4].index];
            computer.HD = General.Hard_Disks[your_pc[12].index];
            computer.RAM = new RAM();
            computer.RAM.Company = your_pc[5].item;
            computer.RAM.Name = computer.RAM.Size = your_pc[6].item;
            computer.Extenal_Graphic = !your_pc[7].item.Equals("Built-in GPU");
            if (computer.Extenal_Graphic)
                computer.VGA = General.Graphigc_Cards[your_pc[9].index];
            List<string[]> parts = new List<string[]>();
            foreach (string[] i in Summary())
                parts.Add(new string[] { i[1], i[2] + "<br/>" + i[3], await Picture_Data(i[4], i[5]) });
            General.SaveFile(computer.ToHTML(parts, Compatibility()), "Desktop");
        }

        private void Random_Click(object sender, RoutedEventArgs e)
        {
            New_Click(sender, e);
            GridView GV = platform;
            while (GV != null && GV.Items.Count > 0)
            {
                GV.SelectedIndex = General.GetRandomInt(GV.Items.Count);
                GridView last = (GridView)((Grid)container.Children[container.Children.Count - 1]).Children[2];
                GV = last.SelectedItem == null ? last : null;
            }
            AppBar1.IsOpen = false;
        }

        private void pageRoot_Loaded(object sender, RoutedEventArgs e)
        {
            if (your_pc.Count > 0)
                return;
            MB_companies.Add("ASRock");
            MB_companies.Add("ASUS");
            MB_companies.Add("Foxconn");
            MB_companies.Add("GigaByte");
            MB_companies.Add("MSI");
            for (int i = 0; i < 19; i++)
            {
                your_pc.Add(new PCItem());
                your_pc[i].index = -1;
            }
            ImageSource j = intel.Source;
        }
    }
}
