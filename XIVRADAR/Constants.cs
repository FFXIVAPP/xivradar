// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Constants.cs">
//   Copyright© 2021 Ryan Wilson
//   Licensed under the MIT license. See LICENSE.md in the solution root for full license information.
// </copyright>
// <summary>
//   Constants.cs Implementation
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace XIVRADAR {
    using System;
    using System.IO;

    public static class Constants {
        public const string AppPack = "pack://application:,,,/XIVRADAR;component/";

        public static readonly string[] ChatAlliance = {
            "000F",
        };

        public static readonly string[] ChatCWLS = {
            "0025",
            "0026",
            "0027",
            "0028",
            "0029",
            "002A",
            "002B",
            "002C",
        };

        public static readonly string[] ChatFC = {
            "0018",
        };

        public static readonly string[] ChatLS = {
            "0010",
            "0011",
            "0012",
            "0013",
            "0014",
            "0015",
            "0016",
            "0017",
        };

        public static readonly string[] ChatNovice = {
            "001B",
        };

        public static readonly string[] ChatParty = {
            "000E",
        };

        public static readonly string[] ChatSay = {
            "000A",
        };

        public static readonly string[] ChatShout = {
            "000B",
        };

        public static readonly string[] ChatTell = {
            "000C",
            "000D",
        };

        public static readonly string[] ChatYell = {
            "001E",
        };

        public static string CachePath {
            get {
                try {
                    string documentsFolder = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                    return Path.Combine(documentsFolder, "XIVRADAR");
                }
                catch (Exception) {
                    return Path.Combine(Directory.GetCurrentDirectory(), "AppCache");
                }
            }
        }

        public static string ConfigurationsPath => Path.Combine(CachePath, "Configurations");

        public static string LogsPath => Path.Combine(CachePath, "Logs");

        public static string SettingsPath => Path.Combine(CachePath, "Settings");
    }
}