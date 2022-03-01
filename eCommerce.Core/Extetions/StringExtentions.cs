namespace eCommerce.Core.Extetions
{
    public static class StringExtensions
    {
        public static bool IsNullOrWhiteSpace(this string expression)
        {
            return string.IsNullOrWhiteSpace(expression);
        }
    }
}
