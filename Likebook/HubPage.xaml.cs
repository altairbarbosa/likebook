using System.Collections.ObjectModel;
using Windows.Storage;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace Likebook
{
    public sealed partial class HubPage : Page
    {
        private readonly ApplicationDataContainer localSettings = ApplicationData.Current.LocalSettings;

        public ObservableCollection<SiteOption> Sites { get; } = new ObservableCollection<SiteOption>();

        public HubPage()
        {
            InitializeComponent();
            LoadSites();
        }

        private void LoadSites()
        {
            Sites.Add(new SiteOption("Facebook", "https://www.facebook.com/", "Mozilla/5.0 (Android 4; Mobile; rv:90.0) Gecko/90.0 Firefox/90.0", "\uE12B", "Versão mobile do Facebook.", "#3b5998"));
            Sites.Add(new SiteOption("X / Twitter", "https://mobile.twitter.com/", "Mozilla/5.0 (Linux; Android 10; Pixel 3 Build/QP1A.190711.020) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/88.0.4324.93 Mobile Safari/537.36", "\uE12A", "Interface mobile do X (antigo Twitter).", "#000000"));
            Sites.Add(new SiteOption("Instagram", "https://www.instagram.com/", "Mozilla/5.0 (Linux; Android 12; Pixel 5 XL build/Beta6) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/99.0.9999.999 Mobile Safari/537.36", "\uE158", "Instagram com user-agent de Android.", "#C13584"));
            Sites.Add(new SiteOption("YouTube", "https://m.youtube.com/", "Mozilla/5.0 (iPhone; CPU iPhone OS 15 like Mac OS X) AppleWebKit/605.1.15 (KHTML, like Gecko) Version/15.0 Mobile/15E148 Safari/604.1", "\uE714", "YouTube mobile em modo iPhone.", "#FF0000"));
        }

        private void SiteList_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (e.ClickedItem is SiteOption site)
            {
                localSettings.Values["lastSiteUrl"] = site.Url;
                localSettings.Values["lastSiteUserAgent"] = site.UserAgent;
                Frame.Navigate(typeof(MainPage), site);
            }
        }
    }

    public sealed class SiteOption
    {
        public SiteOption(string name, string url, string userAgent, string glyph, string description, string colorHex = "#3b5998")
        {
            Name = name;
            Url = url;
            UserAgent = userAgent;
            Glyph = glyph;
            Description = description;
            ColorHex = colorHex;
        }

        public string Name { get; }
        public string Url { get; }
        public string UserAgent { get; }
        public string Glyph { get; }
        public string Description { get; }
        public string ColorHex { get; }
    }
}
