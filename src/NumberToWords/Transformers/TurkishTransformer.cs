using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace NumberToWords.Transformers
{
    public class TurkishTransformer : ITransformer
    {
        private const string minus = "eksi";
        private const string wordSeparator = " ";

        private readonly string[] exponent = new[] {
            "",
            "bin",
            "milyon",
            "milyar",
            "trilyon",
            "katrilyon",
            "kuintrilyon",
            "seksilyon",
            "septrilyon",
            "oktrilyon",
            "nanilyon",
            "desilyon",
            "andesilyon",
            "dudesilyon",
            "tredesilyon",
            "kattırdesilyon",
            "kuindesilyon",
            "seksdesilyon",
            "septendesilyon",
            "oktadesilyon",
            "novemdesilyon",
            "vijintilyon"
        };

        private readonly string[] digits = new[] {
            "sıfır",
            "bir",
            "iki",
            "üç",
            "dört",
            "beş",
            "altı",
            "yedi",
            "sekiz",
            "dokuz"
        };

        private readonly string[] digits_second = {
            "",
            "on",
            "yirmi",
            "otuz",
            "kırk",
            "elli",
            "altmış",
            "yetmiş",
            "seksen",
            "doksan",
            "yüz"
        };

        private readonly IDictionary<string, KeyValuePair<string, string>> currencyNames = new Dictionary<string, KeyValuePair<string, string>> {
            { "ALL", new KeyValuePair<string, string>("Arnavut leki", "qindarka") },
            { "AUD", new KeyValuePair<string, string>("Avusturalya doları", "sent") },
            { "BAM", new KeyValuePair<string, string>("Bosna-Hersek değiştirilebilir markı", "fenig") },
            { "BGN", new KeyValuePair<string, string>("Bulgar levası", "stotinka") },
            { "BRL", new KeyValuePair<string, string>("Brezilya reali", "centavos") },
            { "BWP", new KeyValuePair<string, string>("Botswana pulası", "thebe") },
            { "BYR", new KeyValuePair<string, string>("Belarus rublesi", "kopiejka") },
            { "CAD", new KeyValuePair<string, string>("Kanada doları", "sent") },
            { "CHF", new KeyValuePair<string, string>("İsveç frankı", "rapp") },
            { "CNY", new KeyValuePair<string, string>("Çin yuanı", "fen") },
            { "CYP", new KeyValuePair<string, string>("Kıbrıs poundu", "sent") },
            { "CZK", new KeyValuePair<string, string>("Çek kronu", "halerz") },
            { "DKK", new KeyValuePair<string, string>("Danimarka kronu", "ore") },
            { "EEK", new KeyValuePair<string, string>("Estonya kronu", "senti") },
            { "EUR", new KeyValuePair<string, string>("avro", "sent") },
            { "GBP", new KeyValuePair<string, string>("pound", "pence") },
            { "HKD", new KeyValuePair<string, string>("Hong Kong doları", "sent") },
            { "HRK", new KeyValuePair<string, string>("Hırvatistan kunası", "lipa") },
            { "HUF", new KeyValuePair<string, string>("Macar forinti", "filler") },
            { "ILS", new KeyValuePair<string, string>("İsrail şekeli", "agora") },
            { "ISK", new KeyValuePair<string, string>("Izlanda kronu", "aurar") },
            { "JPY", new KeyValuePair<string, string>("Japon yeni", "sen") },
            { "LTL", new KeyValuePair<string, string>("Litvanya litası", "sent") },
            { "LVL", new KeyValuePair<string, string>("Letonya latı", "sentim") },
            { "MKD", new KeyValuePair<string, string>("Makedonya dinarı", "deni") },
            { "MTL", new KeyValuePair<string, string>("Malta lirası", "centym") },
            { "NOK", new KeyValuePair<string, string>("Norveç kronu", "oere") },
            { "PLN", new KeyValuePair<string, string>("Polonya zlotisi", "grosz") },
            { "ROL", new KeyValuePair<string, string>("Roman leyi", "bani") },
            { "RUB", new KeyValuePair<string, string>("Rus rublesi", "kopiejka") },
            { "SEK", new KeyValuePair<string, string>("İsveç kronu", "oere") },
            { "SIT", new KeyValuePair<string, string>("Slovenya toları", "stotinia") },
            { "SKK", new KeyValuePair<string, string>("Slovakya kronu", "") },
            { "TRY", new KeyValuePair<string, string>("Türk lirası", "kuruş") },
            { "UAH", new KeyValuePair<string, string>("Ukrayna hryvnyası", "kopiyka") },
            { "USD", new KeyValuePair<string, string>("ABD doları", "sent") },
            { "YUM", new KeyValuePair<string, string>("Yugoslav dinarı", "para") },
            { "ZAR", new KeyValuePair<string, string>("Güney Afrika randı", "sent") }
        };

        public string ToWords(int value)
        {
            string result = string.Empty;
            string num = value.ToString();

            if (value == 0)
            {
                return digits[0];
            }

            if (num.Substring(0, 1) == "-")
            {
                result = $"{minus}{wordSeparator}";
                num = num.Substring(1);
            }

            num = Regex.Replace(num, "/^0+/", string.Empty);

            if (num.Length % 3 != 0)
                num = num.PadLeft(num.Length + (3 - (num.Length % 3)), '0');

            string[] groups = new string[num.Length / 3];
            for (int i = 0; i < num.Length / 3; i++)
            {
                groups[i] = num.Substring(i * 3, 3);
            }

            int index = 0;
            int groupIndex = groups.Length - 1;
            foreach (var group in groups)
            {
                if (int.Parse(group[0].ToString()) > 1)
                    result += digits[int.Parse(group[0].ToString())] + wordSeparator;

                if (int.Parse(group[0].ToString()) > 0)
                    result += $"yüz{wordSeparator}";

                if (int.Parse(group[1].ToString()) > 0)
                    result += digits_second[int.Parse(group[1].ToString())] + wordSeparator;

                if (int.Parse(group[2].ToString()) > 0 && !(num.Length == 4 && index == 0 && int.Parse(group[2].ToString()) <= 1) && (groupIndex == 1 ? int.Parse(group[2].ToString()) != 1 : true))
                    result += digits[int.Parse(group[2].ToString())] + wordSeparator;

                if (int.Parse(group) >= 1)
                    result += $"{exponent[groupIndex]}{wordSeparator}";

                index++;
                groupIndex--;
            }

            return result.Trim();
        }

        public string ToCurrencyWords(decimal currency, string currencyCode)
        {
            bool hasFraction = (currency % 1) != 0;

            currencyCode = currencyCode.ToUpper();
            if (!currencyNames.ContainsKey(currencyCode))
                throw new NotSupportedException($"Currency {currencyCode} is not available for this language!");

            var currencyName = currencyNames[currencyCode];
            string result = ToWords((int)currency).Trim();
            result += $"{wordSeparator}{currencyName.Key}";

            if (hasFraction)
            {
                var fraction = new string((currency - (int)currency).ToString().Where(char.IsDigit).ToArray());
                result += $"{wordSeparator}{ToWords(int.Parse(fraction)).Trim()}";

                if (int.Parse(fraction) != 1)
                {
                    result += $"{wordSeparator}{currencyName.Value}";
                }
                else
                {
                    result += $"{wordSeparator}{currencyName.Key}";
                }
            }

            return result.Trim();
        }
    }
}