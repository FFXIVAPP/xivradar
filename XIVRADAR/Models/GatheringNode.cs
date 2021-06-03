// --------------------------------------------------------------------------------------------------------------------
// <copyright file="GatheringNode.cs">
//   Copyright© 2021 Ryan Wilson
//   Licensed under the MIT license. See LICENSE.md in the solution root for full license information.
// </copyright>
// <summary>
//   GatheringNode.cs Implementation
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace XIVRADAR.Models {
    using Sharlayan.Models;

    using XIVRADAR.Enums;

    public class GatheringNode {
        public Localization Localization { get; set; } = new Localization();
        public GatheringRarity Rarity { get; set; } = GatheringRarity.Normal;
        public GatheringType Type { get; set; } = GatheringType.Unknown;
    }
}