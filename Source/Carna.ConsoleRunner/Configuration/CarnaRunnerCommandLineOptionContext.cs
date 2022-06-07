// Copyright (C) 2022 Fievus
//
// This software may be modified and distributed under the terms
// of the MIT license.  See the LICENSE file for details.
namespace Carna.ConsoleRunner.Configuration;

/// <summary>
/// Represents a context of a command line option.
/// </summary>
public class CarnaRunnerCommandLineOptionContext
{
    /// <summary>
    /// Gets an argument of the command line option.
    /// </summary>
    public string Argument { get; }

    /// <summary>
    /// Gets a key of the command line option.
    /// </summary>
    public string? Key { get; }

    /// <summary>
    /// Gets a value of the command line option.
    /// </summary>
    public string? Value { get; }

    /// <summary>
    /// Gets a value that indicates whether the context has key.
    /// </summary>
    public bool HasKey => Key != null;

    private CarnaRunnerCommandLineOptionContext(string arg)
    {
        Argument = arg;
        if (!Argument.StartsWith("/") && !Argument.StartsWith("--") && !Argument.StartsWith("-")) return;

        var separatorIndex = Argument.IndexOf(":", StringComparison.Ordinal);
        if (separatorIndex < 0)
        {
            Key = Argument;
        }
        else
        {
            Key = Argument[..separatorIndex];
            Value = Argument[(separatorIndex + 1)..];
        }
    }

    /// <summary>
    /// Creates a new instance of the <see cref="CarnaRunnerCommandLineOptionContext"/>
    /// with the specified argument value.
    /// </summary>
    /// <param name="arg">The argument value of the command line option.</param>
    /// <returns>The new instance of the <see cref="CarnaRunnerCommandLineOptionContext"/>.</returns>
    public static CarnaRunnerCommandLineOptionContext Of(string arg) => new(arg);
}