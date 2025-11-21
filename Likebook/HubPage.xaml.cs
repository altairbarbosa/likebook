using System.Collections.ObjectModel;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace Likebook
{
    public sealed partial class HubPage : Page
    {
        public ObservableCollection<SiteOption> Sites { get; } = new ObservableCollection<SiteOption>();

        public HubPage()
        {
            InitializeComponent();
            DataContext = this;
            LoadSites();
        }

        private void LoadSites()
        {
            Sites.Clear();
            foreach (var site in SiteSettingsHelper.GetSites())
            {
                Sites.Add(site);
            }
        }

        private void SiteList_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (e.ClickedItem is SiteOption site)
            {
                Frame.Navigate(typeof(MainPage), site);
            }
        }
    }
}
