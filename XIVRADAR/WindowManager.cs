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
    using System.Collections.Concurrent;

    using XIVRADAR.Properties;
    using XIVRADAR.Windows;

    public class WindowManager {
        private static Lazy<WindowManager> _instance = new Lazy<WindowManager>(() => new WindowManager());

        private ConcurrentDictionary<int, RadarWindow> _radarWindows = new ConcurrentDictionary<int, RadarWindow>();

        private ConcurrentDictionary<int, TransparentRadarWindow> _transparentRadarWindows = new ConcurrentDictionary<int, TransparentRadarWindow>();

        public static WindowManager Instance => _instance.Value;

        public void ShowRadarWindow(int processID) {
            if (this._radarWindows.TryGetValue(processID, out RadarWindow existing)) {
                return;
            }

            Settings.Default.ShowRadarOnOpen = true;

            RadarWindow radarWindow = new RadarWindow(processID);
            radarWindow.Show();
            radarWindow.Closed += (_, _) => {
                this._radarWindows.TryRemove(processID, out RadarWindow removed);
            };

            this._radarWindows.TryAdd(processID, radarWindow);
        }

        public void ShowTransparentRadarWindow(int processID) {
            if (this._transparentRadarWindows.TryGetValue(processID, out TransparentRadarWindow existing)) {
                return;
            }

            Settings.Default.ShowTransparentRadarOnOpen = true;

            TransparentRadarWindow transparentRadarWindow = new TransparentRadarWindow(processID);
            transparentRadarWindow.Show();
            transparentRadarWindow.Closed += (_, _) => {
                this._transparentRadarWindows.TryRemove(processID, out TransparentRadarWindow removed);
            };

            this._transparentRadarWindows.TryAdd(processID, transparentRadarWindow);
        }
    }
}