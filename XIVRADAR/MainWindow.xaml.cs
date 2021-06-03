// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MainWindow.xaml.cs">
//   Copyright© 2021 Ryan Wilson
//   Licensed under the MIT license. See LICENSE.md in the solution root for full license information.
// </copyright>
// <summary>
//   MainWindow.xaml.cs Implementation
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace XIVRADAR {
    using System;
    using System.ComponentModel;
    using System.Windows;
    using System.Windows.Threading;

    using XIVRADAR.Helpers;
    using XIVRADAR.Properties;
    using XIVRADAR.ViewModels;
    using XIVRADAR.Windows;

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window {
        private static RadarWindow _radarWindow;

        private static TransparentRadarWindow _transparentRadarWindow;

        public MainWindow() {
            this.InitializeComponent();

            Instance = this;

            this.DataContext = new MainWindowViewModel();
        }

        public static MainWindow Instance { get; set; }

        private void CloseApplication() {
            if (Application.Current.MainWindow is not null) {
                Application.Current.MainWindow.WindowState = WindowState.Normal;
            }

            _radarWindow?.Close();
            _transparentRadarWindow?.Close();

            Settings.Default.Save();

            SettingsHelper.SaveChatCodes();
            SettingsHelper.SaveFilters();
            SavedLogsHelper.SaveCurrentLog();

            Environment.Exit(0);
        }

        private void MainWindow_OnClosing(object sender, CancelEventArgs e) {
            e.Cancel = true;
            DispatcherHelper.Invoke(this.CloseApplication, DispatcherPriority.Send);
        }

        private void MainWindow_OnContentRendered(object? sender, EventArgs e) {
            LocaleHelper.UpdateLocale(Settings.Default.Culture);
        }

        private void MainWindow_OnLoaded(object sender, RoutedEventArgs e) {
            AppContext.Instance.Initialize();

            if (Settings.Default.ShowRadarOnOpen) {
                WindowManager.Instance.ShowRadarWindow();
            }

            if (Settings.Default.ShowTransparentRadarOnOpen) {
                WindowManager.Instance.ShowTransparentRadarWindow();
            }
        }
    }
}