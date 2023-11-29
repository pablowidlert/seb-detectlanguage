# SEB Detect Language Test


## Build

Open the solution in Visual Studio.

Let NuGet download the packages and then:
- Click *Build* -> *Build Solution* (This will take several minutes)

## Running integration tests

**NOTE**: You need to provide your own DetectLanguageAPIToken in the SEBDetectLanguageProperties.json file for the tests to work.

The integration tests can be run:
- via Visual Studio Test Explorer:
  - [Run unit tests with Test Explorer](https://docs.microsoft.com/en-us/visualstudio/test/run-unit-tests-with-test-explorer?view=vs-2019)
- via CLI:
  - from API.IntegrationTests root folder
    - run `dotnet test`

## Authors

* **Pablo Widlert** - API integration tests