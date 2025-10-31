namespace Toyota.Shared.Extensions
{
    public static class RandomGeneratorExtensions
    {
        public static List<string> GetRandomCodeGeneratorList(int quantity,int length)
        {
            var randomNumber = new Random((int)DateTime.UtcNow.Ticks);
            List<string> generatedVouchers = new();
            char[] keys = "ABCDEFGHIJKLMNOPQRSTUVWXYZ01234567890".ToCharArray();

            while (generatedVouchers.Count < quantity)
            {
                var voucher = Enumerable
                    .Range(1, length)
                    .Select(k => keys[randomNumber.Next(0, keys.Length - 1)])  // generate a new random char 
                    .Aggregate("", (e, c) => e + c);

                if (!generatedVouchers.Contains(voucher))
                    generatedVouchers.Add(voucher);

            }

            return generatedVouchers;
        }
    }
}
