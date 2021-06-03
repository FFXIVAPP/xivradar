// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TransparentRadarWindow.xaml.cs">
//   Copyright© 2021 Ryan Wilson
//   Licensed under the MIT license. See LICENSE.md in the solution root for full license information.
// </copyright>
// <summary>
//   TransparentRadarWindow.xaml.cs Implementation
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace XIVRADAR.Windows {
    using System.ComponentModel;
    using System.Timers;
    using System.Windows;
    using System.Windows.Input;
    using System.Windows.Threading;

    using XIVRADAR.Helpers;
    using XIVRADAR.Properties;

    /// <summary>
    /// Interaction logic for TransparentRadarWindow.xaml
    /// </summary>
    public partial class TransparentRadarWindow : Window {
        private readonly Timer _renderTimer = new Timer(100);

        private bool _isRendered;

        public TransparentRadarWindow() {
            this.InitializeComponent();

            Instance = this;
        }

        public static TransparentRadarWindow Instance { get; set; }

        private void TransparentRadarWindow_OnClosing(object sender, CancelEventArgs e) {
            Settings.Default.ShowTransparentRadarOnOpen = false;
            this._renderTimer.Elapsed -= this.RenderTimer_OnElapsed;
            this._renderTimer.Dispose();
        }

        private void TransparentRadarWindow_OnLoaded(object sender, RoutedEventArgs e) {
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

        private void CloseWindow_OnClick(object sender, RoutedEventArgs e) {
            this.Close();
        }

        private void UIElement_OnPreviewMouseDown(object sender, MouseButtonEventArgs e) {
            if (Mouse.LeftButton == MouseButtonState.Pressed) {
                this.DragMove();
            }
        }
    }
}