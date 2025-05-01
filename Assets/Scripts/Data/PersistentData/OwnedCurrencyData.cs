using System;
using Miscs;

namespace Data.PersistentData
{
    [Serializable]
    public class OwnedCurrencyData
    {
        public CurrencyType Type;
        public int Amount;
    }
}