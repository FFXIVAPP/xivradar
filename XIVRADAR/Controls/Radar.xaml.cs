// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Radar.xaml.cs">
//   Copyright© 2021 Ryan Wilson
//   Licensed under the MIT license. See LICENSE.md in the solution root for full license information.
// </copyright>
// <summary>
//   Radar.xaml.cs Implementation
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace XIVRADAR.Controls {
    using System;
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using System.Text;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Media;
    using System.Windows.Media.Imaging;

    using NLog;

    using Sharlayan;
    using Sharlayan.Core;
    using Sharlayan.Core.Enums;

    using XIVRADAR.Enums;
    using XIVRADAR.Helpers;
    using XIVRADAR.Models;
    using XIVRADAR.Properties;
    using XIVRADAR.SharlayanWrappers;
    using XIVRADAR.Utilities;
    using XIVRADAR.ViewModels;

    /// <summary>
    /// Interaction logic for Radar.xaml
    /// </summary>
    public partial class Radar : UserControl, IDisposable {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        private BrushConverter _brushConverter = new BrushConverter();

        private CultureInfo _cultureInfo = CultureInfo.InvariantCulture;

        private FlowDirection _flowDirection = FlowDirection.LeftToRight;

        private bool _isRendered;

        private int _processID;

        private Typeface _typeface = new Typeface("Verdana");

        public Radar() {
            this.InitializeComponent();

            if (this._isRendered) {
                return;
            }

            this._isRendered = true;

            EventHost.Instance.OnNewCurrentUser += this.Radar_OnNewCurrentUser;
            EventHost.Instance.OnNewPCActorItems += this.Radar_OnNewPCActorItems;
            EventHost.Instance.OnNewNPCActorItems += this.Radar_OnNewNPCActorItems;
            EventHost.Instance.OnNewMonsterActorItems += this.Radar_OnNewMonsterActorItems;

            this.RankBMonsters = LocaleHelper.GetRankedMonsters("B");
            this.RankAMonsters = LocaleHelper.GetRankedMonsters("A");
            this.RankSMonsters = LocaleHelper.GetRankedMonsters("S");
        }

        public List<FilterItem> NPCFilters { get; set; } = new List<FilterItem>();

        public List<FilterItem> PCFilters { get; set; } = new List<FilterItem>();

        public List<FilterItem> MonsterFilters { get; set; } = new List<FilterItem>();

        public List<string> RankSMonsters { get; set; }

        public List<string> RankAMonsters { get; set; }

        public List<string> RankBMonsters { get; set; }

        private ConcurrentDictionary<uint, ActorItem> CurrentNPCs { get; set; } = new ConcurrentDictionary<uint, ActorItem>();

        public ConcurrentDictionary<uint, ActorItem> CurrentPCs { get; set; } = new ConcurrentDictionary<uint, ActorItem>();

        public ConcurrentDictionary<uint, ActorItem> CurrentMonsters { get; set; } = new ConcurrentDictionary<uint, ActorItem>();

        public ActorItem CurrentUser { get; set; }

        public void Dispose() {
            EventHost.Instance.OnNewCurrentUser -= this.Radar_OnNewCurrentUser;
            EventHost.Instance.OnNewPCActorItems -= this.Radar_OnNewPCActorItems;
            EventHost.Instance.OnNewNPCActorItems -= this.Radar_OnNewNPCActorItems;
            EventHost.Instance.OnNewMonsterActorItems -= this.Radar_OnNewMonsterActorItems;
        }

        private void Radar_OnNewNPCActorItems(object sender, MemoryHandler memoryhandler, ConcurrentDictionary<uint, ActorItem> eventdata) {
            if (memoryhandler.Configuration.ProcessModel.ProcessID == this._processID) {
                this.CurrentNPCs = eventdata;
            }
        }

        private void Radar_OnNewMonsterActorItems(object sender, MemoryHandler memoryhandler, ConcurrentDictionary<uint, ActorItem> eventdata) {
            if (memoryhandler.Configuration.ProcessModel.ProcessID == this._processID) {
                this.CurrentMonsters = eventdata;
            }
        }

        private void Radar_OnNewPCActorItems(object sender, MemoryHandler memoryhandler, ConcurrentDictionary<uint, ActorItem> eventdata) {
            if (memoryhandler.Configuration.ProcessModel.ProcessID == this._processID) {
                this.CurrentPCs = eventdata;
            }
        }

        private void Radar_OnNewCurrentUser(object sender, MemoryHandler memoryhandler, ActorItem eventdata) {
            if (memoryhandler.Configuration.ProcessModel.ProcessID == this._processID) {
                this.CurrentUser = eventdata;
            }
        }

        public void Refresh(int processID) {
            this._processID = processID;
            this.InvalidateVisual();
        }

        protected override void OnRender(DrawingContext drawingContext) {
            base.OnRender(drawingContext);

            ActorItem user = this.CurrentUser;
            if (user == null) {
                return;
            }

            Coordinate origin = new Coordinate {
                X = (float) (this.ActualWidth / 2),
                Y = (float) (this.ActualHeight / 2),
            };

            float scale = (float) (this.ActualHeight / 2.0f) / 125.0f;
            double angle = Math.Abs(user.Heading * (180 / Math.PI) - 180);

            DrawingGroup drawingGroup = new DrawingGroup();

            if (Settings.Default.CompassMode) {
                drawingGroup.Transform = new RotateTransform {
                    Angle = angle,
                    CenterX = origin.X,
                    CenterY = origin.Y,
                };
            }

            drawingGroup.Children.Add(new ImageDrawing(GameIcons.RadarHeading, new Rect(origin.X - 64, origin.Y - 128, 128, 128)));
            drawingGroup.Children.Add(new ImageDrawing(GameIcons.Player, new Rect(origin.X - 8, origin.Y - 16, 16, 21)));

            this.DrawDrawing(drawingContext, drawingGroup);

            StringBuilder stringBuilder = new StringBuilder();

            List<ActorItem> npcEntites = this.CurrentNPCs.Values.ToList();
            List<ActorItem> monsterEntites = this.CurrentMonsters.Values.ToList();
            List<ActorItem> pcEntites = this.CurrentPCs.Values.ToList();

            if (Settings.Default.FilterItems) {
                this.MonsterFilters.Clear();
                this.NPCFilters.Clear();
                this.PCFilters.Clear();

                this.MonsterFilters.AddRange(AppViewModel.Instance.FilterItems.Where(filter => filter.FilterType == FilterType.Monster && filter.IsEnabled));
                this.MonsterFilters.AddRange(AppViewModel.Instance.RankedMonsters);
                if (this.MonsterFilters.Any()) {
                    monsterEntites = FilterItemHelper.ResolveFilteredEntities(this.MonsterFilters, monsterEntites);
                }

                this.NPCFilters.AddRange(AppViewModel.Instance.FilterItems.Where(filter => filter.FilterType != (FilterType.Monster & FilterType.PC) && filter.IsEnabled));
                if (this.NPCFilters.Any()) {
                    npcEntites = FilterItemHelper.ResolveFilteredEntities(this.NPCFilters, npcEntites);
                }

                this.PCFilters.AddRange(AppViewModel.Instance.FilterItems.Where(filter => filter.FilterType == FilterType.PC && filter.IsEnabled));
                if (this.PCFilters.Any()) {
                    pcEntites = FilterItemHelper.ResolveFilteredEntities(this.PCFilters, pcEntites);
                }

                monsterEntites = FilterItemHelper.CleanupEntities(monsterEntites, this.CurrentUser.MapIndex);
            }

            #region Resolve PCs

            if (Settings.Default.PCShow) {
                foreach (ActorItem actorEntity in pcEntites) {
                    stringBuilder.Clear();
                    drawingContext.PushOpacity(1);
                    try {
                        if (!actorEntity.IsValid || user == null) {
                            continue;
                        }

                        if (actorEntity.ID == user.ID) {
                            continue;
                        }

                        Coordinate screen;
                        if (Settings.Default.CompassMode) {
                            Coordinate coordinate = user.Coordinate.Subtract(actorEntity.Coordinate).Scale(scale);
                            screen = new Coordinate(-coordinate.X, 0, -coordinate.Y).Add(origin);
                        }
                        else {
                            screen = user.Coordinate.Subtract(actorEntity.Coordinate).Rotate2D(user.Heading).Scale(scale).Add(origin);
                        }

                        screen = screen.Add(-8, -8, 0);
                        if (Settings.Default.PCShowName) {
                            stringBuilder.Append(actorEntity.Name);
                        }

                        if (Settings.Default.PCShowHPP) {
                            stringBuilder.AppendFormat($"{Environment.NewLine}{actorEntity.HPPercent:P0}");
                        }

                        if (Settings.Default.PCShowDistance) {
                            stringBuilder.AppendFormat($" {user.GetDistanceTo(actorEntity):N2} {this.ResolveHeightVariance(user, actorEntity)}");
                        }

                        bool useJob = Settings.Default.PCShowJob;
                        if (Settings.Default.PCShowJob) {
                            #region Get Job Icons

                            switch (actorEntity.Job) {
                                case Actor.Job.Unknown:
                                    if (actorEntity.OwnerID > 0 && actorEntity.OwnerID < 3758096384) {
                                        this.DrawImage(drawingContext, GameIcons.Chocobo, new Rect(new Point(), new Size(16, 16)));
                                    }

                                    useJob = false;
                                    break;
                                default:
                                    this.DrawImage(drawingContext, GameIcons.GetIconByName(actorEntity.Job.ToString()), new Rect(new Point(screen.X, screen.Y), new Size(16, 16)));
                                    break;
                            }

                            #endregion
                        }

                        if (!useJob) {
                            BitmapImage imageSource = actorEntity.HPCurrent > 0
                                                          ? GameIcons.Player
                                                          : GameIcons.Unknown;
                            this.DrawImage(drawingContext, imageSource, new Rect(new Point(screen.X, screen.Y), new Size(16, 16)));
                        }

                        this.RenderDebugInformation(actorEntity, ref stringBuilder);

                        if (Settings.Default.PCShowName || Settings.Default.PCShowHPP || Settings.Default.PCShowDistance) {
                            FormattedText text = this.FormatText(stringBuilder.ToString(), Settings.Default.PCFontSize, Settings.Default.PCFontColor);
                            this.DrawText(drawingContext, text, screen.X + 20, screen.Y);
                        }
                    }
                    catch (Exception ex) {
                        Logging.Log(Logger, ex);
                    }

                    drawingContext.Pop();
                }
            }

            #endregion

            #region Resolve Monsters

            if (Settings.Default.MonsterShow) {
                foreach (ActorItem actorEntity in monsterEntites) {
                    stringBuilder.Clear();

                    string fontColor = Settings.Default.MonsterFontColor;

                    bool isRanked = false;

                    if (this.RankAMonsters.Any(x => x.Equals(actorEntity.Name, StringComparison.InvariantCultureIgnoreCase))) {
                        fontColor = Settings.Default.MonsterARankFontColor;
                        isRanked = true;
                    }

                    if (this.RankSMonsters.Any(x => x.Equals(actorEntity.Name, StringComparison.InvariantCultureIgnoreCase))) {
                        fontColor = Settings.Default.MonsterBRankFontColor;
                        isRanked = true;
                    }

                    if (this.RankBMonsters.Any(x => x.Equals(actorEntity.Name, StringComparison.InvariantCultureIgnoreCase))) {
                        fontColor = Settings.Default.MonsterBRankFontColor;
                        isRanked = true;
                    }

                    drawingContext.PushOpacity(1);

                    try {
                        if (!actorEntity.IsValid || user == null) {
                            continue;
                        }

                        if (actorEntity.ID == user.ID) {
                            continue;
                        }

                        Coordinate screen;
                        if (Settings.Default.CompassMode) {
                            Coordinate coordinate = user.Coordinate.Subtract(actorEntity.Coordinate).Scale(scale);
                            screen = new Coordinate(-coordinate.X, 0, -coordinate.Y).Add(origin);
                        }
                        else {
                            screen = user.Coordinate.Subtract(actorEntity.Coordinate).Rotate2D(user.Heading).Scale(scale).Add(origin);
                        }

                        screen = screen.Add(-8, -8, 0);
                        BitmapImage actorIcon;

                        if (actorEntity.IsFate) {
                            actorIcon = GameIcons.MobFate;
                        }
                        else {
                            switch (actorEntity.DifficultyRank) {
                                case 6:
                                    actorIcon = actorEntity.IsAggressive
                                                    ? GameIcons.MobAggressive5
                                                    : GameIcons.MobPassive5;
                                    break;
                                case 4:
                                    actorIcon = actorEntity.IsAggressive
                                                    ? GameIcons.MobAggressive3
                                                    : GameIcons.MobPassive3;
                                    break;
                                case 3:
                                    actorIcon = actorEntity.IsAggressive
                                                    ? GameIcons.MobAggressive2
                                                    : GameIcons.MobPassive2;
                                    break;
                                case 2:
                                    actorIcon = actorEntity.IsAggressive
                                                    ? GameIcons.MobAggressive6
                                                    : GameIcons.MobPassive6;
                                    break;
                                case 1:
                                    actorIcon = actorEntity.IsAggressive
                                                    ? GameIcons.MobAggressive4
                                                    : GameIcons.MobPassive4;
                                    break;
                                default:
                                    actorIcon = actorEntity.IsAggressive
                                                    ? GameIcons.MobAggressive1
                                                    : GameIcons.MobPassive1;
                                    break;
                            }

                            if (actorEntity.OwnerID is > 0 and < 3758096384) {
                                actorIcon = GameIcons.Chocobo;
                            }
                        }

                        Size iconSize = new Size(16, 16);
                        Point point = new Point(screen.X, screen.Y);

                        if (isRanked) {
                            iconSize = new Size(24, 24);
                            point = new Point(screen.X - 4, screen.Y - 4);
                        }

                        if (actorEntity.HPCurrent > 0) {
                            if (actorIcon is not null) {
                                this.DrawImage(drawingContext, actorIcon, new Rect(point, iconSize));
                            }
                        }
                        else {
                            this.DrawImage(drawingContext, GameIcons.Unknown, new Rect(point, iconSize));
                        }

                        if (Settings.Default.MonsterShowName) {
                            stringBuilder.Append(actorEntity.Name);
                        }

                        if (Settings.Default.MonsterShowHPP) {
                            stringBuilder.AppendFormat($"{Environment.NewLine}{actorEntity.HPPercent:P0}");
                        }

                        if (Settings.Default.MonsterShowDistance) {
                            stringBuilder.AppendFormat($" {user.GetDistanceTo(actorEntity):N2} {this.ResolveHeightVariance(user, actorEntity)}");
                        }

                        this.RenderDebugInformation(actorEntity, ref stringBuilder);

                        if (Settings.Default.MonsterShowName || Settings.Default.MonsterShowHPP || Settings.Default.MonsterShowDistance) {
                            FormattedText text = this.FormatText(stringBuilder.ToString(), Settings.Default.MonsterFontSize, fontColor);
                            this.DrawText(drawingContext, text, screen.X + 20, screen.Y);
                        }
                    }
                    catch (Exception ex) {
                        Logging.Log(Logger, ex);
                    }

                    drawingContext.Pop();
                }
            }

            #endregion

            #region Resolve NPCs, Gathering & Other

            foreach (ActorItem actorEntity in npcEntites) {
                stringBuilder.Clear();
                drawingContext.PushOpacity(1);

                try {
                    if (!actorEntity.IsValid || user == null) {
                        continue;
                    }

                    if (actorEntity.ID == user.ID) {
                        continue;
                    }

                    Coordinate screen;
                    if (Settings.Default.CompassMode) {
                        Coordinate coord = user.Coordinate.Subtract(actorEntity.Coordinate).Scale(scale);
                        screen = new Coordinate(-coord.X, 0, -coord.Y).Add(origin);
                    }
                    else {
                        screen = user.Coordinate.Subtract(actorEntity.Coordinate).Rotate2D(user.Heading).Scale(scale).Add(origin);
                    }

                    screen = screen.Add(-8, -8, 0);
                    BitmapImage actorIcon;

                    switch (actorEntity.Type) {
                        case Actor.Type.NPC:

                            #region Resolve NPCs

                            if (Settings.Default.NPCShow) {
                                try {
                                    if (Settings.Default.NPCShowName) {
                                        stringBuilder.Append(actorEntity.Name);
                                    }

                                    if (Settings.Default.NPCShowHPP) {
                                        stringBuilder.AppendFormat($"{Environment.NewLine}{actorEntity.HPPercent:P0}");
                                    }

                                    if (Settings.Default.NPCShowDistance) {
                                        stringBuilder.AppendFormat($" {user.GetDistanceTo(actorEntity):N2} {this.ResolveHeightVariance(user, actorEntity)}");
                                    }

                                    actorIcon = actorEntity.HPCurrent > 0
                                                    ? GameIcons.Vendor
                                                    : GameIcons.Unknown;
                                    this.DrawImage(drawingContext, actorIcon, new Rect(new Point(screen.X, screen.Y), new Size(16, 16)));

                                    this.RenderDebugInformation(actorEntity, ref stringBuilder);

                                    if (Settings.Default.NPCShowName || Settings.Default.NPCShowHPP || Settings.Default.NPCShowDistance) {
                                        FormattedText text = this.FormatText(stringBuilder.ToString(), Settings.Default.NPCFontSize, Settings.Default.NPCFontColor);
                                        this.DrawText(drawingContext, text, screen.X + 20, screen.Y);
                                    }
                                }
                                catch (Exception ex) {
                                    Logging.Log(Logger, ex);
                                }

                                drawingContext.Pop();
                            }

                            #endregion

                            break;
                        case Actor.Type.Gathering:

                            #region Resolve Gathering

                            if (Settings.Default.GatheringShow && actorEntity.GatheringInvisible == 0) {
                                try {
                                    if (Settings.Default.GatheringShowName) {
                                        stringBuilder.Append(actorEntity.Name);
                                    }

                                    if (Settings.Default.GatheringShowDistance) {
                                        stringBuilder.AppendFormat($" {user.GetDistanceTo(actorEntity):N2} {this.ResolveHeightVariance(user, actorEntity)}");
                                    }

                                    actorIcon = GameIcons.Gathering;
                                    if (AppViewModel.Instance.GatheringNodes.TryGetValue(user.Job.ToString(), out List<GatheringNode> node)) {
                                        GatheringNode nodeMatch = node.FirstOrDefault(n => n.Localization.Matches(actorEntity.Name));
                                        if (nodeMatch is not null) {
                                            switch (user.Job) {
                                                case Actor.Job.BTN:
                                                    actorIcon = GameIcons.Harvesting;
                                                    switch (nodeMatch.Rarity) {
                                                        case GatheringRarity.Unspoiled:
                                                        case GatheringRarity.Ephemeral:
                                                        case GatheringRarity.Legendary:
                                                            actorIcon = GameIcons.HarvestingSuper;
                                                            break;
                                                    }

                                                    break;
                                                case Actor.Job.FSH:
                                                    actorIcon = GameIcons.Fishing;
                                                    break;
                                                case Actor.Job.MIN:
                                                    actorIcon = GameIcons.Mining;
                                                    switch (nodeMatch.Rarity) {
                                                        case GatheringRarity.Unspoiled:
                                                        case GatheringRarity.Ephemeral:
                                                        case GatheringRarity.Legendary:
                                                            actorIcon = GameIcons.MiningSuper;
                                                            break;
                                                    }

                                                    break;
                                            }
                                        }
                                    }

                                    this.DrawImage(drawingContext, actorIcon, new Rect(new Point(screen.X, screen.Y), new Size(16, 16)));

                                    this.RenderDebugInformation(actorEntity, ref stringBuilder);

                                    if (Settings.Default.GatheringShowName || Settings.Default.GatheringShowDistance) {
                                        FormattedText text = this.FormatText(stringBuilder.ToString(), Settings.Default.GatheringFontSize, Settings.Default.GatheringFontColor);
                                        this.DrawText(drawingContext, text, screen.X + 20, screen.Y);
                                    }
                                }
                                catch (Exception ex) {
                                    Logging.Log(Logger, ex);
                                }

                                drawingContext.Pop();
                            }

                            #endregion

                            break;
                        default:

                            #region Resolve Other

                            if (Settings.Default.OtherShow) {
                                try {
                                    switch (actorEntity.Type) {
                                        case Actor.Type.Aetheryte:
                                            actorIcon = GameIcons.Aetheryte;
                                            break;
                                        case Actor.Type.Minion:
                                            actorIcon = GameIcons.Avatar;
                                            break;
                                        default:
                                            actorIcon = GameIcons.Vendor;
                                            break;
                                    }

                                    if (actorEntity.HPCurrent > 0 || actorEntity.Type == Actor.Type.Aetheryte) {
                                        if (actorIcon is not null) {
                                            this.DrawImage(drawingContext, actorIcon, new Rect(new Point(screen.X, screen.Y), new Size(16, 16)));
                                        }
                                    }
                                    else {
                                        this.DrawImage(drawingContext, GameIcons.Unknown, new Rect(new Point(screen.X, screen.Y), new Size(16, 16)));
                                    }

                                    if (Settings.Default.OtherShowName) {
                                        stringBuilder.Append(actorEntity.Name);
                                    }

                                    if (Settings.Default.OtherShowHPP) {
                                        stringBuilder.AppendFormat($"{Environment.NewLine}{actorEntity.HPPercent:P0}");
                                    }

                                    if (Settings.Default.OtherShowDistance) {
                                        stringBuilder.AppendFormat($" {user.GetDistanceTo(actorEntity):N2} {this.ResolveHeightVariance(user, actorEntity)}");
                                    }

                                    this.RenderDebugInformation(actorEntity, ref stringBuilder);

                                    if (Settings.Default.OtherShowName || Settings.Default.OtherShowHPP || Settings.Default.OtherShowDistance) {
                                        FormattedText text = this.FormatText(stringBuilder.ToString(), Settings.Default.OtherFontSize, Settings.Default.OtherFontColor);
                                        this.DrawText(drawingContext, text, screen.X + 20, screen.Y);
                                    }
                                }
                                catch (Exception ex) {
                                    Logging.Log(Logger, ex);
                                }

                                drawingContext.Pop();
                            }

                            #endregion

                            break;
                    }
                }
                catch (Exception ex) { }
            }

            #endregion
        }

        private FormattedText FormatText(string message, int fontSize, string color) {
            string resolvedColor = string.IsNullOrWhiteSpace(color)
                                       ? "FFFF00"
                                       : color;
            return new FormattedText(message, this._cultureInfo, this._flowDirection, this._typeface, fontSize, (SolidColorBrush) this._brushConverter.ConvertFromString(resolvedColor), VisualTreeHelper.GetDpi(this).PixelsPerDip);
        }

        private void RenderDebugInformation(ActorItem actorEntity, ref StringBuilder stringBuilder) {
            if (!Settings.Default.ShowEntityDebugInfo) {
                return;
            }

            stringBuilder.Append($"{Environment.NewLine}NPCID1: {actorEntity.NPCID1:X}");
            stringBuilder.Append($"{Environment.NewLine}NPCID2: {actorEntity.NPCID2:X}");
            stringBuilder.Append($"{Environment.NewLine}ModelID: {actorEntity.ModelID:X}");
        }

        private string ResolveHeightVariance(ActorItem user, ActorItem actorEntity) {
            string modifier = string.Empty;
            if (user.Z < actorEntity.Z) {
                modifier = "+";
            }

            if (user.Z > actorEntity.Z) {
                modifier = "-";
            }

            return $"{modifier}{Math.Abs(this.ResolveHeightVarianceDecimal(user, actorEntity)):N2}";
        }

        private decimal ResolveHeightVarianceDecimal(ActorItem user, ActorItem actorEntity) {
            double variance = 0;
            if (user.Z < actorEntity.Z) {
                variance = user.Z - actorEntity.Z;
            }

            if (user.Z > actorEntity.Z) {
                variance = actorEntity.Z - user.Z;
            }

            return (decimal) variance;
        }

        private void DrawDrawing(DrawingContext context, DrawingGroup group) {
            context.DrawDrawing(group);
        }

        private void DrawImage(DrawingContext context, BitmapImage image, double x, double y, double width, double height) {
            context.DrawImage(image, new Rect(new Point(x, y), new Size(width, height)));
        }

        private void DrawImage(DrawingContext context, BitmapImage image, Rect rect) {
            context.DrawImage(image, rect);
        }

        private void DrawText(DrawingContext context, FormattedText text, double x, double y) {
            context.DrawText(text, new Point(x, y));
        }

        ~Radar() {
            this.Dispose();
        }
    }
}