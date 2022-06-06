// Copyright (C) 2022 Fievus
//
// This software may be modified and distributed under the terms
// of the MIT license.  See the LICENSE file for details.
using System.Reflection;

namespace Carna.Runner;

[Specification("FixtureBuilder Spec")]
class FixtureBuilderSpec : FixtureSteppable
{
    IFixtureBuilder Builder { get; } = new FixtureBuilder();
    IEnumerable<IFixture> Fixtures { get; set; } = default!;

    [Example("Fixture contains a fixture method (Context fixture > Example fixture)")]
    void Ex01()
    {
        When("a fixture is built", () => Fixtures = Builder.Build(new[] { typeof(TestFixtures.SimpleFixture).GetTypeInfo() }));
        Then("the number of built fixtures should be 1", () => Fixtures.Count() == 1);

        var assemblyFixture = Fixtures.ElementAt(0);
        Then("the built fixture should be an Assembly fixture", () => assemblyFixture.FixtureDescriptor.FixtureAttributeType == typeof(AssemblyFixtureAttribute));
        Then("the parent fixture of the built fixture should be null", () => assemblyFixture.ParentFixture == null);

        var childFixturesOfAssemblyFixture = GetFixtures(assemblyFixture as FixtureContainer);
        Then("the number of fixtures contained by the Assembly fixture should be 1", () => childFixturesOfAssemblyFixture.Count() == 1);

        var namespaceFixture = childFixturesOfAssemblyFixture.ElementAt(0);
        Then("the fixture contained by the Assembly fixture should be a Namespace fixture", () => namespaceFixture.FixtureDescriptor.FixtureAttributeType == typeof(NamespaceFixtureAttribute));
        Then("the parent fixture of the fixture contained by the Assembly fixture should be the Assembly fixture", () => namespaceFixture.ParentFixture == assemblyFixture);

        var childFixturesOfNamespaceFixture = GetFixtures(namespaceFixture as FixtureContainer);
        Then("the number of fixtures contained by the Namespace fixture should be 1", () => childFixturesOfNamespaceFixture.Count() == 1);

        var contextFixture = childFixturesOfNamespaceFixture.ElementAt(0);
        Then("the fixture contained by the Namespace fixture should be a Context fixture", () => contextFixture.FixtureDescriptor.FixtureAttributeType == typeof(ContextAttribute));
        Then("the parent fixture of the fixture contained by the Namespace fixture should be the Namespace fixture", () => contextFixture.ParentFixture == namespaceFixture);

        var childFixturesOfContextFixture = GetFixtures(contextFixture as FixtureContainer);
        Then("the number of fixtures contained by the Context fixture should be 1", () => childFixturesOfContextFixture.Count() == 1);

        var exampleFixture = childFixturesOfContextFixture.ElementAt(0);
        Then("the fixture contained by the Context fixture should be an Example fixture", () => exampleFixture.FixtureDescriptor.FixtureAttributeType == typeof(ExampleAttribute));
        Then("the parent fixture of the fixture contained by the Context fixture should be the parent fixture of the Context fixture", () => exampleFixture.ParentFixture == contextFixture.ParentFixture);
    }

