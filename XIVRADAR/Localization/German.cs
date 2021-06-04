// --------------------------------------------------------------------------------------------------------------------
// <copyright file="German.cs">
//   Copyright© 2021 Ryan Wilson
//   Licensed under the MIT license. See LICENSE.md in the solution root for full license information.
// </copyright>
// <summary>
//   German.cs Implementation
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace XIVRADAR.Localization {
    using System.Collections.Generic;
    using System.Windows;

    public class German {
        private static readonly ResourceDictionary _translations = new ResourceDictionary();

        private static readonly List<string> RankA = new List<string> {
            "Alectryon",
            "Cornu",
            "Dalvags Letzte Flamme",
            "Forneus",
            "Ghede Titus Häme",
            "Girtab",
            "Höllenklaue",
            "Kurrea",
            "Mahisha",
            "Marberry",
            "Marraco",
            "Nahn",
            "Sabotender Bailarina",
            "Schmelze",
            "Unktehi",
            "Vogaal Ja",
            "Zanig'oh",

            // Heavensward Rank A
            "Mirka",
            "Lyuba",
            "Bune",
            "Agathos",
            "Xhauron",
            "Wyvern-Lord",
            "Rutschfix Stahlscharnier",
            "Stolas",
            "Campacti",
            "Pestwurz",
            "Engedoras",
            "Sisiutl",

            // Stormblood Rank A
            "Angada",
            "Aqrabuamelu",
            "Erle",
            "Funa Yurei",
            "Gajasura",
            "Girimekhala",
            "Luminare",
            "Mahisha",
            "Oni Yumemi",
            "Orcus",
            "Sum",
            "Vochstein",
        };

        private static readonly List<string> RankB = new List<string> {
            "Abwasser-Sirup",
            "Albin Aschfahl",
            "Barbasteile",
            "Bloody Mary",
            "Dalvag",
            "Dunkelhelm",
            "Egelkönig",
            "Gatling",
            "Monarch Ogerlibelle",
            "Myradrosh",
            "Naul",
            "Ovjang",
            "Phecda",
            "Skogs Fru",
            "Stinkige Sophie",
            "Vuokho",
            "Weißer Joker",

            // Heavensward Rank B
            "Artic",
            "Gigantopithecus",
            "Gnath-Kometdrohne",
            "Kreutzet",
            "Lykidas",
            "Omni",
            "Pterygotus",
            "Sanu Vali die Tanzende Schwinge",
            "Scitalis",
            "Squonk",
            "Tyranno",
            "Dexter",

            // Stormblood Rank B
            "Aswang",
            "Buccaboo",
            "Deidar",
            "Gauki Starkklinge",
            "Guhuo Niao",
            "Gwas-y-neidr",
            "Gyorai Schnellschlag",
            "Kiwa",
            "Kurma",
            "Manes",
            "Ouzelum",
            "Schattenkauer-Yamini",
        };

        private static readonly List<string> RankS = new List<string> {
            "Agrippa",
            "Balaur",
            "Bonnacon",
            "Brontes",
            "Croque Mitaine",
            "Czernobog",
            "Garlok",
            "Laideronette",
            "Minhocao",
            "Nandi",
            "Nunyunuwi",
            "Quakpuak",
            "Safat",
            "Seelenbrenner",
            "Tausendzahn Theda",
            "Wulgaru",
            "Laideronette",
            "Zona Sucher",

            // Heavensward Rank S
            "Kaiser-Behemoth",
            "Gandalva",
            "Simurgh",
            "Bleich Reiter",
            "Leucrotta",
            "Paradiesvogel",

            // Stormblood Rank S
            "Gamma",
            "Knochenkriecher",
            "Okina",
            "Orghana",
            "Salzlicht",
            "Udumbara",
        };

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