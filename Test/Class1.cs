using System;
using ClienApiFb;

namespace Test
{
    public class Class1
    {
        static void Main()
        {
            var c = new Client("https://localhost:44340/swagger/index.html").ParseWebAsync().Result;
            Console.WriteLine(c);
        }
        
    }
}
