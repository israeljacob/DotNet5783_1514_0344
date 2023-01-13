using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Xml;

namespace PL
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    /// 


    public partial class Login : Window
    {
        public Login()
        {
            InitializeComponent();
        }



        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {

            XmlDocument doc = new XmlDocument();
            doc.Load("PL/users.xml");
            doc.Save(Console.Out);

            XmlNodeList Clist = doc.GetElementsByTagName("username");

            var Username = "";
            var Password = "";
            var Error = "";

            foreach (XmlNode node in doc.SelectNodes("//users"))
            {
                Username = node.SelectSingleNode("username").InnerText;
                Password = node.SelectSingleNode("password").InnerText;

                if (Username == txtUsername.Text && Password == txtPassword.Password)
                {
                 //   new .Show();
                }
                else
                {
                    Error = "Invalid username/password";
                    break;
                }
            }
            if (Error != "")
            { MessageBox.Show(Error); }
        }

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
