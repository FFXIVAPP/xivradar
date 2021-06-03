// --------------------------------------------------------------------------------------------------------------------
// <copyright file="XMLHelper.cs">
//   Copyright© 2021 Ryan Wilson
//   Licensed under the MIT license. See LICENSE.md in the solution root for full license information.
// </copyright>
// <summary>
//   XMLHelper.cs Implementation
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace XIVRADAR.Helpers {
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Xml.Linq;

    public static class XMLHelper {
        public static void SaveXMLNode(XDocument xDoc, string xRoot, string xNode, string xKey, IEnumerable<KeyValuePair<string, string>> xValuePairs) {
            XElement element = xDoc.Element(xRoot);
            if (element == null) {
                return;
            }

            XElement newElement = new XElement(xNode, new XAttribute("Key", xKey));
            foreach ((string key, string value) in xValuePairs) {
                newElement.Add(new XElement(key, value));
            }

            element.Add(newElement);
        }

        internal static T DeserializeValue<T>(string value, T defaultValue) {
            if (string.IsNullOrEmpty(value)) {
                return defaultValue;
            }

            try {
                return (T) TypeDescriptor.GetConverter(typeof(T)).ConvertFromInvariantString(value);
            }
            catch (Exception) {
                return defaultValue;
            }
        }

        internal static T GetAttributeValue<T>(this XElement xElement, string attributeName, T defaultvalue) {
            XAttribute xAttribute = xElement.Attribute(attributeName);
            return xAttribute == null
                       ? defaultvalue
                       : DeserializeValue(xAttribute.Value, defaultvalue);
        }

        internal static T GetElementValue<T>(this XContainer xElement, string childElementName, T defaultvalue) {
            XElement childElement = xElement.Element(childElementName);
            return childElement == null
                       ? defaultvalue
                       : DeserializeValue(childElement.Value, defaultvalue);
        }
    }
}