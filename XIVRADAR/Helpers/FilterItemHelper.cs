// --------------------------------------------------------------------------------------------------------------------
// <copyright file="FilterItemHelper.cs">
//   Copyright© 2021 Ryan Wilson
//   Licensed under the MIT license. See LICENSE.md in the solution root for full license information.
// </copyright>
// <summary>
//   FilterItemHelper.cs Implementation
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace XIVRADAR.Helpers {
    using System.Collections.Generic;

    using Sharlayan.Core;
    using Sharlayan.Core.Enums;

    using XIVRADAR.Models;

    public static class FilterItemHelper {
        public static List<ActorItem> CleanupEntities(IEnumerable<ActorItem> entities, uint mapIndex) {
            List<ActorItem> filtered = new List<ActorItem>();
            foreach (ActorItem actorEntity in entities) {
                bool correctMap = mapIndex == actorEntity.MapIndex;
                bool isDead = actorEntity.ActionStatus != Actor.ActionStatus.Dead;

                if (isDead && correctMap) {
                    filtered.Add(actorEntity);
                }
            }

            return filtered;
        }

        public static List<ActorItem> ResolveFilteredEntities(List<FilterItem> filters, IEnumerable<ActorItem> entities) {
            List<ActorItem> filtered = new List<ActorItem>();
            foreach (ActorItem actorEntity in entities) {
                foreach (FilterItem radarFilterItem in filters) {
                    if (radarFilterItem.CompiledRegEx.IsMatch(actorEntity.Name) && actorEntity.Level >= radarFilterItem.MinLevel) {
                        filtered.Add(actorEntity);
                    }
                }
            }

            return filtered;
        }
    }
}