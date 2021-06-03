// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ChatCodesViewModel.cs">
//   Copyright© 2021 Ryan Wilson
//   Licensed under the MIT license. See LICENSE.md in the solution root for full license information.
// </copyright>
// <summary>
//   ChatCodesViewModel.cs Implementation
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace XIVRADAR.ViewModels {
    using System;

    using XIVRADAR.Controls;

    public class ChatCodesViewModel {
        private static Lazy<ChatCodesViewModel> _instance = new Lazy<ChatCodesViewModel>(() => new ChatCodesViewModel());

        public ChatCodesViewModel() {
            this.SaveChatCodesCommand = new DelegatedCommand(
                _ => {
                    ChatCodes.Instance?.ChatCodesDataGrid.CommitEdit();
                });
        }

        public static ChatCodesViewModel Instance => _instance.Value;

        public DelegatedCommand SaveChatCodesCommand { get; }
    }
}