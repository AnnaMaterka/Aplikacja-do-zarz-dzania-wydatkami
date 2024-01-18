using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplikacja_do_zarzadzania_wydatkami
{
    internal class WplywStaly : Wplyw
    {
        public List<string> KategoriwWplywuStalego = new List<string>() { 
            "Pensja", "Alimenty", "Przychody z tytułu najmu", "Kieszonkowe"
        };


        public WplywStaly(decimal kwota, DateTime data) : base(kwota, data)
        {
            
        }

    }
}
