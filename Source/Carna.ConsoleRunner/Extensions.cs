// Copyright (C) 2017 Fievus
//
// This software may be modified and distributed under the terms
// of the MIT license.  See the LICENSE file for details.
using System;
using System.Collections.Generic;
using System.Linq;

namespace Carna.ConsoleRunner
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

            foreach (T item in @this) { action(item); }
        }

        public static void ForEachWithIndex<T>(this IEnumerable<T> @this, Action<T, int> action)
        {
            var index = 0;
            @this.ForEach(item => action(item, index++));
        }

        public static bool IsEmpty<T>(this IEnumerable<T> @this) => !@this.Any();
    }
}
