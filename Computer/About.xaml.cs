using Computer.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Windows.ApplicationModel;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace Computer
{
    public sealed partial class About : Page
    {
        private NavigationHelper navigationHelper;
        private ObservableDictionary defaultViewModel = new ObservableDictionary();

        public ObservableDictionary DefaultViewModel
        {
            get { return this.defaultViewModel; }
        }

        public NavigationHelper NavigationHelper
        {
            get { return this.navigationHelper; }
        }

        public About()
        {
            this.InitializeComponent();
            this.navigationHelper = new NavigationHelper(this);
        }

        #region NavigationHelper registration

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            navigationHelper.OnNavigatedTo(e);
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            navigationHelper.OnNavigatedFrom(e);
        }

        #endregion

        private void pageRoot_Loaded(object sender, RoutedEventArgs e)
        {
            PackageVersion v = Package.Current.Id.Version;
            version.Text = "Version " + v.Major + "." + v.Minor + "." + v.Build;
            string date = null;
            foreach (AssemblyMetadataAttribute i in typeof(About).GetTypeInfo().Assembly.GetCustomAttributes<AssemblyMetadataAttribute>())
                if (i.Key.Equals("BuildDate"))
                    date = i.Value;
            if (date == null)
                date = Package.Current.InstalledDate.ToString("yyyy-MM-dd");
            built.Text = "Built " + date;
        }
    }
}
