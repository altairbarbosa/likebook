using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Windows.ApplicationModel;
using Windows.ApplicationModel.DataTransfer;
using Windows.ApplicationModel.Email;
using Windows.ApplicationModel.Resources;
using Windows.Storage;
using Windows.System;
using Windows.UI.Core;
using Windows.UI.StartScreen;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace Likebook
{
    public sealed partial class SettingPage
    {
        ApplicationDataContainer localSettings = ApplicationData.Current.LocalSettings;
        public ObservableCollection<SiteOption> Sites { get; } = new ObservableCollection<SiteOption>();
        private SiteOption selectedSite;

        public SettingPage()
        {
            InitializeComponent();
            InitializeSites();
            InitializeUserAgentCombo();
            LoadSavedValue();

            if (localSettings.Values.ContainsKey("commandbar") && (bool)localSettings.Values["commandbar"])
            {
                commandBar.ClosedDisplayMode = AppBarClosedDisplayMode.Compact;
            }
            else
            {
                commandBar.ClosedDisplayMode = AppBarClosedDisplayMode.Minimal;
            }

            PackageVersion number = Package.Current.Id.Version;
            version.Text += string.Format(" {0}.{1}.{2}", number.Major, number.Minor, number.Build);

            SystemNavigationManager.GetForCurrentView().AppViewBackButtonVisibility = AppViewBackButtonVisibility.Visible;
            SystemNavigationManager.GetForCurrentView().BackRequested += (s, a) =>
            {
                if (Frame.CanGoBack)
                {
                    Frame.GoBack();
                    a.Handled = true;
                }
            };
        }

        private void ComboBox1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox comboBox = sender as ComboBox;
            if (comboBox?.SelectedValue != null)
            {
                localSettings.Values["link"] = comboBox.SelectedValue.ToString();
            }
        }

        private void LoadSavedValue()
        {
            if (localSettings.Values.ContainsKey("link"))
            {
                UserAgent.SelectedValue = localSettings.Values["link"];
            }
            else
            {
                // default to first item and persist it so it is restored on next launch
                UserAgent.SelectedIndex = 0;
                var selected = UserAgent.SelectedValue?.ToString();
                if (!string.IsNullOrEmpty(selected))
                {
                    localSettings.Values["link"] = selected;
                }
            }
            if (localSettings.Values.ContainsKey("fullScreen"))
                fullScreen.IsOn = (bool)localSettings.Values["fullScreen"];
            if (localSettings.Values.ContainsKey("blockTopBar"))
                blockTopBar.IsOn = (bool)localSettings.Values["blockTopBar"];
            if (localSettings.Values.ContainsKey("showRecentNews"))
                showRecentNews.IsOn = (bool)localSettings.Values["showRecentNews"];
            if (localSettings.Values.ContainsKey("centerTextPosts"))
                centerTextPosts.IsOn = (bool)localSettings.Values["centerTextPosts"];
            if (localSettings.Values.ContainsKey("addSpaceBetweenPosts"))
                addSpaceBetweenPosts.IsOn = (bool)localSettings.Values["addSpaceBetweenPosts"];
            if (localSettings.Values.ContainsKey("darkTheme"))
                darkTheme.IsOn = (bool)localSettings.Values["darkTheme"];
            if (localSettings.Values.ContainsKey("browser"))
                browser.IsOn = (bool)localSettings.Values["browser"];
            if (localSettings.Values.ContainsKey("commandbar"))
                commandbar.IsOn = (bool)localSettings.Values["commandbar"];
        }

        private void SaveStatus_Toggled(object sender, RoutedEventArgs e)
        {
            ToggleSwitch thisToggle = (ToggleSwitch)sender;
            bool isOn = thisToggle.IsOn;
            string toggleName = thisToggle.Name;

            if (!localSettings.Values.ContainsKey(toggleName))
            {
                localSettings.Values.Add(toggleName, isOn);
            }
            else
                localSettings.Values[toggleName] = isOn;
        }

        private static async Task<bool> PinSecondaryTileAsync(string id)
        {
            if (SecondaryTile.Exists(tileId: id))
            {
                return false;
            }

            var secondaryTile = new SecondaryTile(id, "Facebook", id, new Uri("ms-appx:///Assets/Tiles/150x150.png"), TileSize.Default);

            secondaryTile.VisualElements.Wide310x150Logo = new Uri("ms-appx:///Assets/Tiles/310x150.png");
            secondaryTile.VisualElements.Square310x310Logo = new Uri("ms-appx:///Assets/Tiles/310x310.png");

            secondaryTile.VisualElements.ShowNameOnWide310x150Logo = false;
            secondaryTile.VisualElements.ShowNameOnSquare310x310Logo = true;
            secondaryTile.VisualElements.ShowNameOnSquare150x150Logo = true;

            return await secondaryTile.RequestCreateAsync();
        }

        private async void IconAppButton_Click(object sender, RoutedEventArgs e)
        {
            await PinSecondaryTileAsync("secondaryTile");
        }

        private async void ReviewAppButton_Click(object sender, RoutedEventArgs e)
        {
            Uri review = new Uri("ms-windows-store://review/?ProductId=9nblggh520bn");
            await Launcher.LaunchUriAsync(review);
        }

        private async void Privacy(object sender, RoutedEventArgs e)
        {
            var privacy = new Uri("https://oquesereu.tumblr.com/post/149621393391/");
            await Launcher.LaunchUriAsync(privacy);
        }

        private async void Aplicativos(object sender, RoutedEventArgs e)
        {
            var apps = new Uri("https://oquesereu.tumblr.com/tagged/windows");
            await Launcher.LaunchUriAsync(apps);
        }

        private void ShareAppButton_Click(object sender, RoutedEventArgs e)
        {
            DataTransferManager.ShowShareUI();
            DataTransferManager.GetForCurrentView().DataRequested += MainPage_DataRequested;
        }

        private void MainPage_DataRequested(DataTransferManager sender, DataRequestedEventArgs args)
        {
            var loader = new ResourceLoader();
            string shareMessage = loader.GetString("shareMessage");
            if (!string.IsNullOrEmpty(shareMessage))
            {
                args.Request.Data.SetText(shareMessage);
                args.Request.Data.Properties.Title = Package.Current.DisplayName;
            }
        }

        private void InitializeUserAgentCombo()
        {
            Dictionary<string, string> userAgents = new Dictionary<string, string>
            {
                { "Chrome (Android 14)", "Mozilla/5.0 (Linux; Android 14; Pixel 7 Pro) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/120.0.6099.144 Mobile Safari/537.36" },
                { "EDGE Mobile", "Mozilla/5.0 (Linux; Android 14; Pixel 7 Pro) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/120.0.6099.144 Mobile Safari/537.36 EdgA/120.0.0.0" },
                { "Firefox", "Mozilla/5.0 (Android 14; Mobile; rv:120.0) Gecko/120.0 Firefox/120.0" },
                { "Firefox Focus", "Mozilla/5.0 (iPhone; CPU iPhone OS 17_1 like Mac OS X) AppleWebKit/605.1.15 (KHTML, like Gecko) FxiOS/120.0 Mobile/15E148 Safari/605.1.15" },
                { "IE Mobile", "Mozilla/5.0 (Mobile; Windows Phone 10.0; Android 4.2.1; ARM; Trident/7.0; Touch; rv:11.0; IEMobile/11.0) like iPhone OS 7 Mac OS X AppleWebKit/537 (KHTML, like Gecko) Mobile Safari/537" },
                { "Opera Mobile", "Mozilla/5.0 (Linux; Android 14; SM-S911B) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/120.0.6099.144 Mobile Safari/537.36 OPR/73.0.3856.100" },
                { "Safari on iPad", "Mozilla/5.0 (iPad; CPU OS 17_1 like Mac OS X) AppleWebKit/605.1.15 (KHTML, like Gecko) Version/17.1 Mobile/15E148 Safari/604.1" },
                { "Safari on iPhone", "Mozilla/5.0 (iPhone; CPU iPhone OS 17_1 like Mac OS X) AppleWebKit/605.1.15 (KHTML, like Gecko) Version/17.1 Mobile/15E148 Safari/604.1" },
                { "Samsung Browser", "Mozilla/5.0 (Linux; Android 14; SM-S918B) AppleWebKit/537.36 (KHTML, like Gecko) SamsungBrowser/23.0 Chrome/120.0.6099.144 Mobile Safari/537.36" },
            };

            UserAgent.ItemsSource = userAgents;
            UserAgent.SelectedValuePath = "Value";
            UserAgent.DisplayMemberPath = "Key";
        }

        private async void ClearAppButton_Click(object sender, RoutedEventArgs e)
        {
            await WebView.ClearTemporaryWebDataAsync();

            var loader = new ResourceLoader();
            string Title = loader.GetString("Title");
            string ContentCache = loader.GetString("ContentCache");
            string PrimaryButtonText = loader.GetString("PrimaryButtonTextBack");
            string SecondaryButtonTextBack = loader.GetString("SecondaryButtonTextBack");

            ContentDialog CacheFileDialog = new ContentDialog()
            {
                Title = loader.GetString("Title"),
                Content = loader.GetString("ContentCache"),
                PrimaryButtonText = loader.GetString("PrimaryButtonTextBack"),
                SecondaryButtonText = loader.GetString("SecondaryButtonText")
            };

            ContentDialogResult result = await CacheFileDialog.ShowAsync();

            if (result == ContentDialogResult.Primary)
            {
                Application.Current.Exit();
            }
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            if (Frame.CanGoBack)
                Frame.GoBack();
            else
                Frame.Navigate(typeof(HubPage));
        }

        private void HubButton_Click(object sender, RoutedEventArgs e)
        { Frame.Navigate(typeof(HubPage)); }

        private void InitializeSites()
        {
            Sites.Clear();
            foreach (var site in SiteSettingsHelper.GetSites())
            {
                Sites.Add(site);
            }
            SiteListView.ItemsSource = Sites;
        }

        private void SiteListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            selectedSite = SiteListView.SelectedItem as SiteOption;
            if (selectedSite != null)
            {
                SiteNameBox.Text = selectedSite.Name;
                SiteUrlBox.Text = selectedSite.Url;
                SiteUaBox.Text = selectedSite.UserAgent;
                SiteGlyphBox.Text = selectedSite.Glyph;
                SiteColorBox.Text = selectedSite.ColorHex;
            }
        }

        private void NewSite_Click(object sender, RoutedEventArgs e)
        {
            SiteListView.SelectedItem = null;
            selectedSite = null;
            SiteNameBox.Text = "";
            SiteUrlBox.Text = "";
            SiteUaBox.Text = "";
            SiteGlyphBox.Text = "";
            SiteColorBox.Text = "";
        }

        private void SaveSite_Click(object sender, RoutedEventArgs e)
        {
            string name = SiteNameBox.Text?.Trim();
            string url = SiteUrlBox.Text?.Trim();
            string ua = SiteUaBox.Text?.Trim();
            string glyph = SiteGlyphBox.Text?.Trim();
            string color = SiteColorBox.Text?.Trim();

            if (string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(url))
                return;

            if (selectedSite == null)
            {
                var newSite = new SiteOption(name, url, ua, glyph, "", color);
                Sites.Add(newSite);
            }
            else
            {
                selectedSite.Name = name;
                selectedSite.Url = url;
                selectedSite.UserAgent = ua;
                selectedSite.Glyph = glyph;
                selectedSite.ColorHex = color;
                int idx = Sites.IndexOf(selectedSite);
                if (idx >= 0)
                {
                    Sites.RemoveAt(idx);
                    Sites.Insert(idx, selectedSite);
                }
            }

            SiteSettingsHelper.SaveSites(Sites.ToList());
        }

        private void RemoveSite_Click(object sender, RoutedEventArgs e)
        {
            if (selectedSite != null && Sites.Contains(selectedSite))
            {
                Sites.Remove(selectedSite);
                SiteSettingsHelper.SaveSites(Sites.ToList());
                NewSite_Click(sender, e);
            }
        }
    }
}
