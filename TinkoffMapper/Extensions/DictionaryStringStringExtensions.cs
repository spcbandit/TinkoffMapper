using JetBrains.Annotations;

using System;
using System.Collections.Generic;
using System.Globalization;

namespace TinkoffMapper.Extensions
{
    /// <summary>
    /// Конвертация в стринг
    /// </summary>
    internal static class DictionaryStringStringExtensions
    {
        public static void AddObjectIfNotNull<T>([NotNull] this IDictionary<string, T> dictionary,
            [NotNull] string key, [CanBeNull] T value)
        {
            if (value == null) return;

            dictionary.Add(key, value);
        }
        public static void AddObjectIfNotNull<T>([NotNull] this IDictionary<Enum, T> dictionary,
            [NotNull] Enum key, [CanBeNull] T value)
        {
            if (value == null) return;

            dictionary.Add(key, value);
        }
        public static void AddStringIfNotEmptyOrWhiteSpace([NotNull] this IDictionary<string, string> dictionary,
            [NotNull] string key, [CanBeNull] string value)
        {
            if (string.IsNullOrWhiteSpace(value)) return;

            dictionary.Add(key, value);
        }

        public static void AddDecimal([NotNull] this IDictionary<string, string> dictionary,
            [NotNull] string key, decimal value) =>
            dictionary.Add(key, value.ToFormattedString());

        public static void AddDecimalIfNotNull([NotNull] this IDictionary<string, string> dictionary,
            [NotNull] string key, decimal? value)
        {
            if (!value.HasValue) return;

            dictionary.Add(key, value.Value.ToFormattedString());
        }

        public static void AddBoolean([NotNull] this IDictionary<string, string> dictionary,
            [NotNull] string key, bool value) =>
            dictionary.Add(key, value.ToString(CultureInfo.InvariantCulture).ToLower());

        public static void AddBooleanIfNotNull([NotNull] this IDictionary<string, string> dictionary,
            [NotNull] string key, bool? value)
        {
            if (!value.HasValue) return;

            dictionary.Add(key, value.Value.ToString(CultureInfo.InvariantCulture).ToLower());
        }

        public static void AddSimpleStruct<T>([NotNull] this IDictionary<string, string> dictionary,
            [NotNull] string key, T value) where T : struct
            => dictionary.Add(key, value.ToString());

        public static void AddSimpleStructIfNotNull<T>([NotNull] this IDictionary<string, string> dictionary,
            [NotNull] string key, T? value) where T : struct
        {
            if (!value.HasValue) return;

            dictionary.Add(key, value.Value.ToString());
        }

        public static void AddEnum<T>([NotNull] this IDictionary<string, string> dictionary,
            [NotNull] string key, T value) where T : struct
        {
            dictionary.Add(key, EnumHelper.GetEnumMemberAttributeValue(value));
        }

        public static void AddEnumIfNotNull<T>([NotNull] this IDictionary<string, string> dictionary,
            [NotNull] string key, [CanBeNull] T? value) where T : struct
        {
            if (!value.HasValue) return;

            dictionary.Add(key, EnumHelper.GetEnumMemberAttributeValue(value.Value));
        }

        public static void AddListEnums<T>(this IDictionary<string, string> dictionary, string key, List<T> value) where T : struct
        {
            string str = string.Empty;
            List<string> strs = new List<string>();
            value.ForEach(x => { strs.Add(EnumHelper.GetEnumMemberAttributeValue(x)); });
            str = string.Join(",", strs);
            dictionary.Add(key, str);
        }
    }
}
