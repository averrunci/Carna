// Copyright (C) 2018 Fievus
//
// This software may be modified and distributed under the terms
// of the MIT license.  See the LICENSE file for details.
using System;
using System.Linq;
using Carna.Runner.Step;
using Carna.Step;

namespace Carna.ConsoleRunner
{
    internal class FixtureStepResultContents : MarshalByRefObject
    {
        public string StepTypeName { get; }
        public string StepDescription { get; }
        public FixtureStepStatus Status { get; }
        public Exception Exception { get; }
        public DateTime? StartTime { get; }
        public DateTime? EndTime { get; }

        public FixtureStepResultContents(FixtureStepResult stepResult)
        {
            StepTypeName = stepResult.Step.GetType().AssemblyQualifiedName;
            StepDescription = stepResult.Step.Description;
            Status = stepResult.Status;
            Exception = stepResult.Exception == null ? null : new WrapperAssertionException(stepResult.Exception.GetType().ToString(), stepResult.Exception.Message, stepResult.Exception.StackTrace); ;
            StartTime = stepResult.StartTime;
            EndTime = stepResult.EndTime;
        }
    }

    internal static class FixtureStepResultContentsExtensions
    {
        public static FixtureStepResult ToFixtureStepResult(this FixtureStepResultContents @this)
        {
            var stepType = Type.GetType(@this.StepTypeName) ??
                AppDomain.CurrentDomain.GetAssemblies()
                    .SelectMany(assembly => assembly.DefinedTypes)
                    .FirstOrDefault(type => type.AssemblyQualifiedName == @this.StepTypeName) ??
                throw new InvalidOperationException($"'{@this.StepTypeName}' is not found");
            if (!(Activator.CreateInstance(stepType, @this.StepDescription, null, null, null, 0) is FixtureStep step)) throw new InvalidOperationException($"'{@this.StepTypeName}' can not be instantiated");

            var builder = new FixtureStepResult.Builder(step);
            if (@this.StartTime.HasValue) builder.StartAt(@this.StartTime.Value);
            if (@this.EndTime.HasValue) builder.EndAt(@this.EndTime.Value);

            switch (@this.Status)
            {
                case FixtureStepStatus.None: builder.None(); break;
                case FixtureStepStatus.Ready: builder.Ready(); break;
                case FixtureStepStatus.Pending: builder.Pending(); break;
                case FixtureStepStatus.Running: builder.Running(); break;
                case FixtureStepStatus.Failed: builder.Failed(@this.Exception); break;
                case FixtureStepStatus.Passed: builder.Passed(); break;
            }

            return builder.Build();
        }
    }
}
