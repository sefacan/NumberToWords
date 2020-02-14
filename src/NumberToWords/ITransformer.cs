namespace NumberToWords
{
    public interface ITransformer
    {
        string ToWords(int value);

        string ToCurrencyWords(decimal currency, string currencyCode);
    }
}