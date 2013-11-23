using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using System.Runtime.Serialization;
using System.Windows;
using System.Text;
using Windows.System.Threading;
using Windows.UI.ViewManagement;
using Windows.UI.Core;

namespace Computer_2
{
    public sealed partial class desktop : Computer_2.Common.LayoutAwarePage
    {
        string[] Your_PC = new string[20];
        int[] Your_PC_Index = new int[20];
        string current_path = Windows.Storage.ApplicationData.Current.RoamingFolder.Path + @"\Computer";
        bool Sata2 = true, Sata3 = false, PCI3 = false, PCI2 = true, islocked = false;

        public desktop()
        {
            this.InitializeComponent();
        }

        protected override void LoadState(Object navigationParameter, Dictionary<String, Object> pageState)
        {
        }

        protected override void SaveState(Dictionary<String, Object> pageState)
        {
        }

        private void pageRoot_Loaded(object sender, RoutedEventArgs e)
        {
            for (int i = 0; i < 20; i++)
                Your_PC[i] = "-";
            ComboBox1.IsEnabled = true;
            Window.Current.SizeChanged += this.WindowSizeChanged;
            Initialisation();
        }

        private void Clear(int level)
        {
            Button1.IsEnabled = Button3.IsEnabled = !(islocked = true);
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
                ComboBox14.IsEnabled = ComboBox15.IsEnabled = ComboBox16.IsEnabled = ComboBox17.IsEnabled = ComboBox18.IsEnabled = ComboBox19.IsEnabled = false;
                ComboBox14.Items.Clear();
                ComboBox15.Items.Clear();
                ComboBox16.Items.Clear();
                ComboBox17.Items.Clear();
                ComboBox18.Items.Clear();
                ComboBox19.Items.Clear();
            }
            else
            {
                islocked = false;
                return;
            }
            level = level / 2;
            if (level % 2 == 0)
            {
                ComboBox13.IsEnabled = false;
                ComboBox13.Items.Clear();
            }
            else
            {
                islocked = false;
                return;
            }
            level = level / 2;
            if (level % 2 == 0)
            {
                ComboBox12.IsEnabled = false;
                ComboBox12.Items.Clear();
            }
            else
            {
                islocked = false;
                return;
            }
            level = level / 2;
            if (level % 2 == 0)
            {
                ComboBox11.IsEnabled = false;
                ComboBox11.Items.Clear();
            }
            else
            {
                islocked = false;
                return;
            }
            level = level / 2;
            if (level % 2 == 0)
            {
                ComboBox10.IsEnabled = false;
                ComboBox10.Items.Clear();
            }
            else
            {
                islocked = false;
                return;
            }
            level = level / 2;
            if (level % 2 == 0)
            {
                ComboBox9.IsEnabled = false;
                ComboBox9.Items.Clear();
            }
            else
            {
                islocked = false;
                return;
            }
            level = level / 2;
            if (level % 2 == 0)
            {
                ComboBox8.IsEnabled = false;
                ComboBox8.Items.Clear();
            }
            else
            {
                islocked = false;
                return;
            }
            level = level / 2;
            if (level % 2 == 0)
            {
                ComboBox7.IsEnabled = false;
                ComboBox7.Items.Clear();
            }
            else
            {
                islocked = false;
                return;
            }
            level = level / 2;
            if (level % 2 == 0)
            {
                ComboBox6.IsEnabled = false;
                ComboBox6.Items.Clear();
            }
            else
            {
                islocked = false;
                return;
            }
            level = level / 2;
            if (level % 2 == 0)
            {
                ComboBox5.IsEnabled = false;
                ComboBox5.Items.Clear();
            }
            else
            {
                islocked = false;
                return;
            }
            level = level / 2;
            if (level % 2 == 0)
            {
                ComboBox4.IsEnabled = false;
                ComboBox4.Items.Clear();
            }
            else
            {
                islocked = false;
                return;
            }
            level = level / 2;
            if (level % 2 == 0)
            {
                ComboBox3.IsEnabled = false;
                ComboBox3.Items.Clear();
            }
            else
            {
                islocked = false;
                return;
            }
            level = level / 2;
            if (level % 2 == 0)
            {
                ComboBox2.IsEnabled = false;
                ComboBox2.Items.Clear();
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

        private void ComboBox1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (!islocked)
            {
                Clear(8192);
                ComboBoxItem x = new ComboBoxItem();
                x = (ComboBoxItem)ComboBox1.SelectedItem;
                string type = x.Content.ToString();
                Your_PC[0] = type;
                ComboBox2.Items.Add("ASRock");
                ComboBox2.Items.Add("ASUS");
                ComboBox2.Items.Add("Foxconn");
                ComboBox2.Items.Add("GigaByte");
                ComboBox2.Items.Add("MSI");
                ComboBox2.IsEnabled = true;
            }
        }

