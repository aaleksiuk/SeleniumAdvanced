using FluentAssertions;
using System;
using System.Collections.Generic;

namespace SeleniumAdvanced.Helpers;

public class RandomHelper
{
    private static readonly Random Random = new();
    public static T GetRandomEnum<T>() where T : Enum
    {
        var values = Enum.GetValues(typeof(T));
        return (T)values.GetValue(Random.Next(values.Length));
    }

    public static T GetRandomItemFromList<T>(List<T> list)
    {
        list.Should().NotBeEmpty();
        return list[Random.Next(list.Count)];
    }
}