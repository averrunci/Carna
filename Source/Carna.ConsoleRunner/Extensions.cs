// Copyright (C) 2022 Fievus
//
// This software may be modified and distributed under the terms
// of the MIT license.  See the LICENSE file for details.
namespace Carna.ConsoleRunner;

internal static class Extensions
{
    public static void ForEach<T>(this IEnumerable<T>? @this, Action<T> action)
    {
        if (@this is null) return;

        foreach (var item in @this) action(item);
    }

    public static void ForEachWithIndex<T>(this IEnumerable<T> @this, Action<T, int> action)
    {
        var index = 0;
        @this.ForEach(item => action(item, index++));
    }

    public static bool IsEmpty<T>(this IEnumerable<T> @this) => !@this.Any();
}