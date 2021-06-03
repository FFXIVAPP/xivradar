// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AppContext.cs">
//   Copyright© 2021 Ryan Wilson
//   Licensed under the MIT license. See LICENSE.md in the solution root for full license information.
// </copyright>
// <summary>
//   AppContext.cs Implementation
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace XIVRADAR.Launcher {
    using System;

    public class AppContext {
        private static Lazy<AppContext> _instance = new Lazy<AppContext>(() => new AppContext());

        public static AppContext Instance => _instance.Value;

        public GitHubRelease ReleaseInfo { get; set; }
    }
}