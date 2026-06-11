using System;
using System.Collections.Generic;

namespace Lexio.App.Helpers;

public static class ListExtension {
    private static readonly Random Rng = new Random();

    public static void Shuffle<T>(this List<T> list) {
        int n = list.Count;
        while (n > 1) {
            n--;
            int k = Rng.Next(n + 1);
            T value = list[k];
            list[k] = list[n];
            list[n] = value;
        }
    }

    public static T Pop<T>(this List<T> list) {
        var item = list[^1];
        list.RemoveAt(list.Count - 1);
        return item;
    }

    public static T? RandomElement<T>(this IList<T> list)
    {
        if (list == null)
            throw new ArgumentNullException(nameof(list));

        if (list.Count == 0)
            return default;

        return list[Random.Shared.Next(list.Count)];
    }
}