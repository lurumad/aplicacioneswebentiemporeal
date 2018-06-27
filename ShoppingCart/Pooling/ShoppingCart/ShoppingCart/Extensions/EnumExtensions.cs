using ShoppingCart.Model;

namespace System
{
    public static class EnumExtensions
    {
        public static OrderStatus Next(this OrderStatus status)
        {
            var values = (OrderStatus[])Enum.GetValues(typeof(OrderStatus));
            var nextIndex = Array.IndexOf(values, status) + 1;
            return values.Length == nextIndex ? values[0] : values[nextIndex];
        }
    }
}
