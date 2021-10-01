using System;
using System.IO;
using Windows.ApplicationModel.DataTransfer;
using Windows.ApplicationModel.Resources;
using Windows.Foundation.Metadata;
using Windows.Graphics.Display;
using Windows.Storage;
using Windows.Storage.Streams;
using Windows.UI;
using Windows.UI.Core;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

namespace Likebook
{
    public sealed partial class MainPage
    {
        readonly string urlLikebook = "https://www.facebook.com/";

        ApplicationDataContainer localSettings = ApplicationData.Current.LocalSettings;
        ApplicationView view = ApplicationView.GetForCurrentView();

        public MainPage()
        {
            InitializeComponent();
            UserAgent();
            GoHome();

            likebookWebView.ContainsFullScreenElementChanged += WebView_ContainsFullScreenElementChanged;

            if (localSettings.Values.ContainsKey("fullScreen") && (bool)localSettings.Values["fullScreen"])
            {
                view.TryEnterFullScreenMode();
            }
            else
            {
                view.ExitFullScreenMode();
            }

            if (localSettings.Values.ContainsKey("commandbar") && (bool)localSettings.Values["commandbar"])
            {
                commandBar.ClosedDisplayMode = AppBarClosedDisplayMode.Compact;
            }
            else
            {
                commandBar.ClosedDisplayMode = AppBarClosedDisplayMode.Minimal;
            }

            if (ApiInformation.IsTypePresent("Windows.UI.ViewManagement.ApplicationView"))
            {
                ApplicationView.GetForCurrentView().TitleBar.BackgroundColor = Color.FromArgb(0, 59, 89, 152);
                ApplicationView.GetForCurrentView().TitleBar.ButtonBackgroundColor = Color.FromArgb(0, 59, 89, 152);
            }

            if (ApiInformation.IsTypePresent("Windows.UI.ViewManagement.StatusBar"))
            {
                StatusBar.GetForCurrentView().BackgroundColor = Color.FromArgb(0, 59, 89, 152);
                StatusBar.GetForCurrentView().BackgroundOpacity = 1;
            }

            SystemNavigationManager.GetForCurrentView().BackRequested += SystemNavigationManager_BackRequested;
        }

        private void UserAgent()
        {
            object value = localSettings.Values["link"];

            if (value == null)
            {
                UserAgentHelper.SetDefaultUserAgent("Mozilla/5.0 (Android 4; Mobile; rv:90.0) Gecko/90.0 Firefox/90.0");
            }
            else
            {
                UserAgentHelper.SetDefaultUserAgent('\u0022' + value.ToString() + '\u0022');
            }
        }

        private void SystemNavigationManager_BackRequested(object sender, BackRequestedEventArgs e)
        {
            Frame frame = Window.Current.Content as Frame;

            if (frame.CanGoBack || likebookWebView.CanGoBack)
            {
                e.Handled = true;

                if (likebookWebView.CanGoBack)
                    likebookWebView.GoBack();


                if (frame.CanGoBack)
                    frame.GoBack();
            }
        }

        private void LikebookWebView_NavigationStarting(WebView sender, WebViewNavigationStartingEventArgs args)
        {
            progressBar.IsIndeterminate = true;
        }

