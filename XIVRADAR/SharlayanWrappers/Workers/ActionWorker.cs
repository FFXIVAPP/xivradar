// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ActionWorker.cs">
//   Copyright© 2021 Ryan Wilson
//   Licensed under the MIT license. See LICENSE.md in the solution root for full license information.
// </copyright>
// <summary>
//   ActionWorker.cs Implementation
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace XIVRADAR.SharlayanWrappers.Workers {
    using System;
    using System.Timers;

    using Sharlayan;
    using Sharlayan.Models.ReadResults;

    using XIVRADAR.Properties;

    internal class ActionWorker : PropertyChangedBase, IDisposable {
        private readonly MemoryHandler _memoryHandler;

        private readonly Timer _scanTimer;

        private bool _isScanning;

        public ActionWorker(MemoryHandler memoryHandler) {
            this._memoryHandler = memoryHandler;
            this._scanTimer = new Timer(250);
            this._scanTimer.Elapsed += this.ScanTimerElapsed;
        }

        public void Dispose() {
            this._scanTimer.Elapsed -= this.ScanTimerElapsed;
        }

        ~ActionWorker() {
            this.Dispose();
        }

        public void StartScanning() {
            this._scanTimer.Enabled = true;
        }

        public void StopScanning() {
            this._scanTimer.Enabled = false;
        }

        private void ScanTimerElapsed(object sender, ElapsedEventArgs e) {
            if (this._isScanning) {
                return;
            }

            this._scanTimer.Interval = Settings.Default.ActionWorkerTiming;

            this._isScanning = true;

            ActionResult result = this._memoryHandler.Reader.GetActions();

            EventHost.Instance.RaiseNewActionContainersEvent(this._memoryHandler, result.ActionContainers);

            this._isScanning = false;
        }
    }
}