    [Example("Fixture contains three fixture methods (Context fixture > Example fixture, Example fixture, Example fixture)")]
    void Ex02()
    {
        When("a fixture is built", () => Fixtures = Builder.Build(new[] { typeof(TestFixtures.SimpleFixtureWithSomeMethods).GetTypeInfo() }));
        Then("the number of built fixtures should be 1", () => Fixtures.Count() == 1);

        var assemblyFixture = Fixtures.ElementAt(0);
        Then("the built fixture should be an Assembly fixture", () => assemblyFixture.FixtureDescriptor.FixtureAttributeType == typeof(AssemblyFixtureAttribute));
        Then("the parent fixture of the built fixture should be null", () => assemblyFixture.ParentFixture == null);

        var childFixturesOfAssemblyFixture = GetFixtures(assemblyFixture as FixtureContainer);
        Then("the number of fixtures contained by the Assembly fixture should be 1", () => childFixturesOfAssemblyFixture.Count() == 1);

        var namespaceFixture = childFixturesOfAssemblyFixture.ElementAt(0);
        Then("the fixture contained by the Assembly fixture should be a Namespace fixture", () => namespaceFixture.FixtureDescriptor.FixtureAttributeType == typeof(NamespaceFixtureAttribute));
        Then("the parent fixture of the fixture contained by the Assembly fixture should be the Assembly fixture", () => namespaceFixture.ParentFixture == assemblyFixture);

        var childFixturesOfNamespaceFixture = GetFixtures(namespaceFixture as FixtureContainer);
        Then("the number of fixtures contained by the Namespace fixture should be 1", () => childFixturesOfNamespaceFixture.Count() == 1);

        var contextFixture = childFixturesOfNamespaceFixture.ElementAt(0);
        Then("the fixture contained by the Namespace fixture should be a Context fixture", () => contextFixture.FixtureDescriptor.FixtureAttributeType == typeof(ContextAttribute));
        Then("the parent fixture of the fixture contained by the Namespace fixture should be the Namespace fixture", () => contextFixture.ParentFixture == namespaceFixture);

        var childFixturesOfContextFixture = GetFixtures(contextFixture as FixtureContainer);
        Then("the number of fixtures contained by the Context fixture should be 3", () => childFixturesOfContextFixture.Count() == 3);

        Then("the fixtures contained by the Context fixture should be all Example fixtures", () => childFixturesOfContextFixture.All(childFixture => childFixture.FixtureDescriptor.FixtureAttributeType == typeof(ExampleAttribute)));
        Then("the parent fixture of the fixtures contained by the Context fixture should be the parent fixture of the Context fixture", () => childFixturesOfContextFixture.All(childFixture => childFixture.ParentFixture == contextFixture.ParentFixture));
    }

    [Example("Fixture contains a container fixture as a nested class (Requirement fixture > Context fixture > Example fixture)")]
    void Ex03()
    {
        When("a fixture is built", () => Fixtures = Builder.Build(new[] { typeof(TestFixtures.SimpleFixtureWithContainerFixtureAsNestedClass).GetTypeInfo() }));
        Then("the number of built fixtures should be 1", () => Fixtures.Count() == 1);

        var assemblyFixture = Fixtures.ElementAt(0);
        Then("the built fixture should be an Assembly fixture", () => assemblyFixture.FixtureDescriptor.FixtureAttributeType == typeof(AssemblyFixtureAttribute));
        Then("the parent fixture of the build fixture should be null", () => assemblyFixture.ParentFixture == null);

        var childFixturesOfAssemblyFixture = GetFixtures(assemblyFixture as FixtureContainer);
        Then("the number of fixtures contained by the Assembly fixture should be 1", () => childFixturesOfAssemblyFixture.Count() == 1);

        var namespaceFixture = childFixturesOfAssemblyFixture.ElementAt(0);
        Then("the fixture contained by the Assembly fixture should be a Namespace fixture", () => namespaceFixture.FixtureDescriptor.FixtureAttributeType == typeof(NamespaceFixtureAttribute));
        Then("the parent fixture of the fixture contained by the Assembly fixture should be the Assembly fixture", () => namespaceFixture.ParentFixture == assemblyFixture);

        var childFixturesOfNamespaceFixture = GetFixtures(namespaceFixture as FixtureContainer);
        Then("the number of fixtures contained by the Namespace fixture should be 1", () => childFixturesOfNamespaceFixture.Count() == 1);

        var requirementFixture = childFixturesOfNamespaceFixture.ElementAt(0);
        Then("the fixture contained by the Namespace fixture should be a Requirement fixture", () => requirementFixture.FixtureDescriptor.FixtureAttributeType == typeof(RequirementAttribute));
        Then("the parent fixture of the fixture contained by the Namespace fixture should be the Namespace fixture", () => requirementFixture.ParentFixture == namespaceFixture);

        var childFixturesOfRequirementFixture = GetFixtures(requirementFixture as FixtureContainer);
        Then("the number of fixtures contained by the Requirement fixture should be 1", () => childFixturesOfRequirementFixture.Count() == 1);

        var contextFixture = childFixturesOfRequirementFixture.ElementAt(0);
        Then("the fixture contained by the Requirement fixture should be a Context fixture", () => contextFixture.FixtureDescriptor.FixtureAttributeType == typeof(ContextAttribute));
        Then("the parent fixture of the fixture contained by the Requirement fixture should be the Requirement fixture", () => contextFixture.ParentFixture == requirementFixture);

        var childFixturesOfContextFixture = GetFixtures(contextFixture as FixtureContainer);
        Then("the number of fixtures contained by the Context fixture should be 1", () => childFixturesOfContextFixture.Count() == 1);

        var exampleFixture = childFixturesOfContextFixture.ElementAt(0);
        Then("the fixture contained by the Context fixture should be an Example fixture", () => exampleFixture.FixtureDescriptor.FixtureAttributeType == typeof(ExampleAttribute));
        Then("the parent fixture of the fixture contained by the Context fixture should be the parent fixture of the Context fixture", () => exampleFixture.ParentFixture == contextFixture.ParentFixture);
    }

