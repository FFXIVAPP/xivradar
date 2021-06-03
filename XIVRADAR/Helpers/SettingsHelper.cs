// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SettingsHelper.cs">
//   Copyright© 2021 Ryan Wilson
//   Licensed under the MIT license. See LICENSE.md in the solution root for full license information.
// </copyright>
// <summary>
//   SettingsHelper.cs Implementation
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace XIVRADAR.Helpers {
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Xml.Linq;

    using XIVRADAR.Models;
    using XIVRADAR.ViewModels;

    public static class SettingsHelper {
        public static void SaveChatCodes() {
            IEnumerable<XElement> xElements = AppViewModel.Instance.XChatCodes.Descendants().Elements("Code");
            XElement[] enumerable = xElements as XElement[] ?? xElements.ToArray();

            foreach (ChatCode chatCode in AppViewModel.Instance.ChatCodes) {
                XElement element = enumerable.FirstOrDefault(e => e.Attribute("Key")?.Value == chatCode.Code);

                string xKey = chatCode.Code;
                string xColor = chatCode.Color;
                string xDescription = chatCode.Description;

                List<KeyValuePair<string, string>> keyValuePairs = new List<KeyValuePair<string, string>>();

                keyValuePairs.Add(new KeyValuePair<string, string>(xKey, xColor));
                keyValuePairs.Add(new KeyValuePair<string, string>(xKey, xDescription));

                if (element is null) {
                    XMLHelper.SaveXMLNode(AppViewModel.Instance.XChatCodes, "Codes", "Code", xKey, keyValuePairs);
                }
                else {
                    XElement xColorElement = element.Element("Color");
                    if (xColorElement is not null) {
                        xColorElement.Value = xColor;
                    }
                    else {
                        element.Add(new XElement("Color", xColor));
                    }

                    XElement xDescriptionElement = element.Element("Description");
                    if (xDescriptionElement is not null) {
                        xDescriptionElement.Value = xDescription;
                    }
                    else {
                        element.Add(new XElement("Description", xDescription));
                    }
                }
            }

            AppViewModel.Instance.XChatCodes.Save(Path.Combine(AppViewModel.Instance.ConfigurationsPath, "ChatCodes.xml"));
        }

        public static void SaveFilters() {
            AppViewModel.Instance.XFilters.Descendants("Filter").Where(node => AppViewModel.Instance.FilterItems.All(item => item.Key?.ToString() != node.Attribute("Key")?.Value)).Remove();
            IEnumerable<XElement> xElements = AppViewModel.Instance.XFilters.Descendants().Elements("Filter");
            XElement[] enumerable = xElements as XElement[] ?? xElements.ToArray();

            foreach (FilterItem filterItem in AppViewModel.Instance.FilterItems) {
                XElement element = enumerable.FirstOrDefault(e => e.Attribute("Key")?.Value.ToString() == filterItem.Key.ToString());

                string xKeyString = filterItem.Key.ToString();
                string xIsEnabled = filterItem.IsEnabled.ToString();
                string xRegEx = filterItem.RegEx;
                string xType = filterItem.FilterType.ToString();
                string xMinLevel = filterItem.MinLevel.ToString();

                List<KeyValuePair<string, string>> keyValuePairs = new List<KeyValuePair<string, string>>();

                keyValuePairs.Add(new KeyValuePair<string, string>("IsEnabled", xIsEnabled));
                keyValuePairs.Add(new KeyValuePair<string, string>("RegEx", xRegEx));
                keyValuePairs.Add(new KeyValuePair<string, string>("Type", xType));
                keyValuePairs.Add(new KeyValuePair<string, string>("MinLevel", xMinLevel));

                if (element is null) {
                    XMLHelper.SaveXMLNode(AppViewModel.Instance.XFilters, "Filters", "Filter", xKeyString, keyValuePairs);
                }
                else {
                    XElement xIsEnabledElement = element.Element("IsEnabled");
                    if (xIsEnabledElement is not null) {
                        xIsEnabledElement.Value = xIsEnabled;
                    }
                    else {
                        element.Add(new XElement("IsEnabled", xIsEnabled));
                    }

                    XElement xRegExElement = element.Element("RegEx");
                    if (xRegExElement is not null) {
                        xRegExElement.Value = xRegEx;
                    }
                    else {
                        element.Add(new XElement("RegEx", xRegEx));
                    }

                    XElement xTypeElement = element.Element("Type");
                    if (xTypeElement is not null) {
                        xTypeElement.Value = xType;
                    }
                    else {
                        element.Add(new XElement("Type", xType));
                    }

                    XElement xMinLevelElement = element.Element("MinLevel");
                    if (xMinLevelElement is not null) {
                        xMinLevelElement.Value = xMinLevel;
                    }
                    else {
                        element.Add(new XElement("MinLevel", xMinLevel));
                    }
                }
            }

            AppViewModel.Instance.XFilters.Save(Path.Combine(AppViewModel.Instance.SettingsPath, "Filters.xml"));
        }
    }
}