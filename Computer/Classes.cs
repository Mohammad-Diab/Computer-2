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
using Computer.Common;

namespace Computer
{
    public static class SavedFilesNumber
    {
        static private int Desktop = 0;
        static private int Laptop = 0;
        static private int Taptop = 0;
        static private int Element = 0;

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
                    return ++Element;
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
                    return --Element;
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

        public virtual void Specification(out string[] subTitles, out string[] informations)
        {
            subTitles = new string[] { Kind + ":", "Manufacturer", "Name" };
            informations = new string[] { "", Company, Name };
        }

        public virtual void Specification(out string[][] subTitles, out string[][] informations)
        {
            string[] subTitle;
            string[] information;
            Specification(out subTitle, out information);
            subTitles = new string[][] { subTitle };
            informations = new string[][] { information };
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

        private string family = "-";
        public string Family
        {
            get { return this.family; }
            set { this.SetProperty(ref this.family, value); }
        }

        public Processor()
        {
            ID = number;
            number++;
        }

        public string ToHTML()
        {
            string[] informations;
            string[] subTitles;
            Specification(out subTitles, out informations);
            return General.GetTable(subTitles[0], subTitles, informations);
        }

        public override void Specification(out string[] subTitles, out string[] informations)
        {
            int i = 8;
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
                subTitles[9] = "Cache Level 3"; informations[9] = General.bool2string(!L3.Contains("-"), L3, "NO");
                i = 10;
            }
            else
                subTitles[7] = "Cache Memory"; informations[7] = L1;
            subTitles[i] = "Graphic Processor"; informations[i] = General.bool2string(Internal_Graphic, Graphic_Processor, "NO");
            subTitles[i + 1] = "Maximum Memory"; informations[i + 1] = Max_Memory;
            subTitles[i + 2] = "Power"; informations[i + 2] = Power;
            subTitles[i + 3] = "Lithography"; informations[i + 3] = Technique;
            subTitles[i + 4] = "Maximum Temperature"; informations[i + 4] = Maxtmp;
            subTitles[i + 5] = "Direct Media Interface"; informations[i + 5] = DMI;
            subTitles[i + 6] = "Instruction Set Extensions"; informations[i + 6] = Instructions;
            subTitles[i + 7] = "Number of Memory Channels"; informations[i + 7] = General.bool2string(Memory_Channels > 0, Memory_Channels.ToString(), "-");
            subTitles[i + 8] = "Advanced Technologies"; informations[i + 8] = Advanced_Technologies;
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
            set { this.SetProperty(ref this.image, value); }
        }

        private static int number = 1;

        public MB()
        {
            ID = number;
            number++;
        }

        public string ToHTML()
        {
            string[] informations;
            string[] subTitles;
            Specification(out subTitles, out informations);
            return General.GetTable(subTitles[0], subTitles, informations);
        }

        public override void Specification(out string[] subTitles, out string[] informations)
        {
            informations = new string[14];
            subTitles = new string[14];
            subTitles[0] = "Mother Board:"; informations[0] = "";
            subTitles[1] = "Manufacturer"; informations[1] = Company;
            subTitles[2] = "Name"; informations[2] = Name;
            subTitles[3] = "Socket"; informations[3] = Socket;
            subTitles[4] = "Chipset"; informations[4] = Chipset;
            subTitles[5] = "Graphic Processor"; informations[5] = General.bool2string(Internal_Graphic, Graphic_Processor, "NO");
            subTitles[6] = "Maximum Memory"; informations[6] = Max_Mem;
            subTitles[7] = "Power"; informations[7] = Power;
            subTitles[8] = "Lan"; informations[8] = Lan;
            subTitles[9] = "WLAN"; informations[9] = General.bool2string(!WLAN.Contains("-"), WLAN, "NO");
            subTitles[10] = "Bluetooth"; informations[10] = General.bool2string(!Bluetooth.Contains("-"), Bluetooth, "NO");
            subTitles[11] = "Slots"; informations[11] = Slot;
            subTitles[12] = "Ports"; informations[12] = Port;
            subTitles[13] = "Advanced Technologies"; informations[13] = Advanced_Technologies;
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
            string[] informations;
            string[] subTitles;
            Specification(out subTitles, out informations);
            return General.GetTable(subTitles[0], subTitles, informations);
        }

        public override void Specification(out string[] subTitles, out string[] informations)
        {
            informations = new string[18];
            subTitles = new string[18];
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
            subTitles[15] = General.bool2string(Kind.Contains("AMD"), "Stream Cores", "CUDA Cores"); informations[15] = Cores;
            subTitles[16] = "Ports"; informations[16] = Port;
            subTitles[17] = "Advanced Technologies"; informations[17] = Advanced_Technologies;
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
            Specification(out subTitles, out informations);
            return General.GetTable(subTitles[0], subTitles, informations);
        }

        public override void Specification(out string[] subTitles, out string[] informations)
        {
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
            string[] informations;
            string[] subTitles;
            Specification(out subTitles, out informations);
            return General.GetTable(subTitles[0], subTitles, informations);
        }

