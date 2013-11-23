using Computer_2.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.Storage.Pickers;
using Windows.System;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;

namespace Computer_2
{
    public static class SavedFilesNumber
    {
        static private int Desktop = 0;
        static private int Laptop = 0;
        static private int Taptop = 0;

        static public int Get(string Type)
        {
            switch (Type)
            {
                case "Desktop":
                    return ++Desktop;
                case "Laptop":
                    return ++Laptop;
                case "Tablet":
                    return ++Taptop;
                default:
                    return 0;
            }
        }

        static public int Minus(string Type)
        {
            switch (Type)
            {
                case "Desktop":
                    return --Desktop;
                case "Laptop":
                    return --Laptop;
                case "Tablet":
                    return --Taptop;
                default:
                    return 0;
            }
        }
    }

    public class Device : BindableBase
    {
        private int iD;
        public int ID
        {
            get { return this.iD; }
            set { this.SetProperty(ref this.iD, value); }
        }

        protected string company;
        public string Company
        {
           // this is Manufacturer of this device(Intel,AMD) (Asus,MSI.....);
            get { return this.company; }
            set { this.SetProperty(ref this.company, value); }
        }

        private string name;
        public string Name
        {
            get { return this.name; }
            set { this.SetProperty(ref this.name, value); }
        }

        private string kind = "-";
        public string Kind
        {
            get { return this.kind; }
            set { this.SetProperty(ref this.kind, value); }
        }

        private string socket = "-";
        public string Socket
        {
            get { return this.socket; }
            set { this.SetProperty(ref this.socket, value); }
        }

        private string power = "-";
        public string Power
        {
            get { return this.power; }
            set { this.SetProperty(ref this.power, value); }
        }

        private float price = 0;
        public float Price
        {
            get { return this.price; }
            set { this.SetProperty(ref this.price, value); }
        }
    }

    public class Processor : Device
    {
        private static int number = 1;

        private float cores = 0;
        public float Cores
        {
            get { return this.cores; }
            set { this.SetProperty(ref this.cores, value); }
        }

        private float thread = 0;
        public float Thread
        {
            get { return this.thread; }
            set { this.SetProperty(ref this.thread, value); }
        }

        private string speed = "-";
        public string Speed
        {
            get { return this.speed; }
            set { this.SetProperty(ref this.speed, value); }
        }

        private string max_Speed = "-";
        public string Max_Speed
        {
            get { return this.max_Speed; }
            set { this.SetProperty(ref this.max_Speed, value); }
        }

        private string l1 = "-";
        public string L1
        {
            get { return this.l1; }
            set { this.SetProperty(ref this.l1, value); }
        }

        private string l2 = "-";
        public string L2
        {
            get { return this.l2; }
            set { this.SetProperty(ref this.l2, value); }
        }

        private string l3 = "-";
        public string L3
        {
            get { return this.l3; }
            set { this.SetProperty(ref this.l3, value); }
        }

        private string technique = "-";
        public string Technique
        {
            get { return this.technique; }
            set { this.SetProperty(ref this.technique, value); }
        }

        private string maxtmp = "-";
        public string Maxtmp
        {
            get { return this.maxtmp; }
            set { this.SetProperty(ref this.maxtmp, value); }
        }

        private bool virtualization_Technology = false;
        public bool Virtualization_Technology
        {
            get { return this.virtualization_Technology; }
            set { this.SetProperty(ref this.virtualization_Technology, value); }
        }

        private string advanced_Technologies = "-";
        public string Advanced_Technologies
        {
            get { return this.advanced_Technologies; }
            set { this.SetProperty(ref this.advanced_Technologies, value); }
        }

        private bool internal_Graphic = false;
        public bool Internal_Graphic
        {
            get { return this.internal_Graphic; }
            set { this.SetProperty(ref this.internal_Graphic, value); }
        }

        private string graphic_Processor = "-";
        public string Graphic_Processor
        {
            get { return this.graphic_Processor; }
            set { this.SetProperty(ref this.graphic_Processor, value); }
        }

        private string dMI = "-";     //سرعة الناقل
        public string DMI
        {
            get { return this.dMI; }
            set { this.SetProperty(ref this.dMI, value); }
        }

        private string instructions = "-";
        public string Instructions
        {
            get { return this.instructions; }
            set { this.SetProperty(ref this.instructions, value); }
        }

        private string max_Memory = "-";
        public string Max_Memory
        {
            get { return this.max_Memory; }
            set { this.SetProperty(ref this.max_Memory, value); }
        }

        private float memory_Channels = 0;
        public float Memory_Channels
        {
            get { return this.memory_Channels; }
            set { this.SetProperty(ref this.memory_Channels, value); }
        }

        public Processor()
        {
            ID = number;
            number++;
        }

        public string ToHTML()
        {
            int i = 8;
            string[] informations;
            string[] subTitles;
            if (Company.Contains("AMD"))
            { subTitles = new string[19]; informations = new string[19]; }
            else
            { subTitles = new string[17]; informations = new string[17]; }
            subTitles[0] = "Processor:"; informations[0] = "";
            subTitles[1] = "Kind"; informations[1] = Company;
            subTitles[2] = "Name"; informations[2] = Kind + " " + Name;
            subTitles[3] = "Socket"; informations[3] = Socket;
            subTitles[4] = "Speed"; informations[4] = Speed;
            subTitles[5] = "Number of Cores"; informations[5] = Cores.ToString();
            subTitles[6] = "Number of Thread"; informations[6] = Thread.ToString();
            if (Company.Contains("AMD"))
            {
                subTitles[7] = "Cache Level 1"; informations[7] = L1;
                subTitles[8] = "Cache Level 2"; informations[8] = L2;
                subTitles[9] = "Cache Level 3"; informations[9] = Functions.bool2string(!L3.Contains("-"), L3, "NO");
                i = 10;
            }
            else
                subTitles[7] = "Cache Memory"; informations[7] = L1;
            subTitles[i] = "Graphic Processor"; informations[i] = Functions.bool2string(Internal_Graphic, Graphic_Processor, "NO");
            subTitles[i + 1] = "Maximum Memory"; informations[i + 1] = Max_Memory;
            subTitles[i + 2] = "Power"; informations[i + 2] = Power;
            subTitles[i + 3] = "Lithography"; informations[i + 3] = Technique;
            subTitles[i + 4] = "Maximum Temperature"; informations[i + 4] = Maxtmp;
            subTitles[i + 5] = "Direct Media Interface"; informations[i + 5] = DMI;
            subTitles[i + 6] = "Instruction Set Extensions"; informations[i + 6] = Instructions;
            subTitles[i + 7] = "Number of Memory Channels"; informations[i + 7] = Functions.bool2string(Memory_Channels > 0, Memory_Channels.ToString(), "-");
            subTitles[i + 8] = "Advanced Technologies"; informations[i + 8] = Advanced_Technologies;
            return Functions.GetTable(subTitles[0], subTitles, informations);
        }

        public string ToText()
        {
            return String.Empty;
        }
    }

    public class MB : Device
    {
        private string chipset = "-";
        public string Chipset
        {
            get { return this.chipset; }
            set { this.SetProperty(ref this.chipset, value); }
        }

        private string max_Mem = "-";
        public string Max_Mem
        {
            get { return this.max_Mem; }
            set { this.SetProperty(ref this.max_Mem, value); }
        }

        private bool internal_Graphic = false;
        public bool Internal_Graphic
        {
            get { return this.internal_Graphic; }
            set { this.SetProperty(ref this.internal_Graphic, value); }
        }

        private string graphic_Processor = "-";
        public string Graphic_Processor
        {
            get { return this.graphic_Processor; }
            set { this.SetProperty(ref this.graphic_Processor, value); }
        }

        private string lan = "-";
        public string Lan
        {
            get { return this.lan; }
            set { this.SetProperty(ref this.lan, value); }
        }

