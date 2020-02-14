## NumberToWords - Convert number or currency values to Turkish words

.Net Standard 2.1 package for converting number or currency values to Turkish words.

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
