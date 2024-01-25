using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplikacja_do_zarzadzania_wydatkami
{
    public class BrakSrodkow : Exception
    {
        public BrakSrodkow() : base() { }
        public BrakSrodkow(string message) : base(message)
        {
            Console.WriteLine(message);
        }
    }
}
