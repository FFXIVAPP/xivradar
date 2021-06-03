// --------------------------------------------------------------------------------------------------------------------
// <copyright file="HomeTabItemViewModel.cs">
//   Copyright© 2021 Ryan Wilson
//   Licensed under the MIT license. See LICENSE.md in the solution root for full license information.
// </copyright>
// <summary>
//   HomeTabItemViewModel.cs Implementation
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace XIVRADAR.ViewModels {
    using System;
    using System.Threading.Tasks;

    using MaterialDesignThemes.Wpf;

    using XIVRADAR.Controls;
    using XIVRADAR.Models;

    public class HomeTabItemViewModel : PropertyChangedBase {
        private static Lazy<HomeTabItemViewModel> _instance = new Lazy<HomeTabItemViewModel>(() => new HomeTabItemViewModel());

        public HomeTabItemViewModel() {
            this.AddFilterItemCommand = new DelegatedCommand(
                _ => {
                    AppViewModel.Instance.FilterItems.Add(new FilterItem());
                });

            this.SaveFilterItemCommand = new DelegatedCommand(
                _ => {
                    HomeTabItem.Instance?.FilterItemsDataGrid.CommitEdit();
                });

            this.DeleteFilterItemCommand = new DelegatedCommand(
                filterItem => {
                    if (filterItem is FilterItem item) {
                        AppViewModel.Instance.FilterItems.Remove(item);
                    }
                });

            this.EditFilterItemCommand = new DelegatedCommand(
                async filterItem => {
                    if (filterItem is not FilterItem selectedFilterItem) {
                        return;
                    }

                    FilterItemEdit view = new FilterItemEdit {
                        DataContext = selectedFilterItem,
                    };

                    //show the dialog
                    await DialogHost.Show(view, "RootDialog", null, this.ExtendedClosingEventHandler);
                });

            this.ShowRadarWindowCommand = new DelegatedCommand(
                _ => {
                    WindowManager.Instance.ShowRadarWindow();
                });

            this.ShowTransparentRadarWindowCommand = new DelegatedCommand(
                _ => {
                    WindowManager.Instance.ShowTransparentRadarWindow();
                });
        }

        public static HomeTabItemViewModel Instance => _instance.Value;

        public DelegatedCommand AddFilterItemCommand { get; }

        public DelegatedCommand ShowRadarWindowCommand { get; }

        public DelegatedCommand ShowTransparentRadarWindowCommand { get; }

        public DelegatedCommand EditFilterItemCommand { get; }

        public DelegatedCommand SaveFilterItemCommand { get; }

        public DelegatedCommand DeleteFilterItemCommand { get; }

        private void ExtendedClosingEventHandler(object sender, DialogClosingEventArgs e) {
            if (e.Parameter is bool and false) {
                return;
            }

            e.Cancel();

            if (e.Session.Content is FilterItemEdit { DataContext: FilterItem filterItem, }) {
                AppViewModel.Instance.FilterItems[AppViewModel.Instance.FilterItems.IndexOf(filterItem)] = filterItem;
            }

            Task.Delay(TimeSpan.Zero).ContinueWith((t, _) => e.Session.Close(false), null, TaskScheduler.FromCurrentSynchronizationContext());
        }
    }
}