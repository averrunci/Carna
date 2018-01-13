// Copyright (C) 2017-2018 Fievus
//
// This software may be modified and distributed under the terms
// of the MIT license.  See the LICENSE file for details.
using System;
using System.Linq;
using System.Reflection;

using Carna.Step;

namespace Carna.Runner.Step
{
    /// <summary>
    /// Represents errors that occur when an assertion is failed.
    /// </summary>
    public class AssertionException : Exception
    {
        /// <summary>
        /// Gets a string representation of the immediate frames on the call stack.
        /// </summary>
        public override string StackTrace => stackTrace;
        private readonly string stackTrace;

        private Exception Cause { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="AssertionException"/> class
        /// with the specified fixture step and assertion description.
        /// </summary>
        /// <param name="step">The fixture step when the assertion was failed.</param>
        /// <param name="description">The assertion description when the assertion was failed.</param>
        public AssertionException(FixtureStep step, AssertionDescription description) : base($"{step?.Description}{Environment.NewLine}{description}")
        {
            stackTrace = CreateStackTrace(step);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AssertionException"/> class
        /// with the specified fixture step and exception that was thrown when the assertion was failed.
        /// </summary>
        /// <param name="step">The fixture step when the assertion was failed.</param>
        /// <param name="cause">The exception that was thrown when the assertion was failed.</param>
        public AssertionException(FixtureStep step, Exception cause) : base(cause?.Message)
        {
            stackTrace = $"{cause.StackTrace}{Environment.NewLine}{CreateStackTrace(step)}";
            Cause = cause;
        }

        /// <summary>
        /// Returns a string that represents the current object.
        /// </summary>
        /// <returns>The string representation of the current exception.</returns>
        public override string ToString()
        {
            return $"{Cause?.GetType() ?? GetType()}: {Message}{Environment.NewLine}{StackTrace}";
        }

        private string CreateStackTrace(FixtureStep step)
        {
            if (step == null) { return string.Empty; }

            var getResourceStringMethod = typeof(Environment).GetTypeInfo().DeclaredMethods.Where(m => m.Name == "GetResourceString").FirstOrDefault();
            var at = getResourceStringMethod?.Invoke(null, new[] { "Word_At" }) as string ?? string.Empty;
            var inFileLineNumber = getResourceStringMethod?.Invoke(null, new[] { "StackTrace_InFileLineNumber" }) as string ?? string.Empty;

            return $"   {at} {step.CallerType}.{step.CallerMemberName} {string.Format(inFileLineNumber, step.CallerFilePath, step.CallerLineNumber)}";
        }
    }
}
