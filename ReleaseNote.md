# Release note

## v3.0.0

### Change

- Update the target framework version to .NET 8.0.

#### Carna.WinUIRunner

- Add the following methods to the CarnaWinUIRunner class:
  - Task InvokeAsync(DispatcherQueueHandler)
  - Task InvokeAsync(DispatcherQueuePriority, DispatcherQueueHandler)
  - Task&lt;TResult&gt; InvokeAsync<TResult>(Func&lt;TResult&gt;)
  - Task&lt;TResult&gt; InvokeAsync<TResult>(DispatcherQueuePriority, Func&lt;TResult&gt;)
- Obsolete the Window Property of the CarnaWinUIRunner class.

## v2.1.1

### Bug fix

#### Carna

- Fix an issure where an assertion is failed when a value type of the EnumerableAssertionProperty is IEnumerable.

## v2.1.0

### Add

#### Carna

- Add the Fixtures property to the FixtureAttribute class.
- Add the fixtures parameter to the constructor of the following classes:
  - FixtureAttribute
  - FeatureAttribute
  - SpecificationAttribute
  - RequirementAttribute
  - StoryAttribute
  - ContextAttribute

#### Carna.Runner

- Add the statement to build fixtures a parent fixture contains from the Fixtures property of the FixtureAttribute class.

#### Carna.ConsoleRunner

- Add /pause option. If the value is specified, a runner waits for a user input before running the fixtures, allowing to attach a debugger.
- Add a prefix '--' or '-' to command line options.

#### carna-runner

- Add the following framework references:
  - Microsoft.AspNetCore.App
  - Microsoft.WindowsDesktop.App

## v2.0.0

### Add

- Add the Carna.WinUIRunner.

### Change

- Update the target framework version to .NET 6.0.
  - Not update the following:
    - CarnaConsoleRunner
    - Carna.UwpRunner
- Enable Nullable reference types.

#### Carna.Runner

- Change to be able to execute the DisposeAsync method of a spec that implements the IAsyncDisposable interface.

## v1.8.2

### Bug fix

#### Carna.Runner

- Fixed an issue where the Background attribute applied to a base fixture is not retrieved when a field is initialized by a method call.

## v1.8.1

### Change

#### Carna

- Change an assertion description when Enumerable.SequenceEqual is failed.

### Bug fix

#### Carna.UwpRunner

- Fixed the condition of the limit to display child fixtures.
- Fixed a crash when unhandled exception occurred.
- Fixed the draggable region of the title bar.

## v1.8.0

### Add

#### Carna

- Add the EnumerableAssertionProperty to assert an enumerable.

### Change

#### Carna

- Change the default prefix/suffix of the string expression of the AssertionObject from '[]' to '{}'.

#### Carna.ConsoleRunner

- Update the .NET Core version to 3.1.
- Change the foreground color of the Ready state to the DarkGray.

#### CarnaConsoleRunner

- Change the foreground color of the Ready state to the DarkGray.

#### carna-runner

- Add .NET 5.0 to the target framework.
- Change the foreground color of the Ready state to the DarkGray.

## v1.7.2

### Bug fix

#### Carna.Runner

- Fixed to run a constructor and a dispose method of a container fixture in a single thread apartment when it is required.

#### carna-runner

- Fixed to be able to load unmanaged dlls.

## v1.7.1

### Bug fix

#### Carna.Runner

- Fixed to run a constructor and a dispose method in a single thread apartment when it is required.

## v1.7.0

### Add

#### Carna

- Add the RequiresSta property in the FixtureAttribute class.

#### Carna.Runner

- Add the behavior to run fixtures that are specified by the RequiresSta property is true in a single thread apartment.

### Change

#### Carna.UwpRunner

- Update the Microsoft.NETCore.UniversalWindowsPlatform version to 6.0.6.

### Bug fix

#### Carna.Runner

- Fixed the behavior to build fixtures so that an assembly/namespace fixture is set to the parent fixture.

## v1.6.0

### Add

- Add the carna-runner that is the console runner for .NET Core 3.

#### Carna

