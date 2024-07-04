using FluentAssertions;
using System;
using System.Collections.Generic;

namespace SeleniumAdvanced.Helpers;

public class RandomHelper
{
    private static readonly Random _random = new();
    public static T GetRandomEnum<T>() where T : Enum
    {
        var values = Enum.GetValues(typeof(T));
        return (T)values.GetValue(_random.Next(values.Length));
    }

    public static T GetRandomItemFromList<T>(List<T> list)
    {
        list.Should().NotBeEmpty();
        return list[_random.Next(list.Count)];
    }
}