    [Example("Fixture contains a container fixture as a property (Requirement fixture > Context fixture > Example fixture)")]
    void Ex04()
    {
        When("a fixture is built", () => Fixtures = Builder.Build(new[] { typeof(TestFixtures.SimpleFixtureWithContainerFixtureAsProperty).GetTypeInfo() }));
        Then("the number of built fixtures should be 1", () => Fixtures.Count() == 1);

        var assemblyFixture = Fixtures.ElementAt(0);
        Then("the built fixture should be an Assembly fixture", () => assemblyFixture.FixtureDescriptor.FixtureAttributeType == typeof(AssemblyFixtureAttribute));
        Then("the parent fixture of the built fixture should be null", () => assemblyFixture.ParentFixture == null);

        var childFixturesOfAssemblyFixture = GetFixtures(assemblyFixture as FixtureContainer);
        Then("the number of fixtures contained by the Assembly fixture should be 1", () => childFixturesOfAssemblyFixture.Count() == 1);

        var namespaceFixture = childFixturesOfAssemblyFixture.ElementAt(0);
        Then("the fixture contained by the Assembly fixture should be a Namespace fixture", () => namespaceFixture.FixtureDescriptor.FixtureAttributeType == typeof(NamespaceFixtureAttribute));
        Then("the parent fixture of the fixture contained by the Assembly fixture should be the Assembly fixture", () => namespaceFixture.ParentFixture == assemblyFixture);

        var childFixturesOfNamespaceFixture = GetFixtures(namespaceFixture as FixtureContainer);
        Then("the number of fixtures contained by the Namespace fixture should be 1", () => childFixturesOfNamespaceFixture.Count() == 1);

        var requirementFixture = childFixturesOfNamespaceFixture.ElementAt(0);
        Then("the fixture contained by the Namespace fixture should be a Requirement fixture", () => requirementFixture.FixtureDescriptor.FixtureAttributeType == typeof(RequirementAttribute));
        Then("the parent fixture of the fixture contained by the Namespace fixture should be the Namespace fixture", () => requirementFixture.ParentFixture == namespaceFixture);

        var childFixturesOfRequirementFixture = GetFixtures(requirementFixture as FixtureContainer);
        Then("the number of fixtures contained by the Requirement fixture should be 1", () => childFixturesOfRequirementFixture.Count() == 1);

        var contextFixture = childFixturesOfRequirementFixture.ElementAt(0);
        Then("the fixture contained by the Requirement fixture should be a Context fixture", () => contextFixture.FixtureDescriptor.FixtureAttributeType == typeof(ContextAttribute));
        Then("the parent fixture of the fixture contained by the Requirement fixture should be the Requirement fixture", () => contextFixture.ParentFixture == requirementFixture);

        var childFixturesOfContextFixture = GetFixtures(contextFixture as FixtureContainer);
        Then("the number of fixtures contained by the Context fixture should be 1", () => childFixturesOfContextFixture.Count() == 1);

        var exampleFixture = childFixturesOfContextFixture.ElementAt(0);
        Then("the fixture contained by the Context fixture should be an Example fixture", () => exampleFixture.FixtureDescriptor.FixtureAttributeType == typeof(ExampleAttribute));
        Then("the parent fixture of the fixture contained by the Context fixture should be the parent fixture of the Context fixture", () => exampleFixture.ParentFixture == contextFixture.ParentFixture);
    }

