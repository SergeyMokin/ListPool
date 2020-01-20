# ListPool

Allocation-free implementation of IList using ArrayPool with two variants, ListPool and ValueListPool

[![GitHub Workflow Status](https://img.shields.io/github/workflow/status/faustodavid/ListPool/Build)](https://github.com/faustodavid/ListPool/actions)
[![Coveralls github](https://img.shields.io/coveralls/github/faustodavid/ListPool)](https://coveralls.io/github/faustodavid/ListPool)
[![Nuget](https://img.shields.io/nuget/v/ListPool)](https://www.nuget.org/packages/ListPool/)
[![GitHub](https://img.shields.io/github/license/faustodavid/ListPool)](https://github.com/faustodavid/ListPool/blob/master/LICENSE)


## Installation

Available on [nuget](https://www.nuget.org/packages/ListPool/)

	PM> Install-Package ListPool

Requirements:
* netstandard2.1 or above
* dotnet core 3.0 or above

## Introduction

When performance matter, ListPool provides all the goodness of ArrayPool with the usability of IList and support for Span.

It has two variants ListPool and ValueListPool.

Differences:

* ListPool:
  * ReferenceType
  * Serializable
  * Because it is a class it has a constant heap allocation of 64kb regardless the size

* ValueListPool
  * ValueType
  * High-performance
  * Allocation-free
  * Cannot be deserialized
  * Cannot be created with parameterless constructors, otherwise it is created in an invalid state
  * Because it is ValueType when it is passed to other methods, it is passed by copy, not by reference. It is good for performance, but any modifications don't affect the original instance. In case it is required to be updated, we need to use the "ref" keyword in the parameter.

 ## How to use

 ListPool and ValueListPool implement IDisposable. After finishing their use, you must dispose the list.

 Examples

 Deserialization:

 ```csharp
static async Task Main()
{
    var httpClient = HttpClientFactory.Create();
    var stream = await httpClient.GetStreamAsync("examplePath");
    using var examples = await JsonSerializer.DeserializeAsync<ListPool<string>>(stream); 
    ...
}
 ```

 Mapping domain object to dto:
 Note: ValueListPool is not been dispose at `MapToResult`. It is dispose at the caller.

  ```csharp
static void Main()
{
    using ValueListPool<Example> examples = new GetAllExamplesUseCase().Query();
    using ValueListPool<ExampleResult> exampleResults = MapToResult(examples); 
    ...
}

public static ValueListPool<ExampleResult> MapToResult(IReadOnlyCollection<Example> examples)
{
    ValueListPool<ExampleResult> examplesResult = new ValueListPool<ExampleResult>(examples.Count);
    foreach (var example in examples)
    {
        examplesResult.Add(new ExampleResult(example));
    }

    return examplesResult;
}
  ```

Mapping a domain object to dto using LINQ (It perform slower than with foreach):

  ```csharp
static void Main()
{
    using ValueListPool<Example> examples = new GetAllExamplesUseCase().Query();
    using ValueListPool<ExampleResult> examplesResult = examples.Select(example => new ExampleResult(example)).ToValueListPool();
    ...
}
  ```


## Contributors

A big thanks to the project contributors!

* [Sergey Mokin](https://github.com/SergeyMokin)
