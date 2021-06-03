// --------------------------------------------------------------------------------------------------------------------
// <copyright file="French.cs">
//   Copyright© 2021 Ryan Wilson
//   Licensed under the MIT license. See LICENSE.md in the solution root for full license information.
// </copyright>
// <summary>
//   French.cs Implementation
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace XIVRADAR.Localization {
    using System.Collections.Generic;
    using System.Windows;

    public class French {
        private static readonly ResourceDictionary _translations = new ResourceDictionary();

        private static readonly List<string> RankA = new List<string> {
            "Griffe Des Enfers Magitek",
            "Unktehi",
            "Vogaal Ja",
            "Cornu",
            "Marberry",
            "Nahn",
            "Forneus",
            "Fondu",
            "Girtab",
            "Guédé Ti-Malice",
            "Marraco",
            "Pampa Ballerine",
            "Maahes",
            "Dernière Flamme De Dalvag",
            "Zanig'oh",
            "Alectryon",
            "Kurrea",

            // Heavensward Rank A
            "Mirka",
            "Lyuba",
            "Bune",
            "Agathos",
            "Pirlasta",
            "Seigneur Des Wyvernes",
            "Slipkinx Joints-d'acier",
            "Stolas",
            "Campacti",
            "Fleur Nauséabonde",
            "Enkélados",
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
            "Albin Le Blafard",
            "Barbastelle",
            "Mary La Sanglante",
            "Casque Noir",
            "Sergent-major Dalvag",
            "Acanthor",
            "Roi Des Sangsues",
            "Agrion Ogre Monarque",
            "Myradrosh",
            "Naul",
            "Ovjang",
            "Phecda",
            "Syrop D'égout",
            "Skogs Fru",
            "Sophie La Dardante",
            "Vuokho",
            "Joker Blanc",

            // Heavensward Rank B
            "Altek",
            "Gigantopithèque",
            "Gnathe Myrmicomète",
            "Kreutzet",
            "Lycidas",
            "Omni",
            "Pterygotus",
            "Sanu Vali",
            "Scitalis",
            "Squonk",
            "Dracosaure Primus",
            "Texta",

            // Stormblood Rank B
            "Aswang",
            "Bucca-boo",
            "Deidar",
            "Gauka La Lame Forte",
            "Guhuo Niao",
            "Gwas-y-neidr",
            "Gyorai Le Vif",
            "Kiwa",
            "Kurma",
            "Manes",
            "Ouzelum",
            "Yamini La Nocturne",
        };

        private static readonly List<string> RankS = new List<string> {
            "Garlok",
            "Croabéros",
            "Croque-Mitaine",
            "Czernobog",
            "Nandi",
            "Bonnacon",
            "Laideronnette",
            "Wulgaru",
            "Theda La Tripoteuse",
            "Flagelleur Mental",
            "Safat",
            "Brontes",
            "Balaur",
            "Minhocao",
            "Nunyunuwi",
            "Zona Seeker",
            "Agrippa the Mighty",

            // Heavensward Rank S
            "Béhémoth Empereur",
            "Gandharva",
            "Sênmurw",
            "Cavalier Pâle",
            "Leucrotta",
            "Oiseau De Paradis",

            // Stormblood Rank S
            "Gamma",
            "Mangeur D'os",
            "Okina",
            "Orghana",
            "Salaclux",
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
