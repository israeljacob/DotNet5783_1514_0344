
using Dal;
namespace DalTest
{
    public class class1
    {
        static void Main(string[] args)
        {
            Random rand = new Random();
            DateTime date = new DateTime(rand.Next(2000,2022), rand.Next(1, 12), rand.Next(1, 31), 0, 0, 0);

        }
    }


}