        public override void Specification(out string[] subTitles, out string[] informations)
        {
            informations = new string[9];
            subTitles = new string[9];
            subTitles[0] = "Random Access Memory:"; informations[0] = "";
            subTitles[1] = "Manufacturer"; informations[1] = Company;
            subTitles[2] = "Name"; informations[2] = Name;
            subTitles[3] = "Slot"; informations[3] = Socket;
            subTitles[4] = "Size"; informations[4] = Size;
            subTitles[5] = "Bus Speed"; informations[5] = Bus;
            subTitles[6] = "Voltage"; informations[6] = Voltage;
            subTitles[7] = "Operating Temperature"; informations[7] = Operating_Temperature;
            subTitles[8] = "Power"; informations[8] = Power;
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
            List<string[]> parts = new List<string[]>();
            parts.Add(new string[] { "Mother board", _MB, "" });
            parts.Add(new string[] { "Processor", CPU, "" });
            parts.Add(new string[] { "Memory", _RAM, "" });
            parts.Add(new string[] { "Graphic Card", GPU, "" });
            parts.Add(new string[] { "Hard Disk", _HD, "" });
            return ToHTML(parts, new List<string[]>());
        }

        public string ToHTML(List<string[]> parts, List<string[]> notes)
        {
            string CPU = Processor.Kind + " " + Processor.Name;
            string GPU = "";
            foreach (string[] i in parts)
                if (i[0].Equals("Graphic Card"))
                    GPU = i[1].Split(new string[] { "<br/>" }, StringSplitOptions.None)[0];
            StringBuilder file = new StringBuilder();
            file.Append("<?xml version=\"1.0\"?>\n<html>\n    <head>\n        <meta http-equiv=\"Content-Type\" content=\"text/html; charset=unicode\" />\n        <title>Desktop Details");
            file.Append("</title>\n        <hta:application id=\"DeviceDetails\" applicationname=\"Device Details\" border=\"thick\" borderstyle=\"normal\" caption=\"yes\" contextmenu=\"no\" maximizebutton=\"yes\" minimizebutton=\"yes\" showintaskbar=\"yes\" singleinstance=\"yes\" sysmenu=\"yes\" version=\"1.0\" />\n        <style>\n            A:link{text-decoration: none;}\n            A:visited{text-decoration: none;}\n            A:hover{text-decoration: none;}\n            A:active{text-decoration: none;}\n            BODY{background-color: #FFFFFF;margin-top: 10px;margin-left: 10px;margin-bottom: 0px;margin-right: 10px;}\n            DIV{}\n            HR{width: 100%;height: 1px;color: #CCCCCC;margin-top: 5px;margin-left: 0px;direction: ltr;text-align: right;}\n            TD{font-family: Segoe UI, Segoe, Tahoma, Arial, Microsoft Sans Serif;font-size: 9pt;font-weight: normal;line-height: normal;letter-spacing: normal;color: #000000;text-align: left}\n            INPUT{font-family: Segoe UI, Segoe, Tahoma, Arial, Microsoft Sans Serif;font-size: 9pt;font-weight: normal;line-height: normal;letter-spacing: normal;color: #000000;}\n            .SubTitle{font-family: Segoe UI, Segoe, Tahoma, Arial, Microsoft Sans Serif;font-size: 9pt;font-weight: normal;line-height: normal;letter-spacing: normal;color: #1370AB;}\n            .SYSLink, .SYSLink:link, .SYSLink:visited{font-family: Segoe UI, Segoe, Tahoma, Arial, Microsoft Sans Serif;font-size: 9pt;font-weight: normal;line-height: normal;letter-spacing: normal;color: #0000FF;}\n            .SYSLink:hover, .SYSLink:active{font-family: Segoe UI, Segoe, Tahoma, Arial, Microsoft Sans Serif;font-size: 9pt;font-weight: normal;line-height: normal;letter-spacing: normal;color: #3333FF;}\n            .title {font-family: Segoe UI, Segoe, Tahoma, Arial, Microsoft Sans Serif;font-weight: normal;font-size: x-large;color: #3399FF;background-color: #FFFFFF;text-align: left}\n            .subtitle {font-family: Segoe UI, Segoe, Tahoma, Arial, Microsoft Sans Serif;font-weight: normal;font-size: medium;color: #1370AB;background-color: #FFFFFF;text-align: left;width:63px;}\n            .style1 {font-family: Segoe UI, Segoe, Tahoma, Arial, Microsoft Sans Serif;letter-spacing: normal; outline-color:#000000; line-height: normal;font-weight: bold;font-size: medium;color: #FFFFFF;background-color: #000000;text-align: left; width: 15px;}\n            .style2 {font-family: Segoe UI, Segoe, Tahoma, Arial, Microsoft Sans Serif;letter-spacing: normal;outline-color:#000000;line-height: normal;font-weight: bold;font-size: medium;color: #FFFFFF;background-color: #000000;text-align: left ;width: 500px;}\n            .style3 {font-family: Segoe UI, Segoe, Tahoma, Arial, Microsoft Sans Serif;letter-spacing: normal;outline-color:#000000;line-height: normal;font-weight: bold;font-size: medium;color: #FFFFFF;background-color: #000000;text-align: left ;width: 150px;}\n            .style4 {font-family: Segoe UI, Segoe, Tahoma, Arial, Microsoft Sans Serif;letter-spacing: normal;outline-color:#000000;line-height: normal;font-weight: normal;font-size: medium;color: #FFFFFF;background-color: #888888;text-align: left; width: 150px;}\n            .style5 {font-family: Segoe UI, Segoe, Tahoma, Arial, Microsoft Sans Serif;letter-spacing: normal;outline-color:#000000;line-height: normal;font-weight: normal;font-size: small;color: #000000;background-color: #FFFFFF;text-align: left; width: 500px;}\n            .style6 {font-family: Segoe UI, Segoe, Tahoma, Arial, Microsoft Sans Serif;letter-spacing: normal;outline-color:#000000;line-height: normal;font-weight: normal;font-size: small;color: #000000;background-color: #FFFFFF;text-align: left; width: 150px;}\n            .normaltext {font-family: Segoe UI, Segoe, Tahoma, Arial, Microsoft Sans Serif;letter-spacing: normal;outline-color:#000000;line-height: normal;font-weight: normal;font-size: small;color: #444444;background-color: #FFFFFF;text-align: left;width: 600px;}\n            .overtext {font-family: Segoe UI, Segoe, Tahoma, Arial, Microsoft Sans Serif;letter-spacing: normal;outline-color: #000000;line-height: normal;font-weight: normal;font-size: small;color: #000000;background-color: #FFFFFF;text-align: left;width: 150px;}\n            .footer {font-family: Segoe UI, Segoe, Tahoma, Arial, Microsoft Sans Serif;font-weight: normal;font-size: small;color: #888888;background-color: #FFFFFF;text-align: center ;width: 700px}\n            .auto-style1 {width: 131px;}\n        </style>\n        <script type=\"text/javascript\" language=\"javascript\">openWindow();\n            function ShowPrintDialog() { window.print(); }\n        </script>\n    </head>\n\n    <body>\n        <table border=\"0\" width=\"100%\" height=\"100%\"}; cellpadding=\"15\" cellspacing=\"0\">\n            <tr><td align=\"center\" valign=\"top\"><table border=\"0\" height=\"100%\" cellpadding=\"0\" cellspacing=\"0\" style=\"width: 700px\">\n                <tr><td align=\"center\" valign=\"top\"><table border=\"0\" height=\"100%\" cellpadding=\"0\" cellspacing=\"0\" style=\"width: 696px\" >\n                    <tr><td align=\"left\" valign=\"bottom\" width=\"100%\" class=\"title\">An addition information about your desktop");
            file.Append(":</td>\n                        <td align=\"right\" valign=\"bottom\">&nbsp;</td>\n                    </tr></table></td></tr>\n                <tr><td align=\"center\" valign=\"top\">\n                    <table border=\"0\"cellpadding=\"8\" cellspacing=\"0\" width=\"100%\" style=\"border: solid 1px #CCCCCC\">\n                        <tr><td align=\"left\" valign=\"bottom\" class=\"style1\" style=\"border-bottom: solid 1px #CCCCCC\" >Component</td>\n                            <td align=\"left\" valign=\"bottom\" class=\"style2\" style=\"border-bottom: solid 1px #CCCCCC\">Details</td>\n                            <td align=\"left\" valign=\"bottom\" class=\"style3\" style=\"border-bottom: solid 1px #CCCCCC\">Price</td></tr>\n");
            foreach (string[] i in parts)
            {
                file.Append("                        <tr><td align=\"left\" valign=\"bottom\" class=\"style4\" style=\"border-bottom: solid 1px #FFFFFF; \">");
                file.Append(i[0]);
                file.Append("</td>\n                            <td align=\"left\" valign=\"bottom\" class=\"style5\" style=\"border-bottom: solid 1px #FFFFFF;\">");
                if (i.Length > 2 && i[2].Length > 0)
                    file.Append("<img src=\"" + i[2] + "\" width=\"64\" height=\"64\" style=\"float: left; margin-right: 8px;\" />");
                file.Append(i[1]);
                file.Append("</td>\n                            <td align=\"left\" valign=\"bottom\" class=\"style6\" style=\"border-bottom: solid 1px #FFFFFF;\">");
                file.Append("-");
                file.Append("</td></tr>\n");
            }
            file.Append("</table>\n                    &nbsp\n");
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
            if (notes.Count > 0)
            {
                string[] subTitles = new string[notes.Count + 1];
                string[] informations = new string[notes.Count + 1];
                subTitles[0] = "Compatibility:"; informations[0] = "";
                for (int i = 0; i < notes.Count; i++)
                {
                    subTitles[i + 1] = General.bool2string(notes[i][0].Equals("1"), "OK", "Check");
                    informations[i + 1] = notes[i][1];
                }
                file.Append(General.GetTable(subTitles[0], subTitles, informations));
            }
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
            string[][] informations;
            string[][] subTitle;
            Specification(out subTitle, out informations);
            file.Append(General.MakeFullFile(Kind, subTitle, informations));
            return file.ToString();
        }

