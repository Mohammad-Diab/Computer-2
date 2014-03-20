using Computer.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Popups;
using Windows.UI.Xaml.Navigation;

// The Basic Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234237

namespace Computer
{
    /// <summary>
    /// A basic page that provides characteristics common to most applications.
    /// </summary>
    public sealed partial class MainPage : Page
    {

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


        public MainPage()
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

        private async void GridView_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (select_mobile == e.ClickedItem || compare_mobiles == e.ClickedItem)
            {
                await new MessageDialog("There are no mobile phones in the data yet.", "Mobile").ShowAsync();
                return;
            }
            string error = null;
            try
            {
                await General.Load();
            }
            catch (Exception ex)
            {
                error = ex.Message;
            }
            if (error != null)
            {
                await new MessageDialog(error, "Can't read the data").ShowAsync();
                return;
            }
            if (build_desktop == e.ClickedItem)
                Frame.Navigate(typeof(Desktop));
            else if (view_specification == e.ClickedItem)
                Frame.Navigate(typeof(Specification), "Desktop");
            else if (laptops_tablets == e.ClickedItem)
                Frame.Navigate(typeof(Specification), "Laptop");
            else if (compare_devices == e.ClickedItem)
                Frame.Navigate(typeof(Compare));
            else if (choose_device == e.ClickedItem)
                Frame.Navigate(typeof(Choose));
        }

        private void pageRoot_Loaded(object sender, RoutedEventArgs e)
        {
            General.Load();
        }

        private void hub_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            Grid[] tiles = { build_desktop, view_specification, laptops_tablets, compare_devices, choose_device, select_mobile, compare_mobiles };
            double available = e.NewSize.Height - 240;
            double height = available >= 2 * 344 + 24 ? Math.Min((available - 24) / 2, 460) : Math.Max(240, Math.Min(available, 460));
            foreach (Grid i in tiles)
            {
                i.Height = height;
                i.Width = Math.Round(height * 300 / 320);
                ((Image)i.Children[0]).Width = i.Width;
                ((Image)i.Children[0]).Height = height - 70;
                ((TextBlock)i.Children[1]).Margin = new Thickness(14, height - 62, 14, 0);
            }
        }

        private void About_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(About));
        }
    }
}
