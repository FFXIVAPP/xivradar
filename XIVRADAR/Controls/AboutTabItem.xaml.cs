// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AboutTabItem.xaml.cs">
//   Copyright© 2021 Ryan Wilson
//   Licensed under the MIT license. See LICENSE.md in the solution root for full license information.
// </copyright>
// <summary>
//   AboutTabItem.xaml.cs Implementation
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace XIVRADAR.Controls {
    using System.Windows;
    using System.Windows.Controls;

    using XIVRADAR.Utilities;

    /// <summary>
    /// Interaction logic for HomeTabItem.xaml
    /// </summary>
    public partial class AboutTabItem : UserControl {
        public AboutTabItem() {
            this.InitializeComponent();
        }

        private void ChatButton_OnClick(object sender, RoutedEventArgs e) {
            Link.OpenInBrowser("https://discord.gg/aCzSANp");
        }

        private void DonateButton_OnClick(object sender, RoutedEventArgs e) {
            Link.OpenInBrowser("https://github.com/sponsors/Icehunter");
        }

        private void GitHubButton_OnClick(object sender, RoutedEventArgs e) {
            Link.OpenInBrowser("https://github.com/FFXIVAPP");
        }
    }
}