        public override void Specification(out string[][] subTitle, out string[][] informations)
        {
            informations = new string[7][];
            subTitle = new string[7][];
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
            subTitle[4][3] = "Touch Screen"; informations[4][3] = General.bool2string(Touch_Screen, "Yes", "NO");
            subTitle[5][0] = "Extra:"; informations[5][0] = "";
            subTitle[5][1] = "Camera"; informations[5][1] = Camera;
            /*
            subTitle[5][2] = "Card Reader"; informations[5][2] = Functions.bool2string(Card_Reader, "Yes", "NO");
            subTitle[5][3] = "Bluetooth"; informations[5][3] = Bluetooth;
            */
            subTitle[5][4] = "Operating System"; informations[5][4] = General.bool2string(!OS.Contains("-"), OS, "NO");
            subTitle[6][0] = "Ports:"; informations[6][0] = "";
            subTitle[6][1] = "Ports"; informations[6][1] = Ports;
            /*
            subTitle[6][1] = "USP3"; informations[6][1] = USB3.ToString();
            subTitle[6][2] = "USB2"; informations[6][2] = USB2.ToString();
            subTitle[6][3] = "HDMI"; informations[6][3] = Functions.bool2string(HDMI, "Yes", "NO");
            subTitle[6][4] = "VGA"; informations[6][4] = Functions.bool2string(VGA, "Yes", "NO");
            subTitle[6][5] = "eSata"; informations[6][5] = Functions.bool2string(!eSata.Contains("-"), eSata, "NO");
            */
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
            string[][] informations;
            string[][] subTitle;
            Specification(out subTitle, out informations);
            file.Append(General.MakeFullFile(Kind, subTitle, informations));
            return file.ToString();
        }

