
namespace DO;
/// <summary>
/// An item that was orderred.
/// </summary>
public struct OrderItem
{
    /// <summary>
    ///  order item ID.
    /// </summary>
    public int UniqID { get; set; }
    /// <summary>
    /// The ID of the item.
    /// </summary>
    public int ProductID { get; set; }
    /// <summary>
    /// The ID of the order that this item was orderred by..
    /// </summary>
    public int OrderID { get; set; }
    /// <summary>
    /// The actual price of the product in the current order.
    /// </summary>
    public double Price { get; set; }
    /// <summary>
    /// The quantity of items of this product in the order.
    /// </summary>
    public int Amount { get; set; }

    /// <summary>
    /// Converts the object to a printable form.
    /// </summary>
    /// <returns> the printable form. </returns>
    public override string ToString() => $@" 
Uniq ID: {UniqID}
Product ID: {ProductID}
Order ID: {OrderID} 
Amount: {Amount} 
Price: {Price} 

";



}