        private string port = "";
        public string Port
        {
            get { return this.port; }
            set { this.SetProperty(ref this.port, value); }
        }

        private string slot = "";
        public string Slot
        {
            get { return this.slot; }
            set { this.SetProperty(ref this.slot, value); }
        }

        private float multiGPU = 0;
        public float MultiGPU
        {
            get { return this.multiGPU; }
            set { this.SetProperty(ref this.multiGPU, value); }
        }

        private float sata = 0;     //3 for Ver.3 , 2 for Ver.2 5 for Ver.2 + Ver.3;
        public float Sata
        {
            get { return this.sata; }
            set { this.SetProperty(ref this.sata, value); }
        }

        private float pCIe = 0;
        public float PCIe
        {
            get { return this.pCIe; }
            set { this.SetProperty(ref this.pCIe, value); }
        }

        private float dDR = 0;
        public float DDR
        {
            get { return this.dDR; }
            set { this.SetProperty(ref this.dDR, value); }
        }

        private string advanced_Technologies = "-";
        public string Advanced_Technologies
        {
            get { return this.advanced_Technologies; }
            set { this.SetProperty(ref this.advanced_Technologies, value); }
        }

        private string bluetooth = "-";
        public string Bluetooth
        {
            get { return this.bluetooth; }
            set { this.SetProperty(ref this.bluetooth, value); }
        }

        private string wLAN = "-";
        public string WLAN
        {
            get { return this.wLAN; }
            set { this.SetProperty(ref this.wLAN, value); }
        }

        private BitmapImage image = null;
        public BitmapImage Image
        {
            get { return image; }
            set{ this.SetProperty(ref this.image, value); }
        }

        private static int number = 1;

        public MB()
        {
            ID = number;
            number++;
        }

        public string ToHTML()
        {
            string[] informations = new string[14];
            string[] subTitles = new string[14];
            subTitles[0] = "Mother Board:"; informations[0] = "";
            subTitles[1] = "Manufacturer"; informations[1] = Company;
            subTitles[2] = "Name"; informations[2] = Name;
            subTitles[3] = "Socket"; informations[3] = Socket;
            subTitles[4] = "Chipset"; informations[4] = Chipset;
            subTitles[5] = "Graphic Processor"; informations[5] = Functions.bool2string(Internal_Graphic, Graphic_Processor, "NO");
            subTitles[6] = "Maximum Memory"; informations[6] = Max_Mem;
            subTitles[7] = "Power"; informations[7] = Power;
            subTitles[8] = "Lan"; informations[8] = Lan;
            subTitles[9] = "WLAN"; informations[9] = Functions.bool2string(!WLAN.Contains("-"), WLAN, "NO");
            subTitles[10] = "Bluetooth"; informations[10] = Functions.bool2string(!Bluetooth.Contains("-"), Bluetooth, "NO");
            subTitles[11] = "Slots"; informations[11] = Slot;
            subTitles[12] = "Ports"; informations[12] = Port;
            subTitles[13] = "Advanced Technologies"; informations[13] = Advanced_Technologies;
            return Functions.GetTable(subTitles[0], subTitles, informations);
        }
    }

    public class VGA : Device
    {
        private string gPU = "-";
        public string GPU
        {
            get { return this.gPU; }
            set { this.SetProperty(ref this.gPU, value); }
        }

        private string mem_Size = "-";
        public string Mem_Size
        {
            get { return this.mem_Size; }
            set { this.SetProperty(ref this.mem_Size, value); }
        }

        private string dDR_Type = "-";
        public string DDR_Type
        {
            get { return this.dDR_Type; }
            set { this.SetProperty(ref this.dDR_Type, value); }
        }

        private string mem_Channel = "-";
        public string Mem_Channel
        {
            get { return this.mem_Channel; }
            set { this.SetProperty(ref this.mem_Channel, value); }
        }

        private string processor_Speed = "-";
        public string Processor_Speed
        {
            get { return this.processor_Speed; }
            set { this.SetProperty(ref this.processor_Speed, value); }
        }

        private string mem_Speed = "-";
        public string Mem_Speed
        {
            get { return this.mem_Speed; }
            set { this.SetProperty(ref this.mem_Speed, value); }
        }

        private string max_Resolution = "-";
        public string Max_Resolution
        {
            get { return this.max_Resolution; }
            set { this.SetProperty(ref this.max_Resolution, value); }
        }

        private string directX_Version = "-";
        public string DirectX_Version
        {
            get { return this.directX_Version; }
            set { this.SetProperty(ref this.directX_Version, value); }
        }

        private string openGL_Version = "-";
        public string OpenGL_Version
        {
            get { return this.openGL_Version; }
            set { this.SetProperty(ref this.openGL_Version, value); }
        }

        private string pixel_Shader_Version = "-";
        public string Pixel_Shader_Version
        {
            get { return this.pixel_Shader_Version; }
            set { this.SetProperty(ref this.pixel_Shader_Version, value); }
        }

        private string cores = "-";
        public string Cores
        {
            get { return this.cores; }
            set { this.SetProperty(ref this.cores, value); }
        }

        private string port = "";
        public string Port
        {
            get { return this.port; }
            set { this.SetProperty(ref this.port, value); }
        }

        private float multiGPU = 0;
        public float MultiGPU
        {
            get { return this.multiGPU; }
            set { this.SetProperty(ref this.multiGPU, value); }
        }

        private string advanced_Technologies = "-";
        public string Advanced_Technologies
        {
            get { return this.advanced_Technologies; }
            set { this.SetProperty(ref this.advanced_Technologies, value); }
        }

        private static int number = 1;

        public VGA()
        {
            ID = number;
            number++;
        }

        public string ToHTML()
        {
            string[] informations = new string[18];
            string[] subTitles = new string[18];
            subTitles[0] = "Graphic Card:"; informations[0] = "";
            subTitles[1] = "Type"; informations[1] = Kind;
            subTitles[2] = "Manufacturer"; informations[2] = Company;
            subTitles[3] = "Name"; informations[3] = Name;
            subTitles[4] = "Slot"; informations[4] = Socket;
            subTitles[5] = "Graphic Processor"; informations[5] = GPU;
            subTitles[6] = "Memory Size"; informations[6] = Mem_Size;
            subTitles[7] = "DDR Type"; informations[7] = DDR_Type;
            subTitles[8] = "Memory Channal"; informations[8] = Mem_Channel;
            subTitles[9] = "Processor Speed"; informations[9] = Processor_Speed;
            subTitles[10] = "Memory Speed"; informations[10] = Mem_Speed;
            subTitles[11] = "Maximum Resolution"; informations[11] = Max_Resolution;
            subTitles[12] = "DirectX Version"; informations[12] = DirectX_Version;
            subTitles[13] = "OpenGL Version"; informations[13] = OpenGL_Version;
            subTitles[14] = "Pixel Shader Version"; informations[14] = Pixel_Shader_Version;
            subTitles[15] = Functions.bool2string(Kind.Contains("AMD"), "Stream Cores", "CUDA Cores"); informations[15] = Cores;
            subTitles[16] = "Ports"; informations[16] = Port;
            subTitles[17] = "Advanced Technologies"; informations[17] = Advanced_Technologies;
            return Functions.GetTable(subTitles[0], subTitles, informations);
        }
    }

    public class HD : Device
    {
        private string code = "-";
        public string Code
        {
            get { return this.code; }
            set { this.SetProperty(ref this.code, value); }
        }

        private string capacity = "-";
        public string Capacity
        {
            get { return this.capacity; }
            set { this.SetProperty(ref this.capacity, value); }
        }

        private string rotation_Speed = "-";
        public string Rotation_Speed
        {
            get { return this.rotation_Speed; }
            set { this.SetProperty(ref this.rotation_Speed, value); }
        }

        private string buffer = "-";
        public string Buffer
        {
            get { return this.buffer; }
            set { this.SetProperty(ref this.buffer, value); }
        }

