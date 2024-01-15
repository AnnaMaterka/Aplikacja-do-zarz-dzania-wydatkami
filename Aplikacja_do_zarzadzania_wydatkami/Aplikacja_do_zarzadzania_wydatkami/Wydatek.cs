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

        protected Wydatek(decimal kwota, DateTime data)
        {
            this.kwota = kwota;
            this.data = data;
        }

        public decimal Kwota { get => kwota; set => kwota = value; }
        public DateTime Data { get => data; set => data = value; }
    }
}
