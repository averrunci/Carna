// Copyright (C) 2022 Fievus
//
// This software may be modified and distributed under the terms
// of the MIT license.  See the LICENSE file for details.
using Carna.Assertions;
using Carna.Runner.Step;

namespace Carna.Runner;

internal class FixtureStepResultAssertion : AssertionObject
{
    [AssertionProperty]
    string StepDescription { get; }

    [AssertionProperty]
    Exception? Exception { get; }

    [AssertionProperty]
    FixtureStepStatus Status { get; }

    [AssertionProperty]
    Type StepType { get; }

    private FixtureStepResultAssertion(string stepDescription, Exception? exception, FixtureStepStatus status, Type stepType)
    {
        StepDescription = stepDescription;
        Exception = exception;
        Status = status;
        StepType = stepType;
    }

    public static FixtureStepResultAssertion Of(string stepDescription, Exception? exception, FixtureStepStatus status, Type stepType) => new(stepDescription, exception, status, stepType);
    public static FixtureStepResultAssertion Of(FixtureStepResult stepResult) => new(stepResult.Step.Description, stepResult.Exception, stepResult.Status, stepResult.Step.GetType());
}