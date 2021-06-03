// --------------------------------------------------------------------------------------------------------------------
// <copyright file="LanguageItem.cs">
//   Copyright© 2021 Ryan Wilson
//   Licensed under the MIT license. See LICENSE.md in the solution root for full license information.
// </copyright>
// <summary>
//   LanguageItem.cs Implementation
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace XIVRADAR.Models {
    using System.Globalization;

    public class LanguageItem {
        public CultureInfo CultureInfo { get; set; }
        public string ImageURI { get; set; }
        public string Language { get; set; }
        public string Title { get; set; }
    }
}