        private string max_Speed = "-";
        public string Max_Speed
        {
            get { return this.max_Speed; }
            set { this.SetProperty(ref this.max_Speed, value); }
        }

        private string read_Speed = "-";
        public string Read_Speed
        {
            get { return this.read_Speed; }
            set { this.SetProperty(ref this.read_Speed, value); }
        }

        private string write_Speed = "-";
        public string Write_Speed
        {
            get { return this.write_Speed; }
            set { this.SetProperty(ref this.write_Speed, value); }
        }

        private string weight = "-";
        public string Weight
        {
            get { return this.weight; }
            set { this.SetProperty(ref this.weight, value); }
        }

        private static int number = 1;

        public HD()
        {
            ID = number;
            number++;
        }

        public string ToHTML()
        {
            string[] informations;
            string[] subTitles;
            if (Kind.Contains("HDD"))
            { subTitles = new string[10]; informations = new string[10]; }
            else
            { subTitles = new string[12]; informations = new string[12]; }
            subTitles[0] = "Storage Device:"; informations[0] = "";
            subTitles[1] = "Type"; informations[1] = Kind;
            subTitles[2] = "Manufacturer"; informations[2] = Company;
            subTitles[3] = "Name"; informations[3] = Name;
            subTitles[4] = "Slot"; informations[4] = Socket;
            subTitles[5] = "Capacity"; informations[5] = Capacity;
            subTitles[6] = "Code"; informations[6] = Code;
            subTitles[7] = "Maximum Speed"; informations[7] = Max_Speed;
            if (Kind.Contains("HDD"))
            {
                subTitles[8] = "Rotation Speed"; informations[8] = Rotation_Speed;
                subTitles[9] = "Buffer"; informations[9] = Buffer;
            }
            else
            {
                subTitles[8] = "Read Speed"; informations[8] = Read_Speed;
                subTitles[9] = "Write Speed"; informations[9] = Write_Speed;
                subTitles[10] = "Weight"; informations[10] = Weight;
                subTitles[11] = "Power"; informations[11] = Power;
            }
            return Functions.GetTable(subTitles[0], subTitles, informations);
        }
    }

    public class RAM : Device
    {
        private static int number = 1;

        private string size;
        public string Size
        {
            get { return this.size; }
            set { this.SetProperty(ref this.size, value); }
        }

        private string voltage;
        public string Voltage
        {
            get { return this.voltage; }
            set { this.SetProperty(ref this.voltage, value); }
        }

        private string bus;
        public string Bus
        {
            get { return this.bus; }
            set { this.SetProperty(ref this.bus, value); }
        }

        private string operating_Temperature;
        public string Operating_Temperature
        {
            get { return this.operating_Temperature; }
            set { this.SetProperty(ref this.operating_Temperature, value); }
        }

        public RAM()
        {
            ID = number;
            number++;
        }

        public string ToHTML()
        {
            StringBuilder Advanced_Technologies = new StringBuilder(String.Empty);
            string[] informations = new string[9];
            string[] subTitles = new string[9];
            subTitles[0] = "Random Access Memory:"; informations[0] = "";
            subTitles[1] = "Manufacturer"; informations[1] = Company;
            subTitles[2] = "Name"; informations[2] = Name;
            subTitles[3] = "Slot"; informations[3] = Socket;
            subTitles[4] = "Size"; informations[4] = Size;
            subTitles[5] = "Bus Speed"; informations[5] = Bus;
            subTitles[6] = "Voltage"; informations[6] = Voltage;
            subTitles[7] = "Operating Temperature"; informations[7] = Operating_Temperature;
            subTitles[8] = "Power"; informations[8] = Power;
            return Functions.GetTable(subTitles[0], subTitles, informations);
        }
    }

    public class PC : BindableBase
    {
        private static int number = 1;

        private int iD;
        public int ID
        {
            get { return this.iD; }
            set { this.SetProperty(ref this.iD, value); }
        }

        private Processor processor;
        public Processor Processor
        {
            get { return this.processor; }
            set { this.SetProperty(ref this.processor, value); }
        }

        private MB mB;
        public MB MB
        {
            get { return this.mB; }
            set { this.SetProperty(ref this.mB, value); }
        }

        private HD hD;
        public HD HD
        {
            get { return this.hD; }
            set { this.SetProperty(ref this.hD, value); }
        }

        private bool extenal_Graphic;
        public bool Extenal_Graphic
        {
            get { return this.extenal_Graphic; }
            set { this.SetProperty(ref this.extenal_Graphic, value); }
        }

        private VGA vGA;
        public VGA VGA
        {
            get { return this.vGA; }
            set { this.SetProperty(ref this.vGA, value); }
        }

        private RAM rAM;
        public RAM RAM
        {
            get { return this.rAM; }
            set { this.SetProperty(ref this.rAM, value); }
        }

        public PC()
        {
            iD = number;
            number++;
        }

        public void Destruct()
        {
            number--;
        }

