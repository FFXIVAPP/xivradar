// --------------------------------------------------------------------------------------------------------------------
// <copyright file="HomeTabItem.xaml.cs">
//   Copyright© 2021 Ryan Wilson
//   Licensed under the MIT license. See LICENSE.md in the solution root for full license information.
// </copyright>
// <summary>
//   HomeTabItem.xaml.cs Implementation
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace XIVRADAR.Controls {
    using System.Windows.Controls;

    using XIVRADAR.ViewModels;

    /// <summary>
    /// Interaction logic for HomeTabItem.xaml
    /// </summary>
    public partial class HomeTabItem : UserControl {
        public HomeTabItem() {
            this.InitializeComponent();

            Instance = this;

            this.DataContext = HomeTabItemViewModel.Instance;
        }

        public static HomeTabItem Instance { get; set; }
    }
}