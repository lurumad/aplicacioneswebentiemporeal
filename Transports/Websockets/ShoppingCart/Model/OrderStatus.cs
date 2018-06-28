namespace ShoppingCart.Model
{
    public enum OrderStatus
    {
        Submitted = 1,
        AwaitingValidation,
        StockConfirmed,
        Paid,
        Shipped
    }
}
