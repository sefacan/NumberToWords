using System;
using System.Collections.Generic;
using System.Text;

namespace NumberToWords
{
    public interface ITransformerFactory
    {
        ITransformer Create(string locale);
    }
}