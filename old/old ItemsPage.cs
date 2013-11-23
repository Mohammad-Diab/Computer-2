Ausing Computer_2.Data;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Graphics.Display;
using Windows.Storage;
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
    public sealed partial class ItemsPage : Computer_2.Common.LayoutAwarePage
    {
        public ItemsPage()
        {
            this.InitializeComponent();
        }

        public static List<Processor> Processors = new List<Processor>();
        public static List<MB> MotherBoards = new List<MB>();
        public static List<VGA> Graphigc_Cards = new List<VGA>();
        public static List<HD> Hard_Disks = new List<HD>();
        public static List<Laptop> Laptops = new List<Laptop>();
        public static List<TabletPC> Tablet_PCs = new List<TabletPC>();
        string current_path = Windows.Storage.ApplicationData.Current.RoamingFolder.Path + @"\Computer";
        protected override void LoadState(Object navigationParameter, Dictionary<String, Object> pageState)
        {
            var sampleDataGroups = SampleDataSource.GetGroups((String)navigationParameter);
            this.DefaultViewModel["Items"] = sampleDataGroups;
        }

        void ItemView_ItemClick(object sender, ItemClickEventArgs e)
        {
            var groupId = ((SampleDataGroup)e.ClickedItem).UniqueId;
            switch (groupId)
            {
                case "Group-1":
                    this.Frame.Navigate(typeof(desktop));
                    break;
                case "Group-2":
                    this.Frame.Navigate(typeof(laptop));
                    break;
                case "Group-3":
                    this.Frame.Navigate(typeof(Tablet));
                    break;
                case "Group-4":
                    this.Frame.Navigate(typeof(SplitPage), groupId);
                    break;
                case "Group-6":
                    this.Frame.Navigate(typeof(Mobile));
                    break;
                default:
                    this.Frame.Navigate(typeof(SplitPage), groupId);
                    break;
            }
        }

        private void pageRoot_Loaded(object sender, RoutedEventArgs e)
        {
            if (Processors.Count == 0)
            {
                itemGridView.IsEnabled = false;
                itemListView.IsEnabled = false;
                Laptop();
                Intel();
                Tablet_PC();
            }
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
                temp.Processor = Functions.AddToList(current_file[5]);
                temp.Processor_Name = Functions.AddToList(current_file[6]);
                temp.Processor_Speed = Functions.AddToList(current_file[8]);
                temp.Processor_Max_Speed = Functions.AddToList(current_file[10]);
                temp.Cashe_Memory = Functions.AddToList(current_file[12]);
                temp.Chipset = Functions.AddToList(current_file[14]);
                temp.RAM = Functions.AddToList(current_file[16]);
                temp.Hard_Disk = Functions.AddToList(current_file[18]);
                temp.Hard_Disk_Rotation = Functions.AddToList(current_file[20]);
                temp.Optical_Driver = current_file[22];
                temp.Graphic_Card = Functions.AddToList(current_file[24]);
                temp.Graphic_Card_Size = Functions.AddToList(current_file[26]);
                temp.Graphic_Card_Max_Size = Functions.AddToList(current_file[28]);
                temp.Camera = current_file[30];
                temp.Screen_Size = current_file[32];
                temp.Screen_Resolution = current_file[34];
                int j;
                for (j = 0; current_file[j].Contains("لمس"); j++)
                    temp.Ports = current_file[j];
                temp.Touch_Screen = !(current_file[j+1].Equals("-"));
                temp.OS = current_file[j + 3];
                temp.Battary = current_file[j + 5] + " " + current_file[j + 6];
                temp.Dimensions = current_file[j + 8];
                temp.Weight = current_file[j + 10];
                temp.Category = current_file[j + 12];
                temp.Price = float.Parse(current_file[j + 14]);
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
                ItemsPage.Processors.Add(temp);
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
                ItemsPage.Processors.Add(temp);
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
                ItemsPage.Processors.Add(temp);
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
                ItemsPage.Graphigc_Cards.Add(temp);
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
                ItemsPage.Hard_Disks.Add(temp);
                itemGridView.IsEnabled = true;
                itemListView.IsEnabled = true;
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
    }
}//514