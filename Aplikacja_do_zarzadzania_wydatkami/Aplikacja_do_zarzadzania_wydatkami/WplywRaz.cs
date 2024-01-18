using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplikacja_do_zarzadzania_wydatkami
{
    internal class WplywRaz :Wplyw
    {
        private string kategoria;
        public WplywRaz(decimal kwota, DateTime data) : base(kwota, data)
        {
            Kategoria = "Inne";
        }
        public WplywRaz(decimal kwota, DateTime data, string kategoria) : base(kwota, data)
        {
            Kategoria = kategoria;
        }
        public string Kategoria { get => kategoria; set => kategoria = value; }
    }
}
