// --------------------------------------------------------------------------------------------------------------------
// <copyright file="RadarSettingsViewModel.cs">
//   Copyright© 2021 Ryan Wilson
//   Licensed under the MIT license. See LICENSE.md in the solution root for full license information.
// </copyright>
// <summary>
//   RadarSettingsViewModel.cs Implementation
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace XIVRADAR.ViewModels {
    using System;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Reflection;
    using System.Windows.Media;

    public class RadarSettingsViewModel {
        private static Lazy<RadarSettingsViewModel> _instance = new Lazy<RadarSettingsViewModel>(() => new RadarSettingsViewModel());

        public RadarSettingsViewModel() {
            this.FontColors = new ObservableCollection<string>(typeof(Brushes).GetProperties(BindingFlags.Public | BindingFlags.Static).Where(propInfo => propInfo.PropertyType == typeof(SolidColorBrush)).Select(propInfo => propInfo.Name));
        }

        public ObservableCollection<string> FontColors { get; }

        public static RadarSettingsViewModel Instance => _instance.Value;
    }
}