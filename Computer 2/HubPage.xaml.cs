using Computer_2.Common;
using Computer_2.Data;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
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

// The Hub Page item template is documented at http://go.microsoft.com/fwlink/?LinkID=321224

namespace Computer_2
{
    /// <summary>
    /// A page that displays a grouped collection of items.
    /// </summary>
    public sealed partial class HubPage : Page
    {
        public static List<Processor> Processors = new List<Processor>();
        public static List<MB> MotherBoards = new List<MB>();
        public static List<VGA> Graphigc_Cards = new List<VGA>();
        public static List<HD> Hard_Disks = new List<HD>();
        public static List<Laptop> Laptops = new List<Laptop>();
        public static List<TabletPC> Tablet_PCs = new List<TabletPC>();
        string current_path = Windows.Storage.ApplicationData.Current.RoamingFolder.Path + @"\Computer";

        private NavigationHelper navigationHelper;
        private ObservableDictionary defaultViewModel = new ObservableDictionary();

        /// <summary>
        /// NavigationHelper is used on each page to aid in navigation and 
        /// process lifetime management
        /// </summary>
        public NavigationHelper NavigationHelper
        {
            get { return this.navigationHelper; }
        }

        /// <summary>
        /// This can be changed to a strongly typed view model.
        /// </summary>
        public ObservableDictionary DefaultViewModel
        {
            get { return this.defaultViewModel; }
        }

        public HubPage()
        {
            this.InitializeComponent();
            this.navigationHelper = new NavigationHelper(this);
            this.navigationHelper.LoadState += navigationHelper_LoadState;
        }

        /// <summary>
        /// Populates the page with content passed during navigation.  Any saved state is also
        /// provided when recreating a page from a prior session.
        /// </summary>
        /// <param name="sender">
        /// The source of the event; typically <see cref="NavigationHelper"/>
        /// </param>
        /// <param name="e">Event data that provides both the navigation parameter passed to
        /// <see cref="Frame.Navigate(Type, Object)"/> when this page was initially requested and
        /// a dictionary of state preserved by this page during an earlier
        /// session.  The state will be null the first time a page is visited.</param>
        private async void navigationHelper_LoadState(object sender, LoadStateEventArgs e)
        {
            // TODO: Create an appropriate data model for your problem domain to replace the sample data
            var sampleDataGroup = await SampleDataSource.GetGroupAsync("Group-1");
            this.DefaultViewModel["Section3Items"] = sampleDataGroup;
        }

        /// <summary>
        /// Invoked when a HubSection header is clicked.
        /// </summary>
        /// <param name="sender">The Hub that contains the HubSection whose header was clicked.</param>
        /// <param name="e">Event data that describes how the click was initiated.</param>
        void Hub_SectionHeaderClick(object sender, HubSectionHeaderClickEventArgs e)
        {
            HubSection section = e.Section;
            var group = section.DataContext;
            this.Frame.Navigate(typeof(ItemPage), ((SampleDataGroup)group).UniqueId);
        }

