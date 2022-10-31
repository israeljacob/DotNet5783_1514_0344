
namespace DO;

public struct OrderItem
{
    public int ProductID { get; set; }
    public int OrderID { get; set; }
    public double Price { get; set; }
    public int Amount { get; set; }
    public int UniqID { get; set; }

    public override string ToString() => $@" 
UniqID: {UniqID}
Product ID={ProductID}, 
Order ID=  {OrderID}, 
Amount: {Amount} 
Price: {Price} ";



}