- Add the AssertionObject class that asserts properties specified by the AssertionPropertyAttribute attribute.
- Add the IAssertionProperty interface and the following its implementations;
  - ActualValueProperty class
  - AssertionProperty class
  - EqualAssertionProperty class
  - NotEqualAssertionProperty class
  - LessThanAssertionProperty class
  - LessThanOrEqualAssertionProperty class
  - GreaterThanAssertionProperty class
  - GreaterThanOrEqualAssertionProperty class
- Add a time-out for running a When step.

#### Carna.Runner

- Add the function to assert a time-out for running a When step.
- Add the constrains to an Expect step. Its status should be
  - Ready/Pending when the status of a Given step is Ready/Pending,
  - Ready/Pending when the status of the latest When step is Ready/Pending.
- Add the constrains to a When step. Its status should be
  - Ready/Pending when the status of a Given step is Ready/Pending,
  - Ready/Pending when the status of the latest When step is Ready/Pending.
- Add the constrains to a Then step. Its status should be
  - Ready/Pending when the status of the latest When step is Ready/Pending.

#### Carna.UwpRunner

- Add the style for the Dark theme.

### Change

#### Carna.UwpRunner

- Extends the view into the title bar.
- Change to be able to bring the first failed fixture content into a view.

## v1.5.1

### Change

#### Carna

- Add the AssertedException property to the ThenStep.

#### Carna.Runner

- Change in order to be able to assert with the multiple Then step when an exception is raised.

#### Carna.UwpRunner

- Change in order to open failed fixture contents.

## v1.5.0

### Add

#### CarnaConsoleRunner

- Add to run fixtures in the separate domain for each assembly.
- Add /nodomain option. If the value is specified, no domain is created and the fixtures are run in the primary domain; otherwise, the separate domain is created for each assembly.

### Bug fix

#### Carna

- Fixed AroundFixtureAttribute name.

#### Carna.Runner

- Fixed the default value for the parallel option.

## v1.4.0

### Add

#### Carna

- Add AroundFixture attribute.
- Add Culture attribute.
- Add UICulture attribute.

#### Carna.Runner

- Add the function to retrieve AroundFixture attribute and execute its methods before/after running a fixture.

### Changes

#### Carna.Runner

- Change a format of an assertion failure message.
- Change Task.Wait() to Task.GetAwaiter().GetResult().

### Bug fix

#### Carna.Runner

- Fix that a NullReferenceException is not thrown when a Parameter attribute is specified and a default constructor is not defined.

## v1.3.1

### Bug fix

#### Carna.UwpRunner

- Fixed dependencies.

## v1.3.0

### Add

#### Carna

- Add Background attribute.

#### Carna.Runner

- Add the function to retrieve Background attribute and format its description.
- Add the attribute of background in xml file report.

### Changes

- Update .NET Standard version to 2.0.

### Bug fix

#### Carna.Runner

- Fixed an assertion description when an exception occurred in its expression.

## v1.2.0

### Add

#### Carna

- Add Then step to which the type of an exception can be specified.

#### Carna.Runner

- Add the runner of Then step to which the type of an exception can be specified.
- Add the assertion description when the expression of the assertion is AndAlso. Each result of the expression is shown.

#### Carna.UwpRunner

- Add the configuration of autoExit and formatter.

### Changes

#### Carna.UwpRunner

- Remove UwpMvc dependency.

### Bug fix

#### Carna.Runner

- Fixed to clear the exception of When step when the exception assertion of Then step is failed.
- Fixed the string representation of the expected/actual value that is null when the assertion is failed.

#### Carna.UwpRunner

- Fixed the display of contents when a filter matches the Assembly fixture.
- Fixed the behavior when an exception is thrown while CarnaUwpRunner is running. Its message is shown.
- Fixed the color of Note step.

## v1.1.0

### Add

- Add Carna.UwpRunner.

#### Carna

- Add SampleAttribute.
- Add ISampleDataSource.

#### Carna.Runner

- Add the function to create a fixture specified with the SampleAttribute.

#### Carna.ConsoleRunner

- Add ICarnaRunnerCommandLineParser.

### Changes

- Change EndTime of FixtureResults from DateTime.MaxValue to DateTime.MinValue when the number of FixtureResults is zero.

## v1.0.1

### Bug fix

#### Carna.Runner
- Fixed assembly files selection when assemblies property is not specified.
