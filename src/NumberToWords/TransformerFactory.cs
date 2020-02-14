using NumberToWords.Transformers;
using System;
using System.Collections.Generic;

namespace NumberToWords
{
    public class TransformerFactory : ITransformerFactory
    {
        private IDictionary<string, Type> _transformers = new Dictionary<string, Type>
        {
            { "tr", typeof(TurkishTransformer) }
        };

        public ITransformer Create(string locale)
        {
            if (string.IsNullOrWhiteSpace(locale))
                throw new ArgumentException($"{nameof(locale)} cannot be null or empty!");

            if(!_transformers.ContainsKey(locale))
                throw new NotSupportedException($"{nameof(locale)} not support yet!");

            var transformer = _transformers[locale.ToLower().Trim()];
            return (ITransformer)Activator.CreateInstance(transformer);
        }
    }
}