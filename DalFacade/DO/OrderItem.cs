
namespace DO;

public struct OrderItem
{
    public int UniqID { get; set; }
    public int ProductID { get; set; }
    public int OrderID { get; set; }
    public double Price { get; set; }
    public int Amount { get; set; }
    

    public override string ToString() => $@" 
Uniq ID: {UniqID}
Product ID: {ProductID}
Order ID: {OrderID} 
Amount: {Amount} 
Price: {Price} 

";



}
