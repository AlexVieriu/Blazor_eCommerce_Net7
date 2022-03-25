namespace eShop.CoreBusiness.Models;
public class Order
{
    public int OrderId { get; set; }
    public DateTime DatePlaced { get; set; }
    public DateTime DateProcessing { get; set; }
    public DateTime DateProcessed { get; set; }
    public string CustomerName { get; set; }
    public string CustomerAddress { get; set; }
    public string CustomerCity { get; set; }
    public string CsutomerStateProvince { get; set; }
    public string CustomerCountry { get; set; }
    public string AdminUser { get; set; }
    public int UniqueId { get; set; }

    List<OrderLineItem> LineItems { get; set; }

    public void AddProduct(int productId, int qty, double price)
    {

    }

    public void RemoveProduct(int productId)
    {

    }
}
