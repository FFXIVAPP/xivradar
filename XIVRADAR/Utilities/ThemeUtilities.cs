// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ThemeUtilities.cs">
//   Copyright© 2021 Ryan Wilson
//   Licensed under the MIT license. See LICENSE.md in the solution root for full license information.
// </copyright>
// <summary>
//   ThemeUtilities.cs Implementation
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace XIVRADAR.Utilities {
    using System;

    using MaterialDesignThemes.Wpf;

    public static class ThemeUtilities {
        public static void ModifyTheme(Action<ITheme> modificationAction) {
            PaletteHelper paletteHelper = new PaletteHelper();
            ITheme theme = paletteHelper.GetTheme();
            modificationAction?.Invoke(theme);
            paletteHelper.SetTheme(theme);
        }
    }
}