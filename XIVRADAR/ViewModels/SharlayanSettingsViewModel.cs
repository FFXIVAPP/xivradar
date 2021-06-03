// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SharlayanSettingsViewModel.cs">
//   Copyright© 2021 Ryan Wilson
//   Licensed under the MIT license. See LICENSE.md in the solution root for full license information.
// </copyright>
// <summary>
//   SharlayanSettingsViewModel.cs Implementation
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace XIVRADAR.ViewModels {
    using System;

    using XIVRADAR.Properties;

    public class SharlayanSettingsViewModel {
        private static Lazy<SharlayanSettingsViewModel> _instance = new Lazy<SharlayanSettingsViewModel>(() => new SharlayanSettingsViewModel());

        public SharlayanSettingsViewModel() {
            this.ResetSettingsCommand = new DelegatedCommand(
                _ => {
                    Settings.Default.Top = 20;
                    Settings.Default.Left = 20;
                    Settings.Default.Reset();
                });
        }

        public static SharlayanSettingsViewModel Instance => _instance.Value;
        public DelegatedCommand ResetSettingsCommand { get; }
    }
}