        public string ToHTML(string CPU, string _MB, string GPU, string _RAM, string _HD)
        {
            StringBuilder file = new StringBuilder();
            file.Append("<?xml version=\"1.0\"?>\n<html>\n    <head>\n        <meta http-equiv=\"Content-Type\" content=\"text/html; charset=unicode\" />\n        <title>Desktop Details");
            file.Append("</title>\n        <hta:application id=\"DeviceDetails\" applicationname=\"Device Details\" border=\"thick\" borderstyle=\"normal\" caption=\"yes\" contextmenu=\"no\" maximizebutton=\"yes\" minimizebutton=\"yes\" showintaskbar=\"yes\" singleinstance=\"yes\" sysmenu=\"yes\" version=\"1.0\" />\n        <style>\n            A:link{text-decoration: none;}\n            A:visited{text-decoration: none;}\n            A:hover{text-decoration: none;}\n            A:active{text-decoration: none;}\n            BODY{background-color: #FFFFFF;margin-top: 10px;margin-left: 10px;margin-bottom: 0px;margin-right: 10px;}\n            DIV{}\n            HR{width: 100%;height: 1px;color: #CCCCCC;margin-top: 5px;margin-left: 0px;direction: ltr;text-align: right;}\n            TD{font-family: Segoe UI, Segoe, Tahoma, Arial, Microsoft Sans Serif;font-size: 9pt;font-weight: normal;line-height: normal;letter-spacing: normal;color: #000000;text-align: left}\n            INPUT{font-family: Segoe UI, Segoe, Tahoma, Arial, Microsoft Sans Serif;font-size: 9pt;font-weight: normal;line-height: normal;letter-spacing: normal;color: #000000;}\n            .SubTitle{font-family: Segoe UI, Segoe, Tahoma, Arial, Microsoft Sans Serif;font-size: 9pt;font-weight: normal;line-height: normal;letter-spacing: normal;color: #1370AB;}\n            .SYSLink, .SYSLink:link, .SYSLink:visited{font-family: Segoe UI, Segoe, Tahoma, Arial, Microsoft Sans Serif;font-size: 9pt;font-weight: normal;line-height: normal;letter-spacing: normal;color: #0000FF;}\n            .SYSLink:hover, .SYSLink:active{font-family: Segoe UI, Segoe, Tahoma, Arial, Microsoft Sans Serif;font-size: 9pt;font-weight: normal;line-height: normal;letter-spacing: normal;color: #3333FF;}\n            .title {font-family: Segoe UI, Segoe, Tahoma, Arial, Microsoft Sans Serif;font-weight: normal;font-size: x-large;color: #3399FF;background-color: #FFFFFF;text-align: left}\n            .subtitle {font-family: Segoe UI, Segoe, Tahoma, Arial, Microsoft Sans Serif;font-weight: normal;font-size: medium;color: #1370AB;background-color: #FFFFFF;text-align: left;width:63px;}\n            .style1 {font-family: Segoe UI, Segoe, Tahoma, Arial, Microsoft Sans Serif;letter-spacing: normal; outline-color:#000000; line-height: normal;font-weight: bold;font-size: medium;color: #FFFFFF;background-color: #000000;text-align: left; width: 15px;}\n            .style2 {font-family: Segoe UI, Segoe, Tahoma, Arial, Microsoft Sans Serif;letter-spacing: normal;outline-color:#000000;line-height: normal;font-weight: bold;font-size: medium;color: #FFFFFF;background-color: #000000;text-align: left ;width: 500px;}\n            .style3 {font-family: Segoe UI, Segoe, Tahoma, Arial, Microsoft Sans Serif;letter-spacing: normal;outline-color:#000000;line-height: normal;font-weight: bold;font-size: medium;color: #FFFFFF;background-color: #000000;text-align: left ;width: 150px;}\n            .style4 {font-family: Segoe UI, Segoe, Tahoma, Arial, Microsoft Sans Serif;letter-spacing: normal;outline-color:#000000;line-height: normal;font-weight: normal;font-size: medium;color: #FFFFFF;background-color: #888888;text-align: left; width: 150px;}\n            .style5 {font-family: Segoe UI, Segoe, Tahoma, Arial, Microsoft Sans Serif;letter-spacing: normal;outline-color:#000000;line-height: normal;font-weight: normal;font-size: small;color: #000000;background-color: #FFFFFF;text-align: left; width: 500px;}\n            .style6 {font-family: Segoe UI, Segoe, Tahoma, Arial, Microsoft Sans Serif;letter-spacing: normal;outline-color:#000000;line-height: normal;font-weight: normal;font-size: small;color: #000000;background-color: #FFFFFF;text-align: left; width: 150px;}\n            .normaltext {font-family: Segoe UI, Segoe, Tahoma, Arial, Microsoft Sans Serif;letter-spacing: normal;outline-color:#000000;line-height: normal;font-weight: normal;font-size: small;color: #444444;background-color: #FFFFFF;text-align: left;width: 600px;}\n            .overtext {font-family: Segoe UI, Segoe, Tahoma, Arial, Microsoft Sans Serif;letter-spacing: normal;outline-color: #000000;line-height: normal;font-weight: normal;font-size: small;color: #000000;background-color: #FFFFFF;text-align: left;width: 150px;}\n            .footer {font-family: Segoe UI, Segoe, Tahoma, Arial, Microsoft Sans Serif;font-weight: normal;font-size: small;color: #888888;background-color: #FFFFFF;text-align: center ;width: 700px}\n            .auto-style1 {width: 131px;}\n        </style>\n        <script type=\"text/javascript\" language=\"javascript\">openWindow();\n            function ShowPrintDialog() { window.print(); }\n        </script>\n    </head>\n\n    <body>\n        <table border=\"0\" width=\"100%\" height=\"100%\"}; cellpadding=\"15\" cellspacing=\"0\">\n            <tr><td align=\"center\" valign=\"top\"><table border=\"0\" height=\"100%\" cellpadding=\"0\" cellspacing=\"0\" style=\"width: 700px\">\n                <tr><td align=\"center\" valign=\"top\"><table border=\"0\" height=\"100%\" cellpadding=\"0\" cellspacing=\"0\" style=\"width: 696px\" >\n                    <tr><td align=\"left\" valign=\"bottom\" width=\"100%\" class=\"title\">An addition information about your desktop");
            file.Append(":</td>\n                        <td align=\"right\" valign=\"bottom\">&nbsp;</td>\n                    </tr></table></td></tr>\n                <tr><td align=\"center\" valign=\"top\">\n                    <table border=\"0\"cellpadding=\"8\" cellspacing=\"0\" width=\"100%\" style=\"border: solid 1px #CCCCCC\">\n                        <tr><td align=\"left\" valign=\"bottom\" class=\"style1\" style=\"border-bottom: solid 1px #CCCCCC\" >Component</td>\n                            <td align=\"left\" valign=\"bottom\" class=\"style2\" style=\"border-bottom: solid 1px #CCCCCC\">Details</td>\n                            <td align=\"left\" valign=\"bottom\" class=\"style3\" style=\"border-bottom: solid 1px #CCCCCC\">Price</td></tr>\n");
            file.Append("                        <tr><td align=\"left\" valign=\"bottom\" class=\"style4\" style=\"border-bottom: solid 1px #FFFFFF; \">Mother board</td>\n                            <td align=\"left\" valign=\"bottom\" class=\"style5\" style=\"border-bottom: solid 1px #FFFFFF;\">");
            file.Append(_MB);
            file.Append("</td>\n                            <td align=\"left\" valign=\"bottom\" class=\"style6\" style=\"border-bottom: solid 1px #FFFFFF;\">");
            file.Append("10");
            file.Append("</td></tr>\n");
            file.Append("                        <tr><td align=\"left\" valign=\"bottom\" class=\"style4\" style=\"border-bottom: solid 1px #FFFFFF; \">Processor</td>\n                            <td align=\"left\" valign=\"bottom\" class=\"style5\" style=\"border-bottom: solid 1px #FFFFFF;\">");
            file.Append(CPU);
            file.Append("</td>\n                            <td align=\"left\" valign=\"bottom\" class=\"style6\" style=\"border-bottom: solid 1px #FFFFFF;\">");
            file.Append("10");
            file.Append("</td></tr>\n");
            file.Append("                        <tr><td align=\"left\" valign=\"bottom\" class=\"style4\" style=\"border-bottom: solid 1px #FFFFFF; \">Memory</td>\n                            <td align=\"left\" valign=\"bottom\" class=\"style5\" style=\"border-bottom: solid 1px #FFFFFF;\">");
            file.Append(_RAM);
            file.Append("</td>\n                            <td align=\"left\" valign=\"bottom\" class=\"style6\" style=\"border-bottom: solid 1px #FFFFFF;\">");
            file.Append("10");
            file.Append("</td></tr>\n");
            file.Append("                        <tr><td align=\"left\" valign=\"bottom\" class=\"style4\" style=\"border-bottom: solid 1px #FFFFFF; \">Graphic Card</td>\n                            <td align=\"left\" valign=\"bottom\" class=\"style5\" style=\"border-bottom: solid 1px #FFFFFF;\">");
            file.Append(GPU);
            file.Append("</td>\n                            <td align=\"left\" valign=\"bottom\" class=\"style6\" style=\"border-bottom: solid 1px #FFFFFF;\">");
            file.Append("10");
            file.Append("</td></tr>\n");
            file.Append("                        <tr><td align=\"left\" valign=\"bottom\" class=\"style4\" style=\"border-bottom: solid 1px #FFFFFF; \">Hard Disk</td>\n                            <td align=\"left\" valign=\"bottom\" class=\"style5\" style=\"border-bottom: solid 1px #FFFFFF;\">");
            file.Append(_HD);
            file.Append("</td>\n                            <td align=\"left\" valign=\"bottom\" class=\"style6\" style=\"border-bottom: solid 1px #FFFFFF;\">");
            file.Append("10");
            file.Append("</td></tr>\n</table>\n                    &nbsp\n");
            file.Append(MB.ToHTML());
            file.Append(Processor.ToHTML());
            if (Extenal_Graphic)
                file.Append(VGA.ToHTML());
            else
            {
                file.Append("                    <table border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"100%\" >\n                        <tr><td align=\"left\" valign=\"middle\" class=\"auto-style1\"><nobr><span class=\"subtitle\">Graphic Card:</span><span class=\"\">&nbsp;</span></nobr></td>\n                            <td align=\"right\" valign=\"bottom\" width=\"80%\"><hr size=\"1\" /></td></tr>\n                        <tr><td colspan=\"2\" align=\"left\" valign=\"top\">\n                            <table border=\"0\" cellpadding=\"0\" cellspacing=\"8\" width=\"100%\">\n                                <tr><td width=\"10\">&nbsp;</td>\n                                    <td align=\"left\" valign=\"middle\" class=\"overtext\">Name</td>\n                                    <td align=\"left\" valign=\"middle\" class=\"normaltext\">");
                file.Append(GPU);
                if (CPU.Contains("APU"))
                {
                    file.Append("</td></tr>\n</td></tr>\n <tr><td width=\"10\">&nbsp;</td>\n                                    <td align=\"left\" valign=\"middle\" class=\"overtext\">DirectX Version</td>\n                                    <td align=\"left\" valign=\"middle\" class=\"normaltext\">");
                    file.Append("11");
                }
                file.Append("</td></tr>\n</td></tr>\n                                </table>\n</td></tr></table>\n");
            }
            file.Append(RAM.ToHTML());
            file.Append(HD.ToHTML());
            file.Append("                    <table border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"100%\" >\n                        <tr><td align=\"left\" valign=\"middle\">&nbsp;</td>\n                            <td align=\"left\" valign=\"bottom\" width=\"100%\"><hr size=\"1\" /></td></tr>\n                        <tr><td colspan=\"2\" align=\"left\" valign=\"top\">\n                            <table border=\"0\" cellpadding=\"0\" cellspacing=\"8\" width=\"100%\">\n                                <tr><td class=\"auto-style8\">&nbsp;</td>\n                                    <td align=\"left\" valign=\"middle\" class=\"footer\">© 2013 ⋈Legend Corporation. All Right Reserved.</td>\n                                </tr></table></td></tr></table>\n                    <tr><td align=\"center\" valign=\"top\" class=\"\">\n                        <table border=\"0\" width=\"0\" height=\"100%\" cellpadding=\"0\" cellspacing=\"0\">\n                            <tr><td colspan=\"2\"><br/></td></tr>\n                            <tr><td align=\"left\" valign=\"top\" width=\"100%\">&nbsp;</td>\n                                <td align=\"right\" valign=\"top\"><input type=\"button\" value=\"Print this page\" style=\"width: 150px; height: 25px;\" onclick=\"ShowPrintDialog();\"/></td></tr></table></td></tr>\n                    </table>\n                </td>\n            </tr>\n        </table>\n    </body>\n</html>");
            return file.ToString();
        }
    }

