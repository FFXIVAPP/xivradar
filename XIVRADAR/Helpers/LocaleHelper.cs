// --------------------------------------------------------------------------------------------------------------------
// <copyright file="LocaleHelper.cs">
//   Copyright© 2021 Ryan Wilson
//   Licensed under the MIT license. See LICENSE.md in the solution root for full license information.
// </copyright>
// <summary>
//   LocaleHelper.cs Implementation
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace XIVRADAR.Helpers {
    using System.Collections;
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using System.Resources;
    using System.Threading;

    using XIVRADAR.Enums;
    using XIVRADAR.Localization;
    using XIVRADAR.Models;
    using XIVRADAR.Properties;
    using XIVRADAR.ViewModels;

    public static class LocaleHelper {
        public static List<string> GetRankedMonsters(string rank = "*") {
            string culture = Settings.Default.Culture.TwoLetterISOLanguageName;
            List<string> monsters;

            switch (culture) {
                case "fr":
                    monsters = French.GetRankedMonsters(rank);
                    break;
                case "ja":
                    monsters = Japanese.GetRankedMonsters(rank);
                    break;
                case "de":
                    monsters = German.GetRankedMonsters(rank);
                    break;
                case "zh":
                    monsters = Chinese.GetRankedMonsters(rank);
                    break;
                case "ko":
                    monsters = Korean.GetRankedMonsters(rank);
                    break;
                default:
                    monsters = English.GetRankedMonsters(rank);
                    break;
            }

            return monsters;
        }

        public static void UpdateLocale(CultureInfo cultureInfo) {
            Thread.CurrentThread.CurrentUICulture = cultureInfo;

            ResourceSet? baseResourceSet = Resources.ResourceManager.GetResourceSet(new CultureInfo("en"), true, true);
            ResourceSet? resourceSet = Resources.ResourceManager.GetResourceSet(cultureInfo, true, true);

            if (baseResourceSet is not null && resourceSet is not null) {
                ConcurrentDictionary<string, string> baseCultureDictionary = new ConcurrentDictionary<string, string>(baseResourceSet.Cast<DictionaryEntry>().ToDictionary(item => (string) item.Key, item => (string) item.Value));
                ConcurrentDictionary<string, string> locale = new ConcurrentDictionary<string, string>(resourceSet.Cast<DictionaryEntry>().ToDictionary(item => (string) item.Key, item => (string) item.Value));

                foreach ((string key, string value) in baseCultureDictionary) {
                    locale.AddOrUpdate(key, value, (k, v) => v);
                }

                AppViewModel.Instance.Locale = locale;
            }

            AppViewModel.Instance.RankedMonsters.Clear();

            List<string> monsters = GetRankedMonsters();

            foreach (string monster in monsters) {
                AppViewModel.Instance.RankedMonsters.Add(
                    new FilterItem {
                        RegEx = monster,
                        MinLevel = 0,
                        FilterType = FilterType.Monster,
                    });
            }
        }
    }
}