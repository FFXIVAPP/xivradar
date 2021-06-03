// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SharlayanSettings.xaml.cs">
//   Copyright© 2021 Ryan Wilson
//   Licensed under the MIT license. See LICENSE.md in the solution root for full license information.
// </copyright>
// <summary>
//   SharlayanSettings.xaml.cs Implementation
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace XIVRADAR.Controls {
    using System.Windows.Controls;

    using XIVRADAR.ViewModels;

    /// <summary>
    /// Interaction logic for Settings.xaml
    /// </summary>
    public partial class SharlayanSettings : UserControl {
        public SharlayanSettings() {
            this.InitializeComponent();

            this.DataContext = SharlayanSettingsViewModel.Instance;
        }
    }
}