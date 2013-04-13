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

class Processor
{
    public int ID;
    public string Type; //AMD or Intel
    public string Name;
    public string Kind;
    public float Cores;
    public float Thread;
    public string Speed;
    public string Max_Speed;
    public string L1;
    public string L2;
    public string L3;
    public bool Bit_64;
    public string Technique;
    public string Power;
    public string Maxtmp;
    public bool AMD_Virtualization_Technology;
    public bool Intel_Turbo_Boost_Technology_2;
    public bool Intel_Turbo_Boost_Technology;
    public bool Intel_Hyper_Threading_Technology;
    public bool VT_x;
    public bool Enhanced_Intel_SpeedStep_Technology;
    public bool Internal_Graphic;
    public string Graphic_Processor;
    public string Socket;
    public float Price;
    public string DMI;     //”—⁄… «·‰«ﬁ·
    public string Instructions;
    public string Max_Memory;
    public static int number = 1;
    public float Memory_Channels;
    public bool Black_Edition;

    public Processor()
    {
        ID = number;
        number++;
    }

}

class MB
{
    public int ID;
    public string Type; //AMD or Intel
    public string Socket;
    public string Company;
    public string Name;
    public string Power;
    public string Chipset;
    public string Max_Mem;
    public bool Internal_Graphic;
    public string Graphic_Processor;
    public string Lan;
    public float DMI;
    public float VGA;
    public float HDMI;
    public float DVI;
    public float PCIe_20_x16;
    public float PCIe_20_x1;
    public float PCI_22;
    public float PCI;
    public float SATA_6Gbs;
    public float SATA_3Gbs;
    public float External_SATA;
    public float IEEE_1394a;
    public float LAN_RJ45_port;
    public float Channel_Audio_IO;
    public float PS2_Keyboard;
    public float PS2_mouse;
    public float USB_30;
    public float USB_20;
    public string Bluetooth;
    public string WLAN;
    public bool AMD_CrossFire;
    public bool NVIDIA_SLI;
    public bool Winki_3;
    public bool Auto_OC_Genie;
    public bool Intel_vPro_Technology;
    public bool EPU;
    public float Price;
    public static int number = 1;

    public MB()
    {
        ID = number;
        number++;
    }

}

class VGA
{
    public int ID;
    public string Type; //AMD or Nvidia
    public string Company;
    public string Name;
    public string GPU;
    public string Socket;
    public string Mem_Size;
    public string DDR_Type;
    public string Mem_Channel;
    public string Processor_Speed;
    public string Mem_Speed;
    public string Max_Resolution;
    public string DirectX_Version;
    public string OpenGL_Version;
    public string Pixel_Shader_Version;
    public string Cores;
    public bool CrossFire;
    public bool AMD_HD3D;
    public bool SLI;
    public bool PhysX;
    public bool Vision_3D;
    public float DVI;
    public float HDMI;
    public bool Mini_HDMI;
    public float DisplayPort;
    public bool Mini_DisplayPort;
    public float D_SUB;
    public float Price;
    public static int number = 1;

    public VGA()
    {
        ID = number;
        number++;
    }

}

class HD
{
    public int ID;
    public string Type;    //SSD or HDD
    public string Socket;
    public string Company;
    public string Name;
    public string Code;
    public string Capacity;
    public string Rotation_Speed;
    public string Buffer;
    public string Max_Speed;
    public string Read_Speed;
    public string Write_Speed;
    public string Weight;
    public string Power;
    public float Price;
    public static int number = 1;

    public HD()
    {
        ID = number;
        number++;
    }

}

class Tablet
{
    public int ID;
    public string Company;
    public string Name;
    public string Size;
    public string Weight;
    public string Screen_Size;
    public string Resolution;
    public string OS;
    public string Processor_Name;
    public string Processor_Speed;
    public string Cores;
    public string Memory_Size;
    public string Internal_Memory;
    public bool Memory_Card;
    public string Max_Memory_Card_Size;
    public string Camera;
    public bool Flash;
    public string Video_Resolution;
    public string Web_Cam;
    public string G;   //ÃÌ· ‘»ﬂ… «·« ’«·
    public string Bluetooth;
    public float Price;
    public static int number = 1;

    public Tablet()
    {
        ID = number;
        number++;
    }

}


// The Basic Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234237

namespace Computer_2
{
    /// <summary>
    /// A basic page that provides characteristics common to most applications.
    /// </summary>
    public sealed partial class BasicPage1 : Computer_2.Common.LayoutAwarePage
    {
        public BasicPage1()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// Populates the page with content passed during navigation.  Any saved state is also
        /// provided when recreating a page from a prior session.
        /// </summary>
        /// <param name="navigationParameter">The parameter value passed to
        /// <see cref="Frame.Navigate(Type, Object)"/> when this page was initially requested.
        /// </param>
        /// <param name="pageState">A dictionary of state preserved by this page during an earlier
        /// session.  This will be null the first time a page is visited.</param>
        protected override void LoadState(Object navigationParameter, Dictionary<String, Object> pageState)
        {
        }

        /// <summary>
        /// Preserves state associated with this page in case the application is suspended or the
        /// page is discarded from the navigation cache.  Values must conform to the serialization
        /// requirements of <see cref="SuspensionManager.SessionState"/>.
        /// </summary>
        /// <param name="pageState">An empty dictionary to be populated with serializable state.</param>
        protected override void SaveState(Dictionary<String, Object> pageState)
        {
        }

    }
}