        /// <summary>
        /// Invoked when an item within a section is clicked.
        /// </summary>
        /// <param name="sender">The GridView or ListView
        /// displaying the item clicked.</param>
        /// <param name="e">Event data that describes the item clicked.</param>
        void ItemView_ItemClick(object sender, ItemClickEventArgs e)
        {
            // Navigate to the appropriate destination page, configuring the new page
            // by passing required information as a navigation parameter
            var itemId = ((SampleDataItem)e.ClickedItem).UniqueId;
            this.Frame.Navigate(typeof(ItemPage), itemId);
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

        private void Desktop_Click(object sender, ItemClickEventArgs e)
        {
            Frame.Navigate(typeof(Desktop), null);
        }

        private async void Laptop()
        {
            List<Windows.Storage.StorageFile> Files = new List<Windows.Storage.StorageFile>();
            StorageFolder Directory = await StorageFolder.GetFolderFromPathAsync(current_path + @"\Laptop");
            IReadOnlyList<StorageFolder> Directories = await Directory.GetFoldersAsync();
            foreach (StorageFolder i in Directories)
            {
                IReadOnlyList<StorageFile> Temp = await i.GetFilesAsync();
                foreach (StorageFile j in Temp)
                    Files.Add(j);
            }
            IList<string> current_file = new List<string>();
            for (int i = 0; i < Files.Count; i++)
            {
                current_file = await Windows.Storage.PathIO.ReadLinesAsync(Files[i].Path);
                Laptop temp = new Laptop();
                temp.Kind = "Laptop";
                temp.Company = current_file[1];
                temp.Name = current_file[3];
                temp.Processor = current_file[5];
                temp.Processor_Name = current_file[6];
                temp.Processor_Speed = current_file[8];
                temp.Processor_Max_Speed = current_file[10];
                temp.Cashe_Memory = current_file[12];
                temp.Chipset = current_file[14];
                temp.RAM = current_file[16];
                temp.Hard_Disk = current_file[18];
                temp.Hard_Disk_Rotation = current_file[20];
                temp.Optical_Driver = current_file[22];
                temp.Graphic_Card = current_file[24];
                temp.Graphic_Card_Size = current_file[26];
                temp.Graphic_Card_Max_Size = current_file[28];
                temp.Camera = current_file[30];
                temp.Screen_Size = current_file[32];
                temp.Screen_Resolution = current_file[34];
                int j;
                for (j = 0; current_file[j].Contains("لمس"); j++)
                    temp.Ports = current_file[j];
                temp.Touch_Screen = !(current_file[j + 1].Equals("-"));
                temp.OS = current_file[j + 3];
                temp.Battary = current_file[j + 5] + " " + current_file[j + 6];
                temp.Dimensions = current_file[j + 8];
                temp.Weight = current_file[j + 10];
                temp.Category = current_file[j + 12];
                //temp.Price = float.Parse(current_file[j + 14]);
                current_file.Clear();
                Laptops.Add(temp);
            }
        }

        private async void Intel()
        {
            List<Windows.Storage.StorageFile> Files = new List<Windows.Storage.StorageFile>();
            StorageFolder Directory = await StorageFolder.GetFolderFromPathAsync(current_path + @"\Processors\Intel\");
            IReadOnlyList<StorageFolder> Directories = await Directory.GetFoldersAsync();
            foreach (StorageFolder i in Directories)
            {
                IReadOnlyList<StorageFile> Temp = await i.GetFilesAsync();
                foreach (StorageFile j in Temp)
                    Files.Add(j);
            }
            IList<string> current_file = new List<string>();
            for (int i = 0; i < Files.Count; i++)
            {
                current_file = await Windows.Storage.PathIO.ReadLinesAsync(Files[i].Path);
                Processor temp = new Processor();
                temp.Company = "Intel";
                temp.Kind = current_file[3];
                temp.Name = current_file[5];
                float x;
                float.TryParse(current_file[7], out x);
                temp.Cores = x;
                float.TryParse(current_file[9], out x);
                temp.Thread = x;
                temp.Speed = current_file[11];
                temp.Max_Speed = current_file[13];
                temp.L1 = current_file[15];
                temp.DMI = current_file[17];
                temp.Instructions = current_file[21];
                temp.Technique = current_file[23];
                temp.Power = current_file[25];
                temp.Max_Memory = current_file[27];
                float.TryParse(current_file[29], out x);
                temp.Memory_Channels = x;
                temp.Maxtmp = current_file[31];
                if (current_file[33].Contains("Graphics"))
                {
                    temp.Internal_Graphic = true;
                    temp.Graphic_Processor = current_file[33];
                }
                temp.Socket = current_file[35];
                temp.Virtualization_Technology = current_file[37].Contains("VT-x");
                temp.Advanced_Technologies = "64-bit Instruction Set" + Functions.bool2string(current_file[37].Contains("-"), "", " , " + current_file[37]);
                Processors.Add(temp);
                current_file.Clear();
            }
            AMD();
        }

        private async void AMD()
        {
            List<Windows.Storage.StorageFile> Files = new List<Windows.Storage.StorageFile>();
            StorageFolder Directory = await StorageFolder.GetFolderFromPathAsync(current_path + @"\Processors\AMD");
            IReadOnlyList<StorageFolder> Directories = await Directory.GetFoldersAsync();
            foreach (StorageFolder i in Directories)
            {
                IReadOnlyList<StorageFile> Temp = await i.GetFilesAsync();
                foreach (StorageFile j in Temp)
                    Files.Add(j);
            }
            IList<string> current_file = new List<string>();
            for (int i = 0; i < Files.Count; i++)
            {
                current_file = await Windows.Storage.PathIO.ReadLinesAsync(Files[i].Path);
                Processor temp = new Processor();
                temp.Company = "AMD";
                temp.Kind = current_file[3];
                temp.Name = current_file[5];
                float x;
                float.TryParse(current_file[7], out x);
                temp.Cores = x;
                float.TryParse(current_file[9], out x);
                temp.Thread = x;
                temp.Speed = current_file[11];
                temp.Max_Speed = current_file[13];
                temp.L1 = current_file[15];
                temp.L2 = current_file[17];
                temp.L3 = current_file[19];
                temp.Technique = current_file[23];
                temp.Power = current_file[25];
                temp.Maxtmp = current_file[27];
                temp.Virtualization_Technology = current_file[29].Contains("Virtualization");
                temp.Advanced_Technologies = "64-bit Instruction Set" + Functions.bool2string(current_file[29].Contains("-"), "", " , " + current_file[29]);
                temp.Socket = current_file[31];
                Processors.Add(temp);
                current_file.Clear();
            }
            APU();
        }

        private async void APU()
        {
            List<Windows.Storage.StorageFile> Files = new List<Windows.Storage.StorageFile>();
            StorageFolder Directory = await StorageFolder.GetFolderFromPathAsync(current_path + @"\Processors\AMD APU");
            IReadOnlyList<StorageFile> Temp = await Directory.GetFilesAsync();
            foreach (StorageFile j in Temp)
                Files.Add(j);
            IList<string> current_file = new List<string>();
            for (int i = 0; i < Files.Count; i++)
            {
                current_file = await Windows.Storage.PathIO.ReadLinesAsync(Files[i].Path);
                Processor temp = new Processor();
                temp.Company = "AMD";
                temp.Kind = "APU";
                temp.Name = current_file[5];
                float x;
                float.TryParse(current_file[7], out x);
                temp.Cores = x;
                float.TryParse(current_file[9], out x);
                temp.Thread = x;
                if (current_file[11].Contains("HD"))
                {
                    temp.Internal_Graphic = true;
                    temp.Graphic_Processor = current_file[11];
                }
                temp.Speed = current_file[15];
                temp.Max_Speed = current_file[17];
                temp.L1 = current_file[19];
                temp.L2 = current_file[21];
                temp.L3 = current_file[23];
                temp.Technique = current_file[27];
                temp.Power = current_file[29];
                temp.Maxtmp = current_file[31];
                temp.Virtualization_Technology = current_file[33].Contains("Virtualization");
                temp.Advanced_Technologies = "64-bit Instruction Set" + Functions.bool2string(current_file[33].Contains("-"), "", " , " + current_file[33]);
                temp.Socket = current_file[35];
                current_file.Clear();
                Processors.Add(temp);
            }
            MB();
        }

        private async void MB()
        {
            List<Windows.Storage.StorageFile> Files = new List<Windows.Storage.StorageFile>();
            List<StorageFolder> Dictionary = new List<StorageFolder>();
            Dictionary.Add(await StorageFolder.GetFolderFromPathAsync(current_path + @"\Motherboard\AMD\AM3"));
            Dictionary.Add(await StorageFolder.GetFolderFromPathAsync(current_path + @"\Motherboard\AMD\AM3+"));
            Dictionary.Add(await StorageFolder.GetFolderFromPathAsync(current_path + @"\Motherboard\AMD\FM1"));
            Dictionary.Add(await StorageFolder.GetFolderFromPathAsync(current_path + @"\Motherboard\Intel\775"));
            Dictionary.Add(await StorageFolder.GetFolderFromPathAsync(current_path + @"\Motherboard\Intel\1155"));
            Dictionary.Add(await StorageFolder.GetFolderFromPathAsync(current_path + @"\Motherboard\Intel\1156"));
            Dictionary.Add(await StorageFolder.GetFolderFromPathAsync(current_path + @"\Motherboard\Intel\1366"));
            Dictionary.Add(await StorageFolder.GetFolderFromPathAsync(current_path + @"\Motherboard\Intel\2011"));
            List<StorageFolder> Directories = new List<StorageFolder>();
            foreach (StorageFolder i in Dictionary)
            {
                IReadOnlyList<StorageFolder> Directories_Temp = await i.GetFoldersAsync();
                foreach (StorageFolder j in Directories_Temp)
                    Directories.Add(j);
            }
            foreach (StorageFolder i in Directories)
            {
                IReadOnlyList<StorageFile> Temp = await i.GetFilesAsync();
                foreach (StorageFile j in Temp)
                    if (j.Name.ToLower().Contains(".txt"))
                        Files.Add(j);
            }
            IList<string> current_file = new List<string>();
            for (int k = 0; k < Files.Count; k++)
            {
                current_file = await Windows.Storage.PathIO.ReadLinesAsync(Files[k].Path);
                MB temp = new MB();
                temp.Company = current_file[3].ToString();
                temp.Name = current_file[5];
                temp.Socket = current_file[7];
                temp.Chipset = current_file[9];
                if (Files[k].Path.Contains("Intel"))
                    temp.Kind = "Intel";
                else
                    temp.Kind = "AMD";
                temp.Max_Mem = current_file[11];
                temp.Graphic_Processor = current_file[13];
                temp.Lan = current_file[15];
                int j = 17;
                while (!current_file[j].Contains("المنافذ"))
                { temp.Slot = temp.Slot + "<br/>" + current_file[j]; ++j; }
                if (temp.Slot.Contains("e 2.0"))
                    temp.PCIe += 2;
                if (temp.Slot.Contains("e 3.0"))
                    temp.PCIe += 3;
                if (temp.Slot.Contains("SATA 3Gb"))
                    temp.Sata += 2;
                if (temp.Slot.Contains("SATA 6Gb"))
                    temp.Sata += 3;
                if (temp.Max_Mem.Contains("DDR2"))
                    temp.DDR += 2;
                if (temp.Max_Mem.Contains("DDR3"))
                    temp.DDR += 3;
                ++j;
                while (!current_file[j].Contains("Bluetooth"))
                { temp.Port = temp.Port + "<br/>" + current_file[j]; ++j; }
                temp.Internal_Graphic = (!temp.Graphic_Processor.Contains("-")) || temp.Port.Contains("HDMI") || temp.Port.Contains("DVI") || temp.Port.Contains("Sub") || temp.Port.Contains("VGA");
                temp.Bluetooth = current_file[++j];
                temp.WLAN = current_file[j + 2];
                temp.Advanced_Technologies = current_file[j + 4];
                current_file.Clear();
                if (temp.Advanced_Technologies.Contains("Quad") || temp.Advanced_Technologies.Contains("4-Way"))
                    temp.MultiGPU = temp.MultiGPU + 4 * 100;
                if (temp.Advanced_Technologies.Contains("3-Way"))
                    temp.MultiGPU = temp.MultiGPU + 3 * 10;
                if (temp.Advanced_Technologies.Contains("2-Way"))
                    temp.MultiGPU = temp.MultiGPU + 2;
                if ((temp.Advanced_Technologies.Contains("SLI") || temp.Advanced_Technologies.Contains("CrossFire")) && temp.MultiGPU == 0)
                    temp.MultiGPU = -1;
                MotherBoards.Add(temp);
                StorageFolder b = await StorageFolder.GetFolderFromPathAsync(current_path + @"\Motherboard\Intel\775");
                try
                {
                    //temp.Image = new ImageSource();
                }
                catch { }
            }
            GPU();
        }

        private async void GPU()
        {
            List<Windows.Storage.StorageFile> Files = new List<Windows.Storage.StorageFile>();
            List<StorageFolder> Dictionary = new List<StorageFolder>();
            Dictionary.Add(await StorageFolder.GetFolderFromPathAsync(current_path + @"\Graphic Card\AMD"));
            Dictionary.Add(await StorageFolder.GetFolderFromPathAsync(current_path + @"\Graphic Card\Nvidia"));
            List<StorageFolder> Directories = new List<StorageFolder>();
            foreach (StorageFolder i in Dictionary)
            {
                IReadOnlyList<StorageFolder> Directories_Temp = await i.GetFoldersAsync();
                foreach (StorageFolder j in Directories_Temp)
                    Directories.Add(j);
            }
            foreach (StorageFolder i in Directories)
            {
                IReadOnlyList<StorageFile> Temp = await i.GetFilesAsync();
                foreach (StorageFile j in Temp)
                    Files.Add(j);
            }
            IList<string> current_file = new List<string>();
            for (int i = 0; i < Files.Count; i++)
            {
                current_file = await Windows.Storage.PathIO.ReadLinesAsync(Files[i].Path);
                VGA temp = new VGA();
                if (Files[i].Path.Contains("Nvidia"))
                    temp.Kind = "Nvidia";
                else
                    temp.Kind = "AMD";
                temp.Company = current_file[2];
                temp.Name = current_file[4];
                temp.GPU = current_file[6];
                temp.Socket = current_file[8];
                temp.Mem_Size = current_file[10];
                temp.DDR_Type = current_file[12];
                temp.Mem_Channel = current_file[14];
                temp.Processor_Speed = current_file[16];
                temp.Mem_Speed = current_file[18];
                temp.Max_Resolution = current_file[20];
                temp.DirectX_Version = current_file[22];
                temp.OpenGL_Version = current_file[24];
                temp.Pixel_Shader_Version = current_file[26];
                temp.Cores = current_file[28];
                temp.Advanced_Technologies = current_file[30];
                temp.Port = current_file[32].Replace(", ", "<br/>");
                if (temp.Advanced_Technologies.Contains("Quad") || temp.Advanced_Technologies.Contains("4-Way"))
                    temp.MultiGPU = temp.MultiGPU + 4 * 100;
                if (temp.Advanced_Technologies.Contains("3-Way"))
                    temp.MultiGPU = temp.MultiGPU + 3 * 10;
                if (temp.Advanced_Technologies.Contains("2-Way"))
                    temp.MultiGPU = temp.MultiGPU + 2;
                if ((temp.Advanced_Technologies.Contains("SLI") || temp.Advanced_Technologies.Contains("CrossFire")) && temp.MultiGPU == 0)
                    temp.MultiGPU = -1;
                Graphigc_Cards.Add(temp);
            }
            HD();
        }

        private async void HD()
        {
            IList<Windows.Storage.StorageFile> Files = new List<Windows.Storage.StorageFile>();
            List<StorageFolder> Dictionary = new List<StorageFolder>();
            Dictionary.Add(await StorageFolder.GetFolderFromPathAsync(current_path + @"\Hard Disk\SSD\Sata 2"));
            Dictionary.Add(await StorageFolder.GetFolderFromPathAsync(current_path + @"\Hard Disk\HDD\Sata 2"));
            Dictionary.Add(await StorageFolder.GetFolderFromPathAsync(current_path + @"\Hard Disk\HDD\Sata 3"));
            Dictionary.Add(await StorageFolder.GetFolderFromPathAsync(current_path + @"\Hard Disk\SSD\Sata 3"));
            List<StorageFolder> Directories = new List<StorageFolder>();
            foreach (StorageFolder i in Dictionary)
            {
                IReadOnlyList<StorageFolder> Directories_Temp = await i.GetFoldersAsync();
                foreach (StorageFolder j in Directories_Temp)
                    Directories.Add(j);
            }
            foreach (StorageFolder i in Directories)
            {
                IReadOnlyList<StorageFile> Temp = await i.GetFilesAsync();
                foreach (StorageFile j in Temp)
                    Files.Add(j);
            }
            IList<string> current_file = new List<string>();
            for (int i = 0; i < Files.Count; i++)
            {
                current_file = await Windows.Storage.PathIO.ReadLinesAsync(Files[i].Path);
                HD temp = new HD();
                if (Files[i].Path.Contains("SSD"))
                    temp.Kind = "SSD";
                else
                    temp.Kind = "HDD";
                temp.Name = string.Concat(current_file[0], current_file[1]);
                temp.Company = current_file[4];
                temp.Code = current_file[6];
                temp.Capacity = current_file[8];
                int count = 8;
                if (temp.Kind.Equals("SSD"))
                {
                    temp.Read_Speed = current_file[10];
                    temp.Write_Speed = current_file[12];
                    temp.Max_Speed = current_file[14];
                    temp.Weight = current_file[16];
                    temp.Power = current_file[18];
                    count = 20;
                }
                else
                {
                    temp.Rotation_Speed = current_file[10];
                    temp.Buffer = current_file[12];
                    temp.Max_Speed = current_file[14];
                    count = 16;
                }
                temp.Socket = current_file[count];
                current_file.Clear();
                Hard_Disks.Add(temp);
            }
        }

        private async void Tablet_PC()
        {
            List<Windows.Storage.StorageFile> Files = new List<Windows.Storage.StorageFile>();
            StorageFolder Directory = await StorageFolder.GetFolderFromPathAsync(current_path + @"\Tablet");
            IReadOnlyList<StorageFolder> Directories = await Directory.GetFoldersAsync();
            foreach (StorageFolder i in Directories)
            {
                IReadOnlyList<StorageFile> Temp = await i.GetFilesAsync();
                foreach (StorageFile j in Temp)
                    Files.Add(j);
            }
            IList<string> current_file = new List<string>();
            for (int i = 0; i < Files.Count; i++)
            {
                TabletPC temp = new TabletPC();
                current_file = await Windows.Storage.PathIO.ReadLinesAsync(Files[i].Path);
                temp.Company = current_file[0];
                temp.Kind = "Tablet";
                temp.Company = Functions.First_Word(temp.Company);
                temp.Name = current_file[3];
                temp.Size = current_file[5];
                temp.Weight = current_file[7];
                temp.Screen_Size = current_file[9];
                temp.Screen_Resolution = current_file[11];
                temp.OS = current_file[13];
                temp.Processor_Name = current_file[15];
                temp.Processor_Speed = current_file[17];
                temp.Cores = current_file[19];
                temp.Memory_Size = current_file[21];
                temp.Internal_Memory = current_file[23];
                temp.Max_Memory_Card_Size = current_file[25];
                if (temp.Max_Memory_Card_Size.Contains("-"))
                    temp.Memory_Card = false;
                else
                    temp.Memory_Card = true;
                temp.Camera = current_file[27];
                if (current_file[29].Contains("-"))
                    temp.Flash = false;
                else
                    temp.Flash = true;
                temp.Video_Resolution = current_file[31];
                temp.Web_Cam = current_file[33];
                temp.G = current_file[35];
                temp.Bluetooth = current_file[37];
                temp.Price = 10;
                current_file.Clear();
                Tablet_PCs.Add(temp);
            }
        }

        private void pageRoot_Loaded(object sender, RoutedEventArgs e)
        {
            if (Processors.Count == 0)
            {
                Laptop();
                Intel();
                Tablet_PC();
            }
        }
    }
}//514
