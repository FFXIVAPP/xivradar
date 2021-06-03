// --------------------------------------------------------------------------------------------------------------------
// <copyright file="FilterItem.cs">
//   Copyright© 2021 Ryan Wilson
//   Licensed under the MIT license. See LICENSE.md in the solution root for full license information.
// </copyright>
// <summary>
//   FilterItem.cs Implementation
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace XIVRADAR.Models {
    using System;
    using System.Text.RegularExpressions;

    using XIVRADAR.Enums;
    using XIVRADAR.Utilities;

    public class FilterItem : PropertyChangedBase {
        private FilterType _filterType = FilterType.Monster;

        private bool _isEnabled;

        private Guid? _key;

        private int _minLevel;

        private string _regEx;

        public Guid? Key {
            get => this._key ??= Guid.NewGuid();
            set => this.SetProperty(ref this._key, value);
        }

        public string RegEx {
            get => this._regEx;
            set {
                if (SharedRegEx.IsValidRegex(value)) {
                    this.CompiledRegEx = new Regex(value, SharedRegEx.DefaultOptions);
                }

                this.SetProperty(ref this._regEx, value);
            }
        }

        public FilterType FilterType {
            get => this._filterType;
            set => this.SetProperty(ref this._filterType, value);
        }

        public string Type {
            get => Enum.GetName(this.FilterType);
            set {
                if (Enum.TryParse(value, out FilterType filterType)) {
                    this.FilterType = filterType;
                }
            }
        }

        public int MinLevel {
            get => this._minLevel;
            set => this.SetProperty(ref this._minLevel, value);
        }

        public bool IsEnabled {
            get => this._isEnabled;
            set => this.SetProperty(ref this._isEnabled, value);
        }

        public Regex CompiledRegEx { get; set; } = new Regex(@".+", SharedRegEx.DefaultOptions);
    }
}