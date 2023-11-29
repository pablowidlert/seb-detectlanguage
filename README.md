# SEB Detect Language Test

## Prerequisite
This project uses .NET 8 Download [here](https://dotnet.microsoft.com/en-us/download/dotnet)

## Build

Open the solution in Visual Studio.

Let NuGet download the packages and then:
- Click *Build* -> *Build Solution* (This will take several minutes)

## Running integration tests

**NOTE**: You need to provide your own DetectLanguageAPIToken in the SEBDetectLanguageProperties.json file for the tests to work locally.
The GitHub Action will trigger on a push or a pull request but can be triggered manually. GitHub Actions uses an API key for Detect Language that is stored in GitHub Secrets and passed into the GitHub Action.

The integration tests can be run:
- via Visual Studio Test Explorer:
  - [Run unit tests with Test Explorer](https://docs.microsoft.com/en-us/visualstudio/test/run-unit-tests-with-test-explorer?view=vs-2019)
- via CLI:
  - from API.IntegrationTests root folder
    - run `dotnet test`

## Authors

* **Pablo Widlert** - API integration tests and GitHub Action yaml