    [Example("Fixture contains a container fixture as a field (Requirement fixture > Context fixture > Example fixture)")]
    void Ex05()
    {
        When("a fixture is built", () => Fixtures = Builder.Build(new[] { typeof(TestFixtures.SimpleFixtureWithContainerFixtureAsField).GetTypeInfo() }));
        Then("the number of built fixtures should be 1", () => Fixtures.Count() == 1);

        var assemblyFixture = Fixtures.ElementAt(0);
        Then("the built fixture should be an Assembly fixture", () => assemblyFixture.FixtureDescriptor.FixtureAttributeType == typeof(AssemblyFixtureAttribute));
        Then("the parent fixture of the built fixture should be null", () => assemblyFixture.ParentFixture == null);

        var childFixturesOfAssemblyFixture = GetFixtures(assemblyFixture as FixtureContainer);
        Then("the number of fixtures contained by the Assembly fixture should be 1", () => childFixturesOfAssemblyFixture.Count() == 1);

        var namespaceFixture = childFixturesOfAssemblyFixture.ElementAt(0);
        Then("the fixture contained by the Assembly fixture should be a Namespace fixture", () => namespaceFixture.FixtureDescriptor.FixtureAttributeType == typeof(NamespaceFixtureAttribute));
        Then("the parent fixture of the fixture contained by the Assembly fixture should be the Assembly fixture", () => namespaceFixture.ParentFixture == assemblyFixture);

        var childFixturesOfNamespaceFixture = GetFixtures(namespaceFixture as FixtureContainer);
        Then("the number of fixtures contained by the Namespace fixture should be 1", () => childFixturesOfNamespaceFixture.Count() == 1);

        var requirementFixture = childFixturesOfNamespaceFixture.ElementAt(0);
        Then("the fixture contained by the Namespace fixture should be a Requirement fixture", () => requirementFixture.FixtureDescriptor.FixtureAttributeType == typeof(RequirementAttribute));
        Then("the parent fixture of the fixture contained by the Namespace fixture should be the Namespace fixture", () => requirementFixture.ParentFixture == namespaceFixture);

        var childFixturesOfRequirementFixture = GetFixtures(requirementFixture as FixtureContainer);
        Then("the number of fixtures contained by the Requirement fixture should be 1", () => childFixturesOfRequirementFixture.Count() == 1);

        var contextFixture = childFixturesOfRequirementFixture.ElementAt(0);
        Then("the fixture contained by the Requirement fixture should be a Context fixture", () => contextFixture.FixtureDescriptor.FixtureAttributeType == typeof(ContextAttribute));
        Then("the parent fixture of the fixture contained by the Requirement fixture should be the Requirement fixture", () => contextFixture.ParentFixture == requirementFixture);

        var childFixturesOfContextFixture = GetFixtures(contextFixture as FixtureContainer);
        Then("the number of fixtures contained by the Context fixture should be 1", () => childFixturesOfContextFixture.Count() == 1);

        var exampleFixture = childFixturesOfContextFixture.ElementAt(0);
        Then("the fixture contained by the Context fixture should be an Example fixture", () => exampleFixture.FixtureDescriptor.FixtureAttributeType == typeof(ExampleAttribute));
        Then("the parent fixture of the fixture contained by the Context fixture should be the parent fixture of the Context fixture", () => exampleFixture.ParentFixture == contextFixture.ParentFixture);
    }

