// --------------------------------------------------------------------------------------------------------------------
// <copyright file="App.xaml.cs">
//   Copyright© 2021 Ryan Wilson
//   Licensed under the MIT license. See LICENSE.md in the solution root for full license information.
// </copyright>
// <summary>
//   App.xaml.cs Implementation
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace XIVRADAR {
    using System;
    using System.ComponentModel;
    using System.Configuration;
    using System.IO;
    using System.Linq;
    using System.Windows;
    using System.Xml;
    using System.Xml.Linq;

    using NLog;
    using NLog.Config;

    using XIVRADAR.Helpers;
    using XIVRADAR.Models;
    using XIVRADAR.Properties;
    using XIVRADAR.Utilities;
    using XIVRADAR.ViewModels;

    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        private App() {
            this.ConfigureNLog();

            Settings.Default.PropertyChanged += this.Default_OnPropertyChanged;
            Settings.Default.SettingChanging += this.Default_OnSettingChanging;

            this.CheckSettings();

            this.InitializeComponent();
        }

        private void CheckSettings() {
            try {
                if (!Settings.Default.UpgradeSettings) {
                    Settings.Default.Reload();
                    return;
                }

                Settings.Default.Upgrade();
                Settings.Default.Reload();
                Settings.Default.UpgradeSettings = false;
            }
            catch (Exception) {
                Settings.Default.Reset();
            }
        }

        private void ConfigureNLog() {
            string path = Path.Combine(Directory.GetCurrentDirectory(), "XIVRADAR.exe.nlog");
            StringReader stringReader;
            stringReader = File.Exists(path)
                               ? new StringReader(XElement.Load(path).ToString())
                               : new StringReader(ResourceHelper.LoadXML($"{Constants.AppPack}Resources/XIVRADAR.exe.nlog").ToString());

            using XmlReader xmlReader = XmlReader.Create(stringReader);
            LogManager.Configuration = new XmlLoggingConfiguration(xmlReader, null);
        }

        private void Default_OnPropertyChanged(object? sender, PropertyChangedEventArgs e) {
            Logging.Log(Logger, $"PropertyChanged : {e.PropertyName}");
            try {
                switch (e.PropertyName) {
                    case "InterfaceLanguage":
                        LanguageItem item = AppViewModel.Instance.InterfaceLanguages.FirstOrDefault(languageItem => languageItem.Language == Settings.Default.InterfaceLanguage);
                        if (item is not null) {
                            AppViewModel.Instance.CultureInfo = Settings.Default.Culture = item.CultureInfo;
                            LocaleHelper.UpdateLocale(Settings.Default.Culture);
                        }

                        break;
                }
            }
            catch (Exception ex) {
                Logging.Log(Logger, new LogItem(ex));
            }
        }

        private void Default_OnSettingChanging(object sender, SettingChangingEventArgs e) {
            Logging.Log(Logger, $"SettingChanging : [{e.SettingName},{e.NewValue}]");
        }
    }
}