        private async void LikebookWebView_LoadCompleted(object sender, NavigationEventArgs e)
        {
            SystemNavigationManager.GetForCurrentView().AppViewBackButtonVisibility = likebookWebView.CanGoBack ? AppViewBackButtonVisibility.Visible : AppViewBackButtonVisibility.Collapsed;

            progressBar.IsIndeterminate = false;

            string cssToApply = "";

            cssToApply += ".acy {display:none !important;} ._5d25, html ._5d25, ._5d25.acw {display:none !important;} article[data-ft*=ei]{display: none !important;} .ego{display: none !important;}";

            if (localSettings.Values.ContainsKey("blockTopBar"))
                if ((bool)localSettings.Values["blockTopBar"])
                {
                    cssToApply += "#header {position: fixed; z-index: 12; top: 0px;} #root {padding-top: 80px;} ._a-5 {padding-top: 44px;} .item.more {position:fixed; bottom: 0px; text-align: center !important;}";

                    var h = ApplicationView.GetForCurrentView().VisibleBounds.Height - 44;
                    float density = DisplayInformation.GetForCurrentView().LogicalDpi;
                    int barHeight = (int)(h / density);

                    cssToApply += ".flyout {max-height:" + barHeight + "px; overflow-y:scroll;}";
                }
            if (localSettings.Values.ContainsKey("centerTextPosts"))
                if ((bool)localSettings.Values["centerTextPosts"]) cssToApply += "._5rgt._5msi {text-align: center;}";
            if (localSettings.Values.ContainsKey("addSpaceBetweenPosts"))
                if ((bool)localSettings.Values["addSpaceBetweenPosts"]) cssToApply += "article {margin-top: 50px !important;}";
            if (localSettings.Values.ContainsKey("darkTheme"))
                if ((bool)localSettings.Values["darkTheme"])
                {
                    commandBar.RequestedTheme = ElementTheme.Dark;
                    commandBar.Background.SetValue(SolidColorBrush.ColorProperty, Colors.Black);

                    cssToApply += "body * { border-color: transparent !important; color: #888 !important; background-color: transparent !important; } html, body, ._2v9s { background-color: #000 !important; } .storyStream, ._4nmh, ._4u3j, ._35aq, ._146a, ._5pxb, ._55wq, ._53_-, ._55ws, ._u42, .jx-result, .jx-typeahead-results, ._56bt, ._52x7, ._vqv, ._5rgt, .popover_flyout, .flyout, #m_newsfeed_stream, ._55wo, ._3iln, .mentions-suggest, #header, ._xy, ._bgx, .acb, .acg, .aclb, .nontouch ._5ui0, input[type=text], .acw, ._5up8, ._5kgn, .tlLinkContainer, .aps, .jewel .flyout .header, .appCenterCategorySelectorButton, .tlBody, #timelineBody, .timelineX, .timeline .feed, .timeline .tlPrelude, .timeline .tlFeedPlaceholder, .touch ._5c9u, .touch ._5ca9, .innerLink, ._5dy4, ._52x3, #m_group_stories_container, .albums, .subpage, ._uwu, ._uww, .scrollAreaBody, .al, .apl, .structuredPublisher, .groupChromeView, ._djv, ._bjg, ._5kgn, ._3f50, ._55wm, ._58f0 { background: #000 !important; text-shadow: 0px 0px 2px #000 !important; -webkit-transition: background .2s, box-shadow .4s, border-color .2s !important; /* Safari */ transition: background .2s, box-shadow .4s, border-color .2s !important; transition-timing-function: linear !important; } .composerLinkText, .fcg { color: #d2d2d2 !important; } ._56bu, ._56bs { background: #202020 !important; } .touch ._56bu::before, .touch.wp.x1-5 ._56bu::before, .touch.wp.x2 ._56bu::before{ background-color: #202020 !important; text-shadow: 0px 0px 2px #000 !important; } /* New feed info*/ ._15nz { background-color: #121212 !important; } ._4g33, ._4g34 ._1svy { box-shadow: none !important; background: none !important; border: none !important; } ._15ny::after { border: none !important; } /* load spinner */ ._50cg._2ss { box-shadow:  none !important; background: none !important; border: none !important; } /* comments */ ._5c4t ._1g06 { color: #ccc !important; } /* new message dialog */ ._52z5 { } /* white text */ body, .touch ._2ya3, ._4qas, ._4qau, .composerTextSelected, .composerInput, .mentions-input, input[type=text], ._5001, .timeline .cover .profileName, .appListTitle, ._52jd, ._52jb, ._52jg, ._5qc3, .tlActorText, .tlLinkTitle, ._5379, ._5cqn, ._592p, ._3c9l, ._4yrh, .name, .btn, .upText, .tlLinkTitleOnly, ._5rgt, ._52x2, ._52jh, ._52ja, ._56bz, ._2tbu, ._1mwn, ._55sr, ._5t6r, ._1_oe, ._52lz, ._2l5v, .inputtext, .inputpassword, .touch, .touch tr, .touch input, .touch textarea, .touch .mfsm { color: #d2d2d2 !important; } .touch ._2ya3 { border-radius: 5px; padding: 5px; } /* blue link text */ a, .actor, .mfsl, .fcw, .title, .blueName, ._5aw4, ._vqv, ._5yll, ._5qc3, ._52lz, ._4nwe, ._27vp, ._ir4, ._5wsv, ._46pa { color: #DFEFF0 !important; } /* dark important */ .acy, .nontouch ._55mb .actor-link, .nontouch a.btnD, .inlineMedia.storyAttachment { background: #304702 !important; } .statusBox, ._5whq, ._56bt, .composerInput, .mentions-input, ._bji { background: #323232 !important; } .ufiBorder, ._5as0, ._5ef_, ._35aq { border-color: #555 !important; } /* buttons */ .button>a.touchable, .btn, .touch ._5c9u, ._2l5v, ._52x1, ._tn0, ._52ja, ._5lm6 { background: #000 !important; } .flyout { border: 1px solid #000 !important; } /* context menu */ ._5c0e, ._5bn_ { background: #4e4e4e !important; } article, ._4o50, ._3wjp, ._usq, ._55wq, ._400s { border: 1px dotted #383838 !important; border-radius: 4px; } .messages-flyout-item, .more, ._4ut2, ._52we { border-top: 1px dotted #383838 !important; } ._53_- { border-bottom: 1px dotted #383838 !important; } ._1_oa, ._bmx, ._52x1 { border-bottom: 1px dotted #383838 !important; } article h3 { color: #999 !important; } ._4756, ._4qax { background-color: #1F1F1F !important; } /* no border */ .aclb, ._53_-, ._52x6, ._52x1, ._2l5v, ._tn0, ._52ja, ._5lm6 { border-top: 0px; border-bottom: 0px; } ._59te.popoverOpen, ._59te.isActivem, ._59te { background: #000000; border-bottom: 1px solid #424242; border-right: 1px solid #424242; } ._2lut { border: 1px dotted #e9eaed !important; } /* badges */ ._59tg { background:#da2929 !important; color:#ffffff !important; } /* new messages */ .chatHighlight{ -webkit-animation-duration:0s; } /* unread */ .aclb { background: #323232 !important; }";

                    if (ApiInformation.IsTypePresent("Windows.UI.ViewManagement.ApplicationView"))
                    {
                        ApplicationView.GetForCurrentView().TitleBar.BackgroundColor = Colors.Black;
                        ApplicationView.GetForCurrentView().TitleBar.ButtonBackgroundColor = Colors.Black;
                    }

                    if (ApiInformation.IsTypePresent("Windows.UI.ViewManagement.StatusBar"))
                    {
                        StatusBar.GetForCurrentView().BackgroundColor = Colors.Black;
                        StatusBar.GetForCurrentView().ForegroundColor = Colors.White;
                        StatusBar.GetForCurrentView().BackgroundOpacity = 1;
                    }
                }

            await likebookWebView.InvokeScriptAsync("eval", arguments: new[] { "javascript:function addStyleString(str) { var node = document.createElement('style'); node.innerHTML = " + "str; document.body.appendChild(node); } addStyleString('" + cssToApply + "');" });
        }

