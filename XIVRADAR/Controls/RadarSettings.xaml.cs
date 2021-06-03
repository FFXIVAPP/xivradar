// --------------------------------------------------------------------------------------------------------------------
// <copyright file="RadarSettings.xaml.cs">
//   Copyright© 2021 Ryan Wilson
//   Licensed under the MIT license. See LICENSE.md in the solution root for full license information.
// </copyright>
// <summary>
//   RadarSettings.xaml.cs Implementation
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace XIVRADAR.Controls {
    using System.Windows.Controls;

    using XIVRADAR.ViewModels;

    /// <summary>
    /// Interaction logic for RadarSettings.xaml
    /// </summary>
    public partial class RadarSettings : UserControl {
        public RadarSettings() {
            this.InitializeComponent();

            this.DataContext = RadarSettingsViewModel.Instance;
        }
    }
}