    [Example("Fixture contains a container fixture as a type (Requirement fixture > Context fixture > Example fixture)")]
    void Ex06()
    {
        When("a fixture is built", () => Fixtures = Builder.Build(new[] { typeof(TestFixtures.SimpleFixtureWithContainerFixtureAsType).GetTypeInfo() }));
        Then("the number of built fixtures should be 1", () => Fixtures.Count() == 1);

        var assemblyFixture = Fixtures.ElementAt(0);
        Then("the built fixture should be an Assembly fixture", () => assemblyFixture.FixtureDescriptor.FixtureAttributeType == typeof(AssemblyFixtureAttribute));
        Then("the parent fixture of the built fixture should be null", () => assemblyFixture.ParentFixture == null);

        var childFixturesOfAssemblyFixture = GetFixtures(assemblyFixture as FixtureContainer);
        Then("the number of fixtures contained by the Assembly fixture should be 1", () => childFixturesOfAssemblyFixture.Count() == 1);

        var namespaceFixture = childFixturesOfAssemblyFixture.ElementAt(0);
        Then("the fixture contained by the Assembly fixture should be a Namespace fixture", () => namespaceFixture.FixtureDescriptor.FixtureAttributeType == typeof(NamespaceFixtureAttribute));
        Then("the parent fixture of the fixture contained by the Assembly fixture should be the Assembly fixture", () => namespaceFixture.ParentFixture == assemblyFixture);

        var childFixturesOfNamespaceFixture = GetFixtures(namespaceFixture as FixtureContainer);
        Then("the number of fixtures contained by the Namespace fixture should be 1", () => childFixturesOfNamespaceFixture.Count() == 1);

        var requirementFixture = childFixturesOfNamespaceFixture.ElementAt(0);
        Then("the fixture contained by the Namespace fixture should be a Requirement fixture", () => requirementFixture.FixtureDescriptor.FixtureAttributeType == typeof(RequirementAttribute));
        Then("the parent fixture of the fixture contained by the Namespace fixture should be the Namespace fixture", () => requirementFixture.ParentFixture == namespaceFixture);

        var childFixturesOfRequirementFixture = GetFixtures(requirementFixture as FixtureContainer);
        Then("the number of fixtures contained by the Requirement fixture should be 1", () => childFixturesOfRequirementFixture.Count() == 1);

        var contextFixture = childFixturesOfRequirementFixture.ElementAt(0);
        Then("the fixture contained by the Requirement fixture should be a Context fixture", () => contextFixture.FixtureDescriptor.FixtureAttributeType == typeof(ContextAttribute));
        Then("the parent fixture of the fixture contained by the Requirement fixture should be the Requirement fixture", () => contextFixture.ParentFixture == requirementFixture);

        var childFixturesOfContextFixture = GetFixtures(contextFixture as FixtureContainer);
        Then("the number of fixtures contained by the Context fixture should be 1", () => childFixturesOfContextFixture.Count() == 1);

        var exampleFixture = childFixturesOfContextFixture.ElementAt(0);
        Then("the fixture contained by the Context fixture should be an Example fixture", () => exampleFixture.FixtureDescriptor.FixtureAttributeType == typeof(ExampleAttribute));
        Then("the parent fixture of the fixture contained by the Context fixture should be the parent fixture of the Context fixture", () => exampleFixture.ParentFixture == contextFixture.ParentFixture);
    }

