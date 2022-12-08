namespace DO;
/// <summary>
/// A product in the shop
/// </summary>
public struct Product:IComparable<Product>
{
    /// <summary>
    ///Product ID.
    /// </summary>
    public int UniqID { get; set; }
    /// <summary>
    /// The product name.
    /// </summary>
    public string? Name { get; set; }
    /// <summary>
    /// The price of the product.
    /// </summary>
    public double Price { get; set; }
    /// <summary>
    /// To wich category the product belongs.
    /// </summary>
    public Category? Category { get; set; }
    /// <summary>
    /// How much from this product is available.
    /// </summary>
    public int InStock { get; set; }

    public int CompareTo(Product other)
    {
        if(this.Category<other.Category)
            return 1;
        else if(this.Category > other.Category)
            return -1;
        else return 0;
    }

    /// <summary>
    /// Converts the object to a printable form.
    /// </summary>
    /// <returns> the printable form. </returns>
    public override string ToString() => $@"
uniqID: {UniqID}
name: {Name}
category - {Category}
Price: {Price}
Amount in stock: {InStock} 
";

  
}
