// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Korean.cs">
//   Copyright© 2021 Ryan Wilson
//   Licensed under the MIT license. See LICENSE.md in the solution root for full license information.
// </copyright>
// <summary>
//   Korean.cs Implementation
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace XIVRADAR.Localization {
    using System.Collections.Generic;
    using System.Windows;

    public class Korean {
        private static readonly ResourceDictionary _translations = new ResourceDictionary();

        private static readonly List<string> RankA = new List<string>();

        private static readonly List<string> RankB = new List<string>();

        private static readonly List<string> RankS = new List<string>();

        public static List<string> GetRankedMonsters(string rank = "*") {
            List<string> monsters;
            switch (rank) {
                case "B":
                    monsters = RankB;
                    break;
                case "A":
                    monsters = RankA;
                    break;
                case "S":
                    monsters = RankS;
                    break;
                default:
                    monsters = new List<string>();
                    monsters.AddRange(RankB);
                    monsters.AddRange(RankA);
                    monsters.AddRange(RankS);
                    break;
            }

            return monsters;
        }
    }
}
