// Copyright (C) 2017 Fievus
//
// This software may be modified and distributed under the terms
// of the MIT license.  See the LICENSE file for details.
using System;
using System.Collections.Generic;
using System.Linq;

namespace Carna.Runner
{
    internal static class Extensions
    {
        public static void IfPresent<T>(this T @this, Action<T> action)
        {
            if (@this != null) action(@this);
        }

        public static void IfAbsent(this object @this, Action action)
        {
            if (@this == null) action();
        }

        public static T RequireNonNull<T>(this T @this)
        {
            @this.IfAbsent(() => throw new ArgumentNullException());
            return @this;
        }

        public static T RequireNonNull<T>(this T @this, string parameterName)
        {
            @this.IfAbsent(() => throw new ArgumentNullException(parameterName));
            return @this;
        }

        public static void ForEach<T>(this IEnumerable<T> @this, Action<T> action)
        {
            if (@this == null) return;

            foreach (var item in @this) { action(item); }
        }

        public static TValue GetOrDefault<TKey, TValue>(this IDictionary<TKey, TValue> @this, TKey key, Func<TValue> defaultValue)
            => @this == null ? default(TValue) : @this.ContainsKey(key) ? @this[key] : defaultValue();

        public static bool IsEmpty<T>(this IEnumerable<T> @this) => !@this.Any();
    }
}
