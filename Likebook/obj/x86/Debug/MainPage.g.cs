﻿#pragma checksum "E:\Windows\Apps\Microsoft\BETA\Likebook\Likebook\MainPage.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "AB6C8186C1226F4F7DC7343CB32AB417"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Likebook
{
    partial class MainPage : 
        global::Windows.UI.Xaml.Controls.Page, 
        global::Windows.UI.Xaml.Markup.IComponentConnector,
        global::Windows.UI.Xaml.Markup.IComponentConnector2
    {
        /// <summary>
        /// Connect()
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 14.0.0.0")]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public void Connect(int connectionId, object target)
        {
            switch(connectionId)
            {
            case 1:
                {
                    this.likebookWebView = (global::Windows.UI.Xaml.Controls.WebView)(target);
                    #line 7 "..\..\..\MainPage.xaml"
                    ((global::Windows.UI.Xaml.Controls.WebView)this.likebookWebView).NavigationStarting += this.LikebookWebView_NavigationStarting;
                    #line 7 "..\..\..\MainPage.xaml"
                    ((global::Windows.UI.Xaml.Controls.WebView)this.likebookWebView).NewWindowRequested += this.LikebookWebView_NewWindowRequested;
                    #line 7 "..\..\..\MainPage.xaml"
                    ((global::Windows.UI.Xaml.Controls.WebView)this.likebookWebView).LoadCompleted += this.LikebookWebView_LoadCompleted;
                    #line 7 "..\..\..\MainPage.xaml"
                    ((global::Windows.UI.Xaml.Controls.WebView)this.likebookWebView).NavigationFailed += this.LikebookWebView_NavigationFailed;
                    #line 7 "..\..\..\MainPage.xaml"
                    ((global::Windows.UI.Xaml.Controls.WebView)this.likebookWebView).PermissionRequested += this.LikebookWebView_PermissionRequested;
                    #line default
                }
                break;
            case 2:
                {
                    this.progressBar = (global::Windows.UI.Xaml.Controls.ProgressBar)(target);
                }
                break;
            case 3:
                {
                    this.commandBar = (global::Windows.UI.Xaml.Controls.CommandBar)(target);
                }
                break;
            case 4:
                {
                    this.backButton = (global::Windows.UI.Xaml.Controls.AppBarButton)(target);
                    #line 13 "..\..\..\MainPage.xaml"
                    ((global::Windows.UI.Xaml.Controls.AppBarButton)this.backButton).Click += this.BackButton_Click;
                    #line default
                }
                break;
            case 5:
                {
                    this.topButton = (global::Windows.UI.Xaml.Controls.AppBarButton)(target);
                    #line 14 "..\..\..\MainPage.xaml"
                    ((global::Windows.UI.Xaml.Controls.AppBarButton)this.topButton).Click += this.TopButton_Click;
                    #line default
                }
                break;
            case 6:
                {
                    this.refreshButton = (global::Windows.UI.Xaml.Controls.AppBarButton)(target);
                    #line 15 "..\..\..\MainPage.xaml"
                    ((global::Windows.UI.Xaml.Controls.AppBarButton)this.refreshButton).Click += this.RefreshButton_Click;
                    #line default
                }
                break;
            case 7:
                {
                    this.homeButton = (global::Windows.UI.Xaml.Controls.AppBarButton)(target);
                    #line 16 "..\..\..\MainPage.xaml"
                    ((global::Windows.UI.Xaml.Controls.AppBarButton)this.homeButton).Click += this.HomeButton_Click;
                    #line default
                }
                break;
            case 8:
                {
                    this.saveButton = (global::Windows.UI.Xaml.Controls.AppBarButton)(target);
                    #line 18 "..\..\..\MainPage.xaml"
                    ((global::Windows.UI.Xaml.Controls.AppBarButton)this.saveButton).Click += this.SaveButton_Click;
                    #line default
                }
                break;
            case 9:
                {
                    this.clipButton = (global::Windows.UI.Xaml.Controls.AppBarButton)(target);
                    #line 19 "..\..\..\MainPage.xaml"
                    ((global::Windows.UI.Xaml.Controls.AppBarButton)this.clipButton).Click += this.ClipButton_Click;
                    #line default
                }
                break;
            case 10:
                {
                    this.settingsButton = (global::Windows.UI.Xaml.Controls.AppBarButton)(target);
                    #line 20 "..\..\..\MainPage.xaml"
                    ((global::Windows.UI.Xaml.Controls.AppBarButton)this.settingsButton).Click += this.SettingsButton_Click;
                    #line default
                }
                break;
            default:
                break;
            }
            this._contentLoaded = true;
        }

        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 14.0.0.0")]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public global::Windows.UI.Xaml.Markup.IComponentConnector GetBindingConnector(int connectionId, object target)
        {
            global::Windows.UI.Xaml.Markup.IComponentConnector returnValue = null;
            return returnValue;
        }
    }
}
