// Copyright (C) 2022 Fievus
//
// This software may be modified and distributed under the terms
// of the MIT license.  See the LICENSE file for details.
namespace Carna.Runner;

internal static class Extensions
{
    public static void ForEach<T>(this IEnumerable<T>? @this, Action<T> action)
    {
        if (@this is null) return;

        foreach (var item in @this) action(item);
    }

    public static TValue? GetOrDefault<TKey, TValue>(this IDictionary<TKey, TValue>? @this, TKey key, Func<TValue> defaultValue)
        => @this is null ? default : @this.ContainsKey(key) ? @this[key] : defaultValue();

    public static bool IsEmpty<T>(this IEnumerable<T> @this) => !@this.Any();
}