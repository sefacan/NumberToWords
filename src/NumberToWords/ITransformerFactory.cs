namespace NumberToWords
{
    public interface ITransformerFactory
    {
        ITransformer Create(string locale);
    }
}