        public override void Specification(out string[][] subTitle, out string[][] informations)
        {
            informations = new string[6][];
            subTitle = new string[6][];
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
            subTitle[4][2] = "Flash"; informations[4][2] = General.bool2string(Flash, "Yes", "NO");
            subTitle[4][3] = "Video Resolution"; informations[4][3] = Video_Resolution;
            subTitle[4][4] = "Front Camera"; informations[4][4] = Web_Cam;
            subTitle[5][0] = "Extra:"; informations[5][0] = "";
            subTitle[5][1] = "Bluetooth"; informations[5][1] = Bluetooth;
            subTitle[5][2] = "Communication Network Generation"; informations[5][2] = G;
            subTitle[5][3] = "Operating System"; informations[5][3] = OS;
        }

        public TabletPC()
        {
            ID = number;
            number++;
        }
    }

    class PCItem
    {
        public int index;
        public string item;
    }

    public static class General
    {
        public static List<Processor> Processors = new List<Processor>();
        public static List<MB> MotherBoards = new List<MB>();
        public static List<VGA> Graphigc_Cards = new List<VGA>();
        public static List<HD> Hard_Disks = new List<HD>();
        public static List<Laptop> Laptops = new List<Laptop>();
        public static List<TabletPC> Tablet_PCs = new List<TabletPC>();
        public static string current_path = Windows.Storage.ApplicationData.Current.RoamingFolder.Path + @"\Computer";

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
                    for (int i = 0; i < General.Processors.Count; i++)
                        if (General.Processors[i].Name.Equals(Std))
                            return i;
                    break;
                case "MB":
                    for (int i = 0; i < General.MotherBoards.Count; i++)
                        if (General.MotherBoards[i].Name.Equals(Std))
                            return i;
                    break;
                case "VGA":
                    for (int i = 0; i < General.Graphigc_Cards.Count; i++)
                        if (General.Graphigc_Cards[i].Name.Equals(Std))
                            return i;
                    break;
                case "HD":
                    for (int i = 0; i < General.Hard_Disks.Count; i++)
                        if (General.Hard_Disks[i].Name.Contains(Std))
                            return i;
                    break;
                case "Laptop":
                    for (int i = 0; i < General.Laptops.Count; i++)
                        if (General.Laptops[i].Name.Equals(Std))
                            return i;
                    break;
                case "Tablet":
                    for (int i = 0; i < General.Tablet_PCs.Count; i++)
                        if (General.Tablet_PCs[i].Name.Equals(Std))
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
            return bool2string(General.Graphigc_Cards[i].Socket.Contains("3.0"), "(PCI-E x16 3.0)", bool2string(General.Graphigc_Cards[i].Socket.Contains("2.1"), "(PCI-E x16 2.1)", "(PCI-E x16 2.0)"));
        }

        static public string bool2string(bool condition, string iftrue, string iffalse)
        {
            return condition ? iftrue : iffalse;
        }

        static private Random Rand = new Random((int)DateTime.Now.Ticks);