    [Example("Fixture contains a fixture method with a sample (Context fixture > Example fixture (1 sample))")]
    void Ex07()
    {
        When("a fixture is built", () => Fixtures = Builder.Build(new[] { typeof(TestFixtures.SimpleFixtureWithSample).GetTypeInfo() }));
        Then("the number of built fixtures should be 1", () => Fixtures.Count() == 1);

        var assemblyFixture = Fixtures.ElementAt(0);
        Then("the built fixture should be an Assembly fixture", () => assemblyFixture.FixtureDescriptor.FixtureAttributeType == typeof(AssemblyFixtureAttribute));
        Then("the parent fixture of the built fixture should be null", () => assemblyFixture.ParentFixture == null);

        var childFixturesOfAssemblyFixture = GetFixtures(assemblyFixture as FixtureContainer);
        Then("the number of fixtures contained by the Assembly fixture should be 1", () => childFixturesOfAssemblyFixture.Count() == 1);

        var namespaceFixture = childFixturesOfAssemblyFixture.ElementAt(0);
        Then("the fixture contained by the Assembly fixture should be a Namespace fixture", () => namespaceFixture.FixtureDescriptor.FixtureAttributeType == typeof(NamespaceFixtureAttribute));
        Then("the parent fixture of the fixture contained by the Assembly fixture should be the Assembly fixture", () => namespaceFixture.ParentFixture == assemblyFixture);

        var childFixturesOfNamespaceFixture = GetFixtures(namespaceFixture as FixtureContainer);
        Then("the number of fixtures contained by the Namespace fixture should be 1", () => childFixturesOfNamespaceFixture.Count() == 1);

        var contextFixture = childFixturesOfNamespaceFixture.ElementAt(0);
        Then("the fixture contained by the Namespace fixture should be a Context fixture", () => contextFixture.FixtureDescriptor.FixtureAttributeType == typeof(ContextAttribute));
        Then("the parent fixture of the fixture contained by the Namespace fixture should be the Namespace fixture", () => contextFixture.ParentFixture == namespaceFixture);

        var childFixturesOfContextFixture = GetFixtures(contextFixture as FixtureContainer);
        Then("the number of fixtures contained by the Context fixture should be 1", () => childFixturesOfContextFixture.Count() == 1);

        var exampleFixture = childFixturesOfContextFixture.ElementAt(0);
        Then("the fixture contained by the Context fixture should be an Example fixture", () => exampleFixture.FixtureDescriptor.FixtureAttributeType == typeof(ExampleAttribute));
        Then("the parent fixture contained by the Context fixture should be the parent fixture of the Context fixture", () => exampleFixture.ParentFixture == contextFixture.ParentFixture);

        var childFixturesOfExampleFixture = GetFixtures(exampleFixture as FixtureContainer);
        Then("the number of fixtures contained by the Example fixture should be 1", () => childFixturesOfExampleFixture.Count() == 1);

        var sampleFixture = childFixturesOfExampleFixture.ElementAt(0);
        Then("the fixture contained by the Example fixture should be a Sample fixture", () => sampleFixture.FixtureDescriptor.FixtureAttributeType == typeof(SampleFixtureAttribute));
        Then("the description of the Sample fixture should be one created with the specified sample data", () => sampleFixture.FixtureDescriptor.Description == "parameter1=1, parameter2=parameter, parameter3=True");
        Then("the parent fixture of the fixture contained by the Example fixture should be the parent fixture of the Example fixture", () => sampleFixture.ParentFixture == exampleFixture.ParentFixture);

        var sampleData = GetSampleData(sampleFixture as Fixture);
        Then("the sample data of the Sample fixture should be the specified data", () => Equals(sampleData[0], 1) && Equals(sampleData[1], "parameter") && Equals(sampleData[2], true));
    }

