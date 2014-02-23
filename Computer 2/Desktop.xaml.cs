using Computer_2.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
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

// The Basic Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234237

namespace Computer_2
{
    /// <summary>
    /// A basic page that provides characteristics common to most applications.
    /// </summary>
    public sealed partial class Desktop : Page
    {

        string[] Your_PC = new string[20];
        int[] Your_PC_Index = new int[20];
        string current_path = Windows.Storage.ApplicationData.Current.RoamingFolder.Path + @"\Computer";
        bool Sata2 = true, Sata3 = false, PCI3 = false, PCI2 = true, islocked = false;

        private NavigationHelper navigationHelper;
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

        private void pageRoot_Loaded(object sender, RoutedEventArgs e)
        {
            for (int i = 0; i < 20; i++)
                Your_PC[i] = "-";
            pc_kind.IsEnabled = true;
            Window.Current.SizeChanged += this.WindowSizeChanged;
            Initialisation();
        }

        private void Clear(int level)
        {
            done.IsEnabled = appBar_save.IsEnabled = !(islocked = true);
            int tmp = level;
            int count = 14;
            TextBlock1.Text = "Processor:";
            TextBlock2.Text = "MotherBoard:";
            TextBlock3.Text = "Graphic Card:";
            TextBlock4.Text = "Memory:";
            TextBlock5.Text = "Hard Disk:";
            TextBlock6.Text = "Up To:";
            ToggleSwitch1.IsOn = ToggleSwitch2.IsOn = ToggleSwitch3.IsOn = false;
            while (tmp % 2 == 0)
            {
                Your_PC[count] = "-";
                tmp = tmp / 2;
                count--;
            }
            if (level % 2 == 0)
            {
                power_supply.IsEnabled = _case.IsEnabled = optical_drive.IsEnabled = monitor.IsEnabled = mouse.IsEnabled = keyboard.IsEnabled = false;
                power_supply.Items.Clear();
                _case.Items.Clear();
                optical_drive.Items.Clear();
                monitor.Items.Clear();
                mouse.Items.Clear();
                keyboard.Items.Clear();
            }
            else
            {
                islocked = false;
                return;
            }
            level = level / 2;
            if (level % 2 == 0)
            {
                gpu_model.IsEnabled = false;
                gpu_model.Items.Clear();
            }
            else
            {
                islocked = false;
                return;
            }
            level = level / 2;
            if (level % 2 == 0)
            {
                gpu_company.IsEnabled = false;
                gpu_company.Items.Clear();
            }
            else
            {
                islocked = false;
                return;
            }
            level = level / 2;
            if (level % 2 == 0)
            {
                gpu_type.IsEnabled = false;
                gpu_type.Items.Clear();
            }
            else
            {
                islocked = false;
                return;
            }
            level = level / 2;
            if (level % 2 == 0)
            {
                disk_model.IsEnabled = false;
                disk_model.Items.Clear();
            }
            else
            {
                islocked = false;
                return;
            }
            level = level / 2;
            if (level % 2 == 0)
            {
                disk_company.IsEnabled = false;
                disk_company.Items.Clear();
            }
            else
            {
                islocked = false;
                return;
            }
            level = level / 2;
            if (level % 2 == 0)
            {
                disk_type.IsEnabled = false;
                disk_type.Items.Clear();
            }
            else
            {
                islocked = false;
                return;
            }
            level = level / 2;
            if (level % 2 == 0)
            {
                ram_model.IsEnabled = false;
                ram_model.Items.Clear();
            }
            else
            {
                islocked = false;
                return;
            }
            level = level / 2;
            if (level % 2 == 0)
            {
                ram_company.IsEnabled = false;
                ram_company.Items.Clear();
            }
            else
            {
                islocked = false;
                return;
            }
            level = level / 2;
            if (level % 2 == 0)
            {
                processor.IsEnabled = false;
                processor.Items.Clear();
            }
            else
            {
                islocked = false;
                return;
            }
            level = level / 2;
            if (level % 2 == 0)
            {
                cpu_model.IsEnabled = false;
                cpu_model.Items.Clear();
            }
            else
            {
                islocked = false;
                return;
            }
            level = level / 2;
            if (level % 2 == 0)
            {
                motherboard.IsEnabled = false;
                motherboard.Items.Clear();
            }
            else
            {
                islocked = false;
                return;
            }
            level = level / 2;
            if (level % 2 == 0)
            {
                mb_company.IsEnabled = false;
                mb_company.Items.Clear();
            }
            islocked = false;
        }