    public class Laptop : Device
    {
        private static int number = 1;

        private string category;
        public string Category
        {
            get { return this.category; }
            set { this.SetProperty(ref category, value); }
        }

        private string weight;
        public string Weight
        {
            get { return this.weight; }
            set { this.SetProperty(ref weight, value); }
        }

        private string dimensions;
        public string Dimensions
        {
            get { return this.dimensions; }
            set { this.SetProperty(ref dimensions, value); }
        }

        private string processor;
        public string Processor
        {
            get { return this.processor; }
            set { this.SetProperty(ref this.processor, value); }
        }

        private string processor_Name;
        public string Processor_Name
        {
            get { return this.processor_Name; }
            set { this.SetProperty(ref this.processor_Name, value); }
        }

        private string processor_Speed;
        public string Processor_Speed
        {
            get { return this.processor_Speed; }
            set { this.SetProperty(ref this.processor_Speed, value); }
        }

        private string processor_Max_Speed;
        public string Processor_Max_Speed
        {
            get { return this.processor_Max_Speed; }
            set { this.SetProperty(ref this.processor_Max_Speed, value); }
        }

        private string chipset;
        public string Chipset
        {
            get { return this.chipset; }
            set { this.SetProperty(ref this.chipset, value); }
        }

        private string cashe_Memory;
        public string Cashe_Memory
        {
            get { return this.cashe_Memory; }
            set { this.SetProperty(ref this.cashe_Memory, value); }
        }

        private string rAM;
        public string RAM
        {
            get { return this.rAM; }
            set { this.SetProperty(ref this.rAM, value); }
        }

        private string hard_Disk;
        public string Hard_Disk
        {
            get { return this.hard_Disk; }
            set { this.SetProperty(ref this.hard_Disk, value); }
        }

        private string hard_Disk_Rotation;
        public string Hard_Disk_Rotation
        {
            get { return this.hard_Disk_Rotation; }
            set { this.SetProperty(ref this.hard_Disk_Rotation, value); }
        }

        private string optical_Driver;
        public string Optical_Driver
        {
            get { return this.optical_Driver; }
            set { this.SetProperty(ref this.optical_Driver, value); }
        }

        private string graphic_Card;
        public string Graphic_Card
        {
            get { return this.graphic_Card; }
            set { this.SetProperty(ref this.graphic_Card, value); }
        }

        private string graphic_Card_Size;
        public string Graphic_Card_Size
        {
            get { return this.graphic_Card_Size; }
            set { this.SetProperty(ref this.graphic_Card_Size, value); }
        }

        private string graphic_Card_Max_Size;
        public string Graphic_Card_Max_Size
        {
            get { return this.graphic_Card_Max_Size; }
            set { this.SetProperty(ref this.graphic_Card_Max_Size, value); }
        }

        private string camera;
        public string Camera
        {
            get { return this.camera; }
            set { this.SetProperty(ref this.camera, value); }
        }

        private string screen_Size;
        public string Screen_Size
        {
            get { return this.screen_Size; }
            set { this.SetProperty(ref this.screen_Size, value); }
        }

        private string screen_Resolution;
        public string Screen_Resolution
        {
            get { return this.screen_Resolution; }
            set { this.SetProperty(ref this.screen_Resolution, value); }
        }

        private bool touch_Screen;
        public bool Touch_Screen
        {
            get { return this.touch_Screen; }
            set { this.SetProperty(ref this.touch_Screen, value); }
        }

        private string battary;
        public string Battary
        {
            get { return this.battary; }
            set { this.SetProperty(ref battary, value); }
        }

        private string ports;
        public string Ports
        {
            get { return this.ports; }
            set { this.SetProperty(ref ports, value); }
        }

        private string oS;
        public string OS
        {
            get { return this.oS; }
            set { this.SetProperty(ref this.oS, value); }
        }

        public Laptop()
        {
            ID = number;
            number++;
        }

