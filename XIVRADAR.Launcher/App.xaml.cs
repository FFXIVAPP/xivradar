// --------------------------------------------------------------------------------------------------------------------
// <copyright file="App.xaml.cs">
//   Copyright© 2021 Ryan Wilson
//   Licensed under the MIT license. See LICENSE.md in the solution root for full license information.
// </copyright>
// <summary>
//   App.xaml.cs Implementation
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace XIVRADAR.Launcher {
    using System;
    using System.Diagnostics;
    using System.Windows;

    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application {
        public App() {
            this.Startup += this.OnStartup;

            this.InitializeComponent();
        }

        private void LaunchApplication() {
            try {
                Process process = new Process {
                    StartInfo = {
                        FileName = "XIVRADAR.exe",
                    },
                };
                process.Start();
            }
            catch (Exception ex) {
                MessageBox.Show($"{ex.Message} [XIVRADAR.exe]", "Exception");
            }
            finally {
                Environment.Exit(0);
            }
        }

        private void OnStartup(object sender, StartupEventArgs e) {
            GitHubRelease currentRelease = GitHub.GetCurrentRelease();
            if (currentRelease is null) {
                this.LaunchApplication();
            }
            else {
                AppContext.Instance.ReleaseInfo = currentRelease;
            }
        }
    }
}