        private bool Scan_All()
        {
            bool ans = true;
            for (int i = 0; i < 19; i++)
                if (Your_PC[i].Equals("-"))
                    ans = false;
            return ans;
        }

        private void PCKind_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (!islocked)
            {
                Clear(8192);
                ComboBoxItem x = new ComboBoxItem();
                x = (ComboBoxItem)pc_kind.SelectedItem;
                string type = x.Content.ToString();
                Your_PC[0] = type;
                mb_company.Items.Add("ASRock");
                mb_company.Items.Add("ASUS");
                mb_company.Items.Add("Foxconn");
                mb_company.Items.Add("GigaByte");
                mb_company.Items.Add("MSI");
                mb_company.IsEnabled = true;
            }
        }

        private void MBCompany_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (!islocked)
            {
                Clear(4096);
                string x = (String)mb_company.SelectedItem;
                Your_PC[1] = x;
                for (int i = 0; i < HubPage.MotherBoards.Count; i++)
                    if (HubPage.MotherBoards[i].Kind.ToLower().Contains(Your_PC[0].ToLower()) && HubPage.MotherBoards[i].Company.ToLower().Contains(Your_PC[1].ToLower()))
                        motherboard.Items.Add(HubPage.MotherBoards[i].Name);
                motherboard.IsEnabled = true;
            }
        }

        private void Motherboard_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (!islocked)
            {
                List<string> list = new List<string>();
                Clear(2048);
                string x = (String)motherboard.SelectedItem;
                Your_PC[2] = x;
                Your_PC_Index[0] = Functions.Get_Index("MB", Your_PC[2]);
                for (int i = 0; i < HubPage.Processors.Count; i++)
                    if (HubPage.Processors[i].Company.Contains(Your_PC[0]) && Functions.Suffix(HubPage.Processors[i].Socket, (HubPage.MotherBoards[Your_PC_Index[0]].Socket)) && (!Functions.item_Existing(list, HubPage.Processors[i].Kind)))
                    {
                        cpu_model.Items.Add(HubPage.Processors[i].Kind);
                        list.Add(HubPage.Processors[i].Kind);
                    }
                cpu_model.IsEnabled = true;
            }
        }

        private void CPUModel_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (!islocked)
            {
                Clear(1024);
                string x = (String)cpu_model.SelectedItem;
                Your_PC[3] = x;
                for (int i = 0; i < HubPage.Processors.Count; i++)
                    if (HubPage.Processors[i].Kind.ToLower().Contains(Your_PC[3].ToLower()) && Functions.Suffix(HubPage.Processors[i].Socket, (HubPage.MotherBoards[Your_PC_Index[0]].Socket)))
                        processor.Items.Add(HubPage.Processors[i].Name);
                processor.IsEnabled = true;
            }
        }

        private void Processor_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (!islocked)
            {
                Clear(512);
                string x = (String)processor.SelectedItem;
                Your_PC[4] = x;
                Your_PC_Index[1] = Functions.Get_Index("Processor", Your_PC[4]);
                ram_company.Items.Add("ADATA");
                ram_company.Items.Add("Ketron");
                ram_company.Items.Add("Kingston");
                ram_company.Items.Add("Other (Unfamous Company)");
                ram_company.Items.Add("ZEPPLEIN");
                ram_company.IsEnabled = true;
                /*
                 * Here to put memory conditions;
                 * for (int i = 0; i < Memory.Count; i++)
                 *   if (HubPage.MotherBoards[i].Type.ToLower().Contains(Your_PC[0].ToLower()) && HubPage.MotherBoards[i].Company.ToLower().Contains(Your_PC[1].ToLower()))
                 *        motherboard.Items.Add(HubPage.MotherBoards[i].Name);
                 */
            }
        }

        private void RamCompany_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (!islocked)
            {
                Clear(256);
                string x = (String)ram_company.SelectedItem;
                Your_PC[5] = x;
                ram_model.Items.Add("1 GB");
                ram_model.Items.Add("2 GB");
                ram_model.Items.Add("3 GB");
                ram_model.Items.Add("4 GB");
                ram_model.IsEnabled = true;
                /*
                 * Here to put memory conditions;
                 * for (int i = 0; i < Memory.Count; i++)
                 *   if (HubPage.MotherBoards[i].Type.ToLower().Contains(Your_PC[0].ToLower()) && HubPage.MotherBoards[i].Company.ToLower().Contains(Your_PC[1].ToLower()))
                 *        motherboard.Items.Add(HubPage.MotherBoards[i].Name);
                 */
            }
        }

        private void RamModel_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (!islocked)
            {
                Clear(128);
                string x = (String)ram_model.SelectedItem;
                Your_PC[6] = x;
                disk_type.Items.Add("Hard Disk Drive");
                disk_type.Items.Add("Solid State Drive");
                disk_type.IsEnabled = true;
            }
        }

        private void DiskType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (!islocked)
            {
                Clear(64);
                string tmp = null;
                List<string> list = new List<string>();
                string x = (String)disk_type.SelectedItem;
                tmp = Functions.bool2string((x.Equals("Solid State Drive")), "SSD", "HDD");
                Your_PC[7] = tmp;
                Sata3 = (HubPage.MotherBoards[Your_PC_Index[0]].Sata == 3) || (HubPage.MotherBoards[Your_PC_Index[0]].Sata == 5);
                Sata2 = (HubPage.MotherBoards[Your_PC_Index[0]].Sata == 2) || (HubPage.MotherBoards[Your_PC_Index[0]].Sata == 5);
                if (Sata2)
                {
                    list.Clear();
                    for (int i = 0; i < HubPage.Hard_Disks.Count; i++)
                        if (HubPage.Hard_Disks[i].Kind.Contains(tmp))
                            if (!Functions.item_Existing(list, HubPage.Hard_Disks[i].Company))
                            {
                                disk_company.Items.Add(String.Concat(HubPage.Hard_Disks[i].Company, " (Sata 2)"));
                                list.Add(HubPage.Hard_Disks[i].Company);
                            }
                }
                if (Sata3)
                {
                    list.Clear();
                    for (int i = 0; i < HubPage.Hard_Disks.Count; i++)
                        if (HubPage.Hard_Disks[i].Kind.Contains(tmp))
                            if (!Functions.item_Existing(list, HubPage.Hard_Disks[i].Company))
                            {
                                disk_company.Items.Add(String.Concat(HubPage.Hard_Disks[i].Company, " (Sata 3)"));
                                list.Add(HubPage.Hard_Disks[i].Company);
                            }
                }
                disk_company.IsEnabled = true;
            }
        }

        private void DiskCompany_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (!islocked)
            {
                Clear(32);
                string x = (String)disk_company.SelectedItem;
                Your_PC[8] = Functions.Company(x);
                string Sata;
                Sata = Functions.bool2string(x.Contains("Sata 2"), "Sata 2", "Sata 3");
                for (int i = 0; i < HubPage.Hard_Disks.Count; i++)
                    if ((HubPage.Hard_Disks[i].Kind.ToLower().Contains(Your_PC[7].ToLower())) && (HubPage.Hard_Disks[i].Company.ToLower().Contains(Your_PC[8].ToLower())) && (HubPage.Hard_Disks[i].Socket.ToLower().Contains(Sata.ToLower())))
                        disk_model.Items.Add(Functions.Remove_Hard_Drive(HubPage.Hard_Disks[i].Name));
                disk_model.IsEnabled = true;
            }
        }

        private void DiskModel_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (!islocked)
            {
                Clear(16);
                string x = (String)disk_model.SelectedItem;
                Your_PC[9] = x;
                Your_PC_Index[2] = Functions.Get_Index("HD", Your_PC[9]);
                if (HubPage.MotherBoards[Your_PC_Index[0]].Internal_Graphic)
                {
                    if (HubPage.MotherBoards[Your_PC_Index[0]].Graphic_Processor.Contains("Processor") && HubPage.Processors[Your_PC_Index[1]].Internal_Graphic)
                        gpu_type.Items.Add("Build-in GPU");
                    else if (!HubPage.MotherBoards[Your_PC_Index[0]].Graphic_Processor.Contains("Processor"))
                        gpu_type.Items.Add("Build-in GPU");
                }
                else if (HubPage.Processors[Your_PC_Index[1]].Internal_Graphic)
                    gpu_type.Items.Add("Build-in GPU");
                gpu_type.Items.Add("AMD");
                gpu_type.Items.Add("Nvidia");
                gpu_type.IsEnabled = true;
            }
        }

        private void GPUType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (!islocked)
            {
                Clear(8);
                string x = (String)gpu_type.SelectedItem;
                Your_PC[10] = x;
                if (x.ToLower().Contains("GPU".ToLower()))
                {
                    Your_PC_Index[3] = -1;
                    gpu_company.IsEnabled = false;
                    gpu_model.IsEnabled = false;
                    //Calculate how much power you will need for your PC;
                    power_supply.Items.Add("??? Watt Power Supply");
                    power_supply.IsEnabled = true;
                    Your_PC[11] = "0";
                    Your_PC[12] = "0";
                    return;
                }
                else
                {
                    Your_PC_Index[3] = 0;
                    List<string> list = new List<string>();
                    PCI2 = (HubPage.MotherBoards[Your_PC_Index[0]].PCIe == 2) || (HubPage.MotherBoards[Your_PC_Index[0]].PCIe == 5);
                    PCI3 = (HubPage.MotherBoards[Your_PC_Index[0]].PCIe == 3) || (HubPage.MotherBoards[Your_PC_Index[0]].PCIe == 5);
                    if (PCI3)
                    {
                        list.Clear();
                        gpu_company.Items.Add("ASUS");
                        if (Your_PC[10].Equals("Nvidia"))
                            gpu_company.Items.Add("EVGA");
                        gpu_company.Items.Add("Gigabyte");
                        gpu_company.Items.Add("MSI");
                        if (Your_PC[10].Equals("AMD"))
                            gpu_company.Items.Add("Sapphirete");
                    }
                    else if (PCI2)
                    {
                        list.Clear();
                        for (int i = 0; i < HubPage.Graphigc_Cards.Count; i++)
                            if (Functions.Contains(HubPage.Graphigc_Cards[i].Socket, "x16", "2.0") && HubPage.Graphigc_Cards[i].Kind.ToLower().Equals(Your_PC[10].ToLower()))
                                if (!Functions.item_Existing(list, HubPage.Graphigc_Cards[i].Company))
                                {
                                    gpu_company.Items.Add(HubPage.Graphigc_Cards[i].Company);
                                    list.Add(HubPage.Graphigc_Cards[i].Company);
                                }
                    }
                }
                gpu_company.IsEnabled = true;
            }
        }

        private void GPUCompany_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (!islocked)
            {
                Clear(4);
                string x = (String)gpu_company.SelectedItem;
                Your_PC[11] = x;
                String PCI = "";
                PCI = Functions.bool2string(PCI3, ".", "2.0");
                for (int i = 0; i < HubPage.Graphigc_Cards.Count; i++)
                    if ((Functions.Contains(HubPage.Graphigc_Cards[i].Socket, "x16", PCI)) && HubPage.Graphigc_Cards[i].Kind.ToLower().Equals(Your_PC[10].ToLower()) && HubPage.Graphigc_Cards[i].Company.ToLower().Contains(Your_PC[11].ToLower()))
                        gpu_model.Items.Add(HubPage.Graphigc_Cards[i].Name + " " + Functions.PCIType(i));
                gpu_model.IsEnabled = true;
            }
        }

        private void GPUModel_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (!islocked)
            {
                //Calculate how much power you will need for your PC;
                Clear(2);
                string x = (String)gpu_model.SelectedItem;
                Your_PC[12] = x;
                Your_PC_Index[3] = Functions.Get_Index("VGA", Functions.Company(Your_PC[12]));
                if (Your_PC_Index[3] == -1)
                    Your_PC_Index[3] = 0;
                power_supply.Items.Add("??? Watt Power Supply");
                power_supply.IsEnabled = true;
            }
        }

        private void PowerSupply_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (!islocked)
            {
                //Put caces that compatible with your pieces;
                Clear(1);
                string x = (String)power_supply.SelectedItem;
                Your_PC[13] = x;
                _case.Items.Add("Case");
                _case.IsEnabled = true;
                if (Scan_All())
                {
                    done.IsEnabled = true;
                    appBar_save.IsEnabled = true;
                }
            }
        }

        private void Case_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (!islocked)
            {
                string x = (String)_case.SelectedItem;
                Your_PC[14] = x;
                optical_drive.Items.Add("DVD RW x24");
                optical_drive.Items.Add("BlueRay Combo");
                optical_drive.Items.Add("BlueRay RW");
                optical_drive.IsEnabled = true;
                if (Scan_All())
                {
                    done.IsEnabled = true;
                    appBar_save.IsEnabled = true;
                }
            }
        }

        private void OpticalDrive_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (!islocked)
            {
                string x = (String)optical_drive.SelectedItem;
                Your_PC[15] = x;
                monitor.IsEnabled = false;
                monitor.Items.Clear();
                mouse.IsEnabled = false;
                mouse.Items.Clear();
                keyboard.IsEnabled = false;
                keyboard.Items.Clear();
                monitor.Items.Add("Monitor");
                mouse.Items.Add("Mouse");
                keyboard.Items.Add("KeyBoard");
                monitor.IsEnabled = true;
                mouse.IsEnabled = true;
                keyboard.IsEnabled = true;
                if (Scan_All())
                {
                    done.IsEnabled = true;
                    appBar_save.IsEnabled = true;
                }
            }
        }

        private void Monitor_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (!islocked)
                Your_PC[16] = (String)monitor.SelectedItem;
        }

        private void Mouse_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (!islocked)
                Your_PC[17] = (String)mouse.SelectedItem;
        }

        private void Keyboard_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (!islocked)
            {
                Your_PC[18] = (String)keyboard.SelectedItem;
                if (Scan_All())
                {
                    done.IsEnabled = true;
                    appBar_save.IsEnabled = true;
                }
            }
        }

        private void Done_Click(object sender, RoutedEventArgs e)
        {
            ShowSpecification();
        }

        private void Button2_Click(object sender, RoutedEventArgs e)
        {
            Clear(8192);
            islocked = true;
            pc_kind.SelectedItem = null;
            islocked = false;
            AppBar1.IsOpen = false;
        }

        private void Button3_Click(object sender, RoutedEventArgs e)
        {
            if (Scan_All())
            {
                PC computer;
                Get_Computer(out computer);
                string toSaveString = string.Empty;
                string Graphic_Card;
                if (Your_PC[11] == "0")
                    Graphic_Card = Functions.bool2string((HubPage.MotherBoards[Your_PC_Index[0]].Graphic_Processor.ToLower().Contains("processor".ToLower())), HubPage.Processors[Your_PC_Index[1]].Graphic_Processor, HubPage.MotherBoards[Your_PC_Index[0]].Graphic_Processor);
                else
                    Graphic_Card = HubPage.Graphigc_Cards[Your_PC_Index[3]].Company + " " + HubPage.Graphigc_Cards[Your_PC_Index[3]].Name + " " + HubPage.Graphigc_Cards[Your_PC_Index[3]].Mem_Size;
                toSaveString = computer.ToHTML(Your_PC[3] + " " + Your_PC[4], Your_PC[1] + " " + Your_PC[2], Graphic_Card, Your_PC[5] + " " + Your_PC[6], Your_PC[8] + " " + Your_PC[9]);
                Functions.SaveFile(toSaveString, "Desktop");
                AppBar1.IsOpen = false;
            }
        }

        private void Get_Computer(out PC computer)
        {
            computer = new PC();
            computer.MB = HubPage.MotherBoards[Your_PC_Index[0]];
            computer.Processor = HubPage.Processors[Your_PC_Index[1]];
            computer.HD = HubPage.Hard_Disks[Your_PC_Index[2]];
            computer.RAM = new RAM();
            if (Your_PC_Index[3] > -1)
            {
                computer.VGA = HubPage.Graphigc_Cards[Your_PC_Index[3]];
                computer.Extenal_Graphic = true;
            }
            else
            {
                computer.VGA = null;
                computer.Extenal_Graphic = false;
            }
        }

        private void Button4_Click(object sender, RoutedEventArgs e)
        {
            pc_kind.SelectedIndex = Functions.GetRandomInt(pc_kind.Items.Count);
            mb_company.SelectedIndex = Functions.GetRandomInt(mb_company.Items.Count);
            motherboard.SelectedIndex = Functions.GetRandomInt(motherboard.Items.Count);
            cpu_model.SelectedIndex = Functions.GetRandomInt(cpu_model.Items.Count);
            processor.SelectedIndex = Functions.GetRandomInt(processor.Items.Count);
            ram_company.SelectedIndex = Functions.GetRandomInt(ram_company.Items.Count);
            ram_model.SelectedIndex = Functions.GetRandomInt(ram_model.Items.Count);
            disk_type.SelectedIndex = Functions.GetRandomInt(disk_type.Items.Count);
            disk_company.SelectedIndex = Functions.GetRandomInt(disk_company.Items.Count);
            disk_model.SelectedIndex = Functions.GetRandomInt(disk_model.Items.Count);
            gpu_type.SelectedIndex = Functions.GetRandomInt(gpu_type.Items.Count);
            if (!Your_PC[10].Contains("GPU"))
            {
                gpu_company.SelectedIndex = Functions.GetRandomInt(gpu_company.Items.Count);
                gpu_model.SelectedIndex = Functions.GetRandomInt(gpu_model.Items.Count);
            }
            power_supply.SelectedIndex = Functions.GetRandomInt(power_supply.Items.Count);
            _case.SelectedIndex = Functions.GetRandomInt(_case.Items.Count);
            optical_drive.SelectedIndex = Functions.GetRandomInt(optical_drive.Items.Count);
            monitor.SelectedIndex = Functions.GetRandomInt(monitor.Items.Count);
            mouse.SelectedIndex = Functions.GetRandomInt(mouse.Items.Count);
            keyboard.SelectedIndex = Functions.GetRandomInt(keyboard.Items.Count);
            ShowSpecification();
            AppBar1.IsOpen = false;
        }

        private void ShowSpecification()
        {
            string tmp;
            tmp = Functions.bool2string(HubPage.Processors[Your_PC_Index[1]].L3.Equals("-"), Functions.bool2string(HubPage.Processors[Your_PC_Index[1]].L2.Equals("-"), HubPage.Processors[Your_PC_Index[1]].L1, HubPage.Processors[Your_PC_Index[1]].L2), HubPage.Processors[Your_PC_Index[1]].L3);
            TextBlock1.Text = "Processor: " + HubPage.Processors[Your_PC_Index[1]].Name + " " + HubPage.Processors[Your_PC_Index[1]].Speed + " " + tmp;
            TextBlock2.Text = "Motherboard: " + HubPage.MotherBoards[Your_PC_Index[0]].Company + " " + HubPage.MotherBoards[Your_PC_Index[0]].Name;
            if (Your_PC[11] == "0")
            {
                TextBlock3.Text = "Graphic Card: " + Functions.bool2string((HubPage.MotherBoards[Your_PC_Index[0]].Graphic_Processor.ToLower().Contains("Processor".ToLower())), HubPage.Processors[Your_PC_Index[1]].Graphic_Processor, HubPage.MotherBoards[Your_PC_Index[0]].Graphic_Processor);
                ToggleSwitch1.IsOn = (HubPage.MotherBoards[Your_PC_Index[0]].MultiGPU != 0);
            }
            else
            {
                TextBlock3.Text = "Graphic Card: " + HubPage.Graphigc_Cards[Your_PC_Index[3]].Company + " " + HubPage.Graphigc_Cards[Your_PC_Index[3]].Name + " " + HubPage.Graphigc_Cards[Your_PC_Index[3]].Mem_Size;
                ToggleSwitch1.IsOn = ((HubPage.MotherBoards[Your_PC_Index[0]].MultiGPU != 0) && (HubPage.Graphigc_Cards[Your_PC_Index[3]].MultiGPU != 0));
            }
            TextBlock4.Text = "Memory: " + Your_PC[5] + " " + Your_PC[6];
            TextBlock5.Text = "Hard Disk: " + HubPage.Hard_Disks[Your_PC_Index[2]].Company + " " + HubPage.Hard_Disks[Your_PC_Index[2]].Capacity;
            ToggleSwitch2.IsOn = HubPage.Processors[Your_PC_Index[1]].Virtualization_Technology;
            ToggleSwitch3.IsOn = (!(HubPage.Processors[Your_PC_Index[1]].Max_Speed == "-"));
            TextBlock6.Text = Functions.bool2string(ToggleSwitch3.IsOn, "Up To: " + HubPage.Processors[Your_PC_Index[1]].Max_Speed, "");
        }

        private void WindowSizeChanged(object sender, WindowSizeChangedEventArgs e)
        {
            Initialisation();
        }

        private void Initialisation()
        {
            bool LandScape = false;
            LandScape = (ApplicationView.Value == ApplicationViewState.FullScreenLandscape) || (Window.Current.Bounds.Width > 1300);
            List<Control> controlList;
            List<TextBlock> TextBlockList;
            if (!LandScape)
                ApplyStyle(out controlList, out TextBlockList, 50, 320, 18, 20, 380, 550, 785, 1020, 1430, 40, 14, 0, 25, 200, 40, 15, 65);
            else
                ApplyStyle(out controlList, out TextBlockList, 60, 480, 24, 28, 310, 460, 660, 860, 1180, 60, 22, 3, 0, 263, 50, 20, 50);
        }

        private void ApplyStyle(out List<Control> controlList, out List<TextBlock> TextBlockList, double Top, double Grid1Width, double ScrollViewer2FontSize, double TextBlock0iFontSize, double TextBlock02Margin, double TextBlock03Margin, double TextBlock04Margin, double Rectangle1Margin, double doneMargin, double doneHeight, double doneFontSize, double TextBlockListAdd, double controlListAdd, double controlListWidth, int firstAdd, int lastAdd, int add)
        {
            controlList = new List<Control>();
            TextBlockList = new List<TextBlock>();
            controlList.Add(pc_kind); TextBlockList.Add(TextBlock_1);
            controlList.Add(mb_company); TextBlockList.Add(TextBlock_2);
            controlList.Add(motherboard); TextBlockList.Add(TextBlock_3);
            controlList.Add(cpu_model); TextBlockList.Add(TextBlock_4);
            controlList.Add(processor); TextBlockList.Add(TextBlock_5);
            controlList.Add(ram_company); TextBlockList.Add(TextBlock_6);
            controlList.Add(ram_model); TextBlockList.Add(TextBlock_7);
            controlList.Add(disk_type); TextBlockList.Add(TextBlock_8);
            controlList.Add(disk_company); TextBlockList.Add(TextBlock_9);
            controlList.Add(disk_model); TextBlockList.Add(TextBlock_10);
            controlList.Add(gpu_type); TextBlockList.Add(TextBlock_11);
            controlList.Add(gpu_company); TextBlockList.Add(TextBlock_12);
            controlList.Add(gpu_model); TextBlockList.Add(TextBlock_13);
            controlList.Add(power_supply); TextBlockList.Add(TextBlock_14);
            controlList.Add(_case); TextBlockList.Add(TextBlock_15);
            controlList.Add(optical_drive); TextBlockList.Add(TextBlock_16);
            controlList.Add(monitor); TextBlockList.Add(TextBlock_17);
            controlList.Add(mouse); TextBlockList.Add(TextBlock_18);
            controlList.Add(keyboard); TextBlockList.Add(TextBlock_19);
            Grid1.Width = Grid1Width;
            ScrollViewer2.Margin = new Thickness(Grid1Width, 0, 0, 0);
            ScrollViewer1.FontSize = ScrollViewer2.FontSize = ToggleSwitch1.FontSize = ToggleSwitch2.FontSize = ToggleSwitch3.FontSize = ScrollViewer2FontSize;
            TextBlock01.FontSize = TextBlock02.FontSize = TextBlock03.FontSize = TextBlock04.FontSize = TextBlock0iFontSize;
            TextBlock02.Margin = new Thickness(0, TextBlock02Margin, 0, 0);
            TextBlock03.Margin = new Thickness(0, TextBlock03Margin, 0, 0);
            TextBlock04.Margin = new Thickness(0, TextBlock04Margin, 0, 0);
            Rectangle1.Margin = new Thickness(40, Rectangle1Margin, 40, 0);
            done.Margin = new Thickness(40, doneMargin, 40, 0);
            done.Height = doneHeight;
            done.FontSize = doneFontSize;
            for (int i = 0; i < TextBlockList.Count; i++)
            {
                TextBlockList[i].Margin = new Thickness(10, Top + TextBlockListAdd, 0, 0);
                controlList[i].Margin = new Thickness(0, Top + controlListAdd, 20, 0);
                controlList[i].Width = controlListWidth;
                if (i + 1 == 5) Top += firstAdd;
                if (i + 1 == 7) Top += firstAdd;
                if (i + 1 == 10) Top += firstAdd;
                if (i + 1 == 13) Top += lastAdd;
                Top += add;
            }
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
    }
}
