﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="StringToBrushConverter.cs">
//   Copyright© 2021 Ryan Wilson
//   Licensed under the MIT license. See LICENSE.md in the solution root for full license information.
// </copyright>
// <summary>
//   StringToBrushConverter.cs Implementation
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace XIVRADAR.Converters {
    using System;
    using System.Globalization;
    using System.Windows.Data;
    using System.Windows.Media;

    using NLog;

    using XIVRADAR.Models;
    using XIVRADAR.Utilities;

    public class StringToBrushConverter : IValueConverter {
        private const string HASH_STRING = "#";

        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            BrushConverter brushConverter = new BrushConverter();
            value = value.ToString().StartsWith(HASH_STRING)
                        ? value
                        : HASH_STRING + value;
            Brush result = (Brush) brushConverter.ConvertFrom("#FFFFFFFF");
            try {
                result = (Brush) brushConverter.ConvertFrom(value);
            }
            catch (Exception ex) {
                Logging.Log(Logger, new LogItem(ex));
            }

            return result;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            return new BrushConverter().ConvertFrom("#FFFFFFFF");
        }

        public object Convert(object value) {
            BrushConverter brushConverter = new BrushConverter();
            value = value.ToString()?.Substring(0, 1) == HASH_STRING
                        ? value
                        : HASH_STRING + value;
            Brush result = (Brush) brushConverter.ConvertFrom("#FFFFFFFF");
            try {
                result = (Brush) brushConverter.ConvertFrom(value);
            }
            catch (Exception ex) {
                Logging.Log(Logger, new LogItem(ex));
            }

            return result;
        }
    }
}