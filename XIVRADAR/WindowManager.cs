// --------------------------------------------------------------------------------------------------------------------
// <copyright file="WindowManager.cs">
//   Copyright© 2021 Ryan Wilson
//   Licensed under the MIT license. See LICENSE.md in the solution root for full license information.
// </copyright>
// <summary>
//   WindowManager.cs Implementation
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace XIVRADAR {
    using System;

    using XIVRADAR.Properties;
    using XIVRADAR.Windows;

    public class WindowManager {
        private static Lazy<WindowManager> _instance = new Lazy<WindowManager>(() => new WindowManager());

        private RadarWindow _radarWindow;

        private TransparentRadarWindow _transparentRadarWindow;

        public static WindowManager Instance => _instance.Value;

        public void ShowRadarWindow() {
            if (this._radarWindow is not null) {
                return;
            }

            Settings.Default.ShowRadarOnOpen = true;

            this._radarWindow = new RadarWindow();
            this._radarWindow.Show();
            this._radarWindow.Closed += (_, _) => {
                this._radarWindow = null;
            };
        }

        public void ShowTransparentRadarWindow() {
            if (this._transparentRadarWindow is not null) {
                return;
            }

            Settings.Default.ShowTransparentRadarOnOpen = true;

            this._transparentRadarWindow = new TransparentRadarWindow();
            this._transparentRadarWindow.Show();
            this._transparentRadarWindow.Closed += (_, _) => {
                this._transparentRadarWindow = null;
            };
        }
    }
}