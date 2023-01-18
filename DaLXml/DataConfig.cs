using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace DAL;


/// <summary>
/// this class provids a Serials IDs for DAL for order and order item
/// </summary>
public class DataConfig
{
   
    static XElement? Root;
    const string s_dir = @"..\xml\";
    static string s_data = @"data-config.xml";

    /// <summary>
    /// will load the XML file using the LoadData() method, 
    /// and return the value of the "order" element in the XML file.
    /// Before returning the value, the value is incremented and the XML file is saved using the Save() method.
    /// </summary>
    internal static int GetOrderId
    {
        get
        {
            LoadData();
            int id = int.Parse(Root!.Element("order")!.Value);
            Root.Element("order")!.Value = (id + 1).ToString();
            Save();
            return id;
        }
    }

    /// <summary>
    /// will load the XML file using the LoadData() method, 
    /// and return the value of the "orderItem" element in the XML file.
    /// Before returning the value, the value is incremented and the XML file is saved using the Save() method.
    /// </summary>
    internal static int GetOrderItemId
    {
        get
        {
            LoadData();
            int id = int.Parse(Root!.Element("orderItem")!.Value);
            Root.Element("orderItem")!.Value = (id + 1).ToString();
            Save();
            return id;
        }
    }

    /// <summary>
    /// loading the XML file. If the Root variable is not null, it saves the current state of the XML file. 
    /// Then it loads the XML file using the XElement.Load method and assigns the root element to the Root variable. 
    /// In case of any exception, the method throws an exception with the message "Can't load data".
    /// </summary>
    /// <exception cref="Exception"></exception>
    private static void LoadData()
    {
        try
        {
            if (Root != null)
                Root.Save(s_dir + s_data);
            Root = XElement.Load(s_dir+ s_data);
        }
        catch 
        {
            throw new Exception("Can't load data");
        }
    }

    /// <summary>
    /// responsible for saving the XML file. It first checks if the Root variable is not null,
    /// and then saves the XML file using the XElement.Save method. 
    /// In case of an IOException, the method throws an exception with the message "Can't save data".
    /// </summary>
    /// <exception cref="Exception"></exception>
    private static void Save()
    {
        try
        {
            if (Root != null)
                Root.Save(s_dir + s_data);
        }
        catch (IOException)
        {
            throw new Exception("Can't save data");
        }
    }
}
