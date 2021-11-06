using System;

namespace DemoFP
{
    interface IMoney { }
    class Currency
    {
        public string Symbol { get; }
        public Currency(string symbol)
        {
            this.Symbol = !string.IsNullOrEmpty(symbol)
                ? symbol : throw new ArgumentException();
        }
    }
    class Amount
    {
        public decimal Value { get; }
        public Currency Currency { get; }

        public Amount(decimal value, Currency currency)
        {
            this.Value = value;
            this.Currency = currency ?? throw new ArgumentNullException(nameof(currency));
        }
    }
    class Cash : IMoney
    {
        public decimal Value { get; }
        public Currency Currency { get; }
        public Cash(decimal value, Currency currency)
        {
            this.Value = value;
            this.Currency = currency ?? throw new ArgumentNullException(nameof(currency));
        }
    }
    class Gift : IMoney
    {
        public decimal Value { get; }
        public Currency Currency { get; }
        public DateTime ValidBefore { get; }
        public Gift(decimal value, Currency currency, DateTime validBefore)
        {
            this.Value = value;
            this.Currency = currency ?? throw new ArgumentNullException(nameof(currency));
            this.ValidBefore = validBefore;
        }
    }
    class Program
    {
        static  (IMoney money, Amount added)  Add(IMoney money, Amount amount, DateTime at)
        {
            switch (money) 
            {
                case Cash cash when amount.Currency == cash.Currency:
                    return (new Cash(cash.Value + amount.Value, cash.Currency), amount);
                case Cash _:
                    return (money, new Amount(0, amount.Currency));
                case Gift gift when at < gift.ValidBefore && gift.Currency == amount.Currency:
                    return (new Gift(gift.Value + amount.Value, gift.Currency, gift.ValidBefore), amount);
                case Gift _:
                    return (money, new Amount(0, amount.Currency));
                default:
                    throw new ArgumentException();
            }
        }
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
        }
    }
}
