# Release note

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