    [Example("Fixture contains a fixture method with some samples (Context fixture > Example fixture (3 samples))")]
    void Ex08()
    {
        When("a fixture is built", () => Fixtures = Builder.Build(new[] { typeof(TestFixtures.SimpleFixtureWithSamples).GetTypeInfo() }));
        Then("the number of built fixtures should be 1", () => Fixtures.Count() == 1);

        var assemblyFixture = Fixtures.ElementAt(0);
        Then("the built fixture should be an Assembly fixture", () => assemblyFixture.FixtureDescriptor.FixtureAttributeType == typeof(AssemblyFixtureAttribute));
        Then("the parent fixture of the built fixture should be null", () => assemblyFixture.ParentFixture == null);

        var childFixturesOfAssemblyFixture = GetFixtures(assemblyFixture as FixtureContainer);
        Then("the number of fixtures contained by the Assembly fixture should be 1", () => childFixturesOfAssemblyFixture.Count() == 1);

        var namespaceFixture = childFixturesOfAssemblyFixture.ElementAt(0);
        Then("the fixture contained by the Assembly fixture should be a Namespace fixture", () => namespaceFixture.FixtureDescriptor.FixtureAttributeType == typeof(NamespaceFixtureAttribute));
        Then("the parent fixture of the fixture contained by the Assembly fixture should be the Assembly fixture", () => namespaceFixture.ParentFixture == assemblyFixture);

        var childFixturesOfNamespaceFixture = GetFixtures(namespaceFixture as FixtureContainer);
        Then("the number of fixtures contained by the Namespace fixture should be 1", () => childFixturesOfNamespaceFixture.Count() == 1);

        var contextFixture = childFixturesOfNamespaceFixture.ElementAt(0);
        Then("the fixture contained by the Namespace fixture should be a Context fixture", () => contextFixture.FixtureDescriptor.FixtureAttributeType == typeof(ContextAttribute));
        Then("the parent fixture of the fixture contained by the Namespace fixture should be the Namespace fixture", () => contextFixture.ParentFixture == namespaceFixture);

        var childFixturesOfContextFixture = GetFixtures(contextFixture as FixtureContainer);
        Then("the number of fixtures contained by the Context fixture should be 1", () => childFixturesOfContextFixture.Count() == 1);

        var exampleFixture = childFixturesOfContextFixture.ElementAt(0);
        Then("the fixture contained by the Context fixture should be an Example fixture", () => exampleFixture.FixtureDescriptor.FixtureAttributeType == typeof(ExampleAttribute));
        Then("the parent fixture of the fixture contained by the Context fixture should be the parent fixture of the Context fixture", () => exampleFixture.ParentFixture == contextFixture.ParentFixture);

        var childFixturesOfExampleFixture = GetFixtures(exampleFixture as FixtureContainer).ToList();
        Then("the number of fixtures contained by the Example fixture should be 3", () => childFixturesOfExampleFixture.Count == 3);

        AssertSample(childFixturesOfExampleFixture.ElementAt(0), "first", "Sample #1", 1, "parameter1", true);
        AssertSample(childFixturesOfExampleFixture.ElementAt(1), "second", "Sample #2", 2, "parameter2", false);
        AssertSample(childFixturesOfExampleFixture.ElementAt(2), "third", "Sample #3", 3, "parameter3", true);
        Then("the parent fixture of the fixtures contained by the Example fixture should be the parent fixture of the Example fixture", () => childFixturesOfExampleFixture.All(childFixture => childFixture.ParentFixture == exampleFixture.ParentFixture));
    }

