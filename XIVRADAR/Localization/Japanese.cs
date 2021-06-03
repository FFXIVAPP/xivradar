// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Japanese.cs">
//   Copyright© 2021 Ryan Wilson
//   Licensed under the MIT license. See LICENSE.md in the solution root for full license information.
// </copyright>
// <summary>
//   Japanese.cs Implementation
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace XIVRADAR.Localization {
    using System.Collections.Generic;
    using System.Windows;

    public class Japanese {
        private static readonly ResourceDictionary _translations = new ResourceDictionary();

        private static readonly List<string> RankA = new List<string> {
            "魔導ヘルズクロー",
            "ウンクテヒ",
            "醜男のヴォガージャ",
            "コンヌ",
            "マーベリー",
            "ナン",
            "ファルネウス",
            "メルティゼリー",
            "ギルタブ",
            "ゲーデ",
            "マラク",
            "サボテンダー・バイラリーナ",
            "マヘス",
            "ファイナルフレイム",
            "ザニゴ",
            "アレクトリオン",
            "クーレア",

            // Heavensward Rank A
            "ミルカ",
            "リューバ",
            "ブネ",
            "アガトス",
            "パイルラスタ",
            "ワイバーンロード",
            "機兵のスリップキンクス",
            "ストラス",
            "キャムパクティ",
            "センチブロッサム",
            "エンケドラス",
            "シシウトゥル",

            // Stormblood Rank A
            "アンガダ",
            "アクラブアメル",
            "アール",
            "船幽霊",
            "ガジャースラ",
            "ギリメカラ",
            "ルミナーレ",
            "マヒシャ",
            "オニユメミ",
            "オルクス",
            "ソム",
            "バックスタイン",
        };

        private static readonly List<string> RankB = new List<string> {
            "アシエン・アルビン",
            "バーバステル",
            "ブラッディ・マリー",
            "ダークヘルメット",
            "不滅のフェランド闘軍曹",
            "ガトリングス",
            "リーチキング",
            "モナーク・オーガフライ",
            "ミラドロッシュ",
            "ナウル",
            "アヴゼン",
            "フェクダ",
            "スェアーシロップ",
            "スコッグ・フリュー",
            "スティンギング・ソフィー",
            "ヴオコー",
            "ホワイトジョーカー",

            // Heavensward Rank B
            "アルティック",
            "ギガントピテクス",
            "グナース・コメットドローン",
            "クルーゼ",
            "リュキダス",
            "オムニ",
            "プテリゴトゥス",
            "舞手のサヌバリ",
            "スキタリス",
            "スクオンク",
            "スケアクロウ",
            "テクスタ",

            // Stormblood Rank B
            "アスワング",
            "ブッカブー",
            "デイダラ",
            "剣豪ガウキ",
            "姑獲鳥",
            "グアス・ア・ニードル",
            "雷撃のギョライ",
            "キワ",
            "クールマ",
            "マネス",
            "オゼルム",
            "宵闇のヤミニ",
        };

        private static readonly List<string> RankS = new List<string> {
            "ガーロック",
            "ケロゲロス",
            "クロック・ミテーヌ",
            "チェルノボーグ",
            "ナンディ",
            "ボナコン",
            "レドロネット",
            "ウルガル",
            "サウザンドキャスト・セダ",
            "マインドフレア",
            "サファト",
            "ブロンテス",
            "バルウール",
            "ミニョーカオン",
            "ヌニュヌウィ",
            "ゾーナ・シーカー",
            "アグリッパ",

            // Heavensward Rank S
            "カイザーベヒーモス",
            "ガンダルヴァ",
            "セーンムルウ",
            "ペイルライダー",
            "レウクロッタ",
            "極楽鳥",

            // Stormblood Rank S
            "ボーンクローラー",
            "ガンマ",
            "オキナ",
            "オルガナ",
            "ソルト・アンド・ライト",
            "ウドンゲ",
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
