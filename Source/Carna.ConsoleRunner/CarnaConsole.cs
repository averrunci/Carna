// Copyright (C) 2022 Fievus
//
// This software may be modified and distributed under the terms
// of the MIT license.  See the LICENSE file for details.
namespace Carna.ConsoleRunner;

internal static class CarnaConsole
{
    public static TextWriter Out => Console.Out;
    public static void SetOut(TextWriter newOut) => Console.SetOut(newOut);

    public static int CursorLeft
    {
        get => Console.CursorLeft;
        set => Console.CursorLeft = value;
    }

    public static int CursorTop
    {
        get => Console.CursorTop;
        set => Console.CursorTop = value;
    }

    public static void Backspace() => Write("\b \b");
    public static void BackspaceToHome() => Enumerable.Range(0, CursorLeft).ForEach(_ => Backspace());

    public static void WriteLine() => Console.WriteLine();

    public static void WriteLine(object? value) => Console.WriteLine(value);
    public static void WriteLine(string format, params object?[]? args) => Console.WriteLine(format, args);

    public static void Write(object? value) => Console.Write(value);
    public static void Write(string format, params object?[]? args) => Console.Write(format, args);

    public static void WriteLineFailure(object? value) => WriteLine(ConsoleColor.Red, value);
    public static void WriteLineFailure(string format, params object?[]? args) => WriteLine(ConsoleColor.Red, format, args);

    public static void WriteFailure(object? value) => Write(ConsoleColor.Red, value);
    public static void WriteFailure(string format, params object?[]? args) => Write(ConsoleColor.Red, format, args);

    public static void WriteLineSuccess(object? value) => WriteLine(ConsoleColor.Green, value);
    public static void WriteLineSuccess(string format, params object?[]? args) => WriteLine(ConsoleColor.Green, format, args);

    public static void WriteSuccess(object? value) => Write(ConsoleColor.Green, value);
    public static void WriteSuccess(string format, params object?[]? args) => Write(ConsoleColor.Green, format, args);

    public static void WriteLineReady(object? value) => WriteLine(ConsoleColor.DarkGray, value);
    public static void WriteLineReady(string format, params object?[]? args) => WriteLine(ConsoleColor.DarkGray, format, args);

    public static void WriteReady(object? value) => Write(ConsoleColor.DarkGray, value);
    public static void WriteReady(string format, params object?[]? args) => Write(ConsoleColor.DarkGray, format, args);

    public static void WriteLinePending(object? value) => WriteLine(ConsoleColor.Yellow, value);
    public static void WriteLinePending(string format, params object?[]? args) => WriteLine(ConsoleColor.Yellow, format, args);

    public static void WritePending(object? value) => Write(ConsoleColor.Yellow, value);
    public static void WritePending(string format, params object?[]? args) => Write(ConsoleColor.Yellow, format, args);

    public static void WriteLineNote(object? value) => WriteLine(ConsoleColor.Yellow, value);
    public static void WriteLineNote(string format, params object?[]? args) => WriteLine(ConsoleColor.Yellow, format, args);

    public static void WriteNote(object? value) => Write(ConsoleColor.Yellow, value);
    public static void WriteNote(string format, params object?[]? args) => Write(ConsoleColor.Yellow, format, args);

    public static void WriteLineTitle(string format, params object?[]? args) => WriteLine(ConsoleColor.White, format, args);
    public static void WriteTitle(string format, params object?[]? args) => Write(ConsoleColor.White, format, args);

    public static void WriteLineHeader(string format, params object?[]? args) => WriteLine(ConsoleColor.Cyan, format, args);
    public static void WriteHeader(string format, params object?[]? args) => Write(ConsoleColor.Cyan, format, args);

    public static void WriteLineItem(string format, params object?[]? args) => WriteLine(ConsoleColor.Green, format, args);
    public static void WriteItem(string format, params object?[]? args) => Write(ConsoleColor.Green, format, args);

    public static void WriteLineValue(object? value) => WriteLine(ConsoleColor.White, value);
    public static void WriteValue(object? value) => Write(ConsoleColor.White, value);

    private static void WriteLine(ConsoleColor foregroundColor, object? value) => Write(() => Console.WriteLine(value), foregroundColor);
    private static void WriteLine(ConsoleColor foregroundColor, string format, params object?[]? args) => Write(() => Console.WriteLine(format, args), foregroundColor);

    private static void Write(ConsoleColor foregroundColor, object? value) => Write(() => Console.Write(value), foregroundColor);
    private static void Write(ConsoleColor foregroundColor, string format, params object?[]? args) => Write(() => Console.Write(format, args), foregroundColor);

    private static void Write(Action action, ConsoleColor foregroundColor)
    {
        var currentForegroundColor = Console.ForegroundColor;
        try
        {
            Console.ForegroundColor = foregroundColor;
            action();
        }
        finally
        {
            Console.ForegroundColor = currentForegroundColor;
        }
    }
}