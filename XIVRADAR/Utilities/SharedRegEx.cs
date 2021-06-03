// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SharedRegEx.cs">
//   Copyright© 2021 Ryan Wilson
//   Licensed under the MIT license. See LICENSE.md in the solution root for full license information.
// </copyright>
// <summary>
//   SharedRegEx.cs Implementation
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace XIVRADAR.Utilities {
    using System;
    using System.Text.RegularExpressions;

    public static class SharedRegEx {
        public const RegexOptions DefaultOptions = RegexOptions.Compiled | RegexOptions.ExplicitCapture;

        public static bool IsValidRegex(string pattern) {
            if (string.IsNullOrWhiteSpace(pattern)) {
                return false;
            }

            try {
                Regex regex = new Regex(pattern, DefaultOptions);
            }
            catch (Exception) {
                return false;
            }

            return true;
        }
    }
}