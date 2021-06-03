// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DebugTabItem.xaml.cs">
//   Copyright© 2021 Ryan Wilson
//   Licensed under the MIT license. See LICENSE.md in the solution root for full license information.
// </copyright>
// <summary>
//   DebugTabItem.xaml.cs Implementation
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace XIVRADAR.Controls {
    using System.Windows.Controls;

    /// <summary>
    /// Interaction logic for DebugTabItem.xaml
    /// </summary>
    public partial class DebugTabItem : UserControl {
        public static DebugTabItem Instance;

        public DebugTabItem() {
            this.InitializeComponent();

            Instance = this;
        }
    }
}