        public string ToHTML()
        {
            StringBuilder file = new StringBuilder();
            string[][] informations = new string[7][];
            string[][] subTitle = new string[7][];
            informations[0] = new string[3]; subTitle[0] = new string[3];
            informations[1] = new string[6]; subTitle[1] = new string[6];
            informations[2] = new string[5]; subTitle[2] = new string[5];
            informations[3] = new string[4]; subTitle[3] = new string[4];
            informations[4] = new string[4]; subTitle[4] = new string[4];
            informations[5] = new string[5]; subTitle[5] = new string[5];
            informations[6] = new string[6]; subTitle[6] = new string[6];
            subTitle[0][0] = Kind + ":"; informations[0][0] = "";
            subTitle[0][1] = "Manufacturer"; informations[0][1] = Company;
            subTitle[0][2] = "Name"; informations[0][2] = Name;
            subTitle[1][0] = "Processor:"; informations[1][0] = "";
            subTitle[1][1] = "Type"; informations[1][1] = Processor;
            subTitle[1][2] = "Name"; informations[1][2] = Processor_Name;
            subTitle[1][3] = "Speed"; informations[1][3] = Processor_Speed;
            subTitle[1][4] = "Maximum Speed"; informations[1][4] = Processor_Max_Speed;
            subTitle[1][5] = "Cache Memory"; informations[1][5] = Cashe_Memory;
            subTitle[2][0] = "Memory and Graphic Card:"; informations[2][0] = "";
            subTitle[2][1] = "RAM"; informations[2][1] = RAM;
            subTitle[2][2] = "Graphic Card"; informations[2][2] = Graphic_Card;
            subTitle[2][3] = "Graphic Card Memory"; informations[2][3] = Graphic_Card_Size;
            subTitle[2][4] = "Graphic Card Maximum Memory Size"; informations[2][4] = graphic_Card_Max_Size;
            subTitle[3][0] = "Storage Devices:"; informations[3][0] = "";
            subTitle[3][1] = "Hard Disk"; informations[3][1] = Hard_Disk;
            subTitle[3][2] = "Hard Disk Rotation"; informations[3][2] = Hard_Disk_Rotation;
            subTitle[3][3] = "Optical Driver"; informations[3][3] = Optical_Driver;
            subTitle[4][0] = "Screen:"; informations[4][0] = "";
            subTitle[4][1] = "Size"; informations[4][1] = Screen_Size;
            subTitle[4][2] = "Resolution:"; informations[4][2] = Screen_Resolution;
            subTitle[4][3] = "Touch Screen"; informations[4][3] = Functions.bool2string(Touch_Screen, "Yes", "NO");
            subTitle[5][0] = "Extra:"; informations[5][0] = "";
            subTitle[5][1] = "Camera"; informations[5][1] = Camera;
            /*
            subTitle[5][2] = "Card Reader"; informations[5][2] = Functions.bool2string(Card_Reader, "Yes", "NO");
            subTitle[5][3] = "Bluetooth"; informations[5][3] = Bluetooth;
            */
            subTitle[5][4] = "Operating System"; informations[5][4] = Functions.bool2string(!OS.Contains("-"), OS, "NO");
            subTitle[6][0] = "Ports:"; informations[6][0] = "";
            subTitle[6][1] = ports;
            /*
            subTitle[6][1] = "USP3"; informations[6][1] = USB3.ToString();
            subTitle[6][2] = "USB2"; informations[6][2] = USB2.ToString();
            subTitle[6][3] = "HDMI"; informations[6][3] = Functions.bool2string(HDMI, "Yes", "NO");
            subTitle[6][4] = "VGA"; informations[6][4] = Functions.bool2string(VGA, "Yes", "NO");
            subTitle[6][5] = "eSata"; informations[6][5] = Functions.bool2string(!eSata.Contains("-"), eSata, "NO");
            */
            file.Append(Functions.MakeFullFile(Kind, subTitle, informations));
            return file.ToString();
        }
    }

    public class TabletPC : Device
    {
        private string size;
        public string Size
        {
            get { return this.size; }
            set { this.SetProperty(ref this.size, value); }
        }

        private string weight;
        public string Weight
        {
            get { return this.weight; }
            set { this.SetProperty(ref this.weight, value); }
        }

        private string processor_Name;
        public string Processor_Name
        {
            get { return this.processor_Name; }
            set { this.SetProperty(ref this.processor_Name, value); }
        }

        private string processor_Speed;
        public string Processor_Speed
        {
            get { return this.processor_Speed; }
            set { this.SetProperty(ref this.processor_Speed, value); }
        }

        private string screen_Size;
        public string Screen_Size
        {
            get { return this.screen_Size; }
            set { this.SetProperty(ref this.screen_Size, value); }
        }

        private string screen_Resolution;
        public string Screen_Resolution
        {
            get { return this.screen_Resolution; }
            set { this.SetProperty(ref this.screen_Resolution, value); }
        }

        private string oS;
        public string OS
        {
            get { return this.oS; }
            set { this.SetProperty(ref this.oS, value); }
        }

        private string cores;
        public string Cores
        {
            get { return this.cores; }
            set { this.SetProperty(ref this.cores, value); }
        }

        private string memory_Size;
        public string Memory_Size
        {
            get { return this.memory_Size; }
            set { this.SetProperty(ref this.memory_Size, value); }
        }

        private string internal_Memory;
        public string Internal_Memory
        {
            get { return this.internal_Memory; }
            set { this.SetProperty(ref this.internal_Memory, value); }
        }

        private bool memory_Card;
        public bool Memory_Card
        {
            get { return this.memory_Card; }
            set { this.SetProperty(ref this.memory_Card, value); }
        }

        private string max_Memory_Card_Size;
        public string Max_Memory_Card_Size
        {
            get { return this.max_Memory_Card_Size; }
            set { this.SetProperty(ref this.max_Memory_Card_Size, value); }
        }

        private string camera;
        public string Camera
        {
            get { return this.camera; }
            set { this.SetProperty(ref this.camera, value); }
        }

        private bool flash;
        public bool Flash
        {
            get { return this.flash; }
            set { this.SetProperty(ref this.flash, value); }
        }

        private string video_Resolution;
        public string Video_Resolution
        {
            get { return this.video_Resolution; }
            set { this.SetProperty(ref this.video_Resolution, value); }
        }

        private string web_Cam;
        public string Web_Cam
        {
            get { return this.web_Cam; }
            set { this.SetProperty(ref this.web_Cam, value); }
        }

        private string g;
        public string G
        {
            get { return this.g; }
            set { this.SetProperty(ref this.g, value); }
        }

        private string bluetooth;
        public string Bluetooth
        {
            get { return this.bluetooth; }
            set { this.SetProperty(ref this.bluetooth, value); }
        }

        private static int number = 1;

        public string ToHTML()
        {
            StringBuilder file = new StringBuilder();
            string[][] informations = new string[6][];
            string[][] subTitle = new string[6][];
            informations[0] = new string[5]; subTitle[0] = new string[5];
            informations[1] = new string[4]; subTitle[1] = new string[4];
            informations[2] = new string[4]; subTitle[2] = new string[4];
            informations[3] = new string[3]; subTitle[3] = new string[3];
            informations[4] = new string[5]; subTitle[4] = new string[5];
            informations[5] = new string[4]; subTitle[5] = new string[4];
            subTitle[0][0] = Kind + ":"; informations[0][0] = "";
            subTitle[0][1] = "Manufacturer"; informations[0][1] = Company;
            subTitle[0][2] = "Name"; informations[0][2] = Name;
            subTitle[0][3] = "Size"; informations[0][3] = Size;
            subTitle[0][4] = "Weight"; informations[0][4] = Weight;
            subTitle[1][0] = "Processor:"; informations[1][0] = "";
            subTitle[1][1] = "Name"; informations[1][1] = Processor_Name;
            subTitle[1][2] = "Speed"; informations[1][2] = Processor_Speed;
            subTitle[1][3] = "Number of Cores"; informations[1][3] = Cores;
            subTitle[2][0] = "Memory:"; informations[2][0] = "";
            subTitle[2][1] = "RAM"; informations[2][1] = Memory_Size;
            subTitle[2][2] = "Internal Memory"; informations[2][2] = Internal_Memory;
            subTitle[2][3] = "Maximum Memory Card Size"; informations[2][3] = Max_Memory_Card_Size;
            subTitle[3][0] = "Screen:"; informations[3][0] = "";
            subTitle[3][1] = "Size"; informations[3][1] = Screen_Size;
            subTitle[3][2] = "Resolution"; informations[3][2] = Screen_Resolution;
            subTitle[4][0] = "Camera:"; informations[4][0] = "";
            subTitle[4][1] = "Main Camera"; informations[4][1] = Camera;
            subTitle[4][2] = "Flash"; informations[4][2] = Functions.bool2string(Flash, "Yes", "NO");
            subTitle[4][3] = "Video Resolution"; informations[4][3] = Video_Resolution;
            subTitle[4][4] = "Front Camera"; informations[4][4] = Web_Cam;
            subTitle[5][0] = "Extra:"; informations[5][0] = "";
            subTitle[5][1] = "Bluetooth"; informations[5][1] = Bluetooth;
            subTitle[5][2] = "Communication Network Generation"; informations[5][2] = G;
            subTitle[5][3] = "Operating System"; informations[5][3] = OS;
            file.Append(Functions.MakeFullFile(Kind, subTitle, informations));
            return file.ToString();
        }

        public TabletPC()
        {
            ID = number;
            number++;
        }
    }

