using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplikacja_do_zarzadzania_wydatkami
{
    enum KategoriaWydatku { Spożywcze, Ubrania, Buty, Opłaty, Rozrywka, Elektronika, Uroda, DoDomu, Inne }
    public abstract class Wydatek
    {
        private decimal kwota;
        private DateTime data;
        private string kategoria;

        protected Wydatek(decimal kwota, DateTime data, string kategoria)
        {
            this.kwota = kwota;
            this.data = data;
            this.kategoria = kategoria;
        }

        public decimal Kwota { get => kwota; set => kwota = value; }
        public DateTime Data { get => data; set => data = value; }
        public string Kategoria { get => kategoria; set => kategoria = value; }
    }
}
