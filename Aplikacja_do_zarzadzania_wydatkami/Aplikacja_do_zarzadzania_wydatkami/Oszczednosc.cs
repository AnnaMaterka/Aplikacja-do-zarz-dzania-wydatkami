using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplikacja_do_zarzadzania_wydatkami
{
    internal class Oszczednosc: Wydatek
    {
        private decimal kwota;
        private DateTime data;
        private string cel; //lista celów do zrobienia

        public Oszczednosc(decimal kwota, DateTime data, string cel):base(kwota, data)
        {
            this.cel = cel;
        }

        public decimal Kwota1 { get => kwota; set => kwota = value; }
        public DateTime Data1 { get => data; set => data = value; }
        public string Cel { get => cel; set => cel = value; }
    }
}
