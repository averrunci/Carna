// Copyright (C) 2022 Fievus
//
// This software may be modified and distributed under the terms
// of the MIT license.  See the LICENSE file for details.
using Carna.Step;

namespace Carna.Runner.Step;

/// <summary>
/// Provides the function to run a Then step.
/// </summary>
public class ThenStepRunner : FixtureStepRunner<ThenStep>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="NoteStepRunner"/> class
    /// with the specified Then step.
    /// </summary>
    /// <param name="step">The Then step to run.</param>
    public ThenStepRunner(ThenStep step) : base(step)
    {
    }

    /// <summary>
    /// Runs a Then step with the specified results of a fixture step.
    /// </summary>
    /// <param name="results">The results of the fixture step that was completed running.</param>
    /// <returns>The result of the Then step running.</returns>
    /// <exception cref="InvalidFixtureStepException">
    /// The <paramref name="results"/> does not have the <see cref="WhenStep"/>.
    /// </exception>
    protected override FixtureStepResult.Builder Run(FixtureStepResultCollection results)
    {
        if (!results.Has(typeof(WhenStep)))
        {
            throw new InvalidFixtureStepException("Then must be after When.");
        }

        if (IsPending)
        {
            return FixtureStepResult.Of(Step).Pending();
        }

        if (HasAssertionWithoutException && results.HasLatestExceptionAt<WhenStep>())
        {
            return FixtureStepResult.Of(Step).Ready();
        }

        if (results.HasStatusAtLatest<WhenStep>(FixtureStepStatus.Ready))
        {
            return FixtureStepResult.Of(Step).Ready();
        }

        if (results.HasStatusAtLatest<WhenStep>(FixtureStepStatus.Pending))
        {
            return FixtureStepResult.Of(Step).Pending();
        }

        try
        {
            if (Step.Assertion is not null) Step.ExecuteAssertion(Step.Assertion);
            Step.Action?.Invoke();
            Step.AsyncAction?.Invoke().GetAwaiter().GetResult();

            if (HasAssertionWithException)
            {
                RunExceptionAssertion(results);
            }

            return FixtureStepResult.Of(Step).Passed();
        }
        catch (Exception exc)
        {
            return FixtureStepResult.Of(Step).Failed(exc);
        }
    }

    private void RunExceptionAssertion(FixtureStepResultCollection results)
    {
        Exception? exception;
        var lastStep = results.Last().Step;
        if (lastStep is ThenStep lastThenStep)
        {
            exception = lastThenStep.AssertedException;
        }
        else
        {
            exception = results.GetLatestExceptionAt<WhenStep>();
            if (exception is not null) results.ClearException(exception);
        }

        Step.AssertedException = exception;
        if (Step.ExceptionType is not null)
        {
            if (exception is null)
            {
                Step.ExecuteAssertion(() => null == Step.ExceptionType);
            }
            else
            {
                Step.ExecuteAssertion(() => exception.GetType() == Step.ExceptionType);
            }
        }
        if (exception is null) return;

        if (Step.ExceptionAssertion is not null) Step.ExecuteAssertion(Step.ExceptionAssertion, exception);
        Step.ExceptionAction?.Invoke(exception);
        Step.AsyncExceptionAction?.Invoke(exception).GetAwaiter().GetResult();
    }

    private bool IsPending =>!HasAssertionWithoutException && !HasAssertionWithException;
    private bool HasAssertionWithoutException => Step.Assertion is not null || Step.Action is not null || Step.AsyncAction is not null;
    private bool HasAssertionWithException => Step.ExceptionAssertion is not null || Step.ExceptionAction is not null || Step.AsyncExceptionAction is not null || Step.ExceptionType is not null;
}