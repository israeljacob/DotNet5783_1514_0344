using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    /// <summary>
    /// Categories of clothing.
    /// </summary>
    public enum Category
    {
        Shirts = 1,//חולצות
        trousers,//מכנסיים
        shoes,//נעלים
        coats,//מעילים
        sweaters//סוודרים

    }

    public enum StatusOfOrder
    {
        InProcess=1,
        Orderred,
        Sent,
        Delivered
    }
}
