using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Runtime.Serialization;
namespace WpfApplication1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<Processor> Processors = new List<Processor>();
        List<MB> MotherBoards = new List<MB>();
        List<VGA> Graphigc_Cards = new List<VGA>();
        List<HD> Hard_Disks = new List<HD>();
        List<Tablet> Tablet_PCs = new List<Tablet>();
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Intel(object sender, RoutedEventArgs e)
        {
            List<string> Files = new List<string>();
            System.IO.DirectoryInfo get_directory = new DirectoryInfo(@"C:\Users\User\Desktop\Computer\Processors\Intel");
            var directories = get_directory.GetDirectories();
            foreach (DirectoryInfo i in directories)
            {
                var tmp = i.GetFiles();
                foreach (var j in tmp)
                    Files.Add(j.FullName);
            }
            string Type = "Intel";    //AMD or Intel
            string Kind = "-";      //Category of Processor
            string Name = "-";
            float Cores = 0;
            float Thread = 0;
            string Speed = "-";
            string Max_Speed = "-";
            string L1 = "-";
            string L2 = "-";
            string L3 = "-";
            bool Bit_64 = false;
            string Technique = "-";
            string Power = "-";
            string Maxtmp = "-";
            bool AMD_Virtualization_Technology = false;
            bool Intel_Turbo_Boost_Technology = false;
            bool Intel_Turbo_Boost_Technology_2 = false;
            bool Intel_Hyper_Threading_Technology = false;
            bool VT_x = false;
            bool Enhanced_Intel_SpeedStep_Technology = false;
            bool Internal_Graphic = false;
            string Graphic_Processor = "-";
            string Socket = "-";
            float Price = 10;
            string DMI = "-";//سرعة الناقل
            string Instructions = "-";
            string Max_Memory = "-";
            float Memory_Channels = 0;
            bool Black_Edition = false;
            for (int i = 0; i < Files.Count; i++)
            {
                System.IO.StreamReader SR = new System.IO.StreamReader(Files[i]);
                SR.ReadLine();
                SR.ReadLine();
                SR.ReadLine();
                Name = SR.ReadLine();
                SR.ReadLine();
                Kind = SR.ReadLine();
                SR.ReadLine();
                float.TryParse(SR.ReadLine(), out Cores);
                SR.ReadLine();
                float.TryParse(SR.ReadLine(), out Thread);
                SR.ReadLine();
                Speed = SR.ReadLine();
                SR.ReadLine();
                Max_Speed = SR.ReadLine();
                SR.ReadLine();
                L1 = SR.ReadLine();
                SR.ReadLine();
                DMI = SR.ReadLine();
                SR.ReadLine();
                if (SR.ReadLine() == "64 bit")
                    Bit_64 = true;
                SR.ReadLine();
                Instructions = SR.ReadLine();
                SR.ReadLine();
                Technique = SR.ReadLine();
                SR.ReadLine();
                Power = SR.ReadLine();
                SR.ReadLine();
                Max_Memory = SR.ReadLine();
                SR.ReadLine();
                float.TryParse(SR.ReadLine(), out Memory_Channels);
                SR.ReadLine();
                Maxtmp = SR.ReadLine();
                SR.ReadLine();
                string tmp = SR.ReadLine();
                if (tmp.Contains("Graphics"))
                {
                    Internal_Graphic = true;
                    Graphic_Processor = tmp;
                }
                SR.ReadLine();
                Socket = SR.ReadLine();
                SR.ReadLine();
                tmp = SR.ReadToEnd();
                if (tmp.Contains("Intel® Turbo Boost Technology 2"))
                    Intel_Turbo_Boost_Technology_2 = true;
                if (tmp.Contains("Intel® Turbo Boost Technology"))
                    Intel_Turbo_Boost_Technology = true;
                if (tmp.Contains("Intel® Hyper-Threading Technology"))
                    Intel_Hyper_Threading_Technology = true;
                if (tmp.Contains("VT-x"))
                    VT_x = true;
                if (tmp.Contains("Enhanced Intel SpeedStep® Technology"))
                    Enhanced_Intel_SpeedStep_Technology = true;
                SR.Close();
                Processor temp = new Processor();
                temp.Set(Type, Name, Kind, Cores, Thread, Speed, Max_Speed, L1, L2, L3, Bit_64, Technique, Power, Maxtmp, AMD_Virtualization_Technology, Intel_Turbo_Boost_Technology_2, Intel_Turbo_Boost_Technology, Intel_Hyper_Threading_Technology, VT_x, Enhanced_Intel_SpeedStep_Technology, Internal_Graphic, Graphic_Processor, Socket, Price, DMI, Instructions, Max_Memory, Memory_Channels, Black_Edition);
                Processors.Add(temp);
            }
            MessageBox.Show("Done");
        }

        private void AMD(object sender, RoutedEventArgs e)
        {
            List<string> Files = new List<string>();
            System.IO.DirectoryInfo get_directory = new DirectoryInfo(@"C:\Users\User\Desktop\Computer\Processors\AMD");
            var directories = get_directory.GetDirectories();
            foreach (DirectoryInfo i in directories)
            {
                var tmp = i.GetFiles();
                foreach (var j in tmp)
                    Files.Add(j.FullName);
            }
            string Type = "AMD";    //AMD or Intel
            string Kind = "-";      //Category of Processor
            string Name = "-";
            float Cores = 0;
            float Thread = 0;
            string Speed = "-";
            string Max_Speed = "-";
            string L1 = "-";
            string L2 = "-";
            string L3 = "-";
            bool Bit_64 = false;
            string Technique = "-";
            string Power = "-";
            string Maxtmp = "-";
            bool AMD_Virtualization_Technology = false;
            bool Intel_Turbo_Boost_Technology = false;
            bool Intel_Turbo_Boost_Technology_2 = false;
            bool Intel_Hyper_Threading_Technology = false;
            bool VT_x = false;
            bool Enhanced_Intel_SpeedStep_Technology = false;
            bool Internal_Graphic = false;
            string Graphic_Processor = "-";
            string Socket = "-";
            float Price = 10;
            string DMI = "-";//سرعة الناقل
            string Instructions = "-";
            string Max_Memory = "-";
            float Memory_Channels = 0;
            bool Black_Edition = false;
            for (int i = 0; i < Files.Count; i++)
            {
                System.IO.StreamReader SR = new System.IO.StreamReader(Files[i]);
                SR.ReadLine();
                SR.ReadLine();
                SR.ReadLine();
                Kind = SR.ReadLine();
                SR.ReadLine();
                Name = SR.ReadLine();
                SR.ReadLine();
                float.TryParse(SR.ReadLine(), out Cores);
                SR.ReadLine();
                float.TryParse(SR.ReadLine(), out Thread);
                SR.ReadLine();
                Speed = SR.ReadLine();
                SR.ReadLine();
                Max_Speed = SR.ReadLine();
                SR.ReadLine();
                L1 = SR.ReadLine();
                SR.ReadLine();
                L2 = SR.ReadLine();
                SR.ReadLine();
                L3 = SR.ReadLine();
                SR.ReadLine();
                if (SR.ReadLine() == "64 bit")
                    Bit_64 = true;
                SR.ReadLine();
                Technique = SR.ReadLine();
                SR.ReadLine();
                Power = SR.ReadLine();
                SR.ReadLine();
                Maxtmp = SR.ReadLine();
                if (Maxtmp.Contains('?'))
                    Maxtmp.Replace('?', '°');
                SR.ReadLine();
                string tmp = SR.ReadLine();
                if (tmp.Contains("Virtualization"))
                    AMD_Virtualization_Technology = true;
                if (tmp.Contains("Black Edition"))
                    Black_Edition = true;
                SR.ReadLine();
                Socket = SR.ReadLine();
                Processor temp = new Processor();
                temp.Set(Type, Name, Kind, Cores, Thread, Speed, Max_Speed, L1, L2, L3, Bit_64, Technique, Power, Maxtmp, AMD_Virtualization_Technology, Intel_Turbo_Boost_Technology_2, Intel_Turbo_Boost_Technology, Intel_Hyper_Threading_Technology, VT_x, Enhanced_Intel_SpeedStep_Technology, Internal_Graphic, Graphic_Processor, Socket, Price, DMI, Instructions, Max_Memory, Memory_Channels, Black_Edition);
                Processors.Add(temp);
                SR.Close();
            }
            MessageBox.Show("Done");
        }

        private void APU(object sender, RoutedEventArgs e)
        {
            List<string> Files = new List<string>();
            System.IO.DirectoryInfo get_directory = new DirectoryInfo(@"C:\Users\User\Desktop\Computer\Processors\AMD APU");
            var directories = get_directory.GetDirectories();
            foreach (DirectoryInfo i in directories)
            {
                var tmp = i.GetFiles();
                foreach (var j in tmp)
                    Files.Add(j.FullName);
            }
            string Type = "AMD";    //AMD or Intel
            string Kind = "APU";      //Category of Processor
            string Name = "-";
            float Cores = 0;
            float Thread = 0;
            string Speed = "-";
            string Max_Speed = "-";
            string L1 = "-";
            string L2 = "-";
            string L3 = "-";
            bool Bit_64 = false;
            string Technique = "-";
            string Power = "-";
            string Maxtmp = "-";
            bool AMD_Virtualization_Technology = false;
            bool Intel_Turbo_Boost_Technology = false;
            bool Intel_Turbo_Boost_Technology_2 = false;
            bool Intel_Hyper_Threading_Technology = false;
            bool VT_x = false;
            bool Enhanced_Intel_SpeedStep_Technology = false;
            bool Internal_Graphic = false;
            string Graphic_Processor = "-";
            string Socket = "-";
            float Price = 10;
            string DMI = "-";//سرعة الناقل
            string Instructions = "-";
            string Max_Memory = "-";
            float Memory_Channels = 0;
            bool Black_Edition = false;
            for (int i = 0; i < Files.Count; i++)
            {
                System.IO.StreamReader SR = new System.IO.StreamReader(Files[i]);
                SR.ReadLine();
                SR.ReadLine();
                SR.ReadLine();
                SR.ReadLine();
                SR.ReadLine();
                Name = SR.ReadLine();
                SR.ReadLine();
                float.TryParse(SR.ReadLine(), out Cores);
                SR.ReadLine();
                float.TryParse(SR.ReadLine(), out Thread);
                SR.ReadLine();
                string tmp = SR.ReadLine();
                if (tmp.Contains("HD"))
                {
                    Internal_Graphic = true;
                    Graphic_Processor = tmp;
                }
                SR.ReadLine();
                Speed = SR.ReadLine();
                SR.ReadLine();
                Max_Speed = SR.ReadLine();
                SR.ReadLine();
                L1 = SR.ReadLine();
                SR.ReadLine();
                L2 = SR.ReadLine();
                SR.ReadLine();
                L3 = SR.ReadLine();
                SR.ReadLine();
                if (SR.ReadLine() == "64 bit")
                    Bit_64 = true;
                SR.ReadLine();
                Technique = SR.ReadLine();
                SR.ReadLine();
                Power = SR.ReadLine();
                SR.ReadLine();
                Maxtmp = SR.ReadLine();
                if (Maxtmp.Contains('?'))
                    Maxtmp.Replace('?', '°');
                SR.ReadLine();
                string tmp1 = SR.ReadLine();
                if (tmp1.Contains("Virtualization"))
                    AMD_Virtualization_Technology = true;
                if (tmp1.Contains("Black Edition"))
                    Black_Edition = true;
                SR.ReadLine();
                Socket = SR.ReadLine();
                SR.Close();
                Processor temp = new Processor();
                temp.Set(Type, Name, Kind, Cores, Thread, Speed, Max_Speed, L1, L2, L3, Bit_64, Technique, Power, Maxtmp, AMD_Virtualization_Technology, Intel_Turbo_Boost_Technology_2, Intel_Turbo_Boost_Technology, Intel_Hyper_Threading_Technology, VT_x, Enhanced_Intel_SpeedStep_Technology, Internal_Graphic, Graphic_Processor, Socket, Price, DMI, Instructions, Max_Memory, Memory_Channels, Black_Edition);
                Processors.Add(temp);
            }
            MessageBox.Show("Done");
        }

        private void MB(object sender, RoutedEventArgs e)
        {
            List<string> Files = new List<string>();
            DirectoryInfo[] get_directory = new DirectoryInfo[8];
            get_directory[0] = new DirectoryInfo(@"C:\Users\User\Desktop\Computer\Motherboard\AMD\AM3");
            get_directory[1] = new DirectoryInfo(@"C:\Users\User\Desktop\Computer\Motherboard\AMD\AM3+");
            get_directory[2] = new DirectoryInfo(@"C:\Users\User\Desktop\Computer\Motherboard\AMD\FM1");
            get_directory[3] = new DirectoryInfo(@"C:\Users\User\Desktop\Computer\Motherboard\Intel\775");
            get_directory[4] = new DirectoryInfo(@"C:\Users\User\Desktop\Computer\Motherboard\Intel\1155");
            get_directory[5] = new DirectoryInfo(@"C:\Users\User\Desktop\Computer\Motherboard\Intel\1156");
            get_directory[6] = new DirectoryInfo(@"C:\Users\User\Desktop\Computer\Motherboard\Intel\1366");
            get_directory[7] = new DirectoryInfo(@"C:\Users\User\Desktop\Computer\Motherboard\Intel\2011");
            DirectoryInfo[][] directories = new DirectoryInfo[8][];
            for (int k = 0; k < 8; k++)
            {
                directories[k] = get_directory[k].GetDirectories();
                foreach (DirectoryInfo i in directories[k])
                {
                    var tmp = i.GetFiles();
                    foreach (var j in tmp)
                        Files.Add(j.FullName);
                }
            }
            string Type = "-"; //AMD or Intel
            string Socket = "-";
            string Company = "-";
            string Name = "-";
            string Power = "-";
            string Chipset = "-";
            string Max_Mem = "-";
            bool Internal_Graphic = false;
            string Graphic_Processor = "-";
            string Lan = "-";
            float DMI = 0;
            float VGA = 0;
            float HDMI = 0;
            float DVI = 0;
            float PCIe_20_x16 = 0;
            float PCIe_20_x1 = 0;
            float PCI_22 = 0;
            float PCI = 0;
            float SATA_6Gbs = 0;
            float SATA_3Gbs = 0;
            float External_SATA = 0;
            float IEEE_1394a = 0;
            float LAN_RJ45_port = 0;
            float Channel_Audio_IO = 0;
            float PS2_Keyboard = 0;
            float PS2_mouse = 0;
            float USB_30 = 0;
            float USB_20 = 0;
            string Bluetooth = "-";
            string WLAN = "-";
            bool AMD_CrossFire = false;
            bool NVIDIA_SLI = false;
            bool Winki_3 = false;
            bool Auto_OC_Genie = false;
            bool Intel_vPro_Technology = false;
            bool EPU = false;
            float Price = 10;
            for (int i = 0; i < Files.Count; i++)
            {
                System.IO.StreamReader SR = new System.IO.StreamReader(Files[i]);
                SR.ReadLine();
                SR.ReadLine();
                SR.ReadLine();
                Company = SR.ReadLine();
                SR.ReadLine();
                Name = SR.ReadLine();
                SR.ReadLine();
                Socket = SR.ReadLine();
                SR.ReadLine();
                Chipset = SR.ReadLine();
                if (Chipset.Contains("Intel"))
                    Type = "Intel";
                else
                    Type = "AMD";
                SR.ReadLine();
                Max_Mem = SR.ReadLine();
                SR.ReadLine();
                Graphic_Processor=SR.ReadLine();
                if (!Graphic_Processor.Contains('-'))
                    Internal_Graphic = true;
                SR.ReadLine();
                Lan = SR.ReadLine();
                string Temp = SR.ReadLine();
                while (string.Compare(Temp, "Bluetooth") != 0)
                {
                    if (Temp.Contains("DMI")) { float.TryParse(Temp[0].ToString(), out DMI); Temp = SR.ReadLine(); continue; }
                    if (Temp.Contains("VGA")) { float.TryParse(Temp[0].ToString(), out VGA); Temp = SR.ReadLine(); continue; }
                    if (Temp.Contains("HDMI")) { float.TryParse(Temp[0].ToString(), out HDMI); Temp = SR.ReadLine(); continue; }
                    if (Temp.Contains("DVI")) { float.TryParse(Temp[0].ToString(), out DVI); Temp = SR.ReadLine(); continue; }
                    if (Temp.Contains("PCIe 2.0 x16")) { float.TryParse(Temp[0].ToString(), out PCIe_20_x16); Temp = SR.ReadLine(); continue; }
                    if (Temp.Contains("PCIe 2.0 x1")) { float.TryParse(Temp[0].ToString(), out PCIe_20_x1); Temp = SR.ReadLine(); continue; }
                    if (Temp.Contains("PCI 2.2")) { float.TryParse(Temp[0].ToString(), out PCI_22); Temp = SR.ReadLine(); continue; }
                    if (Temp.Contains("PCI")) { float.TryParse(Temp[0].ToString(), out PCI); Temp = SR.ReadLine(); continue; }
                    if (Temp.Contains("SATA 6Gb/s")) { float.TryParse(Temp[0].ToString(), out SATA_6Gbs); Temp = SR.ReadLine(); continue; }
                    if (Temp.Contains("SATA 3Gb/s")) { float.TryParse(Temp[0].ToString(), out SATA_3Gbs); Temp = SR.ReadLine(); continue; }
                    if (Temp.Contains("External SATA")) { float.TryParse(Temp[0].ToString(), out External_SATA); Temp = SR.ReadLine(); continue; }
                    if (Temp.Contains("IEEE 1394a")) { float.TryParse(Temp[0].ToString(), out IEEE_1394a); Temp = SR.ReadLine(); continue; }
                    if (Temp.Contains("LAN(RJ45) port")) { float.TryParse(Temp[0].ToString(), out LAN_RJ45_port); Temp = SR.ReadLine(); continue; }
                    if (Temp.Contains("Channel Audio I/O")) { float.TryParse(Temp[0].ToString(), out Channel_Audio_IO); Temp = SR.ReadLine(); continue; }
                    if (Temp.Contains("Keyboard")) { float.TryParse(Temp[0].ToString(), out PS2_Keyboard); Temp = SR.ReadLine(); continue; }
                    if (Temp.Contains("Mouse")) { float.TryParse(Temp[0].ToString(), out PS2_mouse); Temp = SR.ReadLine(); continue; }
                    if (Temp.Contains("USB 3.0")) { float.TryParse(Temp[0].ToString(), out USB_30); Temp = SR.ReadLine(); continue; }
                    if (Temp.Contains("USB 2.0")) { float.TryParse(Temp[0].ToString(), out USB_20); Temp = SR.ReadLine(); continue; }
                    Temp = SR.ReadLine();
                }
                Bluetooth = SR.ReadLine();
                SR.ReadLine();
                WLAN = SR.ReadLine();
                SR.ReadLine();
                Temp = SR.ReadLine();
                if (Temp.Contains("AMD CrossFire")) AMD_CrossFire = true;
                if (Temp.Contains("NVIDIA SLI")) NVIDIA_SLI = true;
                if (Temp.Contains("Winki 3")) Winki_3 = true;
                if (Temp.Contains("Auto OC Genie")) Auto_OC_Genie = true;
                if (Temp.Contains("Intel vPro Technology")) Intel_vPro_Technology = true;
                if (Temp.Contains("EPU")) EPU = true;
                SR.Close();
                MB temp = new MB();
                temp.Set(Type, Socket, Company, Name, Power, Chipset, Max_Mem, Internal_Graphic, Graphic_Processor, Lan, DMI, VGA, HDMI, DVI, PCIe_20_x16, PCIe_20_x1, PCI_22, PCI, SATA_6Gbs, SATA_3Gbs, External_SATA, IEEE_1394a, LAN_RJ45_port, Channel_Audio_IO, PS2_Keyboard, PS2_mouse, USB_30, USB_20, Bluetooth, WLAN, AMD_CrossFire, NVIDIA_SLI, Winki_3, Auto_OC_Genie, Intel_vPro_Technology, EPU, Price);
                MotherBoards.Add(temp);
            }
            MessageBox.Show("Done");
        }

        private void GPU(object sender, RoutedEventArgs e)
        {
            List<string> Files = new List<string>();
            DirectoryInfo[] get_directory = new DirectoryInfo[2];
            get_directory[0] = new DirectoryInfo(@"C:\Users\User\Desktop\Computer\Graphic Card\AMD");
            get_directory[1] = new DirectoryInfo(@"C:\Users\User\Desktop\Computer\Graphic Card\Nvidia");
            DirectoryInfo[][] directories = new DirectoryInfo[2][];
            for (int k = 0; k < 2; k++)
            {
                directories[k] = get_directory[k].GetDirectories();
                foreach (DirectoryInfo i in directories[k])
                {
                    var tmp = i.GetFiles();
                    foreach (var j in tmp)
                        Files.Add(j.FullName);
                }
            }
            //for(int i=0;i<Files.Count;i++)
            //    ListBox1.Items.Add(Files[i]);
            string Type="-"; //AMD or Nvidia
            string Company="-";
            string Name = "-";
            string GPU = "-";
            string Socket = "-";
            string Mem_Size = "-";
            string DDR_Type = "-";
            string Mem_Channel = "-";
            string Processor_Speed = "-";
            string Mem_Speed = "-";
            string Max_Resolution = "-";
            string DirectX_Version = "-";
            string OpenGL_Version = "-";
            string Pixel_Shader_Version = "-";
            string Cores = "-";
            bool CrossFire=false;
            bool AMD_HD3D = false;
            bool SLI = false;
            bool PhysX = false;
            bool Vision_3D = false;
            float DVI = 0;
            float HDMI = 0;
            bool Mini_HDMI = false;
            bool Mini_DisplayPort = false;
            float DisplayPort = 0;
            float D_SUB = 0;
            float Price = 10;
            for (int i = 0; i < Files.Count; i++)
            {
                System.IO.StreamReader SR = new System.IO.StreamReader(Files[i]);
                if (Files[i].Contains("Nvidia"))
                    Type = "Nvidia";
                else
                    Type = "AMD";
                SR.ReadLine();
                SR.ReadLine();
                Company = SR.ReadLine();
                SR.ReadLine();
                Name = SR.ReadLine();
                SR.ReadLine();
                GPU = SR.ReadLine();
                SR.ReadLine();
                Socket = SR.ReadLine();
                SR.ReadLine();
                Mem_Size = SR.ReadLine();
                SR.ReadLine();
                DDR_Type= SR.ReadLine();
                SR.ReadLine();
                Mem_Channel = SR.ReadLine();
                SR.ReadLine();
                Processor_Speed=SR.ReadLine();
                SR.ReadLine();
                Mem_Speed = SR.ReadLine();
                SR.ReadLine();
                Max_Resolution = SR.ReadLine();
                SR.ReadLine();
                DirectX_Version = SR.ReadLine();
                SR.ReadLine();
                OpenGL_Version = SR.ReadLine();
                SR.ReadLine();
                Pixel_Shader_Version = SR.ReadLine();
                SR.ReadLine();
                Cores = SR.ReadLine();
                SR.ReadLine();
                string Tmp=SR.ReadLine();
                if (Tmp.Contains("CrossFire")) CrossFire = true;
                if (Tmp.Contains("AMD HD3D")) AMD_HD3D = true;
                if (Tmp.Contains("SLI")) SLI = true;
                if (Tmp.Contains("PhysX")) PhysX = true;
                if (Tmp.Contains("Vision 3D")) Vision_3D = true;
                SR.ReadLine();
                Tmp = SR.ReadLine();
                var Temp = Tmp.Split(',');
                foreach (string j in Temp)
                {
                    if (j.Contains("DVI"))
                    {
                        int x = 0;
                        if (j[0] == ' ')
                            x = 1;
                        if (j[x] > 30 && j[x] < 39)
                            float.TryParse(j[0].ToString(), out DVI);
                        else
                            DVI = 1;
                    }
                    if (j.Contains("HDMI"))
                    {
                        int x = 0;
                        if (j[0] == ' ')
                            x = 1;
                        if (j[x] > 30 && j[x] < 39)
                            float.TryParse(j[0].ToString(), out HDMI);
                        else
                            HDMI = 1;
                        if (j.Contains("Mini"))
                            Mini_HDMI = true;
                    }
                    if (j.Contains("DisplayPort"))
                    {
                        int x = 0;
                        if (j[0] == ' ')
                            x = 1;
                        if (j[x] > 30 && j[x] < 39)
                            float.TryParse(j[0].ToString(), out DisplayPort);
                        else
                            DisplayPort = 1;
                        if (j.Contains("Mini"))
                            Mini_HDMI = true;
                    }
                    if (j.Contains("D-SUB"))
                    {
                        int x = 0;
                        if (j[0] == ' ')
                            x = 1;
                        if (j[x] > 30 && j[x] < 39)
                            float.TryParse(j[0].ToString(), out D_SUB);
                        else
                            D_SUB = 1;
                    }
                }
                SR.Close();
                VGA temp = new VGA();
                temp.Set(Type, Company, Name, GPU, Socket, Mem_Size, DDR_Type, Mem_Channel, Processor_Speed, Mem_Speed, Max_Resolution, DirectX_Version, OpenGL_Version, Pixel_Shader_Version, Cores, CrossFire, AMD_HD3D, SLI, PhysX, Vision_3D, DVI, HDMI, Mini_HDMI, DisplayPort, Mini_DisplayPort, D_SUB, Price);
                Graphigc_Cards.Add(temp);
            }
            MessageBox.Show("Done");
        }

        private void HD(object sender, RoutedEventArgs e)
        {
            List<string> Files = new List<string>();
            DirectoryInfo[] get_directory = new DirectoryInfo[3];
            get_directory[0] = new DirectoryInfo(@"C:\Users\User\Desktop\Computer\Hard Disk\HDD\Sata 2");
            get_directory[1] = new DirectoryInfo(@"C:\Users\User\Desktop\Computer\Hard Disk\HDD\Sata 3");
            get_directory[2] = new DirectoryInfo(@"C:\Users\User\Desktop\Computer\Hard Disk\SSD\Sata 3");
            DirectoryInfo[][] directories = new DirectoryInfo[3][];
            for (int k = 0; k < 3; k++)
            {
                directories[k] = get_directory[k].GetDirectories();
                foreach (DirectoryInfo i in directories[k])
                {
                    var tmp = i.GetFiles();
                    foreach (var j in tmp)
                        Files.Add(j.FullName);
                }
            }
            //for(int i=0;i<Files.Count;i++)
            //    ListBox1.Items.Add(Files[i]);
            string Type = "-";    //SSD or HDD
            string Socket = "-";
            string Company = "-";
            string Name = "-";
            string Code = "-";
            string Capacity = "-";
            string Rotation_Speed = "-";
            string Buffer = "-";
            string Max_Speed = "-";
            string Read_Speed = "-";
            string Write_Speed = "-";
            string Weight = "-";
            string Power = "-";
            float Price = 10;
            for (int i = 0; i < Files.Count; i++)
            {
                System.IO.StreamReader SR = new System.IO.StreamReader(Files[i]);
                if (Files[i].Contains("SSD"))
                    Type = "SSD";
                else
                    Type = "HDD";
                Name = string.Concat(SR.ReadLine(), SR.ReadLine());
                SR.ReadLine();
                SR.ReadLine();
                Company = SR.ReadLine();
                SR.ReadLine();
                Code = SR.ReadLine();
                SR.ReadLine();
                Capacity = SR.ReadLine();
                SR.ReadLine();
                if (Type.Equals("SSD"))
                {
                    Read_Speed = SR.ReadLine();
                    SR.ReadLine();
                    Write_Speed = SR.ReadLine();
                    SR.ReadLine();
                    Max_Speed = SR.ReadLine();
                    SR.ReadLine();
                    Weight = SR.ReadLine();
                    SR.ReadLine();
                    Power = SR.ReadLine();
                }
                else
                {
                    Rotation_Speed = SR.ReadLine();
                    SR.ReadLine();
                    Buffer = SR.ReadLine();
                    SR.ReadLine();
                    Max_Speed = SR.ReadLine();
                }
                SR.ReadLine();
                Socket = SR.ReadLine();
                SR.Close();
                HD temp = new HD();
                temp.Set(Type, Socket, Company, Name, Code, Capacity, Rotation_Speed, Buffer, Max_Speed, Read_Speed, Write_Speed, Weight, Power, Price);
                Hard_Disks.Add(temp);
            }
            MessageBox.Show("Done");
        }

        private void Tablet(object sender, RoutedEventArgs e)
        {
            List<string> Files = new List<string>();
            System.IO.DirectoryInfo get_directory = new DirectoryInfo(@"C:\Users\User\Desktop\Computer\Tablet");
            var directories = get_directory.GetDirectories();
            foreach (DirectoryInfo i in directories)
            {
                var tmp = i.GetFiles();
                foreach (var j in tmp)
                    Files.Add(j.FullName);
            }
            string Company = "-";
            string Name = "-";
            string Size = "-";
            string Weight = "-";
            string Screen_Size = "-";
            string Resolution = "-";
            string OS = "-";
            string Processor_Name = "-";
            string Processor_Speed = "-";
            string Cores = "-";
            string Memory_Size = "-";
            string Internal_Memory = "-";
            bool Memory_Card = false;
            string Max_Memory_Card_Size = "-";
            string Camera = "-";
            bool Flash = false;
            string Video_Resolution = "-";
            string Web_Cam = "-";
            string G = "-";   //جيل شبكة الاتصال
            string Bluetooth = "-";
            float Price = 10;
            for (int i = 0; i < Files.Count; i++)
            {
                System.IO.StreamReader SR = new System.IO.StreamReader(Files[i]);
                Company = SR.ReadLine();
                Company = First_Word(Company);
                SR.ReadLine();
                Name = SR.ReadLine();
                SR.ReadLine();
                Size = SR.ReadLine();
                SR.ReadLine();
                Weight = SR.ReadLine();
                SR.ReadLine();
                Screen_Size = SR.ReadLine();
                SR.ReadLine();
                Resolution = SR.ReadLine();
                SR.ReadLine();
                OS = SR.ReadLine();
                SR.ReadLine();
                Processor_Name = SR.ReadLine();
                SR.ReadLine();
                Processor_Speed = SR.ReadLine();
                SR.ReadLine();
                Cores = SR.ReadLine();
                Memory_Size = SR.ReadLine();
                SR.ReadLine();
                Internal_Memory = SR.ReadLine();
                SR.ReadLine();
                Max_Memory_Card_Size = SR.ReadLine();
                SR.ReadLine();
                if (Max_Memory_Card_Size.Contains("-"))
                    Memory_Card = false;
                else
                    Memory_Card = true;
                Camera = SR.ReadLine();
                SR.ReadLine();
                if (SR.ReadLine().Contains("-"))
                    Flash = false;
                else
                    Flash = true;
                SR.ReadLine();
                Video_Resolution = SR.ReadLine();
                SR.ReadLine();
                Web_Cam = SR.ReadLine();
                SR.ReadLine();
                G = SR.ReadLine();
                SR.ReadLine();
                Bluetooth = SR.ReadLine();
                SR.ReadLine();
                SR.Close();
                Tablet temp = new Tablet();
                temp.Set(Company, Name, Size, Weight, Screen_Size, Resolution, OS, Processor_Name, Processor_Speed, Cores, Memory_Size, Internal_Memory, Memory_Card, Max_Memory_Card_Size, Camera, Flash, Video_Resolution, Web_Cam, G, Bluetooth, Price);
                Tablet_PCs.Add(temp);
            }
            MessageBox.Show("Done");
        }

        private void save_data_strucure(List<Processor> OBJ, string Path)
        {
            BinaryFormatter Serlizer = new BinaryFormatter();
            Stream Writer = File.Create(Path);
            Serlizer.Serialize(Writer, OBJ);
            Writer.Close();
        }

        private void save_data_strucure(List<MB> OBJ, string Path)
        {
            BinaryFormatter Serlizer = new BinaryFormatter();
            Stream Writer = File.Create(Path);
            Serlizer.Serialize(Writer, OBJ);
            Writer.Close();
        }

        private void save_data_strucure(List<VGA> OBJ, string Path)
        {
            BinaryFormatter Serlizer = new BinaryFormatter();
            Stream Writer = File.Create(Path);
            Serlizer.Serialize(Writer, OBJ);
            Writer.Close();
        }

        private void save_data_strucure(List<HD> OBJ, string Path)
        {
            BinaryFormatter Serlizer = new BinaryFormatter();
            Stream Writer = File.Create(Path);
            Serlizer.Serialize(Writer, OBJ);
            Writer.Close();
        }

        private void save_data_strucure(List<Tablet> OBJ, string Path)
        {
            BinaryFormatter Serlizer = new BinaryFormatter();
            Stream Writer = File.Create(Path);
            Serlizer.Serialize(Writer, OBJ);
            Writer.Close();
        }

        private void read_data_strucure(string Path, out List<Processor> list)
        {
            list=new List<Processor>();
            BinaryFormatter deserlizer = new BinaryFormatter();
            if (File.Exists(Path))
            {
                Stream Reader = File.OpenRead(Path);
                list = (List<Processor>)deserlizer.Deserialize(Reader);
                Reader.Close();
            }
        }

        private void read_data_strucure(string Path, out List<MB> list)
        {
            list = new List<MB>();
            BinaryFormatter deserlizer = new BinaryFormatter();
            if (File.Exists(Path))
            {
                Stream Reader = File.OpenRead(Path);
                list = (List<MB>)deserlizer.Deserialize(Reader);
                Reader.Close();
            }
        }

        private void read_data_strucure(string Path, out List<VGA> list)
        {
            list = new List<VGA>();
            BinaryFormatter deserlizer = new BinaryFormatter();
            if (File.Exists(Path))
            {
                Stream Reader = File.OpenRead(Path);
                list = (List<VGA>)deserlizer.Deserialize(Reader);
                Reader.Close();
            }
        }

        private void read_data_strucure(string Path, out List<HD> list)
        {
            list = new List<HD>();
            BinaryFormatter deserlizer = new BinaryFormatter();
            if (File.Exists(Path))
            {
                Stream Reader = File.OpenRead(Path);
                list = (List<HD>)deserlizer.Deserialize(Reader);
                Reader.Close();
            }
        }

        private void read_data_strucure(string Path, out List<Tablet> list)
        {
            list = new List<Tablet>();
            BinaryFormatter deserlizer = new BinaryFormatter();
            if (File.Exists(Path))
            {
                Stream Reader = File.OpenRead(Path);
                list = (List<Tablet>)deserlizer.Deserialize(Reader);
                Reader.Close();
            }
        }

        private string First_Word(string Str)
        {
            string[] tmp = Str.Split(' ');
            return tmp[0];
        }

        private void Read(object sender, RoutedEventArgs e)
        {
            if (TextBox1.Text.Contains("Processors"))
                read_data_strucure(TextBox1.Text, out Processors);
            else if (TextBox1.Text.Contains("MotherBoards"))
                read_data_strucure(TextBox1.Text, out MotherBoards);
            else if (TextBox1.Text.Contains("GraphigcCards"))
                read_data_strucure(TextBox1.Text, out Graphigc_Cards);
            else if (TextBox1.Text.Contains("HardDisks"))
                read_data_strucure(TextBox1.Text, out Hard_Disks);
            else if (TextBox1.Text.Contains("TabletPCs"))
                read_data_strucure(TextBox1.Text, out Tablet_PCs);
            if (TextBox1.Text.Contains("Processors"))
                foreach (Processor i in Processors)
                {
                    TextBox2.Text = TextBox2.Text+i.display();
                    TextBox2.Text = TextBox2.Text + "\n" + "\n";
                }
            else if (TextBox1.Text.Contains("MotherBoards"))
                foreach (MB i in MotherBoards)
                {
                    TextBox2.Text = TextBox2.Text + i.display();
                    TextBox2.Text = TextBox2.Text + "\n" + "\n";
                }
            else if (TextBox1.Text.Contains("GraphigcCards"))
                foreach (VGA i in Graphigc_Cards)
                {
                    TextBox2.Text = TextBox2.Text + i.display();
                    TextBox2.Text = TextBox2.Text + "\n" + "\n";
                }
            else if (TextBox1.Text.Contains("HardDisks"))
                foreach (HD i in Hard_Disks)
                {
                    TextBox2.Text = TextBox2.Text + i.display();
                    TextBox2.Text = TextBox2.Text + "\n" + "\n";
                }
            else if (TextBox1.Text.Contains("TabletPCs"))
                foreach (Tablet i in Tablet_PCs)
                {
                    TextBox2.Text = TextBox2.Text + i.display();
                    TextBox2.Text = TextBox2.Text + "\n" + "\n";
                }
        }

        private void Save(object sender, RoutedEventArgs e)
        {
            save_data_strucure(Processors, @"C:\Users\User\Desktop\Files\Processors.Mhmd");
            save_data_strucure(MotherBoards, @"C:\Users\User\Desktop\Files\MotherBoards.Mhmd");
            save_data_strucure(Graphigc_Cards, @"C:\Users\User\Desktop\Files\GraphigcCards.Mhmd");
            save_data_strucure(Hard_Disks, @"C:\Users\User\Desktop\Files\HardDisks.Mhmd");
            save_data_strucure(Tablet_PCs, @"C:\Users\User\Desktop\Files\TabletPCs.Mhmd");
        }

    }

    [Serializable]
    class Processor
    {
        int ID;
        string Type; //AMD or Intel
        string Name;
        string Kind;
        float Cores;
        float Thread;
        string Speed;
        string Max_Speed;
        string L1;
        string L2;
        string L3;
        bool Bit_64;
        string Technique;
        string Power;
        string Maxtmp;
        bool AMD_Virtualization_Technology;
        bool Intel_Turbo_Boost_Technology_2;
        bool Intel_Turbo_Boost_Technology;
        bool Intel_Hyper_Threading_Technology;
        bool VT_x;
        bool Enhanced_Intel_SpeedStep_Technology;
        bool Internal_Graphic;
        string Graphic_Processor;
        string Socket;
        float Price;
        string DMI;     //سرعة الناقل
        string Instructions;
        string Max_Memory;
        static int number = 1;
        float Memory_Channels;
        bool Black_Edition;

        public Processor()
        {
            ID = number;
            number++;
        }

        public void Set(string Type1, string Name1, string Kind1, float Cores1, float Thread1, string Speed1, string Max_Speed1, string L11, string L21, string L31, bool Bit_641, string Technique1, string Power1, string Maxtmp1, bool AMD_Virtualization_Technology1, bool Intel_Turbo_Boost_Technology_21, bool Intel_Turbo_Boost_Technology1, bool Intel_Hyper_Threading_Technology1, bool VT_x1, bool Enhanced_Intel_SpeedStep_Technology1, bool Internal_Graphic1, string Graphic_Processor1, string Socket1, float Price1, string DMI1, string Instructions1, string Max_Memory1, float Memory_Channels1, bool Black_Edition1)
        {
            Type = Type1;
            Name = Name1;
            Kind = Kind1;
            Cores = Cores1;
            Thread = Thread1;
            Speed = Speed1;
            Max_Speed = Max_Speed1;
            L1 = L11;
            L2 = L21;
            L3 = L31;
            Bit_64 = Bit_641;
            Technique = Technique1;
            Power = Power1;
            Maxtmp = Maxtmp1;
            AMD_Virtualization_Technology = AMD_Virtualization_Technology1;
            Intel_Turbo_Boost_Technology_2 = Intel_Turbo_Boost_Technology_21;
            Intel_Turbo_Boost_Technology = Intel_Turbo_Boost_Technology1;
            Intel_Hyper_Threading_Technology = Intel_Hyper_Threading_Technology1;
            VT_x = VT_x1;
            Enhanced_Intel_SpeedStep_Technology = Enhanced_Intel_SpeedStep_Technology1;
            Internal_Graphic = Internal_Graphic1;
            Graphic_Processor = Graphic_Processor1;
            Socket = Socket1;
            Price = Price1;
            DMI = DMI1;
            Instructions = Instructions1;
            Max_Memory = Max_Memory1;
            Memory_Channels = Memory_Channels1;
            Black_Edition = Black_Edition1;
        }

        public string display()
        {
            string Text = null;
            Text = (Text + ID.ToString() + "\n");
            Text=(Text+Name);
            Text = (Text + Cores.ToString() + "\n");
            Text = (Text + Thread.ToString() + "\n");
            Text = (Text + Speed.ToString() + "\n");
            Text = (Text + Max_Speed.ToString() + "\n");
            Text = (Text + L1.ToString() + "\n");
            Text = (Text + L2.ToString() + "\n");
            Text = (Text + L3.ToString() + "\n");
            Text = (Text + Bit_64.ToString() + "\n");
            Text = (Text + Technique.ToString() + "\n");
            Text = (Text + Maxtmp.ToString() + "\n");
            Text = (Text + AMD_Virtualization_Technology.ToString() + "\n");
            Text = (Text + Intel_Turbo_Boost_Technology_2.ToString() + "\n");
            Text = (Text + Intel_Turbo_Boost_Technology.ToString() + "\n");
            Text = (Text + VT_x.ToString() + "\n");
            Text = (Text + Enhanced_Intel_SpeedStep_Technology.ToString() + "\n");
            Text = (Text + Internal_Graphic.ToString() + "\n");
            Text = (Text + Graphic_Processor.ToString() + "\n");
            Text = (Text + Socket.ToString() + "\n");
            Text = (Text + Price.ToString() + "\n");
            Text = (Text + DMI.ToString() + "\n");
            Text = (Text + Instructions.ToString() + "\n");
            Text = (Text + Max_Memory.ToString() + "\n");
            Text = (Text + Memory_Channels.ToString() + "\n");
            return Text;
        }

    }

    [Serializable]
    class MB
    {
        int ID;
        string Type; //AMD or Intel
        string Socket;
        string Company;
        string Name;
        string Power;
        string Chipset;
        string Max_Mem;
        bool Internal_Graphic;
        string Graphic_Processor;
        string Lan;
        float DMI;
        float VGA;
        float HDMI;
        float DVI;
        float PCIe_20_x16;
        float PCIe_20_x1;
        float PCI_22;
        float PCI;
        float SATA_6Gbs;
        float SATA_3Gbs;
        float External_SATA;
        float IEEE_1394a;
        float LAN_RJ45_port;
        float Channel_Audio_IO;
        float PS2_Keyboard;
        float PS2_mouse;
        float USB_30;
        float USB_20;
        string Bluetooth;
        string WLAN;
        bool AMD_CrossFire;
        bool NVIDIA_SLI;
        bool Winki_3;
        bool Auto_OC_Genie;
        bool Intel_vPro_Technology;
        bool EPU;        
        float Price;
        static int number = 1;

        public MB()
        {
            ID = number;
            number++;
        }

        public void Set(string Type1,string Socket1,string Company1,string Name1,string Power1,string Chipset1,string Max_Mem1,bool Internal_Graphic1,string Graphic_Processor1,string Lan1,float DMI1,float VGA1,float HDMI1,float DVI1,float PCIe_20_x161,float PCIe_20_x11,float PCI_221,float PCI1,float SATA_6Gbs1,float SATA_3Gbs1,float External_SATA1,float IEEE_1394a1,float LAN_RJ45_port1,float Channel_Audio_IO1,float PS2_Keyboard1,float PS2_mouse1,float USB_301,float USB_201,string Bluetooth1,string WLAN1,bool AMD_CrossFire1,bool NVIDIA_SLI1,bool Winki_31,bool Auto_OC_Genie1,bool Intel_vPro_Technology1,bool EPU1,float Price1)
        {
            Type = Type1;
            Name = Name1;         
            Power = Power1;
            Internal_Graphic = Internal_Graphic1;
            Graphic_Processor = Graphic_Processor1;
            Socket = Socket1;
            Price = Price1;
            DMI = DMI1;
            Company = Company1;
            Chipset = Chipset1;
            Max_Mem = Max_Mem1;
            Internal_Graphic = Internal_Graphic1;
            Graphic_Processor = Graphic_Processor1;
            Lan = Lan1;
            DMI = DMI1;
            VGA = VGA1;
            HDMI = HDMI1;
            DVI = DVI1;
            PCIe_20_x16 = PCIe_20_x161;
            PCIe_20_x1 = PCIe_20_x11;
            PCI_22 = PCI_221;
            PCI = PCI1;
            SATA_6Gbs = SATA_6Gbs1;
            SATA_3Gbs = SATA_3Gbs1;
            External_SATA = External_SATA1;
            IEEE_1394a = IEEE_1394a1;
            LAN_RJ45_port = LAN_RJ45_port1;
            Channel_Audio_IO = Channel_Audio_IO1;
            PS2_Keyboard = PS2_Keyboard1;
            PS2_mouse = PS2_mouse1;
            USB_30 = USB_301;
            USB_20 = USB_201;
            Bluetooth = Bluetooth1;
            WLAN = WLAN1;
            AMD_CrossFire = AMD_CrossFire1;
            NVIDIA_SLI = NVIDIA_SLI1;
            Winki_3 = Winki_31;
            Auto_OC_Genie = Auto_OC_Genie1;
            Intel_vPro_Technology = Intel_vPro_Technology1;
            EPU = EPU1;
            Price = Price1;
        }

        public string display()
        {
            return null;
        }

    }

    [Serializable]
    class VGA
    {
        int ID;
        string Type; //AMD or Nvidia
        string Company;
        string Name;
        string GPU;
        string Socket;
        string Mem_Size;
        string DDR_Type;
        string Mem_Channel;
        string Processor_Speed;
        string Mem_Speed;
        string Max_Resolution;
        string DirectX_Version;
        string OpenGL_Version;
        string Pixel_Shader_Version;
        string Cores;
        bool CrossFire;
        bool AMD_HD3D;
        bool SLI;
        bool PhysX;
        bool Vision_3D;
        float DVI;
        float HDMI;
        bool Mini_HDMI;
        float DisplayPort;
        bool Mini_DisplayPort;
        float D_SUB;
        float Price;
        static int number = 1;

        public VGA()
        {
            ID = number;
            number++;
        }

        public void Set(string Type1, string Company1, string Name1, string GPU1, string Socket1, string Mem_Size1, string DDR_Type1, string Mem_Channel1, string Processor_Speed1, string Mem_Speed1, string Max_Resolution1, string DirectX_Version1, string OpenGL_Version1, string Pixel_Shader_Version1, string Cores1, bool CrossFire1, bool AMD_HD3D1, bool SLI1, bool PhysX1, bool Vision_3D1, float DVI1, float HDMI1,bool Mini_HDMI1, float DisplayPort1, bool Mini_DisplayPort1, float D_SUB1, float Price1)
        {
            Type = Type1;
            Company = Company1;
            Name = Name1;
            GPU = GPU1;
            Socket = Socket1;
            Mem_Size = Mem_Size1;
            DDR_Type = DDR_Type1;
            Mem_Channel = Mem_Channel1;
            Processor_Speed = Processor_Speed1;
            Mem_Speed = Mem_Speed1;
            Max_Resolution = Max_Resolution1;
            DirectX_Version = DirectX_Version1;
            OpenGL_Version = OpenGL_Version1;
            Pixel_Shader_Version = Pixel_Shader_Version1;
            Cores = Cores1;
            CrossFire = CrossFire1;
            AMD_HD3D = AMD_HD3D1;
            SLI = SLI1;
            PhysX = PhysX1;
            Vision_3D = Vision_3D1;
            Mini_DisplayPort = Mini_DisplayPort1;
            DVI = DVI1;
            HDMI = HDMI1;
            Mini_HDMI = Mini_HDMI1;
            DisplayPort = DisplayPort1;
            D_SUB = D_SUB1;
            Price = Price1;
        }

        public string display()
        {
            return null;
        }

    }

    [Serializable]
    class HD
    {
        int ID;
        string Type;    //SSD or HDD
        string Socket;
        string Company;
        string Name;
        string Code;
        string Capacity;
        string Rotation_Speed;
        string Buffer;
        string Max_Speed;
        string Read_Speed;
        string Write_Speed;
        string Weight;
        string Power;
        float Price;
        static int number = 1;

        public HD()
        {
            ID = number;
            number++;
        }

        public void Set(string Type1,string Socket1,string Company1,string Name1,string Code1,string Capacity1,string Rotation_Speed1,string Buffer1,string Max_Speed1,string Read_Speed1,string Write_Speed1,string Weight1,string Power1,float Price1)
        {
            Type = Type1;
            Socket = Socket1;
            Company = Company1;
            Name = Name1;
            Code = Code1;
            Capacity = Capacity1;
            Rotation_Speed = Rotation_Speed1;
            Buffer = Buffer1;
            Max_Speed = Max_Speed1;
            Read_Speed = Read_Speed1;
            Write_Speed = Write_Speed1;
            Weight = Weight1;
            Power = Power1;
            Price = Price1;
        }

        public string display()
        {
            return null;
        }

    }

    [Serializable]
    class Tablet
    {
        int ID;
        string Company;
        string Name;
        string Size;
        string Weight;
        string Screen_Size;
        string Resolution;
        string OS;
        string Processor_Name;
        string Processor_Speed;
        string Cores;
        string Memory_Size;
        string Internal_Memory;
        bool Memory_Card;
        string Max_Memory_Card_Size;
        string Camera;
        bool Flash;
        string Video_Resolution;
        string Web_Cam;
        string G;   //جيل شبكة الاتصال
        string Bluetooth;
        float Price;
        static int number = 1;

        public Tablet()
        {
            ID = number;
            number++;
        }

        public void Set(string Company1,string Name1,string Size1,string Weight1,string Screen_Size1,string Resolution1,string OS1,string Processor_Name1,string Processor_Speed1,string Cores1,string Memory_Size1,string Internal_Memory1,bool Memory_Card1,string Max_Memory_Card_Size1,string Camera1,bool Flash1,string Video_Resolution1,string Web_Cam1,string G1,string Bluetooth1,float Price1)
        {
            Company = Company1;
            Name = Name1;
            Size = Size1;
            Weight = Weight1;
            Screen_Size = Screen_Size1;
            Resolution = Resolution1;
            OS = OS1;
            Processor_Name = Processor_Name1;
            Processor_Speed = Processor_Speed1;
            Cores = Cores1;
            Memory_Size = Memory_Size1;
            Internal_Memory = Internal_Memory1;
            Memory_Card = Memory_Card1;
            Max_Memory_Card_Size = Max_Memory_Card_Size1;
            Camera = Camera1;
            Flash = Flash1;
            Video_Resolution = Video_Resolution1;
            Web_Cam = Web_Cam1;
            G = G1;
            Bluetooth = Bluetooth1;
            Price = Price1;
        }

        public string display()
        {
            return null;
        }

    }

}
