namespace eShop.Common.Utilities
{
    public static class StringUtilities
    {
        public static bool IsEmpty(this string input)
        {
            return string.IsNullOrEmpty(input);
        }

        public static bool IsNotEmpty(this string input)
        {
            return !IsEmpty(input);
        }

        public static bool IsNull(this object input)
        {
            return input == null;
        }
    }
}