    public static class Functions
    {
        static public List<string> AddToList(string value)
        {
            List<string> list = new List<string>();
            string[] items = value.Split(';');
            foreach (var i in items)
                list.Add(i);
            return list;
        }

        static public int GetIndexOfStr(List<string> SearchIn, string Str)
        {
            for (int i = 0; i < SearchIn.Count; i++)
                if (SearchIn[i].Contains(Str))
                    return i;
            return 0;
        }

        static public string First_Word(string Str)
        {
            string[] tmp = Str.Split(' ');
            return tmp[0];
        }

        static public int Get_Index(string Pos, string Std)
        {
            switch (Pos)
            {
                case "Processor":
                    for (int i = 0; i < ItemsPage.Processors.Count; i++)
                        if (ItemsPage.Processors[i].Name.Equals(Std))
                            return i;
                    break;
                case "MB":
                    for (int i = 0; i < ItemsPage.MotherBoards.Count; i++)
                        if (ItemsPage.MotherBoards[i].Name.Equals(Std))
                            return i;
                    break;
                case "VGA":
                    for (int i = 0; i < ItemsPage.Graphigc_Cards.Count; i++)
                        if (ItemsPage.Graphigc_Cards[i].Name.Equals(Std))
                            return i;
                    break;
                case "HD":
                    for (int i = 0; i < ItemsPage.Hard_Disks.Count; i++)
                        if (ItemsPage.Hard_Disks[i].Name.Contains(Std))
                            return i;
                    break;
                case "Laptop":
                    for (int i = 0; i < ItemsPage.Laptops.Count; i++)
                        if (ItemsPage.Laptops[i].Name.Equals(Std))
                            return i;
                    break;
                case "Tablet":
                    for (int i = 0; i < ItemsPage.Tablet_PCs.Count; i++)
                        if (ItemsPage.Tablet_PCs[i].Name.Equals(Std))
                            return i;
                    break;
                default:
                    break;
            }
            return -1;
        }

        static public bool Suffix(string str1, string str2)
        {
            bool ans = false;
            if (str1.Contains("775") && str2.Contains("775"))
                ans = true;
            else if (str1.Contains("1155") && str2.Contains("1155"))
                ans = true;
            else if (str1.Contains("1156") && str2.Contains("1156"))
                ans = true;
            else if (str1.Contains("1366") && str2.Contains("1366"))
                ans = true;
            else if (str1.Contains("AM3+") && str2.Contains("AM3+"))
                ans = true;
            else if (str1.Contains("AM3") && str2.Contains("AM3"))
            {
                if (!str1.Contains("AM3+") && !str2.Contains("AM3+"))
                    ans = true;
            }
            else if (str1.Contains("FM1") && str2.Contains("FM1"))
                ans = true;
            else if (str1.Contains("2011") && str2.Contains("2011"))
                ans = true;
            return ans;
        }

        static public bool item_Existing(List<string> list, string str)
        {
            bool Exist = false;
            foreach (string i in list)
                if (i.Equals(str))
                    Exist = true;
            return Exist;
        }

        static public string Company(string str)
        {
            int index = str.IndexOf('(');
            return str.Substring(0, index - 1);
        }

        static public string Remove_Hard_Drive(string str)
        {
            if (!str.Contains("Hard Drive"))
                return str;
            else
                return str.Substring(0, str.IndexOf("Hard Drive") - 1);
        }

        static public bool Contains(string scanned, string str1, string str2)
        {
            bool ans = false;
            if (scanned.ToLower().Contains(str1.ToLower()))
                if (scanned.ToLower().Contains(str2.ToLower()))
                    ans = true;
            return ans;
        }

        static public string PCIType(int i)
        {
            return bool2string(ItemsPage.Graphigc_Cards[i].Socket.Contains("3.0"), "(PCI-E x16 3.0)", bool2string(ItemsPage.Graphigc_Cards[i].Socket.Contains("2.1"), "(PCI-E x16 2.1)", "(PCI-E x16 2.0)"));
        }

        static public string bool2string(bool condition, string iftrue, string iffalse)
        {
            return condition ? iftrue : iffalse;
        }

        static public int GetRandomInt(int last)
        {
            Random Rand = new Random((int)DateTime.Now.Ticks);
            return Rand.Next(last);
        }

        static public string Combo2String(string str)
        {
            if (str.Equals("Any"))
                return "";
            else
                return str;
        }

        static public string GetColumn(string title, string information)
        {
            return "                                <tr><td width=\"10\">&nbsp;</td>\n                                    <td align=\"left\" valign=\"middle\" class=\"overtext\">" + title + "</td>\n                                    <td align=\"left\" valign=\"middle\" class=\"normaltext\">" + information + "</td></tr>";
        }

        static public string GetTable(string title,string[] subTitles, string[] informations)
        {
            StringBuilder Table = new StringBuilder(String.Empty);
            Table.Append("                    <table border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"100%\" >\n                        <tr><td align=\"left\" valign=\"middle\" class=\"auto-style1\"><nobr><span class=\"subtitle\">" + title + "</span><span class=\"\">&nbsp;</span></nobr></td>\n                            <td align=\"right\" valign=\"bottom\" width=\"80%\"><hr size=\"1\" /></td></tr>\n                        <tr><td colspan=\"2\" align=\"left\" valign=\"top\">\n                            <table border=\"0\" cellpadding=\"0\" cellspacing=\"8\" width=\"100%\">");
            for (int i = 1; i < subTitles.Length; i++)
                Table.Append(GetColumn(subTitles[i], informations[i]));
            Table.Append("								\n                    		</table>\n                    		</td>\n                    	</tr>\n                    </table>\n");
            return Table.ToString();
        }

