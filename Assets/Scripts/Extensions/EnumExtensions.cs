using System;
using System.Collections.Generic;

namespace Extensions
{
    public static class EnumExtensions
    {
        private static readonly Dictionary<Type, Array> _enumValuesCache = new Dictionary<Type, Array>();
        private static readonly Random _random = new Random();

        public static T GetRandom<T>(this T _) where T : Enum
        {
            var type = typeof(T);

            if (!_enumValuesCache.TryGetValue(type, out var values))
            {
                values = Enum.GetValues(type);
                _enumValuesCache[type] = values;
            }

            return (T)values.GetValue(_random.Next(values.Length));
        }
    }
}