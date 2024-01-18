using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplikacja_do_zarzadzania_wydatkami
{
    internal abstract class Wplyw
    {
        private decimal kwota;
        private DateTime data;

        protected Wplyw(decimal kwota, DateTime data)
        {
            Kwota = kwota;
            Data = data;
        }

        public decimal Kwota { get => kwota; set => kwota = value; }
        public DateTime Data { get => data; set => data = value; }
    }
}
