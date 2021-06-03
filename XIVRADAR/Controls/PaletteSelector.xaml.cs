// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PaletteSelector.xaml.cs">
//   Copyright© 2021 Ryan Wilson
//   Licensed under the MIT license. See LICENSE.md in the solution root for full license information.
// </copyright>
// <summary>
//   PaletteSelector.xaml.cs Implementation
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace XIVRADAR.Controls {
    using System.Windows.Controls;

    using XIVRADAR.ViewModels;

    /// <summary>
    /// Interaction logic for PaletteSelector.xaml
    /// </summary>
    public partial class PaletteSelector : UserControl {
        public PaletteSelector() {
            this.InitializeComponent();

            this.DataContext = PaletteSelectorViewModel.Instance;
        }
    }
}