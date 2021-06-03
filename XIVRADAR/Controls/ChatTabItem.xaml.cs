// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ChatTabItem.xaml.cs">
//   Copyright© 2021 Ryan Wilson
//   Licensed under the MIT license. See LICENSE.md in the solution root for full license information.
// </copyright>
// <summary>
//   ChatTabItem.xaml.cs Implementation
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace XIVRADAR.Controls {
    using System;
    using System.Windows.Controls;

    using Sharlayan;
    using Sharlayan.Core;

    using XIVRADAR.Helpers;
    using XIVRADAR.SharlayanWrappers;

    /// <summary>
    /// Interaction logic for ChatTabItem.xaml
    /// </summary>
    public partial class ChatTabItem : UserControl, IDisposable {
        public static ChatTabItem Instance;

        public ChatTabItem() {
            this.InitializeComponent();

            Instance = this;

            EventHost.Instance.OnNewChatLogItem += this.OnNewChatLogItem;
        }

        public void Dispose() {
            EventHost.Instance.OnNewChatLogItem -= this.OnNewChatLogItem;
        }

        ~ChatTabItem() {
            this.Dispose();
        }

        private void OnNewChatLogItem(object? sender, MemoryHandler memoryHandler, ChatLogItem chatLogItem) {
            FlowDocHelper.AppendChatLogItem(memoryHandler, chatLogItem, this.ChatLogReader._FDR);
        }
    }
}