# TestFountain

[![.NET Publish](https://github.com/JaCraig/TestFountain/actions/workflows/dotnet-publish.yml/badge.svg)](https://github.com/JaCraig/TestFountain/actions/workflows/dotnet-publish.yml) [![Coverage Status](https://coveralls.io/repos/github/JaCraig/TestFountain/badge.svg?branch=master)](https://coveralls.io/github/JaCraig/TestFountain?branch=master)

TestFountain is a C# library that provides an attribute for [xUnit.Net](https://xunit.net/) to generate random test data. It aims to simplify the process of generating diverse and comprehensive test cases by automatically creating random input values for your xUnit.Net tests.

## Features

- **Random Data Generation**: TestFountain allows you to easily generate random test data for your xUnit.Net tests. By using the `FountainDataAttribute`, you can annotate your test methods and have them automatically receive randomized input parameters.

- **Customization**: You can customize the generated data by specifying the data type, range, length, and other attributes using various options provided by TestFountain.

- **Simplified Test Cases**: With TestFountain, you no longer need to manually define and manage multiple test cases. It generates a wide range of test inputs automatically, allowing you to focus on writing assertions and verifying the behavior of your code.

## Getting Started

### Prerequisites

- .NET 8.0 or later

### Installation

You can install TestFountain via NuGet package manager or by adding a reference to your project file.

#### Using NuGet Package Manager

1. Open the NuGet Package Manager Console in Visual Studio.
2. Execute the following command:
   ```shell
   Install-Package TestFountain
   ```

#### Adding a Reference

1. Right-click on your project in Visual Studio.
2. Select "Manage NuGet Packages."
3. Search for "TestFountain" and click on "Install."

### Usage

To use TestFountain in your xUnit.Net tests, follow these steps:

1. Annotate your test method with the `RandomDataAttribute` and specify the desired options:
   ```csharp
   [Theory]
   [FountainData]
   public void MyRandomTestMethod(int randomNumber, string randomString)
   {
       // Use the random values for testing
   }
   ```

   In this example, `randomNumber` and `randomString` will be automatically populated with random values each time the test runs.

2. Customize the generated data by using the available options. For example:
   ```csharp
   [Theory]
   [FountainData(10)]
   public void MyRandomTestMethod(int randomNumber)
   {
       // Use the random integer value between 0 and 100 for testing
   }
   ```

3. Run your xUnit.Net tests as usual, and TestFountain will generate random test data for your annotated test methods.