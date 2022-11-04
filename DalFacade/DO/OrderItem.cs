
namespace DO;

public struct OrderItem
{
    public int UniqId { get; set; }
    public int ProductID { get; set; }
    public int OrderID { get; set; }
    public double Price { get; set; }
    public int Amount { get; set; }
    

    public override string ToString() => $@" 
Product ID={ProductID}, 
Order ID=  {OrderID}, 
Amount: {Amount} 
Price: {Price} ";



}