    [Example("Fixture contains a fixture method with a sample data source (Context fixture > Example fixture (3 samples))")]
    void Ex09()
    {
        When("a fixture is built", () => Fixtures = Builder.Build(new[] { typeof(TestFixtures.SimpleFixtureWithSampleDataSource).GetTypeInfo() }));
        Then("the number of built fixtures should be 1", () => Fixtures.Count() == 1);

        var assemblyFixture = Fixtures.ElementAt(0);
        Then("the built fixture should be an Assembly fixture", () => assemblyFixture.FixtureDescriptor.FixtureAttributeType == typeof(AssemblyFixtureAttribute));
        Then("the parent fixture of the built fixture should be null", () => assemblyFixture.ParentFixture == null);

        var childFixturesOfAssemblyFixture = GetFixtures(assemblyFixture as FixtureContainer);
        Then("the number of fixtures contained by the Assembly fixture should be 1", () => childFixturesOfAssemblyFixture.Count() == 1);

        var namespaceFixture = childFixturesOfAssemblyFixture.ElementAt(0);
        Then("the fixture contained by the Assembly fixture should be a Namespace fixture", () => namespaceFixture.FixtureDescriptor.FixtureAttributeType == typeof(NamespaceFixtureAttribute));
        Then("the parent fixture of the fixture contained by the Assembly fixture should be the Assembly fixture", () => namespaceFixture.ParentFixture == assemblyFixture);

        var childFixturesOfNamespaceFixture = GetFixtures(namespaceFixture as FixtureContainer);
        Then("the number of fixtures contained by the Namespace fixture should be 1", () => childFixturesOfNamespaceFixture.Count() == 1);

        var contextFixture = childFixturesOfNamespaceFixture.ElementAt(0);
        Then("the fixture contained by the Namespace fixture should be a Context fixture", () => contextFixture.FixtureDescriptor.FixtureAttributeType == typeof(ContextAttribute));
        Then("the parent fixture of the fixture contained by the Namespace fixture should be the Namespace fixture", () => contextFixture.ParentFixture == namespaceFixture);

        var childFixturesOfContextFixture = GetFixtures(contextFixture as FixtureContainer);
        Then("the number of fixtures contained by the Context fixture should be 1", () => childFixturesOfContextFixture.Count() == 1);

        var exampleFixture = childFixturesOfContextFixture.ElementAt(0);
        Then("the fixture contained by the Context fixture should be an Example fixture", () => exampleFixture.FixtureDescriptor.FixtureAttributeType == typeof(ExampleAttribute));
        Then("the parent fixture of the fixture contained by the Context fixture should be the parent fixture of the Context fixture", () => exampleFixture.ParentFixture == contextFixture.ParentFixture);

        var childFixturesOfExampleFixture = GetFixtures(exampleFixture as FixtureContainer).ToList();
        Then("the number of fixtures contained by the Example fixture should be 3", () => childFixturesOfExampleFixture.Count == 3);

        AssertSample(childFixturesOfExampleFixture.ElementAt(0), "first", "Sample #1", 1, "parameter1", true);
        AssertSample(childFixturesOfExampleFixture.ElementAt(1), "second", "parameter1=2, parameter2=parameter2, parameter3=False", 2, "parameter2", false);
        AssertSample(childFixturesOfExampleFixture.ElementAt(2), "third", "Sample #3", 3, "parameter3", true);
        Then("the parent fixture of the fixtures contained by the Example fixture should be the parent fixture of the Example fixture", () => childFixturesOfExampleFixture.All(childFixture => childFixture.ParentFixture == exampleFixture.ParentFixture));
    }

    void AssertSample(IFixture sampleFixture, string nth, string expectedDescription, params object[] expectedSampleData)
    {
        Then($"the {nth} fixture contained by the Example fixture should be a Sample fixture", () => sampleFixture.FixtureDescriptor.FixtureAttributeType == typeof(SampleFixtureAttribute));
        Then($"the description of the {nth} Sample fixture should be the specified description", () => sampleFixture.FixtureDescriptor.Description == expectedDescription);

        var sampleData = GetSampleData(sampleFixture as Fixture);
        Then($"the sample data of the {nth} Sample fixture should be the specified data", () =>
            Equals(sampleData[0], expectedSampleData[0]) && Equals(sampleData[1], expectedSampleData[1]) && Equals(sampleData[2], expectedSampleData[2])
        );
    }

    [Example("Fixture contains sample fixture that has a source that does not implement ISampleDataSource")]
    void Ex10()
    {
        When("a fixture is built", () => Builder.Build(new[] { typeof(TestFixtures.SimpleFixtureWithInvalidSampleDataSource).GetTypeInfo() }));
        Then("InvalidSampleDataSourceTypeException should be thrown", exc => exc.GetType() == typeof(InvalidSampleDataSourceTypeException));
    }

    [Example("Fixture contains sample fixture that has a source that does not have a parameterless constructor")]
    void Ex11()
    {
        When("a fixture is built", () => Builder.Build(new[] { typeof(TestFixtures.SimpleFixtureWithSampleDataSourceWithoutParameterlessConstructor).GetTypeInfo() }));
        Then("InvalidSampleDataSourceTypeException should be thrown", exc => exc.GetType() == typeof(InvalidSampleDataSourceTypeException));
    }

    private IEnumerable<IFixture> GetFixtures(FixtureContainer? container)
        => container?.GetType().GetProperty("Fixtures", BindingFlags.Instance | BindingFlags.NonPublic)?.GetValue(container) as IEnumerable<IFixture> ?? Enumerable.Empty<IFixture>();

    private object?[] GetSampleData(Fixture? fixture)
        => fixture?.GetType().GetProperty("SampleData", BindingFlags.Instance | BindingFlags.NonPublic)?.GetValue(fixture) as object?[] ?? Array.Empty<object?>();
}