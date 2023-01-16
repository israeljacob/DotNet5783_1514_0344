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
/// this class provids a Serials IDs for DL for Line and UserTrip
/// </summary>
public class SerialNumbers
{

    static XElement? Root;
    const string s_dir = @"..\xml\";
    static string s_data = @"data-config.xml";
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
