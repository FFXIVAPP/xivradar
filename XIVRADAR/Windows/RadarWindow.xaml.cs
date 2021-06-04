// --------------------------------------------------------------------------------------------------------------------
// <copyright file="RadarWindow.xaml.cs">
//   Copyright© 2021 Ryan Wilson
//   Licensed under the MIT license. See LICENSE.md in the solution root for full license information.
// </copyright>
// <summary>
//   RadarWindow.xaml.cs Implementation
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace XIVRADAR.Windows {
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;
    using System.Timers;
    using System.Windows;
    using System.Windows.Threading;

    using Sharlayan;
    using Sharlayan.Core;

    using XIVRADAR.Helpers;
    using XIVRADAR.Properties;
    using XIVRADAR.SharlayanWrappers;

    /// <summary>
    /// Interaction logic for RadarWindow.xaml
    /// </summary>
    public partial class RadarWindow : Window, INotifyPropertyChanged {
        private readonly int _processID;

        private readonly Timer _renderTimer = new Timer(100);

        private bool _isRendered;

        private string _windowTitle;

        public RadarWindow(int processID) {
            this._processID = processID;

            this.InitializeComponent();

            EventHost.Instance.OnNewCurrentUser += this.EventHost_OnNewCurrentUser;
        }

        public string WindowTitle {
            get => this._windowTitle ??= "RADAR";
            set => this.SetProperty(ref this._windowTitle, $"RADAR - {value}");
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        private void EventHost_OnNewCurrentUser(object sender, MemoryHandler memoryhandler, ActorItem eventdata) {
            if (memoryhandler.Configuration.ProcessModel.ProcessID == this._processID) {
                this.WindowTitle = eventdata.Name;
            }
        }

        private void RadarWindow_OnClosing(object sender, CancelEventArgs e) {
            Settings.Default.ShowRadarOnOpen = false;
            this._renderTimer.Elapsed -= this.RenderTimer_OnElapsed;
            this._renderTimer.Dispose();
        }

        private void RadarWindow_OnLoaded(object sender, RoutedEventArgs e) {
            if (this._isRendered) {
                return;
            }

            this._isRendered = true;
            this._renderTimer.Elapsed += this.RenderTimer_OnElapsed;
            this._renderTimer.Start();
        }

        private void RenderTimer_OnElapsed(object sender, ElapsedEventArgs e) {
            DispatcherHelper.Invoke(
                () => {
                    this.RadarControl.Refresh(this._processID);
                }, DispatcherPriority.Render);
        }

        /// <summary>
        /// Notifies listeners that a property value has changed.
        /// </summary>
        /// <param name="propertyName">Name of the property, used to notify listeners.</param>
        protected void OnPropertyChanged([CallerMemberName] string? propertyName = null) {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        /// <summary>
        /// Sets property if it does not equal existing value. Notifies listeners if change occurs.
        /// </summary>
        /// <typeparam name="T">Type of property.</typeparam>
        /// <param name="member">The property's backing field.</param>
        /// <param name="value">The new value.</param>
        /// <param name="propertyName">Name of the property used to notify listeners.  This
        /// value is optional and can be provided automatically when invoked from compilers
        /// that support <see cref="CallerMemberNameAttribute"/>.</param>
        protected virtual bool SetProperty<T>(ref T member, T value, [CallerMemberName] string? propertyName = null) {
            if (EqualityComparer<T>.Default.Equals(member, value)) {
                return false;
            }

            member = value;
            this.OnPropertyChanged(propertyName);
            return true;
        }
    }
}