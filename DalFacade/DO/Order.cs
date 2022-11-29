

namespace DO;
/// <summary>
/// an order
/// </summary>
public struct Order
{
    /// <summary>
    /// order ID.
    /// </summary>
    public int UniqID { get; set; }
    /// <summary>
    /// The name of the costomer.
    /// </summary>
    public string? CustomerName { get; set; }
    /// <summary>
    /// The email address of the customer.
    /// </summary>
    public string? CustomerEmail { get; set; }
    /// <summary>
    /// The address of the customer.
    /// </summary>
    public string? CustomerAdress { get; set; }
    /// <summary>
    /// The date (and time) that the order has been accepted.
    /// </summary>
    public DateTime? OrderDate { get; set; } 
    /// <summary>
    /// The date (and time) that the order has been sent.
    /// </summary>
    public DateTime? ShipDate { get; set; }
    /// <summary>
    /// The date (and time) that the order has been Deliverred.
    /// </summary>
    public DateTime? DeliveryrDate { get; set; }



    /// <summary>
    /// Converts the object to a printable form.
    /// </summary>
    /// <returns> the printable form. </returns>
    public override string ToString() => $@" 
        UniqID: {UniqID}
        Name: {CustomerName}, 
        Customer Email=  {CustomerEmail}, 
        Customer Adress: {CustomerAdress} 
        Order Date: {OrderDate}  
        Ship Date: {ShipDate} 
        Deliveryr Date: {DeliveryrDate}

";


    
}
