## NumberToWords - Convert number or currency values to Turkish words

.Net Standard 2.1 package for converting number or currency values to Turkish words.

### Build Status
| Build server    | Platform       | Status      |
|-----------------|----------------|-------------|
| Azure CI Pipelines  | All            |![](https://dev.azure.com/fsefacan/NumberToWords/_apis/build/status/sefacan.NumberToWords?branchName=master) |
| Github Actions  | All            |![](https://github.com/sefacan/NumberToWords/workflows/.NET%20Core%20CI/badge.svg) |
| Travis CI       | Linux  |![](https://travis-ci.org/sefacan/NumberToWords.svg?branch=master) |

## Installation

Install the [NumberToWords NuGet Package](https://www.nuget.org/packages/Number2Words).

### Package Manager Console

```
Install-Package Number2Words
```

### .NET Core CLI

```
dotnet add package Number2Words
```

### Usage

``` csharp
ITransformerFactory transformerFactory = new TransformerFactory() // create transformer factory
var transformer = transformerFactory.Create("tr");

// currency converting
var currencyToWords = transformer.ToCurrencyWords(123456789.26m, "TRY");
// YÜZ YİRMİ ÜÇ MİLYON DÖRT YÜZ ELLİ ALTI BİN YEDİ YÜZ SEKSEN DOKUZ LİRA YİRMİ ALTI KURUŞ

// number converting
var numberToWords = transformer.ToWords(123456789);
// YÜZ YİRMİ ÜÇ MİLYON DÖRT YÜZ ELLİ ALTI BİN YEDİ YÜZ SEKSEN DOKUZ
```
