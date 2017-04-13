# Release note

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