        private void LikebookWebView_NewWindowRequested(WebView sender, WebViewNewWindowRequestedEventArgs e)
        {
            if (e.Uri.AbsoluteUri.Contains(".gif") || e.Uri.AbsoluteUri.Contains("video"))
            {
                likebookWebView.Navigate(e.Uri);
                e.Handled = true;
            }

            if (localSettings.Values.ContainsKey("browser") && (bool)localSettings.Values["browser"])
            {
                if (e.Uri.AbsoluteUri.Contains("http://") || e.Uri.AbsoluteUri.Contains("https://"))
                {
                    likebookWebView.Navigate(e.Uri);
                    e.Handled = true;
                }
            }

            if (e.Uri.AbsoluteUri.Contains(".jpg") || e.Uri.AbsoluteUri.Contains("photo"))
            {
                likebookWebView.Navigate(e.Uri);
                e.Handled = true;
            }
        }

        private void LikebookWebView_NavigationFailed(object sender, WebViewNavigationFailedEventArgs e)
        {
            var loader = new ResourceLoader();
            string noConnection = loader.GetString("noConnection");
            likebookWebView.NavigateToString(noConnection);
        }

        private void LikebookWebView_PermissionRequested(WebView sender, WebViewPermissionRequestedEventArgs args)
        {
            if (args.PermissionRequest.PermissionType == WebViewPermissionType.Geolocation)
            {
                args.PermissionRequest.Allow();
            }
        }

