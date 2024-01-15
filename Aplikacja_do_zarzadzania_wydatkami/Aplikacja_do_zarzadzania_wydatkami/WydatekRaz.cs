using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplikacja_do_zarzadzania_wydatkami
{
    public enum KategoriaWydatku { Spożywcze, Ubrania, Buty, Opłaty, Rozrywka, Elektronika, Uroda, DoDomu, Inne }
    public class WydatekRaz : Wydatek
    {
        private decimal kwota;
        private DateTime data;
        private KategoriaWydatku kategoria;

        public WydatekRaz(decimal kwota, DateTime data, KategoriaWydatku kategoria) : base(kwota, data)
        {
            this.kategoria = kategoria;
        }
        public KategoriaWydatku Kategoria { get => kategoria; set => kategoria = value; }
    }
}
