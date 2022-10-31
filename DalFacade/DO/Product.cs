
using DocumentFormat.OpenXml.Drawing.Diagrams;
using DocumentFormat.OpenXml.Office2010.Excel;

namespace DO;

public struct Product
{
    public int UniqID { get; set; }
    public string Name { get; set; }
    public double Price { get; set; }
    public Category Category { get; set; }
    public int InStock { get; set; }

    public override string ToString() => $@"
UniqID: {UniqID}
{Name}
category - {Category}
Price: {Price}
Amount in stock: {InStock}";


}