        private void WebView_ContainsFullScreenElementChanged(WebView sender, object args)
        {
            if (sender.ContainsFullScreenElement)
            {
                ApplicationView.GetForCurrentView().TryEnterFullScreenMode();
                commandBar.Visibility = Visibility.Collapsed;
            }
            else if (ApplicationView.GetForCurrentView().IsFullScreenMode)
            {
                if (localSettings.Values.ContainsKey("fullScreen") && (bool)localSettings.Values["fullScreen"])
                {
                    view.TryEnterFullScreenMode();
                }
                else
                {
                    view.ExitFullScreenMode();
                }
                commandBar.Visibility = Visibility.Visible;
            }
        }

        private void GoHome()
        {
            if (!localSettings.Values.ContainsKey("showRecentNews"))
                likebookWebView.Navigate(new Uri(urlLikebook));
            else
                if ((bool)localSettings.Values["showRecentNews"])
                likebookWebView.Navigate(new Uri(urlLikebook + "?sk=h_chr"));
            else
                likebookWebView.Navigate(new Uri(urlLikebook + "?sk=h_nor"));
        }

        private async void TopButton_Click(object sender, RoutedEventArgs e)
        { await likebookWebView.InvokeScriptAsync("eval", new[] { "window.scrollTo(0,0);" }); }

        private void RefreshButton_Click(object sender, RoutedEventArgs e)
        {
            likebookWebView.Refresh();
        }

        private async void BackButton_Click(object sender, RoutedEventArgs e)
        {
            if (likebookWebView.CanGoBack)
            {
                likebookWebView.GoBack();
            }
            else
            {
                var loader = new ResourceLoader();
                string Title = loader.GetString("Title");
                string ContentBack = loader.GetString("ContentBack");
                string PrimaryButtonTextBack = loader.GetString("PrimaryButtonTextBack");
                string SecondaryButtonTextBack = loader.GetString("SecondaryButtonTextBack");

                ContentDialog BackFileDialog = new ContentDialog()
                {
                    Title = loader.GetString("Title"),
                    Content = loader.GetString("ContentBack"),
                    PrimaryButtonText = loader.GetString("PrimaryButtonTextBack"),
                    SecondaryButtonText = loader.GetString("SecondaryButtonText")
                };

                ContentDialogResult result = await BackFileDialog.ShowAsync();

                if (result == ContentDialogResult.Primary)
                {
                    Application.Current.Exit();
                }
            }
        }

        private void HomeButton_Click(object sender, RoutedEventArgs e)
        { GoHome(); }

        private async void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            Uri url = likebookWebView.Source;

            var fileName = Path.GetFileName(url.LocalPath);
            var thumbnail = RandomAccessStreamReference.CreateFromUri(url);

            var remoteFile = await StorageFile.CreateStreamedFileFromUriAsync(fileName, url, thumbnail);
            await remoteFile.CopyAsync(KnownFolders.SavedPictures, fileName, NameCollisionOption.GenerateUniqueName);

            var loader = new ResourceLoader();
            string saveTitleDialog = loader.GetString("Title");
            string saveDialog = loader.GetString("saveDialog");
            string PrimaryButtonText = loader.GetString("PrimaryButtonText");

            ContentDialog SaveFileDialog = new ContentDialog()
            {
                Title = loader.GetString("Title"),
                Content = loader.GetString("saveDialog"),
                PrimaryButtonText = loader.GetString("PrimaryButtonText")
            };

            ContentDialogResult result = await SaveFileDialog.ShowAsync();
        }

        private async void ClipButton_Click(object sender, RoutedEventArgs e)
        {
            string url = likebookWebView.Source.ToString();
            var dataPackage = new DataPackage();
            dataPackage.SetText(url);
            Clipboard.SetContent(dataPackage);

            var loader = new ResourceLoader();

            ContentDialog CopyFileDialog = new ContentDialog()
            {
                Title = loader.GetString("Title"),
                Content = loader.GetString("linkCopy"),
                PrimaryButtonText = loader.GetString("PrimaryButtonText")
            };

            ContentDialogResult result = await CopyFileDialog.ShowAsync();
        }

        private void SettingsButton_Click(object sender, RoutedEventArgs e)
        { Frame.Navigate(typeof(SettingPage)); }
    }
}