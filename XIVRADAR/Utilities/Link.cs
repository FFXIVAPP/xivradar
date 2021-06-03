// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Link.cs">
//   Copyright© 2021 Ryan Wilson
//   Licensed under the MIT license. See LICENSE.md in the solution root for full license information.
// </copyright>
// <summary>
//   Link.cs Implementation
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace XIVRADAR.Utilities {
    using System.Diagnostics;
    using System.Runtime.InteropServices;

    public static class Link {
        public static void OpenInBrowser(string url) {
            //https://github.com/dotnet/corefx/issues/10361
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows)) {
                url = url.Replace("&", "^&");
                Process.Start(
                    new ProcessStartInfo("cmd", $"/c start {url}") {
                        CreateNoWindow = true,
                    });
            }
        }
    }
}