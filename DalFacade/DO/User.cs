using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DO;

public struct User
{
    /// <summary>
    /// user ID.
    /// </summary>
    public int UniqID { get; set; }
    /// <summary>
    /// The name of the costomer.
    /// </summary>
    public string? Name { get; set; }
    /// <summary>
    /// The email address of the customer.
    /// </summary>
    public string? Email { get; set; }
    /// <summary>
    /// The address of the customer.
    /// </summary>
    public string? Adress { get; set; }
    public bool IsAdmin { get; set; }

    public string? UserName { get; set; }

    public string? password { get; set; }



    /// <summary>
    /// Converts the object to a printable form.
    /// </summary>
    /// <returns> the printable form. </returns>
    public override string ToString() => $@" 
UniqID: {UniqID}
Name: {Name}, 
Email=  {Email}, 
Adress: {Adress} 
Is admin: {IsAdmin}  
User name: {UserName} 
Password: {password}
";


}