        static public string MakeFullFile(string title, string[][] subTitles, string[][] informations)
        {
            StringBuilder File = new StringBuilder(String.Empty);
            File.Append("<?xml version=\"1.0\"?>\n<html>\n    <head>\n        <meta http-equiv=\"Content-Type\" content=\"text/html; charset=unicode\" />\n        <title>");
            File.Append(title + " Details");
            File.Append("</title>\n        <hta:application id=\"DeviceDetails\" applicationname=\"Device Details\" border=\"thick\" borderstyle=\"normal\" caption=\"yes\" contextmenu=\"no\" maximizebutton=\"yes\" minimizebutton=\"yes\" showintaskbar=\"yes\" singleinstance=\"yes\" sysmenu=\"yes\" version=\"1.0\" />\n        <style>\n            A:link{text-decoration: none;}\n            A:visited{text-decoration: none;}\n            A:hover{text-decoration: none;}\n            A:active{text-decoration: none;}\n            BODY{background-color: #FFFFFF;margin-top: 10px;margin-left: 10px;margin-bottom: 0px;margin-right: 10px;}\n            DIV{}\n            HR{width: 100%;height: 1px;color: #CCCCCC;margin-top: 5px;margin-left: 0px;direction: ltr;text-align: right;}\n            TD{font-family: Segoe UI, Segoe, Tahoma, Arial, Microsoft Sans Serif;font-size: 9pt;font-weight: normal;line-height: normal;letter-spacing: normal;color: #000000;text-align: left}\n            INPUT{font-family: Segoe UI, Segoe, Tahoma, Arial, Microsoft Sans Serif;font-size: 9pt;font-weight: normal;line-height: normal;letter-spacing: normal;color: #000000;}\n            .SubTitle{font-family: Segoe UI, Segoe, Tahoma, Arial, Microsoft Sans Serif;font-size: 9pt;font-weight: normal;line-height: normal;letter-spacing: normal;color: #1370AB;}\n            .SYSLink, .SYSLink:link, .SYSLink:visited{font-family: Segoe UI, Segoe, Tahoma, Arial, Microsoft Sans Serif;font-size: 9pt;font-weight: normal;line-height: normal;letter-spacing: normal;color: #0000FF;}\n            .SYSLink:hover, .SYSLink:active{font-family: Segoe UI, Segoe, Tahoma, Arial, Microsoft Sans Serif;font-size: 9pt;font-weight: normal;line-height: normal;letter-spacing: normal;color: #3333FF;}\n            .title {font-family: Segoe UI, Segoe, Tahoma, Arial, Microsoft Sans Serif;font-weight: normal;font-size: x-large;color: #3399FF;background-color: #FFFFFF;text-align: left}\n            .subtitle {font-family: Segoe UI, Segoe, Tahoma, Arial, Microsoft Sans Serif;font-weight: normal;font-size: medium;color: #1370AB;background-color: #FFFFFF;text-align: left;width:63px;}\n            .style1 {font-family: Segoe UI, Segoe, Tahoma, Arial, Microsoft Sans Serif;letter-spacing: normal; outline-color:#000000; line-height: normal;font-weight: bold;font-size: medium;color: #FFFFFF;background-color: #000000;text-align: left; width: 15px;}\n            .style2 {font-family: Segoe UI, Segoe, Tahoma, Arial, Microsoft Sans Serif;letter-spacing: normal;outline-color:#000000;line-height: normal;font-weight: bold;font-size: medium;color: #FFFFFF;background-color: #000000;text-align: left ;width: 500px;}\n            .style3 {font-family: Segoe UI, Segoe, Tahoma, Arial, Microsoft Sans Serif;letter-spacing: normal;outline-color:#000000;line-height: normal;font-weight: bold;font-size: medium;color: #FFFFFF;background-color: #000000;text-align: left ;width: 150px;}\n            .style4 {font-family: Segoe UI, Segoe, Tahoma, Arial, Microsoft Sans Serif;letter-spacing: normal;outline-color:#000000;line-height: normal;font-weight: normal;font-size: medium;color: #FFFFFF;background-color: #888888;text-align: left; width: 150px;}\n            .style5 {font-family: Segoe UI, Segoe, Tahoma, Arial, Microsoft Sans Serif;letter-spacing: normal;outline-color:#000000;line-height: normal;font-weight: normal;font-size: small;color: #000000;background-color: #FFFFFF;text-align: left; width: 500px;}\n            .style6 {font-family: Segoe UI, Segoe, Tahoma, Arial, Microsoft Sans Serif;letter-spacing: normal;outline-color:#000000;line-height: normal;font-weight: normal;font-size: small;color: #000000;background-color: #FFFFFF;text-align: left; width: 150px;}\n            .normaltext {font-family: Segoe UI, Segoe, Tahoma, Arial, Microsoft Sans Serif;letter-spacing: normal;outline-color:#000000;line-height: normal;font-weight: normal;font-size: small;color: #444444;background-color: #FFFFFF;text-align: left;width: 600px;}\n            .overtext {font-family: Segoe UI, Segoe, Tahoma, Arial, Microsoft Sans Serif;letter-spacing: normal;outline-color: #000000;line-height: normal;font-weight: normal;font-size: small;color: #000000;background-color: #FFFFFF;text-align: left;width: 150px;}\n            .footer {font-family: Segoe UI, Segoe, Tahoma, Arial, Microsoft Sans Serif;font-weight: normal;font-size: small;color: #888888;background-color: #FFFFFF;text-align: center ;width: 700px}\n            .auto-style1 {width: 131px;}\n        </style>\n        <script type=\"text/javascript\" language=\"javascript\">openWindow();\n            function ShowPrintDialog() { window.print(); }\n        </script>\n    </head>\n\n    ");
            File.Append("<body>\n    <table border=\"0\" width=\"100%\" height=\"100%\"}; cellpadding=\"15\" cellspacing=\"0\">\n        <tr><td align=\"center\" valign=\"top\">\n             <table border=\"0\" height=\"100%\" cellpadding=\"0\" cellspacing=\"0\" style=\"width: 700px\">\n            <tr><td align=\"center\" valign=\"top\">\n                <table border=\"0\" height=\"100%\" cellpadding=\"0\" cellspacing=\"0\" style=\"width: 696px\" >\n                <tr><td align=\"left\" valign=\"bottom\" width=\"100%\" class=\"title\">An addition information about your " + title + ":</td></tr>\n                </table>\n                </td></tr>\n                <tr><td align=\"center\" valign=\"top\"><br/>\n");
            for (int i = 0; i < subTitles.Length; i++)
                File.Append(GetTable(subTitles[i][0], subTitles[i], informations[i]));
            File.Append("		<table border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"100%\" >\n            <tr><td align=\"left\" valign=\"middle\">&nbsp;</td>\n                <td align=\"left\" valign=\"bottom\" width=\"100%\"><hr size=\"1\" /></td></tr>\n            <tr><td colspan=\"2\" align=\"left\" valign=\"top\">\n                <table border=\"0\" cellpadding=\"0\" cellspacing=\"8\" width=\"100%\">\n                    <tr><td class=\"auto-style8\">&nbsp;</td>\n                        <td align=\"left\" valign=\"middle\" class=\"footer\">© 2013 ⋈Legend Corporation. All Right Reserved.</td></tr>\n				</table></td>			</tr>\n        </table>\n            <tr><td align=\"center\" valign=\"top\" class=\"\">\n                 <table border=\"0\" width=\"0\" height=\"100%\" cellpadding=\"0\" cellspacing=\"0\">\n                     <tr><td colspan=\"2\"><br/></td></tr>\n                     <tr><td align=\"left\" valign=\"top\" width=\"100%\">&nbsp;</td>\n                         <td align=\"right\" valign=\"top\"><input type=\"button\" value=\"Print this page\" style=\"width: 150px; height: 25px;\" onclick=\"ShowPrintDialog();\"/></td>\n						 </tr></table></td></tr>\n						</td></tr> \n			</table></td></tr>\n	</table>\n</body>");
            return File.ToString();
        }

        static async public void SaveFile(string File, string Type)
        {
            if (ApplicationView.Value == ApplicationViewState.Snapped && !ApplicationView.TryUnsnap())
                throw new Exception("File picker cannot display in snapped view.");
            FileSavePicker picker = new FileSavePicker();
            picker.FileTypeChoices.Add("HTML Document", new string[] { ".htm", ".html" });
            picker.FileTypeChoices.Add("HTML Application", new string[] { ".hta" });
            picker.SuggestedFileName = "My " + Type + " (" + SavedFilesNumber.Get(Type) + ")";
            IStorageFile outputFile = await picker.PickSaveFileAsync();
            if (!(outputFile == null))
            {
                await FileIO.WriteTextAsync(outputFile, File, Windows.Storage.Streams.UnicodeEncoding.Utf16BE);
                await Launcher.LaunchFileAsync(outputFile);
            }
            else
                SavedFilesNumber.Minus(Type);
        }
    }
}//1694 //1662 //1614 //1582 //1600 //1507 //1417 //1403 //1399

//private float uSB3;
//public float USB3
//{
//    get { return this.uSB3; }
//    set { this.SetProperty(ref this.uSB3, value); }
//}

//private float uSB2;
//public float USB2
//{
//    get { return this.uSB2; }
//    set { this.SetProperty(ref this.uSB2, value); }
//}

//private bool hDMI;
//public bool HDMI
//{
//    get { return this.hDMI; }
//    set { this.SetProperty(ref this.hDMI, value); }
//}

//private bool vGA;
//public bool VGA
//{
//    get { return this.vGA; }
//    set { this.SetProperty(ref this.vGA, value); }
//}

//private string bluetooth;
//public string Bluetooth
//{
//    get { return this.bluetooth; }
//    set { this.SetProperty(ref this.bluetooth, value); }
//}

//private bool card_Reader;
//public bool Card_Reader
//{
//    get { return this.card_Reader; }
//    set { this.SetProperty(ref this.card_Reader, value); }
//}

//private string ESata;
//public string eSata
//{
//    get { return this.ESata; }
//    set { this.SetProperty(ref this.ESata, value); }
//}