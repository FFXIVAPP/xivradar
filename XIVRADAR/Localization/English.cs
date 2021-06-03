// --------------------------------------------------------------------------------------------------------------------
// <copyright file="English.cs">
//   Copyright© 2021 Ryan Wilson
//   Licensed under the MIT license. See LICENSE.md in the solution root for full license information.
// </copyright>
// <summary>
//   English.cs Implementation
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace XIVRADAR.Localization {
    using System.Collections.Generic;
    using System.Windows;

    public class English {
        private static readonly ResourceDictionary _translations = new ResourceDictionary();

        private static readonly List<string> RankA = new List<string> {
            "Hellsclaw",
            "Unktehi",
            "Vogaal Ja",
            "Cornu",
            "Marberry",
            "Nahn",
            "Forneus",
            "Melt",
            "Girtab",
            "Ghede Ti Malice",
            "Marraco",
            "Sabotender Bailarina",
            "Maahes",
            "Dalvag's Final Flame",
            "Zanig'oh",
            "Alectryon",
            "Kurrea",

            // Heavensward Rank A
            "Mirka",
            "Lyuba",
            "Bune",
            "Agathos",
            "Pylraster",
            "Lord of the Wyverns",
            "Slipkinx Steeljoints",
            "Stolas",
            "Campacti",
            "Stench Blossom",
            "Enkelados",
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
            "Albin the Ashen",
            "Barbastelle",
            "Bloody Mary",
            "Dark Helmet",
            "Flame Sergeant Dalvag",
            "Gatling",
            "Leech King",
            "Monarch Ogrefly",
            "Myradrosh",
            "Naul",
            "Ovjang",
            "Phecda",
            "Sewer Syrup",
            "Skogs Fru",
            "Stinging Sophie",
            "Vuokho",
            "White Joker",

            // Heavensward Rank B
            "Alteci",
            "False Gigantopithecus",
            "Gnath Cometdrone",
            "Kreutzet",
            "Lycidas",
            "Omni",
            "Pterygotus",
            "Sanu Vali of Dancing Wings",
            "Scitalis",
            "Squonk",
            "The Scarecrow",
            "Thextera",

            // Stormblood Rank B
            "Aswang",
            "Buccaboo",
            "Deidar",
            "Gauki Strongblade",
            "Guhuo Niao",
            "Gwas-y-neidr",
            "Gyorai Quickstrike",
            "Kiwa",
            "Kurma",
            "Manes",
            "Ouzelum",
            "Shadow-dweller Yamini",
        };

        private static readonly List<string> RankS = new List<string> {
            "Garlok",
            "Croakadile",
            "Croque-Mitaine",
            "Chernobog",
            "Nandi",
            "Bonnacon",
            "Laideronnette",
            "Wulgaru",
            "Thousand-cast Theda",
            "Mindflayer",
            "Safat",
            "Brontes",
            "Lampalagua",
            "Minhocao",
            "Nunyunuwi",
            "Zona Seeker",
            "Agrippa the Mighty",

            // Heavensward Rank S
            "Kaiser Behemoth",
            "Gandarewa",
            "Senmurv",
            "The Pale Rider",
            "Leucrotta",
            "Bird of Paradise",

            // Stormblood Rank S
            "Bone Crawler",
            "Gamma",
            "Okina",
            "Orghana",
            "Salt and Light",
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
