
public class ShoppingCartItem
{
	public string UserName { get; set; }
	public List<ShoppingCartItem> Items { get; set; }
	public decimal TotalPrice => Items.Sum(x => x.Price )
}