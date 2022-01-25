// Copyright (C) 2022 Fievus
//
// This software may be modified and distributed under the terms
// of the MIT license.  See the LICENSE file for details.
using Carna.Assertions;
using Carna.Step;

namespace Carna.Runner.Step;

internal class FixtureStepResultAssertion : AssertionObject
{
    [AssertionProperty]
    FixtureStepStatus Status { get; }

    AssertionProperty<Exception?> Exception { get; }

    [AssertionProperty]
    FixtureStep Step { get; }

    private FixtureStepResultAssertion(FixtureStepStatus status, AssertionProperty<Exception?> exception, FixtureStep step)
    {
        Status = status;
        Exception = exception;
        Step = step;
    }

    public static FixtureStepResultAssertion ForNullException(FixtureStepStatus status, FixtureStep step) => new(status, new EqualAssertionProperty<Exception?>(null), step);
    public static FixtureStepResultAssertion ForNotNullException(FixtureStepStatus status, FixtureStep step) => new(status, new NotEqualAssertionProperty<Exception?>(null), step);

    public static FixtureStepResultAssertion Of(FixtureStepResult result) => new(result.Status, new ActualValueProperty<Exception?>(result.Exception), result.Step);
}