// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ChatCodes.xaml.cs">
//   Copyright© 2021 Ryan Wilson
//   Licensed under the MIT license. See LICENSE.md in the solution root for full license information.
// </copyright>
// <summary>
//   ChatCodes.xaml.cs Implementation
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace XIVRADAR.Controls {
    using System.Windows.Controls;

    using XIVRADAR.ViewModels;

    /// <summary>
    /// Interaction logic for ChatCodes.xaml
    /// </summary>
    public partial class ChatCodes : UserControl {
        public ChatCodes() {
            this.InitializeComponent();

            Instance = this;

            this.DataContext = ChatCodesViewModel.Instance;
        }

        public static ChatCodes Instance { get; set; }
    }
}