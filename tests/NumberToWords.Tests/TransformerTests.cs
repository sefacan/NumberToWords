using System;
using System.Globalization;
using Xunit;

namespace NumberToWords.Tests
{
    public class TransformerTests
    {
        [Fact]
        public void ThrowsException_Transformer_From_Factory_With_EmptyParam()
        {
            var transformerFactory = new TransformerFactory();
            Assert.Throws<ArgumentException>(() => transformerFactory.Create(null));
        }

        [Fact]
        public void ThrowsException_Transformer_From_Factory_With_NotSupportedParam()
        {
            var transformerFactory = new TransformerFactory();
            Assert.Throws<NotSupportedException>(() => transformerFactory.Create("en"));
        }

        [Fact]
        public void Create_Transformer_From_Factory()
        {
            var transformerFactory = new TransformerFactory();
            var transformer = transformerFactory.Create("tr");

            Assert.NotNull(transformer);
        }

        [Fact]
        public void Convert_Currency_ToWords()
        {
            var transformerFactory = new TransformerFactory();
            var transformer = transformerFactory.Create("tr");
            var result = transformer.ToCurrencyWords(123456789.26m, "TRY");

            CultureInfo.DefaultThreadCurrentCulture = new CultureInfo("tr-TR");

            Assert.Equal("YÜZ YİRMİ ÜÇ MİLYON DÖRT YÜZ ELLİ ALTI BİN YEDİ YÜZ SEKSEN DOKUZ TÜRK LİRASI YİRMİ ALTI KURUŞ", result.ToUpper());
        }

        [Fact]
        public void Convert_Number_ToWords()
        {
            var transformerFactory = new TransformerFactory();
            var transformer = transformerFactory.Create("tr");
            var result = transformer.ToWords(123456789);

            CultureInfo.DefaultThreadCurrentCulture = new CultureInfo("tr-TR");

            Assert.Equal("YÜZ YİRMİ ÜÇ MİLYON DÖRT YÜZ ELLİ ALTI BİN YEDİ YÜZ SEKSEN DOKUZ", result.ToUpper());
        }
    }
}