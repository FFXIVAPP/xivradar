// --------------------------------------------------------------------------------------------------------------------
// <copyright file="JobResourceWorker.cs">
//   Copyright© 2021 Ryan Wilson
//   Licensed under the MIT license. See LICENSE.md in the solution root for full license information.
// </copyright>
// <summary>
//   JobResourceWorker.cs Implementation
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace XIVRADAR.SharlayanWrappers.Workers {
    using System;
    using System.Timers;

    using Sharlayan;
    using Sharlayan.Models.ReadResults;

    using XIVRADAR.Properties;

    internal class JobResourceWorker : PropertyChangedBase, IDisposable {
        private readonly MemoryHandler _memoryHandler;

        private readonly Timer _scanTimer;

        private bool _isScanning;

        public JobResourceWorker(MemoryHandler memoryHandler) {
            this._memoryHandler = memoryHandler;
            this._scanTimer = new Timer(250);
            this._scanTimer.Elapsed += this.ScanTimerElapsed;
        }

        public void Dispose() {
            this._scanTimer.Elapsed -= this.ScanTimerElapsed;
        }

        ~JobResourceWorker() {
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

            this._scanTimer.Interval = Settings.Default.JobResourceWorkerTiming;

            this._isScanning = true;

            JobResourceResult result = this._memoryHandler.Reader.GetJobResources();

            EventHost.Instance.RaiseNewJobResourcesContainerEvent(this._memoryHandler, result.JobResourcesContainer);

            this._isScanning = false;
        }
    }
}