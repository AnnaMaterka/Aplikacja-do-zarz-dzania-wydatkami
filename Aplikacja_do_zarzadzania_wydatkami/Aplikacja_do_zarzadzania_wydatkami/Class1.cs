using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplikacja_do_zarzadzania_wydatkami
{
    public abstract class Wydatek
    {
        private decimal kwota;
        private DateTime data;
        private string kategoria;

        public decimal Kwota { get => kwota; set => kwota = value; }
        public DateTime Data { get => data; set => data = value; }
        public string Kategoria { get => kategoria; set => kategoria = value; }
    }
}
