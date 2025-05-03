using System;
using System.Collections.Generic;
using Data.PersistentData;
using EventBus.Events;
using EventBusSystem;
using Miscs;

namespace Data.Controllers
{
    [Serializable]
    public class CurrencyDataController
    {
        public Dictionary<CurrencyType, OwnedCurrencyData> OwnedCurrencies = new()
        {
            { CurrencyType.Coin, new OwnedCurrencyData { Type = CurrencyType.Coin, Amount = 0 } },
        };

        public CurrencyDataController()
        {

        }

        public void IncreaseCurrency(CurrencyType type, int amount)
        {
            var isExist = OwnedCurrencies.TryGetValue(type, out var currency);
            if (isExist)
            {
                currency.Amount += amount;
                EventBusNew.Raise(new SaveDataEvent());
            }
        }

        public void DecreaseCurrency(CurrencyType type, int amount)
        {
            var isExist = OwnedCurrencies.TryGetValue(type, out var currency);
            if (isExist)
            {
                currency.Amount -= amount;
                if (currency.Amount <= 0)
                    currency.Amount = 0;
            
                EventBusNew.Raise(new SaveDataEvent());
            }
        }

        public bool HasEnoughCurrency(CurrencyType type, int amount)
        {
            var isExist = OwnedCurrencies.TryGetValue(type, out var currency);
            if (isExist)
                return currency.Amount >= amount;

            return false;
        }

        public bool TryToDecreaseCurrency(CurrencyType type, int amount)
        {
            if (HasEnoughCurrency(type, amount))
            {
                DecreaseCurrency(type, amount);
                return true;
            }

            return false;
        }

        public OwnedCurrencyData GetOwnedCurrency(CurrencyType type)
        {
            OwnedCurrencies.TryGetValue(type, out var currency);
            return currency;
        }
    }
}