        static public int GetRandomInt(int last)
        {
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

        static public string GetTable(string title, string[] subTitles, string[] informations)
        {
            StringBuilder Table = new StringBuilder(String.Empty);
            Table.Append("                    <table border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"100%\" >\n                        <tr><td align=\"left\" valign=\"middle\" class=\"auto-style1\"><nobr><span class=\"subtitle\">" + title + "</span><span class=\"\">&nbsp;</span></nobr></td>\n                            <td align=\"right\" valign=\"bottom\" width=\"80%\"><hr size=\"1\" /></td></tr>\n                        <tr><td colspan=\"2\" align=\"left\" valign=\"top\">\n                            <table border=\"0\" cellpadding=\"0\" cellspacing=\"8\" width=\"100%\">");
            for (int i = 1; i < subTitles.Length; i++)
                if (subTitles[i] != null)
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

        static private Task loading;

        static public Task Load()
        {
            if (loading == null)
                loading = LoadAll();
            return loading;
        }

        static async private Task LoadAll()
        {
            await CopyData();
            await Laptop();
            await Intel();
            await Tablet_PC();
        }

        static async private Task CopyData()
        {
            StorageFolder roaming = ApplicationData.Current.RoamingFolder;
            if (await roaming.TryGetItemAsync("Computer") != null)
                return;
            IStorageItem old = await roaming.TryGetItemAsync("Computer Copy");
            if (old != null)
                await old.DeleteAsync();
            StorageFolder data = await Windows.ApplicationModel.Package.Current.InstalledLocation.GetFolderAsync("Data");
            StorageFolder source = await data.GetFolderAsync("Computer");
            StorageFolder target = await roaming.CreateFolderAsync("Computer Copy");
            await CopyFolder(source, target);
            await target.RenameAsync("Computer");
        }

        static async private Task CopyFolder(StorageFolder source, StorageFolder target)
        {
            foreach (StorageFile i in await source.GetFilesAsync())
                await i.CopyAsync(target, i.Name, NameCollisionOption.ReplaceExisting);
            foreach (StorageFolder i in await source.GetFoldersAsync())
                await CopyFolder(i, await target.CreateFolderAsync(i.Name));
        }

        static public string Value(IList<string> file, string title, int line)
        {
            for (int i = 0; i + line < file.Count; i++)
                if (file[i].Trim().Equals(title))
                    return file[i + line].Trim();
            return "-";
        }

        static public string Folder_Name(string path)
        {
            return System.IO.Path.GetFileName(System.IO.Path.GetDirectoryName(path));
        }

        static async public Task Laptop()
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
                temp.Company = Value(current_file, "اسم الشركة", 1);
                if (temp.Company.Equals("-"))
                    temp.Company = current_file[0].Trim();
                temp.Name = Value(current_file, "اسم الجهاز", 1);
                temp.Processor = Value(current_file, "المعالج", 1);
                temp.Processor_Name = Value(current_file, "المعالج", 2);
                temp.Processor_Speed = Value(current_file, "السرعة", 1);
                temp.Processor_Max_Speed = Value(current_file, "السرعة العظمى", 1);
                temp.Cashe_Memory = Value(current_file, "ذاكرة الكاش", 1);
                temp.Chipset = Value(current_file, "الشريحة", 1);
                temp.RAM = Value(current_file, "الذاكرة", 1);
                temp.Hard_Disk = Value(current_file, "القرص الصلب", 1);
                temp.Hard_Disk_Rotation = Value(current_file, "سرعة الدوران", 1);
                temp.Optical_Driver = Value(current_file, "محرك أقراص مضغوطة", 1);
                temp.Graphic_Card = Value(current_file, "كرت الشاشة", 1);
                temp.Graphic_Card_Size = Value(current_file, "ذاكرة كرت الشاشة", 1);
                temp.Graphic_Card_Max_Size = Value(current_file, "ذاكرة كرت الشاشة العظمى", 1);
                temp.Camera = Value(current_file, "كاميرا", 1);
                temp.Screen_Size = Value(current_file, "الشاشة", 1);
                temp.Screen_Resolution = Value(current_file, "دقة الشاشة", 1);
                int j = 0;
                while (j < current_file.Count && !current_file[j].Trim().Equals("المخارج"))
                    j++;
                temp.Ports = "";
                for (j++; j < current_file.Count && !current_file[j].Contains("لمس"); j++)
                    if (!current_file[j].Trim().Equals("-"))
                        temp.Ports = temp.Ports + General.bool2string(temp.Ports.Length > 0, ", ", "") + current_file[j].Trim();
                temp.Touch_Screen = !Value(current_file, "شاشة لمس", 1).Equals("-");
                temp.OS = Value(current_file, "نظام التشغيل", 1);
                temp.Battary = Value(current_file, "بطارية", 1) + General.bool2string(Value(current_file, "بطارية", 2).Equals("-"), "", " " + Value(current_file, "بطارية", 2));
                temp.Dimensions = Value(current_file, "الابعاد", 1);
                temp.Weight = Value(current_file, "الوزن", 1);
                temp.Category = Value(current_file, "الفئة", 1);
                //temp.Price = float.Parse(current_file[j + 14]);
                current_file.Clear();
                Laptops.Add(temp);
            }
        }

