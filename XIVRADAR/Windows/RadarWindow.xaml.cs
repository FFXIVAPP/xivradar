// --------------------------------------------------------------------------------------------------------------------
// <copyright file="RadarWindow.xaml.cs">
//   Copyright© 2021 Ryan Wilson
//   Licensed under the MIT license. See LICENSE.md in the solution root for full license information.
// </copyright>
// <summary>
//   RadarWindow.xaml.cs Implementation
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace XIVRADAR.Windows {
    using System.ComponentModel;
    using System.Timers;
    using System.Windows;
    using System.Windows.Threading;

    using XIVRADAR.Helpers;
    using XIVRADAR.Properties;

    /// <summary>
    /// Interaction logic for RadarWindow.xaml
    /// </summary>
    public partial class RadarWindow : Window {
        private readonly Timer _renderTimer = new Timer(10);

        private bool _isRendered;

        public RadarWindow() {
            this.InitializeComponent();

            Instance = this;
        }

        public static RadarWindow Instance { get; set; }

        private void RadarWindow_OnClosing(object sender, CancelEventArgs e) {
            Settings.Default.ShowRadarOnOpen = false;
            this._renderTimer.Elapsed -= this.RenderTimer_OnElapsed;
            this._renderTimer.Dispose();
        }

        private void RadarWindow_OnLoaded(object sender, RoutedEventArgs e) {
            if (this._isRendered) {
                return;
            }

            this._isRendered = true;
            this._renderTimer.Elapsed += this.RenderTimer_OnElapsed;
            this._renderTimer.Start();
        }

        private void RenderTimer_OnElapsed(object sender, ElapsedEventArgs e) {
            DispatcherHelper.Invoke(Instance.RadarControl.Refresh, DispatcherPriority.Render);
        }
    }
}