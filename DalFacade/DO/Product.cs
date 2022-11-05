

namespace DO;

public struct Product
{
    //public  Product(int Id, string Name, int Price, Category Category, int InStock)
    //{
    //    this.UniqID = Id;
    //    this.Name = Name;
    //    this.Price = Price;
    //    this.Category = Category;
    //    this.InStock = InStock;
    //}
    //public static (int,string,int,Category,int) Assign()
    //{
    //   // Product p = new Product();
    //    int id = insertInteger("Enter station id: ");
    //    int name = insertInteger("Enter station name: ");
    //    double latitude = insertDouble("Enter lattitude: ");
    //    double longitude = insertDouble("Enter longitude: ");
    //    int chargeSlotavailable = insertInteger("Enter available chargeSlot: ");

    //    //IBL.BO.BaseStation baseStation = new(id, name, new(lattitude, longitude), chargeSlotavailable, null);//List have to be empty
    //    return (id, name, latitude, longitude, chargeSlotavailable);
    //}
    public int UniqID { get; set; }
    public string Name { get; set; }
    public double Price { get; set; }
    public Category? Category { get; set; }
    public int? InStock { get; set; }

    public override string ToString() => $@"
UniqID: {UniqID}
{Name}
category - {Category}
Price: {Price}
Amount in stock: {InStock}";


}
