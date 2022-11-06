

namespace DO;

public struct Order
{
    



    public int UniqID { get; set; }
    public string CustomerName { get; set; }
    public string CustomerEmail { get; set; }
    public string CustomerAdress { get; set; }
    public DateTime OrderDate { get; set; }
    public DateTime ShipDate { get; set; }
    public DateTime DeliveryrDate  { get; set; }
    

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
