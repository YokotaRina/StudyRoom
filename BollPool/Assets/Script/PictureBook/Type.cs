
using System;

namespace Script.PictureBook
{
    /// <summary>
    /// タイプ
    /// </summary>
    public enum Type
    {
        [Display(Name = "_____")]
        None = 0,

        [Display(Name = "ノーマル")]
        Normal = 1,

        [Display(Name = "ほのお")]
        Fire = 2,

        [Display(Name = "みず")]
        Water = 3,

        [Display(Name = "でんき")]
        Electric = 4,

        [Display(Name = "くさ")]
        Grass = 5,

        [Display(Name = "こおり")]
        Ice = 6,

        [Display(Name = "かくとう")]
        Fighting = 7,

        [Display(Name = "どく")]
        Poison = 8,

        [Display(Name = "じめん")]
        Ground = 9,

        [Display(Name = "ひこう")]
        Flying = 10,

        [Display(Name = "エスパー")]
        Psychic = 11,

        [Display(Name = "むし")]
        Bug = 12,

        [Display(Name = "いわ")]
        Rock = 13,

        [Display(Name = "ゴースト")]
        Ghost = 14,

        [Display(Name = "ドラゴン")]
        Dragon = 15,

        [Display(Name = "あく")]
        Dark = 16,

        [Display(Name = "はがね")]
        Steel = 17,

        [Display(Name = "フェアリー")]
        Fairy = 18,
    }

    /// <summary>
    /// ディスプレイ用の文字列を設定するAttribute
    /// </summary>
    [AttributeUsage(AttributeTargets.Field)]
    public class DisplayAttribute : Attribute
    {
        public string Name { get; set; }
    }

    /// <summary>
    /// Enum 拡張クラス
    /// </summary>
    public static class EnumExtend
    {
        /// <summary>
        /// Display属性で指定された文字列を返す
        /// </summary>
        public static string ToDisplayName(this Enum enumInstance)
        {
            var descriptionAttributes = QueryAttribute<DisplayAttribute>(enumInstance);
            if (descriptionAttributes != null && descriptionAttributes.Length > 0)
            {
                return descriptionAttributes[0].Name;
            }

            return enumInstance.ToString();
        }

        private static T[] QueryAttribute<T>(Enum enumInstance)
        {
            var type = enumInstance.GetType();
            var fieldInfo = type.GetField(enumInstance.ToString());
            var attributes = fieldInfo.GetCustomAttributes(typeof(T), false) as T[];
            return attributes;
        }
    }
}
