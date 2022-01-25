// Copyright (C) 2022 Fievus
//
// This software may be modified and distributed under the terms
// of the MIT license.  See the LICENSE file for details.
namespace Carna.Runner;

/// <summary>
/// Represents errors that occur when the fixture instance is not instantiate.
/// </summary>
public class FixtureInstanceNotInstantiateException : Exception
{
    /// <summary>
    /// Initializes a new instance of the <see cref="FixtureInstanceNotInstantiateException"/> class.
    /// </summary>
    public FixtureInstanceNotInstantiateException()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="FixtureInstanceNotInstantiateException"/> class
    /// with the specified error message.
    /// </summary>
    /// <param name="message">The message that describes the error.</param>
    public FixtureInstanceNotInstantiateException(string message) : base(message)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="FixtureInstanceNotInstantiateException"/> class
    /// with the specified error message and reference to the inner exception that
    /// is the cause of this exception.
    /// </summary>
    /// <param name="message">The message that describes the error.</param>
    /// <param name="innerException">
    /// The exception that is the cause of the current exception.
    /// </param>
    public FixtureInstanceNotInstantiateException(string message, Exception? innerException) : base(message, innerException)
    {
    }
}