        private void ComboBox2_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (!islocked)
            {
                Clear(4096);
                string x = (String)ComboBox2.SelectedItem;
                Your_PC[1] = x;
                for (int i = 0; i < ItemsPage.MotherBoards.Count; i++)
                    if (ItemsPage.MotherBoards[i].Kind.ToLower().Contains(Your_PC[0].ToLower()) && ItemsPage.MotherBoards[i].Company.ToLower().Contains(Your_PC[1].ToLower()))
                        ComboBox3.Items.Add(ItemsPage.MotherBoards[i].Name);
                ComboBox3.IsEnabled = true;
            }
        }

        private void ComboBox3_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (!islocked)
            {
                List<string> list = new List<string>();
                Clear(2048);
                string x = (String)ComboBox3.SelectedItem;
                Your_PC[2] = x;
                Your_PC_Index[0] = Functions.Get_Index("MB", Your_PC[2]);
                for (int i = 0; i < ItemsPage.Processors.Count; i++)
                    if (ItemsPage.Processors[i].Company.Contains(Your_PC[0]) && Functions.Suffix(ItemsPage.Processors[i].Socket, (ItemsPage.MotherBoards[Your_PC_Index[0]].Socket)) && (!Functions.item_Existing(list, ItemsPage.Processors[i].Kind)))
                    {
                        ComboBox4.Items.Add(ItemsPage.Processors[i].Kind);
                        list.Add(ItemsPage.Processors[i].Kind);
                    }
                ComboBox4.IsEnabled = true;
            }
        }

        private void ComboBox4_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (!islocked)
            {
                Clear(1024);
                string x = (String)ComboBox4.SelectedItem;
                Your_PC[3] = x;
                for (int i = 0; i < ItemsPage.Processors.Count; i++)
                    if (ItemsPage.Processors[i].Kind.ToLower().Contains(Your_PC[3].ToLower()) && Functions.Suffix(ItemsPage.Processors[i].Socket, (ItemsPage.MotherBoards[Your_PC_Index[0]].Socket)))
                        ComboBox5.Items.Add(ItemsPage.Processors[i].Name);
                ComboBox5.IsEnabled = true;
            }
        }

        private void ComboBox5_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (!islocked)
            {
                Clear(512);
                string x = (String)ComboBox5.SelectedItem;
                Your_PC[4] = x;
                Your_PC_Index[1] = Functions.Get_Index("Processor", Your_PC[4]);
                ComboBox6.Items.Add("ADATA");
                ComboBox6.Items.Add("Ketron");
                ComboBox6.Items.Add("Kingston");
                ComboBox6.Items.Add("Other (Unfamous Company)");
                ComboBox6.Items.Add("ZEPPLEIN");
                ComboBox6.IsEnabled = true;
                /*
                 * Here to put memory conditions;
                 * for (int i = 0; i < Memory.Count; i++)
                 *   if (ItemsPage.MotherBoards[i].Type.ToLower().Contains(Your_PC[0].ToLower()) && ItemsPage.MotherBoards[i].Company.ToLower().Contains(Your_PC[1].ToLower()))
                 *        ComboBox3.Items.Add(ItemsPage.MotherBoards[i].Name);
                 */
            }
        }

        private void ComboBox6_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (!islocked)
            {
                Clear(256);
                string x = (String)ComboBox6.SelectedItem;
                Your_PC[5] = x;
                ComboBox7.Items.Add("1 GB");
                ComboBox7.Items.Add("2 GB");
                ComboBox7.Items.Add("3 GB");
                ComboBox7.Items.Add("4 GB");
                ComboBox7.IsEnabled = true;
                /*
                 * Here to put memory conditions;
                 * for (int i = 0; i < Memory.Count; i++)
                 *   if (ItemsPage.MotherBoards[i].Type.ToLower().Contains(Your_PC[0].ToLower()) && ItemsPage.MotherBoards[i].Company.ToLower().Contains(Your_PC[1].ToLower()))
                 *        ComboBox3.Items.Add(ItemsPage.MotherBoards[i].Name);
                 */
            }
        }

        private void ComboBox7_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (!islocked)
            {
                Clear(128);
                string x = (String)ComboBox7.SelectedItem;
                Your_PC[6] = x;
                ComboBox8.Items.Add("Hard Disk Drive");
                ComboBox8.Items.Add("Solid State Drive");
                ComboBox8.IsEnabled = true;
            }
        }

        private void ComboBox8_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (!islocked)
            {
                Clear(64);
                string tmp = null;
                List<string> list = new List<string>();
                string x = (String)ComboBox8.SelectedItem;
                tmp = Functions.bool2string((x.Equals("Solid State Drive")), "SSD", "HDD");
                Your_PC[7] = tmp;
                Sata3 = (ItemsPage.MotherBoards[Your_PC_Index[0]].Sata == 3) || (ItemsPage.MotherBoards[Your_PC_Index[0]].Sata == 5);
                Sata2 = (ItemsPage.MotherBoards[Your_PC_Index[0]].Sata == 2) || (ItemsPage.MotherBoards[Your_PC_Index[0]].Sata == 5);
                if (Sata2)
                {
                    list.Clear();
                    for (int i = 0; i < ItemsPage.Hard_Disks.Count; i++)
                        if (ItemsPage.Hard_Disks[i].Kind.Contains(tmp))
                            if (!Functions.item_Existing(list, ItemsPage.Hard_Disks[i].Company))
                            {
                                ComboBox9.Items.Add(String.Concat(ItemsPage.Hard_Disks[i].Company, " (Sata 2)"));
                                list.Add(ItemsPage.Hard_Disks[i].Company);
                            }
                }
                if (Sata3)
                {
                    list.Clear();
                    for (int i = 0; i < ItemsPage.Hard_Disks.Count; i++)
                        if (ItemsPage.Hard_Disks[i].Kind.Contains(tmp))
                            if (!Functions.item_Existing(list, ItemsPage.Hard_Disks[i].Company))
                            {
                                ComboBox9.Items.Add(String.Concat(ItemsPage.Hard_Disks[i].Company, " (Sata 3)"));
                                list.Add(ItemsPage.Hard_Disks[i].Company);
                            }
                }
                ComboBox9.IsEnabled = true;
            }
        }

        private void ComboBox9_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (!islocked)
            {
                Clear(32);
                string x = (String)ComboBox9.SelectedItem;
                Your_PC[8] = Functions.Company(x);
                string Sata;
                Sata = Functions.bool2string(x.Contains("Sata 2"), "Sata 2", "Sata 3");
                for (int i = 0; i < ItemsPage.Hard_Disks.Count; i++)
                    if ((ItemsPage.Hard_Disks[i].Kind.ToLower().Contains(Your_PC[7].ToLower())) && (ItemsPage.Hard_Disks[i].Company.ToLower().Contains(Your_PC[8].ToLower())) && (ItemsPage.Hard_Disks[i].Socket.ToLower().Contains(Sata.ToLower())))
                        ComboBox10.Items.Add(Functions.Remove_Hard_Drive(ItemsPage.Hard_Disks[i].Name));
                ComboBox10.IsEnabled = true;
            }
        }

        private void ComboBox10_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (!islocked)
            {
                Clear(16);
                string x = (String)ComboBox10.SelectedItem;
                Your_PC[9] = x;
                Your_PC_Index[2] = Functions.Get_Index("HD", Your_PC[9]);
                if (ItemsPage.MotherBoards[Your_PC_Index[0]].Internal_Graphic)
                {
                    if (ItemsPage.MotherBoards[Your_PC_Index[0]].Graphic_Processor.Contains("Processor") && ItemsPage.Processors[Your_PC_Index[1]].Internal_Graphic)
                        ComboBox11.Items.Add("Build-in GPU");
                    else if (!ItemsPage.MotherBoards[Your_PC_Index[0]].Graphic_Processor.Contains("Processor"))
                        ComboBox11.Items.Add("Build-in GPU");
                }
                else if (ItemsPage.Processors[Your_PC_Index[1]].Internal_Graphic)
                    ComboBox11.Items.Add("Build-in GPU");
                ComboBox11.Items.Add("AMD");
                ComboBox11.Items.Add("Nvidia");
                ComboBox11.IsEnabled = true;
            }
        }

        private void ComboBox11_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (!islocked)
            {
                Clear(8);
                string x = (String)ComboBox11.SelectedItem;
                Your_PC[10] = x;
                if (x.ToLower().Contains("GPU".ToLower()))
                {
                    Your_PC_Index[3] = -1;
                    ComboBox12.IsEnabled = false;
                    ComboBox13.IsEnabled = false;
                    //Calculate how much power you will need for your PC;
                    ComboBox14.Items.Add("??? Watt Power Supply");
                    ComboBox14.IsEnabled = true;
                    Your_PC[11] = "0";
                    Your_PC[12] = "0";
                    return;
                }
                else
                {
                    Your_PC_Index[3] = 0;
                    List<string> list = new List<string>();
                    PCI2 = (ItemsPage.MotherBoards[Your_PC_Index[0]].PCIe == 2) || (ItemsPage.MotherBoards[Your_PC_Index[0]].PCIe == 5);
                    PCI3 = (ItemsPage.MotherBoards[Your_PC_Index[0]].PCIe == 3) || (ItemsPage.MotherBoards[Your_PC_Index[0]].PCIe == 5);
                    if (PCI3)
                    {
                        list.Clear();
                        ComboBox12.Items.Add("ASUS");
                        if (Your_PC[10].Equals("Nvidia"))
                            ComboBox12.Items.Add("EVGA");
                        ComboBox12.Items.Add("Gigabyte");
                        ComboBox12.Items.Add("MSI");
                        if (Your_PC[10].Equals("AMD"))
                            ComboBox12.Items.Add("Sapphirete");
                    }
                    else if (PCI2)
                    {
                        list.Clear();
                        for (int i = 0; i < ItemsPage.Graphigc_Cards.Count; i++)
                            if (Functions.Contains(ItemsPage.Graphigc_Cards[i].Socket, "x16", "2.0") && ItemsPage.Graphigc_Cards[i].Kind.ToLower().Equals(Your_PC[10].ToLower()))
                                if (!Functions.item_Existing(list, ItemsPage.Graphigc_Cards[i].Company))
                                {
                                    ComboBox12.Items.Add(ItemsPage.Graphigc_Cards[i].Company);
                                    list.Add(ItemsPage.Graphigc_Cards[i].Company);
                                }
                    }
                }
                ComboBox12.IsEnabled = true;
            }
        }

        private void ComboBox12_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (!islocked)
            {
                Clear(4);
                string x = (String)ComboBox12.SelectedItem;
                Your_PC[11] = x;
                String PCI = "";
                PCI = Functions.bool2string(PCI3, ".", "2.0");
                for (int i = 0; i < ItemsPage.Graphigc_Cards.Count; i++)
                    if ((Functions.Contains(ItemsPage.Graphigc_Cards[i].Socket, "x16", PCI)) && ItemsPage.Graphigc_Cards[i].Kind.ToLower().Equals(Your_PC[10].ToLower()) && ItemsPage.Graphigc_Cards[i].Company.ToLower().Contains(Your_PC[11].ToLower()))
                        ComboBox13.Items.Add(ItemsPage.Graphigc_Cards[i].Name + " " + Functions.PCIType(i));
                ComboBox13.IsEnabled = true;
            }
        }

        private void ComboBox13_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (!islocked)
            {
                //Calculate how much power you will need for your PC;
                Clear(2);
                string x = (String)ComboBox13.SelectedItem;
                Your_PC[12] = x;
                Your_PC_Index[3] = Functions.Get_Index("VGA", Functions.Company(Your_PC[12]));
                if (Your_PC_Index[3] == -1)
                    Your_PC_Index[3] = 0;
                ComboBox14.Items.Add("??? Watt Power Supply");
                ComboBox14.IsEnabled = true;
            }
        }

        private void ComboBox14_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (!islocked)
            {
                //Put caces that compatible with your pieces;
                Clear(1);
                string x = (String)ComboBox14.SelectedItem;
                Your_PC[13] = x;
                ComboBox15.Items.Add("Case");
                ComboBox15.IsEnabled = true;
                if (Scan_All())
                {
                    Button1.IsEnabled = true;
                    Button3.IsEnabled = true;
                }
            }
        }

        private void ComboBox15_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (!islocked)
            {
                string x = (String)ComboBox15.SelectedItem;
                Your_PC[14] = x;
                ComboBox16.Items.Add("DVD RW x24");
                ComboBox16.Items.Add("BlueRay Combo");
                ComboBox16.Items.Add("BlueRay RW");
                ComboBox16.IsEnabled = true;
                if (Scan_All())
                {
                    Button1.IsEnabled = true;
                    Button3.IsEnabled = true;
                }
            }
        }

        private void ComboBox16_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (!islocked)
            {
                string x = (String)ComboBox16.SelectedItem;
                Your_PC[15] = x;
                ComboBox17.IsEnabled = false;
                ComboBox17.Items.Clear();
                ComboBox18.IsEnabled = false;
                ComboBox18.Items.Clear();
                ComboBox19.IsEnabled = false;
                ComboBox19.Items.Clear();
                ComboBox17.Items.Add("Monitor");
                ComboBox18.Items.Add("Mouse");
                ComboBox19.Items.Add("KeyBoard");
                ComboBox17.IsEnabled = true;
                ComboBox18.IsEnabled = true;
                ComboBox19.IsEnabled = true;
                if (Scan_All())
                {
                    Button1.IsEnabled = true;
                    Button3.IsEnabled = true;
                }
            }
        }

        private void ComboBox17_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (!islocked)
                Your_PC[16] = (String)ComboBox17.SelectedItem;
        }

        private void ComboBox18_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (!islocked)
                Your_PC[17] = (String)ComboBox18.SelectedItem;
        }

        private void ComboBox19_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (!islocked)
            {
                Your_PC[18] = (String)ComboBox19.SelectedItem;
                if (Scan_All())
                {
                    Button1.IsEnabled = true;
                    Button3.IsEnabled = true;
                }
            }
        }

        private void Button1_Click(object sender, RoutedEventArgs e)
        {
            ShowSpecification();
            ApplicationView.TryUnsnap();
            if (ApplicationView.Value == ApplicationViewState.Filled)
                ScrollViewer2.Width = 700;
            if (ApplicationView.Value == ApplicationViewState.FullScreenLandscape)
                ScrollViewer2.Width = 885;
        }

        private void Button2_Click(object sender, RoutedEventArgs e)
        {
            Clear(8192);
            islocked = true;
            ComboBox1.SelectedItem = null;
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
                    Graphic_Card = Functions.bool2string((ItemsPage.MotherBoards[Your_PC_Index[0]].Graphic_Processor.ToLower().Contains("processor".ToLower())), ItemsPage.Processors[Your_PC_Index[1]].Graphic_Processor, ItemsPage.MotherBoards[Your_PC_Index[0]].Graphic_Processor);
                else
                    Graphic_Card = ItemsPage.Graphigc_Cards[Your_PC_Index[3]].Company + " " + ItemsPage.Graphigc_Cards[Your_PC_Index[3]].Name + " " + ItemsPage.Graphigc_Cards[Your_PC_Index[3]].Mem_Size;
                toSaveString = computer.ToHTML(Your_PC[3] + " " + Your_PC[4], Your_PC[1] + " " + Your_PC[2], Graphic_Card, Your_PC[5] + " " + Your_PC[6], Your_PC[8] + " " + Your_PC[9]);
                Functions.SaveFile(toSaveString, "Desktop");
                AppBar1.IsOpen = false;
            }
        }

        private void Get_Computer(out PC computer)
        {
            computer = new PC();
            computer.MB = ItemsPage.MotherBoards[Your_PC_Index[0]];
            computer.Processor = ItemsPage.Processors[Your_PC_Index[1]];
            computer.HD = ItemsPage.Hard_Disks[Your_PC_Index[2]];
            computer.RAM = new RAM();
            if (Your_PC_Index[3] > -1)
            {
                computer.VGA = ItemsPage.Graphigc_Cards[Your_PC_Index[3]];
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
            ComboBox1.SelectedIndex = Functions.GetRandomInt(ComboBox1.Items.Count);
            ComboBox2.SelectedIndex = Functions.GetRandomInt(ComboBox2.Items.Count);
            ComboBox3.SelectedIndex = Functions.GetRandomInt(ComboBox3.Items.Count);
            ComboBox4.SelectedIndex = Functions.GetRandomInt(ComboBox4.Items.Count);
            ComboBox5.SelectedIndex = Functions.GetRandomInt(ComboBox5.Items.Count);
            ComboBox6.SelectedIndex = Functions.GetRandomInt(ComboBox6.Items.Count);
            ComboBox7.SelectedIndex = Functions.GetRandomInt(ComboBox7.Items.Count);
            ComboBox8.SelectedIndex = Functions.GetRandomInt(ComboBox8.Items.Count);
            ComboBox9.SelectedIndex = Functions.GetRandomInt(ComboBox9.Items.Count);
            ComboBox10.SelectedIndex = Functions.GetRandomInt(ComboBox10.Items.Count);
            ComboBox11.SelectedIndex = Functions.GetRandomInt(ComboBox11.Items.Count);
            if (!Your_PC[10].Contains("GPU"))
            {
                ComboBox12.SelectedIndex = Functions.GetRandomInt(ComboBox12.Items.Count);
                ComboBox13.SelectedIndex = Functions.GetRandomInt(ComboBox13.Items.Count);
            }
            ComboBox14.SelectedIndex = Functions.GetRandomInt(ComboBox14.Items.Count);
            ComboBox15.SelectedIndex = Functions.GetRandomInt(ComboBox15.Items.Count);
            ComboBox16.SelectedIndex = Functions.GetRandomInt(ComboBox16.Items.Count);
            ComboBox17.SelectedIndex = Functions.GetRandomInt(ComboBox17.Items.Count);
            ComboBox18.SelectedIndex = Functions.GetRandomInt(ComboBox18.Items.Count);
            ComboBox19.SelectedIndex = Functions.GetRandomInt(ComboBox19.Items.Count);
            ShowSpecification();
            ApplicationView.TryUnsnap();
            if (ApplicationView.Value == ApplicationViewState.Filled)
                ScrollViewer2.Width = 700;
            if (ApplicationView.Value == ApplicationViewState.FullScreenLandscape)
                ScrollViewer2.Width = 885;
            AppBar1.IsOpen = false;
        }

        private void ShowSpecification()
        {
            string tmp;
            tmp = Functions.bool2string(ItemsPage.Processors[Your_PC_Index[1]].L3.Equals("-"), Functions.bool2string(ItemsPage.Processors[Your_PC_Index[1]].L2.Equals("-"), ItemsPage.Processors[Your_PC_Index[1]].L1, ItemsPage.Processors[Your_PC_Index[1]].L2), ItemsPage.Processors[Your_PC_Index[1]].L3);
            TextBlock1.Text = "Processor: " + ItemsPage.Processors[Your_PC_Index[1]].Name + " " + ItemsPage.Processors[Your_PC_Index[1]].Speed + " " + tmp;
            TextBlock2.Text = "Motherboard: " + ItemsPage.MotherBoards[Your_PC_Index[0]].Company + " " + ItemsPage.MotherBoards[Your_PC_Index[0]].Name;
            if (Your_PC[11] == "0")
            {
                TextBlock3.Text = "Graphic Card: " + Functions.bool2string((ItemsPage.MotherBoards[Your_PC_Index[0]].Graphic_Processor.ToLower().Contains("Processor".ToLower())), ItemsPage.Processors[Your_PC_Index[1]].Graphic_Processor, ItemsPage.MotherBoards[Your_PC_Index[0]].Graphic_Processor);
                ToggleSwitch1.IsOn = (ItemsPage.MotherBoards[Your_PC_Index[0]].MultiGPU != 0);
            }
            else
            {
                TextBlock3.Text = "Graphic Card: " + ItemsPage.Graphigc_Cards[Your_PC_Index[3]].Company + " " + ItemsPage.Graphigc_Cards[Your_PC_Index[3]].Name + " " + ItemsPage.Graphigc_Cards[Your_PC_Index[3]].Mem_Size;
                ToggleSwitch1.IsOn = ((ItemsPage.MotherBoards[Your_PC_Index[0]].MultiGPU != 0) && (ItemsPage.Graphigc_Cards[Your_PC_Index[3]].MultiGPU != 0));
            }
            TextBlock4.Text = "Memory: " + Your_PC[5] + " " + Your_PC[6];
            TextBlock5.Text = "Hard Disk: " + ItemsPage.Hard_Disks[Your_PC_Index[2]].Company + " " + ItemsPage.Hard_Disks[Your_PC_Index[2]].Capacity;
            ToggleSwitch2.IsOn = ItemsPage.Processors[Your_PC_Index[1]].Virtualization_Technology;
            ToggleSwitch3.IsOn = (!(ItemsPage.Processors[Your_PC_Index[1]].Max_Speed == "-"));
            TextBlock6.Text = Functions.bool2string(ToggleSwitch3.IsOn, "Up To: " + ItemsPage.Processors[Your_PC_Index[1]].Max_Speed, "");
        }

        private void WindowSizeChanged(object sender, WindowSizeChangedEventArgs e)
        {
            Initialisation();
        }

        private void Initialisation()
        {
            bool LandScape = false;
            ApplicationViewState viewState = ApplicationView.Value;
            LandScape = (viewState == ApplicationViewState.FullScreenLandscape) || (Window.Current.Bounds.Width > 1300);
            if ((viewState == ApplicationViewState.Filled) || (Window.Current.Bounds.Width < 1300))
                ScrollViewer2.Width = 700;
            List<Control> controlList;
            List<TextBlock> TextBlockList;
            if (!LandScape)
                ApplyStyle(out controlList, out TextBlockList, 50, 320, 18, 20, 380, 550, 785, 1020, 1430, 40, 14, 0, 25, 200, 40, 15, 65);
            else
                ApplyStyle(out controlList, out TextBlockList, 60, 475, 24, 28, 310, 460, 660, 860, 1180, 60, 22, 3, 0, 263, 50, 20, 50);
        }

        private void ApplyStyle(out List<Control> controlList, out List<TextBlock> TextBlockList, double Top, double Grid1Width, double ScrollViewer2FontSize, double TextBlock0iFontSize, double TextBlock02Margin, double TextBlock03Margin, double TextBlock04Margin, double Rectangle1Margin, double Button1Margin, double Button1Height, double Button1FontSize, double TextBlockListAdd, double controlListAdd, double controlListWidth, int firstAdd, int lastAdd, int add)
        {
            controlList = new List<Control>();
            TextBlockList = new List<TextBlock>();
            controlList.Add(ComboBox1); TextBlockList.Add(TextBlock_1);
            controlList.Add(ComboBox2); TextBlockList.Add(TextBlock_2);
            controlList.Add(ComboBox3); TextBlockList.Add(TextBlock_3);
            controlList.Add(ComboBox4); TextBlockList.Add(TextBlock_4);
            controlList.Add(ComboBox5); TextBlockList.Add(TextBlock_5);
            controlList.Add(ComboBox6); TextBlockList.Add(TextBlock_6);
            controlList.Add(ComboBox7); TextBlockList.Add(TextBlock_7);
            controlList.Add(ComboBox8); TextBlockList.Add(TextBlock_8);
            controlList.Add(ComboBox9); TextBlockList.Add(TextBlock_9);
            controlList.Add(ComboBox10); TextBlockList.Add(TextBlock_10);
            controlList.Add(ComboBox11); TextBlockList.Add(TextBlock_11);
            controlList.Add(ComboBox12); TextBlockList.Add(TextBlock_12);
            controlList.Add(ComboBox13); TextBlockList.Add(TextBlock_13);
            controlList.Add(ComboBox14); TextBlockList.Add(TextBlock_14);
            controlList.Add(ComboBox15); TextBlockList.Add(TextBlock_15);
            controlList.Add(ComboBox16); TextBlockList.Add(TextBlock_16);
            controlList.Add(ComboBox17); TextBlockList.Add(TextBlock_17);
            controlList.Add(ComboBox18); TextBlockList.Add(TextBlock_18);
            controlList.Add(ComboBox19); TextBlockList.Add(TextBlock_19);
            Grid1.Width = Grid1Width;
            ScrollViewer2.Margin = new Thickness(Grid1Width, 0, 0, 0);
            ScrollViewer1.FontSize = ScrollViewer2.FontSize = ToggleSwitch1.FontSize = ToggleSwitch2.FontSize = ToggleSwitch3.FontSize = ScrollViewer2FontSize;
            TextBlock01.FontSize = TextBlock02.FontSize = TextBlock03.FontSize = TextBlock04.FontSize = TextBlock0iFontSize;
            TextBlock02.Margin = new Thickness(0, TextBlock02Margin, 0, 0);
            TextBlock03.Margin = new Thickness(0, TextBlock03Margin, 0, 0);
            TextBlock04.Margin = new Thickness(0, TextBlock04Margin, 0, 0);
            Rectangle1.Margin = new Thickness(40, Rectangle1Margin, 40, 0);
            Button1.Margin = new Thickness(40, Button1Margin, 40, 0);
            Button1.Height = Button1Height;
            Button1.FontSize = Button1FontSize;
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
    }
}//812 //773