        static async public Task Intel()
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
                temp.Family = Folder_Name(Files[i].Path);
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
                temp.Advanced_Technologies = "64-bit Instruction Set" + General.bool2string(current_file[37].Contains("-"), "", " , " + current_file[37]);
                Processors.Add(temp);
                current_file.Clear();
            }
            await AMD();
        }

        static async public Task AMD()
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
                temp.Family = Folder_Name(Files[i].Path);
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
                temp.Advanced_Technologies = "64-bit Instruction Set" + General.bool2string(current_file[29].Contains("-"), "", " , " + current_file[29]);
                temp.Socket = current_file[31];
                Processors.Add(temp);
                current_file.Clear();
            }
            await APU();
        }

        static async public Task APU()
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
                temp.Family = Folder_Name(Files[i].Path);
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
                temp.Advanced_Technologies = "64-bit Instruction Set" + General.bool2string(current_file[33].Contains("-"), "", " , " + current_file[33]);
                temp.Socket = current_file[35];
                current_file.Clear();
                Processors.Add(temp);
            }
            await MB();
        }

        static async public Task MB()
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
                if (temp.Slot.Contains("e 2.0") || temp.Slot.Contains("x16 2.0"))
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
            await GPU();
        }

        static async public Task GPU()
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
            await HD();
        }

        static async public Task HD()
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

        static async public Task Tablet_PC()
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
                temp.Company = General.First_Word(temp.Company);
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

        static public List<Device> Devices(string category)
        {
            List<Device> list = new List<Device>();
            switch (category)
            {
                case "Processor":
                    foreach (Processor i in Processors)
                        list.Add(i);
                    break;
                case "Motherboard":
                    foreach (MB i in MotherBoards)
                        list.Add(i);
                    break;
                case "Graphic Card":
                    foreach (VGA i in Graphigc_Cards)
                        list.Add(i);
                    break;
                case "Storage Device":
                    foreach (HD i in Hard_Disks)
                        list.Add(i);
                    break;
                case "Laptop":
                    foreach (Laptop i in Laptops)
                        list.Add(i);
                    break;
                case "Tablet":
                    foreach (TabletPC i in Tablet_PCs)
                        list.Add(i);
                    break;
                default:
                    break;
            }
            return list;
        }

        static public string[] Keys(Device device)
        {
            if (device is Processor)
                return new string[] { device.Company, ((Processor)device).Family, device.Name };
            if (device is VGA)
                return new string[] { device.Kind, device.Company, device.Name };
            if (device is HD)
                return new string[] { bool2string(device.Kind.Equals("SSD"), "Solid State Drive", "Hard Disk Drive"), device.Company, ((HD)device).Code + " " + ((HD)device).Capacity };
            return new string[] { device.Company, device.Name };
        }

        static public string Title(Device device)
        {
            if (device is Processor)
                return device.Kind + " " + device.Name;
            if (device is HD)
                return device.Company + " " + ((HD)device).Code + " " + ((HD)device).Capacity;
            return device.Company + " " + device.Name;
        }

        static public bool Matches(Device device, List<string> chosen)
        {
            string[] keys = Keys(device);
            for (int i = 0; i < chosen.Count && i < keys.Length; i++)
                if (!keys[i].Equals(chosen[i]))
                    return false;
            return true;
        }

        static public string Logo(string company)
        {
            switch (company.ToLower())
            {
                case "intel":
                    return "intel_logo";
                case "amd":
                    return "amd_logo";
                default:
                    return company.ToLower();
            }
        }

        static public string Safe(string name)
        {
            foreach (char i in new char[] { '/', '\\', ':', '*', '?', '"', '<', '>', '|' })
                name = name.Replace(i, '_');
            return name;
        }

        static public string Icon(string name)
        {
            return "Assets/Images/Icons/" + name + ".png";
        }

        static public string Icon(Device device)
        {
            if (device is Processor)
                return Icon("cpu");
            if (device is MB)
                return Icon("board");
            if (device is VGA)
                return Icon("gpu");
            if (device is HD)
                return Icon(bool2string(device.Kind.Equals("SSD"), "ssd", "hdd"));
            if (device is RAM)
                return Icon("ram");
            if (device is Laptop)
                return Icon("laptop");
            if (device is TabletPC)
                return Icon("tablet");
            return Icon("generic");
        }

        static public string Picture(Device device)
        {
            if (device is Processor)
                return "Assets/Images/Desktop/" + Safe(((Processor)device).Family) + ".png";
            if (device is MB)
                return "Assets/Images/Desktop/" + Safe(device.Name) + ".jpg";
            if (device is VGA)
                return "Assets/Images/Graphic Card/" + Safe(device.Name) + ".jpg";
            if (device is Laptop)
                return "Assets/Images/Laptop/" + Safe(device.Name) + ".jpg";
            if (device is TabletPC)
                return "Assets/Images/Tablet/" + Safe(device.Name) + ".jpg";
            return Icon(device);
        }

        static public string Logo_Picture(string company)
        {
            return "Assets/Images/Desktop/" + Logo(company) + ".png";
        }

        static public List<string[]> Summary(int mb, int cpu, string ram_company, string ram_size, string gpu_type, int vga, int hd, string psu, string _case, string optical, string monitor, string mouse, string keyboard)
        {
            List<string[]> list = new List<string[]>();
            string none = "Not chosen yet";
            string generic = "Generic option, not from the data";
            if (cpu >= 0)
            {
                Processor p = Processors[cpu];
                list.Add(new string[] { "0", "Processor", p.Kind + " " + p.Name, p.Speed + bool2string(p.Max_Speed.Equals("-"), "", " (up to " + p.Max_Speed + ")") + ", " + p.Cores + " cores, " + p.Thread + " threads, socket " + p.Socket + ", " + p.Power, Picture(p), Icon(p), "" });
            }
            else
                list.Add(new string[] { "0", "Processor", none, "", Icon("cpu"), Icon("cpu"), "" });
            if (mb >= 0)
            {
                MB m = MotherBoards[mb];
                list.Add(new string[] { "0", "Mother Board", m.Company + " " + m.Name, "Socket " + m.Socket + ", chipset " + m.Chipset + ", memory " + m.Max_Mem, Picture(m), Icon(m), "" });
            }
            else
                list.Add(new string[] { "0", "Mother Board", none, "", Icon("board"), Icon("board"), "" });
            if (ram_size != null)
                list.Add(new string[] { "1", "Memory", ram_company + " " + ram_size, generic, Icon("ram"), Icon("ram"), "1" });
            else
                list.Add(new string[] { "1", "Memory", none, "", Icon("ram"), Icon("ram"), "" });
            if (gpu_type != null && gpu_type.Equals("Built-in GPU") && mb >= 0 && cpu >= 0)
                list.Add(new string[] { "2", "Graphic Card", Built_in_GPU(mb, cpu), "Built-in graphics", bool2string(Processors[cpu].Internal_Graphic && !MotherBoards[mb].Internal_Graphic, Picture(Processors[cpu]), Picture(MotherBoards[mb])), Icon("gpu"), "" });
            else if (vga >= 0)
            {
                VGA v = Graphigc_Cards[vga];
                list.Add(new string[] { "2", "Graphic Card", v.Company + " " + v.Name, v.GPU + ", " + v.Mem_Size + " " + v.DDR_Type + ", " + v.Socket, Picture(v), Icon(v), "" });
            }
            else
                list.Add(new string[] { "2", "Graphic Card", none, "", Icon("gpu"), Icon("gpu"), "" });
            if (hd >= 0)
            {
                HD h = Hard_Disks[hd];
                string speed = bool2string(h.Kind.Equals("SSD"), "read " + h.Read_Speed + ", write " + h.Write_Speed, h.Rotation_Speed + ", buffer " + h.Buffer);
                list.Add(new string[] { "3", "Storage Device", h.Company + " " + h.Code + " " + h.Capacity, bool2string(h.Kind.Equals("SSD"), "Solid State Drive", "Hard Disk Drive") + ", " + h.Socket.Trim() + ", " + speed, Picture(h), Icon(h), "" });
            }
            else
                list.Add(new string[] { "3", "Storage Device", none, "", Icon("hdd"), Icon("hdd"), "" });
            string[][] others = { new string[] { "4", "Power Supply", psu, "psu" }, new string[] { "4", "Case", _case, "case" }, new string[] { "5", "Optical Drive", optical, "optical" }, new string[] { "5", "Monitor", monitor, "monitor" }, new string[] { "5", "Mouse", mouse, "mouse" }, new string[] { "5", "Keyboard", keyboard, "keyboard" } };
            foreach (string[] i in others)
                list.Add(new string[] { i[0], i[1], bool2string(i[2] == null, none, i[2]), bool2string(i[2] == null, "", generic), Icon(i[3]), Icon(i[3]), bool2string(i[2] == null, "", "1") });
            return list;
        }

        static public List<string[]> Compatibility(int mb, int cpu, string ram_size, string gpu_type, int vga, int hd)
        {
            List<string[]> list = new List<string[]>();
            if (mb < 0)
                return list;
            MB m = MotherBoards[mb];
            if (cpu >= 0)
                list.Add(new string[] { bool2string(Suffix(Processors[cpu].Socket, m.Socket), "1", ""), "Socket: the processor (" + Processors[cpu].Socket + ") fits the mother board (" + m.Socket + ")" });
            if (ram_size != null)
            {
                string ddr = bool2string(ram_size.Contains("DDR"), ram_size.Substring(Math.Max(ram_size.IndexOf("DDR"), 0)), "");
                list.Add(new string[] { bool2string(ddr.Length == 0 || m.Max_Mem.Contains(ddr), "1", ""), "Memory: the mother board takes " + m.Max_Mem + bool2string(ddr.Length > 0, ", the memory is " + ddr, "") });
            }
            string pci = bool2string(m.PCIe == 5, "PCI Express 2.0 and 3.0", bool2string(m.PCIe == 3, "PCI Express 3.0", "PCI Express 2.0"));
            if (gpu_type != null && gpu_type.Equals("Built-in GPU") && cpu >= 0)
                list.Add(new string[] { bool2string(Has_Built_in_GPU(mb, cpu), "1", ""), "Graphics: built-in " + Built_in_GPU(mb, cpu) });
            else if (vga >= 0)
                list.Add(new string[] { bool2string(PCI_Fits(vga, mb), "1", ""), "Graphic card: " + Graphigc_Cards[vga].Socket + " card in a mother board with " + pci });
            if (hd >= 0)
            {
                string sata = Hard_Disks[hd].Socket.Trim();
                bool Sata3 = (m.Sata == 3) || (m.Sata == 5);
                bool Sata2 = (m.Sata == 2) || (m.Sata == 5) || (m.Sata == 0);
                string ports = bool2string(Sata2 && Sata3, "SATA 2 and SATA 3", bool2string(Sata3, "SATA 3", "SATA 2"));
                list.Add(new string[] { bool2string((sata.Equals("Sata 2") && Sata2) || (sata.Equals("Sata 3") && Sata3), "1", ""), "Storage: " + sata + " drive on a mother board with " + ports + " ports" });
            }
            return list;
        }

        static public List<int> MotherBoards_Of(string platform, string company)
        {
            List<int> list = new List<int>();
            for (int i = 0; i < MotherBoards.Count; i++)
                if (MotherBoards[i].Kind.ToLower().Contains(platform.ToLower()) && MotherBoards[i].Company.ToLower().Contains(company.ToLower()))
                    list.Add(i);
            return list;
        }

        static public List<string> CPU_Models(string platform, int mb)
        {
            List<string> list = new List<string>();
            for (int i = 0; i < Processors.Count; i++)
                if (Processors[i].Company.ToLower().Contains(platform.ToLower()) && Suffix(Processors[i].Socket, MotherBoards[mb].Socket) && !item_Existing(list, Processors[i].Family))
                    list.Add(Processors[i].Family);
            return list;
        }

        static public List<int> Processors_Of(string family, int mb)
        {
            List<int> list = new List<int>();
            for (int i = 0; i < Processors.Count; i++)
                if (Processors[i].Family.Equals(family) && Suffix(Processors[i].Socket, MotherBoards[mb].Socket))
                    list.Add(i);
            return list;
        }

        static public List<string> RAM_Sizes(int mb)
        {
            List<string> list = new List<string>();
            string[] sizes = { "1 GB", "2 GB", "3 GB", "4 GB" };
            foreach (string i in sizes)
            {
                if (MotherBoards[mb].DDR == 0)
                    list.Add(i);
                if (MotherBoards[mb].DDR == 2 || MotherBoards[mb].DDR == 5)
                    list.Add(i + " DDR2");
                if (MotherBoards[mb].DDR == 3 || MotherBoards[mb].DDR == 5)
                    list.Add(i + " DDR3");
            }
            return list;
        }

        static public bool Has_Built_in_GPU(int mb, int cpu)
        {
            if (MotherBoards[mb].Internal_Graphic)
                return !MotherBoards[mb].Graphic_Processor.Contains("Processor") || Processors[cpu].Internal_Graphic;
            return Processors[cpu].Internal_Graphic;
        }

        static public string Built_in_GPU(int mb, int cpu)
        {
            if (MotherBoards[mb].Graphic_Processor.ToLower().Contains("processor") || MotherBoards[mb].Graphic_Processor.Equals("-"))
                return bool2string(Processors[cpu].Internal_Graphic, Processors[cpu].Graphic_Processor, MotherBoards[mb].Graphic_Processor);
            return MotherBoards[mb].Graphic_Processor;
        }

        static public List<string> GPU_Types(int mb, int cpu)
        {
            List<string> list = new List<string>();
            if (Has_Built_in_GPU(mb, cpu))
                list.Add("Built-in GPU");
            list.Add("AMD");
            list.Add("Nvidia");
            return list;
        }

        static public bool PCI_Fits(int vga, int mb)
        {
            bool PCI3 = (MotherBoards[mb].PCIe == 3) || (MotherBoards[mb].PCIe == 5);
            return PCI3 || !Graphigc_Cards[vga].Socket.Contains("3.0");
        }

        static public List<string> GPU_Companies(string type, int mb)
        {
            List<string> list = new List<string>();
            for (int i = 0; i < Graphigc_Cards.Count; i++)
                if (Graphigc_Cards[i].Kind.ToLower().Equals(type.ToLower()) && PCI_Fits(i, mb) && !item_Existing(list, Graphigc_Cards[i].Company))
                    list.Add(Graphigc_Cards[i].Company);
            list.Sort();
            return list;
        }

        static public List<int> Graphic_Cards_Of(string type, string company, int mb)
        {
            List<int> list = new List<int>();
            for (int i = 0; i < Graphigc_Cards.Count; i++)
                if (Graphigc_Cards[i].Kind.ToLower().Equals(type.ToLower()) && Graphigc_Cards[i].Company.Equals(company) && PCI_Fits(i, mb))
                    list.Add(i);
            return list;
        }

        static public List<string> HD_Companies(string type, int mb)
        {
            List<string> list = new List<string>();
            bool Sata3 = (MotherBoards[mb].Sata == 3) || (MotherBoards[mb].Sata == 5);
            bool Sata2 = (MotherBoards[mb].Sata == 2) || (MotherBoards[mb].Sata == 5) || (MotherBoards[mb].Sata == 0);
            for (int i = 0; i < Hard_Disks.Count; i++)
                if (Hard_Disks[i].Kind.Equals(type))
                {
                    string sata = Hard_Disks[i].Socket.Trim();
                    if ((sata.Equals("Sata 2") && Sata2) || (sata.Equals("Sata 3") && Sata3))
                        if (!item_Existing(list, Hard_Disks[i].Company + " (" + sata + ")"))
                            list.Add(Hard_Disks[i].Company + " (" + sata + ")");
                }
            list.Sort();
            return list;
        }

        static public List<int> Hard_Disks_Of(string type, string company_sata)
        {
            List<int> list = new List<int>();
            for (int i = 0; i < Hard_Disks.Count; i++)
                if (Hard_Disks[i].Kind.Equals(type) && company_sata.Equals(Hard_Disks[i].Company + " (" + Hard_Disks[i].Socket.Trim() + ")"))
                    list.Add(i);
            return list;
        }

        static public List<string> Distinct(List<string> values)
        {
            List<string> list = new List<string>();
            foreach (string i in values)
                foreach (string j in AddToList(i))
                    if (!j.Trim().Equals("-") && j.Trim().Length > 0 && !item_Existing(list, j.Trim()))
                        list.Add(j.Trim());
            list.Sort();
            return list;
        }

        static public List<Laptop> Find_Laptop(string Processor, string ProcessorSpeed, string Memory, string Graphic, string HardDisk, string Screen, bool Bluetooth, bool CardReader, bool TouchScreen)
        {
            List<Laptop> list = new List<Laptop>();
            foreach (Laptop i in Laptops)
                if (i.Processor.Contains(Processor) && i.Processor_Speed.Contains(ProcessorSpeed) && i.RAM.Contains(Memory) && i.Graphic_Card.Contains(Graphic) && i.Hard_Disk.Contains(HardDisk) && i.Screen_Size.Contains(Screen))
                    if ((!Bluetooth || i.Ports.Contains("Bluetooth")) && (!CardReader || i.Ports.Contains("Card Reader") || i.Ports.Contains("Cara Reader")) && (!TouchScreen || i.Touch_Screen))
                        list.Add(i);
            return list;
        }

        static public List<TabletPC> Find_Tablet(string Screen, string OS, string Weight, string Processor, string InternalMemory, string Camera, bool Flash, bool MemoryCard)
        {
            List<TabletPC> list = new List<TabletPC>();
            foreach (TabletPC i in Tablet_PCs)
                if (i.Screen_Size.Contains(Screen) && i.OS.Contains(OS) && i.Weight.Contains(Weight) && i.Processor_Name.Contains(Processor) && i.Internal_Memory.Contains(InternalMemory) && i.Camera.Contains(Camera))
                    if ((!Flash || i.Flash) && (!MemoryCard || i.Memory_Card))
                        list.Add(i);
            return list;
        }
    }
}
