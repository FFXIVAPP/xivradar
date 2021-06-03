// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ResourceHelper.cs">
//   Copyright© 2021 Ryan Wilson
//   Licensed under the MIT license. See LICENSE.md in the solution root for full license information.
// </copyright>
// <summary>
//   ResourceHelper.cs Implementation
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace XIVRADAR.Helpers {
    using System;
    using System.Windows;
    using System.Windows.Resources;
    using System.Xml.Linq;

    public static class ResourceHelper {
        public static StreamResourceInfo GetStreamResource(string path) {
            return Application.GetResourceStream(new Uri(path));
        }

        public static XDocument LoadXML(string path) {
            StreamResourceInfo resource = GetStreamResource(path);
            return resource == null
                       ? null
                       : new XDocument(XElement.Load(resource.Stream));
        }
    }
}