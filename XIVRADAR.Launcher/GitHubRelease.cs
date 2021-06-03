// --------------------------------------------------------------------------------------------------------------------
// <copyright file="GitHubRelease.cs">
//   Copyright© 2021 Ryan Wilson
//   Licensed under the MIT license. See LICENSE.md in the solution root for full license information.
// </copyright>
// <summary>
//   GitHubRelease.cs Implementation
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace XIVRADAR.Launcher {
    using System.Collections.Generic;

    public class GitHubRelease {
        public List<GitHubReleaseAsset> assets { get; set; }
        public string tag_name { get; set; }
        public string target_commitish { get; set; }
    }
}