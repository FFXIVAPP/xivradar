// --------------------------------------------------------------------------------------------------------------------
// <copyright file="FilterType.cs">
//   Copyright© 2021 Ryan Wilson
//   Licensed under the MIT license. See LICENSE.md in the solution root for full license information.
// </copyright>
// <summary>
//   FilterType.cs Implementation
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace XIVRADAR.Enums {
    using System;

    [Flags]
    public enum FilterType {
        Unknown,

        PC,

        NPC,

        Monster,

        Gathering,

        Aetheryte,

        Minion,
    }
}